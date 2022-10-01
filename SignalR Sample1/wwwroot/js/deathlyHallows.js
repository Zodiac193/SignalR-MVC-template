var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");

//Create Connection                                                                    
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    /*.configureLogging(signalR.LogLevel.Information)*/
    .withUrl("/hubs/deathlyHallows").build();

//Connect to methods that hub invokes aka recieve notifications from hub
connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak, stone, wand) => {      //the method that the hub is invoking 
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
});

//invoke hub methods aka send notification to hub

//start connection
function fulfilled() {

    //do something on start
    console.log("Connection to User Hub Successful");

    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter) => {
        cloakspan.innerText = raceCounter.value.toString();
        stonespan.innerText = raceCounter.value.toString();
        wandspan.innerText = raceCounter.value.toString();
    });
}
function rejected() {
    //rejected logs    
}

connectionDeathlyHallows.start().then(fulfilled, rejected);