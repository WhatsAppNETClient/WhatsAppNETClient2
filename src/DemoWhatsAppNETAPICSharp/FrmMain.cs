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

            _wa.WaAutomatePath = txtLokasiWhatsAppNETAPINodeJs.Text;

            if (!_wa.IsWaAutomatePathExists)
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

        private void Connect()
        {
            this.UseWaitCursor = true;

            _wa.ImageAndDocumentPath = txtLokasiPenyimpananFileAtauGambar.Text;

            // subscribe event
            _wa.OnStartup += OnStartupHandler;
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

        private void Disconnect()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnGrabContacts.Enabled = false;
            btnGrabGroupAndMembers.Enabled = false;
            btnUnreadMessages.Enabled = false;
            btnArchiveChat.Enabled = false;
            btnDeleteChat.Enabled = false;
            btnKirim.Enabled = false;

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
                _wa.OnScanMe -= OnScanMeHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnReceiveMessages -= OnReceiveMessagesHandler;
                _wa.OnReceiveMessageStatus -= OnReceiveMessageStatusHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;

                _wa.Disconnect();
            }
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            var jumlahPesan = int.Parse(txtJumlahPesan.Text);

            if (jumlahPesan > 1) // broadcast
            {
                var list = new List<MsgArgs>();

                for (int i = 0; i < jumlahPesan; i++)
                {
                    MsgArgs msgArgs = null;

                    if (chkKirimPesanDgGambar.Checked)
                        msgArgs = new MsgArgs(txtKontak.Text, txtPesan.Text, "image", txtFileGambar.Text);
                    else if (chkKirimFileAja.Checked)
                        msgArgs = new MsgArgs(txtKontak.Text, txtPesan.Text, "file", txtFileDokumen.Text);
                    else
                        msgArgs = new MsgArgs(txtKontak.Text, txtPesan.Text, "text");

                    list.Add(msgArgs);
                }

                _wa.BroadcastMessage(list);
            }
            else
            {
                MsgArgs msgArgs = null;

                if (chkKirimPesanDgGambar.Checked)
                    msgArgs = new MsgArgs(txtKontak.Text, txtPesan.Text, "image", txtFileGambar.Text);
                else if (chkKirimFileAja.Checked)
                    msgArgs = new MsgArgs(txtKontak.Text, txtPesan.Text, "file", txtFileDokumen.Text);
                else
                    msgArgs = new MsgArgs(txtKontak.Text, txtPesan.Text, "text");

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
                                               : "File dokumen (*.pdf)|*.pdf";
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
                txtFileDokumen.Clear();
            }
            else
                txtFileGambar.Clear();
        }

        private void chkKirimFileAja_CheckedChanged(object sender, EventArgs e)
        {
            btnCariDokumen.Enabled = chkKirimFileAja.Checked;

            if (chkKirimFileAja.Checked)
            {
                chkKirimPesanDgGambar.Checked = false;
                txtFileGambar.Clear();
            }
            else
                txtFileDokumen.Clear();
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
            _wa.DeleteChat();
        }

        private void btnArchiveChat_Click(object sender, EventArgs e)
        {
            _wa.ArchiveChat();
        }

        private void btnUnreadMessages_Click(object sender, EventArgs e)
        {
            _wa.GetUnreadMessage();
        }

        # region event handler

        private void OnStartupHandler(string message)
        {
            // koneksi ke WA berhasil
            if (message.IndexOf("OPEN-WA ready") >= 0 || message.IndexOf("SUCCESS") >= 0)
            {
                btnStart.Invoke(new MethodInvoker(() => btnStart.Enabled = false));
                btnStop.Invoke(new MethodInvoker(() => btnStop.Enabled = true));
                btnGrabContacts.Invoke(new MethodInvoker(() => btnGrabContacts.Enabled = true));
                btnGrabGroupAndMembers.Invoke(new MethodInvoker(() => btnGrabGroupAndMembers.Enabled = true));

                btnUnreadMessages.Invoke(new MethodInvoker(() => btnUnreadMessages.Enabled = true));
                btnArchiveChat.Invoke(new MethodInvoker(() => btnArchiveChat.Enabled = true));
                btnDeleteChat.Invoke(new MethodInvoker(() => btnDeleteChat.Enabled = true));

                btnKirim.Invoke(new MethodInvoker(() => btnKirim.Enabled = true));
                chkSubscribe.Invoke(new MethodInvoker(() => chkSubscribe.Enabled = true));
                chkMessageSentSubscribe.Invoke(new MethodInvoker(() => chkMessageSentSubscribe.Enabled = true));                

                this.UseWaitCursor = false;
            }

            // koneksi ke WA GAGAL, bisa dicoba lagi
            if (message.IndexOf("App Offline") >= 0 || message.IndexOf("Timeout") >= 0
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

                MessageBox.Show("Koneksi ke WA gagal, silahkan dicoba lagi.", "Peringatan", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnScanMeHandler(string qrcodePath)
        {
            
        }

        private void OnReceiveMessageHandler(WhatsAppNETAPI.Message message)
        {
            var msg = message.type == "chat" ? message.content : message.caption;
            var pengirim = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;

            var data = string.Format("[{0}] Pengirim: {1}, Isi pesan: {2}",
                message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg);

            // update UI dari thread yang berbeda
            lstPesanMasuk.Invoke(() =>
            {
                lstPesanMasuk.Items.Add(data);
                lstPesanMasuk.SelectedIndex = lstPesanMasuk.Items.Count - 1;
            });

            if (chkAutoReplay.Checked)
            {
                var msgReplay = string.Format("Bpk/Ibu *{0}*, pesan *{1}* sudah kami terima. Silahkan ditunggu.",
                        pengirim, msg);

                _wa.SendMessage(new MsgArgs(message.from, msgReplay, "text"));
            }
        }

        private void OnReceiveMessagesHandler(IList<WhatsAppNETAPI.Message> messages)
        {
            foreach (var message in messages)
            {
                var msg = message.type == "chat" ? message.content : message.caption;
                var pengirim = string.IsNullOrEmpty(message.sender.name) ? message.from : message.sender.name;

                var data = string.Format("[{0}] Pengirim: {1}, Isi pesan: {2}",
                    message.datetime.ToString("yyyy-MM-dd HH:mm:ss"), pengirim, msg);

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

                    _wa.SendMessage(new MsgArgs(message.from, msgReplay, "text"));
                }

            }
        }

        private void OnReceiveMessageStatusHandler(WhatsAppNETAPI.MessageStatus msgStatus)
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

        private void OnClientConnectedHandler()
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
        
    }
}
