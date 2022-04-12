/*
 *
 *                  !!!IMPORTANT!!!
 *     This requires a jquery script call before
 *     it is called itself.
 *
 *     Usage:
 *     <script src="~/lib/jquery/dist/jquery.min.js"></script>
 *     <script src="~/js/DynamicTextAreaFields.js"></script>
 *     
 *     Optional:
 *     <script src="~/js/InputTextArea.js"></script>
 *
 *
 */

// This will allow textarea fields to be added to allow for list submission
$(document).ready(function () {
	var add_button = $(".add_field_button");
	var field_count = 0;

	$(add_button).click(function (e) {
		e.preventDefault();
		if (field_count < 5) {
			field_count++;
			var wrapper = this.value;
			$(wrapper).append('<div class="form-padding d-flex justify-content-between"><div class="col-11"><textarea name="SkillsGained" asp-for="SkillsGained" class="form-control form-text-area" placeholder="Write about something you learned..."></textarea></div><div class="align-right col-1"><button type=button href="#" class="remove_field betterbtn betterbtnstatic">Remove</button></div></div>'); // Add input box
        }
	});

	$(document).on("click", ".remove_field", function (e) {
		e.preventDefault();
		$(this).parent('div').parent('div').remove(); x--;
	});
});

// This automatically triggers the event once to create one pre-made field
$(document).ready(function () {
	$('.add_field_button').trigger('click');
});