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
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using WhatsAppNETAPI;
using ConceptCave.WaitCursor;

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

            _wa.OnCreatedGroupStatus += OnCreatedGroupStatusHandler;
            _wa.OnUnreadMessage += OnUnreadMessageHandler; // subscribe event
            _wa.OnReceiveMessages += OnReceiveMessagesHandler;
            _wa.OnGroupJoin += OnGroupJoinHandler;
            _wa.OnGroupLeave += OnGroupLeaveHandler;
            _wa.OnClientConnected += OnClientConnectedHandler;
            _wa.OnMonitoringLog += OnMonitoringLogHandler;
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

        private void OnCreatedGroupStatusHandler(GroupStatus groupStatus, string sessionId)
        {
            var status = groupStatus.status == "true" ? "BERHASIL" : "GAGAL";

            var msg = string.Format("Status pembuatan group = {0}, status = {1}, groupId = {2}",
                groupStatus.name, status, groupStatus.id);

            // update UI dari thread yang berbeda
            lstPesanKeluar.Invoke(() =>
            {
                lstPesanKeluar.Items.Add(msg);
                lstPesanKeluar.SelectedIndex = lstPesanKeluar.Items.Count - 1;
            });
        }

        private void OnMessageAckHandler(WhatsAppNETAPI.Message message, string sessionId)
        {
            var msg = string.Format("Status pengiriman pesan ke {0}, messageId = {1}, status = {2}",
                message.to, message.id, message.ack.ToString());

            // update UI dari thread yang berbeda
            lstStatusPesanKeluar.Invoke(() =>
            {
                lstStatusPesanKeluar.Items.Add(msg);
                lstStatusPesanKeluar.SelectedIndex = lstStatusPesanKeluar.Items.Count - 1;
            });
        }

        private void OnGroupJoinHandler(GroupNotification notification, string sessionId)
        {
            var recipients = string.Join(", ", notification.recipients
                .Select(f => f.name)
                .ToArray());

            var msgReplay = string.Format("Selamat bergabung Bpk/Ibu *{0}*, di group *{1}*.",
                        recipients, notification.name);

            var msgArgs = new MsgArgs(notification.id, msgReplay, MsgArgsType.Text);
            _wa.SendMessage(msgArgs);
        }

        private void OnGroupLeaveHandler(GroupNotification notification, string sessionId)
        {
            var recipients = string.Join(", ", notification.recipients
                .Select(f => f.name)
                .ToArray());

            var msgReplay = string.Format("Selamat berpisah Bpk/Ibu *{0}*, semoga bisa join lagi di group *{1}*.",
                        recipients, notification.name);

            var msgArgs = new MsgArgs(notification.id, msgReplay, MsgArgsType.Text);
            _wa.SendMessage(msgArgs);
        }

        private void Disconnect(bool isLogout = false)
        {
            btnStart.Invoke(new MethodInvoker(() => btnStart.Enabled = true));
            btnStop.Invoke(new MethodInvoker(() => btnStop.Enabled = false));
            btnLogout.Invoke(new MethodInvoker(() => btnLogout.Enabled = false));
            btnGrabContacts.Invoke(new MethodInvoker(() => btnGrabContacts.Enabled = false));
            btnVerifyContact.Invoke(new MethodInvoker(() => btnVerifyContact.Enabled = false));
            btnCheckBusinessProfile.Invoke(new MethodInvoker(() => btnCheckBusinessProfile.Enabled = false));
            btnCreateGroup.Invoke(new MethodInvoker(() => btnCreateGroup.Enabled = false));
            btnAddRemoveGroupMember.Invoke(new MethodInvoker(() => btnAddRemoveGroupMember.Enabled = false));
            btnSendContact.Invoke(new MethodInvoker(() => btnSendContact.Enabled = false));
            btnSendSticker.Invoke(new MethodInvoker(() => btnSendSticker.Enabled = false));
            btnSendGif.Invoke(new MethodInvoker(() => btnSendGif.Enabled = false));
            btnSetStatusOnlineOffline.Invoke(new MethodInvoker(() => btnSetStatusOnlineOffline.Enabled = false));
            btnGrabGroupAndMembers.Invoke(new MethodInvoker(() => btnGrabGroupAndMembers.Enabled = false));
            btnAllMessages.Invoke(new MethodInvoker(() => btnAllMessages.Enabled = false));
            btnWANumber.Invoke(new MethodInvoker(() => btnWANumber.Enabled = false));
            btnArchiveChat.Invoke(new MethodInvoker(() => btnArchiveChat.Enabled = false));
            btnDeleteChat.Invoke(new MethodInvoker(() => btnDeleteChat.Enabled = false));
            btnKirim.Invoke(new MethodInvoker(() => btnKirim.Enabled = false));

            chkGroup.Invoke(new MethodInvoker(() => chkGroup.Checked = false));
            chkGroup.Invoke(new MethodInvoker(() => chkGroup.Enabled = false));

            txtFileDokumen.Invoke(new MethodInvoker(() => txtFileDokumen.Clear()));
            txtFileGambar.Invoke(new MethodInvoker(() => txtFileGambar.Clear()));

            chkSubscribe.Invoke(new MethodInvoker(() =>
            {
                chkSubscribe.Checked = false;
                chkSubscribe.Enabled = false;
            }));

            chkMessageSentSubscribe.Invoke(new MethodInvoker(() =>
            {
                chkMessageSentSubscribe.Checked = false;
                chkMessageSentSubscribe.Enabled = false;
            }));

            chkMessageSentStatusSubscribe.Invoke(new MethodInvoker(() =>
            {
                chkMessageSentStatusSubscribe.Checked = false;
                chkMessageSentStatusSubscribe.Enabled = false;
            }));

            chkAutoReplay.Invoke(new MethodInvoker(() =>
            {
                chkAutoReplay.Checked = false;
                chkAutoReplay.Enabled = false;
            }));

            lstPesanMasuk.Invoke(new MethodInvoker(() => lstPesanMasuk.Items.Clear()));

            using (new StCursor(Cursors.WaitCursor, new TimeSpan(0, 0, 0, 0)))
            {
                // unsubscribe event
                _wa.OnStartup -= OnStartupHandler;
                _wa.OnChangeState -= OnChangeStateHandler;

                _wa.OnScanMe -= OnScanMeHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnUnreadMessage -= OnUnreadMessageHandler;
                _wa.OnReceiveMessages -= OnReceiveMessagesHandler;
                _wa.OnCreatedGroupStatus -= OnCreatedGroupStatusHandler;
                _wa.OnMessageAck -= OnMessageAckHandler;
                _wa.OnReceiveMessageStatus -= OnReceiveMessageStatusHandler;
                _wa.OnGroupJoin -= OnGroupJoinHandler;
                _wa.OnGroupLeave -= OnGroupLeaveHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;
                _wa.OnMonitoringLog -= OnMonitoringLogHandler;

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
                {
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Image, txtFileGambar.Text);
                }
                else if (chkKirimGambarDariUrl.Checked)
                {
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Url, txtUrl.Text);
                }
                else if (chkKirimFileAja.Checked)
                {
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.File, txtFileDokumen.Text);
                }
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
                    list.footer = "http://wa-net.coding4ever.net/";
                    list.content = @"Assalamualaikum warahmatullahi wabarakatuh
        
Selamat datang, silahkan pilih menu yang tersedia.";

                    var section1 = new Section
                    {
                        title = "MENU MAKANAN",
                        items = new ListItem[]
                        {
                            new ListItem { id = "baksoUrat", title = "Bakso Urat", description = "Rp. 20,000" },
                            new ListItem { id = "baksoTelor", title = "Bakso Telor", description = "Rp. 15,000" },
                            new ListItem { id = "sotoAyam", title = "Soto Ayam", description = "Rp. 17,000" }
                        }
                    };

                    var section2 = new Section
                    {
                        title = "MENU MINUMAN",
                        items = new ListItem[]
                        {
                            new ListItem { id = "esJeruk", title = "Es Jeruk", description = "Rp. 7,000" },
                            new ListItem { id = "esTeh", title = "Es Teh", description = "Rp. 5,000" },
                            new ListItem { id = "jusAlpukat", title = "Jus Alpukat", description = "Rp. 8,000" }
                        }
                    };

                    list.sections = new Section[] { section1, section2 };

                    msgArgs = new MsgArgs(kontak, list);
                }
                else if (chkKirimPesanButton.Checked)
                {
                    var button = new WhatsAppNETAPI.Button();
                    button.title = "Menu";
                    button.footer = "http://wa-net.coding4ever.net/";
                    button.content = @"Assalamualaikum warahmatullahi wabarakatuh
        
Selamat datang, silahkan klik tombol yang tersedia.";

                    button.items = new ButtonItem[]
                    {
                        new ButtonItem { id = "btn_1", title = "Tombol 1" },
                        new ButtonItem { id = "btn_2", title = "Tombol 2" }
                    };

                    msgArgs = new MsgArgs(kontak, button);
                }
                else if (chkKirimPesanButtonDgGambar.Checked)
                {
                    var button = new WhatsAppNETAPI.Button();
                    button.footer = "http://wa-net.coding4ever.net/";
                    button.content = @"*Assalamualaikum warahmatullahi wabarakatuh*
        
Selamat datang, silahkan klik tombol yang tersedia.";

                    button.items = new ButtonItem[]
                    {
                        new ButtonItem { id = "btn_1", title = "Tombol 1" },
                        new ButtonItem { id = "btn_2", title = "Tombol 2" }
                    };

                    msgArgs = new MsgArgs(kontak, button, txtFileLocalAtauUrl.Text);
                }
                else if (chkKirimPesanButtonCTA.Checked)
                {
                    var button = new WhatsAppNETAPI.Button();
                    button.footer = "http://wa-net.coding4ever.net/";
                    button.content = @"*Assalamualaikum warahmatullahi wabarakatuh*
        
Selamat datang, silahkan klik tombol yang tersedia.";

                    button.items = new ButtonItem[]
                    {
                        new ButtonItem { index = 1, urlButton = new UrlButton { title = "⭐ Star WhatsApp NET Client", url  = "https://github.com/WhatsAppNETClient/WhatsAppNETClient2"} },
                        new ButtonItem { index = 2, callButton = new CallButton { title = "🤙 Call me!", phoneNumber  = "+6281381769915"} },
                        new ButtonItem { index = 3, quickReplyButton = new QuickReplyButton { id = "id-like-buttons-message", title = "This is a reply, just like normal buttons!" } }
                    };

                    msgArgs = new MsgArgs(kontak, button);
                }
                else if (chkKirimPesanButtonCTADgGambar.Checked)
                {
                    var button = new WhatsAppNETAPI.Button();
                    button.footer = "http://wa-net.coding4ever.net/";
                    button.content = @"*Assalamualaikum warahmatullahi wabarakatuh*
        
Selamat datang, silahkan klik tombol yang tersedia.";

                    button.items = new ButtonItem[]
                    {
                        new ButtonItem { index = 1, urlButton = new UrlButton { title = "⭐ Star WhatsApp NET Client", url  = "https://github.com/WhatsAppNETClient/WhatsAppNETClient2"} },
                        new ButtonItem { index = 2, callButton = new CallButton { title = "🤙 Call me!", phoneNumber  = "+6281381769915"} },
                        new ButtonItem { index = 3, quickReplyButton = new QuickReplyButton { id = "id-like-buttons-message", title = "This is a reply, just like normal buttons!" } }
                    };

                    msgArgs = new MsgArgs(kontak, button, txtFileLocalAtauUrl2.Text);
                }
                else
                {
                    msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text);

                    // contoh penggunaan mention user
                    // var mentions = new string[] { "081381712345", "08138174444", "tambahkan nomor yang lain" };
                    // msgArgs = new MsgArgs(kontak, txtPesan.Text, MsgArgsType.Text, mentions);
                }

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
            var fileName = Helper.ShowDialogOpen("Lokasi file gambar", true);
            if (!string.IsNullOrEmpty(fileName)) txtFileGambar.Text = fileName;
        }

        private void btnCariDokumen_Click(object sender, EventArgs e)
        {
            var fileName = Helper.ShowDialogOpen("Lokasi file dokumen");
            if (!string.IsNullOrEmpty(fileName)) txtFileDokumen.Text = fileName;
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
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

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
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

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
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

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
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

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

        private void btnVerifyContact_Click(object sender, EventArgs e)
        {
            // daftar kontak yang mau di verifikasi
            // bisa diambil dari database atau hasil generatean
            var contacts = new List<string>
            {
                "081381712345", "089652948305",
                "085211112345", "081381712345", "085291123456", "081336123456"
            };

            using (var frm = new FrmContactOrGroup("Contacts"))
            {
                _wa.OnReceiveContacts += frm.OnReceiveContactsHandler; // subscribe event
                _wa.VerifyWANumber(contacts);

                frm.ShowDialog();
                _wa.OnReceiveContacts -= frm.OnReceiveContactsHandler; // unsubscribe event
            }
        }

        private void btnGrabGroupAndMembers_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmContactOrGroup("Groups and Members"))
            {
                _wa.OnReceiveGroups += frm.OnReceiveGroupsHandler; // subscribe event
                _wa.GetGroups(); // menampilkan semua group dan member

                // contoh jika ingin menampilkan member dari group tertentu, tinggal tambahkan parameter groupId
                // var groupId = "1203630xxxxx3785177@g.us";
                // _wa.GetGroups(groupId);

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

        private void btnArchiveChat_Click(object sender, EventArgs e)
        {
            var msg = "Fungsi ini akan MENGARSIPKAN semua pesan.\n" +
                      "Apakah ingin dilanjutkan";

            if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _wa.ArchiveChat(); // arsip semua chat

                // contoh jika ingin mengarsipkan berdasarkan phoneNumber
                // var phoneNumber = "0813123456789";
                // _wa.ArchiveChat(phoneNumber);                
            }
        }

        private void btnAllMessages_Click(object sender, EventArgs e)
        {
            var phoneNumber = "081381761234";
            _wa.GetAllMessage(phoneNumber, 3); // menampilkan 3 pesan terakhir            
        }

        #region event handler

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
                btnVerifyContact.Invoke(new MethodInvoker(() => btnVerifyContact.Enabled = true));
                btnCheckBusinessProfile.Invoke(new MethodInvoker(() => btnCheckBusinessProfile.Enabled = true));
                btnCreateGroup.Invoke(new MethodInvoker(() => btnCreateGroup.Enabled = true));
                btnAddRemoveGroupMember.Invoke(new MethodInvoker(() => btnAddRemoveGroupMember.Enabled = true));
                btnSetStatusOnlineOffline.Invoke(new MethodInvoker(() => btnSetStatusOnlineOffline.Enabled = true));
                btnSendContact.Invoke(new MethodInvoker(() => btnSendContact.Enabled = true));
                btnSendSticker.Invoke(new MethodInvoker(() => btnSendSticker.Enabled = true));
                btnSendGif.Invoke(new MethodInvoker(() => btnSendGif.Enabled = true));

                btnAllMessages.Invoke(new MethodInvoker(() => btnAllMessages.Enabled = true));
                btnArchiveChat.Invoke(new MethodInvoker(() => btnArchiveChat.Enabled = true));
                btnDeleteChat.Invoke(new MethodInvoker(() => btnDeleteChat.Enabled = true));

                btnWANumber.Invoke(new MethodInvoker(() => btnWANumber.Enabled = true));

                chkGroup.Invoke(new MethodInvoker(() => chkGroup.Enabled = true));
                btnKirim.Invoke(new MethodInvoker(() => btnKirim.Enabled = true));
                chkSubscribe.Invoke(new MethodInvoker(() => chkSubscribe.Enabled = true));
                chkMessageSentSubscribe.Invoke(new MethodInvoker(() => chkMessageSentSubscribe.Enabled = true));
                chkMessageSentStatusSubscribe.Invoke(new MethodInvoker(() => chkMessageSentStatusSubscribe.Enabled = true));

                this.UseWaitCursor = false;
            }

            // koneksi ke WA GAGAL, bisa dicoba lagi
            System.Diagnostics.Debug.Print("Error: {0}", message);

            if (message.IndexOf("Failure") >= 0 || message.IndexOf("Timeout") >= 0
                || message.IndexOf("ERR_NAME") >= 0 || message.IndexOf("ERR_CONNECTION") >= 0
                || message.IndexOf("close") >= 0)
            {
                // unsubscribe event
                _wa.OnStartup -= OnStartupHandler;
                _wa.OnScanMe -= OnScanMeHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnUnreadMessage -= OnUnreadMessageHandler;
                _wa.OnReceiveMessages -= OnReceiveMessagesHandler;
                _wa.OnCreatedGroupStatus -= OnCreatedGroupStatusHandler;
                _wa.OnMessageAck -= OnMessageAckHandler;
                _wa.OnReceiveMessageStatus -= OnReceiveMessageStatusHandler;
                _wa.OnGroupJoin -= OnGroupJoinHandler;
                _wa.OnGroupLeave -= OnGroupLeaveHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;
                _wa.OnMonitoringLog -= OnMonitoringLogHandler;

                _wa.Disconnect();

                this.UseWaitCursor = false;

                var msg = string.Format("Err: {0}\n\nKoneksi ke WA gagal, silahkan cek koneksi internet Anda", message);
                MessageBox.Show(msg, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnChangeStateHandler(WhatsAppNETAPI.WAState state, string sessionId)
        {
            System.Diagnostics.Debug.Print("State: {0}", state.ToString());
            if (state == WAState.CLOSE && btnStop.Enabled) Disconnect();
        }

        private void OnScanMeHandler(string qrcodePath, string sessionId)
        {
        }

        private void OnReceiveMessageHandler(WhatsAppNETAPI.Message message, string sessionId)
        {
            var msg = message.content;

            var pengirim = string.Empty;
            var group = string.Empty;

            if (message.id == "status@broadcast") // status@broadcast -> dummy message, penanda load data selesai
                return;

            var isGroup = message.group != null;

            if (isGroup) // pesan dari group
            {
                group = string.IsNullOrEmpty(message.group.name) ? message.from : message.group.name;

                var sender = message.group.sender;
                pengirim = string.IsNullOrEmpty(sender.name) ? message.from : sender.name;
            }
            else
            {
                pengirim = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;
            }

            // tandai pesan sudah dibaca
            _wa.MarkRead(message.from);

            // hapus pesan di group dengan kondisi tertentu
            if (isGroup)
            {
                // TODO: validasi pesan yang mau dihapus
                if (msg == "saru" || msg == "test")
                    _wa.DeleteMessage(message.group.id, message.group.sender.id);
            }

            var fileName = message.filename;

            var data = string.Empty;

            if (isGroup)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    data = string.Format("[{0}] Group: {1}, Pesan teks: {2}, Pengirim: {3}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim);
                }
                else
                    data = string.Format("[{0}] Group: {1}, Pesan gambar/dokumen: {2}, Pengirim: {3}, nama file: {4}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim, fileName);
            }
            else
            {
                if (message.type == MessageType.Call) // handle telepon masuk
                {
                    data = string.Format("[{0}] Telpon masuk dari : {1}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim);
                }
                else
                {
                    if (string.IsNullOrEmpty(fileName))
                    {
                        data = string.Format("[{0}] Pengirim: {1}, Pesan teks: {2}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg);
                    }
                    else
                        data = string.Format("[{0}] Pengirim: {1}, Pesan gambar/dokumen: {2}, nama file: {3}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg, fileName);
                }
            }

            // update UI dari thread yang berbeda
            lstPesanMasuk.Invoke(() =>
            {
                lstPesanMasuk.Items.Add(data);

                if (message.type == MessageType.Location)
                {
                    var location = message.location;
                    var dataLocation = string.Format("--> live location: {0}, latitude: {1}, longitude: {2}, description: {3}, thumbnail: {4}",
                        location.liveLocation, location.latitude, location.longitude, location.description, location.thumbnail);

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
                else if (message.type == MessageType.ButtonResponse)
                {
                    lstPesanMasuk.Items.Add(string.Format("--> Id button yang dipilih: {0}", message.selectedButtonId));
                }
                else if (message.type == MessageType.ListResponse)
                {
                    lstPesanMasuk.Items.Add(string.Format("--> Id list yang dipilih: {0}", message.selectedRowId));
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
                    /*var msgReplay = string.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                        pengirim, msg);

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, msgReplay, message.id));*/

                    var sticker = new Sticker
                    {
                        packName = "Ngopi",
                        author = "Coding4ever",
                        attachmentOrUrl = @"F:\Lab\WhatsAppNETAPINodeJs\engine-baru\docs\rider\rider.png",
                        quality = 50
                    };

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, sticker, message.id));
                }
            }
        }

        private void OnUnreadMessageHandler(WhatsAppNETAPI.Message message, string sessionId)
        {
            var msg = message.content;

            var pengirim = string.Empty;
            var group = string.Empty;

            if (message.id == "status@broadcast") // status@broadcast -> dummy message, penanda load data selesai
                return;

            var isGroup = message.group != null;

            if (isGroup) // pesan dari group
            {
                group = string.IsNullOrEmpty(message.group.name) ? message.from : message.group.name;

                var sender = message.group.sender;
                pengirim = string.IsNullOrEmpty(sender.name) ? message.from : sender.name;
            }
            else
            {
                pengirim = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;
            }

            // TODO: validasi pesan yang mau ditandai sudah dibaca
            // _wa.MarkRead(message.from);

            if (isGroup)
            {
                // TODO: validasi pesan yang mau dihapus
                if (msg == "saru" || msg == "test")
                    _wa.DeleteMessage(message.group.id, message.group.sender.id);
            }

            var fileName = message.filename;

            var data = string.Empty;

            if (isGroup)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    data = string.Format("[{0}] Group: {1}, Pesan teks: {2}, Pengirim: {3}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim);
                }
                else
                    data = string.Format("[{0}] Group: {1}, Pesan gambar/dokumen: {2}, Pengirim: {3}, nama file: {4}",
                        message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), group, msg, pengirim, fileName);
            }
            else
            {
                if (message.type == MessageType.Call) // handle telepon masuk
                {
                    data = string.Format("[{0}] Telpon masuk dari : {1}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim);
                }
                else
                {
                    if (string.IsNullOrEmpty(fileName))
                    {
                        data = string.Format("[{0}] Pengirim: {1}, Pesan teks: {2}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg);
                    }
                    else
                        data = string.Format("[{0}] Pengirim: {1}, Pesan gambar/dokumen: {2}, nama file: {3}",
                            message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg, fileName);
                }
            }

            // update UI dari thread yang berbeda
            lstPesanMasuk.Invoke(() =>
            {
                lstPesanMasuk.Items.Add("-- unread message --");
                lstPesanMasuk.Items.Add(data);

                if (message.type == MessageType.Location)
                {
                    var location = message.location;
                    var dataLocation = string.Format("--> live location: {0}, latitude: {1}, longitude: {2}, description: {3}, thumbnail: {4}",
                        location.liveLocation, location.latitude, location.longitude, location.description, location.thumbnail);

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
                else if (message.type == MessageType.ButtonResponse)
                {
                    lstPesanMasuk.Items.Add(string.Format("--> Id button yang dipilih: {0}", message.selectedButtonId));
                }
                else if (message.type == MessageType.ListResponse)
                {
                    lstPesanMasuk.Items.Add(string.Format("--> Id list yang dipilih: {0}", message.selectedRowId));
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
                    /*var msgReplay = string.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                        pengirim, msg);

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, msgReplay, message.id));*/

                    var sticker = new Sticker
                    {
                        packName = "Ngopi",
                        author = "Coding4ever",
                        attachmentOrUrl = @"F:\Lab\WhatsAppNETAPINodeJs\engine-baru\docs\rider\rider.png",
                        quality = 50
                    };

                    _wa.ReplyMessage(new ReplyMsgArgs(message.from, sticker, message.id));
                }
            }
        }

        private void OnReceiveMessagesHandler(IList<WhatsAppNETAPI.Message> messages, string sessionId)
        {
            foreach (var message in messages)
            {
                if (message.id == "status@broadcast") // status@broadcast -> dummy message, penanda load data selesai
                    continue;

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

            var msg = string.Format("Status pengiriman pesan ke {0}, status = {1}, messageId = {2}",
                msgStatus.send_to, status, msgStatus.messageId);

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

        /// <summary>
        /// Menampilkan log INFO/ERROR yang dikirim dari node js
        /// </summary>
        /// <param name="level">Berisi INFO atau ERROR</param>
        /// <param name="message"></param>
        /// <param name="sessionId"></param>
        private void OnMonitoringLogHandler(string level, string message, string sessionId)
        {
            System.Diagnostics.Debug.Print("level: {0}, message: {1}", level, message);
        }

        #endregion

        private void btnLokasiWAAutomateNodejs_Click(object sender, EventArgs e)
        {
            var folderName = Helper.ShowDialogOpenFolder();

            if (!string.IsNullOrEmpty(folderName)) txtLokasiWhatsAppNETAPINodeJs.Text = folderName;
        }

        private void btnLokasiPenyimpananFileAtauGambar_Click(object sender, EventArgs e)
        {
            var folderName = Helper.ShowDialogOpenFolder();

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
                _wa.GetGroups(false); // menampilkan semua group

                // contoh jika ingin menampilkan group berdasarkan groupId
                // var groupId = "1203630xxxxx3785177@g.us";
                // _wa.GetGroups(groupId, false);

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
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

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
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
        }

        private void btnInfoWANumber_Click(object sender, EventArgs e)
        {
            var msg = string.Format("Nomor WA: {0}", _wa.GetCurrentNumber);

            MessageBox.Show(msg, "Infomasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkMessageSentStatusSubscribe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMessageSentStatusSubscribe.Checked)
            {
                _wa.OnMessageAck += OnMessageAckHandler; // subscribe event
            }
            else
            {
                _wa.OnMessageAck -= OnMessageAckHandler; // unsubscribe event
                lstStatusPesanKeluar.Items.Clear();
            }
        }

        private void chkKirimPesanButtonDgGambar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimPesanButtonDgGambar.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanButton.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
        }

        private void chkKirimPesanButtonCTA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimPesanButtonCTA.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanButton.Checked = false;
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTADgGambar.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
        }

        private void chkKirimPesanButtonCTADgGambar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKirimPesanButtonCTADgGambar.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                chkKirimGambarDariUrl.Checked = false;
                chkKirimFileAja.Checked = false;
                chkKirimPesanList.Checked = false;
                chkKirimLokasi.Checked = false;
                chkKirimPesanButton.Checked = false;
                chkKirimPesanButtonDgGambar.Checked = false;
                chkKirimPesanButtonCTA.Checked = false;

                txtFileGambar.Clear();
                txtFileDokumen.Clear();

                txtLatitude.Enabled = false;
                txtLongitude.Enabled = false;
                txtDescription.Enabled = false;
            }
        }

        private void btnSetStatusOnlineOffline_Click(object sender, EventArgs e)
        {
            // set status online
            _wa.SetOnlineStatus(true);

            // set status offline
            // _wa.SetOnlineStatus(false);
        }

        private void btnSendContact_Click(object sender, EventArgs e)
        {
            var vcard = new VCard
            {
                fn = "Kamarudin",
                org = "Innovation Center",
                title = ".NET Developer",
                telWork = "02748812345",
                mobile = "6283124312345" // khusus mobile gunakan kode area, tanpa tanda +
            };

            var list = new List<VCard>();
            list.Add(vcard);

            var msgArgs = new MsgArgs(txtKontak.Text, list.ToArray());

            _wa.SendMessage(msgArgs);
        }

        private void btnSendSticker_Click(object sender, EventArgs e)
        {
            var sticker = new Sticker
            {
                packName = "Ngopi",
                author = "Coding4ever",
                attachmentOrUrl = @"C:\Users\Roedhi\Pictures\rider.png",
                quality = 50
            };

            var kontak = txtKontak.Text;

            var msgArgs = new MsgArgs(kontak, sticker);
            _wa.SendMessage(msgArgs);
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            // minimal 1 contact
            var contact = new Contact { id = "081381761234" };

            var newGroup = new Group
            {
                name = "My Fav Group",
                greeting_message = "Selamat bergabung di My Fav Group",
                members = new List<Contact> { contact }
            };

            _wa.CreateGroup(newGroup);
        }

        private void btnCheckBusinessProfile_Click(object sender, EventArgs e)
        {
            var contacts = new List<string> { "082324565565", "089685899699" };

            using (var frm = new FrmContactOrGroup("Business Profiles"))
            {
                _wa.OnReceiveBusinessProfiles += frm.OnReceiveBusinessProfilesHandler; // subscribe event
                _wa.GetBusinessProfile(contacts);

                frm.ShowDialog();
                _wa.OnReceiveBusinessProfiles -= frm.OnReceiveBusinessProfilesHandler; // unsubscribe event
            }
        }

        private void btnAddRemoveGroupMember_Click(object sender, EventArgs e)
        {
            var contact = new Contact { id = "081381761234" };

            var group = new Group
            {
                id = "120363022253785177@g.us",
                members = new List<Contact> { contact }
            };

            // add member
            _wa.AddRemoveGroupMember(group, "add");

            // remove member       
            // _wa.AddRemoveGroupMember(group, "remove");
        }

        private void btnSendGif_Click(object sender, EventArgs e)
        {
            var msgArgs = new MsgArgs(txtKontak.Text, "Tes kirim gif", MsgArgsType.Gif, @"C:\Users\Roedhi\Videos\running.mp4");
            _wa.SendMessage(msgArgs);
        }

        private void btnDeleteChat_Click(object sender, EventArgs e)
        {
            var msg = "Fungsi ini akan MENGHAPUS semua pesan.\n" +
                      "Apakah ingin dilanjutkan";

            if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _wa.DeleteChat(); // hapus semua pesan

                // contoh jika ingin menghapus berdasarkan phoneNumber
                // var phoneNumber = "0813123456789";
                // _wa.DeleteChat(phoneNumber);
            }
        }
    }
}
