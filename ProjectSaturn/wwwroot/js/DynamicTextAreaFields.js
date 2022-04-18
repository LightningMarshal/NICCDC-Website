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
	var persistant_count = 0;
	var max_fields = 0;
	var added_fields = "";



	$(add_button).click(function (e) {
		e.preventDefault();
		if (add_button.attr("data-type") == "SkillsGained") {
			max_fields = 5;
			added_fields = // Adds Skill Fields (Education and Professional Skills Attribute)
				'<div class="form-padding d-flex justify-content-between align-items-end">' +
					'<div class="col-10">' +
						'<textarea name="SkillsGained" asp-for="SkillsGained" data-type="SkillsGained" class= "form-control form-text-area" placeholder = "Write about something you learned..." ></textarea >' +
					'</div >' +
					'<div class="align-right col-2">' +
						'<button type=button href="#" value="' + wrapper + '" class="remove_field betterbtn betterbtnstatic">Remove</button>' +
					'</div >' +
				'</div >';
		} else if (add_button.attr("data-type") == "Trainings") {
			max_fields = 10;
			added_fields = // Adds Training Fields
				'<div class="form-padding d-flex flex-wrap justify-content-between align-items-end">' +
					'<div class="form-padding col-12 col-md-6">' + 
						'<label asp-for= "Desc" > Certification/Training*</label >' +
						'<textarea name="TDesc" asp-for="Desc" class="form-control form-text-area"></textarea>' +
					'</div>' +
					'<div class="form-padding col-12 col-md-4">' +
						'<label asp-for="Date">Date Recieved/Predicted*</label>' +
						'<input name="TDate" asp-for="Date" type="month" class="form-control" />' +
					'</div>'+
					'<div class="form-padding col-12 col-md-3">' +
						'<label asp-for= "Completed"> Status*</label >' +
						'<select name="TCompleted" asp-for="Completed" >' +
							'<option value="true">Completed</option>' +
							'<option value="false">In Progress</option>' +
						'</select>' +
					'</div >' +
					'<div class="form-padding col-12 col-md-1">' +
						'<button type=button href="#" value=".training_fields_wrap" class="remove_field betterbtn betterbtnstatic">Remove</button>' +
					'</div>' +
				'</div>';
		} else if (add_button.attr("data-type") == "Awards") {
			max_fields = 10;
			added_fields = // Adds Awards Fields
				'<div class="form-padding d-flex flex-wrap justify-content-between align-items-end col-12 col-md-6">' +
					'<div class="form-padding col-12 col-md-6">' + 
						'<label asp-for= "Desc" > Award*</label >' +
						'<textarea name="ADesc" asp-for="Desc" class="form-control form-text-area"></textarea>' +
					'</div>' +
					'<div class="form-padding col-12 col-md-4">' +
						'<label asp-for="Date">Date Recieved*</label>' +
						'<input name="ADate" asp-for="Date" type="month" class="form-control" />' +
					'</div>'+
					'<div class="col-12 col-md-2">' +
						'<button type=button href="#" value=".awards_fields_wrap" class="remove_field betterbtn betterbtnstatic">Remove</button>' +
					'</div>' +
				'</div>';
        } else if (add_button.attr("data-type") == "Knowledge") {
			max_fields = 18;
			added_fields = // Adds Knowledge Fields
				'<div class="form-padding d-flex flex-wrap justify-content-left align-items-end col-12 col-md-6 col-lg-4">' +
					'<div class="form-padding col-12 col-md-8">' +
						'<label asp-for="Desc" > Skill*</label >' +
						'<textarea name="KDesc" asp-for="Desc" class="form-control form-text-area"></textarea>' +
					'</div>' +
					'<div class="form-padding col-12 col-md-4">' +
						'<button type=button href="#" value=".awards_fields_wrap" class="remove_field betterbtn betterbtnstatic">Remove</button>' +
					'</div>' +
				'</div>';
		} else {
			alert("Something went wrong. Please try again. As a last resort, refresh the page.")
		}



		if (field_count < max_fields) { // Add input box
			field_count++;
			var wrapper = this.value;
			$(wrapper).append(added_fields); // Creates new fields
		}



		if (field_count == max_fields) { // Hide button when max fields reached
			$(this).hide();
        }
	});

	$(document).on("click", ".remove_field", function (e) {
		e.preventDefault();
		persistant_count = field_count;
		field_count--;



		if (persistant_count == 1 + field_count) { // Unhide button when max fields not reached
			$(".add_field_button").show();
		}



		$(this).parent('div').parent('div').remove();
	});
});

// This automatically triggers the event once to create one pre-made field
$(document).ready(function () {
	$('.add_field_button').trigger('click');
});