function getData(url, callback) {
    ajaxWrapper(url, null, "GET", callback);
}

function postData(url, data, callback) {
    ajaxWrapper(url, data, "POST", callback);
}

function ajaxWrapper(url, data, type, callback) {
    $.ajax({
        type: type,
        contentType: "application/json; charset=utf-8",
        url: url,
        data: makeJSONString(data),
        dataType: "json",
        dataFilter: function (json) {
            return filterData(json);
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

function makeJSONString(data) {
    // All WebService methods expect a paramater called jsonObj containg....JSON
    var jsonObj =
	{
	    "jsonObj": data
	};
    
    return JSON.stringify(jsonObj);
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
        if (res.responseText != "") {
            // errorMessage can be an object with 3 string properties: ExceptionType, Message and StackTrace
            var errorMessage = $.parseJSON(res.responseText);
            alert(errorMessage.Message);
        } else {
            alert(res.statusText);
        }
    }
    if (status === "systemException") alert(res);
}

function defaultAction(json) {
	alert("You need to send a callback");
}
