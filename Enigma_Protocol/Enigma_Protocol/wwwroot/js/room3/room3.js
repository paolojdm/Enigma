console.log('Room 3 JS file loaded'); // Ensure the script is running

// TIME LOGIC

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

// Ghost image and sound handling

function mostraImmagine() {
    let mySound = new Audio("/Music/Fantasma.mp3");
    document.getElementById('immagine').style.display = 'block';
    setTimeout(nascondiImmagine, 4000);
    mySound.play();
    document.getElementById('button0').disabled = true;
}

function nascondiImmagine() {
    document.getElementById('immagine').style.display = 'none';
}

// AJAX Puzzle Submission
async function submitCode() {
    const code = document.getElementById('word').value; // Get the input value from the form
    const submission = { Code: code };

    try {
        const response = await fetch('/Room3/ValidateCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(submission)
        });

        const result = await response.json();

        if (result.correct) {
            if (result.nextRoom) {
                alert("Congratulations! You've completed the escape room!");
                window.location.href = '/PrologoeFinale/Fine'; // Redirect to the finale
            }
        } else {
            alert(`Incorrect code! Lives remaining: ${result.livesRemaining}`);
            if (result.livesRemaining === 0) {
                alert("You lost all your lives in this room!");
                window.location.href = '/Puzzle/Fail';
            }
        }
    } catch (error) {
        console.error('Error:', error);
    }
}
