// Fra form to db
function SerializaFormEventJson(formData) {
    var headline;
    var text;
    var startDate;
    var endDate;
    var media;
    var credit;
    var caption;
    var category;
    var id;
    for (var i = 0; i < formData.length; i++) {
        if (formData[i].id == "eventId") id = formData[i].value;
        if (formData[i].id == "headline") headline = formData[i].value;
        if (formData[i].id == "text") text = formData[i].value;
        if (formData[i].id == "startDate") startDate = formData[i].value;
        if (formData[i].id == "endDate") endDate = formData[i].value;
        if (formData[i].id == "media") media = formData[i].value;
        if (formData[i].id == "credit") credit = formData[i].value;
        if (formData[i].id == "caption") caption = formData[i].value;
        if (formData[i].id == "category") category = formData[i].value;
    }
    
    if (id == "") id = null; // For new events Raven will create the id if it is null
    var jsonObj =
    {
        "id": id,
        "headline": headline,
        "text": text,
        "asset": {
            "media": media,
            "credit": credit,
            "caption": caption
        },
        
        "category": MakeArrayOfCategoryObjects(category),
        "startDate": startDate,
        "endDate": endDate
    };
    return jsonObj;
}

function MakeArrayOfCategoryObjects(cats) {
    cats = $.trim(cats);
    var arr = cats.split(" ");
    for (var i = 0; i < arr.length; i++) {
        arr[i] = { "name": $.trim(arr[i]) };
    }
    
    return arr;
}

function SplitArrayOfCategoryObjects(cats) {
    var str = "";
    for (var i = 0; i < cats.length; i++) {
        str += cats[i].name + " ";
    }
    return str;
}


// From db to form
function populateForm(data) {

    var event = data.timeline.date[0];
    $("#eventId").val(event.Id);
    $("#headline").val(event.headline);
    $("#text").val(event.text);
    $("#startDate").val(event.startDate);
    $("#endDate").val(event.endDate);
    $("#media").val(event.asset.media);
    $("#credit").val(event.asset.credit);
    $("#caption").val(event.asset.caption);

    var cats = SplitArrayOfCategoryObjects(event.category);
    $("#category").val(cats);

    // Load the image
    $("#imgShow").attr('src', event.asset.media);

    document.getElementById("eystein").src = "http://en.wikipedia.org/wiki/" + event.headline;

}


function ClearForm() {
    $('form input').val('');
    $('#text').val('');

    // Clear the image
    $("#imgShow").attr('src', null);
    
    document.getElementById("eystein").src = "http://en.wikipedia.org/wiki/";
}

