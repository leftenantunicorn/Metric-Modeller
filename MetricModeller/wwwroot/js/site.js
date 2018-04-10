// Write your JavaScript code.
//$(document).ready(function () {
//    $("#submit").click(function(){
//        var metrics = "" + $('#functionPoint').val() + "," + $('#developerCost').val() + ","
//            + $('#language').val() + "," + $('#prec').val() + "," + $('#flex').val() + ","
//            + $('#resl').val() + "," + $('#team').val() + "," + $('#pmat').val() + ","
//            + $('#rel').val() + "," + $('#data').val() + "," + $('#cplx').val() + ","
//            + $('#ruse').val() + "," + $('#docu').val() + "," + $('#time').val() + ","
//            + $('#stor').val() + "," + $('#pvol').val() + "," + $('#acap').val() + ","
//            + $('#pcap').val() + "," + $('#pcon').val() + "," + $('#apex').val() + ","
//            + $('#plex').val() + "," + $('#ltex').val() + "," + $('#tool').val() + ","
//            + $('#site').val() + "," + $('#sched').val() + "";

//        //var result = ExecutePythonScript("predictor.py", "data.csv", metrics);
//        $.ajax({
//            type: 'GET',
//            url: '@Url.Action("sendInputs", "Home")',
//            data: metrics,
//            dataType: "string",
//            cache: false,
//            async: false,
//            success: function (data) {
//                clientStuff = data;
//            },
//            error: function (errorMsg) {
//                alert(JSON.stringify(errorMsg));
//            }
//        });

//    });

//    var month = JSON.parse(clientStuff);
//    //var obj = jQuery.parseJSON(month);
    
//    $("#results").append("<li>Months: " + month.months + "</li>");
//    $("#results").append("<li>Months: " + month.cost + "</li>");

//});


//$(document).ready(function () {
//    $("#isaiahResults").hide();
//});

function isaiahFunction() {

    var fp = document.getElementById('functionPoint').value;
    var cst = document.getElementById('developerCost').value;
    var lng = document.getElementById('language').value;
    var rsk = document.getElementById('resl').value;
    var coh = document.getElementById('team').value;

    var multiplier = 1;

    var totalCost = 0;
    var totalTime = 0;
    var linesOfCode = 0;

    $("#isaiahResults").fadeIn();

    if (lng == 1) {
        multiplier = 1.5;
    } else if (lng == 2) {
        multiplier = 10.5;
    } else if (lng == 3) {
        multiplier = 1.4;
    } else if (lng == 3) {
        multiplier = 2.4;
    } else {
        multiplier = 1;
    }

    if (fp != null) {
        totalCost = fp * cst * multiplier;
    } else {
        console.log("Cannot be empty!");
    }

    linesOfCode = fp * 50 * multiplier;

    totalTime = 25 * rsk * coh;

    $("#loc").html("<strong>Lines of Code: </strong>" + linesOfCode);
    $("#time").html("<strong>Total Time: </strong>" + totalTime + " working hours");
    $("#cost").html("<strong>Total Cost:</strong> $" + totalCost);
}