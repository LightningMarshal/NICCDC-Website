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
        alert("Intercepted!");

        let genericinfo = $(this).serializeArray();
        let generic = {};
        var genericlist = [];

        // Determines if element needs to be in a list
        genericinfo.forEach((value) => {
            if (value.name != 'SkillsGained') {
                generic[value.name] = value.value;
            } else {
                genericlist.push(value.value);
            }
        });

        // TODO: code to allow the list to make a list through the value.name
            if (genericlist[0] != null) { generic['SkillsGained'] = genericlist; }

        var url = e.currentTarget.action;
        $.ajax({
            method: "POST",
            url: url,
            data: { jsonString: JSON.stringify(generic) },
        }).done(function (msg) {
            // TODO: change this to re - render the targeted elements
            if (msg == "true") {
                alert("Success! Your response has been recorded. Either add another response or continue to the next page.");
            } else if (msg == "false") {
                alert("Error! Something went wrong! Please try resubmitting. As a last resort, reload the page.");
            } else if (msg == "required") {
                alert("All inputs are required. Please resubmit with all inputs.");
            }

        }).fail(function (err, textstatus, error) {
            $('span[name="error"]').text(textstatus, error , err);
        });
    });
});
