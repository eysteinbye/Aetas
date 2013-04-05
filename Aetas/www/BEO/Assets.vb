Namespace BEO
    Public Class Assets
        Public Property media() As String
        Public Property credit() As String
        Public Property caption() As String

        Public Overloads Function ToString() As String
            Return caption
        End Function

    End Class
End Namespace