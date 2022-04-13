"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

document.getElementById("sendButton").classList.add("disabled");

async function start() {
    try {
        await connection.start();
        console.assert(connection.state === signalR.HubConnectionState.Connected);
        console.log("SignalR Connected.");
        document.getElementById("sendButton").classList.remove("disabled");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

document.getElementById("sendButton").addEventListener("click", function (event) {
    var msg = document.getElementById("stockCode").value;
    sendMessage(msg);
    event.preventDefault();
});

async function sendMessage(msg) {
    try {
        await connection.invoke("SendMessageAsync", msg);
    } catch (err) {
        console.error(err);
    }
}

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

// Start the connection
start();