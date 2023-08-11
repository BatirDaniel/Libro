$('#forgotPassword').on('click', () => {
    Swal.fire({
        title: 'Forgot password',
        input: 'Send a message to admin',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Send',
        showLoaderOnConfirm: true,
        preConfirm: (login) => {
            return fetch(`//api.github.com/users/${login}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(response.statusText)
                    }
                    return response.json()
                })
                .catch(error => {
                    Swal.showValidationMessage(
                        `Request failed: ${error}`
                    )
                })
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Your message has been send',
                showConfirmButton: false,
                timer: 1500
            })
        }
    })
})

$('#createAccount').on('click', () => {
    Swal.fire({
        icon: 'error',
        title: "Don't have an account ?",
        text: 'Please notify the administrator',
        confirmButtonText: `Okey`
    })
})