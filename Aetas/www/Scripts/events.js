// Fra form to db

// Dette er starten på et binding- rammeverk.

// Lag et name.space av dette


var TransFormer = function() {};



function mapFormToModel(formData) {
    var event = new EventModel();

    for (var i = 0; i < formData.length; i++) {
        var elem = formData[i];

        var value = elem.value;
        if (value === "") value = null;
        

        var arrDescriptor = getDataArrayDescriptor(elem);
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


function getDataArrayDescriptor(elem) {
    var dataArrayDescriptor = null;

    for (var i = 0; i < elem.attributes.length; i++) {
        if (elem.attributes[i].nodeName === "data-array") {
            dataArrayDescriptor = elem.attributes[i].nodeValue;
            break;
        }
    }
    return dataArrayDescriptor;
}


function mapModelToForm(data) {

    for (var i = 0; i < document.forms[0].elements.length; i++) {

        var elem = document.forms[0].elements[i];

        var propName = elem.id;
        if (propName === "id") propName = "Id";


        if (elem.tagName === "INPUT" || elem.tagName === "TEXTAREA") {

            var verdi = data[propName];

            if (typeof(verdi) === "undefined") {
                // Sub object
                var objName = getDataObject(elem);
                verdi = data[objName][propName];
            }

            if (typeof(verdi) === "object") {
                // Array of objects
                var str = "";
                var descriptor = getDataArrayDescriptor(elem);
                for (var j = 0; j < verdi.length; j++) {
                    str += verdi[j][descriptor] + " ";
                }
                verdi = str;

            }
            document.forms[0].elements[i].value = verdi;
        }

        if (elem.tagName === "SELECT") {
        }


    }

}









