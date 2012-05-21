function getData(url, callback) {
	ajaxWrapper(url, null, "GET", callback);
}
function postData(url, data, callback) {
	ajaxWrapper(url, data, "POST", callback);
}
function ajaxWrapper(url, data, type, callback) {
	$.ajax({
		type: type,
		url: url,
		data: "{jsonObj : '" + data + "'}",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		dataFilter: function (data) {
			return filterData(data);
		},
		success: function (json) {
			if (!callback) callback = defaultAction;
			callback(json);
		},
		error: function (res, status) {
			errorHandling(res, status);
		}
	});
}
function filterData(data) {
	var response;
	if (typeof (JSON) !== "undefined" && typeof (JSON.parse) === "function")
		response = $.parseJSON(data);
	else
		response = eval("(" + data + ")");
	if (response.hasOwnProperty("d"))
		return response.d;
	else
		return response;
}
function errorHandling(res, status) {
	if (status === "error") {
		// errorMessage can be an object with 3 string properties: ExceptionType, Message and StackTrace
		var errorMessage = $.parseJSON(res.responseText);
		alert(errorMessage.Message);
	}
	if (status === "systemException") alert(res);
}
function defaultAction(json) {
	alert("You need to send a callback");
}
function loadTimeline(json) {
	var timeline = new VMM.Timeline();
	timeline.init(json);
}
function demodemo() {
	alert("DEMO");
}