Imports System.Web
Imports Raven.Abstractions.Data
Imports Raven.Client.Document

Public Module Raven

	Public Property Store As DocumentStore

	Public Sub Initialize()
		Try
			Dim parser = ConnectionStringParser(Of RavenConnectionStringOptions).FromConnectionStringName("RavenDB")
			parser.Parse()

			Store = New DocumentStore() With { _
			 .ApiKey = parser.ConnectionStringOptions.ApiKey, _
			 .Url = parser.ConnectionStringOptions.Url _
			}

			Store.Initialize()
		Catch ex As Exception

		End Try
	End Sub

End Module


