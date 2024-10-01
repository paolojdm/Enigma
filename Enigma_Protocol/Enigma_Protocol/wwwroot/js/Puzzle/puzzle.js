
var rows = 3;
var columns = 3;

var currTile;
var otherTile; //blank tile

var turns = 0;

// var imgOrder = ["1", "2", "3", "4", "5", "6", "7", "8", "9"];
var imgOrder = ["/Images/4", "/Images/2", "/Images/8", "/Images/5", "/Images/1", "/Images/6", "/Images/7", "/Images/9", "/Images/3"];

window.onload = function () {
    for (let r = 0; r < rows; r++) {
        for (let c = 0; c < columns; c++) {

            //<img id="0-0" src="1.jpg">
            let tile = document.createElement("img");
            tile.id = r.toString() + "-" + c.toString();
            tile.src = imgOrder.shift() + ".jpg";

            //DRAG FUNCTIONALITY
            tile.addEventListener("dragstart", dragStart);  //click an image to drag
            tile.addEventListener("dragover", dragOver);    //moving image around while clicked
            tile.addEventListener("dragenter", dragEnter);  //dragging image onto another one
            tile.addEventListener("dragleave", dragLeave);  //dragged image leaving anohter image
            tile.addEventListener("drop", dragDrop);        //drag an image over another image, drop the image
            tile.addEventListener("dragend", dragEnd);      //after drag drop, swap the two tiles

            document.getElementById("board").append(tile);

        }
    }
}

function dragStart() {
    currTile = this; //this refers to the img tile being dragged
}

function dragOver(e) {
    e.preventDefault();
}

function dragEnter(e) {
    e.preventDefault();
}

function dragLeave() {

}

function dragDrop() {
    otherTile = this; //this refers to the img tile being dropped on
}

function dragEnd() {
    if (!otherTile.src.includes("3.jpg")) {
        return;
    }

    let currCoords = currTile.id.split("-"); //ex) "0-0" -> ["0", "0"]
    let r = parseInt(currCoords[0]);
    let c = parseInt(currCoords[1]);

    let otherCoords = otherTile.id.split("-");
    let r2 = parseInt(otherCoords[0]);
    let c2 = parseInt(otherCoords[1]);

    let moveLeft = r == r2 && c2 == c - 1;
    let moveRight = r == r2 && c2 == c + 1;

    let moveUp = c == c2 && r2 == r - 1;
    let moveDown = c == c2 && r2 == r + 1;

    let isAdjacent = moveLeft || moveRight || moveUp || moveDown;

    if (isAdjacent) {
        let currImg = currTile.src;
        let otherImg = otherTile.src;

        currTile.src = otherImg;
        otherTile.src = currImg;

        turns += 1;
        document.getElementById("turns").innerText = turns;
        checkIfPuzzleSolved();
    }
}
document.addEventListener('DOMContentLoaded', () => {
    let timeLeft = 180; // Time in seconds
    const timerElement = document.getElementById('timer');
    const puzzleElement = document.getElementById('puzzle'); // Replace with your actual puzzle element ID

    function updateTimer() {
        if (timeLeft > 0) {
            timeLeft--;
            timerElement.innerText = `Tempo rimasto: ${timeLeft} s`; // Update displayed time
        } else {
            clearInterval(timerInterval);
            alert('Tempo scaduto! Il puzzle non è stato completato in tempo.');

            // Optional: Add any logic to handle the end of the game here
            // For example, you might want to hide the puzzle or show a game over screen
            // puzzleElement.style.display = 'none'; // Uncomment if you want to hide the puzzle
        }
    }

    // Start the timer interval
    const timerInterval = setInterval(updateTimer, 1000); // Update the timer every second

    // Initial display update
    timerElement.innerText = `Tempo rimasto: ${timeLeft} s`; // Ensure initial display shows the starting time
});
function checkIfPuzzleSolved() {
    let tiles = document.querySelectorAll("#board img"); // Seleziona tutte le immagini del puzzle
    for (let i = 0; i < tiles.length; i++) {
        if (!tiles[i].src.includes(correctOrder[i])) {
            return; // Se una tessera non è nell'ordine corretto, esce
        }
    }

    // Se il puzzle è risolto, fai il redirect a Room2
    alert("Puzzle completato!");
    window.location.href = '/Puzzle/Room2';
}