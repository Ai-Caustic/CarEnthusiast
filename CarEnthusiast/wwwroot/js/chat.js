"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

//Disable send button until connection is established
document.getElementById("sendBtn").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
  var userName = user.split("@")[0];
  //displayMessage(userName, message);
});

connection
  .start()
  .then(function () {
    document.getElementById("sendBtn").disabled = false;
    console.log("Connection started");
  })
  .catch(function (err) {
    return console.error(err.toString());
  });

connection.onclose(function (err) {
  console.error("Connection closed due to:", err.toString());
});

document.getElementById("sendBtn").addEventListener("click", function (event) {
  if (connection.state == signalR.HubConnectionState.Connected) {
    var message = document.getElementById("txtmessage").value;
    connection.invoke("SendMessage", message).catch(function (err) {
      return console.error(err.toString());
    });
  } else {
    console.error("Connection State: False");
  }
  event.preventDefault();
});


