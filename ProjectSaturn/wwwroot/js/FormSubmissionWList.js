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

        // Determines if element needs to be in a list
        genericinfo.forEach((value) => {
            if (value.name == 'SkillsGained') { // Creates list of strings
                type = 'SkillsGained';
                genericlist.push(value.value);
            } else if (value.name == 'TDesc') { // Creates list of JSON Training objects
                type = 'strings';
                modelobj['Desc'] = value.value;
            } else if (value.name == 'TDate') {
                type = 'strings';
                modelobj['Date'] = value.value;
            } else if (value.name == 'TCompleted') {
                type = 'strings';
                modelobj['Completed'] = value.value;
                genericlist.push(JSON.stringify(modelobj));
                modelobj = {};
            } else if (value.name == "ADesc") { // Creates list of JSON Awards objects
                type = "strings";
                modelobj['Desc'] = value.value;
            } else if (value.name == "ADate") {
                type = "strings";
                modelobj['Date'] == value.value;
                genericlist.push(JSON.stringify(modelobj));
                modelobj = {};
            } else if (value.name == "KDesc") {
                type = "strings";
                modelobj['Desc'] = value.value;
                genericlist.push(JSON.stringify(modelobj));
                modelobj = {};
            } else {
                generic[value.name] = value.value;
            }
        });

        if (genericlist[0] != null) { generic[type] = genericlist; }

        var url = e.currentTarget.action;
        $.ajax({
            method: "POST",
            url: url,
            data: { jsonString: JSON.stringify(generic) },
        }).done(function (msg) {
            if (msg == "true") {
                alert("Success! Your response has been recorded. Either add another response or continue to the next page.");
            } else if (msg == "false") {
                alert("Error! Something went wrong! Please try resubmitting. As a last resort, reload the page.");
            } else if (msg == "required") {
                alert("Required inputs (*) are missing. Please resubmit with all required (*) inputs.");
            } else if (msg == "trequired") {
                alert("All certifications/trainings with an description needs a date recieved/predicted. Please correct and resubmit.");
            }

        }).fail(function (err, textstatus, error) {
            $('span[name="error"]').text(textstatus, error , err);
        });
    });
});
