// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//install service worker
installServiceWorker();

function installServiceWorker() {
    if ('serviceWorker' in navigator) {
        window.addEventListener("load", () => {
            navigator.serviceWorker.register("/js/ServiceWorker.js")
                .then((reg) => {
                    // checks if notifications are enabled
                    if (Notification.permission === "granted") {
                        // if they are show the form and get all subscriptions
                        $("#notifications-enabled").show();
                        getSubscription(reg);
                        // if they are blocked, don't show the form and notify user notifications are disabled
                    } else if (Notification.permission === "blocked") {
                        $("#no-support").show();
                        // show a button for enablining nofications (might be useful)
                    } else {
                        requestNotificationAccess(reg)
                        $("#notifications-disabled").show();
                        setTimeout(installServiceWorker, 5000);
                        // $("#GiveAccess").show();
                        // $("#PromptForAccessBtn").onclick(() => requestNotificationAccess(reg));
                    }
                });
        });
        // all else fails, show no support
    } else {
        $("#no-support").show();
    }
}


//requesting notifications button
function requestNotificationAccess(reg) {
    Notification.requestPermission(function (status) {
        if (status == "granted") {
            $("#notifications-enabled").show();
            getSubscription(reg);
        } else {
            $("#no-support").show();
        }
    });
}

// gets all of the subscriptions 
function getSubscription(reg) {
    reg.pushManager.getSubscription().then(function (sub) {
        if (sub === null) {
            reg.pushManager.subscribe({
                userVisibleOnly: true,
                applicationServerKey: "@ViewBag.applicationServerKey"
            }).then(function (sub) {
                fillFormFields(sub);
            }).catch(function (e) {
                console.error("Unable to subscribe to push", e);
            });
        } else {
            fillFormFields(sub);
        }
    });
}

function fillFormFields(sub) {
    $("#endpoint").val(sub.endpoint);
    $("#p256dh").val(arrayBufferToBase64(sub.getKey("p256dh")));
    $("#auth").val(arrayBufferToBase64(sub.getKey("auth")));
}

function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

//check that the user has enabled notifications
