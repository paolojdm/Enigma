//room script


//let timeRemaining = @Model.GetRemainingRoomTime(); // Server-side calculated time

//const timerElement = document.getElementById("timer");

//setInterval(() => {
//    if (timeRemaining > 0) {
//        timeRemaining--;
//        timerElement.innerText = `Time Remaining: ${timeRemaining} s`;
//    } else {
//        alert("Time's up!");
//        // Optionally redirect to failure page or show modal
//    }
//}, 1000);





// Safe Code Puzzle Submission
function submitSafeCode() {
    var code = document.getElementById('codeDisplay').value;

    $.post("/Puzzle/ValidatePuzzleAnswer", { puzzleId: 1, answer: code }, function (response) {
        if (response.success) {
            alert(response.message);
            $('#safeModal').modal('hide');
            $('#puzzleModal').modal('show'); // Show the next puzzle modal
        } else {
            alert(response.message);
        }
    });
}
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
    const wardrobeModal = new bootstrap.Modal(document.getElementById('wardrobeModal'));
    wardrobeModal.show();
}

// JavaScript logic for image reorder puzzle
var rows = 3;
var columns = 3;
//var imgOrder_NEW = ["/Images/1", "/Images/3", "/Images/2", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];
var imgOrder = ["/Images/1", "/Images/3", "/Images/2", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];

var correctOrder = ["/Images/1", "/Images/2", "/Images/3", "/Images/4", "/Images/5", "/Images/6", "/Images/7", "/Images/8", "/Images/9"];
var turns = 0;
var currTile, otherTile;
var timerInterval; // Timer variable
let timeLeft = 180; // Initialize the time left

window.onload = function () {
    // Removed the timer start from here
}

// Setup puzzle function
function setupPuzzle() {
    let puzzleBoard = document.getElementById('board');  // Correct board element
    puzzleBoard.innerHTML = ''; // Clear any existing puzzle

    // Create an array of image paths for shuffling
    var shuffledImages = imgOrder;//.slice(); // Copy the original order to shuffle
    //shuffledImages.sort(() => Math.random() - 0.5); // Shuffle the array

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

// Remaining code unchanged...
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('openChestButton').onclick = function () {
        $('#chestModal').modal('show');
    }

    $('#chestModal').on('hidden.bs.modal', function () {
        document.getElementById('wardrobeButton').style.display = 'inline-block'; // Show wardrobe button
    });

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
// Function to handle form submission for the safe puzzle
document.getElementById('cassaforteForm').onsubmit = function (event) {
    event.preventDefault(); // Prevent default form submission behavior

    const code = document.getElementById('codeDisplay').value;
    if (!code) {
        alert('Please enter a code.');
        return;
    }

    // Send the form data to the backend via AJAX to check the answer
    $.ajax({
        type: 'POST',
        url: '/Puzzle/ValidateCode',
        data: { code: code },
        success: function (response) {
            if (response.correct) {
                // If correct, proceed to the next puzzle or logic
                $('#safeModal').modal('hide'); // Close the modal
                $('#puzzleModal').modal('show'); // Show next puzzle modal
            } else {
                // If incorrect, update the player's lives and reset the form
                alert('Incorrect code! You lost a life.');
                document.getElementById('codeDisplay').value = ''; // Clear the input

                // Check if lives are remaining
                if (response.livesRemaining > 0) {
                    // Re-enable form for another attempt
                    $('#cassaforteForm button[type="submit"]').prop('disabled', false);
                } else {
                    // Handle the game over logic if no lives are left
                    alert('No lives remaining. Game over.');
                    window.location.href = '/Puzzle/Fail';
                }
            }
        },
        error: function () {
            alert('An error occurred. Please try again.');
        }
    });
};

// Ensure to call startTimer() when opening the image reorder puzzle modal
$('#puzzleModal').on('show.bs.modal', function () {
    setupPuzzle(); // Setup the puzzle when the modal is shown
    startTimer(); // Start the timer when the modal opens
});
