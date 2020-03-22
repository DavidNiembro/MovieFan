document.addEventListener('DOMContentLoaded', function () {
    if (document.getElementById('flashmessage'))
        setTimeout(function () {
            flashmessage.classList.add('d-none')
        }, 3000)
})