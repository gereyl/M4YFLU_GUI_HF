let leagues = [];
getdata();


fetch('http://localhost:4894/league')
    .then(x => x.json())
    .then(y => {
        leagues = y;
        console.log(leagues);
        display();
    });

async function getdata() {
    fetch('http://localhost:4894/league')
        .then(x => x.json())
        .then(y => {
            leagues = y;
            //display();
        });
}



function display() {
    leagues.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.league_Name + "</td><td>"
            + t.nation + "</td></tr>";
    });
    
}

function create() {
    let leaguenameee = document.getElementById('leaguename').value;
    let ntn = document.getElementById('nation').value;
    let cleaagueplaces = document.getElementById('clplaces').value;

    fetch('http://localhost:4894/league', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                League_Name: leaguenameee,
                Nation: ntn,
                CL_Places: cleaagueplaces

            }),
    })



}