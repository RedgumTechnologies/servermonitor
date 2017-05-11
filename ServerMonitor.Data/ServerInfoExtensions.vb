Imports System.Runtime.CompilerServices
Imports ServerMonitor.Core
Imports Newtonsoft.Json

Public Module ServerInfoExtensions
    <Extension()>
    Public Function AsJsonDataTable(info As ServerInfo, settings As SystemSettings)

        Dim dt As DataTable = New DataTable

        dt.Columns.Add("IDServer", GetType(Integer))
        dt.Columns.Add("DataKey", GetType(String))
        dt.Columns.Add("Data", GetType(String))

        'Add the various bits
        Dim row As DataRow
        If info.DetailedInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "DetailedInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.DetailedInfo)
            dt.Rows.Add(row)
        End If

        If info.DiskInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "DiskInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.DiskInfo)
            dt.Rows.Add(row)
        End If

        If info.DriveInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "DriveInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.DriveInfo)
            dt.Rows.Add(row)
        End If

        If info.NetworkInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "NetworkInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.NetworkInfo)
            dt.Rows.Add(row)
        End If

        If info.TopCpuProcesses IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "TopCpuProcesses"
            row.Item("Data") = JsonConvert.SerializeObject(info.TopCpuProcesses)
            dt.Rows.Add(row)
        End If

        If info.TopMemoryProcesses IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "TopMemoryProcesses"
            row.Item("Data") = JsonConvert.SerializeObject(info.TopMemoryProcesses)
            dt.Rows.Add(row)
        End If

        If info.WebsiteInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "WebsiteInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.WebsiteInfo)
            dt.Rows.Add(row)
        End If

        If info.AppPoolInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "AppPoolInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.AppPoolInfo)
            dt.Rows.Add(row)
        End If

        If info.SqlServerInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "SqlServerInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.SqlServerInfo)
            dt.Rows.Add(row)
        End If

        If info.SqlDatabaseInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "SqlDatabaseInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.SqlDatabaseInfo)
            dt.Rows.Add(row)
        End If

        If info.Services IsNot Nothing Then


            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "ServiceInfo"

            'Mark key items as Watched
            For Each service In info.Services.Where(Function(si) settings.MonitoredServices.Contains(si.Name))
                service.IsWatchedItem = True
            Next

            row.Item("Data") = JsonConvert.SerializeObject(info.Services)
            dt.Rows.Add(row)
        End If

        If info.PendingUpdates IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "PendingUpdateInfo"
            row.Item("Data") = JsonConvert.SerializeObject(info.PendingUpdates)
            dt.Rows.Add(row)
        End If

        Return dt
    End Function

    <Extension()>
    Public Function AsAggregateDataTable(info As ServerInfo, settings As SystemSettings)

        Dim dt As DataTable = New DataTable

        dt.Columns.Add("IDServer", GetType(Integer))
        dt.Columns.Add("DataKey", GetType(String))
        dt.Columns.Add("Data", GetType(String))

        'Add the various bits
        Dim row As DataRow
        If info.DiskInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "DiskInfo"
            If info.DiskInfo.Any(Function(di) di.PercentFree < 15) Then
                row.Item("Data") = "Warning"
            Else
                row.Item("Data") = "OK"
            End If
            dt.Rows.Add(row)
        End If

        If info.DriveInfo IsNot Nothing Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "DriveInfo"
            If info.DriveInfo.Any(Function(di) Not di.Status.Equals(DriveStatus.OK)) Then
                row.Item("Data") = "Warning"
            Else
                row.Item("Data") = "OK"
            End If
            dt.Rows.Add(row)
        End If

        If info.WebsiteInfo IsNot Nothing AndAlso info.WebsiteInfo.Any(Function(wsi) wsi IsNot Nothing) Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "WebsiteCount"
            row.Item("Data") = info.WebsiteInfo.Count
            dt.Rows.Add(row)

            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "WebsiteStatus"
            If info.WebsiteInfo.Where(Function(wsi) wsi IsNot Nothing AndAlso String.Equals(wsi.State, "Started", StringComparison.InvariantCultureIgnoreCase)).Count = info.WebsiteInfo.Where(Function(wsi) wsi IsNot Nothing).Count Then
                row.Item("Data") = "OK"
            Else
                row.Item("Data") = "Warning"
            End If
            dt.Rows.Add(row)

        End If

        If info.SqlServerInfo IsNot Nothing AndAlso info.SqlServerInfo.Any(Function(ssi) ssi IsNot Nothing) Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "SqlServerInfoStatus"

            If info.SqlServerInfo.Where(Function(ssi) ssi IsNot Nothing AndAlso ssi.Status = "OK").Count = info.SqlServerInfo.Where(Function(ssi) ssi IsNot Nothing).Count Then
                row.Item("Data") = "OK"
            Else
                row.Item("Data") = "Warning"
            End If

            dt.Rows.Add(row)
        End If

        If info.SqlDatabaseInfo IsNot Nothing AndAlso info.SqlDatabaseInfo.Any(Function(sdi) sdi IsNot Nothing) Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "SqlDatabaseInfoStatus"


            If info.SqlDatabaseInfo.Where(Function(sdi) sdi IsNot Nothing AndAlso Not sdi.IsSystemObject AndAlso DateDiff(DateInterval.Hour, sdi.LastBackupDate, Now) < 48).Count = info.SqlDatabaseInfo.Where(Function(sdi) sdi IsNot Nothing).Count Then
                row.Item("Data") = "OK"
            Else
                row.Item("Data") = "Warning"
            End If

            dt.Rows.Add(row)
        End If

        If info.Services IsNot Nothing AndAlso info.Services.Any(Function(si) si IsNot Nothing) Then
            row = dt.NewRow()
            row.Item("IDServer") = 0
            row.Item("DataKey") = "ServiceStatus"
            If info.Services.Any(Function(si) si IsNot Nothing) Then

                'If they are on the watch list and are not running then report an error
                If info.Services.Any(Function(si) settings.MonitoredServices.Contains(si.Name) AndAlso Not si.State = "Running") Then
                    row.Item("Data") = "Error"
                ElseIf info.Services.Where(Function(si) si IsNot Nothing AndAlso si.Status = "OK").Count = info.Services.Where(Function(si) si IsNot Nothing).Count Then
                    row.Item("Data") = "OK"
                Else
                    row.Item("Data") = "Warning"
                End If

                dt.Rows.Add(row)
            End If
            End If

            If info.PendingUpdates IsNot Nothing Then
                row = dt.NewRow()
                row.Item("IDServer") = 0
                row.Item("DataKey") = "PendingUpdateCount"
                row.Item("Data") = info.PendingUpdates.Count
                dt.Rows.Add(row)
            End If
            Return dt
    End Function
End Module
