$("#ImageFiles").on("change", function (event) {
    const files = event.target.files;
    $("#previewList").empty();

    Array.from(files).forEach(file => {
        const reader = new FileReader();
        reader.onload = function (e) {
            $("#previewList").append(`
                <img src="${e.target.result}" style="width:100px; height:100px; object-fit:cover; margin:5px; border:1px solid #ccc;" />
            `);
        };
        reader.readAsDataURL(file);
    });
});


function insertProduct() {
    const formData = new FormData();
    formData.append("CategoryId", $("#CategoryId").val());
    formData.append("Name", $("#Name").val());
    formData.append("Description", $("#Description").val());
    formData.append("Price", $("#Price").val());
    formData.append("Stock", $("#Stock").val());
    formData.append("Status", $("#Status").val());

    const files = $("#ImageFiles")[0].files;
    for (let i = 0; i < files.length; i++) {
        formData.append("Images", files[i]); // gửi nhiều file
    }

    $.ajax({
        url: '/Admin/ProductManagement/Insert',
        type: 'POST',
        contentType: false,   // ❌ không phải application/json
        processData: false,   // ❌ không cho jQuery biến FormData thành string
        data: formData,
        success: function (response) {
            if (response.success) {
                alert(response.message);
                window.location.href = "/Admin/ProductManagement/Index";
            } else {
                alert(response.message);
            }
        },
        error: function (xhr, status, error) {
            alert("Lỗi: " + error);
        }
    });
}

function showImages(button) {
    const images = JSON.parse($(button).attr("data-images"));
    var carouselInner = $("#carouselInner");
    carouselInner.empty();

    if (images && images.length > 0) {
        images.forEach(function (url, index) {
            carouselInner.append(`
                <div class="carousel-item ${index === 0 ? 'active' : ''}">
                    <img src="${url}" class="d-block w-100" style="height:500px; object-fit:contain;" />
                </div>
            `);
        });
    } else {
        carouselInner.append("<p>Không có ảnh cho sản phẩm này.</p>");
    }

    $("#imageModal").modal("show");
    $('.modal-backdrop').remove();
}

function openUpdateModal(button) {
    const product = JSON.parse($(button).attr("data-product"));

    $("#UpdateProductId").val(product.ProductId);
    $("#UpdateName").val(product.Name);
    $("#CategoryId").val(product.CategoryId);
    $("#UpdateDescription").val(product.Description);
    $("#UpdatePrice").val(product.Price);
    $("#UpdateStock").val(product.Stock);
    $("#UpdateStatus").val(product.Status);
    

    $("#updateProductModal").modal("show");
    $('.modal-backdrop').remove();
}

function updateProduct() {
    const product = {
        ProductId: $("#UpdateProductId").val(),
        CategoryId: $("#CategoryId").val(),
        Name: $("#UpdateName").val(),
        Description: $("#UpdateDescription").val(),
        Price: $("#UpdatePrice").val(),
        Stock: $("#UpdateStock").val(),
        Status: $("#UpdateStatus").val()
    };

    $.ajax({
        url: '/Admin/ProductManagement/Update',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(product),
        success: function (response) {
            if (response.success) {
                alert(response.message);
                location.reload();
            } else {
                alert(response.message);
            }
        },
        error: function (xhr, status, error) {
            alert("Lỗi: " + error);
        }
    });
}
