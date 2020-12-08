let firstname = document.querySelector("#firstname");
let lastname = document.querySelector("#lastname");
let email = document.querySelector("#email");
let phone = document.querySelector("#phone");
let address = document.querySelector("#address");
let password = document.querySelector("#password");

let errorLabelForName = document.querySelector(".errorLabelForName");
let errorLabelForLastName = document.querySelector(".errorLabelForLastName");
let errorLabelForEmail = document.querySelector(".errorLabelForEmail");
let errorLabelForPhone = document.querySelector(".errorLabelForPhone");
let errorLabelForPassword = document.querySelector(".errorLabelForPassword");

let errorLabels = document.querySelectorAll(".errorLabel");
let inputs = document.querySelectorAll(".form-control");

let submitCard = document.querySelector(".formButton");

errorLabels.forEach(function (label) {
    label.style.color = "darkred";
    label.style.fontSize = "0.6em";
});

firstname.addEventListener('keyup', function (event) {
    let letters = /^[A-Za-z]+$/;
    if (!this.value.match(letters)) {
        errorLabelForName.innerText = "You can only enter letters for firstname";
        this.style.marginBottom = "0";
    }
    else if (this.value.length < 3) {
        errorLabelForName.innerText = "Length must be a least 3 for firstname";
        this.style.marginBottom = "0";
    } else {
        errorLabelForName.innerText = "";
        this.style.marginBottom = "1em";
    }
})

lastname.addEventListener('keyup', function (event) {
    let letters = /^[A-Za-z]+$/;
    if (!this.value.match(letters)) {
        errorLabelForLastName.innerText = "You can only enter letters for lastname";
        this.style.marginBottom = "0";
    }
    else if (this.value.length < 3) {
        errorLabelForLastName.innerText = "Length must be a least 3 for lastname";
        this.style.marginBottom = "0";
    } else {
        errorLabelForLastName.innerText = "";
        this.style.marginBottom = "1em";
    }
})

phone.addEventListener('keyup', function (event) {
    if (isNaN(this.value)) {
        errorLabelForPhone.innerText = "You can enter only digits for phone number";
        this.style.marginBottom = "0";
    }
    else if (this.value.length < 4) {
        errorLabelForPhone.innerText = "Phone number cannot less than 4";
        this.style.marginBottom = "0";
    } else {
        errorLabelForPhone.innerText = "";
        this.style.marginBottom = "1em";
    }
})

email.addEventListener('keyup', function (event) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(String(this.value).toLowerCase())) {
        errorLabelForEmail.innerText = "Email must be correct format";
        this.style.marginBottom = "0";
    }
    else if (this.value.length < 6) {
        errorLabelForEmail.innerText = "Email must be at least 6 characters";
        this.style.marginBottom = "0";
    } else {
        errorLabelForEmail.innerText = "";
        this.style.marginBottom = "1em";
    }
})

address.addEventListener('keyup', function (event) {
    if (this.value.length < 4) {
        errorLabelForName.innerText = "Length must be a least 4 for address";
        this.style.marginBottom = "0";
    } else {
        errorLabelForName.innerText = "";
        this.style.marginBottom = "1em";
    }
})

inputs.forEach(function (input) {
    input.addEventListener("keyup", function () {
        for (var i = 0; i < errorLabels.length; i++) {
            if (errorLabels[i].innerText != "") {
                console.log("disabled")
                submitCard.disabled = true
                break
            }
            else {
                submitCard.disabled = false
            }
        }
    })
});

