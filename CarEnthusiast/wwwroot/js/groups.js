var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

const joinButtons = document.querySelectorAll(".joinButton"); 

window.onload = () => {
    joinButtons.forEach(function (button) {
        button.addEventListener("click", function (event) {
            var groupCard = button.closest(".card"); // Get the closest parent card element
            var groupName = groupCard.querySelector(".card-title").textContent; // Get the group name
            var groupIdStr = groupCard.querySelector("input[type=hidden]").value; // Get the group ID
            var groupId = parseInt(groupIdStr, 10);

            console.log(`Group ${groupName} join attempt`);
            window.alert(`Successfully joined ${groupName}`);

            connection.invoke("JoinGroup", groupId, groupName).catch(function (err) {
                console.log("Group join failed due to", err);
                return console.error(err.toString());
            });
        });
    });
};

// Handle creating groups
document
    .getElementById("createGroupButton")
    .addEventListener("click", function (event) {
        event.preventDefault();
        var groupName = document.getElementById("groupName").value;
        var groupDesc = document.getElementById("groupDesc").value;
        var groupImageInput = document.getElementById("groupImage").files[0];

        let base64String = "";
        let reader = new FileReader();
        console.log("next");

        reader.onload = function () {
            base64String = reader.result.replace("data:", "").replace(/^.+,/, "");
            imageBase64Stringsep = base64String;

            // Invoke CreateGroup method with the base64 image
            connection
                .invoke("CreateGroup", groupName, groupDesc, base64String.toString())
                .catch(function (err) {
                    return console.log(err.toString());
                });

            // Close the modal
            $("#createGroupModal").modal("hide");
        };
        reader.readAsDataURL(groupImageInput);
    });

// Add an event listener to the modal to clear the form when it is hidden
$("#createGroupModal").on("hidden.bs.modal", function () {
    document.getElementById("createGroupForm").reset();
});

// Handle the GroupAlreadyExists event
connection.on("GroupAlreadyExists", function (groupName) {
    // Show an alert or update the UI to inform the user
    window.alert(
        `The group name '${groupName}' already exists. Please choose a different name.`
    );
});

// Handle the AlreadyJoinedGroup event
connection.on("AlreadyJoinedGroup", function (groupId) {
    var groupCard = document
        .querySelector(`input[value='${groupId}']`)
        .closest(".card");
    var groupName = groupCard.querySelector(".card-title").textContent;
    window.alert(`You are already in ${groupName}`);
});

// Handle the view Messages link click
var viewMessageLinks = document.querySelectorAll(".view-messages-link");
viewMessageLinks.forEach(function (link){
    link.addEventListener("click", function (event) {
        event.preventDefault();

        var groupCard = link.closest(".card");
        var groupIdStr = groupCard.querySelector("input[type=hidden]").value;
        var groupId = parseInt(groupIdStr, 10);

        connection.invoke("CheckUserInGroup", groupId).catch(function (err) {
            console.log("Validation check failed due to", err);
            return console.log(err.toString());
        });
});
});

connection.on('UserInGroup', function (response) {

    viewMessageLinks.forEach(function (link) {

    link.addEventListener('click', function (event) {
        event.preventDefault();
    console.log(response);
        var groupCard = link.closest(".card");
        var groupIdStr = groupCard.querySelector("input[type=hidden]").value;
        var groupName = groupCard.querySelector(".card-title").textContent;
        var groupId = parseInt(groupIdStr, 10);
    if (response) {
        //User is in the group, navigate to the "Chat" action in the correct group
        window.location.href = `/Chat/Chat?groupId=${groupId}`;
    } else if (!response) {
        //User is not in the group, show an alert
        window.alert(`You are not in group ${groupName}`);
        
    }
        });
    });
});

connection
    .start()
    .then(function () {
        console.log("SignalR Connection started successfully");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });