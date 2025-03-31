// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Auto click on the cross icon to trigger close button
$(document).ready(function () {
    var element = $("#Layer_1");
    if (element.length && element.parent().length) {
        element.parent().trigger("click");
    }
});


// ************ Connect for SignalR ************
const connection = new signalR.HubConnectionBuilder()
.withUrl("/leaveNotificationHub")
.build();

// Listen for notifications
connection.on("ReceiveLeaveStatus", (message) => {
    //showFloatingMessage(message);
    $.notify(message, "info");
});

// Start the connection
connection.start()
    .then(() => console.log("SignalR connection established."))
    .catch(err => console.error(err.toString()));


// ************ Notification Message Box ************
function showFloatingMessage(message) {
    // Create the dark background overlay
    let overlay = document.createElement("div");
    overlay.style.position = "fixed";
    overlay.style.top = "0";
    overlay.style.left = "0";
    overlay.style.width = "100%";
    overlay.style.height = "100%";
    overlay.style.backgroundColor = "rgba(0, 0, 0, 0.5)"; 
    overlay.style.zIndex = "9998";
    overlay.style.opacity = "0";
    overlay.style.transition = "opacity 0.5s ease-in-out";

    // Create the message bubble
    let bubble = document.createElement("div");
    bubble.innerText = message;
    bubble.style.position = "fixed";
    bubble.style.top = "-100px"; // Start off-screen at the top
    bubble.style.left = "50%";
    bubble.style.transform = "translateX(-50%)";
    bubble.style.backgroundColor = "#FFFFFF"; 
    bubble.style.color = "#000000";
    bubble.style.padding = "10px 20px";
    bubble.style.borderRadius = "10px";
    bubble.style.boxShadow = "0 4px 10px rgba(0, 0, 0, 0.2)";
    bubble.style.zIndex = "9999"; // Above the overlay
    bubble.style.fontFamily = "Arial, sans-serif";
    bubble.style.fontSize = "16px";
    bubble.style.transition = "top 0.5s ease-in-out, opacity 0.5s ease-in-out";
    bubble.style.opacity = "0";

    // Append the overlay and bubble to the body
    document.body.appendChild(overlay);
    document.body.appendChild(bubble);

    // Trigger the animation after a short delay to allow the DOM to update
    setTimeout(() => {
        overlay.style.opacity = "1"; 
        bubble.style.top = "50px"; 
        bubble.style.opacity = "1";
    }, 10);

    // Fade out and remove the bubble and overlay after 3 seconds
    setTimeout(() => {
        bubble.style.opacity = "0"; 
        overlay.style.opacity = "0";
    }, 3000);

    // Remove the bubble and overlay from the DOM after the fade-out animation completes
    setTimeout(() => {
        bubble.remove();
        overlay.remove();
    }, 3500);
}