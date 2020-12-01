$(document).ready(function () {
    $(".regularTransaction").click(function(){
        $(".newTransactionDivContent").slideToggle()
    })
    $(".generateCodeBtn").click(function(){
        $(".generateCodeDivContent").slideToggle()
    })
})