function toggleMobileMenu() {
    document.getElementById("mobileMenu").classList.toggle("show");
}

window.addEventListener('scroll', function () {
    const navbar = document.getElementById("navbar");
    if (window.scrollY > 50) {
        navbar.classList.add("scrolled");
    } else {
        navbar.classList.remove("scrolled");
    }
});