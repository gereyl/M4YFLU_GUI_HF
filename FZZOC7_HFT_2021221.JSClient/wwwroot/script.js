let players = [];


fetch('http://localhost:4894/player')
    .then(x => x.json())
    .then(y => {
        players = y;
        console.log(players);
        display();
    });

function display() {
    players.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.player_ID + "</td><td>"
            + t.player_Name + "</td><td>"
            + t.nationality + "</td><td>"
            + t.club_ID + "</td><td>"
            + t.position + "</td><td>"
            + t.wage + "</td></tr>";
    });
}