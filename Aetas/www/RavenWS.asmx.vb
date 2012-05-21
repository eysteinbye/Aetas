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
Public Class RavenWS
	Inherits System.Web.Services.WebService

	<WebMethod()> _
	Public Function HelloWorld() As String
		Return "Hello World"
	End Function


	<WebMethod()> _
	<ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function SaveDummy() As String

		Dim parser = ConnectionStringParser(Of RavenConnectionStringOptions).FromConnectionStringName("RavenDB")
		parser.Parse()

		Dim store As New DocumentStore() With { _
		 .ApiKey = parser.ConnectionStringOptions.ApiKey, _
		 .Url = parser.ConnectionStringOptions.Url _
		}

		store.Initialize()


		'Dim store As New DocumentStore() With {.ConnectionStringName = "https://1.ravenhq.com/databases/AppHarbor_0c9d6757-e342-4494-abde-ea634062980f"}
		'store.Initialize()




		Dim assets As New Assets
		assets.media = "http://upload.wikimedia.org/wikipedia/commons/9/98/Pablo_picasso_1.jpg"
		assets.credit = "Eystein Bye"
		assets.caption = "from Wikipedia"

		Dim events As New Events
		events.headline = "Pablo Picasso"
		events.text = "<p>a Spanish painter, sculptor, printmaker, ceramicist, and stage designer who spent most of his adult life in France</p>"
		events.asset = assets
		events.startDate = "1881,10,25"
		events.endDate = "1973,04,08"


		Using session As IDocumentSession = store.OpenSession()
			session.Store(events)
			session.SaveChanges()
		End Using

		Return ""

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
