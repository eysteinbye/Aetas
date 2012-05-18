Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebService
	Inherits System.Web.Services.WebService

	'Private _TestJSON_ORG As String = "{  ""timeline"": {""asset"": {""caption"": ""Caption text goes here"", ""credit"": ""Credit Name Goes Here"", ""media"": ""http://yourdomain_or_socialmedialink_goes_here.jpg""}, ""date"": [{""asset"": {""caption"": ""Caption text goes here"", ""credit"": ""Credit Name Goes Here"", ""media"": ""http://twitter.com/ArjunaSoriano/status/164181156147900416""}, ""endDate"": ""2011,12,11"", ""headline"": ""Headline Goes Here"", ""startDate"": ""2011,12,10"", ""text"": ""<p>Body text goes here, some HTML is OK</p>""}], ""headline"": ""The Main Timeline Headline Goes here"", ""startDate"": ""1888"", ""text"": ""<p>Intro body text goes here, some HTML is ok</p>"", ""type"": ""default""}}"
	'Private _TestObject As New TestClass
	Private _TestJSON As String = "{""timeline"":{""headline"":""Eystein was born"",""text"":""<p>Intro body text goes here, some HTML is ok</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":[{""headline"":""French Revolution"",""text"":""<p>A watershed event in modern European history</p>"",""asset"":{""media"":""http://wiki.theplaz.com/w/images/French_Revolution_Napoleon-peque.jpg"",""credit"":""Eystein Bye"",""caption"":""from Wikipedia""},""startDate"":""1789,12,10"",""endDate"":""1790,07,11""},{""headline"":""Pablo Picasso"",""text"":""<p>a Spanish painter, sculptor, printmaker, ceramicist, and stage designer who spent most of his adult life in France</p>"",""asset"":{""media"":""http://upload.wikimedia.org/wikipedia/commons/9/98/Pablo_picasso_1.jpg"",""credit"":""Eystein Bye"",""caption"":""from Wikipedia""},""startDate"":""1881,10,25"",""endDate"":""1973,04,08""}]}}"
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

	'<WebMethod()> _
	'<ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	'Public Function getDynaminJSON() As String

	'	Dim aa As New System.Web.Script.Serialization.JavaScriptSerializer
	'	Dim ee As StringBuilder = Nothing
	'	aa.Serialize(_TestObject, ee)

	'	Return ee.ToString
	'End Function

End Class


'Server=1368afe0-ea03-44c6-88b9-a0520079ab2a.sqlserver.sequelizer.com;Database=db1368afe0ea0344c688b9a0520079ab2a;User ID=dpyhzkuagfdrqrot;Password=2hRRrXv7Faoy7Rn6ofphEdckVHH7GgY7kkzFEhnqqaS6kxdEakBHzApXfF5Qpxxz;

'Public Class TestClass
'	Public Property Id As Integer
'	Public Property Name As String

'	Public Sub New()
'		Id = 23
'		Name = "Demo"
'	End Sub
'End Class

