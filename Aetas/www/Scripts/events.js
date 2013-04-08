// Fra form to db

// Dette er starten på et binding- rammeverk.

// Lag et name.space av dette



function mapFormToModel(formData) {
    var event = new EventModel();

    for (var i = 0; i < formData.length; i++) {
        var elem = formData[i];

        var value = elem.value;
        if (value === "") value = null;
        

        var arrDescriptor = getDataArrayDescripto(elem);
        if (arrDescriptor !== null) {
            value = event.makeObjectArrayFromString(value, arrDescriptor);
        }

        var obj = getDataObject(elem);
        if (obj !== null) {
            if (typeof (event[obj]) === "undefined") {
                event[obj] = {};
            }
            event[obj][elem.id] = value;
        } else {
            event[elem.id] = value;
        }
    }
    return event;
}

var EventModel = function () {

    this.makeObjectArrayFromString = function(str, descriptor) {
        str = $.trim(str);
        var arr = str.split(" ");
        for (var i = 0; i < arr.length; i++) {
            var value = $.trim(arr[i]);
            arr[i] = {};
            arr[i][descriptor] = value;
        }
        return arr;
    };
};

// Gets value of attribute data-object, used for sub-objects like asset
function getDataObject(elem) {
    var dataObject = null;

    for (var i = 0; i < elem.attributes.length; i++) {
        if (elem.attributes[i].nodeName === "data-object") {
            dataObject = elem.attributes[i].nodeValue;
            break;
        }
    }
    return dataObject;
}


function getDataArrayDescripto(elem) {
    var dataArrayDescriptor = null;

    for (var i = 0; i < elem.attributes.length; i++) {
        if (elem.attributes[i].nodeName === "data-array") {
            dataArrayDescriptor = elem.attributes[i].nodeValue;
            break;
        }
    }
    return dataArrayDescriptor;
}
















