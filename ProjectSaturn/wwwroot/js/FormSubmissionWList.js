/*
 *
 *                  !!!IMPORTANT!!!
 *     This requires a jquery script call before
 *     it is called itself.
 *
 *     Usage:
 *     <script src="~/lib/jquery/dist/jquery.min.js"></script>
 *     <script src="~/js/FormSubmissionWList.js"></script>
 *
 *
 */

//This allows forms that have elements that are stored as a list to be submitted
$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault();

        let genericinfo = $(this).serializeArray();
        let generic = {};
        var genericlist = [];
        var modelobj = {};
        var type = '';

        // Determines if element needs to be in a list or model
        genericinfo.forEach((value) => {
            if (value.name == 'SkillsGained') { // Creates list of strings
                type = 'SkillsGained';
                genericlist.push(value.value);
            } else if (value.name == 'CCertification') { // Creates list of JSON Training objects
                type = 'strings';
                modelobj['Certification'] = value.value;
            } else if (value.name == 'CDate') {
                type = 'strings';
                modelobj['Date'] = value.value;
            } else if (value.name == 'CCompleted') {
                type = 'strings';
                modelobj['Completed'] = value.value;
                genericlist.push(JSON.stringify(modelobj));
                modelobj = {};
            } else if (value.name == "ADesc") { // Creates list of JSON Awards objects
                type = "strings";
                modelobj['Award'] = value.value;
            } else if (value.name == "ADate") {
                type = "strings";
                modelobj['EarnDate'] = value.value;
                genericlist.push(JSON.stringify(modelobj));
                modelobj = {};
            } else if (value.name == "SDesc") {
                type = "strings";
                modelobj['Skill'] = value.value;
                genericlist.push(JSON.stringify(modelobj));
                modelobj = {};
            } else {
                generic[value.name] = value.value;
            }
        });

        if (genericlist[0] != null) { generic[type] = genericlist; }

        var url = e.currentTarget.action;
        var datalength = (JSON.stringify(generic)).datalength();
        alert(datalength);
        $.ajax({
            method: "POST",
            url: url,
            data: { jsonString: JSON.stringify(generic) },
        }).done(function (msg) {
            if (msg == "true") {
                alert("Success! Your response has been recorded. Please continue to the next section");
            } else if (msg == "false") {
                alert("Error! Something went wrong! Please try resubmitting. As a last resort, reload the page.");
            } else if (msg == "true another") {
                alert("Success! Your response has been recorded. Please add another submission or continue to next section.")
            } else if (msg == "required") {
                alert("Required inputs (*) are missing. Please resubmit with all required (*) inputs.");
            } else if (msg == "crequired") {
                alert("All certifications/trainings with an description needs a date recieved/predicted. Please correct and resubmit.");
            } else if (msg == "blank") {

            }

        }).fail(function (err, textstatus, error) {
            $('span[name="error"]').text(textstatus, error , err);
        });
    });
});
