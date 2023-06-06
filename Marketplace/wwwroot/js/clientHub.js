"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();

connection.on("ReceiveMessage", function (message) {
    var div = document.createElement("div");
    div.classList.add('bubble');
    div.classList.add('me');
    div.textContent = `${message}`;
    var chatGroupId = groupName; // Встановіть значення chatGroupId відповідно до вашого коду
    var chatSelector = '.chat[data-chat="' + chatGroupId + '"]';
    var chatElement = document.querySelector(chatSelector);
    if (chatElement) {
        chatElement.appendChild(div);
    }
});

connection.start().then(function () {
    return console.log("SignalR connected!");
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var messageInput = document.getElementById("message-input");
    var message = messageInput.value;
    connection.invoke("SendMessage", productId, groupName, userNameSender, owner, message).catch(function (err) {
        return console.error(err);
    });
    messageInput.value = "";

    event.preventDefault();
});
