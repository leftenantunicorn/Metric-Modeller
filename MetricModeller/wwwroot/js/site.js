// Write your JavaScript code.
$(document).ready(function () {
    $("#submit").click(function(){
        var metrics = "" + $('#functionPoint').val() + "," + $('#developerCost').val() + ","
            + $('#language').val() + "," + $('#prec').val() + "," + $('#flex').val() + ","
            + $('#resl').val() + "," + $('#team').val() + "," + $('#pmat').val() + ","
            + $('#rel').val() + "," + $('#data').val() + "," + $('#cplx').val() + ","
            + $('#ruse').val() + "," + $('#docu').val() + "," + $('#time').val() + ","
            + $('#stor').val() + "," + $('#pvol').val() + "," + $('#acap').val() + ","
            + $('#pcap').val() + "," + $('#pcon').val() + "," + $('#apex').val() + ","
            + $('#plex').val() + "," + $('#ltex').val() + "," + $('#tool').val() + ","
            + $('#site').val() + "," + $('#sched').val() + "";

        //var result = ExecutePythonScript("predictor.py", "data.csv", metrics);
        $.ajax({
            type: 'GET',
            url: '@Url.Action("sendInputs", "HomeController")',
            data: metrics,
            dataType: "string",
            cache: false,
            async: false,
            success: function (data) {
                clientStuff = data;
            },
            error: function (errorMsg) {
                alert(errorMsg);
            }
        });

    });

    var month = JSON.parse(clientStuff);
    //var obj = jQuery.parseJSON(month);
    
    $("#results").append("<li>Months: " + month.months + "</li>");
    $("#results").append("<li>Months: " + month.cost + "</li>");

});
