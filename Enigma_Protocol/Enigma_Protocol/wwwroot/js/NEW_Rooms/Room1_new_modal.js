// Show the modal
function openModal() {
    document.getElementById("puzzleModal").style.display = "block";
}

// Close the modal
function closeModal() {
    document.getElementById("puzzleModal").style.display = "none";
}

// Automatically close modal after 120 seconds (2 minutes)
setTimeout(function () {
    closeModal();
}, 120000);