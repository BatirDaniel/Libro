$('#createAccountButton').on('click', () => {
    Swal.fire({
        icon: 'error',
        title: "Don't have an account ?",
        text: 'Please notify the administrator',
        confirmButtonText: `Okey`
    })
})

document.addEventListener("DOMContentLoaded", function () {
    var getSBtn = document.getElementById("getStartedButton");

    var currentUrl = window.location.pathname;

    if (currentUrl === "/") {
        getSBtn.disabled = false;
    } else {
        getSBtn.disabled = true;
    }
});

$("#menu-button").on("click", () => {
    const isExpanded = menuButton.getAttribute('aria-expanded') === 'true';

    if (isExpanded) {
        menuButton.setAttribute('aria-expanded', 'false');
        $(".absolute").classList.add('hidden');
    } else {
        menuButton.setAttribute('aria-expanded', 'true');
        $(".absolute").classList.remove('hidden');
    }
})