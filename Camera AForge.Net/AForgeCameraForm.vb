'需要下面两个 DLL
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.ComponentModel

Public Class AForgeCameraForm
    Private VideoDevicesList As FilterInfoCollection = New FilterInfoCollection(FilterCategory.VideoInputDevice)
    Private VideoSource As IVideoSource = New VideoCaptureDevice(VideoDevicesList(0).MonikerString) '使用默认设备

    Private Sub Form1_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.BackgroundImage.Save("d:\1.jpg", Imaging.ImageFormat.Jpeg) '保存单帧图像
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        VideoSource.SignalToStop() '关闭摄像头
        Application.Exit() '退出程序
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImageLayout = ImageLayout.Zoom
        Me.DoubleBuffered = True
        '绑定画面刷新事件
        AddHandler VideoSource.NewFrame, AddressOf video_NewFrame
        '开启摄像头
        VideoSource.Start()
    End Sub

    Private Sub video_NewFrame(sender As Object, eventArgs As NewFrameEventArgs)
        '实时刷新画面
        Me.BackgroundImage = eventArgs.Frame.Clone
        '强制内存回收
        GC.Collect()
    End Sub
End Class
