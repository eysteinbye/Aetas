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

    // format date?
    return MakeEventJson(id, headline, text, startDate, endDate, media, credit, caption, category);
}

// Fra db to show
function SerializeTimelineEventJson(data) {
    var event = data.timeline.date[0];
    
    var headline = event.Headline;
    var text = event.Text;
    var startDate = event.StartDate;
    var endDate = event.EndDate;
    var media = event.Asset.Media;
    var credit = event.Asset.Credit;
    var caption = event.Asset.Caption;
    var category = event.Category;
    var id = event.Id;
    return MakeEventJson(id, headline, text, startDate, endDate, media, credit, caption, category);
}

function populateForm(data) {
    var event = SerializeTimelineEventJson(data);
    $("#eventId").val(data.timeline.date[0].Id);
    $("#headline").val(event.Headline);
    $("#text").val(event.Text);
    $("#startDate").val(event.StartDate);
    $("#endDate").val(event.EndDate);
    $("#media").val(event.Asset.Media);
    $("#credit").val(event.Asset.Credit);
    $("#caption").val(event.Asset.Caption);
    $("#category").val(event.Category);
    // Load the image
    $("#imgShow").attr('src', event.Asset.media);
}

function ClearForm() {
    $('form input').val('');
    $('#text').val('');

    // Clear the image
    $("#imgShow").attr('src', null);
}

function MakeEventJson(id, headline, text, startDate, endDate, media, credit, caption, category) {

    if (id == "") id = null;
    var jsonObj =
    {
        "Id": id,
        "Headline": headline,
        "Text": text,
        "Asset": {
            "Media": media,
            "Credit": credit,
            "Caption": caption
        },
        "Category": category,
        "StartDate": startDate,
        "EndDate": endDate
    };
    return jsonObj;
}

// The ID of the one to edit (get back)
function queryOptions(id) {
    var myOptions =
            {
                "EventId": id
            };
    return myOptions;
}


function categoryRequest(cat) {
    var myOptions =
            {
                "Category": cat
            };
    return myOptions;
}