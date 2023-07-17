"use strict";
var send = document.getElementById("sendButton");
var messages = document.getElementById("messageList");

var connection = new signalR.HubConnectionBuilder().withUrl("Views/Home/ChatHub").build();

//Disable the send button until connection is established
send.disables = true;

connection.on("ReceiveMessage", function (user, message) {
    var list = document.createElement("li");
    messages.appendChild(list);
    list.testContent = `${user} : ${message}`;
});

connection.start().then(function () {
    send.disabled = false;
}).catch(function (Ex) {
    return Console.Error(Ex.ToString());
});


send.addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("Send Message", user, message).catch(function (Ex) {
        return console.error(Ex.ToString());
    });

    event.preventDefault();
});
