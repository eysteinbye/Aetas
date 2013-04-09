
/*
transFormer.js Framwork - Easy binding of form-data
*/

var transFormer = {

    // Gets value of attribute data-object, used for sub-objects like asset
    getDataObject: function (elem) {
        var dataObject = null;

        for (var i = 0; i < elem.attributes.length; i++) {
            if (elem.attributes[i].nodeName === "data-object") {
                dataObject = elem.attributes[i].nodeValue;
                break;
            }
        }
        return dataObject;
    },

    // Gets value of attribute data-array, used for arrays of objects
    getDataArrayDescriptor: function (elem) {
        var dataArrayDescriptor = null;

        for (var i = 0; i < elem.attributes.length; i++) {
            if (elem.attributes[i].nodeName === "data-array") {
                dataArrayDescriptor = elem.attributes[i].nodeValue;
                break;
            }
        }
        return dataArrayDescriptor;
    },

    populateModel: function (formData) {

        var eventModel = function () {

            this.makeObjectArrayFromString = function (str, descriptor) {
                str = $.trim(str);
                var arr = str.split(" ");
                for (var x = 0; x < arr.length; x++) {
                    var qq = $.trim(arr[x]);
                    arr[x] = {};
                    arr[x][descriptor] = qq;
                }
                return arr;
            };
        };

        var event = new eventModel();

        for (var i = 0; i < formData.length; i++) {
            var elem = formData[i];

            var value = elem.value;
            if (value === "") value = null;


            var arrDescriptor = this.getDataArrayDescriptor(elem);
            if (arrDescriptor !== null) {
                value = event.makeObjectArrayFromString(value, arrDescriptor);
            }

            var obj = this.getDataObject(elem);
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
    },

    populateForm: function (data) {
        for (var i = 0; i < document.forms[0].elements.length; i++) {

            var elem = document.forms[0].elements[i];

            var propName = elem.id;
            if (propName === "id") propName = "Id";


            if (elem.tagName === "INPUT" || elem.tagName === "TEXTAREA") {

                var verdi = data[propName];

                if (typeof (verdi) === "undefined") {
                    // Sub object
                    var objName = this.getDataObject(elem);
                    verdi = data[objName][propName];
                }

                if (typeof (verdi) === "object") {
                    // Array of objects
                    var str = "";
                    var descriptor = this.getDataArrayDescriptor(elem);
                    for (var j = 0; j < verdi.length; j++) {
                        str += verdi[j][descriptor] + " ";
                    }
                    verdi = str;

                }
                document.forms[0].elements[i].value = verdi;
            }

            if (elem.tagName === "SELECT") {
                // todo : Add populate for select
            }
        }
    }
};
