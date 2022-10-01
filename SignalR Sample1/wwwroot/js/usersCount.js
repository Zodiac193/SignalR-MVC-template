//Create Connection                                                                    
var connectionUserCount = new signalR.HubConnectionBuilder()
    /*.configureLogging(signalR.LogLevel.Information)*/
    .withAutomaticReconnect(),
    .withUrl("/hubs/userCount").build();

//Connect to methods that hub invokes aka recieve notifications from hub
connectionUserCount.on("updateTotalViews", (value) => {              //the method that the hub is invoking 
    var newCountSpan = document.getElementById("totalViewsCounter"); //method on the span in client view
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {              //the method that the hub is invoking 
    var newUserSpan = document.getElementById("totalUsersCounter"); //method on the span in client view
    newUserSpan.innerText = value.toString();
});

//invoke hub methods aka send notification to hub
function newWindowLoadedOnClient() {
    connectionUserCount.invoke("NewWindowsLoaded").then((value) => console.log(value));           //Method in the Hub We want to Invoke
};

//start connection
function fulfilled() {
    //do something on start
    console.log("Connection to User Hub Successful");     
};
function rejected() {
    //rejected logs
    
};



connectionUserCount.start().then(fulfilled, rejected);