Imports System.Web
Imports Raven.Abstractions.Data
Imports Raven.Client.Document

Public Module Raven

	Public Property Store As DocumentStore

	Public Sub Initialize()
		Dim parser = ConnectionStringParser(Of RavenConnectionStringOptions).FromConnectionStringName("RavenDB")
		parser.Parse()

		Store = New DocumentStore() With { _
		 .ApiKey = parser.ConnectionStringOptions.ApiKey, _
		 .Url = parser.ConnectionStringOptions.Url _
		}

		Store.Initialize()
	End Sub

End Module


