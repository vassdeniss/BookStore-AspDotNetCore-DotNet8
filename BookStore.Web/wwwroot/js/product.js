var dataTable;

$(document).ready(function () {
  dataTable = $('#table-data').DataTable({
    ajax: {
      url: '/admin/product/getall',
    },
    columns: [
      { data: 'title', width: '25%' },
      { data: 'isbn', width: '15%' },
      { data: 'listPrice', width: '10%' },
      { data: 'author', width: '15%' },
      { data: 'category.name', width: '10%' },
      {
        data: 'id',
        render: function (data) {
          return `<div class="w-75 btn-group" role="group">
          <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
          <a onClick=deleteProduct('/admin/product/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
          </div>`;
        },
        width: '25%',
      },
    ],
  });
});

function deleteProduct(url) {
  Swal.fire({
    title: 'Are you sure you want to delete this product?',
    text: "You won't be able to revert this!",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, delete it permenantly!',
  }).then((result) => {
    if (result.isDismissed) {
      return;
    }

    $.ajax({
      url: url,
      type: 'DELETE',
      success: function (data) {
        dataTable.ajax.reload();
        toastr.success(data.message);
      },
    });
  });
}
