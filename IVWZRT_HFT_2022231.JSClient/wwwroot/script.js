let players = [];

fetch('http://localhost:64082/player')
    .then(x => x.json())
    .then(y => {
        players = y;
        console.log(players);
        display();
    });

function display() {
    players.forEach(p => {
        document.getElementById('resultarea').innerHTML
            += "<tr><td>" + p.playerId
            + "</td><td>" + p.userName
            + "</td><td>" + p.age
            + "</td><td>" + p.rank
            + "</td><td>" + p.numGames
            + "</td><td>" + p.totalKills
            + "</td><td>" + p.totalDeaths
            + "</td></tr>";
    });
}

function create() {
    let name = document.getElementById('playername').value;
    let age = parseInt(document.getElementById('playerage').value);
    let rank = document.getElementById('playerrank').value;
    let games = parseInt(document.getElementById('playergames').value);
    let kills = parseInt(document.getElementById('playerkills').value);
    let deaths = parseInt(document.getElementById('playerdeaths').value);

    fetch('http://localhost:64082/player', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                userName: name, age: age, rank: rank,
                games: games, totalKills: kills, totalDeaths: deaths
            }),
    })
        .then(response => response)
        .then(data => { console.log('Success:', data); })
        .catch((error) => { console.log('Error:', error); });
}