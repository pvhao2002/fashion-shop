let categoryEdit = {
    categoryId: 0,
    categoryName: '',
    categoryImage: '',
    status: 'inactive'
};
let isEdit = false;

const cateTitle = document.getElementById('cate-title');
const modalContainer = document.getElementById('model-container');
const modalBody = document.getElementById('model-body');
const preview = document.getElementById('preview');

function openDialog(categoryId) {
    if (!categoryId) {
        cateTitle.textContent = 'Thêm danh mục';
        isEdit = false;
    } else {
        cateTitle.textContent = 'Sửa danh mục';
        isEdit = true;
        categoryEdit.categoryId = categoryId;
    }

    modalContainer.style.removeProperty('display');
    modalBody.style.removeProperty('display');

    modalContainer.style.animationDuration = '100ms';
    modalBody.style.animationDuration = '100ms';

    if (categoryId) {
        $.ajax({
            url: '/Admin/CategoryManagement/GetCategoryById',
            type: 'GET',
            data: {categoryId: categoryId},
            success: function (response) {
                categoryEdit = response.data;

                const img = document.getElementById('preview');
                img.src = categoryEdit.categoryImage;
                img.style.display = 'block';

                document.getElementById('form-category-name').value = categoryEdit.categoryName;
                if (categoryEdit.status === 'ACTIVE') {
                    document.getElementById('status-active').checked = true;
                } else {
                    document.getElementById('status-inactive').checked = true;
                }
            },
            error: function (xhr, status, error) {
            }
        });
    }
}

function closeDialog() {
    modalContainer.style.display = 'none';
    modalBody.style.display = 'none';
}

document.getElementById('upload').addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            categoryEdit.categoryImage = e.target.result;
            preview.src = e.target.result;
            preview.style.display = 'block';
        };
        reader.readAsDataURL(file);
    } else {
        showAlert('Vui lòng chọn file', 'warning');
    }
});

document.getElementById('form-category-name').addEventListener('input', function (event) {
    categoryEdit.categoryName = event.target.value;
});

document.querySelectorAll('input[name="status"]').forEach(function (radio) {
    radio.addEventListener('change', function (event) {
        categoryEdit.status = event.target.value;
    });
});

function insertOrUpdate() {
    $.ajax({
        url: '/Admin/CategoryManagement/InsertOrUpdate',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(categoryEdit),
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                showAlert(response.message, 'warning');
            }
        },
        error: function (xhr, status, error) {
            showAlert(error, 'warning');
        }
    });
}

