Imports Microsoft.VisualBasic
Imports System
Imports System.Threading

Public Class FrmShutDownTest
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

        FormInit()

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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NumericUpDown6 = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown1)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 59)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(245, 74)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "设定关机时间:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(201, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "秒"
        '
        'NumericUpDown6
        '
        Me.NumericUpDown6.Location = New System.Drawing.Point(161, 45)
        Me.NumericUpDown6.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumericUpDown6.Name = "NumericUpDown6"
        Me.NumericUpDown6.Size = New System.Drawing.Size(37, 21)
        Me.NumericUpDown6.TabIndex = 7
        Me.NumericUpDown6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(136, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(19, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "分"
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Location = New System.Drawing.Point(96, 45)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(37, 21)
        Me.NumericUpDown5.TabIndex = 6
        Me.NumericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(64, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "时"
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Location = New System.Drawing.Point(8, 45)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(53, 21)
        Me.NumericUpDown4.TabIndex = 5
        Me.NumericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(201, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(19, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "日"
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(161, 18)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.NumericUpDown3.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(37, 21)
        Me.NumericUpDown3.TabIndex = 4
        Me.NumericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(136, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "月"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(96, 18)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(37, 21)
        Me.NumericUpDown2.TabIndex = 3
        Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(64, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(19, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "年"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(8, 18)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(53, 21)
        Me.NumericUpDown1.TabIndex = 2
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Controls.Add(Me.RadioButton4)
        Me.GroupBox3.Controls.Add(Me.RadioButton3)
        Me.GroupBox3.Controls.Add(Me.RadioButton2)
        Me.GroupBox3.Controls.Add(Me.RadioButton1)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 141)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(245, 71)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "选择关机类型:"
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(94, 40)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(121, 21)
        Me.TextBox2.TabIndex = 12
        Me.TextBox2.Text = "Time out!"
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Enabled = False
        Me.RadioButton4.Location = New System.Drawing.Point(85, 18)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(65, 16)
        Me.RadioButton4.TabIndex = 9
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "重启(R)"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Enabled = False
        Me.RadioButton3.Location = New System.Drawing.Point(10, 42)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(77, 16)
        Me.RadioButton3.TabIndex = 11
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "仅提示(A)"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Enabled = False
        Me.RadioButton2.Location = New System.Drawing.Point(158, 18)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(65, 16)
        Me.RadioButton2.TabIndex = 10
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "注销(L)"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(10, 18)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(65, 16)
        Me.RadioButton1.TabIndex = 8
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "关机(P)"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(5, 214)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 25)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "取消(C)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(134, 214)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 25)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "确定(S)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FrmShutDown
        '
        Me.AcceptButton = Me.Button2
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(254, 241)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximumSize = New System.Drawing.Size(270, 279)
        Me.MinimumSize = New System.Drawing.Size(270, 279)
        Me.Name = "FrmShutDown"
        Me.Text = "定时关机-Stone制作"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton

#End Region

#Region " Variables "

    Dim _intmonth As Integer = Nothing
    Dim _intday As Integer = Nothing
    Dim _inthour As Integer = Nothing
    Dim _intminute As Integer = Nothing
    Dim _boolclick As Boolean = False
    Dim _boolTboxChange As Boolean = False
    Dim _boolNumericChange As Boolean = False
    Dim longTime As Long = 36000000000

#End Region

#Region " Method "

    Private Sub FormInit()
        ' Description :
        '	
        '
        ' Author/Date : 2011-05-29, Stone Shi

        Try
            'Me.MinimizeBox = False
            Me.MaximizeBox = False
            Dim ojbdate As Date = New Date(Date.Now.Ticks + longTime)
            Me.TextBox1.Text = "60"
            Me.NumericUpDown1.Maximum = ojbdate.Year + 100
            Me.NumericUpDown1.Minimum = ojbdate.Year - 100
            Me.NumericUpDown1.Value = ojbdate.Year
            Me.NumericUpDown2.Value = ojbdate.Month
            Me.NumericUpDown3.Value = ojbdate.Day
            Me.NumericUpDown4.Value = ojbdate.Hour
            Me.NumericUpDown5.Value = ojbdate.Minute
            Me.NumericUpDown6.Value = ojbdate.Second
            SetMaxDay()
            _intmonth = ojbdate.Month
            _intday = ojbdate.Day
            _inthour = ojbdate.Hour + 1
            _intminute = ojbdate.Minute
            _boolclick = True
        Catch objE As Exception
            CloseForm()
        End Try
    End Sub

    Private Sub CloseForm()
        ' Description :
        '	
        '
        ' Author/Date : 2011-05-29, Stone Shi

        Try
            MessageBox.Show("程序错误！！！")
            Me.Dispose()
            Me.Close()
        Catch ex As Exception
            Me.CloseForm()
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Description :
        '	
        '
        ' Author/Date : 2011-05-29, Stone Shi

        Try
            'ControlShow(True)
            '_boolNumericChange = True
            'Datechange()
            'Command1_Click()

            Shell("c:\windows\system32\shutdown -s -t 0")
        Catch ex As Exception
            Me.CloseForm()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Description :
        '	
        '
        ' Author/Date : 2011-05-29, Stone Shi

        Try
            ControlShow(False)
            Me.Close()
        Catch ex As Exception
            Me.CloseForm()
        End Try
    End Sub

#End Region

    'Private Declare Function RtlAdjustPrivilege& Lib "ntdll" (ByVal Privilege&, ByVal NewValue&, ByVal NewThread&, ByVal OldValue&)
    'Private Declare Function NtShutdownSystem& Lib "ntdll" (ByVal ShutdownAction&)
    'Private Const SE_SHUTDOWN_PRIVILEGE& = 19
    'Private Const SHUTDOWN& = 0
    'Private Const RESTART& = 1
    'Private Const POWEROFF& = 2

    'Private Sub Command1_Click() 'PowerOff 
    '    RtlAdjustPrivilege(SE_SHUTDOWN_PRIVILEGE, 1, 0, 0)
    '    NtShutdownSystem(POWEROFF)
    'End Sub











    Private Sub SetMaxDay()
        If Me.NumericUpDown2.Value = 1 Or Me.NumericUpDown2.Value = 3 Or Me.NumericUpDown2.Value = 5 _
        Or Me.NumericUpDown2.Value = 7 Or Me.NumericUpDown2.Value = 8 Or Me.NumericUpDown2.Value = 10 Or Me.NumericUpDown2.Value = 12 Then
            Me.NumericUpDown3.Maximum = 31
        ElseIf Me.NumericUpDown2.Value = 4 Or Me.NumericUpDown2.Value = 6 Or Me.NumericUpDown2.Value = 9 Or Me.NumericUpDown2.Value = 11 Then
            Me.NumericUpDown3.Maximum = 30
        Else
            If Date.IsLeapYear(CInt(Me.NumericUpDown1.Value)) Then
                Me.NumericUpDown3.Maximum = 29
            Else
                Me.NumericUpDown3.Maximum = 28
            End If
        End If
    End Sub

    Private Sub ControlShow(ByVal boolvalue As Boolean)
        If boolvalue Then
            Me.TextBox1.Enabled = False
            Me.NumericUpDown1.Enabled = False
            Me.NumericUpDown2.Enabled = False
            Me.NumericUpDown3.Enabled = False
            Me.NumericUpDown4.Enabled = False
            Me.NumericUpDown5.Enabled = False
            Me.NumericUpDown6.Enabled = False
        Else
            Me.TextBox1.Enabled = True
            Me.NumericUpDown1.Enabled = True
            Me.NumericUpDown2.Enabled = True
            Me.NumericUpDown3.Enabled = True
            Me.NumericUpDown4.Enabled = True
            Me.NumericUpDown5.Enabled = True
            Me.NumericUpDown6.Enabled = True
        End If
    End Sub

    Private Sub SetTime(ByVal longTime As Long)
        _boolTboxChange = True
        _boolNumericChange = True
        Dim ojbdate As Date = New Date(longTime)
        Me.NumericUpDown1.Value = ojbdate.Year
        Me.NumericUpDown2.Value = ojbdate.Month
        Me.NumericUpDown3.Value = ojbdate.Day
        Me.NumericUpDown4.Value = ojbdate.Hour
        Me.NumericUpDown5.Value = ojbdate.Minute
        Me.NumericUpDown6.Value = ojbdate.Second
        _boolNumericChange = False
        _boolTboxChange = False
    End Sub

    Private Sub Datechange()
        Dim ojbdate As Date = Date.Now
        Dim ojbdate1 As Date = New Date(CInt(Me.NumericUpDown1.Value), CInt(Me.NumericUpDown2.Value), CInt(Me.NumericUpDown3.Value), _
                                        CInt(Me.NumericUpDown4.Value), CInt(Me.NumericUpDown5.Value), CInt(Me.NumericUpDown6.Value))
        Me.TextBox1.Text = Str((ojbdate1 - ojbdate).Hours * 60 + (ojbdate1 - ojbdate).Days * 24 * 60 + (ojbdate1 - ojbdate).Minutes)
        longTime = CLng(Me.TextBox1.Text) * 60 * 10000000
        'If ojbdate1.Ticks - ojbdate.Ticks - longTime > 300000000 Then
        '    SetTime(Date.Now.Ticks + longTime)
        'End If
        'MessageBox.Show((ojbdate1 - ojbdate).ToString, "", MessageBoxButtons.OK)
    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If _boolclick And Not _boolNumericChange Then
            Try
                longTime = CLng(Me.TextBox1.Text) * 60 * 10000000
                Dim ojbdate As Date = New Date(Date.Now.Ticks + longTime)
                _boolTboxChange = True
                Me.NumericUpDown1.Value = ojbdate.Year
                Me.NumericUpDown2.Value = ojbdate.Month
                Me.NumericUpDown3.Value = ojbdate.Day
                Me.NumericUpDown4.Value = ojbdate.Hour
                Me.NumericUpDown5.Value = ojbdate.Minute
                Me.NumericUpDown6.Value = ojbdate.Second
            Catch
            End Try
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If _boolclick And Not _boolTboxChange Then
            _boolNumericChange = True
            Datechange()
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        SetMaxDay()
        If _boolclick And Not _boolTboxChange Then
            _boolNumericChange = True
            Datechange()
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        If _boolclick And Not _boolTboxChange Then
            _boolNumericChange = True
            Datechange()
        End If
    End Sub

    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        If _boolclick And Not _boolTboxChange Then
            _boolNumericChange = True
            Datechange()
        End If
    End Sub

    Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        If _boolclick And Not _boolTboxChange Then
            _boolNumericChange = True
            Datechange()
        End If
    End Sub

    Private Sub NumericUpDown6_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown6.ValueChanged
        If _boolclick And Not _boolTboxChange Then
            _boolNumericChange = True
            Datechange()
        End If
    End Sub



    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        _boolTboxChange = False
        _boolNumericChange = False
    End Sub

    Private Sub NumericUpDown1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown1.Leave
        _boolTboxChange = False
        _boolNumericChange = False
    End Sub

    Private Sub NumericUpDown2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown2.Leave
        _boolTboxChange = False
        _boolNumericChange = False
    End Sub

    Private Sub NumericUpDown3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown3.Leave
        _boolTboxChange = False
        _boolNumericChange = False
    End Sub

    Private Sub NumericUpDown4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown4.Leave
        _boolTboxChange = False
        _boolNumericChange = False
    End Sub

    Private Sub NumericUpDown5_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown5.Leave
        _boolTboxChange = False
        _boolNumericChange = False
    End Sub

End Class








