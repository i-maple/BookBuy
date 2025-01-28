$(document).ready(() => {
    loadDataTable();
});

let dataTable;

function loadDataTable() {
    dataTable = $("#dataTable").DataTable({
        "ajax": "/Admin/Product/GetAll",
        "columns": [
            {
                "data": "title"
            },
            {
                "data": "author"
            },
            {
                "data": "isbn"
            },
            {
                "data": "price"
            },
            {
                "data": "description"
            },
            {
                "data": "category.name"
            },
            {
                "data":"coverType.name"
            },
            {
                "data": "id",
                "render": function (id) {
                    return `
                    <div class="btn-group">
					<button class="btn btn-warning">
						<a href="Product/Upsert?id=${id}" class="text-white">
							Edit
						</a>
					</button>
					<button class="btn btn-danger">
						<a onclick='deleteProduct("Product/DeletePOST?id=${id}")' class="text-white">
							Delete
						</a>
					</button>
				</div>
                    `
                }
            }
        ]
    });
}

function deleteProduct(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload()
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                    }
                    else {
                        Swal.fire({
                            title: "Failed!",
                            text: "Failed Deleting File. Please Try Again",
                            icon: "error"
                        });
                    }
                }
            })
        }
    });
}