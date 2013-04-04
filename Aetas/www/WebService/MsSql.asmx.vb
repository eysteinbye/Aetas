Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Data

<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://aetas.apphb.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class MsSql
	Inherits System.Web.Services.WebService


	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
	Public Function GetEvents() As String
		Return ConnectToSQL()
	End Function


	Private Function ConnectToSQL() As String
		Dim myConnection As New SqlConnection
		Dim myCommand As New SqlCommand

		Dim str As New StringBuilder
		myConnection = New SqlConnection("Server=1368afe0-ea03-44c6-88b9-a0520079ab2a.sqlserver.sequelizer.com;Database=db1368afe0ea0344c688b9a0520079ab2a;User ID=dpyhzkuagfdrqrot;Password=2hRRrXv7Faoy7Rn6ofphEdckVHH7GgY7kkzFEhnqqaS6kxdEakBHzApXfF5Qpxxz;")
		Try
			myConnection.Open()
			myCommand = New SqlCommand("Select headline, text, media, credit, caption, startDate, endDate from Events", myConnection)
			Dim dr As IDataReader = myCommand.ExecuteReader()

			Call CreateJSONFromReader(str, dr)

			myConnection.Close()
		Catch e As Exception
		End Try
		Return str.ToString
	End Function


	Private Sub CreateJSONFromReader(sb As StringBuilder, reader As IDataReader)
		If reader Is Nothing OrElse reader.FieldCount = 0 Then
			sb.Append("null")
			Return
		End If

		Dim rowCount As Integer = 0

		sb.Append("{""timeline"":{""headline"":""Eystein was born"",""text"":""<p>Intro body text goes here, some HTML is ok</p>"",""asset"":{""media"":""http://MakeEventJson.exprosoft.com/Staff/EysteinBye.jpg"",""credit"":""Eystein Bye"",""caption"":""Lets get started""},""startDate"":""1978"",""type"":""default"",""date"":[")

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







	<WebMethod()> _
	  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	Public Function Save(headline As String, text As String, media As String, credit As String, caption As String, startDate As String, endDate As String) As String
		Dim msg As String

		Dim dtmStart As Date
		Date.TryParse(startDate, dtmStart)

		Dim dtmEnd As Date
		Date.TryParse(endDate, dtmEnd)

		Dim myConnection As New SqlConnection("Server=1368afe0-ea03-44c6-88b9-a0520079ab2a.sqlserver.sequelizer.com;Database=db1368afe0ea0344c688b9a0520079ab2a;User ID=dpyhzkuagfdrqrot;Password=2hRRrXv7Faoy7Rn6ofphEdckVHH7GgY7kkzFEhnqqaS6kxdEakBHzApXfF5Qpxxz;")

		Dim myCommand As SqlCommand
		Dim insertCmd As String
		' Check that four of the input values are not empty. If any of them
		'  is empty, show a message to the user and rebind the DataGrid.
		' If (au_id.Value = "" Or au_fname.Value = "" Or au_lname.Value = "" _
		'   Or phone.Value = "") Then
		'  msg = "ERROR: Null values not allowed for Author ID, Name or Phone"
		' Exit Sub
		' End If

		insertCmd = "INSERT INTO Events VALUES (@headline, @text, @media, @credit, @caption, @startdate, @enddate);"

		myCommand = New SqlCommand(insertCmd, myConnection)

		myCommand.Parameters.Add(New SqlParameter("@headline", SqlDbType.VarChar, 100))
		myCommand.Parameters("@headline").Value = headline
		myCommand.Parameters.Add(New SqlParameter("@text", SqlDbType.VarChar, 255))
		myCommand.Parameters("@text").Value = text
		myCommand.Parameters.Add(New SqlParameter("@media", SqlDbType.VarChar, 255))
		myCommand.Parameters("@media").Value = media
		myCommand.Parameters.Add(New SqlParameter("@credit", SqlDbType.Char, 100))
		myCommand.Parameters("@credit").Value = credit
		myCommand.Parameters.Add(New SqlParameter("@caption", SqlDbType.VarChar, 100))
		myCommand.Parameters("@caption").Value = caption
		myCommand.Parameters.Add(New SqlParameter("@startdate", SqlDbType.Date))
		myCommand.Parameters("@startdate").Value = dtmStart
		myCommand.Parameters.Add(New SqlParameter("@enddate", SqlDbType.Date))
		myCommand.Parameters("@enddate").Value = dtmEnd

		myCommand.Connection.Open()
		' Test whether the new row can be added and  display the 
		' appropriate message box to the user.
		Try
			myCommand.ExecuteNonQuery()
			msg = "<b>Record Added</b><br>" & insertCmd
		Catch ex As SqlException
			If ex.Number = 2627 Then
				msg = "ERROR: A record already exists with the same primary key"
			Else
				msg = "ERROR: Could not add record, please ensure the fields are correctly filled out"
			End If
		End Try

		myCommand.Connection.Close()
		Return msg
	End Function

End Class