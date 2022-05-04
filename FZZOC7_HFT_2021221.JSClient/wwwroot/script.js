﻿let leagues = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4894/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on
        (
            "LeagueCreated", (user, message) => {
                getdata();
        });
    connection.on
        (
            "LeagueRemoved", (user, message) => {
                getdata();
            });

    connection.onclose(async () => {
            await start();
        });
      start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch('http://localhost:4894/league')
        .then(x => x.json())
        .then(y => {
            leagues = y;
            display();
        });
}



function display() {
    document.getElementById('resultarea').innerHTML = "";
    leagues.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.league_Name + "</td><td>"
            + t.nation + "</td><td>" + t.league_ID +"</td><td>" +
            `<button  onclick="remove(${ t.league_ID })>Delete</button>`+ "</td></tr>";
    });
    
}

function create() {

    let leaguenameee = document.getElementById('leaguename').value;
    let ntn = document.getElementById('nation').value;
    
    fetch('http://localhost:4894/league', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                League_Name: leaguenameee,
                Nation: ntn,
            }),
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });
    
}

function remove(id) {
    fetch('http://localhost:4894/league/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });
}