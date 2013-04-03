Imports System.Web
Imports Raven.Abstractions.Data
Imports Raven.Client
Imports Raven.Client.Document

Public Module Raven

	Public Property Store As DocumentStore

    Public Sub Initialize()
        Try
            Dim parser = ConnectionStringParser(Of RavenConnectionStringOptions).FromConnectionStringName("RAVENHQ_CONNECTION_STRING")
            parser.Parse()
            Store = New DocumentStore() With { _
             .ApiKey = parser.ConnectionStringOptions.ApiKey, _
             .Url = parser.ConnectionStringOptions.Url _
            }
            Store.Initialize()
        Catch ex As Exception
            Throw New Exception("Could not connect to RavenDB")
        End Try
    End Sub

    Public ReadOnly Property CurrentSession() As IDocumentSession
        Get
            Return DirectCast(HttpContext.Current.Items("CurrentRequestRavenSession"), IDocumentSession)
        End Get
    End Property

End Module

