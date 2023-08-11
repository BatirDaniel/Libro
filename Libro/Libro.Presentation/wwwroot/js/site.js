$('#createAccountButton').on('click', () => {
    Swal.fire({
        icon: 'error',
        title: "Don't have an account ?",
        text: 'Please notify the administrator',
        confirmButtonText: `Okey`
    })
})

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