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
    public partial class FrmPilihGroup : Form
    {
        private int noUrut = 1;

        private IList<Group> _listOfGroup = new List<Group>();

        public Group Group
        {
            get
            {
                return lstGroup.SelectedIndex < 0 ? null : _listOfGroup[lstGroup.SelectedIndex];
            }
        }

        public FrmPilihGroup(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        public void OnReceiveGroupsHandler(IList<Group> groups, string sessionId)
        {
            // update UI dari thread yang berbeda
            lstGroup.Invoke(() =>
            {
                foreach (var group in groups)
                {
                    if (!(group.id == "status@broadcast"))
                    {
                        _listOfGroup.Add(group);

                        lstGroup.Items.Add(string.Format("{0}. {1} - {2}",
                        noUrut, group.id, group.name));

                        noUrut++;
                    }
                }
            });
        }

        private void btnPilih_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }        
    }
}
