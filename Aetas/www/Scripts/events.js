// Fra form to save
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

        if (formData[i].id == "headline") headline = formData[i].value;
        if (formData[i].id == "text") text = formData[i].value;
        if (formData[i].id == "startDate") startDate = formData[i].value;
        if (formData[i].id == "endDate") endDate = formData[i].value;
        if (formData[i].id == "media") media = formData[i].value;
        if (formData[i].id == "credit") credit = formData[i].value;
        if (formData[i].id == "caption") caption = formData[i].value;
        if (formData[i].id == "category") category = formData[i].value;
        if (formData[i].id == "eventId") id = formData[i].value;
        
    }

    return MakeEventJson(id, headline, text, startDate, endDate, media, credit, caption, category);
}

// Fra db to show
function SerializeTimelineEventJson(data) {
    var event = data.timeline.date[0];
    
    var headline = event.headline;
    var text = event.text;
    var startDate = event.startDate;
    var endDate = event.endDate;
    var media = event.asset.media;
    var credit = event.asset.credit;
    var caption = event.asset.caption;
    var category = event.category;
    var id = event.id;
    return MakeEventJson(id, headline, text, startDate, endDate, media, credit, caption, category);
}

function populateForm(data) {
    var event = SerializeTimelineEventJson(data);
    $("#eventId").val(data.timeline.date[0].Id);
    $("#headline").val(event.headline);
    $("#text").val(event.text);
    $("#startDate").val(event.startDate);
    $("#endDate").val(event.endDate);
    $("#media").val(event.asset.media);
    $("#credit").val(event.asset.credit);
    $("#caption").val(event.asset.caption);
    $("#category").val(event.category);
}

function MakeEventJson(id, headline, text, startDate, endDate, media, credit, caption, category) {

    if (id == "") id = null;
    var jsonObj =
    {
        "Id": id,
        "headline": headline,
        "text": text,
        "asset": {
            "media": media,
            "credit": credit,
            "caption": caption
        },
        "category": category,
        "startDate": startDate,
        "endDate": endDate
    };
    return jsonObj;
}

// The ID of the one to edit (get back)
function queryOptions(id) {
    var myOptions =
            {
                "eventId": id
            };
    return myOptions;
}