function submitCode() {
    fetch('/Secondopiatto/ValidateCode', {
        method: 'POST',
        body: JSON.stringify({ code: enteredCode }),
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Cassaforte aperta!");

            } else {
                alert("Codice errato!");
                clearCode();
            }
        });
}

function clearCode() {
    enteredCode = "";
    document.getElementById("word").value = "";
}
