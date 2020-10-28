"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/devicesHub").build();

connection.on("UpdateDeviceStatus", function (deviceId, pollTime) {
    var pollTimeSpanId = "last-polled-" + deviceId;
    var pollTimeSpan = document.getElementById(pollTimeSpanId);

    if (pollTimeSpan) {
        pollTimeSpan.innerHTML = "";
        pollTimeSpan.appendChild(document.createTextNode("Last polled: " + pollTime));
        return;
    }

    createNewDeviceRecord(deviceId, pollTime, pollTimeSpanId);
});

connection.on("ChangeConnectionStatus", function (deviceId, connected) {
    var statusSpan = document.getElementById("status-" + deviceId);

    if (statusSpan) {
        var connectedMessage = connected ? "Connected" : "Disconnected";

        statusSpan.innerHTML = "";
        statusSpan.appendChild(document.createTextNode(" | Status: " + connectedMessage));
    }
});

connection.start().then(function () {
    connection.invoke("RegisterAsManager").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

function createNewDeviceRecord(deviceId, pollTime, pollTimeSpanId) {
    // Ensure that deviceId doesn't brake HTML
    var encodedDeviceId = deviceId.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var div = document.createElement("div");
    div.id = "device-" + encodedDeviceId;
    var deviceSpan = document.createElement("span");
    deviceSpan.appendChild(document.createTextNode("Device: " + encodedDeviceId + " | "));
    div.appendChild(deviceSpan);
    var pollTimeSpan = document.createElement("span");
    pollTimeSpan.id = pollTimeSpanId;
    pollTimeSpan.appendChild(document.createTextNode("Last polled: " + pollTime));
    div.appendChild(pollTimeSpan);
    var statusSpan = document.createElement("span");
    statusSpan.id = "status-" + deviceId;
    statusSpan.appendChild(document.createTextNode(" | Status: Connected"));
    div.appendChild(statusSpan);
    document.getElementById("deviceList").appendChild(div);
}