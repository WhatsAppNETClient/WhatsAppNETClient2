/**
 * Copyright (C) 2021 Kamarudin (http://wa-net.coding4ever.net/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * The latest version of this file can be found at https://github.com/WhatsAppNETClient/WhatsAppNETClient2
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using WhatsAppNETAPI;
using ConceptCave.WaitCursor;
using System.IO;

namespace DemoWhatsAppNETAPICSharp
{
    public partial class FrmMain : Form
    {
        private IWhatsAppNETAPI _wa;
        private Group _selectedGroup;

        public FrmMain()
        {
            InitializeComponent();

            _wa = new WhatsAppNETAPI.WhatsAppNETAPI();            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLokasiWhatsAppNETAPINodeJs.Text))
            {
                MessageBox.Show("Maaf, lokasi folder 'WhatsApp NET API NodeJs'  belum di set", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtLokasiWhatsAppNETAPINodeJs.Focus();
                return;
            }

            _wa.WaNetApiNodeJsPath = txtLokasiWhatsAppNETAPINodeJs.Text;

            // TODO: aktifkan kode ini agar bisa mengirimkan file dalam format video
            // _wa.ChromePath = "C:/Program Files (x86)/Google/Chrome/Application/chrome.exe";

            if (!_wa.IsWaNetApiNodeJsPathExists)
            {
                MessageBox.Show("Maaf, lokasi folder 'WhatsApp NET API NodeJs' tidak ditemukan !!!", "Peringatan", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtLokasiWhatsAppNETAPINodeJs.Focus();
                return;
            }

            Connect();            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var msg = "Fungsi ini akan MENGHAPUS sesi koneksi ke Whatsapp Web.\n" +
                      "Jadi Anda harus melakukan scan ulang qrcode.\n\n" +
                      "Apakah ingin dilanjutkan";

            if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Disconnect(true);
            }
        }

        private void Connect()
        {
            this.UseWaitCursor = true;

            _wa.ImageAndDocumentPath = txtLokasiPenyimpananFileAtauGambar.Text;

            // subscribe event
            _wa.OnStartup += OnStartupHandler;
            _wa.OnChangeState += OnChangeStateHandler;
            _wa.OnReceiveMessages += OnReceiveMessagesHandler;
            _wa.OnClientConnected += OnClientConnectedHandler;
            
            _wa.Connect();

            using (var frm = new FrmStartUp())
            {
                // subscribe event
                _wa.OnStartup += frm.OnStartupHandler;
                _wa.OnScanMe += frm.OnScanMeHandler;

                frm.UseWaitCursor = true;
                frm.ShowDialog();

                // unsubscribe event
                _wa.OnStartup -= frm.OnStartupHandler;
                _wa.OnScanMe -= frm.OnScanMeHandler;
            }
        }        

        private void Disconnect(bool isLogout = false)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnLogout.Enabled = false;
            btnGrabContacts.Enabled = false;
            btnGrabGroupAndMembers.Enabled = false;
            btnUnreadMessages.Enabled = false;
            btnArchiveChat.Enabled = false;
            btnDeleteChat.Enabled = false;
            btnKirim.Enabled = false;

            chkGroup.Checked = false;
            chkGroup.Enabled = false;

            txtFileDokumen.Clear();
            txtFileGambar.Clear();

            chkSubscribe.Checked = false;
            chkSubscribe.Enabled = false;

            chkMessageSentSubscribe.Checked = false;
            chkMessageSentSubscribe.Enabled = false;

            chkAutoReplay.Checked = false;
            chkAutoReplay.Enabled = false;

            lstPesanMasuk.Items.Clear();

            using (new StCursor(Cursors.WaitCursor, new TimeSpan(0, 0, 0, 0)))
            {
                // unsubscribe event
                _wa.OnStartup -= OnStartupHandler;
                _wa.OnChangeState -= OnChangeStateHandler;
                _wa.OnScanMe -= OnScanMeHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnReceiveMessages -= OnReceiveMessagesHandler;
                _wa.OnReceiveMessageStatus -= OnReceiveMessageStatusHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;

                if (isLogout)
                    _wa.Logout();
                else
                    _wa.Disconnect();
            }
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            var jumlahPesan = int.Parse(txtJumlahPesan.Text);

            if (jumlahPesan > 1) // broadcast
            {
                var list = new List<MsgArgs>();

                var kontak = string.Empty;

                if (chkGroup.Checked)
                {
                    if (_selectedGroup != null)
                        kontak = _selectedGroup.id;
                    else
                        MessageBox.Show("Maaf, group belum dipilih", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    kontak = txtKontak.Text;

                for (int i = 0; i < jumlahPesan; i++)
                {
                    MsgArgs msgArgs = null;

                    if (chkKirimPesanDgGambar.Checked)
                        msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Image, txtFileGambar.Text);
                    else if (chkKirimGambarDariUrl.Checked)
                        msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Url, txtUrl.Text);
                    else if (chkKirimFileAja.Checked)
                        msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.File, txtFileDokumen.Text);
                    else if (chkKirimLokasi.Checked)
                    {
                        var location = new Location
                        {
                            latitude = txtLatitude.Text,
                            longitude = txtLongitude.Text,
                            description = txtDescription.Text
                        };

                        msgArgs = new MsgArgs(kontak, location);
                    }
                    else
                        msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text);

                    list.Add(msgArgs);
                }

                _wa.BroadcastMessage(list);
            }
            else
            {
                MsgArgs msgArgs = null;

                var kontak = string.Empty;

                if (chkGroup.Checked)
                {
                    if (_selectedGroup != null)
                        kontak = _selectedGroup.id;
                    else
                        MessageBox.Show("Maaf, group belum dipilih", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    kontak = txtKontak.Text;

                if (chkKirimPesanDgGambar.Checked)
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Image, txtFileGambar.Text);
                else if (chkKirimGambarDariUrl.Checked)
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Url, txtUrl.Text);
                else if (chkKirimFileAja.Checked)
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.File, txtFileDokumen.Text);
                else if (chkKirimLokasi.Checked)
                {
                    var location = new Location
                    {
                        latitude = txtLatitude.Text,
                        longitude = txtLongitude.Text,
                        description = txtDescription.Text
                    };

                    msgArgs = new MsgArgs(kontak, location);
                }
                else if (chkKirimPesanList.Checked)
                {
                    var list = new WhatsAppNETAPI.List();

                    list.title = "Menu";
                    list.listText = "Pilih Menu";
                    list.content = @"Assalamualaikum warahmatullahi wabarakatuh
        
Selamat datang, silahkan pilih menu yang tersedia.";

                    var section = new Section
                    {
                        title = "Daftar Menu",
                        items = new ListItem[]
                        {
                            new ListItem { title = "Berzakat", description = "Zakal maal, zakat fitrah, dll" },
                            new ListItem { title = "Berinfak", description = "Infak pendidikan, infak kesehatan, dll" },
                            new ListItem { title = "Bantuan", description = "Klo masih bingung" }
                        }
                    };

                    list.sections = new Section[] { section };

                    msgArgs = new MsgArgs(kontak, list);
                }
                else if (chkKirimPesanButton.Checked)
                {
                    var button = new WhatsAppNETAPI.Button();
                    button.title = "Menu";
                    button.content = @"Assalamualaikum warahmatullahi wabarakatuh
        
Selamat datang, silahkan klik tombol yang tersedia.";

                    button.items = new ButtonItem[]
                    {
                        new ButtonItem { title = "Tombol 1" },
                        new ButtonItem { title = "Tombol 2" }
                    };

                    msgArgs = new MsgArgs(kontak, button);
                }
                else
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text);
                
                _wa.SendMessage(msgArgs);
            }
        }

        private void chkSubscribe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubscribe.Checked)
                _wa.OnReceiveMessage += OnReceiveMessageHandler; // subscribe event
            else
            {
                _wa.OnReceiveMessage -= OnReceiveMessageHandler; // unsubscribe event
                lstPesanMasuk.Items.Clear();
            }

            chkAutoReplay.Enabled = chkSubscribe.Checked;
        }

        private void btnCariGambar_Click(object sender, EventArgs e)
        {
            var fileName = ShowDialogOpen("Lokasi file gambar", true);
            if (!string.IsNullOrEmpty(fileName)) txtFileGambar.Text = fileName;
        }

        private void btnCariDokumen_Click(object sender, EventArgs e)
        {
            var fileName = ShowDialogOpen("Lokasi file dokumen");
            if (!string.IsNullOrEmpty(fileName)) txtFileDokumen.Text = fileName;
        }

        private string ShowDialogOpen(string title, bool fileImageOnly = false)
        {
            var fileName = string.Empty;

            using (var dlgOpen = new OpenFileDialog())
            {
                dlgOpen.Filter = fileImageOnly ? "File gambar (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png"
                                               : "File dokumen (*.*)|*.*";
                dlgOpen.Title = title;

                var result = dlgOpen.ShowDialog();
                if (result == DialogResult.OK) fileName = dlgOpen.FileName;
            }

            return fileName;
        }

        private string ShowDialogOpenFolder()
        {
            var folderName = string.Empty;

            using (var dlgOpen = new FolderBrowserDialog())
            {
                var result = dlgOpen.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlgOpen.SelectedPath))
                {
                    folderName = dlgOpen.SelectedPath;
                }
            }

            return folderName;
        }

        private void chkKirimPesanDgGambar_CheckedChanged(object sender, EventArgs e)
        {
            btnCariGambar.Enabled = chkKirimPesanDgGambar.Checked;
            if (chkKirimPesanDgGambar.Checked)
            {
                chkKirimFileAja.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimPesanButton.Checked = false;

                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
            else
                txtFileGambar.Clear();
        }

        private void chkKirimGambarDariUrl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimGambarDariUrl.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimPesanButton.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }            
        }

        private void chkKirimFileAja_CheckedChanged(object sender, EventArgs e)
        {
            btnCariDokumen.Enabled = chkKirimFileAja.Checked;

            if (chkKirimFileAja.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimPesanButton.Checked = false;

                txtFileGambar.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
            else
                txtFileDokumen.Clear();
        }

        private void chkKirimLokasi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimLokasi.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimPesanButton.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = true;
                txtLongitude.Enabled = true;
                txtDescription.Enabled = true;
            }
        }

        private void btnGrabContacts_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmContactOrGroup("Contacts"))
            {                
                _wa.OnReceiveContacts += frm.OnReceiveContactsHandler; // subscribe event
                _wa.GetContacts();

                frm.ShowDialog();
                _wa.OnReceiveContacts -= frm.OnReceiveContactsHandler; // unsubscribe event
            }
        }

        private void btnGrabGroupAndMembers_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmContactOrGroup("Groups and Members"))
            {
                _wa.OnReceiveGroups += frm.OnReceiveGroupsHandler; // subscribe event
                _wa.GetGroups();

                frm.ShowDialog();
                _wa.OnReceiveGroups -= frm.OnReceiveGroupsHandler; // unsubscribe event
            }
        }

        private void chkMessageSentSubscribe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMessageSentSubscribe.Checked)
            {
                _wa.OnReceiveMessageStatus += OnReceiveMessageStatusHandler; // subscribe event
            }
            else
            {
                _wa.OnReceiveMessageStatus -= OnReceiveMessageStatusHandler; // unsubscribe event
                lstPesanKeluar.Items.Clear();
            }
        }

        private void btnDeleteChat_Click(object sender, EventArgs e)
        {
            var msg = "Fungsi ini akan MENGHAPUS semua pesan.\n" +
                      "Apakah ingin dilanjutkan";

            if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _wa.DeleteChat();
            }            
        }

        private void btnArchiveChat_Click(object sender, EventArgs e)
        {
            var msg = "Fungsi ini akan MENGARSIPKAN semua pesan.\n" +
                      "Apakah ingin dilanjutkan";

            if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _wa.ArchiveChat();
            }                
        }

        private void btnUnreadMessages_Click(object sender, EventArgs e)
        {
            _wa.GetUnreadMessage();
        }

        # region event handler

        private void OnStartupHandler(string message, string sessionId)
        {
            // koneksi ke WA berhasil
            if (message.IndexOf("Ready") >= 0)
            {
                btnStart.Invoke(new MethodInvoker(() => btnStart.Enabled = false));

                btnStop.Invoke(new MethodInvoker(() => btnStop.Enabled = true));
                btnLogout.Invoke(new MethodInvoker(() => btnLogout.Enabled = true));

                btnGrabContacts.Invoke(new MethodInvoker(() => btnGrabContacts.Enabled = true));
                btnGrabGroupAndMembers.Invoke(new MethodInvoker(() => btnGrabGroupAndMembers.Enabled = true));

                btnUnreadMessages.Invoke(new MethodInvoker(() => btnUnreadMessages.Enabled = true));
                btnArchiveChat.Invoke(new MethodInvoker(() => btnArchiveChat.Enabled = true));
                btnDeleteChat.Invoke(new MethodInvoker(() => btnDeleteChat.Enabled = true));

                chkGroup.Invoke(new MethodInvoker(() => chkGroup.Enabled = true));
                btnKirim.Invoke(new MethodInvoker(() => btnKirim.Enabled = true));
                chkSubscribe.Invoke(new MethodInvoker(() => chkSubscribe.Enabled = true));
                chkMessageSentSubscribe.Invoke(new MethodInvoker(() => chkMessageSentSubscribe.Enabled = true));                

                this.UseWaitCursor = false;
            }

            // koneksi ke WA GAGAL, bisa dicoba lagi
            if (message.IndexOf("Failure") >= 0 || message.IndexOf("Timeout") >= 0
                || message.IndexOf("ERR_NAME") >= 0)
            {
                // unsubscribe event
                _wa.OnStartup -= OnStartupHandler;
                _wa.OnScanMe -= OnScanMeHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnReceiveMessages -= OnReceiveMessagesHandler;
                _wa.OnReceiveMessageStatus -= OnReceiveMessageStatusHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;

                _wa.Disconnect();

                this.UseWaitCursor = false;

                var msg = string.Format("{0}\n\nKoneksi ke WA gagal, silahkan cek koneksi internet Anda", message);
                MessageBox.Show(msg, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnChangeStateHandler(WhatsAppNETAPI.WAState state, string sessionId)
        {
            lblState.Invoke(new MethodInvoker(() => lblState.Text = string.Format("State: {0}", state.ToString())));
        }

        private void OnScanMeHandler(string qrcodePath, string sessionId)
        {
        }

        private void OnReceiveMessageHandler(WhatsAppNETAPI.Message message, string sessionId)
        {
            var msg = message.content;

            var pengirim = string.Empty;
            var pushName = string.Empty;
            var group = string.Empty;

            var isGroup = message.group != null;

            if (isGroup) // pesan dari group
            {
                group = string.IsNullOrEmpty(message.group.name) ? message.from : message.group.name;

                var sender = message.group.sender;
                pengirim = string.IsNullOrEmpty(sender.name) ? message.from : sender.name;
                pushName = sender.pushname;                
            }
            else
            {
                pengirim = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;
                pushName = message.sender.pushname;
            }                

            var fileName = message.filename;

            var data = string.Empty;

            if (isGroup)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    data = string.Format("[{0}] Group: {1}, Pesan teks: {2}, Pengirim: {3} [{4}]",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim, pushName);
                }
                else
                    data = string.Format("[{0}] Group: {1}, Pesan gambar/dokumen: {2}, Pengirim: {3} [{4}], nama file: {5}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim, pushName, fileName);
            }
            else
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    data = string.Format("[{0}] Pengirim: {1} [{2}], Pesan teks: {3}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, pushName, msg);
                }
                else
                    data = string.Format("[{0}] Pengirim: {1} [{2}], Pesan gambar/dokumen: {3}, nama file: {4}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, pushName, msg, fileName);
            }            

            // update UI dari thread yang berbeda
            lstPesanMasuk.Invoke(() =>
            {
                lstPesanMasuk.Items.Add(data);

                if (message.type == MessageType.Location)
                {
                    var location = message.location;
                    var dataLocation = string.Format("--> latitude: {0}, longitude: {1}, description: {2}",
                        location.latitude, location.longitude, location.description);

                    System.Diagnostics.Debug.Print(dataLocation);

                    lstPesanMasuk.Items.Add(dataLocation);
                }
                else if (message.type == MessageType.VCard || message.type == MessageType.MultiVCard)
                {
                    var vcards = message.vcards;
                    var vcardFilenames = message.vcardFilenames;

                    var index = 0;
                    foreach (var vcard in vcards)
                    {
                        var dataVCard = string.Format("--> N: {0}, FN: {1}, WA Id: {2}, fileName: {3}",
                            vcard.n, vcard.fn, vcard.waId, vcardFilenames[index]);

                        lstPesanMasuk.Items.Add(dataVCard);
                        index++;
                    }
                }

                lstPesanMasuk.SelectedIndex = lstPesanMasuk.Items.Count - 1;
            });

            if (chkAutoReplay.Checked)
            {
                if (chkKirimLokasi.Checked)
                {
                    var location = new Location
                    {
                        latitude = txtLatitude.Text,
                        longitude = txtLongitude.Text,
                        description = txtDescription.Text
                    };

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, location, message.id));
                }
                else
                {
                    var msgReplay = string.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                        pengirim, msg);

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, msgReplay, message.id));
                }                               
            }
        }

        private void OnReceiveMessagesHandler(IList<WhatsAppNETAPI.Message> messages, string sessionId)
        {
            foreach (var message in messages)
            {
                var msg = message.content;

                var pengirim = string.Empty;
                var group = string.Empty;

                var isGroup = message.group != null;

                if (isGroup) // pesan dari group
                {
                    group = string.IsNullOrEmpty(message.group.name) ? message.from : message.group.name;

                    var sender = message.group.sender;
                    pengirim = string.IsNullOrEmpty(sender.name) ? message.from : sender.name;
                }
                else
                    pengirim = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;

                var data = string.Empty;

                if (isGroup)
                {
                    data = string.Format("[{0}] Group: {1}, Pesan teks: {2}, Pengirim: {3}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim);
                }
                else
                {
                    data = string.Format("[{0}] Pengirim: {1}, Isi pesan: {2}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg);
                }                

                // update UI dari thread yang berbeda
                lstPesanMasuk.Invoke(() =>
                {
                    lstPesanMasuk.Items.Add(data);
                    lstPesanMasuk.SelectedIndex = lstPesanMasuk.Items.Count - 1;
                });

                if (chkAutoReplay.Checked)
                {
                    var senderName = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;

                    var msgReplay = string.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                            senderName, msg);

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, msgReplay, message.id));
                }

            }
        }

        private void OnReceiveMessageStatusHandler(WhatsAppNETAPI.MessageStatus msgStatus, string sessionId)
        {
            var status = msgStatus.status == "true" ? "BERHASIL" : "GAGAL";

            var msg = string.Format("Status pengiriman pesan ke {0}, {1}",
                msgStatus.send_to, status);

            // update UI dari thread yang berbeda
            lstPesanKeluar.Invoke(() =>
            {
                lstPesanKeluar.Items.Add(msg);
                lstPesanKeluar.SelectedIndex = lstPesanKeluar.Items.Count - 1;
            });
        }

        private void OnClientConnectedHandler(string sessionId)
        {
            System.Diagnostics.Debug.Print("ClientConnected on {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        }

        #endregion

        private void btnLokasiWAAutomateNodejs_Click(object sender, EventArgs e)
        {
            var folderName = ShowDialogOpenFolder();

            if (!string.IsNullOrEmpty(folderName)) txtLokasiWhatsAppNETAPINodeJs.Text = folderName;
        }

        private void btnLokasiPenyimpananFileAtauGambar_Click(object sender, EventArgs e)
        {
            var folderName = ShowDialogOpenFolder();

            if (!string.IsNullOrEmpty(folderName)) txtLokasiPenyimpananFileAtauGambar.Text = folderName;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnPilihGroup_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPilihGroup("Pilih Group"))
            {
                _wa.OnReceiveGroups += frm.OnReceiveGroupsHandler; // subscribe event
                _wa.GetGroups(false);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _selectedGroup = frm.Group;

                    if (_selectedGroup != null) txtKontak.Text = _selectedGroup.name;
                }

                _wa.OnReceiveGroups -= frm.OnReceiveGroupsHandler; // unsubscribe event
            }
        }

        private void chkGroup_CheckedChanged(object sender, EventArgs e)
        {
            _selectedGroup = null;
            txtKontak.Clear();

            btnPilihGroup.Enabled = chkGroup.Checked;
            txtKontak.Enabled = !chkGroup.Checked;            
        }

        private void chkKirimPesanList_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimPesanList.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimPesanButton.Checked = false;
                chkKirimLokasi.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
        }

        private void chkKirimPesanButton_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimPesanButton.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimLokasi.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
        }
    }
}
