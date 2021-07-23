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
        public FrmContactOrGroup(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        public void OnReceiveContactsHandler(IList<Contact> contacts)
        {
            // update UI dari thread yang berbeda
            lstContactOrGroup.Invoke(() =>
            {
                foreach (var contact in contacts)
                {
                    lstContactOrGroup.Items.Add(string.Format("{0}. {1} - {2}",
                        lstContactOrGroup.Items.Count + 1, contact.id, contact.name));
                }                
            });
        }

        public void OnReceiveGroupsHandler(IList<Group> groups)
        {
            // update UI dari thread yang berbeda
            lstContactOrGroup.Invoke(() =>
            {
                var noUrut = 1;

                foreach (var group in groups)
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
            });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
