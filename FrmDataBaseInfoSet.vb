Imports System.Threading
Public Class FrmDataBaseInfoSet
    Dim th, th1 As Thread

    Private Sub Form1_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'If th.IsAlive Then
        th.Abort()

        ' End If
        th1.Abort()
    End Sub
    Sub getxml()
        Dim xml As String = Application.StartupPath & ("/XmlFile.xml")
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        th = New Thread(AddressOf RunsOnWorkerThread)
        th.Start()
        'th1 = New Thread(AddressOf RunsOnxmlThread)
        'th1.Start()
    End Sub
    Sub RunsOnxmlThread()
        Dim mi As New MethodInvoker(AddressOf getxml)
        BeginInvoke(mi)
    End Sub

    Sub RunsOnWorkerThread()
        Dim mi As New MethodInvoker(AddressOf GetSqlServers)
        BeginInvoke(mi)
    End Sub


    Sub GetSqlServers()
        Return

        Dim dbL As New DB
        Dim list As IList(Of String) = (dbL.GetSqlServerNames)
        For I = 0 To list.Count - 1
            cbServer.Items.Add(list.Item(I))
        Next


    End Sub
    '得到指定SQL服务器所有数据库的列表

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetSqlServers()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        s.DbName = cboDatabase.Text
        MsgBox(s.ServerName)
        FrmMain.Show()
    End Sub




    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        s.ServerName = cbServer.Text
        s.Uid = UserT.Text
        s.pwd = pwdt.Text
        s.DbName = cboDatabase.Text

        Dim xml As String = Application.StartupPath & ("/XmlFile.xml")
        Dim m As String
        m = XmlHelper.Read(xml, "/Root/servers/Server", "servername")

        If m = "" Or m <> s.ServerName Then
            XmlHelper.Insert(xml, "/Root/servers", "Server", "servername", s.ServerName)
            XmlHelper.Insert(xml, "/Root/servers/Server[@servername='" & s.ServerName & "']", "", "uid", s.Uid)
            XmlHelper.Insert(xml, "/Root/servers/Server[@servername='" & s.ServerName & "']", "", "pwd", s.pwd)
            XmlHelper.Insert(xml, "/Root/servers/Server[@servername='" & s.ServerName & "']", "", "DbName", s.DbName)
        Else
            XmlHelper.Update(xml, "/Root/servers/Server[@servername='" & s.ServerName & "']", "uid", s.Uid)
            XmlHelper.Update(xml, "/Root/servers/Server[@servername='" & s.ServerName & "']", "pwd", s.pwd)
            XmlHelper.Update(xml, "/Root/servers/Server[@servername='" & s.ServerName & "']", "DbName", s.DbName)

        End If

        Me.Close()
    End Sub




    Private Sub cboDatabase_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDatabase.GotFocus
        If cboDatabase.Items.Count = 0 Then
            'Dim th As New Thread(AddressOf dblistthreat)
            'th.Start()
        End If
    End Sub





End Class
