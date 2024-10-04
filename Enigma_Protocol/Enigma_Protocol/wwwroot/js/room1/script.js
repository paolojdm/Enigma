// JavaScript logic for safe puzzle
function enterDigit(digit) {
    let display = document.getElementById('codeDisplay');
    display.value += digit;
}

function clearCode() {
    document.getElementById('codeDisplay').value = '';
}

// Function to open the wardrobe and access the safe puzzle
function openWardrobe() {
    // Show the wardrobe modal
    const wardrobeModal = new bootstrap.Modal(document.getElementById('wardrobeModal'));
    wardrobeModal.show();
}

// JavaScript logic for image reorder puzzle
var rows = 3;
var columns = 3;
var imgOrder = ["/Images/1", "/Images/3", "/Images/2", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];
var correctOrder = ["/Images/1", "/Images/2", "/Images/3", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];
var turns = 0;
var currTile, otherTile;
var timerInterval; // Declare a variable to hold the timer interval
let timeLeft = 180; // Initialize the time left

window.onload = function () {
    // Removed the timer start from here
}

// Setup puzzle function remains the same
function setupPuzzle() {
    let puzzleBoard = document.getElementById('board');  // Correct board element
    puzzleBoard.innerHTML = ''; // Clear any existing puzzle

    // Create an array of image paths for shuffling
    var shuffledImages = imgOrder.slice(); // Copy the original order to shuffle
    shuffledImages.sort(() => Math.random() - 0.5); // Shuffle the array

    for (let r = 0; r < rows; r++) {
        for (let c = 0; c < columns; c++) {
            let tile = document.createElement("img");
            tile.id = r.toString() + "-" + c.toString();
            tile.src = shuffledImages.shift() + ".jpg";  // Adjust the image path
            tile.draggable = true; // Make the tile draggable
            tile.addEventListener("dragstart", dragStart);
            tile.addEventListener("dragover", dragOver);
            tile.addEventListener("dragenter", dragEnter);
            tile.addEventListener("dragleave", dragLeave);
            tile.addEventListener("drop", dragDrop);
            tile.addEventListener("dragend", dragEnd);
            puzzleBoard.appendChild(tile);
        }
    }
}

function startTimer() {
    // Reset the timer each time it starts
    timeLeft = 180; // 3 minutes
    let timerElement = document.getElementById("timer");

    // Log to check if the timer element is found
    if (!timerElement) {
        console.error("Timer element not found!");
        return; // Exit if timer element doesn't exist
    }

    // Clear any existing timer to prevent multiple intervals
    if (timerInterval) {
        clearInterval(timerInterval);
    }

    timerInterval = setInterval(() => {
        timeLeft--;
        timerElement.innerText = `Time Remaining: ${timeLeft} s`;

        // Log to check timer status
        console.log(`Time left: ${timeLeft}`);

        if (timeLeft <= 0) {
            clearInterval(timerInterval);
            alert("Time is up! You failed the puzzle.");
            window.location.href = '/Puzzle/Fail'; // Redirect to fail page
        }
    }, 1000);
}


function dragStart() {
    currTile = this;
}

function dragOver(e) {
    e.preventDefault();
}

function dragEnter(e) {
    e.preventDefault();
}

function dragLeave() { }

function dragDrop() {
    otherTile = this;
}

function dragEnd() {
    let currCoords = currTile.id.split("-");
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

function checkIfPuzzleSolved() {
    let tiles = document.querySelectorAll("#board img"); // Use the correct board ID
    for (let i = 0; i < tiles.length; i++) {
        if (!tiles[i].src.includes(correctOrder[i])) {
            return;
        }
    }

    // All tiles are in the correct order
    alert("Congratulations! You solved the puzzle!");

    // Redirect to the CompleteImagePuzzle method in the Puzzle controller
    window.location.href = '/Puzzle/CompleteImagePuzzle';
}
function completeReorderPuzzle() {
    checkIfPuzzleSolved();
}

// script.js

document.addEventListener('DOMContentLoaded', function () {
    // Show the wardrobe button when the key is found
    document.getElementById('openChestButton').onclick = function () {
        $('#chestModal').modal('show');
    }

    // Ensure the wardrobe button is shown after the chest modal closes
    $('#chestModal').on('hidden.bs.modal', function () {
        document.getElementById('wardrobeButton').style.display = 'inline-block'; // Show wardrobe button
    });

    // Open the safe modal when the wardrobe button is clicked
    document.getElementById('wardrobeButton').onclick = function () {
        $('#safeModal').modal('show'); // Show the safe puzzle modal
    };
});

// Function to enter a digit into the code display
function enterDigit(digit) {
    var codeDisplay = document.getElementById('codeDisplay');
    codeDisplay.value += digit; // Append the digit to the display
}

// Function to clear the code display
function clearCode() {
    document.getElementById('codeDisplay').value = ''; // Clear the display
}

// Show the wardrobe button when the key is found
document.getElementById('openChestButton').onclick = function () {
    $('#chestModal').modal('show'); //fix the button positioning later
}

// Ensure the wardrobe button is shown after the chest modal closes
$('#chestModal').on('hidden.bs.modal', function () {
    document.getElementById('wardrobeButton').style.display = 'inline-block'; // Show wardrobe button
});

// Open the safe modal when the wardrobe button is clicked
document.getElementById('wardrobeButton').onclick = function () {
    $('#safeModal').modal('show'); // Show the safe puzzle modal
};

// Open the puzzle modal when the safe code is correctly submitted
document.getElementById('cassaforteForm').onsubmit = function (event) {
    event.preventDefault(); // Prevent form submission
    // Check if the code is correct here
    // Assuming the correct code is validated and you want to open the puzzle modal
    $('#safeModal').modal('hide'); // Close the safe modal
    $('#puzzleModal').modal('show'); // Open the image reorder puzzle modal
};

// Ensure to call startTimer() when opening the image reorder puzzle modal
$('#puzzleModal').on('show.bs.modal', function () {
    console.log("Opening puzzle modal. Starting timer.");
    setupPuzzle(); // Setup the puzzle when the modal is shown
    startTimer(); // Start the timer when the modal opens
});

// Include this if you need to set the puzzle up when the modal opens
$('#puzzleModal').on('shown.bs.modal', function () {
    setupPuzzle(); // Ensure puzzle setup is called here if needed
});