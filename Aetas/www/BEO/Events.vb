Namespace BEO

    Public Class Events
        Public Property Id() As String
        Public Property headline() As String
        Public Property text() As String
        Public Property asset() As Assets
        Public Property category() As Category()
        Public Property startDate() As String
        Public Property endDate() As String


        'country
        'MadeByUser
        'Link to source
        'Releance
        'Private for user
        'Approved for public

        Public Overloads Function ToString() As String
            Return headline
        End Function

    End Class

End Namespace