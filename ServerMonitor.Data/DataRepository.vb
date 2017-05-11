Imports ServerMonitor.Core

Public Class SqlDataRepository
    Implements IDataRepository

    Private _ConnectionString As String
    Public Sub New(connectionString As String)
        MyBase.New()
        Me._ConnectionString = connectionString
    End Sub

    Private Function GetConnection() As SqlClient.SqlConnection
        Dim lConn As SqlClient.SqlConnection = New SqlClient.SqlConnection(_ConnectionString)
        lConn.Open()
        Return lConn
    End Function

    Public Function GetServerInfo(serverName As String) As IRepositoryResult(Of ServerInfo) Implements IRepository.GetServerInfo
        Dim lServer As ServerInfo = New ServerInfo
        Dim lResult As IRepositoryResult(Of ServerInfo) = New RepositoryResult(Of ServerInfo)(lServer)

        Dim lIDServer As Integer = 0
        Using lConnection As SqlClient.SqlConnection = GetConnection()
            Using lTransaction = lConnection.BeginTransaction
                Try
                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_Server_Get", lConnection, lTransaction)
                        lCmd.CommandType = CommandType.StoredProcedure
                        With lCmd.Parameters
                            .Add("@pServerName", SqlDbType.NVarChar, 50)
                        End With

                        With lCmd
                            .Parameters("@pServerName").Value = serverName
                        End With

                        Using lDA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(lCmd)
                            Using lDT As DataTable = New DataTable

                                lDA.Fill(lDT)

                                If lDT.Rows.Count > 0 Then
                                    lIDServer = Integer.Parse(lDT.Rows(0).Item("IDServer").ToString)
                                    lServer.Server = lDT.Rows(0).Item("Name").ToString
                                    lServer.DomainWorkgroup = lDT.Rows(0).Item("Domain").ToString
                                    lServer.ReportDateTime = lDT.Rows(0).Item("UpdateDateTime").ToString
                                End If

                            End Using

                        End Using
                    End Using

                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_ServerDataAggregate_List_ForServer", lConnection, lTransaction)
                        lCmd.CommandType = CommandType.StoredProcedure
                        With lCmd.Parameters
                            .Add("@pIDServer", SqlDbType.Int)
                        End With

                        With lCmd
                            .Parameters("@pIDServer").Value = lIDServer
                        End With
                        Using lDA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(lCmd)
                            Using lDT As DataTable = New DataTable

                                lDA.Fill(lDT)

                                For lIndex = 0 To lDT.Rows.Count - 1
                                    Dim lAggregate As RawData = New RawData
                                    Dim lName As String = lDT.Rows(lIndex).Item("Name").ToString
                                    Dim lDomain As String = lDT.Rows(lIndex).Item("Domain").ToString


                                    lAggregate.DataKey = lDT.Rows(lIndex).Item("DataKey").ToString
                                    lAggregate.Data = lDT.Rows(lIndex).Item("Data").ToString

                                    If lServer.AggregateData Is Nothing Then lServer.AggregateData = New List(Of RawData)
                                    lServer.AggregateData.Add(lAggregate)
                                Next
                            End Using

                        End Using
                    End Using

                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_ServerData_List_ForServer", lConnection, lTransaction)
                        lCmd.CommandType = CommandType.StoredProcedure
                        With lCmd.Parameters
                            .Add("@pIDServer", SqlDbType.Int)
                        End With

                        With lCmd
                            .Parameters("@pIDServer").Value = lIDServer
                        End With
                        Using lDA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(lCmd)
                            Using lDT As DataTable = New DataTable

                                lDA.Fill(lDT)

                                For lIndex = 0 To lDT.Rows.Count - 1
                                    Dim lData As RawData = New RawData
                                    Dim lName As String = lDT.Rows(lIndex).Item("Name").ToString
                                    Dim lDomain As String = lDT.Rows(lIndex).Item("Domain").ToString

                                    lData.DataKey = lDT.Rows(lIndex).Item("DataKey").ToString
                                    lData.Data = lDT.Rows(lIndex).Item("Data").ToString

                                    If lServer.Data Is Nothing Then lServer.Data = New List(Of RawData)
                                    lServer.Data.Add(lData)
                                Next
                            End Using

                        End Using
                    End Using
                    lTransaction.Commit()
                Catch ex As Exception
                    lTransaction.Rollback()
                    lResult = New RepositoryResult(Of List(Of ServerInfo))(ex)
                End Try
            End Using
            lConnection.Close()
        End Using
        Return lResult
    End Function

    Public Function ListServerInfo() As IRepositoryResult(Of List(Of ServerInfo)) Implements IRepository.ListServerInfo
        Dim lServers = New List(Of ServerInfo)
        Dim lResult As IRepositoryResult(Of List(Of ServerInfo)) = New RepositoryResult(Of List(Of ServerInfo))(lServers)

        Using lConnection As SqlClient.SqlConnection = GetConnection()
            Using lTransaction = lConnection.BeginTransaction
                Try
                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_Server_List", lConnection, lTransaction)
                        lCmd.CommandType = CommandType.StoredProcedure

                        Using lDA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(lCmd)
                            Using lDT As DataTable = New DataTable

                                lDA.Fill(lDT)

                                For lIndex = 0 To lDT.Rows.Count - 1
                                    Dim lServer As ServerInfo = New ServerInfo
                                    lServer.Server = lDT.Rows(lIndex).Item("Name").ToString
                                    lServer.DomainWorkgroup = lDT.Rows(lIndex).Item("Domain").ToString
                                    lServer.ReportDateTime = lDT.Rows(lIndex).Item("UpdateDateTime").ToString
                                    lResult.Result.Add(lServer)
                                Next

                            End Using

                        End Using
                    End Using

                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_ServerDataAggregate_List", lConnection, lTransaction)
                        lCmd.CommandType = CommandType.StoredProcedure

                        Using lDA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(lCmd)
                            Using lDT As DataTable = New DataTable

                                lDA.Fill(lDT)

                                For lIndex = 0 To lDT.Rows.Count - 1
                                    Dim lAggregate As RawData = New RawData
                                    Dim lName As String = lDT.Rows(lIndex).Item("Name").ToString
                                    Dim lDomain As String = lDT.Rows(lIndex).Item("Domain").ToString

                                    Dim lServer As ServerInfo = lResult.Result.FirstOrDefault(Function(si) si.DomainWorkgroup = lDomain AndAlso si.Server = lName)
                                    If lServer IsNot Nothing Then
                                        lAggregate.DataKey = lDT.Rows(lIndex).Item("DataKey").ToString
                                        lAggregate.Data = lDT.Rows(lIndex).Item("Data").ToString

                                        If lServer.AggregateData Is Nothing Then lServer.AggregateData = New List(Of RawData)
                                        lServer.AggregateData.Add(lAggregate)
                                    End If
                                Next
                            End Using

                        End Using
                    End Using
                    lTransaction.Commit()
                Catch ex As Exception
                    lTransaction.Rollback()
                    lResult = New RepositoryResult(Of List(Of ServerInfo))(ex)
                End Try
            End Using
            lConnection.Close()
        End Using
        Return lResult

    End Function

    Public Function UpdateServerInfo(info As ServerInfo, settings As SystemSettings) As IRepositoryResult Implements IRepository.UpdateServerInfo
        Dim lResult As IRepositoryResult = New RepositoryResult()

        Using lConnection As SqlClient.SqlConnection = GetConnection()
            Using lTransaction = lConnection.BeginTransaction
                Try
                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_ServerInfo_Write", lConnection, lTransaction)
                        lCmd.CommandType = CommandType.StoredProcedure

                        With lCmd.Parameters
                            .Add("@pServerName", SqlDbType.NVarChar, 50)
                            .Add("@pDomainName", SqlDbType.NVarChar, 100)
                            .Add("@pUpdateDateTime", SqlDbType.DateTimeOffset)
                            .Add("@pDataAggregate", SqlDbType.Structured)
                            .Add("@pData", SqlDbType.Structured)
                        End With

                        With lCmd
                            .Parameters("@pServerName").Value = info.Server
                            .Parameters("@pDomainName").Value = info.DomainWorkgroup
                            .Parameters("@pUpdateDateTime").Value = DateTimeOffset.UtcNow

                            'Build the server data as Json packets
                            .Parameters("@pDataAggregate").Value = info.AsAggregateDataTable(settings)
                            .Parameters("@pData").Value = info.AsJsonDataTable(settings)
                        End With
                        lCmd.ExecuteNonQuery()
                    End Using


                    lTransaction.Commit()
                Catch ex As Exception
                    lTransaction.Rollback()
                    lResult = New RepositoryResult(ex)
                End Try
            End Using
            lConnection.Close()
        End Using
        Return lResult
    End Function

    Public Function GetSystemSettings() As IRepositoryResult(Of SystemSettings) Implements IRepository.GetSystemSettings
        Dim lSystemSettings As SystemSettings = New SystemSettings
        Dim lResult As IRepositoryResult(Of SystemSettings) = New RepositoryResult(Of SystemSettings)(lSystemSettings)
        Using lConnection As SqlClient.SqlConnection = GetConnection()
            Using lTransaction = lConnection.BeginTransaction
                Try
                    Using lCmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("dbo.usp_MON_Setting_List", lConnection, lTransaction)
                        Using lDA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(lCmd)
                            Using lDT As DataTable = New DataTable

                                lDA.Fill(lDT)

                                If lDT.Rows.Count > 0 Then
                                    Dim DataKey As String
                                    Dim Data As String
                                    For lIndex = 0 To lDT.Rows.Count - 1
                                        DataKey = lDT.Rows(lIndex).Item("DataKey").ToString
                                        Data = lDT.Rows(lIndex).Item("Data").ToString

                                        Select Case DataKey
                                            Case "MonitoredService"
                                                lSystemSettings.MonitoredServices.Add(Data)
                                            Case Else
                                                'drop it on the floor...
                                        End Select
                                    Next
                                End If

                            End Using

                        End Using
                    End Using

                    lTransaction.Commit()
                Catch ex As Exception
                    lTransaction.Rollback()
                    lResult = New RepositoryResult(ex)
                End Try
            End Using
        End Using

        Return lResult
    End Function
End Class
