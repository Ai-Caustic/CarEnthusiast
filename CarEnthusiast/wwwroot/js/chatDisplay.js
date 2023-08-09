//This is code to fetch messages from the server and display them without refreshing the page

//TODO : Implement in groupchat.cshtml


// Function to fetch latest messages from the server using AJAX
function fetchLatestMessages(groupId) {
    fetch("/YourController/GetLatestGroupMessages?groupId=" + groupId)
        .then(response => response.json())
        .then(messages => {
            var ul = document.getElementById("groupMessages");
            ul.innerHTML = ""; // Clear the existing message list
            messages.forEach(message => {
                var li = document.createElement("li");
                li.textContent = message.userName + ": " + message.text;
                ul.appendChild(li);
            });
        })
        .catch(error => console.error(error));
}

// Fetch latest messages every 5 seconds 
setInterval(function () {
    var groupId = @Model.Id;
    fetchLatestMessages(groupId);
}, 5000); // Fetch every 5 seconds 



