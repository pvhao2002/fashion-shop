let userEdit = undefined;

function openDialog(accountId) {
    const modalContainer = document.getElementById('model-container');
    const modalBody = document.getElementById('model-body');

    modalContainer.style.removeProperty('display');
    modalBody.style.removeProperty('display');

    modalContainer.style.animationDuration = '100ms';
    modalBody.style.animationDuration = '100ms';

    $.ajax({
        url: '/Admin/UserManagement/GetUserById',
        type: 'GET',
        data: { accountId: accountId },
        success: function(response) {
            if (response.success) {
                userEdit = response.data;
                bindFormData()
            } else {
                alert(response.message);
            }
        },
        error: function(xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}

function closeDialog() {
    document.getElementById('model-container').style.display = 'none';
    document.getElementById('model-body').style.display = 'none';
}

function bindFormData(user) {
    document.getElementById('form-user-name').value = userEdit.Username;

    document.getElementById('form-email').value = userEdit.Email;

    if (userEdit.Status === 'ACTIVE') {
        document.getElementById('status-active').checked = true;
    } else {
        document.getElementById('status-inactive').checked = true;
    }
}

function saveUser() {
    $.ajax({
        url: '/Admin/UserManagement/EditUser',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(userEdit),
        success: function(response) {
            if(response.success) {
                location.reload();
            } else {
                console.error(response.message); // Xử lý lỗi từ server
            }
        },
        error: function(xhr, status, error) {
            console.error("Error: " + error); // Xử lý lỗi nếu có
        }
    });
}

document.getElementById('form-user-name').addEventListener('input', function(event) {
    userEdit.Username = event.target.value;
});
document.getElementById('form-email').addEventListener('input', function(event) {
    userEdit.Emai = event.target.value;
});
document.querySelectorAll('input[name="status"]').forEach(function(radio) {
    radio.addEventListener('change', function(event) {
        userEdit.Status = event.target.value;
    });
});