Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebService
	Inherits System.Web.Services.WebService

	Private _TestJSON As String = "{""timeline"":{""headline"":""Eystein was born"",""text"":""<p>Intro body text goes here, some HTML is ok</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":[{""headline"":""French Revolution"",""text"":""<p>A watershed event in modern European history</p>"",""asset"":{""media"":""http://wiki.theplaz.com/w/images/French_Revolution_Napoleon-peque.jpg"",""credit"":""Eystein Bye"",""caption"":""from Wikipedia""},""startDate"":""1789,12,10"",""endDate"":""1790,07,11""},{""headline"":""Pablo Picasso"",""text"":""<p>a Spanish painter, sculptor, printmaker, ceramicist, and stage designer who spent most of his adult life in France</p>"",""asset"":{""media"":""http://upload.wikimedia.org/wikipedia/commons/9/98/Pablo_picasso_1.jpg"",""credit"":""Eystein Bye"",""caption"":""from Wikipedia""},""startDate"":""1881,10,25"",""endDate"":""1973,04,08""}]}}"

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function getStaticJSON() As String
		Return ConnectToSQL()
	End Function

	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	Public Function postJSONandReturn(headline as string, text as string) As String
                Return "2"
		
	End Function

	'<WebMethod()> _
	'<ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	'Public Function getDynaminJSON() As String
	'	Dim aa As New System.Web.Script.Serialization.JavaScriptSerializer
	'	Dim ee As StringBuilder = Nothing
	'	aa.Serialize(_TestObject, ee)
	'	Return ee.ToString
	'End Function

	Private Function ConnectToSQL() As String
		Dim myConnection As New SqlConnection
		Dim myCommand As New SqlCommand

		Dim str As New StringBuilder
		myConnection = New SqlConnection("Server=1368afe0-ea03-44c6-88b9-a0520079ab2a.sqlserver.sequelizer.com;Database=db1368afe0ea0344c688b9a0520079ab2a;User ID=dpyhzkuagfdrqrot;Password=2hRRrXv7Faoy7Rn6ofphEdckVHH7GgY7kkzFEhnqqaS6kxdEakBHzApXfF5Qpxxz;")
		'establishing connection. you need to provide password for sql server
		Try
			myConnection.Open()
			'opening the connection
			myCommand = New SqlCommand("Select headline, text, media, credit, caption, startDate, endDate from Events", myConnection)
			'executing the command and assigning it to connection 
			Dim dr As IDataReader = myCommand.ExecuteReader()

			CreateJSON(str, dr)

			myConnection.Close()
		Catch e As Exception
		End Try
		Return str.ToString
	End Function


	Private Sub CreateJSON(sb As StringBuilder, reader As IDataReader)
		If reader Is Nothing OrElse reader.FieldCount = 0 Then
			sb.Append("null")
			Return
		End If

		Dim rowCount As Integer = 0

		sb.Append("{""timeline"":{""headline"":""Eystein was born"",""text"":""<p>Intro body text goes here, some HTML is ok</p>"",""asset"":{""media"":""http://www.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":[")

		While reader.Read()
			sb.Append("{")

			sb.Append("""headline""" + ":")
			sb.Append("""" + reader("headline").ToString + """")
			sb.Append(",")

			sb.Append("""text""" + ":")
			sb.Append("""" + reader("text").ToString + """")
			sb.Append(",")

			sb.Append("""asset""" & ":{")

			sb.Append("""media""" + ":")
			sb.Append("""" + reader("media").ToString + """")
			sb.Append(",")

			sb.Append("""credit""" + ":")
			sb.Append("""" + reader("credit").ToString + """")
			sb.Append(",")

			sb.Append("""caption""" + ":")
			sb.Append("""" + reader("caption").ToString + """")

			sb.Append("},")

			sb.Append("""startDate""" + ":")
			Dim ff As DateTime
			DateTime.TryParse(reader("startDate").ToString, ff)
			sb.Append("""" + ff.ToString("yyyy,MM,dd") + """")

			sb.Append(",")

			sb.Append("""endDate""" + ":")
			Dim gg As DateTime
			DateTime.TryParse(reader("endDate").ToString, gg)
			sb.Append("""" + gg.ToString("yyyy,MM,dd") + """")

			sb.Append("},")

			rowCount += 1
		End While

		If rowCount > 0 Then
			Dim index As Integer = sb.ToString().LastIndexOf(",")
			sb.Remove(index, 1)
		End If
		reader.Close()
		sb.Append("]}}")
	End Sub

End Class
