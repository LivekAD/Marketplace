"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    li.textContent = `says ${message}`;
    document.getElementById("messages").appendChild(li);
});

connection.start().then(function () {
    return console.log("SignalR connected!");
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var messageInput = document.getElementById("message-input");
    var message = messageInput.value;
    connection.invoke("SendMessage", myModel.id, myGroupName[0].groupName, userNameSender, myModel.ownerName, message).catch(function (err) {
        return console.error(err);
    });
    messageInput.value = "";

    event.preventDefault();
});
