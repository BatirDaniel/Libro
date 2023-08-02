$('#createAccountButton').on('click', () => {
    Swal.fire({
        icon: 'error',
        title: "Don't have an account ?",
        text: 'Please notify the administrator',
        confirmButtonText: `Okey`
    })
})

$('#getStartedButton').on('click', () => {
    location.href='auth/login'
})

$('#signInButton').on('click', () => {
})