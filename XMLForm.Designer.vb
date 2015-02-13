<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XMLForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.nodepath = New System.Windows.Forms.TextBox
        Me.valuet = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.s = New System.Windows.Forms.Label
        Me.att = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.elet = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'nodepath
        '
        Me.nodepath.Location = New System.Drawing.Point(66, 14)
        Me.nodepath.Name = "nodepath"
        Me.nodepath.Size = New System.Drawing.Size(230, 21)
        Me.nodepath.TabIndex = 0
        '
        'valuet
        '
        Me.valuet.Location = New System.Drawing.Point(66, 74)
        Me.valuet.Multiline = True
        Me.valuet.Name = "valuet"
        Me.valuet.Size = New System.Drawing.Size(407, 102)
        Me.valuet.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(66, 192)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "插入"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(150, 192)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "修改"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "节点"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "属性值"
        '
        's
        '
        Me.s.AutoSize = True
        Me.s.Location = New System.Drawing.Point(14, 43)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(29, 12)
        Me.s.TabIndex = 6
        Me.s.Text = "属性"
        '
        'att
        '
        Me.att.Location = New System.Drawing.Point(66, 43)
        Me.att.Name = "att"
        Me.att.Size = New System.Drawing.Size(230, 21)
        Me.att.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(303, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "元素"
        '
        'elet
        '
        Me.elet.Location = New System.Drawing.Point(339, 17)
        Me.elet.Name = "elet"
        Me.elet.Size = New System.Drawing.Size(100, 21)
        Me.elet.TabIndex = 9
        '
        'XMLForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 227)
        Me.Controls.Add(Me.elet)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.att)
        Me.Controls.Add(Me.s)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.valuet)
        Me.Controls.Add(Me.nodepath)
        Me.Name = "XMLForm"
        Me.Text = "XMLForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents nodepath As System.Windows.Forms.TextBox
    Friend WithEvents valuet As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents s As System.Windows.Forms.Label
    Friend WithEvents att As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents elet As System.Windows.Forms.TextBox
End Class
