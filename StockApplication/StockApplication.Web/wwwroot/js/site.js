// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, date) {
    var mainDiv = document.createElement("div");
    div.classList.add("col-10", "justify-content-start", "mb-4");

    var col = document.createElement("div");
    div.classList.add("col-8");

    var messageBox = document.createElement("div");
    messageBox.classList.add("bg-light", "text-white", "rounded", "border-success", "p-2");

    var messageText = document.createElement("p");

    document.getElementById("messagesContainer")
        .appendChild(mainDiv)
        .appendChild(col)
        .appendChild(messageBox)
        .appendChild(messageText);

    messageText.textContent = user + " says " + message + " at " + date;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("stockCode").value;
    connection.invoke("SendMessageAsync", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});