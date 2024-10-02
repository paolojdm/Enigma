let enteredCode = "";

function enterDigit(digit) {
    if (enteredCode.length < 4) {
        enteredCode += digit;
        document.getElementById("codeDisplay").value = enteredCode;
    }
}


function clearCode() {
    enteredCode = "";
    document.getElementById("codeDisplay").value = "";
}


function submitCode() {
    fetch('/Cassaforte/ValidateCode', {
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
