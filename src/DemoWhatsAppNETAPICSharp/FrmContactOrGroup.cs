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
    public partial class FrmContactOrGroup : Form
    {
        private int noUrut = 1;

        public FrmContactOrGroup(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        private void FrmContactOrGroup_Load(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
        }

        public void OnReceiveContactsHandler(IList<Contact> contacts, string sessionId)
        {
            // update UI dari thread yang berbeda
            lstContactOrGroup.Invoke(() =>
            {
                foreach (var contact in contacts)
                {
                    if (!(contact.id == "status@broadcast"))
                    {
                        lstContactOrGroup.Items.Add(string.Format("{0}. {1} - {2}, {3}",
                        noUrut, contact.id, contact.name, contact.pushname));

                        noUrut++;
                    }
                    else // status@broadcast -> dummy contact, penanda load data contact selesai
                    {
                        if (this.IsHandleCreated)
                            this.Invoke(new MethodInvoker(() => this.UseWaitCursor = false));
                    }
                        
                }                
            });
        }

        public void OnReceiveGroupsHandler(IList<Group> groups, string sessionId)
        {
            // update UI dari thread yang berbeda
            lstContactOrGroup.Invoke(() =>
            {
                foreach (var group in groups)
                {
                    if (!(group.id == "status@broadcast"))
                    {
                        lstContactOrGroup.Items.Add(string.Format("{0}. {1} - {2}",
                        noUrut, group.id, group.name));

                        noUrut++;

                        var noUrutMember = 1;
                        foreach (var member in group.members)
                        {
                            lstContactOrGroup.Items.Add(string.Format("---> {0}. {1} - {2}",
                                noUrutMember, member.id, member.name));

                            noUrutMember++;
                        }
                    }
                    else // status@broadcast -> dummy group, penanda load data group selesai
                    {
                        if (this.IsHandleCreated)
                            this.Invoke(new MethodInvoker(() => this.UseWaitCursor = false));
                    }
                }
            });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
