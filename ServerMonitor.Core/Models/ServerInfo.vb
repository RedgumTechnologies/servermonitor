Public Class ServerInfo
    Public Sub New()

    End Sub

    Public Property DomainWorkgroup As String
    Public Property Server As String
    Public Property ReportDateTime As String

    Public Property DetailedInfo As DetailedInfo
    Public Property PendingUpdates As List(Of PendingUpdateInfo)
    Public Property SqlServerInfo As List(Of SqlServerInfo)
    Public Property SqlDatabaseInfo As List(Of SqlDatabaseInfo)
    Public Property NetworkInfo As List(Of NetworkInfo)
    Public Property DiskInfo As List(Of DiskInfo)
    Public Property DriveInfo As List(Of DriveInfo)
    Public Property Services As List(Of ServiceInfo)
    Public Property WebsiteInfo As List(Of WebsiteInfo)
    Public Property AppPoolInfo As List(Of AppPoolInfo)
    Public Property TopCpuProcesses As List(Of ProcessInfo)
    Public Property TopMemoryProcesses As List(Of ProcessInfo)


    'Special tracking property for all keyed values
    Public Property Data As List(Of RawData)
    Public Property AggregateData As List(Of RawData)
End Class

Public Class RawData

    Public Property DataKey As String
    Public Property Data As String

End Class

Public Class DetailedInfo
    Public Property HostUptimeInTicks As Long
    Public Property TotalPhysicalMemoryInMB As Long
    Public Property LogicalProcessors As Integer
    Public Property Processors As Integer
    Public Property Manufacturer As String
    Public Property Model As String
    Public Property SystemType As String
    Public Property WindowsVersion As String
    Public Property WindowsVersionName As String

    Public ReadOnly Property HostUptime As TimeSpan
        Get
            Return New TimeSpan(HostUptimeInTicks)
        End Get
    End Property
End Class

Public Class SqlServerInfo
    Public Property Name As String
    Public Property StartMode As String
    Public Property State As String
    Public Property Status As String
End Class

Public Class SqlDatabaseInfo
    Public Property ComputerName As String
    Public Property InstanceName As String
    Public Property Parent As String
    Public Property Name As String
    Public Property AutoShrink As Boolean
    Public Property RecoveryModel As String
    Public Property SizeInMB As Long
    Public Property SpaceAvailableInKB As Long
    Public Property LastBackupDate As DateTime
    Public Property IsSystemObject As Boolean
End Class

Public Class NetworkInfo
    Public Property Index As String
    Public Property Description As String
    Public Property Caption As String
    Public Property MACAddress As String
    Public Property IPAddress As String
    Public Property IPSubnet As String
    Public Property DHCPEnabled As Boolean
    Public Property DNSDomain As String
    Public Property DNSHostName As String
    Public Property IPEnabled As Boolean
    Public Property DefaultIPGateway As String
End Class

Public Class ServiceInfo
    Public Property Name As String
    Public Property DisplayName As String
    Public Property Description As String
    Public Property State As String
    Public Property Status As String
    Public Property StartMode As String
    Public Property PathName As String
    Public Property IsWatchedItem As Boolean
End Class

Public Class DiskInfo
    Public Property VolumeName As String
    Public Property Name As String
    Public Property SizeInGB As Decimal
    Public Property FreeSpaceInGB As Decimal
    Public Property PercentFree As Decimal
End Class

Public Class DriveInfo
    Public Property Status As DriveStatus
    Public Property DeviceID As String
    Public Property StatusInfo As String
    Public Property Partitions As Integer
    Public Property Size As Long
    Public Property CapabilityDescriptions As String
    Public Property Model As String
    Public Property SerialNumber As String
End Class

Public Enum DriveStatus
    Unknown
    OK
    Degraded
    [Error]
    PredFail
    Starting
    Stopping
    Service
    Stressed
    NonRecover
    NoContact
    LostComm
End Enum

Public Class WebsiteInfo
    Public Property Name As String
    Public Property State As String
    Public Property PhysicalPath As String
End Class

Public Class AppPoolInfo
    Public Property Name As String
    Public Property State As String
    Public Property enable32BitAppOnWin64 As Boolean
    Public Property managedRuntimeVersion As String
    Public Property managedPipelineMode As String
End Class

Public Class ProcessInfo
    Public Property ProcessName As String
    Public Property CPUTime As TimeSpan
    Public Property WorkingSetInMB As Decimal
End Class

Public Class PendingUpdateInfo
    Public Property Title As String
    Public Property MinDownloadSize As Integer
    Public Property MaxDownloadSize As Integer
    Public Property AutoSelection As Integer
    Public Property AutoDownload As Integer
    Public Property IsDownloaded As Boolean
End Class

