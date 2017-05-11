Public Interface IRepository

    Function UpdateServerInfo(info As ServerInfo, settings As SystemSettings) As IRepositoryResult
    Function GetServerInfo(serverName As String) As IRepositoryResult(Of ServerInfo)
    Function ListServerInfo() As IRepositoryResult(Of List(Of ServerInfo))
    Function GetSystemSettings() As IRepositoryResult(Of SystemSettings)
End Interface

Public Interface ICacheRepository
    Inherits IRepository

    Function ClearCache() As IRepositoryResult

End Interface

Public Interface IDataRepository
    Inherits IRepository
End Interface