(function () {
    window.onload = function () {
        const preloader = document.querySelector('.page-loading');
        preloader.classList.remove('active');
    };
})();

(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
        new Date().getTime(),event:'gtm.js'});
    const f = d.getElementsByTagName(s)[0],
        j = d.createElement(s), dl = l !== 'dataLayer' ? '&l=' + l : '';j.async=true;j.src=
    '../www.googletagmanager.com/gtm5445.html?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-WKV3GT5');

function showLoading() {
    const preloader = document.querySelector('.page-loading');
    preloader.classList.add('active');
}

function hideLoading() {
    const preloader = document.querySelector('.page-loading');
    preloader.classList.remove('active');
}