function showHideLeftMenu() {
    $(".v-sidebar-menu.employer.vsm_expanded")
        .toggleClass("show_sidebar close_sidebar");

    $(".dashboard-content")
        .toggleClass("dash_show_sidebar dash_close_sidebar");
}

// Gọi hàm khi phần tử toggleButton được nhấn
$("#toggleButton").click(function() {
    showHideLeftMenu();
});

function showNotification(element) {
    var dropdownMenu = element.nextElementSibling;
    if (dropdownMenu.style.display === "none" || dropdownMenu.style.display === "") {
        dropdownMenu.style.display = "block";
    } else {
        dropdownMenu.style.display = "none";
    }
}

function showSubItem(element) {
    const itemContainer = $(element).closest('.vsm--item');

    itemContainer.toggleClass("vsm--item_open");

    const icon = $(element).find('i.fa-angle-right, i.fa-angle-down');

    if (icon.hasClass('fa-angle-right')) {
        icon.removeClass('fa-angle-right').addClass('fa-angle-down');
        itemContainer.find('.vsm--dropdown').css('display', 'block');
    } else {
        icon.removeClass('fa-angle-down').addClass('fa-angle-right');
        itemContainer.find('.vsm--dropdown').css('display', 'none');
    }
}