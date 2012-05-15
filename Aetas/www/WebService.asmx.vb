Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebService
	Inherits System.Web.Services.WebService

	Private _TestJSON As String = "{  ""timeline"": {""asset"": {""caption"": ""Caption text goes here"", ""credit"": ""Credit Name Goes Here"", ""media"": ""http://yourdomain_or_socialmedialink_goes_here.jpg""}, ""date"": [{""asset"": {""caption"": ""Caption text goes here"", ""credit"": ""Credit Name Goes Here"", ""media"": ""http://twitter.com/ArjunaSoriano/status/164181156147900416""}, ""endDate"": ""2011,12,11"", ""headline"": ""Headline Goes Here"", ""startDate"": ""2011,12,10"", ""text"": ""<p>Body text goes here, some HTML is OK</p>""}], ""headline"": ""The Main Timeline Headline Goes here"", ""startDate"": ""1888"", ""text"": ""<p>Intro body text goes here, some HTML is ok</p>"", ""type"": ""default""}}"
	Private _TestObject As New TestClass

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function getStaticJSON() As String
		Return _TestJSON
	End Function

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	Public Function postJSONandReturn(jsonObj As String) As String
		Return _TestJSON
	End Function

	<WebMethod()> _
	<ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function getDynaminJSON() As String
		Return JsonConvert.SerializeObject(_TestObject, Formatting.Indented)
	End Function

End Class


Public Class TestClass
	Public Property Id As Integer
	Public Property Name As String

	Public Sub New()
		Id = 23
		Name = "Demo"
	End Sub
End Class