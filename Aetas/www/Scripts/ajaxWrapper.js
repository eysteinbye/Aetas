function getData(url, callback) {
    ajaxWrapper(url, null, "GET", callback, null);
}

function postData(url, data, callback, callbackFail) {
    ajaxWrapper(url, data, "POST", callback, callbackFail);
}
function postXmlData(url, data, callback) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8", // sendes inn
        url: url,
        data: makeJSONString(data),
        dataType: "xml",  // get back
      success: function (dd) {
            callback(dd);
        },
        error: function (res, status) {
            // alert(res.responseText);
            callback(res.responseText);
        }
    });

}

function ajaxWrapper(url, data, type, callback, callbackFail) {
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
            
            if (json.wasSuccess) {
                callback(json);
            } else {
                if (!callbackFail) callbackFail= callback;
                callbackFail(json);
            }
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
        if (res.responseText == undefined || res.responseText != "") {
            // errorMessage can be an object with 3 string properties: ExceptionType, Message and StackTrace
            var errorMessage = $.parseJSON(res.responseText);
            alert(errorMessage.Message);
        } else {
            alert(res.statusText);
        }
    }
    if (status === "systemException") alert(res);

    if (status === "parsererror") alert(res); // Feilt format på respons?
    
}

function defaultAction(json) {
	alert("You need to send a callback");
}
