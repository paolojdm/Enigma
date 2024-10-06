console.log('Room2 JS file loaded'); // Ensure the script is running


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
//---------------------------------------------------------------------

