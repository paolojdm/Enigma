console.log('JS file loaded'); // Ensure the script is running

// Handle the deduction of lives and game over scenario
function handleLives(response) {
    if (response.livesRemaining <= 0) {
        alert("No lives remaining. Game over.");
        window.location.href = '/Puzzle/Fail'; // Redirect to fail page
    } else {
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
        alert('Correct code! Proceed to the next puzzle.');
        // Handle transition to the next puzzle or update UI
    } else {
        alert(`Incorrect code! Lives remaining: ${result.livesRemaining}`);
        document.getElementById('currentLives').innerText = result.livesRemaining; // Update lives in UI
        if (result.livesRemaining <= 0) {
            // Redirect to fail page or show fail message
            window.location.href = '/Puzzle/Fail';
        }
    }

    clearCode(); // Clear code display after submission
}

