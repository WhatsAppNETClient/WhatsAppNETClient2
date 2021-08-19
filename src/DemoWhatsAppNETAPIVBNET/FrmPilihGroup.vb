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

Public Class FrmPilihGroup

    Dim noUrut = 1
    Dim _listOfGroup As New List(Of Group)

    Private _group As Group
    Public ReadOnly Property Group() As Group
        Get
            Return IIf(lstGroup.SelectedIndex < 0, Nothing, _listOfGroup(lstGroup.SelectedIndex))
        End Get
    End Property

    Public Sub New(ByVal title As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = title
    End Sub

    Public Sub OnReceiveGroupsHandler(groups As IList(Of Group), ByVal sessionId As String)
        ' update UI dari thread yang berbeda
        lstGroup.Invoke(
            Sub()
                For Each group As Group In groups
                    If Not (group.id = "status@broadcast") Then

                        _listOfGroup.Add(group)

                        lstGroup.Items.Add(String.Format("{0}. {1} - {2}",
                            noUrut, group.id, group.name))

                        noUrut = noUrut + 1
                    End If
                Next
            End Sub
        )
    End Sub

    Private Sub btnPilih_Click(sender As Object, e As EventArgs) Handles btnPilih.Click
        Me.DialogResult = DialogResult.OK
    End Sub

End Class