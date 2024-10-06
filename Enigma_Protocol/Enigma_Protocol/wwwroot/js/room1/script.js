console.log('JS file loaded'); // Ensure the script is running

// Handle the deduction of lives and game over scenario
function handleLives(response) {
    // Update the lives remaining in the UI
    document.getElementById('currentLives').innerText = response.livesRemaining;

    if (response.livesRemaining <= 0) {
        // Game over scenario
        window.location.href = '/Puzzle/Fail'; // Redirect to fail page
    } else {
        // Incorrect code scenario, but still have lives remaining
        alert("Incorrect! You lost a life. Lives remaining: " + response.livesRemaining);
    }
}

// Show clue when "Clue" button is clicked
document.getElementById('clueButton').onclick = function () {
    document.getElementById('clueImage').style.display = 'block';
    document.getElementById('openChestButton').style.display = 'inline-block'; // Enable chest button
};

// Show the wardrobe button when the chest is opened
document.getElementById('openChestButton').onclick = function () {
    $('#chestModal').modal('show');
};

// Ensure the wardrobe button is shown after chest modal closes
$('#chestModal').on('hidden.bs.modal', function () {
    document.getElementById('wardrobeButton').style.display = 'inline-block';
});

// Handle the wardrobe opening and puzzle reveal
document.getElementById('wardrobeButton').onclick = function () {
    $('#safeModal').modal('show'); // Show safe puzzle
};

// Image reorder puzzle setup and timer
$('#puzzleModal').on('show.bs.modal', function () {
    setupPuzzle();
    startTimer();
});

// Example JS logic for submitting the answer on puzzle completion
function submitImageReorderPuzzle(puzzleId) {
    const answer = true; // Image reorder puzzles always submit 'true' as answer

    // Send answer to backend for validation
    $.post('/Puzzle/SubmitPuzzleAnswer', { puzzleId: puzzleId, submittedAnswer: answer }, function (response) {
        if (response.success) {
            // Increment solved puzzles
            incrementSolvedPuzzles();
        } else {
            alert(response.message);
        }
    });
}

function incrementSolvedPuzzles() {
    $.post('/Player/UpdateSolvedPuzzles', function (response) {
        if (response.success) {
            console.log('Puzzle progress updated');
        }
    });
}


let currentCode = "";

function enterDigit(digit) {
    currentCode += digit.toString(); // Add digit to current code
    document.getElementById('codeDisplay').value = currentCode; // Update display
}

function clearCode() {
    currentCode = ""; // Clear the code
    document.getElementById('codeDisplay').value = currentCode; // Update display
}

async function submitSafeCode() {
    console.log('Submitting Code:', currentCode); // Debug log to check what is submitted

    const response = await fetch('/Puzzle/ValidateCode', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ code: currentCode }) // Send the current code
    });

    const result = await response.json();

    console.log('Response from server:', result); // Debug log to check the server response


    if (result.correct) {
        // Correct code scenario
        alert('Correct code! Proceed to the next puzzle.');
        // Show the image reorder puzzle modal
        $('#puzzleModal').modal('show');
        setupPuzzle(); // Set up the puzzle when modal is shown
        startTimer();  // Start the puzzle timer
    } else {
        // Incorrect code scenario, handle lives deduction
        handleLives(result);
    }

    clearCode(); // Clear code display after submission
}

// ------------------- IMAGE REORDER PUZZLE CODE -------------------

var rows = 3;
var columns = 3;

//var imgOrder = ["/Images/1", "/Images/3", "/Images/2", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];
var imgOrder = ["/Images/1", "/Images/3", "/Images/2", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];

var correctOrder = ["/Images/1", "/Images/2", "/Images/3", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];
var turns = 0;
var currTile, otherTile;
var timerInterval; // Timer variable
let timeLeft = 180; // Initialize the time left

// Setup puzzle function
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
    timeLeft = 180; // 3 minutes
    let timerElement = document.getElementById("timer");

    if (!timerElement) {
        console.error("Timer element not found!");
        return; // Exit if timer element doesn't exist
    }

    if (timerInterval) {
        clearInterval(timerInterval);
    }

    timerInterval = setInterval(() => {
        timeLeft--;
        timerElement.innerText = `Time Remaining: ${timeLeft} s`;

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
    // Check if the tile with source "3.jpg" is the one being swapped
    if (!currTile.src.includes("3.jpg") && !otherTile.src.includes("3.jpg")) {
        return; // Prevent swap if neither tile is "3.jpg"
    }

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

    // Only allow swap if the tile being dragged or the other tile is "3.jpg" and they are adjacent
    if (isAdjacent && (currTile.src.includes("3.jpg") || otherTile.src.includes("3.jpg"))) {
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

    alert("Congratulations! You solved the puzzle!");
    window.location.href = '/Puzzle/CompleteImagePuzzle';
}

function completeReorderPuzzle() {
    checkIfPuzzleSolved();
}




//TIME LOGIC

let globalTimeLeft;
let globalTimerInterval;

// Function to start the global timer
function startGlobalTimer() {
    const timerElement = document.getElementById("globalTimer");

    if (!timerElement) {
        console.error("Global timer element not found!");
        return; // Exit if timer element doesn't exist
    }

    // Retrieve remaining time from localStorage, or start at 600 seconds (10 minutes)
    globalTimeLeft = localStorage.getItem('globalTimeLeft') ? parseInt(localStorage.getItem('globalTimeLeft')) : 600;

    globalTimerInterval = setInterval(() => {
        globalTimeLeft--;

        // Save the remaining time in localStorage
        localStorage.setItem('globalTimeLeft', globalTimeLeft);

        // Convert seconds to minutes and seconds
        const minutes = Math.floor(globalTimeLeft / 60);
        const seconds = globalTimeLeft % 60;

        // Format minutes and seconds with leading zeros if needed
        const formattedTime = `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;

        timerElement.innerText = `Time Remaining: ${formattedTime}`;

        if (globalTimeLeft <= 0) {
            clearInterval(globalTimerInterval);
            localStorage.removeItem('globalTimeLeft'); // Clear the stored time
            alert("Time is up! You failed the game.");
            window.location.href = '/Puzzle/Fail'; // Redirect to fail page
        }
    }, 1000);
}

// Start the global timer when the page loads
window.onload = function () {
    startGlobalTimer();
};