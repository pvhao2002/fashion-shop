// FilePond
const pp = inputPondElement[0];
const postForm = document.getElementById('post-form');
postForm.addEventListener('submit', (e) => {
    e.preventDefault();
    const formData = new FormData(postForm); // Lấy dữ liệu từ form
    let fileReadCount = 0;
    const files = pp.getFiles().map(file => file.file); // Lấy danh sách file

    if (files.length > 0) {
        for (let i = 0; i < files.length; i++) {
            const reader = new FileReader();
            reader.onload = (e) => {
                const base64 = e.target.result;
                formData.delete('files'); // Xóa file để tránh gửi lên server
                formData.append('ImageUrls', JSON.stringify(base64)); // Thêm mảng base64 vào form data
                fileReadCount++;
                if (fileReadCount === files.length) {
                    // Gửi dữ liệu lên server
                    sendRequest(formData);
                }
            };
            reader.readAsDataURL(files[i]); // Đọc file dưới dạng base64
        }
    } else {
        // Nếu không có file thì chỉ gửi dữ liệu form
        formData.append('ImageUrls', JSON.stringify([])); // Thêm mảng rỗng nếu không có hình

        // Gửi dữ liệu form lên server
        sendRequest(formData);
    }
});

function sendRequest(formData) {
    showLoading();
    const newFormData = new FormData();
    for (const [key, value] of formData.entries()) {
        const newKey = key.replace(/^Post\./, ''); // Xóa "Post." ở đầu tên thuộc  tính bởi vì sử dụng Html.TextBoxFor của ASP.NET nên tên thuộc tính sẽ có dạng "Post.PropertyName"
        newFormData.append(newKey, value); // Thêm vào newFormData
    }
    fetch('/User/Post/Create', {
        method: 'POST',
        body: newFormData
    })
        .then(response => response.json())
        .then(data => {
            hideLoading();
            showAlert('Đăng bài thành công', 'success');
            setTimeout(() => {
                window.location.href = '/User/Post/Index';
            }, 600);
        })
        .catch((error) => {
            showAlert(error, 'danger');
        });
}