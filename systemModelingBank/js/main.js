$(document).ready(function () {
    $(".generateCodeBtn").click(function () {
        $(".generateCodeDivContent").slideToggle()
    })
})


let startTime = document.querySelector(".startTime");
let endTime = document.querySelector(".endTime");

let errorForstartTime = document.querySelector(".errorForstartTime");
let errorForendTime = document.querySelector(".errorForendTime");

let errorLabels = document.querySelectorAll(".errorLabel");
let inputs = document.querySelectorAll(".form-control");
let invalidfeedbacks = document.querySelectorAll(".invalid-feedback");

let submit = document.querySelector("#submit");
let myForm = document.querySelector("form");

let hiddenPaymentType = document.querySelector(".hiddenPaymentType");
let alertParagraph = document.querySelector(".alert-success")

myForm.style.display = "none"

let selectPayment = document.querySelector("#PaymentType");

let hourlyPrice = document.querySelector(".hourlyPrice").value
let realTimePrice = document.querySelector(".realTimePrice").value

let payNow = document.querySelector(".payNow");
payNow.style.display = "none";
let totalPayment = document.querySelector(".totalPayment");
totalPayment.style.display = "none";

selectPayment.addEventListener('change', function () {
    myForm.style.display = "block"
    if (this.value == "Hourly") {
        hiddenPaymentType.value = this.value
        hiddenPaymentType.style.visibility = "hidden"
    } else if (this.value == "Real") {
        hiddenPaymentType.value = this.value
    } else {
        hiddenPaymentType.value = ""
        myForm.style.display = "none"
    }

})

errorLabels.forEach(function (label) {
    label.style.color = "darkred";
    label.style.fontSize = "0.8em";
});

startTime.addEventListener('keyup', function (event) {
    var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(this.value);
    let startTimeArr = startTime.value.split(":");
    let endTimeArr = endTime.value.split(":");
    if (!isValid) {
        errorForstartTime.innerText = "Must be correct form";
        this.style.marginBottom = "0";
    } else if (endTime.value.length == 5 && startTimeArr[0] > endTimeArr[0]) {
        errorForendTime.innerText = "End time must be grater than start time";
        this.style.marginBottom = "0";
    } else {
        errorForstartTime.innerText = "";
        this.style.marginBottom = "1em";
        if (this.value.length == 5 && endTime.value.length == 5) {
            let startTimeArr = startTime.value.split(":");
            let endTimeArr = this.value.split(":");
            let TimeDurationHour = 0
            let TimeDurationMinute = 0
            let price = 0
            if (PaymentType.value == "Hourly") {
                if (endTimeArr[0] - startTimeArr[0] == 1 && endTimeArr[1] - startTimeArr[1] < 0) {
                    TimeDurationHour = 0
                    TimeDurationMinute = 60 + (endTimeArr[1] - startTimeArr[1])
                    price = 1 * hourlyPrice
                    alertParagraph.style.padding = "1%"
                    alertParagraph.innerText = "Your payment totally is" + " " + price + " " + "euros."
                    totalPayment.value = price
                } else {
                    TimeDurationHour = endTimeArr[0] - startTimeArr[0]
                    TimeDurationMinute = endTimeArr[1] - startTimeArr[1]
                    price = (TimeDurationMinute != 0) ? TimeDurationHour * hourlyPrice + 1 * hourlyPrice : TimeDurationHour * hourlyPrice
                    alertParagraph.style.padding = "1%"
                    alertParagraph.innerText = "Your payment totally is" + " " + price + " " + "euros."
                    totalPayment.value = price
                }
            }
            else if (PaymentType.value == "Real") {
                if (endTimeArr[0] - startTimeArr[0] == 1 && endTimeArr[1] - startTimeArr[1] < 0) {
                    TimeDurationHour = 0
                    TimeDurationMinute = 60 + (endTimeArr[1] - startTimeArr[1])
                    price = TimeDurationMinute * realTimePrice
                    alertParagraph.style.padding = "1%"
                    alertParagraph.innerText = "Your payment totally is" + " " + price + " " + "euros."
                    totalPayment.value = price
                } else {
                    TimeDurationHour = endTimeArr[0] - startTimeArr[0]
                    TimeDurationMinute = endTimeArr[1] - startTimeArr[1]
                    price = (TimeDurationMinute != 0) ? (TimeDurationHour * 60 + TimeDurationMinute) * realTimePrice : TimeDurationHour * 60 * realTimePrice
                    alertParagraph.style.padding = "1%"
                    alertParagraph.innerText = "Your payment totally is" + " " + price + " " + "euros."
                    totalPayment.value = price
                }

            } else {
                totalPayment = "";
            }

        } else {
            totalPayment.value = ""
        }
    }
})

endTime.addEventListener('keyup', function (event) {
    invalidfeedbacks.forEach(function (label) {
        label.innerText = ""
    });
    let startTimeArr = startTime.value.split(":");
    let endTimeArr = this.value.split(":");
    var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(this.value);
    if (!isValid) {
        errorForendTime.innerText = "Must be correct form";
        this.style.marginBottom = "0";
    } else if (startTime.value.length == 5 && startTimeArr[0] > endTimeArr[0]) {
        errorForendTime.innerText = "End time must be grater than start time";
        this.style.marginBottom = "0";
    }
    else {
        errorForendTime.innerText = "";
        this.style.marginBottom = "1em";
        if (this.value.length == 5 && startTime.value.length == 5) {
            let TimeDurationHour = 0
            let TimeDurationMinute = 0
            let price = 0
            if (endTimeArr[0] - startTimeArr[0] == 1 && endTimeArr[1] - startTimeArr[1] < 0) {
                TimeDurationHour = 0
                TimeDurationMinute = 60 + (endTimeArr[1] - startTimeArr[1])
                price = 1 * hourlyPrice
                alertParagraph.style.padding = "1%"
                alertParagraph.innerText = "Your payment totally is" + " " + price + " " + "euros."
                totalPayment.value = price
            } else {
                TimeDurationHour = endTimeArr[0] - startTimeArr[0]
                TimeDurationMinute = endTimeArr[1] - startTimeArr[1]
                price = (TimeDurationMinute != 0) ? TimeDurationHour * hourlyPrice + 1 * hourlyPrice : TimeDurationHour * hourlyPrice
                alertParagraph.style.padding = "1%"
                alertParagraph.innerText = "Your payment totally is" + " " + price + " " + "euros."
                totalPayment.value = price
            }
        }
    }
})

pay.addEventListener("click", function () {
    if (pay.checked == true) {
        payNow.value = "true"
    } else {
        payNow.value = "false"
    }
})

inputs.forEach(function (input) {
    input.addEventListener("keyup", function () {
        for (var i = 0; i < errorLabels.length; i++) {
            if (errorLabels[i].innerText != "") {
                console.log("disabled")
                submit.disabled = true
                break
            }
            else {
                submit.disabled = false
            }
        }
    })
});