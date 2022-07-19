/*
 * 
 *                  !!!IMPORTANT!!!
 *     This requires a jquery script call before
 *     it is called itself.
 *     
 *     Usage:
 *     <script src="~/lib/jquery/dist/jquery.min.js"></script>
 *     <script src="~/js/InputTextArea.js"></script>
 *     
 * 
 */

// This forces all text-area fields to be dynamically resized and set to be visually similar to the input fields
$(document).on('input', 'textarea', textAreaDynamicInput);

function textAreaDynamic( jQuery ){
    this.setAttribute('style', 'height:' + (this.scrollHeight) + 'px;');
}

function textAreaDynamicInput ( jQuery ){
    if (this.scrollHeight > this.clientHeight){
    this.style.height = this.scrollHeight + "px";
    }else if(this.scrollHieght < this.clientHeight){
    this.style.height = 'auto';
    }else{
    this.style.height = "20px";
    }
}