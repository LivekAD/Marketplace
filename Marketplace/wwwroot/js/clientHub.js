"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");    
    document.getElementById("messages").appendChild(li);
    li.textContent = `${user} says ${message}`;
    
});

connection.start().then(function () {
    return console.log("SignalR connected!");
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var messageInput = document.getElementById("message-input");
    var message = messageInput.value;
    connection.invoke("SendMessage", myModel.id, userNameSender, myModel.ownerName, message).catch(function (err) {
        return console.error(err);
    });
    messageInput.value = "";

    event.preventDefault();
});
