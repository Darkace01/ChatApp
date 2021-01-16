// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// var createRoomBtn = document.getElementById('create-room-btn')
// var createRooModal = document.getElementById('create-room-modal')

// createRoomBtn.addEventListener('click', function(){
//     createRooModal.classList.add('active')
// })

// function closeModal(){
//     createRooModal.classList.remove('active')
// }

document.addEventListener('DOMContentLoaded', function () {

    if ('serviceWorker' in navigator) {
        // console.log('Service Worker is supported');
        navigator.serviceWorker.register('/pwa-serviceworker.js')
            .then(function (swReg) {
                //Do Something Here
                // console.log('Service Worker is registered from site.js', swReg);
            })
            .catch(function (error) {
                console.error('Service Worker Error from site.js', error);
            });
    }
    else {
        console.error('Service Worker Not Supported');
    }

}, false);