﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Input.aspx.vb" Inherits="www.Input" %>
<!DOCTYPE>
<html>
<head>
    <title>Input</title>
	<script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script>
	<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
	<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
		$(function () {
			$("#startDate").datepicker();
			$("#endDate").datepicker();
		});
	</script>
</head>
<body>
    <form id="form1" runat="server" method="post" action="WebService.asmx/postJSONandReturn">
    <div>
    
    <table>
	<tr>
		<td>Headline</td><td><input type="text" name="headline" maxlength="100" style="width:200px" /></td>
	</tr><tr>
		<td>Text</td><td><input type="text" name="text" maxlength="255" style="width:200px" /></td>
	</tr><tr>
		<td>Media url</td><td><input type="text" name="media" maxlength="255" style="width:200px" /></td>
	</tr><tr>
		<td>Credit</td><td><input type="text" name="credit" maxlength="100" style="width:200px" /></td>
	</tr><tr>
		<td>Caption</td><td><input type="text" name="caption" maxlength="100" style="width:200px" /></td>
	</tr><tr>
		<td>Start date</td><td><input type="text" name="startDate" id="startDate" /></td>
	</tr><tr>
		<td>End date</td><td><input type="text" name="endDate" id="endDate" /></td>
	</tr></table>
    
	    <input type="submit" name="save" value="save">


    </div>
    </form>
</body>
</html>
