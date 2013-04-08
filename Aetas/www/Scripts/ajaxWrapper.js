function getData(url, callback) {
    ajaxWrapper(url, null, "GET", callback);
}

function postData(url, data, callback) {
    ajaxWrapper(url, data, "POST", callback);
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

// All WebService methods expect a paramater called jsonObj containg....JSON
function makeJSONString(data) {
    return JSON.stringify({ "jsonObj": data });
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
        if (typeof (res.responseText) === "undefined" || res.responseText == "") {
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

function AllCategories() {
    return ["Religion", "Empire", "Period", "Technology", "Exploration", "Art", "Person", "Fiction", "Invention", "Science", "War"];
}