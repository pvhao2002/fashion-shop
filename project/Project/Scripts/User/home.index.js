function openGoogleLoginPopup() {
    const popupUrl = '/User/GoogleLogin';
    const width = 600;
    const height = 600;
    const left = (screen.width - width) / 2;
    const top = (screen.height - height) / 2;

    const popup = window.open(popupUrl, 'Google Login', `width=${width},height=${height},top=${top},left=${left}`);

    // Kiểm tra xem popup có mở thành công không
    if (!popup) {
        showAlert('Vui lòng cho phép mở popup cho trang web này');
    }
}

document.getElementById('signup-form').addEventListener('submit', function (event) {
    event.preventDefault(); // Ngăn không cho gửi form
    const password = document.getElementById('signup-password').value;
    const confirmPassword = document.getElementById('signup-password-confirm').value;
    if (password !== confirmPassword) {
        showAlert('Mật khẩu không khớp', 'danger');
    } else {
        showLoading();
        const action = this.getAttribute('action');
        const method = this.getAttribute('method');
        const formData = new FormData(this);
        fetch(action, {
            method: method,
            body: formData
        }).then(response => response.json())
            .then(data => {
                hideLoading();
                if (data.success) {
                    showAlert('Đăng ký thành công', 'success');
                    const modal = bootstrap.Modal.getInstance(document.getElementById('signup-modal'));
                    modal.hide();
                    document.getElementById('formSignupOpenSignIn').click();
                } else {
                    showAlert(data.message, 'danger');
                }
            })
            .catch(_ => {
                showAlert('Đã xảy ra lỗi', 'danger');
            });
    }
});

document.getElementById('form-login').addEventListener('submit', function (event) {
    event.preventDefault();
    showLoading();
    const action = this.getAttribute('action');
    const method = this.getAttribute('method');
    const formData = new FormData(this);

    // validate form
    const email = formData.get('email');
    const password = formData.get('password');
    if (!email || !password) {
        hideLoading();
        showAlert('Vui lòng nhập đầy đủ thông tin', 'danger');
        return;
    }

    fetch(action, {
        method: method,
        body: formData
    }).then(response => response.json())
        .then(data => {
            hideLoading();
            if (data.success) {
                showAlert('Đăng nhập thành công', 'success');
                window.location.href = data.area === 'user' ? '/User/Home/Index' : '/Admin/HomeAdmin/Index';
            } else {
                showAlert(data.message, 'danger');
            }
        })
        .catch(_ => {
            showAlert('Đã xảy ra lỗi', 'danger');
        });
});

document.getElementById('form-forget').addEventListener('submit', function (event) {
    event.preventDefault();
    showLoading();
    const action = this.getAttribute('action');
    const method = this.getAttribute('method');
    const formData = new FormData(this);

    // validate form
    const email = formData.get('email');
    if (!email) {
        hideLoading();
        showAlert('Vui lòng nhập email', 'danger');
        return;
    }

    fetch(action, {
        method: method,
        body: formData
    }).then(response => response.json())
        .then(data => {
            hideLoading();
            if (data.success) {
                showAlert('Kiểm tra email để lấy lại mật khẩu', 'success');
                const modal = bootstrap.Modal.getInstance(document.getElementById('forget-modal'));
                modal.hide();
            } else {
                showAlert(data.message, 'danger');
            }
        })
        .catch(_ => {
            showAlert('Đã xảy ra lỗi', 'danger');
        });
});