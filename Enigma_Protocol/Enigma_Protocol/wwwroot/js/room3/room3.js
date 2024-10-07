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

        timerElement.innerText = `Time: ${formattedTime}`;

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
    console.log('Submitting Code:', code); // Debug log to check what is submitted

    try {
        const response = await fetch('/Room3/ValidateCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            
            body: JSON.stringify(submission)
        });
        console.log('sending the packet...');
        const result = await response.json();

        if (result.correct) {
            if (result.nextRoom === false) {
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


window.onload = function () {
    startGlobalTimer();
    revealButtonsSequentially();
};

function revealButtonsSequentially() {
    const letteraVasoButton = document.getElementById('letteraVasoButton');
    const armorButton = document.getElementById('armorButton');
    const spadaButton = document.getElementById('spadaButton');
    const scudoButton = document.getElementById('scudoButton');

    letteraVasoButton.style.display = 'block'; // First button

    letteraVasoButton.addEventListener('click', () => {
        armorButton.style.display = 'block'; // Reveal second button
    });

    armorButton.addEventListener('click', () => {
        spadaButton.style.display = 'block'; // Reveal third button
    });

    spadaButton.addEventListener('click', () => {
        scudoButton.style.display = 'block'; // Reveal final solution button
    });
}

document.addEventListener('DOMContentLoaded', function () {
    var audio = document.getElementById('backgroundAudio');
    var playButton = document.getElementById('playButton');
    var pauseButton = document.getElementById('pauseButton');

    // Set initial volume (adjust as needed)
    audio.volume = 0.5;

    // Optional: Play/Pause button event listeners
    playButton.addEventListener('click', function () {
        audio.play();
    });

    pauseButton.addEventListener('click', function () {
        audio.pause();
    });
});