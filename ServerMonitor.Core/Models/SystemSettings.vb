Public Class SystemSettings
    Public Sub New()
        MonitoredServices = New List(Of String)
    End Sub

    Public Property MonitoredServices As List(Of String)
End Class
