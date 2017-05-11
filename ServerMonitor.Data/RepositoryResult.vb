Imports ServerMonitor.Core

Public Class RepositoryResult
    Implements IRepositoryResult

    Public Sub New()
        MyBase.New()
        Me._Exception = Nothing
        Me._StatusCode = RepositoryStatusCode.OK
    End Sub

    Public Sub New(statuscode As RepositoryStatusCode)
        MyBase.New()
        Me._StatusCode = statuscode
    End Sub

    Public Sub New(ex As Exception)
        MyBase.New()
        Me._Exception = ex
        Me._StatusCode = RepositoryStatusCode.UnknownError
    End Sub

    Private _Exception As System.Exception
    Public ReadOnly Property Exception As System.Exception Implements IRepositoryResult.Exception
        Get
            Return _Exception
        End Get
    End Property

    Public ReadOnly Property HasException As Boolean Implements IRepositoryResult.HasException
        Get
            If _Exception IsNot Nothing Then
                Return True
            End If
            Return False
        End Get
    End Property

    Private _StatusCode As RepositoryStatusCode
    Public ReadOnly Property StatusCode As RepositoryStatusCode Implements IRepositoryResult.StatusCode
        Get
            Return _StatusCode
        End Get
    End Property
End Class

Public Class RepositoryResult(Of T)
    Inherits RepositoryResult
    Implements IRepositoryResult(Of T)

    Public Sub New(result As T)
        MyBase.New()
        Me._Result = result
    End Sub

    Public Sub New(statuscode As RepositoryStatusCode)
        MyBase.New(statuscode)
    End Sub

    Public Sub New(ex As Exception)
        MyBase.New(ex)
    End Sub

    Private _Result As T
    Public ReadOnly Property Result As T Implements IRepositoryResult(Of T).Result
        Get
            Return _Result
        End Get
    End Property

End Class