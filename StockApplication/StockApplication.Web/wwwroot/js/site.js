"use strict";

// Creates connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

var sendButton = document.getElementById("sendButton");
var textInput = document.getElementById("stockCode");
sendButton.classList.add("disabled");

// Starts connection and disable send button until the connection is established
async function start() {
    try {
        await connection.start();
        console.assert(connection.state === signalR.HubConnectionState.Connected);
        sendButton.classList.remove("disabled");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

// Takes value from text input and calls the hub method
document.getElementById("sendButton").addEventListener("click", function (event) {
    var msg = textInput.value;
    sendMessage(msg);
    event.preventDefault();
});

// Calls the hub method with the message that was previously taken
async function sendMessage(msg) {
    try {
        await connection.invoke("SendMessageAsync", msg);
        textInput.value = "";
    } catch (err) {
        console.error(err);
    }
}

// Handles the UI behavior when receiving data from hub
connection.on("ReceiveMessage", function (user, message, date) {
    var mainDiv = document.createElement("div");
    mainDiv.classList.add("col-10", "justify-content-start", "mb-4");

    var messageBox = document.createElement("div");
    messageBox.classList.add("bg-info", "text-white", "rounded", "border-success", "p-2");

    var messageText = document.createElement("p");

    document.getElementById("messagesContainer")
        .appendChild(mainDiv)
        .appendChild(messageBox)
        .appendChild(messageText);

    messageText.textContent = user + " says " + message + " at " + date;
});

connection.onclose(async () => {
    await start();
});

// Starts the connection
start();