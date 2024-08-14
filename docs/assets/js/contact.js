function PostToDeck(type) {

    var dataString = "";
    var url = "";
    var doPost = true;

    switch (type) {
        case 'br':
            url = "https://api.codecks.io/user-report/v1/create-report?token=rt_lMkOBNTzpHmjNbFh6yVA874Z";
            var name = $("#name-4").val();
            var email = $("#email-4").val();
            var subjectOfBug = $("#subjectOfBug").val();
            var programBehavior = $("#programBehavior").val();
            var bugDetails = $("#bugDetails").val();
            var errorLog = $("#errorLog").val();
            dataString = JSON.stringify({
                "content": "Needs to be reviewed!" + "\n\n**Program Behavior**:\n" + programBehavior + "\n\n**Subject of Bug**:\n" + subjectOfBug + "\n\n**Bug Details**:\n" + bugDetails + "\n\n" + "**Reported by** *" + name + "*\n(" + email + ")\n\n**Error Log**:\n" + errorLog,
                "userEmail": email
            });
            break;
        case 'fr':
            if ($("formCheck-2").val()) doPost = false;
            url = "https://api.codecks.io/user-report/v1/create-report?token=rt_cuMGTc6eIauc73kJGPAtcK06";
            var name = $("#name-1").val();
            var email = $("#email-1").val();
            var featureDescription = $("#feature-description").val();
            var featureType = $("#feature-type").val();
            var featureTypeS = ($("#featureTypeS").val() == "") ? null : $("#featureTypeS").val();
            var featureTypeSubmission = featureTypeS ?? featureType;
            dataString = JSON.stringify({
                "content": "Needs to be reviewed!" + "\n\n**Feature Type**:\n" + featureTypeSubmission + "\n\n**Feature Description**:\n" + featureDescription + "\n\n" + "**Requested by** *" + name + "*\n(" + email + ")",
                "userEmail": email
            });
            break;
        case 'is':
            if ($("formCheck-8").val()) doPost = false;
            url = "https://api.codecks.io/user-report/v1/create-report?token=rt_cuMGTc6eIauc73kJGPAtcK06";
            var name = $("#name-3").val();
            var email = $("#email-3").val();
            var ideaDescription = $("#idea-description").val();
            dataString = JSON.stringify({
                "content": "Needs to be reviewed!" + "\n\n**Idea Description**:\n" + ideaDescription + "\n\n" + "**Requested by** *" + name + "*\n(" + email + ")",
                "userEmail": email
            });
            break;
    }

    if (doPost) {
        $.ajax({
            type: 'POST',
            url: url,
            data: dataString,
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                return true;
            },
            error: function (x, s, e) {
                alert(e);
            }
        });
    }

    return false;
}
