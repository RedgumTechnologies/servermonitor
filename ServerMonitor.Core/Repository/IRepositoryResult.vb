Public Enum RepositoryStatusCode
    OK = 0
    NotImplemented = 10
    RecordNotFound = 1000
    UnknownError = 10000
End Enum

Public Interface IRepositoryResult
    ReadOnly Property StatusCode As RepositoryStatusCode
    ReadOnly Property HasException As Boolean
    ReadOnly Property Exception As Exception

End Interface

Public Interface IRepositoryResult(Of T)
    Inherits IRepositoryResult

    ReadOnly Property Result As T

End Interface