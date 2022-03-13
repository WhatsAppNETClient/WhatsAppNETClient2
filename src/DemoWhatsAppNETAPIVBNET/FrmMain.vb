'*********************************************************************************************************
' Copyright (C) 2021 Kamarudin (http://wa-net.coding4ever.net/)
'
' Licensed under the Apache License, Version 2.0 (the "License"); you may not
' use this file except in compliance with the License. You may obtain a copy of
' the License at
'
' http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
' WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
' License for the specific language governing permissions and limitations under
' the License.
'
' The latest version of this file can be found at https://github.com/WhatsAppNETClient/WhatsAppNETClient2
'*********************************************************************************************************

Imports WhatsAppNETAPI
Imports ConceptCave.WaitCursor

Public Class FrmMain

    Private _wa As IWhatsAppNETAPI
    Private _selectedGroup As Group

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _wa = New WhatsAppNETAPI.WhatsAppNETAPI()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If (String.IsNullOrEmpty(txtLokasiWhatsAppNETAPINodeJs.Text)) Then
            MessageBox.Show("Maaf, lokasi folder 'WhatsApp NET API NodeJs'  belum di set", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtLokasiWhatsAppNETAPINodeJs.Focus()

            Return
        End If

        _wa.WaNetApiNodeJsPath = txtLokasiWhatsAppNETAPINodeJs.Text

        ' TODO: aktifkan kode ini agar bisa mengirimkan file dalam format video
        ' lokasi file chrome.exe menyesuaikan
        ' _wa.ChromePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"

        If (Not _wa.IsWaNetApiNodeJsPathExists) Then

            MessageBox.Show("Maaf, lokasi folder 'WhatsApp NET API NodeJs' tidak ditemukan !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtLokasiWhatsAppNETAPINodeJs.Focus()

            Return
        End If

        _wa.IsMultiDevice = chkMultiDevice.Checked
        _wa.Headless = chkHeadless.Checked

        Connect()
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Disconnect()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim msg = "Fungsi ini akan MENGHAPUS sesi koneksi ke Whatsapp Web." + Environment.NewLine +
                  "Jadi Anda harus melakukan scan ulang qrcode." + Environment.NewLine + Environment.NewLine +
                  "Apakah ingin dilanjutkan"

        If (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            Disconnect(True)
        End If
    End Sub

    Private Sub Connect()
        Me.UseWaitCursor = True

        _wa.ImageAndDocumentPath = txtLokasiPenyimpananFileAtauGambar.Text

        ' subscribe event
        AddHandler _wa.OnStartup, AddressOf OnStartupHandler
        AddHandler _wa.OnChangeState, AddressOf OnChangeStateHandler

        If Not _wa.IsMultiDevice Then
            AddHandler _wa.OnChangeBattery, AddressOf OnChangeBatteryHandler
        End If

        AddHandler _wa.OnReceiveMessages, AddressOf OnReceiveMessagesHandler
        AddHandler _wa.OnGroupJoin, AddressOf OnGroupJoinHandler
        AddHandler _wa.OnGroupLeave, AddressOf OnGroupLeaveHandler
        AddHandler _wa.OnClientConnected, AddressOf OnClientConnectedHandler
        AddHandler _wa.OnMonitoringLog, AddressOf OnMonitoringLogHandler
        _wa.Connect()

        Using New StCursor(Cursors.WaitCursor, New TimeSpan(0, 0, 0, 0))

            Using frm As New FrmStartUp

                ' subscribe event
                AddHandler _wa.OnStartup, AddressOf frm.OnStartupHandler
                AddHandler _wa.OnScanMe, AddressOf frm.OnScanMeHandler

                frm.UseWaitCursor = True
                frm.ShowDialog()

                ' unsubscribe event
                RemoveHandler _wa.OnStartup, AddressOf frm.OnStartupHandler
                RemoveHandler _wa.OnScanMe, AddressOf frm.OnScanMeHandler

            End Using

        End Using
    End Sub

    Private Sub Disconnect(Optional ByVal isLogout As Boolean = False)

        btnStart.Enabled = True
        btnStop.Enabled = False
        btnLogout.Enabled = False
        btnGrabContacts.Enabled = False
        btnGrabGroupAndMembers.Enabled = False
        btnVerifyContact.Enabled = False
        btnUnreadMessages.Enabled = False
        btnAllMessages.Enabled = False
        btnBatteryStatus.Enabled = False
        btnState.Enabled = False
        btnArchiveChat.Enabled = False
        btnDeleteChat.Enabled = False
        btnKirim.Enabled = False

        btnWANumber.Enabled = False
        btnSetStatus.Enabled = False

        chkGroup.Checked = False
        chkGroup.Enabled = False

        txtFileDokumen.Clear()
        txtFileGambar.Clear()

        chkSubscribe.Checked = False
        chkSubscribe.Enabled = False

        chkMessageSentSubscribe.Checked = False
        chkMessageSentSubscribe.Enabled = False

        chkAutoReplay.Checked = False
        chkAutoReplay.Enabled = False

        lstPesanMasuk.Items.Clear()

        Using New StCursor(Cursors.WaitCursor, New TimeSpan(0, 0, 0, 0))
            ' unsubscribe event
            RemoveHandler _wa.OnStartup, AddressOf OnStartupHandler
            RemoveHandler _wa.OnChangeState, AddressOf OnChangeStateHandler

            If Not _wa.IsMultiDevice Then
                RemoveHandler _wa.OnChangeBattery, AddressOf OnChangeBatteryHandler
            End If

            RemoveHandler _wa.OnScanMe, AddressOf OnScanMeHandler
            RemoveHandler _wa.OnReceiveMessage, AddressOf OnReceiveMessageHandler
            RemoveHandler _wa.OnReceiveMessages, AddressOf OnReceiveMessagesHandler
            RemoveHandler _wa.OnMessageAck, AddressOf OnMessageAckHandler
            RemoveHandler _wa.OnReceiveMessageStatus, AddressOf OnReceiveMessageStatusHandler
            RemoveHandler _wa.OnGroupJoin, AddressOf OnGroupJoinHandler
            RemoveHandler _wa.OnGroupLeave, AddressOf OnGroupLeaveHandler
            RemoveHandler _wa.OnClientConnected, AddressOf OnClientConnectedHandler
            RemoveHandler _wa.OnMonitoringLog, AddressOf OnMonitoringLogHandler
            If isLogout Then
                _wa.Logout()
            Else
                _wa.Disconnect()
            End If


        End Using
    End Sub

    Private Sub btnKirim_Click(sender As Object, e As EventArgs) Handles btnKirim.Click

        Dim msgArgs As MsgArgs
        Dim jumlahPesan = Int32.Parse(txtJumlahPesan.Text)

        If (jumlahPesan > 1) Then ' broadcast

            Dim list As New List(Of MsgArgs)
            Dim kontak = String.Empty

            If chkGroup.Checked Then
                If (_selectedGroup IsNot Nothing) Then
                    kontak = _selectedGroup.id
                Else
                    MessageBox.Show("Maaf, group belum dipilih", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                kontak = txtKontak.Text
            End If

            For index = 1 To jumlahPesan

                If chkKirimPesanDgGambar.Checked Then
                    msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Image, txtFileGambar.Text)
                ElseIf chkKirimGambarDariUrl.Checked Then
                    msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Url, txtUrl.Text)
                ElseIf chkKirimFileAja.Checked Then
                    msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.File, txtFileDokumen.Text)
                ElseIf chkKirimLokasi.Checked Then

                    Dim location = New Location()
                    location.latitude = txtLatitude.Text
                    location.longitude = txtLongitude.Text
                    location.description = txtDescription.Text

                    msgArgs = New MsgArgs(kontak, location)
                Else
                    msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text)
                End If

                list.Add(msgArgs)

            Next

            _wa.BroadcastMessage(list)

        Else

            Dim kontak = String.Empty

            If chkGroup.Checked Then
                If (_selectedGroup IsNot Nothing) Then
                    kontak = _selectedGroup.id
                Else
                    MessageBox.Show("Maaf, group belum dipilih", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                kontak = txtKontak.Text
            End If

            If chkKirimPesanDgGambar.Checked Then
                msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Image, txtFileGambar.Text)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Image, txtFileGambar.Text, mentions)

            ElseIf chkKirimGambarDariUrl.Checked Then
                msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Url, txtUrl.Text)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Url, txtUrl.Text, mentions)
            ElseIf chkKirimFileAja.Checked Then
                msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.File, txtFileDokumen.Text)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.File, txtFileDokumen.Text, mentions)
            ElseIf chkKirimLokasi.Checked Then

                Dim location = New Location()
                location.latitude = txtLatitude.Text
                location.longitude = txtLongitude.Text
                location.description = txtDescription.Text

                msgArgs = New MsgArgs(kontak, location)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, location, mentions)

            ElseIf chkKirimPesanList.Checked Then

                Dim list = New WhatsAppNETAPI.List()

                list.title = "Menu"
                list.listText = "Pilih Menu"

                list.content = "Assalamualaikum warahmatullahi wabarakatuh" + vbCrLf + vbCrLf +
                               "Selamat datang, silahkan pilih menu yang tersedia."

                Dim section As New Section With
                {
                    .title = "Daftar Menu",
                    .items = New ListItem() {
                        New ListItem With {.id = "zakat", .title = "Berzakat", .description = "Zakal maal, zakat fitrah, dll"},
                        New ListItem With {.id = "infak", .title = "Berinfak", .description = "Infak pendidikan, infak kesehatan, dll"},
                        New ListItem With {.id = "bantuan", .title = "Bantuan", .description = "Klo masih bingung"}
                    }
                }

                list.sections = New Section() {section}

                msgArgs = New MsgArgs(kontak, list)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, list, mentions)

            ElseIf chkKirimPesanButton.Checked Then
                Dim button = New WhatsAppNETAPI.Button()

                button.title = "Menu"
                button.content = "Assalamualaikum warahmatullahi wabarakatuh" + vbCrLf + vbCrLf +
                                 "Selamat datang, silahkan klik tombol yang tersedia."

                button.items = New ButtonItem() {
                    New ButtonItem With {.id = "btn_1", .title = "Tombol 1"},
                    New ButtonItem With {.id = "btn_2", .title = "Tombol 2"}
                }

                msgArgs = New MsgArgs(kontak, button)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, button, mentions)

            ElseIf chkKirimPesanButtonDgGambar.Checked Then
                Dim button = New WhatsAppNETAPI.Button()

                button.content = "*Assalamualaikum warahmatullahi wabarakatuh*" + vbCrLf + vbCrLf +
                                 "Selamat datang, silahkan klik tombol yang tersedia."

                button.items = New ButtonItem() {
                    New ButtonItem With {.id = "btn_1", .title = "Tombol 1"},
                    New ButtonItem With {.id = "btn_2", .title = "Tombol 2"}
                }

                msgArgs = New MsgArgs(kontak, button, txtFileLocalAtauUrl.Text)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, button, txtFileLocalAtauUrl.Text, mentions)
            Else
                msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text)

                ' contoh penggunaan mention user                
                ' Dim mentions As String() = {"081381712345", "08138174444", "tambahkan nomor yang lain"}
                ' msgArgs = New MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text, mentions)
            End If

            _wa.SendMessage(msgArgs)
        End If
    End Sub

    Private Sub chkSubscribe_CheckedChanged(sender As Object, e As EventArgs) Handles chkSubscribe.CheckedChanged
        If (chkSubscribe.Checked) Then
            AddHandler _wa.OnReceiveMessage, AddressOf OnReceiveMessageHandler ' subscribe event
        Else
            RemoveHandler _wa.OnReceiveMessage, AddressOf OnReceiveMessageHandler ' unsubscribe event
            lstPesanMasuk.Items.Clear()
        End If

        chkAutoReplay.Enabled = chkSubscribe.Checked
    End Sub

    Private Sub btnCariGambar_Click(sender As Object, e As EventArgs) Handles btnCariGambar.Click
        Dim fileName = ShowDialogOpen("Lokasi file gambar", True)

        If Not String.IsNullOrEmpty(fileName) Then txtFileGambar.Text = fileName
    End Sub

    Private Sub btnCariDokumen_Click(sender As Object, e As EventArgs) Handles btnCariDokumen.Click
        Dim fileName = ShowDialogOpen("Lokasi file dokumen")

        If Not String.IsNullOrEmpty(fileName) Then txtFileDokumen.Text = fileName
    End Sub

    Private Sub chkKirimPesanDgGambar_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimPesanDgGambar.CheckedChanged

        btnCariGambar.Enabled = chkKirimPesanDgGambar.Checked

        If chkKirimPesanDgGambar.Checked Then
            chkKirimFileAja.Checked = False
            chkKirimGambarDariUrl.Checked = False
            chkKirimPesanList.Checked = False
            chkKirimPesanButton.Checked = False
            chkKirimPesanButtonDgGambar.Checked = False

            chkKirimLokasi.Checked = False
            txtFileDokumen.Clear()

            txtLatitude.Enabled = False
            txtLongitude.Enabled = False
            txtDescription.Enabled = False
        Else
            txtFileGambar.Clear()
        End If
    End Sub

    Private Sub chkKirimGambarDariUrl_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimGambarDariUrl.CheckedChanged
        If chkKirimGambarDariUrl.Checked Then
            chkKirimPesanDgGambar.Checked = False
            chkKirimFileAja.Checked = False
            chkKirimLokasi.Checked = False
            chkKirimPesanList.Checked = False
            chkKirimPesanButton.Checked = False
            chkKirimPesanButtonDgGambar.Checked = False

            txtFileGambar.Clear()
            txtFileDokumen.Clear()

            txtLatitude.Enabled = False
            txtLongitude.Enabled = False
            txtDescription.Enabled = False
        End If
    End Sub

    Private Sub chkKirimFileAja_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimFileAja.CheckedChanged

        btnCariDokumen.Enabled = chkKirimFileAja.Checked

        If chkKirimFileAja.Checked Then
            chkKirimPesanDgGambar.Checked = False
            chkKirimGambarDariUrl.Checked = False
            chkKirimPesanList.Checked = False
            chkKirimPesanButton.Checked = False
            chkKirimPesanButtonDgGambar.Checked = False

            chkKirimLokasi.Checked = False
            txtFileGambar.Clear()

            txtLatitude.Enabled = False
            txtLongitude.Enabled = False
            txtDescription.Enabled = False
        Else
            txtFileDokumen.Clear()
        End If

    End Sub

    Private Sub chkKirimLokasi_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimLokasi.CheckedChanged
        If chkKirimLokasi.Checked Then

            If _wa.IsMultiDevice Then
                MessageBox.Show("Maaf fitur pesan dengan tipe location belum support untuk multi device", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)

                chkKirimLokasi.Checked = False
                Return
            End If

            chkKirimPesanDgGambar.Checked = False
            chkKirimGambarDariUrl.Checked = False
            chkKirimFileAja.Checked = False
            chkKirimPesanList.Checked = False
            chkKirimPesanButton.Checked = False
            chkKirimPesanButtonDgGambar.Checked = False

            txtFileGambar.Clear()
            txtFileDokumen.Clear()

            txtLatitude.Enabled = True
            txtLongitude.Enabled = True
            txtDescription.Enabled = True
        End If
    End Sub

    Private Sub btnGrabContacts_Click(sender As Object, e As EventArgs) Handles btnGrabContacts.Click
        Using frm As New FrmContactOrGroup("Contacts")

            AddHandler _wa.OnReceiveContacts, AddressOf frm.OnReceiveContactsHandler ' subscribe event
            _wa.GetContacts()

            frm.ShowDialog()
            RemoveHandler _wa.OnReceiveContacts, AddressOf frm.OnReceiveContactsHandler ' unsubscribe event

        End Using
    End Sub

    Private Sub chkMessageSentSubscribe_CheckedChanged(sender As Object, e As EventArgs) Handles chkMessageSentSubscribe.CheckedChanged
        If chkMessageSentSubscribe.Checked Then
            AddHandler _wa.OnReceiveMessageStatus, AddressOf OnReceiveMessageStatusHandler ' subscribe event
        Else
            RemoveHandler _wa.OnReceiveMessageStatus, AddressOf OnReceiveMessageStatusHandler ' unsubscribe event

            lstPesanKeluar.Items.Clear()
        End If
    End Sub

    Private Sub btnDeleteChat_Click(sender As Object, e As EventArgs) Handles btnDeleteChat.Click
        Dim msg = "Fungsi ini akan MENGHAPUS semua pesan." + Environment.NewLine +
                  "Apakah ingin dilanjutkan"
        If MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            _wa.DeleteChat() ' hapus semua chat

            ' contoh jika ingin menghapus berdasarkan phoneNumber
            ' Dim phoneNumber As String = "0813123456789"
            ' _wa.DeleteChat(phoneNumber)
        End If
    End Sub

    Private Sub btnArchiveChat_Click(sender As Object, e As EventArgs) Handles btnArchiveChat.Click
        Dim msg = "Fungsi ini akan MENGARSIPKAN semua pesan." + Environment.NewLine +
                  "Apakah ingin dilanjutkan"
        If MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            _wa.ArchiveChat() ' arsip semua chat

            ' contoh jika ingin mengarsipkan berdasarkan phoneNumber
            ' Dim phoneNumber As String = "0813123456789"
            ' _wa.ArchiveChat(phoneNumber)
        End If
    End Sub

    Private Sub btnUnreadMessages_Click(sender As Object, e As EventArgs) Handles btnUnreadMessages.Click
        _wa.GetUnreadMessage()
    End Sub

    Private Sub btnLokasiWAAutomateNodejs_Click(sender As Object, e As EventArgs) Handles btnLokasiWAAutomateNodejs.Click
        Dim folderName = ShowDialogOpenFolder()

        If (Not String.IsNullOrEmpty(folderName)) Then txtLokasiWhatsAppNETAPINodeJs.Text = folderName
    End Sub

    Private Sub btnLokasiPenyimpananFileAtauGambar_Click(sender As Object, e As EventArgs) Handles btnLokasiPenyimpananFileAtauGambar.Click
        Dim folderName = ShowDialogOpenFolder()

        If (Not String.IsNullOrEmpty(folderName)) Then txtLokasiPenyimpananFileAtauGambar.Text = folderName
    End Sub

    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Disconnect()
    End Sub

    Private Sub btnGrabGroupAndMembers_Click(sender As Object, e As EventArgs) Handles btnGrabGroupAndMembers.Click
        Using frm As New FrmContactOrGroup("Groups and Members")

            AddHandler _wa.OnReceiveGroups, AddressOf frm.OnReceiveGroupsHandler ' subscribe event
            _wa.GetGroups()

            frm.ShowDialog()
            RemoveHandler _wa.OnReceiveGroups, AddressOf frm.OnReceiveGroupsHandler ' unsubscribe event

        End Using
    End Sub

    Private Sub btnPilihGroup_Click(sender As Object, e As EventArgs) Handles btnPilihGroup.Click
        Using frm As New FrmPilihGroup("Pilih Group")

            AddHandler _wa.OnReceiveGroups, AddressOf frm.OnReceiveGroupsHandler ' subscribe event
            _wa.GetGroups(False)

            If frm.ShowDialog() = DialogResult.OK Then
                _selectedGroup = frm.Group

                If (_selectedGroup IsNot Nothing) Then txtKontak.Text = _selectedGroup.name
            End If

            RemoveHandler _wa.OnReceiveGroups, AddressOf frm.OnReceiveGroupsHandler ' unsubscribe event

        End Using
    End Sub

    Private Sub chkGroup_CheckedChanged(sender As Object, e As EventArgs) Handles chkGroup.CheckedChanged
        _selectedGroup = Nothing
        txtKontak.Clear()

        btnPilihGroup.Enabled = chkGroup.Checked
        txtKontak.Enabled = Not chkGroup.Checked
    End Sub

#Region "Event handler"

    Private Sub OnStartupHandler(ByVal message As String, ByVal sessionId As String)

        ' koneksi ke WA berhasil
        If message.IndexOf("Ready") >= 0 Then

            btnStart.Invoke(Sub() btnStart.Enabled = False)
            btnStop.Invoke(Sub() btnStop.Enabled = True)
            btnLogout.Invoke(Sub() btnLogout.Enabled = True)
            btnGrabContacts.Invoke(Sub() btnGrabContacts.Enabled = True)
            btnGrabGroupAndMembers.Invoke(Sub() btnGrabGroupAndMembers.Enabled = True)
            btnVerifyContact.Invoke(Sub() btnVerifyContact.Enabled = True)
            btnUnreadMessages.Invoke(Sub() btnUnreadMessages.Enabled = True)
            btnAllMessages.Invoke(Sub() btnAllMessages.Enabled = True)
            btnBatteryStatus.Invoke(Sub() btnBatteryStatus.Enabled = True)
            btnState.Invoke(Sub() btnState.Enabled = True)
            btnArchiveChat.Invoke(Sub() btnArchiveChat.Enabled = True)
            btnDeleteChat.Invoke(Sub() btnDeleteChat.Enabled = True)

            btnWANumber.Invoke(Sub() btnWANumber.Enabled = True)
            btnSetStatus.Invoke(Sub() btnSetStatus.Enabled = True)

            chkGroup.Invoke(Sub() chkGroup.Enabled = True)
            btnKirim.Invoke(Sub() btnKirim.Enabled = True)
            chkSubscribe.Invoke(Sub() chkSubscribe.Enabled = True)
            chkMessageSentSubscribe.Invoke(Sub() chkMessageSentSubscribe.Enabled = True)

            Me.UseWaitCursor = False

        End If

        ' koneksi ke WA GAGAL, bisa dicoba lagi
        If message.IndexOf("Failure") >= 0 OrElse message.IndexOf("Timeout") >= 0 _
            OrElse message.IndexOf("ERR_NAME") >= 0 _
            OrElse message.IndexOf("ERR_CONNECTION") >= 0 Then

            ' unsubscribe event
            RemoveHandler _wa.OnStartup, AddressOf OnStartupHandler
            RemoveHandler _wa.OnScanMe, AddressOf OnScanMeHandler
            RemoveHandler _wa.OnReceiveMessage, AddressOf OnReceiveMessageHandler
            RemoveHandler _wa.OnReceiveMessages, AddressOf OnReceiveMessagesHandler
            RemoveHandler _wa.OnReceiveMessageStatus, AddressOf OnReceiveMessageStatusHandler
            RemoveHandler _wa.OnClientConnected, AddressOf OnClientConnectedHandler
            RemoveHandler _wa.OnMonitoringLog, AddressOf OnMonitoringLogHandler

            _wa.Disconnect()

            Me.UseWaitCursor = False

            Dim msg = message + Environment.NewLine + Environment.NewLine +
                      "Koneksi ke WA gagal, silahkan cek koneksi internet Anda"

            MessageBox.Show(msg, "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub OnChangeStateHandler(ByVal state As WhatsAppNETAPI.WAState, ByVal sessionId As String)
        lblState.Invoke(Sub() lblState.Text = String.Format("State: {0}", state.ToString()))
    End Sub

    Private Sub OnChangeBatteryHandler(ByVal status As BatteryStatus, ByVal sessionId As String)
        lblBatteryStatus.Invoke(Sub() lblBatteryStatus.Text = String.Format("Battery: {0}% - Charging? {1}", status.battery, status.plugged))
    End Sub

    Private Sub OnScanMeHandler(ByVal qrcodePath As String, ByVal sessionId As String)

    End Sub

    Private Sub OnReceiveMessageHandler(ByVal message As WhatsAppNETAPI.Message, ByVal sessionId As String)

        Dim msg = message.content

        Dim pengirim = String.Empty
        Dim pushName = String.Empty
        Dim group = String.Empty

        If message.id = "status@broadcast" Then ' status@broadcast -> dummy message, penanda load data selesai
            Return
        End If

        Dim isGroup = message.group IsNot Nothing

        If isGroup Then
            group = IIf(String.IsNullOrEmpty(message.group.name), message.from, message.group.name)

            Dim sender = message.group.sender
            pengirim = IIf(String.IsNullOrEmpty(sender.name), message.from, sender.name)
            pushName = sender.pushname
        Else
            pengirim = IIf(String.IsNullOrEmpty(message.sender.name), message.from, message.sender.name)
            pushName = message.sender.pushname
        End If

        Dim fileName = message.filename

        Dim data = String.Empty

        If isGroup Then ' pesan dari group
            If String.IsNullOrEmpty(fileName) Then
                data = String.Format("[{0}] Group: {1}, Pesan teks: {2}, Pengirim: {3} [{4}]",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim, pushName)
            Else
                data = String.Format("[{0}] Group: {1}, Pesan gambar/dokumen: {2}, Pengirim: {3} [{4}], nama file: {5}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim, pushName, fileName)
            End If
        Else
            If message.type = "call_log" Then ' handle telepon masuk
                data = String.Format("[{0}] Telpon masuk dari : {1} [{2}]",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, pushName)
            Else
                If String.IsNullOrEmpty(fileName) Then
                    data = String.Format("[{0}] Pengirim: {1} [{2}], Pesan teks: {3}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, pushName, msg)
                Else
                    data = String.Format("[{0}] Pengirim: {1} [{2}], Pesan gambar/dokumen: {3}, nama file: {4}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, pushName, msg, fileName)
                End If

            End If
        End If

            ' khusus pesan masuk dengan tipe button dan list
            ' tambahkan pengecekan kode berikut untuk mendapatkan id button/list yang dipilih
            If message.type = MessageType.ButtonResponse Then
            System.Diagnostics.Debug.Print("Id button yang dipilih: {0}", message.selectedButtonId)
        ElseIf message.type = MessageType.ListResponse Then
            System.Diagnostics.Debug.Print("Id list yang dipilih: {0}", message.selectedRowId)
        End If

        ' update UI dari thread yang berbeda
        lstPesanMasuk.Invoke(
            Sub()
                lstPesanMasuk.Items.Add(data)

                If message.type = MessageType.Location Then

                    Dim location = message.location

                    Dim dataLocation = String.Format("--> latitude: {0}, longitude: {1}, description: {2}",
                        location.latitude, location.longitude, location.description)

                    lstPesanMasuk.Items.Add(dataLocation)

                ElseIf message.type = MessageType.VCard OrElse message.type = MessageType.MultiVCard Then
                    Dim vcards = message.vcards
                    Dim vcardFilenames = message.vcardFilenames

                    Dim index = 0
                    For Each vcard As VCard In vcards

                        Dim dataVCard = String.Format("--> N: {0}, FN: {1}, WA Id: {2}, fileName: {3}",
                            vcard.n, vcard.fn, vcard.waId, vcardFilenames(index))

                        lstPesanMasuk.Items.Add(dataVCard)

                        index = index + 1
                    Next
                End If

                lstPesanMasuk.SelectedIndex = lstPesanMasuk.Items.Count - 1
            End Sub
        )

        If chkAutoReplay.Checked Then

            If chkKirimLokasi.Checked Then
                Dim location = New Location()
                location.latitude = txtLatitude.Text
                location.longitude = txtLongitude.Text
                location.description = txtDescription.Text

                _wa.ReplyMessage(New ReplyMsgArgs(message.from, location, message.id))
            Else
                Dim msgReplay = String.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                    pengirim, msg)

                _wa.ReplyMessage(New ReplyMsgArgs(message.from, msgReplay, message.id))
            End If

        End If
    End Sub

    Private Sub OnMessageAckHandler(ByVal message As WhatsAppNETAPI.Message, ByVal sessionId As String)
        Dim msg = String.Format("Status pengiriman pesan ke {0}, messageId = {1}, status = {2}",
            message.to, message.id, message.ack.ToString())

        ' update UI dari thread yang berbeda
        lstStatusPesanKeluar.Invoke(
            Sub()
                lstStatusPesanKeluar.Items.Add(msg)
                lstStatusPesanKeluar.SelectedIndex = lstStatusPesanKeluar.Items.Count - 1
            End Sub
        )
    End Sub

    Private Sub OnReceiveMessagesHandler(ByVal messages As IList(Of Message), ByVal sessionId As String)

        For Each message As Message In messages

            If message.id = "status@broadcast" Then ' status@broadcast -> dummy message, penanda load data selesai
                Continue For
            End If

            Dim msg = message.content

            Dim pengirim = String.Empty
            Dim group = String.Empty

            Dim isGroup = message.group IsNot Nothing

            If isGroup Then ' pesan dari group
                group = IIf(String.IsNullOrEmpty(message.group.name), message.from, message.group.name)

                Dim sender = message.group.sender
                pengirim = IIf(String.IsNullOrEmpty(sender.name), message.from, sender.name)
            Else
                pengirim = IIf(String.IsNullOrEmpty(message.sender.name), message.from, message.sender.name)
            End If

            Dim data = String.Empty

            If isGroup Then
                data = String.Format("[{0}] Group: {1}, Pesan teks: {2}, Pengirim: {3}",
                    message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim)
            Else
                data = String.Format("[{0}] Pengirim: {1}, Isi pesan: {2}",
                    message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg)
            End If

            ' update UI dari thread yang berbeda
            lstPesanMasuk.Invoke(
                Sub()
                    lstPesanMasuk.Items.Add(data)
                    lstPesanMasuk.SelectedIndex = lstPesanMasuk.Items.Count - 1
                End Sub
            )

            If chkAutoReplay.Checked Then

                Dim senderName = IIf(String.IsNullOrEmpty(message.sender.name), message.from, message.sender.name)

                Dim msgReplay = String.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                    senderName, msg)

                _wa.ReplyMessage(New ReplyMsgArgs(message.from, msgReplay, message.id))
            End If
        Next

    End Sub

    Private Sub OnReceiveMessageStatusHandler(ByVal msgStatus As WhatsAppNETAPI.MessageStatus, ByVal sessionId As String)

        Dim status = IIf(msgStatus.status = "true", "BERHASIL", "GAGAL")

        Dim msg = String.Format("Status pengiriman pesan ke {0}, {1}",
            msgStatus.send_to, status)

        ' update UI dari thread yang berbeda
        lstPesanMasuk.Invoke(
            Sub()
                lstPesanKeluar.Items.Add(msg)
                lstPesanKeluar.SelectedIndex = lstPesanKeluar.Items.Count - 1
            End Sub
        )

    End Sub

    Private Sub OnGroupJoinHandler(ByVal notification As GroupNotification, ByVal sessionId As String)

        Dim recipients As String = String.Join(", ", notification.recipients _
            .Select(Function(f) f.name)) _
            .ToArray()

        Dim msgReplay = String.Format("Selamat bergabung Bpk/Ibu *{0}*, di group *{1}*.",
            recipients, notification.name)

        Dim msgArgs = New MsgArgs(notification.id, msgReplay, MsgArgsType.Text)
        _wa.SendMessage(msgArgs)

    End Sub

    Private Sub OnGroupLeaveHandler(ByVal notification As GroupNotification, ByVal sessionId As String)

        Dim recipients As String = String.Join(", ", notification.recipients _
            .Select(Function(f) f.name)) _
            .ToArray()

        Dim msgReplay = String.Format("Selamat berpisah Bpk/Ibu *{0}*, semoga bisa join lagi di group *{1}*.",
            recipients, notification.name)

        Dim msgArgs = New MsgArgs(notification.id, msgReplay, MsgArgsType.Text)
        _wa.SendMessage(msgArgs)

    End Sub

    Private Sub OnClientConnectedHandler(ByVal sessionId As String)
        System.Diagnostics.Debug.Print("ClientConnected on {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
    End Sub

    ''' <summary>
    ''' Menampilkan log INFO/ERROR yang dikirim dari node js
    ''' </summary>
    ''' <param name="level">Berisi INFO atau ERROR</param>
    ''' <param name="message"></param>
    ''' <param name="sessionId"></param>
    Private Sub OnMonitoringLogHandler(ByVal level As String, ByVal message As String, ByVal sessionId As String)
        System.Diagnostics.Debug.Print("level: {0}, message: {1}", level, message)
    End Sub

    Private Sub chkKirimPesanList_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimPesanList.CheckedChanged
        If chkKirimPesanList.Checked Then
            chkKirimPesanDgGambar.Checked = False
            chkKirimGambarDariUrl.Checked = False
            chkKirimFileAja.Checked = False
            chkKirimPesanButton.Checked = False
            chkKirimPesanButtonDgGambar.Checked = False
            chkKirimLokasi.Checked = False

            txtFileGambar.Clear()
            txtFileDokumen.Clear()

            txtLatitude.Enabled = True
            txtLongitude.Enabled = True
            txtDescription.Enabled = True
        End If
    End Sub

    Private Sub chkKirimPesanButton_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimPesanButton.CheckedChanged
        If chkKirimPesanButton.Checked Then

            If _wa.IsMultiDevice Then
                MessageBox.Show("Maaf fitur pesan dengan tipe button belum support untuk multi device", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)

                chkKirimPesanButton.Checked = False
                Return
            End If

            chkKirimPesanDgGambar.Checked = False
            chkKirimGambarDariUrl.Checked = False
            chkKirimFileAja.Checked = False
            chkKirimPesanList.Checked = False
            chkKirimLokasi.Checked = False
            chkKirimPesanButtonDgGambar.Checked = False

            txtFileGambar.Clear()
            txtFileDokumen.Clear()

            txtLatitude.Enabled = True
            txtLongitude.Enabled = True
            txtDescription.Enabled = True
        End If
    End Sub

    Private Sub btnWANumber_Click(sender As Object, e As EventArgs) Handles btnWANumber.Click
        Dim msg As String = "Nomor WA: " + _wa.GetCurrentNumber + Environment.NewLine +
            "MultiDevice: " + _wa.IsMultiDevice.ToString()

        MessageBox.Show(msg, "Infomasi",
                MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnSetStatus_Click(sender As Object, e As EventArgs) Handles btnSetStatus.Click

        If _wa.IsMultiDevice Then
            MessageBox.Show("Maaf fitur set status belum support untuk multi device", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using frm As New FrmSetStatus("Status", _wa)

            frm.ShowDialog()

        End Using
    End Sub

    Private Sub chkMessageSentStatusSubscribe_CheckedChanged(sender As Object, e As EventArgs) Handles chkMessageSentStatusSubscribe.CheckedChanged
        If chkMessageSentStatusSubscribe.Checked Then
            AddHandler _wa.OnMessageAck, AddressOf OnMessageAckHandler ' subscribe event
        Else
            RemoveHandler _wa.OnMessageAck, AddressOf OnMessageAckHandler ' unsubscribe event

            lstStatusPesanKeluar.Items.Clear()
        End If
    End Sub

    Private Sub btnAllMessages_Click(sender As Object, e As EventArgs) Handles btnAllMessages.Click
        Dim phoneNumber As String = "081381769915"
        _wa.GetAllMessage(phoneNumber, 3) 'menampilkan 3 pesan terakhir
    End Sub

    Private Sub btnBatteryStatus_Click(sender As Object, e As EventArgs) Handles btnBatteryStatus.Click
        If _wa.IsMultiDevice Then
            MessageBox.Show("Maaf fitur cek battery status sudah tidak support untuk multi device", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        _wa.GetBatteryStatus()
    End Sub

    Private Sub btnState_Click(sender As Object, e As EventArgs) Handles btnState.Click
        _wa.GetCurrentState()
    End Sub

    Private Sub btnVerifyContact_Click(sender As Object, e As EventArgs) Handles btnVerifyContact.Click

        ' daftar kontak yang mau di verifikasi
        ' bisa diambil dari database atau hasil generatean
        Dim contacts As List(Of String) = New List(Of String)({"081381712345", "089652948305",
                "085211112345", "081381712345", "085291123456", "081336123456"})

        Using frm As New FrmContactOrGroup("Contacts")

            AddHandler _wa.OnReceiveContacts, AddressOf frm.OnReceiveContactsHandler ' subscribe event
            _wa.VerifyWANumber(contacts)

            frm.ShowDialog()
            RemoveHandler _wa.OnReceiveContacts, AddressOf frm.OnReceiveContactsHandler ' unsubscribe event

        End Using
    End Sub

    Private Sub chkKirimPesanButtonDgGambar_CheckedChanged(sender As Object, e As EventArgs) Handles chkKirimPesanButtonDgGambar.CheckedChanged
        If chkKirimPesanButtonDgGambar.Checked Then

            If _wa.IsMultiDevice Then
                MessageBox.Show("Maaf fitur pesan dengan tipe button belum support untuk multi device", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)

                chkKirimPesanButtonDgGambar.Checked = False
                Return
            End If

            chkKirimPesanDgGambar.Checked = False
            chkKirimGambarDariUrl.Checked = False
            chkKirimFileAja.Checked = False
            chkKirimPesanList.Checked = False
            chkKirimLokasi.Checked = False
            chkKirimPesanButton.Checked = False

            txtFileGambar.Clear()
            txtFileDokumen.Clear()

            txtLatitude.Enabled = True
            txtLongitude.Enabled = True
            txtDescription.Enabled = True
        End If
    End Sub

#End Region

End Class