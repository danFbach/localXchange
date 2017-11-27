$(document).ready(function($) {
	//BEGIN username check
		$("#usernameFormItem").focusout(function(){
	        var ajaxuncheckmodel = {
	        	username: $("#usernameFormItem").val(),
	        	isAvailable: false
	        };
	        var ajaxRequest = $.ajax({
	            url: "/Account/UN_Available",
	        	data: {Un: $("#usernameFormItem").val()},
	        	dataType: "html",
        		contentType: 'application/json; charset=utf-8',
        		success: ajaxSuccess(ajaxuncheckmodel, status)
	        });
	        function ajaxSuccess (data, status){
	        	var ajaxLog = "username: " + ajaxuncheckmodel.username + " " + status;
	        	console.log(ajaxLog);
	            alert(ajaxLog);
	        };
	    });
	//END username check
});


  // url: '/home/check',
  //       type: 'POST',
  //       data: JSON.stringify(ai),
  //       contentType: 'application/json; charset=utf-8',
  //       success: function (data) {
  //           alert(data.success);
  //       },
  //       error: function () {
  //           alert("error");
  //       }


	 //            dataType: "html"