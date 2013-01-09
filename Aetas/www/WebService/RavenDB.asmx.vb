Imports System.Web.Services
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports Raven.Client

<ScriptService()> _
<WebService(Namespace:="http://aetas.apphb.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class RavenDB
    Inherits WebService

    Private Const JsonHead As String = "{""timeline"":{""headline"":""Aetas Timeline"",""text"":""<p>The first demo</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":["
    Private Const JsonFotter As String = "]}}"

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
    Public Function GetEvents() As String

        Dim serialize As New Script.Serialization.JavaScriptSerializer()

        Dim jsonString As String = String.Empty

        Using docSession As IDocumentSession = Raven.Store.OpenSession()
            Dim items = docSession.Query(Of Events)("Events").ToArray()

            For Each historyEvent As Events In items
                If jsonString = String.Empty Then
                    jsonString = serialize.Serialize(historyEvent)
                Else
                    jsonString &= "," & serialize.Serialize(historyEvent)
                End If
            Next
        End Using

        Return JsonHead & jsonString & JsonFotter

    End Function

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetEvent(jsonObj As Selection) As String

        Dim serialize As New Script.Serialization.JavaScriptSerializer()

        Dim jsonString As String = String.Empty

        Using docSession As IDocumentSession = Raven.Store.OpenSession()
            Dim item As Events = docSession.Load(Of Events)("events/" & jsonObj.eventId)
            jsonString = serialize.Serialize(item)
        End Using

        Return JsonHead & jsonString & JsonFotter

    End Function

    '<WebMethod()> _
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Function Save(headline As String, text As String, media As String, credit As String, caption As String, startDate As String, endDate As String) As String

    '    Dim assets As New Assets With {.media = media, .credit = credit, .caption = caption}
    '    Dim items As New Events With {.headline = headline, .text = text, .asset = assets, .startDate = startDate, .endDate = endDate}

    '    Using docSession As IDocumentSession = Raven.Store.OpenSession()
    '        docSession.Store(items)
    '        docSession.SaveChanges()
    '    End Using

    '    Dim serialize As New Script.Serialization.JavaScriptSerializer()
    '    Dim resultJs As String = serialize.Serialize(Events)
    '    Return resultJs

    'End Function

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Save(jsonObj As Events) As String
        'headline As String, text As String, media As String, credit As String, caption As String, startDate As String, endDate As String
        'Dim assets As New Assets With {.media = media, .credit = credit, .caption = caption}
        'Dim items As New Events With {.headline = headline, .text = text, .asset = assets, .startDate = startDate, .endDate = endDate}

        Using docSession As IDocumentSession = Raven.Store.OpenSession()
            docSession.Store(jsonObj)
            docSession.SaveChanges()
        End Using

        Dim serialize As New Script.Serialization.JavaScriptSerializer()
        Dim resultJs As String = serialize.Serialize(Events)
        Return resultJs
        'Return Nothing
    End Function


End Class