var websocket = null;
var uuid = null;
var inInfo = null;
var actionInfo = {};
var settingsModel = {};

function connectElgatoStreamDeckSocket(inPort, inUUID, inRegisterEvent, _inInfo, inActionInfo) {uuid = inUUID;
    actionInfo = JSON.parse(inActionInfo);
    inInfo = JSON.parse(_inInfo);
    websocket = new WebSocket('ws://localhost:' + inPort);

    //initialize values
    if (actionInfo.payload.settings.settingsModel) {
        settingsModel.AwsAccessKeyId = actionInfo.payload.settings.settingsModel.AwsAccessKeyId;
settingsModel.AwsAccessKeySecret = actionInfo.payload.settings.settingsModel.AwsAccessKeySecret;
settingsModel.AwsRegion = actionInfo.payload.settings.settingsModel.AwsRegion;
settingsModel.AwsAlarmName = actionInfo.payload.settings.settingsModel.AwsAlarmName;
    } else {
		settingsModel.AwsAccessKeyId = "";
settingsModel.AwsAccessKeySecret = "";
settingsModel.AwsRegion = "";
settingsModel.AwsAlarmName = "";        
    }

	document.getElementById('txtAwsAccessKeyId').value = settingsModel.AwsAccessKeyId;
document.getElementById('txtAwsAccessKeySecret').value = settingsModel.AwsAccessKeySecret;
document.getElementById('txtAwsRegion').value = settingsModel.AwsRegion;
document.getElementById('txtAwsAlarmName').value = settingsModel.AwsAlarmName;

    websocket.onopen = function () {
        var json = { event: inRegisterEvent, uuid: inUUID };
        websocket.send(JSON.stringify(json));
    };

    websocket.onmessage = function (evt) {
        // Received message from Stream Deck
        var jsonObj = JSON.parse(evt.data);
        var sdEvent = jsonObj['event'];
        switch (sdEvent) {
            case "didReceiveSettings":
				if (jsonObj.payload.settings.settingsModel.AwsAccessKeyId) {
                    settingsModel.AwsAccessKeyId = jsonObj.payload.settings.settingsModel.AwsAccessKeyId;
                    document.getElementById('txtAwsAccessKeyId').value = settingsModel.AwsAccessKeyId;
                }
if (jsonObj.payload.settings.settingsModel.AwsAccessKeySecret) {
                    settingsModel.AwsAccessKeySecret = jsonObj.payload.settings.settingsModel.AwsAccessKeySecret;
                    document.getElementById('txtAwsAccessKeySecret').value = settingsModel.AwsAccessKeySecret;
                }
if (jsonObj.payload.settings.settingsModel.AwsRegion) {
                    settingsModel.AwsRegion = jsonObj.payload.settings.settingsModel.AwsRegion;
                    document.getElementById('txtAwsRegion').value = settingsModel.AwsRegion;
                }
if (jsonObj.payload.settings.settingsModel.AwsAlarmName) {
                    settingsModel.AwsAlarmName = jsonObj.payload.settings.settingsModel.AwsAlarmName;
                    document.getElementById('txtAwsAlarmName').value = settingsModel.AwsAlarmName;
                }
                break;
            default:
                break;
        }
    };
}

const setSettings = (value, param) => {
    if (websocket) {
        settingsModel[param] = value;
        var json = {
            "event": "setSettings",
            "context": uuid,
            "payload": {
                "settingsModel": settingsModel
            }
        };
        websocket.send(JSON.stringify(json));
    }
};