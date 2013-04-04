Imports System.Web.Services
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports www.BEO
Imports Raven.Client
Imports www.BEO.Response
Imports www.BEO.Request

Namespace WebService

    <ScriptService()> _
    <WebService(Namespace:="http://aetas.apphb.com/")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class RavenDb
        Inherits Services.WebService

        Private Const JsonHead As String = "{""timeline"":{""headline"":""Aetas Timeline"",""text"":""<p>The first demo</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":["
        Private Const JsonFotter As String = "]}}"

        <WebMethod()> _
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
        Public Function GetEvents() As String

            Dim serialize As New Script.Serialization.JavaScriptSerializer()

            Dim jsonString As String = String.Empty

            Using docSession As IDocumentSession = UoW.Raven.Store.OpenSession()
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
        Public Function GetEventsInCategory(jsonObj As CategoryRequest) As String

            Const indexName As String = "Events"
            Dim historyEvents As List(Of Events)
            'unAnswerdCards = CurrentSession.Query(Of Events)(indexName).Where(Function(c) c.category = jsonObj.category).Take(pageSize).Skip(jsonObj.pageIndex * pageSize).OrderByDescending(Function(c) c.answerTotal).ToList()

            Using docSession As IDocumentSession = UoW.Raven.Store.OpenSession()
                historyEvents = docSession.Query(Of Events)(indexName).Where(Function(c) c.Category = jsonObj.Category).ToList()
            End Using

            Dim serialize As New Script.Serialization.JavaScriptSerializer()

            Dim jsonString As String = String.Empty

            For Each historyEvent As Events In historyEvents
                If jsonString = String.Empty Then
                    jsonString = serialize.Serialize(historyEvent)
                Else
                    jsonString &= "," & serialize.Serialize(historyEvent)
                End If
            Next

            Return JsonHead & jsonString & JsonFotter

        End Function

        <WebMethod()> _
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
        Public Function GetEvent(jsonObj As EventRequest) As String

            Dim serialize As New Script.Serialization.JavaScriptSerializer()

            Dim jsonString As String = String.Empty

            Using docSession As IDocumentSession = UoW.Raven.Store.OpenSession()
                Dim item As Events = docSession.Load(Of Events)(jsonObj.EventId)
                jsonString = serialize.Serialize(item)
            End Using

            Return JsonHead & jsonString & JsonFotter

        End Function

        <WebMethod()> _
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
        Public Function Delete(jsonObj As EventRequest) As String
            Dim status As String
            Try
                Dim item As Events
                Using docSession As IDocumentSession = UoW.Raven.Store.OpenSession()
                    item = docSession.Load(Of Events)(jsonObj.EventId)
                    docSession.Delete(Of Events)(item)
                    docSession.SaveChanges()
                End Using
                status = MakeResponse("Deleted", "Event " & jsonObj.EventId & " is deleted", item)
            Catch ex As Exception
                status = MakeResponse("Not Deleted", ex.Message, Nothing, wasSuccess:=False)
            End Try

            Return status
        End Function

        <WebMethod()> _
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
        Public Function Save(jsonObj As Events) As String
            Dim status As String

            Dim wasUpdate As Boolean = False
            If Not IsNothing(jsonObj.Id) Then
                wasUpdate = True
            End If

            Try
                Using docSession As IDocumentSession = UoW.Raven.Store.OpenSession()
                    docSession.Store(jsonObj)
                    docSession.SaveChanges()
                    ' Get the obj back
                    jsonObj.Id = docSession.Advanced.GetDocumentId(jsonObj)

                    If wasUpdate Then
                        status = MakeResponse("Updated", "Event " & jsonObj.Headline & " is updated", jsonObj)
                    Else
                        status = MakeResponse("Saved", "Event " & jsonObj.Headline & " is saved", jsonObj)
                    End If

                End Using

            Catch ex As Exception
                status = MakeResponse("Not saved", ex.Message, jsonObj, wasSuccess:=False)
            End Try

            Return status
        End Function

        Private Function MakeResponse(ByVal title As String, description As String, obj As Events, Optional wasSuccess As Boolean = True) As String
            Dim response As New CrudResponse
            response.Title = title
            response.WasSuccess = wasSuccess
            response.Events = obj
            response.Description = description

            Dim serialize As New Script.Serialization.JavaScriptSerializer()
            Return serialize.Serialize(response)
        End Function
    End Class
End Namespace