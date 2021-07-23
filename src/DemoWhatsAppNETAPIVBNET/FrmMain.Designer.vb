<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnGrabContacts = New System.Windows.Forms.Button()
        Me.btnGrabGroupAndMembers = New System.Windows.Forms.Button()
        Me.btnUnreadMessages = New System.Windows.Forms.Button()
        Me.btnArchiveChat = New System.Windows.Forms.Button()
        Me.btnDeleteChat = New System.Windows.Forms.Button()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtKontak = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtPesan = New System.Windows.Forms.TextBox()
        Me.flowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnKirim = New System.Windows.Forms.Button()
        Me.txtJumlahPesan = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.flowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkKirimPesanDgGambar = New System.Windows.Forms.CheckBox()
        Me.txtFileGambar = New System.Windows.Forms.TextBox()
        Me.btnCariGambar = New System.Windows.Forms.Button()
        Me.flowLayoutPanel5 = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkKirimFileAja = New System.Windows.Forms.CheckBox()
        Me.txtFileDokumen = New System.Windows.Forms.TextBox()
        Me.btnCariDokumen = New System.Windows.Forms.Button()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.flowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkSubscribe = New System.Windows.Forms.CheckBox()
        Me.chkAutoReplay = New System.Windows.Forms.CheckBox()
        Me.lstPesanMasuk = New System.Windows.Forms.ListBox()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.tableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMessageSentSubscribe = New System.Windows.Forms.CheckBox()
        Me.lstPesanKeluar = New System.Windows.Forms.ListBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnLokasiPenyimpananFileAtauGambar = New System.Windows.Forms.Button()
        Me.btnLokasiWAAutomateNodejs = New System.Windows.Forms.Button()
        Me.txtLokasiPenyimpananFileAtauGambar = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtLokasiWhatsAppNETAPINodeJs = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.tableLayoutPanel1.SuspendLayout()
        Me.flowLayoutPanel1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.tableLayoutPanel2.SuspendLayout()
        Me.flowLayoutPanel2.SuspendLayout()
        Me.flowLayoutPanel4.SuspendLayout()
        Me.flowLayoutPanel5.SuspendLayout()
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.tableLayoutPanel3.SuspendLayout()
        Me.flowLayoutPanel3.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.tableLayoutPanel4.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.ColumnCount = 1
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.Controls.Add(Me.flowLayoutPanel1, 0, 1)
        Me.tableLayoutPanel1.Controls.Add(Me.groupBox1, 0, 2)
        Me.tableLayoutPanel1.Controls.Add(Me.tabControl1, 0, 3)
        Me.tableLayoutPanel1.Controls.Add(Me.groupBox2, 0, 0)
        Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.RowCount = 4
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 205.0!))
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(773, 578)
        Me.tableLayoutPanel1.TabIndex = 1
        '
        'flowLayoutPanel1
        '
        Me.flowLayoutPanel1.Controls.Add(Me.btnStart)
        Me.flowLayoutPanel1.Controls.Add(Me.btnStop)
        Me.flowLayoutPanel1.Controls.Add(Me.btnGrabContacts)
        Me.flowLayoutPanel1.Controls.Add(Me.btnGrabGroupAndMembers)
        Me.flowLayoutPanel1.Controls.Add(Me.btnUnreadMessages)
        Me.flowLayoutPanel1.Controls.Add(Me.btnArchiveChat)
        Me.flowLayoutPanel1.Controls.Add(Me.btnDeleteChat)
        Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel1.Location = New System.Drawing.Point(3, 78)
        Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
        Me.flowLayoutPanel1.Size = New System.Drawing.Size(767, 30)
        Me.flowLayoutPanel1.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(3, 3)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(84, 3)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 23)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnGrabContacts
        '
        Me.btnGrabContacts.Enabled = False
        Me.btnGrabContacts.Location = New System.Drawing.Point(165, 3)
        Me.btnGrabContacts.Name = "btnGrabContacts"
        Me.btnGrabContacts.Size = New System.Drawing.Size(97, 23)
        Me.btnGrabContacts.TabIndex = 3
        Me.btnGrabContacts.Text = "Contacts"
        Me.btnGrabContacts.UseVisualStyleBackColor = True
        '
        'btnGrabGroupAndMembers
        '
        Me.btnGrabGroupAndMembers.Enabled = False
        Me.btnGrabGroupAndMembers.Location = New System.Drawing.Point(268, 3)
        Me.btnGrabGroupAndMembers.Name = "btnGrabGroupAndMembers"
        Me.btnGrabGroupAndMembers.Size = New System.Drawing.Size(152, 23)
        Me.btnGrabGroupAndMembers.TabIndex = 5
        Me.btnGrabGroupAndMembers.Text = "Group and members"
        Me.btnGrabGroupAndMembers.UseVisualStyleBackColor = True
        '
        'btnUnreadMessages
        '
        Me.btnUnreadMessages.Enabled = False
        Me.btnUnreadMessages.Location = New System.Drawing.Point(426, 3)
        Me.btnUnreadMessages.Name = "btnUnreadMessages"
        Me.btnUnreadMessages.Size = New System.Drawing.Size(116, 23)
        Me.btnUnreadMessages.TabIndex = 4
        Me.btnUnreadMessages.Text = "Unread Messages"
        Me.btnUnreadMessages.UseVisualStyleBackColor = True
        '
        'btnArchiveChat
        '
        Me.btnArchiveChat.Enabled = False
        Me.btnArchiveChat.Location = New System.Drawing.Point(548, 3)
        Me.btnArchiveChat.Name = "btnArchiveChat"
        Me.btnArchiveChat.Size = New System.Drawing.Size(109, 23)
        Me.btnArchiveChat.TabIndex = 4
        Me.btnArchiveChat.Text = "Archive Chat"
        Me.btnArchiveChat.UseVisualStyleBackColor = True
        '
        'btnDeleteChat
        '
        Me.btnDeleteChat.Enabled = False
        Me.btnDeleteChat.Location = New System.Drawing.Point(663, 3)
        Me.btnDeleteChat.Name = "btnDeleteChat"
        Me.btnDeleteChat.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteChat.TabIndex = 4
        Me.btnDeleteChat.Text = "Delete Chat"
        Me.btnDeleteChat.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.tableLayoutPanel2)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox1.Location = New System.Drawing.Point(3, 114)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(767, 199)
        Me.groupBox1.TabIndex = 1
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = " [ Kirim Pesan ] "
        '
        'tableLayoutPanel2
        '
        Me.tableLayoutPanel2.ColumnCount = 2
        Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel2.Controls.Add(Me.label1, 0, 0)
        Me.tableLayoutPanel2.Controls.Add(Me.txtKontak, 1, 0)
        Me.tableLayoutPanel2.Controls.Add(Me.label3, 0, 1)
        Me.tableLayoutPanel2.Controls.Add(Me.txtPesan, 1, 1)
        Me.tableLayoutPanel2.Controls.Add(Me.flowLayoutPanel2, 1, 4)
        Me.tableLayoutPanel2.Controls.Add(Me.flowLayoutPanel4, 1, 2)
        Me.tableLayoutPanel2.Controls.Add(Me.flowLayoutPanel5, 1, 3)
        Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
        Me.tableLayoutPanel2.RowCount = 5
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.tableLayoutPanel2.Size = New System.Drawing.Size(761, 180)
        Me.tableLayoutPanel2.TabIndex = 0
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label1.Location = New System.Drawing.Point(3, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(41, 25)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Kontak"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKontak
        '
        Me.txtKontak.Location = New System.Drawing.Point(50, 3)
        Me.txtKontak.Name = "txtKontak"
        Me.txtKontak.Size = New System.Drawing.Size(167, 20)
        Me.txtKontak.TabIndex = 0
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label3.Location = New System.Drawing.Point(3, 25)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(41, 64)
        Me.label3.TabIndex = 0
        Me.label3.Text = "Pesan"
        '
        'txtPesan
        '
        Me.txtPesan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPesan.Location = New System.Drawing.Point(50, 28)
        Me.txtPesan.Multiline = True
        Me.txtPesan.Name = "txtPesan"
        Me.txtPesan.Size = New System.Drawing.Size(708, 58)
        Me.txtPesan.TabIndex = 1
        '
        'flowLayoutPanel2
        '
        Me.flowLayoutPanel2.Controls.Add(Me.btnKirim)
        Me.flowLayoutPanel2.Controls.Add(Me.txtJumlahPesan)
        Me.flowLayoutPanel2.Controls.Add(Me.label2)
        Me.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flowLayoutPanel2.Location = New System.Drawing.Point(50, 148)
        Me.flowLayoutPanel2.Name = "flowLayoutPanel2"
        Me.flowLayoutPanel2.Size = New System.Drawing.Size(708, 29)
        Me.flowLayoutPanel2.TabIndex = 2
        '
        'btnKirim
        '
        Me.btnKirim.Enabled = False
        Me.btnKirim.Location = New System.Drawing.Point(630, 3)
        Me.btnKirim.Name = "btnKirim"
        Me.btnKirim.Size = New System.Drawing.Size(75, 23)
        Me.btnKirim.TabIndex = 2
        Me.btnKirim.Text = "Kirim"
        Me.btnKirim.UseVisualStyleBackColor = True
        '
        'txtJumlahPesan
        '
        Me.txtJumlahPesan.Location = New System.Drawing.Point(599, 3)
        Me.txtJumlahPesan.Name = "txtJumlahPesan"
        Me.txtJumlahPesan.Size = New System.Drawing.Size(25, 20)
        Me.txtJumlahPesan.TabIndex = 1
        Me.txtJumlahPesan.Text = "1"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label2.Location = New System.Drawing.Point(466, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(127, 29)
        Me.label2.TabIndex = 0
        Me.label2.Text = "Broadcast (jumlah Pesan)"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'flowLayoutPanel4
        '
        Me.flowLayoutPanel4.Controls.Add(Me.chkKirimPesanDgGambar)
        Me.flowLayoutPanel4.Controls.Add(Me.txtFileGambar)
        Me.flowLayoutPanel4.Controls.Add(Me.btnCariGambar)
        Me.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel4.Location = New System.Drawing.Point(47, 89)
        Me.flowLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.flowLayoutPanel4.Name = "flowLayoutPanel4"
        Me.flowLayoutPanel4.Size = New System.Drawing.Size(714, 28)
        Me.flowLayoutPanel4.TabIndex = 3
        '
        'chkKirimPesanDgGambar
        '
        Me.chkKirimPesanDgGambar.AutoSize = True
        Me.chkKirimPesanDgGambar.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkKirimPesanDgGambar.Location = New System.Drawing.Point(3, 3)
        Me.chkKirimPesanDgGambar.Name = "chkKirimPesanDgGambar"
        Me.chkKirimPesanDgGambar.Size = New System.Drawing.Size(133, 23)
        Me.chkKirimPesanDgGambar.TabIndex = 0
        Me.chkKirimPesanDgGambar.Text = "Kirim pesan dg gambar"
        Me.chkKirimPesanDgGambar.UseVisualStyleBackColor = True
        '
        'txtFileGambar
        '
        Me.txtFileGambar.Location = New System.Drawing.Point(142, 3)
        Me.txtFileGambar.Name = "txtFileGambar"
        Me.txtFileGambar.ReadOnly = True
        Me.txtFileGambar.Size = New System.Drawing.Size(526, 20)
        Me.txtFileGambar.TabIndex = 1
        '
        'btnCariGambar
        '
        Me.btnCariGambar.Enabled = False
        Me.btnCariGambar.Location = New System.Drawing.Point(674, 3)
        Me.btnCariGambar.Name = "btnCariGambar"
        Me.btnCariGambar.Size = New System.Drawing.Size(34, 23)
        Me.btnCariGambar.TabIndex = 2
        Me.btnCariGambar.Text = "..."
        Me.btnCariGambar.UseVisualStyleBackColor = True
        '
        'flowLayoutPanel5
        '
        Me.flowLayoutPanel5.Controls.Add(Me.chkKirimFileAja)
        Me.flowLayoutPanel5.Controls.Add(Me.txtFileDokumen)
        Me.flowLayoutPanel5.Controls.Add(Me.btnCariDokumen)
        Me.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel5.Location = New System.Drawing.Point(47, 117)
        Me.flowLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.flowLayoutPanel5.Name = "flowLayoutPanel5"
        Me.flowLayoutPanel5.Size = New System.Drawing.Size(714, 28)
        Me.flowLayoutPanel5.TabIndex = 3
        '
        'chkKirimFileAja
        '
        Me.chkKirimFileAja.AutoSize = True
        Me.chkKirimFileAja.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkKirimFileAja.Location = New System.Drawing.Point(3, 3)
        Me.chkKirimFileAja.Name = "chkKirimFileAja"
        Me.chkKirimFileAja.Size = New System.Drawing.Size(88, 23)
        Me.chkKirimFileAja.TabIndex = 0
        Me.chkKirimFileAja.Text = "Kirim file (pdf)"
        Me.chkKirimFileAja.UseVisualStyleBackColor = True
        '
        'txtFileDokumen
        '
        Me.txtFileDokumen.Location = New System.Drawing.Point(97, 3)
        Me.txtFileDokumen.Name = "txtFileDokumen"
        Me.txtFileDokumen.ReadOnly = True
        Me.txtFileDokumen.Size = New System.Drawing.Size(571, 20)
        Me.txtFileDokumen.TabIndex = 1
        '
        'btnCariDokumen
        '
        Me.btnCariDokumen.Enabled = False
        Me.btnCariDokumen.Location = New System.Drawing.Point(674, 3)
        Me.btnCariDokumen.Name = "btnCariDokumen"
        Me.btnCariDokumen.Size = New System.Drawing.Size(34, 23)
        Me.btnCariDokumen.TabIndex = 2
        Me.btnCariDokumen.Text = "..."
        Me.btnCariDokumen.UseVisualStyleBackColor = True
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(3, 319)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(767, 256)
        Me.tabControl1.TabIndex = 2
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.tableLayoutPanel3)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(759, 230)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Pesan Masuk"
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'tableLayoutPanel3
        '
        Me.tableLayoutPanel3.ColumnCount = 1
        Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel3.Controls.Add(Me.flowLayoutPanel3, 0, 0)
        Me.tableLayoutPanel3.Controls.Add(Me.lstPesanMasuk, 0, 1)
        Me.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
        Me.tableLayoutPanel3.RowCount = 2
        Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel3.Size = New System.Drawing.Size(753, 224)
        Me.tableLayoutPanel3.TabIndex = 0
        '
        'flowLayoutPanel3
        '
        Me.flowLayoutPanel3.Controls.Add(Me.chkSubscribe)
        Me.flowLayoutPanel3.Controls.Add(Me.chkAutoReplay)
        Me.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.flowLayoutPanel3.Name = "flowLayoutPanel3"
        Me.flowLayoutPanel3.Size = New System.Drawing.Size(747, 20)
        Me.flowLayoutPanel3.TabIndex = 0
        '
        'chkSubscribe
        '
        Me.chkSubscribe.AutoSize = True
        Me.chkSubscribe.Enabled = False
        Me.chkSubscribe.Location = New System.Drawing.Point(3, 3)
        Me.chkSubscribe.Name = "chkSubscribe"
        Me.chkSubscribe.Size = New System.Drawing.Size(73, 17)
        Me.chkSubscribe.TabIndex = 0
        Me.chkSubscribe.Text = "Subscribe"
        Me.chkSubscribe.UseVisualStyleBackColor = True
        '
        'chkAutoReplay
        '
        Me.chkAutoReplay.AutoSize = True
        Me.chkAutoReplay.Enabled = False
        Me.chkAutoReplay.Location = New System.Drawing.Point(82, 3)
        Me.chkAutoReplay.Name = "chkAutoReplay"
        Me.chkAutoReplay.Size = New System.Drawing.Size(84, 17)
        Me.chkAutoReplay.TabIndex = 1
        Me.chkAutoReplay.Text = "Auto Replay"
        Me.chkAutoReplay.UseVisualStyleBackColor = True
        '
        'lstPesanMasuk
        '
        Me.lstPesanMasuk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstPesanMasuk.FormattingEnabled = True
        Me.lstPesanMasuk.Location = New System.Drawing.Point(3, 29)
        Me.lstPesanMasuk.Name = "lstPesanMasuk"
        Me.lstPesanMasuk.Size = New System.Drawing.Size(747, 192)
        Me.lstPesanMasuk.TabIndex = 1
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.tableLayoutPanel4)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(759, 230)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "Pesan Keluar"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'tableLayoutPanel4
        '
        Me.tableLayoutPanel4.ColumnCount = 1
        Me.tableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel4.Controls.Add(Me.chkMessageSentSubscribe, 0, 0)
        Me.tableLayoutPanel4.Controls.Add(Me.lstPesanKeluar, 0, 1)
        Me.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.tableLayoutPanel4.Name = "tableLayoutPanel4"
        Me.tableLayoutPanel4.RowCount = 2
        Me.tableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel4.Size = New System.Drawing.Size(753, 224)
        Me.tableLayoutPanel4.TabIndex = 0
        '
        'chkMessageSentSubscribe
        '
        Me.chkMessageSentSubscribe.AutoSize = True
        Me.chkMessageSentSubscribe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkMessageSentSubscribe.Enabled = False
        Me.chkMessageSentSubscribe.Location = New System.Drawing.Point(3, 3)
        Me.chkMessageSentSubscribe.Name = "chkMessageSentSubscribe"
        Me.chkMessageSentSubscribe.Size = New System.Drawing.Size(747, 20)
        Me.chkMessageSentSubscribe.TabIndex = 0
        Me.chkMessageSentSubscribe.Text = "Subscribe"
        Me.chkMessageSentSubscribe.UseVisualStyleBackColor = True
        '
        'lstPesanKeluar
        '
        Me.lstPesanKeluar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstPesanKeluar.FormattingEnabled = True
        Me.lstPesanKeluar.Location = New System.Drawing.Point(3, 29)
        Me.lstPesanKeluar.Name = "lstPesanKeluar"
        Me.lstPesanKeluar.Size = New System.Drawing.Size(747, 192)
        Me.lstPesanKeluar.TabIndex = 1
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.btnLokasiPenyimpananFileAtauGambar)
        Me.groupBox2.Controls.Add(Me.btnLokasiWAAutomateNodejs)
        Me.groupBox2.Controls.Add(Me.txtLokasiPenyimpananFileAtauGambar)
        Me.groupBox2.Controls.Add(Me.label5)
        Me.groupBox2.Controls.Add(Me.txtLokasiWhatsAppNETAPINodeJs)
        Me.groupBox2.Controls.Add(Me.label4)
        Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox2.Location = New System.Drawing.Point(3, 3)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(767, 69)
        Me.groupBox2.TabIndex = 3
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = " [ Konfigurasi ] "
        '
        'btnLokasiPenyimpananFileAtauGambar
        '
        Me.btnLokasiPenyimpananFileAtauGambar.Location = New System.Drawing.Point(724, 41)
        Me.btnLokasiPenyimpananFileAtauGambar.Name = "btnLokasiPenyimpananFileAtauGambar"
        Me.btnLokasiPenyimpananFileAtauGambar.Size = New System.Drawing.Size(34, 23)
        Me.btnLokasiPenyimpananFileAtauGambar.TabIndex = 2
        Me.btnLokasiPenyimpananFileAtauGambar.Text = "..."
        Me.btnLokasiPenyimpananFileAtauGambar.UseVisualStyleBackColor = True
        '
        'btnLokasiWAAutomateNodejs
        '
        Me.btnLokasiWAAutomateNodejs.Location = New System.Drawing.Point(724, 15)
        Me.btnLokasiWAAutomateNodejs.Name = "btnLokasiWAAutomateNodejs"
        Me.btnLokasiWAAutomateNodejs.Size = New System.Drawing.Size(34, 23)
        Me.btnLokasiWAAutomateNodejs.TabIndex = 2
        Me.btnLokasiWAAutomateNodejs.Text = "..."
        Me.btnLokasiWAAutomateNodejs.UseVisualStyleBackColor = True
        '
        'txtLokasiPenyimpananFileAtauGambar
        '
        Me.txtLokasiPenyimpananFileAtauGambar.Location = New System.Drawing.Point(269, 43)
        Me.txtLokasiPenyimpananFileAtauGambar.Name = "txtLokasiPenyimpananFileAtauGambar"
        Me.txtLokasiPenyimpananFileAtauGambar.ReadOnly = True
        Me.txtLokasiPenyimpananFileAtauGambar.Size = New System.Drawing.Size(449, 20)
        Me.txtLokasiPenyimpananFileAtauGambar.TabIndex = 1
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(9, 46)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(254, 13)
        Me.label5.TabIndex = 0
        Me.label5.Text = "Lokasi penyimpanan file atau gambar (pesan masuk)"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLokasiWhatsAppNETAPINodeJs
        '
        Me.txtLokasiWhatsAppNETAPINodeJs.Location = New System.Drawing.Point(269, 17)
        Me.txtLokasiWhatsAppNETAPINodeJs.Name = "txtLokasiWhatsAppNETAPINodeJs"
        Me.txtLokasiWhatsAppNETAPINodeJs.ReadOnly = True
        Me.txtLokasiWhatsAppNETAPINodeJs.Size = New System.Drawing.Size(449, 20)
        Me.txtLokasiWhatsAppNETAPINodeJs.TabIndex = 1
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(9, 20)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(175, 13)
        Me.label4.TabIndex = 0
        Me.label4.Text = "Lokasi WhatsApp NET API NodeJs"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 578)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.MaximizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Demo WhatsAppAPI untuk .NET Developer (VB.NET)"
        Me.tableLayoutPanel1.ResumeLayout(False)
        Me.flowLayoutPanel1.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.tableLayoutPanel2.ResumeLayout(False)
        Me.tableLayoutPanel2.PerformLayout()
        Me.flowLayoutPanel2.ResumeLayout(False)
        Me.flowLayoutPanel2.PerformLayout()
        Me.flowLayoutPanel4.ResumeLayout(False)
        Me.flowLayoutPanel4.PerformLayout()
        Me.flowLayoutPanel5.ResumeLayout(False)
        Me.flowLayoutPanel5.PerformLayout()
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tableLayoutPanel3.ResumeLayout(False)
        Me.flowLayoutPanel3.ResumeLayout(False)
        Me.flowLayoutPanel3.PerformLayout()
        Me.tabPage2.ResumeLayout(False)
        Me.tableLayoutPanel4.ResumeLayout(False)
        Me.tableLayoutPanel4.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents tableLayoutPanel1 As TableLayoutPanel
    Private WithEvents flowLayoutPanel1 As FlowLayoutPanel
    Private WithEvents btnStart As Button
    Private WithEvents btnStop As Button
    Private WithEvents btnGrabContacts As Button
    Private WithEvents btnUnreadMessages As Button
    Private WithEvents btnArchiveChat As Button
    Private WithEvents btnDeleteChat As Button
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents tableLayoutPanel2 As TableLayoutPanel
    Private WithEvents label1 As Label
    Private WithEvents txtKontak As TextBox
    Private WithEvents label3 As Label
    Private WithEvents txtPesan As TextBox
    Private WithEvents flowLayoutPanel2 As FlowLayoutPanel
    Private WithEvents btnKirim As Button
    Private WithEvents txtJumlahPesan As TextBox
    Private WithEvents label2 As Label
    Private WithEvents flowLayoutPanel4 As FlowLayoutPanel
    Private WithEvents chkKirimPesanDgGambar As CheckBox
    Private WithEvents txtFileGambar As TextBox
    Private WithEvents btnCariGambar As Button
    Private WithEvents flowLayoutPanel5 As FlowLayoutPanel
    Private WithEvents chkKirimFileAja As CheckBox
    Private WithEvents txtFileDokumen As TextBox
    Private WithEvents btnCariDokumen As Button
    Private WithEvents tabControl1 As TabControl
    Private WithEvents tabPage1 As TabPage
    Private WithEvents tableLayoutPanel3 As TableLayoutPanel
    Private WithEvents flowLayoutPanel3 As FlowLayoutPanel
    Private WithEvents chkSubscribe As CheckBox
    Private WithEvents chkAutoReplay As CheckBox
    Private WithEvents lstPesanMasuk As ListBox
    Private WithEvents tabPage2 As TabPage
    Private WithEvents tableLayoutPanel4 As TableLayoutPanel
    Private WithEvents chkMessageSentSubscribe As CheckBox
    Private WithEvents lstPesanKeluar As ListBox
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents btnLokasiPenyimpananFileAtauGambar As Button
    Private WithEvents btnLokasiWAAutomateNodejs As Button
    Private WithEvents txtLokasiPenyimpananFileAtauGambar As TextBox
    Private WithEvents label5 As Label
    Private WithEvents label4 As Label
    Friend WithEvents btnGrabGroupAndMembers As Button
    Private WithEvents txtLokasiWhatsAppNETAPINodeJs As TextBox
End Class
