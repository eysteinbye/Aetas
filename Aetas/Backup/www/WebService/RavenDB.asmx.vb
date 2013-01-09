Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports Raven.Client

<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://aetas.apphb.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class RavenDB
	Inherits System.Web.Services.WebService

	Private _JSONHead As String = "{""timeline"":{""headline"":""Eystein was born"",""text"":""<p>Intro body text goes here, some HTML is ok</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":["
	Private _JSONFotter As String = "]}}"

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function GetEvents() As String
		Dim serialize = New System.Web.Script.Serialization.JavaScriptSerializer()

		Dim jsonString As String = String.Empty
		Using session As IDocumentSession = Raven.Store.OpenSession()
			Dim events = session.Query(Of Events)("Events").ToArray()
			'session.Load(Of Events)("events/65")
			For Each historyEvent As Events In events
				If jsonString = String.Empty Then
					jsonString = serialize.Serialize(historyEvent)
				Else
					jsonString &= "," & serialize.Serialize(historyEvent)
				End If
			Next
		End Using

		Return _JSONHead & jsonString & _JSONFotter
	End Function

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	Public Function Save(headline As String, text As String, media As String, credit As String, caption As String, startDate As String, endDate As String) As String
		Dim assets As New Assets With {.media = media, .credit = credit, .caption = caption}
		Dim events As New Events With {.headline = headline, .text = text, .asset = assets, .startDate = startDate, .endDate = endDate}

		Using session As IDocumentSession = Raven.Store.OpenSession()
			session.Store(events)
			session.SaveChanges()
		End Using

		Dim serialize = New System.Web.Script.Serialization.JavaScriptSerializer()
		Dim resultJs As String = serialize.Serialize(events)
		Return resultJs
	End Function
End Class