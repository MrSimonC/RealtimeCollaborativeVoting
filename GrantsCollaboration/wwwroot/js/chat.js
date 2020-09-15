"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var userNameClean = user.substring(0, user.indexOf("@"));
    var template = document.getElementById("messageTemplate");
    var list = document.getElementById("chatLog");
    var newMsg = template.content.cloneNode(true);
    var timeNowAmPm = new Date().toLocaleTimeString('en-GB', { hour: "numeric", minute: "numeric", hour12: true });

    newMsg.querySelector('strong').textContent = userNameClean;
    newMsg.querySelector('p').textContent = msg;
    newMsg.querySelector('time').textContent = timeNowAmPm;

    if (loggedInUser === user) { // we are the ones who sent it
        newMsg.querySelector('li').classList.add("hs-chat__message--self");
    }
    list.appendChild(newMsg);
});

connection.on("UpdateCount", function (reviewId, approveCount, rejectCount, approveNames, rejectNames) {
    if (currentReviewId === reviewId) { /* if we're viewing the one which needs updating */
        document.getElementById("approveTotal").innerHTML = approveCount;
        document.getElementById("rejectTotal").innerHTML = rejectCount;
        document.getElementById("approveVoterNames").innerHTML = approveNames;
        document.getElementById("rejectVoterNames").innerHTML = rejectNames;
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("updateCurrentVotes", currentReviewId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("formSendMessage").addEventListener("submit", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("sendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("approveButton").addEventListener("click", function (event) {
    connection.invoke("voteApprove", currentReviewId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    this.disabled = true;
    document.getElementById("rejectButton").disabled = true;
}, { once: true });

document.getElementById("rejectButton").addEventListener("click", function (event) {
    connection.invoke("voteReject", currentReviewId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    this.disabled = true;
    document.getElementById("approveButton").disabled = true;
}, { once: true });


document.getElementById("reveal").addEventListener("click", function () {
    if (this.checked === true) {
        document.getElementById("approveRejectOverall").hidden = false;
    }
    else {
        document.getElementById("approveRejectOverall").hidden = true;
    }
});

document.getElementById("anonVotes").addEventListener("click", function () {
    if (this.checked === true) {
        document.getElementById("approveVoterNames").hidden = true;
        document.getElementById("rejectVoterNames").hidden = true;
        document.getElementById("approveTotalColon").hidden = true;
        document.getElementById("rejectTotalColon").hidden = true;
    }
    else {
        document.getElementById("approveVoterNames").hidden = false;
        document.getElementById("rejectVoterNames").hidden = false;
        document.getElementById("approveTotalColon").hidden = false;
        document.getElementById("rejectTotalColon").hidden = false;
    }
});

