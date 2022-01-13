<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSetStatus
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSetStatus = New System.Windows.Forms.Button()
        Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkUrl = New System.Windows.Forms.CheckBox()
        Me.chkGambar = New System.Windows.Forms.CheckBox()
        Me.btnCariGambar = New System.Windows.Forms.Button()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.txtFileGambar = New System.Windows.Forms.TextBox()
        Me.tableLayoutPanel1.SuspendLayout()
        Me.tableLayoutPanel2.SuspendLayout()
        Me.tableLayoutPanel3.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.ColumnCount = 1
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.Controls.Add(Me.btnSetStatus, 0, 1)
        Me.tableLayoutPanel1.Controls.Add(Me.tableLayoutPanel2, 0, 0)
        Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.RowCount = 2
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(411, 146)
        Me.tableLayoutPanel1.TabIndex = 1
        '
        'btnSetStatus
        '
        Me.btnSetStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSetStatus.Location = New System.Drawing.Point(3, 119)
        Me.btnSetStatus.Name = "btnSetStatus"
        Me.btnSetStatus.Size = New System.Drawing.Size(405, 24)
        Me.btnSetStatus.TabIndex = 1
        Me.btnSetStatus.Text = "Set Status"
        Me.btnSetStatus.UseVisualStyleBackColor = True
        '
        'tableLayoutPanel2
        '
        Me.tableLayoutPanel2.ColumnCount = 1
        Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel2.Controls.Add(Me.tableLayoutPanel3, 0, 0)
        Me.tableLayoutPanel2.Controls.Add(Me.groupBox1, 0, 1)
        Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
        Me.tableLayoutPanel2.RowCount = 2
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tableLayoutPanel2.Size = New System.Drawing.Size(405, 110)
        Me.tableLayoutPanel2.TabIndex = 2
        '
        'tableLayoutPanel3
        '
        Me.tableLayoutPanel3.ColumnCount = 2
        Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel3.Controls.Add(Me.label1, 0, 0)
        Me.tableLayoutPanel3.Controls.Add(Me.txtStatus, 1, 0)
        Me.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
        Me.tableLayoutPanel3.RowCount = 2
        Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tableLayoutPanel3.Size = New System.Drawing.Size(399, 29)
        Me.tableLayoutPanel3.TabIndex = 0
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label1.Location = New System.Drawing.Point(3, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(37, 25)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Status"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStatus
        '
        Me.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStatus.Location = New System.Drawing.Point(46, 3)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(350, 20)
        Me.txtStatus.TabIndex = 1
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.chkUrl)
        Me.groupBox1.Controls.Add(Me.chkGambar)
        Me.groupBox1.Controls.Add(Me.btnCariGambar)
        Me.groupBox1.Controls.Add(Me.txtUrl)
        Me.groupBox1.Controls.Add(Me.txtFileGambar)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox1.Location = New System.Drawing.Point(3, 38)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(399, 69)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = " [ Status dengan gambar ] "
        '
        'chkUrl
        '
        Me.chkUrl.AutoSize = True
        Me.chkUrl.Location = New System.Drawing.Point(6, 44)
        Me.chkUrl.Name = "chkUrl"
        Me.chkUrl.Size = New System.Drawing.Size(97, 17)
        Me.chkUrl.TabIndex = 3
        Me.chkUrl.Text = "Gambar dari url"
        Me.chkUrl.UseVisualStyleBackColor = True
        '
        'chkGambar
        '
        Me.chkGambar.AutoSize = True
        Me.chkGambar.Location = New System.Drawing.Point(6, 22)
        Me.chkGambar.Name = "chkGambar"
        Me.chkGambar.Size = New System.Drawing.Size(63, 17)
        Me.chkGambar.TabIndex = 3
        Me.chkGambar.Text = "Gambar"
        Me.chkGambar.UseVisualStyleBackColor = True
        '
        'btnCariGambar
        '
        Me.btnCariGambar.Enabled = False
        Me.btnCariGambar.Location = New System.Drawing.Point(356, 18)
        Me.btnCariGambar.Name = "btnCariGambar"
        Me.btnCariGambar.Size = New System.Drawing.Size(34, 23)
        Me.btnCariGambar.TabIndex = 1
        Me.btnCariGambar.Text = "..."
        Me.btnCariGambar.UseVisualStyleBackColor = True
        '
        'txtUrl
        '
        Me.txtUrl.Location = New System.Drawing.Point(109, 42)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(281, 20)
        Me.txtUrl.TabIndex = 2
        Me.txtUrl.Text = "http://coding4ever.net/assets/images/avatar.png"
        '
        'txtFileGambar
        '
        Me.txtFileGambar.Location = New System.Drawing.Point(109, 20)
        Me.txtFileGambar.Name = "txtFileGambar"
        Me.txtFileGambar.Size = New System.Drawing.Size(241, 20)
        Me.txtFileGambar.TabIndex = 0
        '
        'FrmSetStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 146)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSetStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmContact"
        Me.tableLayoutPanel1.ResumeLayout(False)
        Me.tableLayoutPanel2.ResumeLayout(False)
        Me.tableLayoutPanel3.ResumeLayout(False)
        Me.tableLayoutPanel3.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents tableLayoutPanel1 As TableLayoutPanel
    Private WithEvents btnSetStatus As Button
    Private WithEvents tableLayoutPanel2 As TableLayoutPanel
    Private WithEvents tableLayoutPanel3 As TableLayoutPanel
    Private WithEvents label1 As Label
    Private WithEvents txtStatus As TextBox
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents chkUrl As CheckBox
    Private WithEvents chkGambar As CheckBox
    Private WithEvents btnCariGambar As Button
    Private WithEvents txtUrl As TextBox
    Private WithEvents txtFileGambar As TextBox
End Class
