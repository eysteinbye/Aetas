Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports Raven.Client.Document
Imports Raven.Abstractions.Data
Imports Raven.Client

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class RavenDB
	Inherits System.Web.Services.WebService

	Private _TestJSON As String = "{""timeline"":{""headline"":""Eystein was born"",""text"":""<p>Intro body text goes here, some HTML is ok</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":[{""headline"":""French Revolution"",""text"":""<p>A watershed event in modern European history</p>"",""asset"":{""media"":""http://wiki.theplaz.com/w/images/French_Revolution_Napoleon-peque.jpg"",""credit"":""Eystein Bye"",""caption"":""from Wikipedia""},""startDate"":""1789,12,10"",""endDate"":""1790,07,11""},{""headline"":""Pablo Picasso"",""text"":""<p>a Spanish painter, sculptor, printmaker, ceramicist, and stage designer who spent most of his adult life in France</p>"",""asset"":{""media"":""http://upload.wikimedia.org/wikipedia/commons/9/98/Pablo_picasso_1.jpg"",""credit"":""Eystein Bye"",""caption"":""from Wikipedia""},""startDate"":""1881,10,25"",""endDate"":""1973,04,08""}]}}"


	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function HelloWorld() As String
		Return _TestJSON
	End Function

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	Public Function SaveDummy(headline As String, text As String, media As String, credit As String, caption As String, startDate As String, endDate As String) As String
		Dim parser = ConnectionStringParser(Of RavenConnectionStringOptions).FromConnectionStringName("RavenDB")
		parser.Parse()

		Dim store As New DocumentStore() With { _
		 .ApiKey = parser.ConnectionStringOptions.ApiKey, _
		 .Url = parser.ConnectionStringOptions.Url _
		}

		store.Initialize()

		Dim assets As New Assets
		assets.media = media
		assets.credit = credit
		assets.caption = caption

		Dim events As New Events
		events.headline = headline
		events.text = text
		events.asset = assets
		events.startDate = startDate
		events.endDate = endDate


		Using session As IDocumentSession = store.OpenSession()
			session.Store(events)
			session.SaveChanges()
		End Using

                'Dim aa As New System.Web.Script.Serialization.JavaScriptSerializer
		'Dim ee As New StringBuilder = Nothing
		'aa.Serialize(events, ee)
		'Return ee.ToString
                Return "{""Svar"":{""status"":""Yes""}"
	End Function
End Class


Public Class Events
	Public Property headline() As String
	Public Property text() As String
	Public Property asset() As Assets
	Public Property startDate() As String
	Public Property endDate() As String
End Class

Public Class Assets
	Public Property media() As String
	Public Property credit() As String
	Public Property caption() As String
End Class

