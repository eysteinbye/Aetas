﻿
/* Dropdown list */
function GetSelectedIdFromDropdown(ddl) {
    var index = ddl.selectedIndex;
    return ddl[index].value;
}

function AddItemToDropDown(ddlName, text, value) {
    // Create an Option object
    var opt = document.createElement("option");
    // Add an Option object to Drop Down/List Box
    document.getElementById(ddlName).options.add(opt);
    // Assign text and value to Option object
    opt.text = text;
    opt.value = value;
}