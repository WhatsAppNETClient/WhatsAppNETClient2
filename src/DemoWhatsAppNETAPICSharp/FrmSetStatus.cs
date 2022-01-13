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

namespace DemoWhatsAppNETAPICSharp
{
    public partial class FrmSetStatus : Form
    {
        private IWhatsAppNETAPI _wa;

        public FrmSetStatus(string title, IWhatsAppNETAPI wa)
        {
            InitializeComponent();
            _wa = wa;
            this.Text = title;
        }

        private void btnSetStatus_Click(object sender, EventArgs e)
        {
            StatusArgs status = null;

            if (string.IsNullOrEmpty(txtStatus.Text))
            {
                MessageBox.Show("Status belum diisi", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStatus.Focus();
                return;
            }

            if (chkGambar.Checked)
            {
                if (string.IsNullOrEmpty(txtFileGambar.Text))
                    status = new StatusArgs(txtStatus.Text);
                else
                    status = new StatusArgs(txtStatus.Text, MsgArgsType.Image, txtFileGambar.Text);
            }
            else if (chkUrl.Checked)
                status = new StatusArgs(txtStatus.Text, MsgArgsType.Url, txtUrl.Text);
            else
                status = new StatusArgs(txtStatus.Text);

            _wa.SetStatus(status);

            this.Close();
        }

        private void btnCariGambar_Click(object sender, EventArgs e)
        {
            var fileName = Helper.ShowDialogOpen("Lokasi file gambar", true);
            if (!string.IsNullOrEmpty(fileName)) txtFileGambar.Text = fileName;
        }

        private void chkGambar_CheckedChanged(object sender, EventArgs e)
        {
            btnCariGambar.Enabled = chkGambar.Checked;
            if (chkGambar.Checked)
            {
                chkUrl.Checked = false;
            }
            else
                txtFileGambar.Clear();
        }

        private void chkUrl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUrl.Checked)
            {
                chkGambar.Checked = false;
            }
        }
    }
}
