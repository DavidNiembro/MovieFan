document.addEventListener('DOMContentLoaded', function () {

    cmdEdit.addEventListener('click', function () {
        console.log('clic')
        divShow.classList.add('d-none')
        divEdit.classList.remove('d-none')
    })

    cmdCancel.addEventListener('click', function () {
        divShow.classList.remove('d-none')
        divEdit.classList.add('d-none')
    })

    cmdDelete.addEventListener('click', function () {
        frmDelete.submit()
    })
})