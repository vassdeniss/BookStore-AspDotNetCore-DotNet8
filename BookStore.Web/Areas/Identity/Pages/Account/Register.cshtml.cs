#nullable disable

using BookStore.Common;
using BookStore.Infrastructure.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BookStoreUser> signInManager;
        private readonly UserManager<BookStoreUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IUserStore<BookStoreUser> userStore;
        private readonly IUserEmailStore<BookStoreUser> emailStore;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<BookStoreUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IUserStore<BookStoreUser> userStore,
            SignInManager<BookStoreUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userStore = userStore;
            this.emailStore = this.GetEmailStore();
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string? Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> Roles { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.Input = new InputModel()
            {
                Roles = this.roleManager.Roles
                    .Select((role) => role.Name)
                    .Select((name) => new SelectListItem
                    {
                        Text = name,
                        Value = name,
                    })
            };

            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                BookStoreUser user = this.CreateUser();

                await this.userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
                await this.emailStore.SetEmailAsync(user, this.Input.Email, CancellationToken.None);
                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    if (!string.IsNullOrEmpty(this.Input.Role))
                    {
                        await this.userManager.AddToRoleAsync(user, this.Input.Role);
                    }
                    else
                    {
                        await this.userManager.AddToRoleAsync(user, Roles.Customer);
                    }

                    string userId = await this.userManager.GetUserIdAsync(user);
                    string code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    string callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }
                foreach (IdentityError error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        private BookStoreUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<BookStoreUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<BookStoreUser> GetEmailStore()
        {
            if (!this.userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<BookStoreUser>)this.userStore;
        }
    }
}
