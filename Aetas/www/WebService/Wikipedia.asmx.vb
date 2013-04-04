Imports System.Web.Services
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Net
Imports System.IO
Imports System.Xml

Namespace WebService

    <ScriptService()> _
    <WebService(Namespace:="http://tempuri.org/")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Wikipedia
        Inherits Services.WebService

        '{"Message":"Invalid web service call, missing value for parameter: \u0027searchString\u0027.","StackTrace":"   at System.Web.Script.Services.WebServiceMethodData.CallMethod(Object target, IDictionary`2 parameters)\r\n   at System.Web.Script.Services.WebServiceMethodData.CallMethodFromRawParams(Object target, IDictionary`2 parameters)\r\n   at System.Web.Script.Services.RestHandler.InvokeMethod(HttpContext context, WebServiceMethodData methodData, IDictionary`2 rawParams)\r\n   at System.Web.Script.Services.RestHandler.ExecuteWebServiceCall(HttpContext context, WebServiceMethodData methodData)","ExceptionType":"System.InvalidOperationException"}

        <WebMethod()> _
        <ScriptMethod(ResponseFormat:=ResponseFormat.Xml)>
        Public Function Search(jsonObj As String) As String
            Dim xmlDoc As New XmlDocument

            Dim wr As HttpWebRequest = DirectCast(WebRequest.Create("http://en.wikipedia.org/w/api.php?action=opensearch&limit=1&format=xml&search=" & jsonObj), HttpWebRequest)
            wr.UserAgent = "Mozilla/5.0"
            wr.Credentials = CredentialCache.DefaultCredentials
            wr.Accept = "text/xml"
            Try
                Dim responseStream As WebResponse = wr.GetResponse()
                Dim webResponse As HttpWebResponse = DirectCast(responseStream, HttpWebResponse)
                Dim sr As StreamReader = New StreamReader(webResponse.GetResponseStream())
                Dim sResponse As String = sr.ReadToEnd()
                xmlDoc.LoadXml(sResponse)
                webResponse.Close()
            Catch ex As Exception
                ex = ex
            End Try

            Return GetImageUrl(xmlDoc)
        End Function

        Private Function GetImageUrl(ByVal str As XmlDocument) As String
            Dim newUrl As String
            Try

                Dim imageNode As XmlNodeList = str.GetElementsByTagName("Image")
                Dim imageUrl As XmlAttribute = imageNode.Item(0).Attributes("source")
                newUrl = MakeFixedImageSizeUrl(imageUrl)

            Catch ex As Exception
                newUrl = String.Empty
            End Try
            Return newUrl
        End Function

        Private Function MakeFixedImageSizeUrl(ByVal imageUrl As XmlAttribute) As String
            'http://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/CH_cow_2.jpg/320px-CH_cow_2.jpg
            'http://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/CH_cow_2.jpg/33px-CH_cow_2.jpg
            'http://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Dphil_gown.jpg/40px-Dphil_gown.jpg

            Dim newUrl As String

            Dim urlParts As String() = imageUrl.Value.Split("/")
            Dim fileName As String = urlParts.Last()
            Dim fileNameParts As String() = fileName.Split(New String() {"px-"}, StringSplitOptions.None)
            Dim newFileName As String = "320px-" & fileNameParts.Last()
            urlParts(urlParts.Count() - 1) = newFileName

            newUrl = String.Join("/", urlParts)
            Return newUrl
        End Function


    End Class
End Namespace