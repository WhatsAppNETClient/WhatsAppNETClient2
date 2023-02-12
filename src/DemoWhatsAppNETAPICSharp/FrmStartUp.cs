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

using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace DemoWhatsAppNETAPICSharp
{
    public partial class FrmStartUp : Form
    {
        public FrmStartUp()
        {
            InitializeComponent();
        }

        public void OnScanMeHandler(string qrcodePath, string sessionId)
        {
            try
            {
                
                if (File.Exists(qrcodePath))
                {
                    Image qrCode = null;

                    // https://stackoverflow.com/questions/13625637/c-sharp-image-from-file-close-connection
                    using (var originalImage = new Bitmap(qrcodePath))
                    {
                        qrCode = new Bitmap(originalImage);
                    }

                    // update UI dari thread yang berbeda
                    picQRCode.Invoke(new MethodInvoker(() =>
                    {
                        picQRCode.Visible = true;
                        picQRCode.Image = qrCode;
                    }));
                }
            }
            catch
            {
                
            }
        }

        public void OnStartupHandler(string message, string sessionId)
        {
            if (message.IndexOf("Ready") >= 0 || message.IndexOf("Failure") >= 0 
                || message.IndexOf("Timeout") >= 0 || message.IndexOf("ERR_NAME") >= 0
                || message.IndexOf("ERR_CONNECTION") >= 0 || message.IndexOf("close") >= 0)
            {
                if (this.IsHandleCreated)
                    this.Invoke(new MethodInvoker(() => this.Close()));
            }
            else
            {
                // update UI dari thread yang berbeda
                lstLog.Invoke(new MethodInvoker(() =>
                {
                    lstLog.Items.Add(message);
                    lstLog.SelectedIndex = lstLog.Items.Count - 1;
                }
                ));
            }
        }        
    }
}
