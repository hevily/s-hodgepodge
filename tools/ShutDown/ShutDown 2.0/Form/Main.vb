Imports Microsoft.VisualBasic
Imports System
Imports System.Threading

Public Class Main
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.FormInit()

    End Sub

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnOK = New System.Windows.Forms.Button
        Me.ObjTimer = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(5, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(245, 45)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "快速设定关机时间:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(53, 18)
        Me.TextBox1.MaximumSize = New System.Drawing.Size(100, 21)
        Me.TextBox1.MinimumSize = New System.Drawing.Size(100, 21)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 21)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(159, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "分钟后关机"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "设定于"
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(5, 56)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(120, 25)
        Me.BtnCancel.TabIndex = 14
        Me.BtnCancel.Text = "取消"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(130, 56)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(120, 25)
        Me.BtnOK.TabIndex = 13
        Me.BtnOK.Text = "确定"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'ObjTimer
        '
        Me.ObjTimer.Interval = 1000
        '
        'Main
        '
        Me.AcceptButton = Me.BtnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(254, 87)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.Text = "Auto Shutdown"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ObjTimer As System.Windows.Forms.Timer

#End Region

    Private Sub FormInit()
        ' Parameters : 
        ' 
        ' Descriptions : 
        '  
        ' 
        ' Return : 
        ' Author/Date : 2011-05-29, Stone Shi
        Try

            Me.TextBox1.Text = "60"
            Me.ObjTimer.Interval = 1000

            Me.BtnOK.Enabled = False
            Me.TextBox1.Enabled = False
            Me.ObjTimer.Enabled = True
            Me.TextBox1.Text = CStr(CInt(Me.TextBox1.Text) - 1).PadLeft(2, "0"c) + ":59"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click, BtnOK.Click
        ' Parameters : 
        ' 
        ' Descriptions : 
        '  
        ' 
        ' Return : 
        ' Author/Date : 2011-05-29, Stone Shi

        Try

            Select Case CType(sender, System.Windows.Forms.Button).Name
                Case Me.BtnOK.Name

                    If CInt(Me.TextBox1.Text) <= 0 Then
                        MessageBox.Show("The number must large than zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    Me.BtnOK.Enabled = False
                    Me.TextBox1.Enabled = False
                    Me.ObjTimer.Enabled = True
                    Me.TextBox1.Text = CStr(CInt(Me.TextBox1.Text) - 1).PadLeft(2, "0"c) + ":59"

                Case Me.BtnCancel.Name

                    If Me.TextBox1.Enabled = False Then
                        Me.TextBox1.Text = (CInt(Me.TextBox1.Text.Split(":"c)(0)) + 1).ToString
                        Me.TextBox1.Enabled = True
                        Me.ObjTimer.Enabled = False
                        Me.BtnOK.Enabled = True
                    Else
                        Me.Dispose()
                    End If

                Case Else

            End Select

        Catch ex As Exception
            MessageBox.Show("Please input a effective number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.TextBox1.Text = "1"
        End Try
    End Sub

    Private Sub TimerTick() Handles ObjTimer.Tick
        ' Parameters : 
        ' 
        ' Descriptions : 
        '  
        ' 
        ' Return : 
        ' Author/Date : 2011-05-29, Stone Shi

        Try

            Dim strTime() As String = Me.TextBox1.Text.Split(":"c)
            Dim intHour As Integer = CInt(strTime(0))
            Dim intMinute As Integer = CInt(strTime(1))

            If intMinute > 0 AndAlso intHour >= 0 Then
                intMinute = intMinute - 1
            ElseIf intMinute = 0 AndAlso intHour > 0 Then
                intMinute = 59
                intHour = intHour - 1
            ElseIf intMinute = 0 AndAlso intHour = 0 Then
                Me.ObjTimer.Enabled = False
                Process.Start("C:\Program Files(Me)\Tencent\TM2009\Bin\TM.exe")
                Me.Dispose()
            End If

            Me.TextBox1.Text = CStr(intHour).PadLeft(2, "0"c) + ":" + CStr(intMinute).PadLeft(2, "0"c)

        Catch ex As Exception

        End Try

    End Sub

End Class





