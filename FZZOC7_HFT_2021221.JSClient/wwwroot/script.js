let leagues = [];
let connection = null;

let leagueIDToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
     connection = new signalR.HubConnectionBuilder()
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
            "LeagueDeleted", (user, message) => {
                getdata();
        });

    connection.on
        (
            "LeagueUpdated", (user, message) => {
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
        `<button  type="button" onclick="remove(${t.league_ID})>Delete</button>`
        + `<button  type="button" onclick="showupdate(${t.league_ID})>Update</button>`
            + "</td></tr>";
    });
  
}

function showupdate(id) {
    document.getElementById('leaguenametoupdate').value = leagues.find(t => t['league_ID'] == id)['league_Name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    leagueIDToUpdate = id;



}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let leaguenameee = document.getElementById('leaguenametoupdate').value;
    let ntn = document.getElementById('nationtoupdate').value;
    

    fetch('http://localhost:4894/league', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                League_Name: leaguenameee,
                Nation: ntn,
                league_ID: leagueIDToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getdata();
        })
        .catch((error) => { console.error('Error: ', error); });

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