var azure = require('azure-sb');

var hubName = "nogare-hub";
var connectionstring = "Endpoint=sb://nogare-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=vt4ZfP7nddkQxMzJal68NQv5XQPfGkHpbocBvPSdElo=";
var notificationHubService = azure.createNotificationHubService(hubName,connectionstring);

var payload = {
  data: {
    message: '#ForzaPalestra!'
  }
};

notificationHubService.gcm.send(null, payload, function(error){
  if(!error) { console.log("Message " + payload.data.message + " sent =D") }
  else { sconsole.log(error) }
});