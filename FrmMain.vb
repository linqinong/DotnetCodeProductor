Imports Microsoft.VisualBasic
Public Class FrmMain
    Dim tm As String
    Dim th As System.Threading.Thread
    Dim dbl As New DB
    Dim connectionstring As String
    Sub ccgc()
        connectionstring = "Data Source=" & s.ServerName & ";Initial Catalog=" & s.DbName & ";User ID=" & s.Uid & ";Password=" & s.pwd
        Dim dt As DataTable = dbl.GetTables(connectionstring)
        If dt Is Nothing Then
            Return
        End If


        TreeView1.Nodes.Clear()
        TreeView1.CheckBoxes = True


        ''得到所有的表
        For Each r As DataRow In dt.Rows
            Dim cl_node As New TreeNode
            cl_node.Text = r("table_name")
            Dim cdt As DataTable = dbl.GetColumnField(connectionstring, r("table_name"))
            If cdt Is Nothing Then
                Continue For
            End If
            For Each cl As Data.DataColumn In cdt.Columns
                node_add(cl_node, cl.ColumnName, cl.DataType.ToString, cl.AutoIncrement)
            Next
            TreeView1.Nodes.Add(cl_node)
        Next
    End Sub

    Function node_add(ByVal pnode As TreeNode, ByVal t As String, ByVal tg As String, ByVal identy As Boolean) As TreeNode
        Dim cnode As New TreeNode
        cnode.Text = t
        cnode.Tag = tg
        If identy Then
            cnode.ToolTipText = "key"
        End If
        pnode.Nodes.Add(cnode)
        Return cnode
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ccgc()
    End Sub

    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck
        If e.Action <> TreeViewAction.Unknown Then
            If e.Node.Nodes.Count > 0 Then '判断有无子节点
                Me.CheckAllChildNodes(e.Node, e.Node.Checked) '选中所有子节点
            End If
            CheckAllFatherNodes(e.Node, e.Node.Checked) '设置父节点状态
            Debug.WriteLine(e.Node.Text) '提示选中节点
        End If
    End Sub
    Private Sub CheckAllChildNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
        '递归选中所有子节点
        Dim node As TreeNode '声明节点

        For Each node In treeNode.Nodes '遍历子节点
            node.Checked = nodeChecked '选中节点
            If node.Nodes.Count > 0 Then '判断有无子节点
                Me.CheckAllChildNodes(node, nodeChecked) '递归调用
            End If
        Next node
    End Sub
    '检查所有的本层节点，如果所有节点都选中，则父节点也选中，如果所有节点非选中，则父节点也不选中

    Private Sub CheckAllFatherNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
        Dim node As TreeNode '声明节点
        Dim isSame As Boolean = True '记下nodeChecked的状态

        If Not treeNode.Parent Is Nothing Then '判断有没有父节点
            For Each node In treeNode.Parent.Nodes '遍历子节点
                '碰到与nodeChecked状态不一样的节点退出循环
                If node.Checked <> nodeChecked Then
                    isSame = False
                    Exit For
                End If
            Next
            If isSame = False Then
                '当所有节点中有一个不一样的节点时，肯定存在一个是选中的节点，所以父节点为选中
                treeNode.Parent.Checked = True
            Else
                '当所有节点的状态都一样时，父节点状态等于节点状态
                treeNode.Parent.Checked = nodeChecked
            End If
            '递归调用
            CheckAllFatherNodes(treeNode.Parent, treeNode.Parent.Checked)
        End If
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim i As Int16

        txt_ui.Text = ""
        tablet.Text = ""

        Dim xml As String = Application.StartupPath & ("/XmlFile.xml")

        For i = 0 To TreeView1.Nodes.Count - 1
            If TreeView1.Nodes(i).Checked = True Then

                txt_insert.Text = ProInsert(TreeView1.Nodes(i)) & ProUpdate(TreeView1.Nodes(i)) & dal(TreeView1.Nodes(i))
                txt_update.Text = bll(TreeView1.Nodes(i))
                txt_ui.Text = UI(TreeView1.Nodes(i))
                tablet.Text = tt(TreeView1.Nodes(i))
                txt_requestCode.Text = rr(TreeView1.Nodes(i))
                txt_initValue.Text = ii(TreeView1.Nodes(i))
                TextBox1.Text = FormUI(TreeView1.Nodes(i))
            End If
        Next
    End Sub
    Function tt(ByVal node As TreeNode) As String
        Dim s As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        s.AppendLine(" Shared function list() as string")
        s.AppendLine(" Dim s As New System.Text.StringBuilder")
        s.AppendLine(" s.AppendLine(""<table class=""""table"""" layoutH=""""118"""" targetType=""""dialog"""" width=""""100%"""">"")")
        s.AppendLine(" s.AppendLine(""<thead>"")")
        Dim i As Integer
        Dim keyzd As String = ""
        s.AppendLine(" s.AppendLine(""<th width=""""30""""><input type=""""checkbox"""" class=""""checkboxCtrl"""" group=""""orgId"""" /></th>"")")
        For i = 0 To node.Nodes.Count - 1
            keyzd = node.Nodes(i).Text
            If node.Nodes(i).Checked = True Then
                s.AppendLine(" s.AppendLine(""<th>" & node.Nodes(i).Text & "</th>"")")
            End If
        Next
        s.AppendLine(" s.AppendLine(""</tr></thead>"")")
        s.AppendLine(" s.AppendLine(""<tbody>"")")


        Dim zd As String = ""
        s.AppendLine(" Dim bll As New BLL." & tb_name.Replace("_", "") & "BLL")

        s.AppendLine("   pi.fldName = """ & node.Nodes(0).Text & """")

        s.AppendLine(" Dim dt As DataTable = bll.PagerDt(pi)")
        s.AppendLine(" for each dr as datarow in dt.rows")
        s.AppendLine(" s.AppendLine(""<tr>"")")

        s.AppendLine("  s.AppendLine(""<td><input type=""""checkbox"""" name=""""orgId"""" /></td>"")")

        For i = 0 To node.Nodes.Count - 1
            If node.Nodes(i).Checked = True Then
                s.AppendLine(" s.AppendLine(" & Chr(34) & "<td>" & Chr(34) & "& dr(""" & node.Nodes(i).Text & """) & ""</td>"")")

            End If
        Next

        s.AppendLine(" s.AppendLine(""</tr>"")")
        s.AppendLine(" next")
        s.AppendLine(" s.AppendLine(""</tbody>"")")
        s.AppendLine(" s.AppendLine(""</table>"")")
        s.AppendLine("return s.tostring")
        s.AppendLine("end function")

        Return s.ToString
    End Function
    Function BigShortString(str As String) As String
        Dim ret As String = ""
        For Each c As Char In str
            If c > "A" And c < "Z" Then
                ret = ret & c
            End If
        Next
        Return ret
    End Function
    Function bll(ByVal node As TreeNode) As String
        Dim s As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        Dim st As String = BigShortString(tb_name)

        Dim qz As String = "DAL." & tb_name & "DAL"
        s.AppendLine("   Shared     Function insert(" & BigShortString(tb_name) & " As Model." & tb_name & ") As Model." & tb_name & "")
        s.AppendLine("   Return " & qz & ".insert(" & BigShortString(tb_name) & ")")
        s.AppendLine("   End Function")
        s.AppendLine("  Shared  Function Update(" & BigShortString(tb_name) & " As Model." & tb_name & ") As Model." & tb_name & "")
        s.AppendLine("     Return " & qz & ".Update(" & BigShortString(tb_name) & ")")

        s.AppendLine("   End Function")

        s.AppendLine("  Shared    Function del(" & node.Nodes(0).Text & " As Integer) As Boolean")
        s.AppendLine("   Return " & qz & ".del(" & node.Nodes(0).Text & ") ")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared    Function Multidel(" & node.Nodes(0).Text & "s As Integer) As Boolean")
        s.AppendLine("   Return " & qz & ".Multidel(" & node.Nodes(0).Text & "s)")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared    Function dt() As datatable")
        s.AppendLine("   Return    " & qz & ".dt() ")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared    Function rs(" & node.Nodes(0).Text & " As Integer) As SqlDataReader")
        s.AppendLine("   Return    " & qz & ".rs(" & node.Nodes(0).Text & " ) ")
        s.AppendLine("  End Function")
        s.AppendLine(" Shared   Function Entity(" & node.Nodes(0).Text & "  As Integer) As Model." & tb_name & "")
        s.AppendLine("  Return  " & qz & ".Entity(" & node.Nodes(0).Text & "  )")
        s.AppendLine("   End Function")
        s.AppendLine(" Shared     Function PagerDt(ByRef pi As PagerInfo) As DataTable")
        s.AppendLine("   Return  " & qz & ".PagerDt(  pi  ) ")
        s.AppendLine("End Function")
        Return s.ToString

    End Function

    Function dal(ByVal node As TreeNode) As String
        Dim s As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        Dim st As String = BigShortString(tb_name)
        s.AppendLine("   Shared     Function del(" & node.Nodes(0).Text & " As Integer) As Boolean")
        s.AppendLine("  Dim sql As String")
        s.AppendLine("   sql = ""delete   from " & node.Text & " where " & node.Nodes(0).Text & "="" & " & node.Nodes(0).Text & "")
        s.AppendLine("   Return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql) > 0")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared      Function Multidel(" & node.Nodes(0).Text & "s As Integer) As Boolean")
        s.AppendLine("  Dim sql As String")
        s.AppendLine("   sql = ""delete   from " & node.Text & " where " & node.Nodes(0).Text & " in("" & " & node.Nodes(0).Text & "s &"")""")
        s.AppendLine("   Return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql) > 0")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared      Function dt() As datatable")
        s.AppendLine("  Dim sql As String")
        s.AppendLine("   sql = ""select * from  " & node.Text)
        s.AppendLine("   Return    SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, sql).Tables(0)")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared      Function rs(" & node.Nodes(0).Text & " As Integer) As SqlDataReader")
        s.AppendLine("  Dim sql As String")
        s.AppendLine("   sql = ""select * from  " & node.Text & " where " & node.Nodes(0).Text & "="" &" & node.Nodes(0).Text & "")
        s.AppendLine("   Return    SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql) ")
        s.AppendLine("  End Function")
        s.AppendLine("  Shared    Function Entity(" & node.Nodes(0).Text & "  As Integer) As Model." & tb_name & "")
        s.AppendLine("  Return Fabricate.Fill(Of Model." & tb_name & ")(rs(" & node.Nodes(0).Text & "))")
        s.AppendLine("   End Function")
        s.AppendLine("  Shared      Function PagerDt(ByRef pi As PagerInfo) As DataTable")
        s.AppendLine("    pi.Tablename = """ & node.Text & """")
        s.AppendLine("   Return PagerHelper.ProPager(pi, ConnStr)")
        s.AppendLine("End Function")
        Return s.ToString

    End Function
    Function ii(ByVal node As TreeNode) As String
        Dim s As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        Dim st As String = BigShortString(tb_name)

        Dim i As Integer
        Dim keyzd As String = ""
        s.AppendLine("    Dim bll As New BLL." & tb_name & "BLL")
        s.AppendLine(" Sub Init_(" & node.Nodes(i).Text & " as integer )")
        s.AppendLine(" Dim " & st & " =bll.entity(" & node.Nodes(i).Text & "  )")
        s.AppendLine(" if " & st & " isnot nothing then")
        For i = 0 To node.Nodes.Count - 1
            keyzd = node.Nodes(i).Text
            If node.Nodes(i).Checked = True Then
                s.AppendLine("txt_" & node.Nodes(i).Text & ".text=" & "" & st & "." & node.Nodes(i).Text & "")
            End If
        Next
        s.AppendLine("end if ")

        s.AppendLine("End sub")

        Return s.ToString
    End Function
    Function rr(ByVal node As TreeNode) As String
        Dim s As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        Dim st As String = BigShortString(tb_name)

        Dim i As Integer
        Dim keyzd As String = ""
        s.AppendLine("function GetPageEntity() as  Model." & tb_name)
        s.AppendLine(" Dim " & st & " As New Model." & tb_name & "")
        For i = 0 To node.Nodes.Count - 1
            keyzd = node.Nodes(i).Text
            If node.Nodes(i).Checked = True Then
                s.AppendLine(st & "." & node.Nodes(i).Text & "  = Request.Form(""txt_" & node.Nodes(i).Text & """)")
            End If
        Next
        s.AppendLine(" Return " & st)
        s.AppendLine("End Function")

        Return s.ToString
    End Function
    Function ProInsert(ByVal node As TreeNode) As String
        Dim s1, s2, s3, sqlstr, ss As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        Dim st As String = BigShortString(tb_name)

        Dim i As Integer
        Dim keyzd As String = ""
        s1.AppendLine(" Shared    function insert(" & st & " as  Model." & tb_name & ") as Model." & tb_name)
        Dim keyid As String = node.Nodes(0).Text
        Dim k As Integer


        For i = 1 To node.Nodes.Count - 1

            keyzd = node.Nodes(i).Text
            If node.Nodes(i).Checked = True Then
               

                ss.Append("," & keyzd)
                sqlstr.Append(",@" & keyzd)
                s2.AppendLine("sqlpara(" & k & ") = New SqlParameter(""@" & keyzd & """, SqlDbType." & getDbType(node.Nodes(i).Tag) & ")")
                s2.AppendLine("sqlpara(" & k & ").Value = " & st & "." & keyzd)
                k = k + 1
            End If
        Next
        s1.AppendLine(" Dim sqlPara(" & k & ") As SqlParameter")
        Dim newsql As String
        newsql = "  Dim sql As String" & vbCrLf & "sql=""insert into " & tb_name & " (" & ss.Remove(0, 1).ToString & ") values (" & sqlstr.Remove(0, 1).ToString & ");SELECT @@IDENTITY"""
        s1.AppendLine(newsql)
        s2.AppendLine(" Dim i As Integer = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, sqlPara)")
        s2.AppendLine(st & "." & keyid & " = i")

        s2.AppendLine(" Return " & st)
        s2.AppendLine("End Function")

        Return s1.ToString & s2.ToString
    End Function
    Function ProUpdate(ByVal node As TreeNode) As String
        Dim s1, s2, s3, sqlstr As New System.Text.StringBuilder
        Dim tb_name As String = node.Text.Replace("_", "")
        Dim st As String = BigShortString(tb_name)

        Dim i As Integer
        Dim keyzd As String = ""
        s1.AppendLine(" Shared   function Update(" & st & " as  Model." & tb_name & ") as Model." & tb_name)
        keyzd = node.Nodes(i).Text
        s2.AppendLine("sqlpara(" & i & ") = New SqlParameter(""@" & keyzd & """, SqlDbType." & getDbType(node.Nodes(i).Tag) & ")")
        s2.AppendLine("sqlpara(" & i & ").Value = " & st & "." & keyzd)
        s3.Append(keyzd & "=@" & Trim(keyzd))
        Dim j As Integer
        For i = 1 To node.Nodes.Count - 1
            If node.Nodes(i).Checked = True Then
                j = j + 1
                keyzd = node.Nodes(i).Text
                sqlstr.Append("," & keyzd & "=@" & Trim(keyzd))
                s2.AppendLine("sqlpara(" & j & ") = New SqlParameter(""@" & keyzd & """, SqlDbType." & getDbType(node.Nodes(i).Tag) & ")")
                s2.AppendLine("sqlpara(" & j & ").Value = " & st & "." & keyzd)
            End If
        Next
        s1.AppendLine(" Dim sqlPara(" & j & ") As SqlParameter")
        Dim newsql As String
        newsql = "  Dim sql As String" & vbCrLf & "sql=""update " & tb_name & "  set " & sqlstr.Remove(0, 1).ToString & "  where  " & s3.ToString & " """
        s1.AppendLine(newsql)

        s2.AppendLine(" Dim i As Integer = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, sqlPara)")


        s2.AppendLine(" Return " & st)
        s2.AppendLine("End Function")

        Return s1.ToString & s2.ToString
    End Function
    Function FormUI(ByVal node As TreeNode) As String

        Dim s As New System.Text.StringBuilder

        Dim tb_name As String = node.Text
        Dim st As String = BigShortString(tb_name)

        Dim i As Integer
        Dim keyzd As String = ""


        For i = 0 To node.Nodes.Count - 1
            keyzd = node.Nodes(i).Text
            If node.Nodes(i).Checked = True Then
                s.AppendLine("<div class=""control-group"">")
                s.AppendLine("<label class=""control-label"" for=""txt_" & keyzd & """>keyzd</label>")
                s.AppendLine("<div class=""controls"">")
                s.AppendLine(GetControl(node.Nodes(i).Tag, keyzd))
                s.AppendLine("</div>")
                s.AppendLine("</div>")


            End If
        Next
        Return s.ToString
    End Function

    Function UI(ByVal node As TreeNode) As String

        Dim s As New System.Text.StringBuilder

        Dim tb_name As String = node.Text
        Dim st As String = BigShortString(tb_name)

        Dim i As Integer
        Dim keyzd As String = ""


        For i = 0 To node.Nodes.Count - 1
            keyzd = node.Nodes(i).Text
            If node.Nodes(i).Checked = True Then
                s.AppendLine("<dl>")
                s.AppendLine("<dt>" & keyzd & "：</dt>")
                s.AppendLine("<dd>")
                s.AppendLine(GetControl(node.Nodes(i).Tag, keyzd))

                s.AppendLine("</dd>")

                s.AppendLine("</dl>")


            End If
        Next
        Return s.ToString
    End Function
    Function GetControl(s As String, keyzd As String) As String
        s = LCase(s)
        If InStr(s, "bit") Then
            Return (" <asp:dropdownlist id=""txt_" & keyzd & """ runat=""server""  " & getClass(s) & "></asp:dropdownlist>")

        Else
            Return (" <asp:textbox id=""txt_" & keyzd & """ runat=""server""  " & getClass(s) & "></asp:textbox>")
        End If
    End Function
    Function getClass(s As String) As String
        s = LCase(s)
        If InStr(s, "datetime") Then
            Return "cssclass=""date required"""
        ElseIf InStr(s, "bit") Then
            Return "cssclass=""combox required"""

        End If
    End Function
    Function getDbType(s As String) As String
        s = LCase(s)
        If InStr(s, "nvarchar") Or InStr(s, "string") Then
            Return "nvarchar"
        ElseIf InStr(s, "int") Then
            Return "int"
        ElseIf InStr(s, "datetime") Then
            Return "datetime"
        ElseIf InStr(s, "boolean") Then
            Return "bit"
        ElseIf InStr(s, "decimal") Then
            Return "Decimal"
        ElseIf InStr(s, "ntext") Then
            Return "NText"
        ElseIf InStr(s, "double") Then
            Return "float"
        Else

            Return s
        End If
    End Function
    Function getVbType(s As String) As String
        If InStr(s, "nvarchar") Or InStr(s, "ntext") Then
            Return "string"
        ElseIf InStr(s, "int") Then
            Return "integer"
        ElseIf InStr(s, "datetime") Then
            Return "datetime"
        ElseIf InStr(s, "bit") Then
            Return "boolean"
        ElseIf InStr(s, "float") Then
            Return "single"
        End If
    End Function

    Function gt(ByVal t As String) As String
        If t = "int" Then
            gt = "Int16"
        ElseIf InStr(t, "nvarchar") > 0 Or t = "text" Then
            gt = "string"
        ElseIf t = "bit" Then
            gt = "boolean"
        ElseIf t = "datetime" Then
            gt = "datetime"
        End If


    End Function
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If FrmDataBaseInfoSet.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TreeView1.Nodes.Clear()

            ccgc()
        End If

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        TreeView1.Nodes.Clear()
        ccgc()
    End Sub




    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        th = New Threading.Thread(AddressOf RunsOnWorkerThread)
        th.Start()


    End Sub
    Sub RunsOnWorkerThread()
        Dim mi As New MethodInvoker(AddressOf init)
        BeginInvoke(mi)
    End Sub

    Sub init()

        ccgc()

    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        XMLForm.Show()
    End Sub

    Private Sub txt_ui_GotFocus(sender As Object, e As EventArgs) Handles txt_ui.GotFocus
        Clipboard.SetText(txt_ui.Text)

    End Sub


    Private Sub txt_update_GotFocus(sender As Object, e As EventArgs) Handles txt_update.GotFocus
        Clipboard.SetText(txt_update.Text)

    End Sub

    Private Sub txt_initValue_GotFocus(sender As Object, e As EventArgs) Handles txt_initValue.GotFocus
        Clipboard.SetText(txt_initValue.Text)

    End Sub

    Private Sub txt_requestCode_GotFocus(sender As Object, e As EventArgs) Handles txt_requestCode.GotFocus
        Clipboard.SetText(txt_requestCode.Text)

    End Sub

    Private Sub txt_insert_GotFocus(sender As Object, e As EventArgs) Handles txt_insert.GotFocus
        Clipboard.SetText(txt_insert.Text)

    End Sub
End Class