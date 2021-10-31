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

Public Class FrmContactOrGroup

    Dim noUrut = 1

    Public Sub New(ByVal title As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = title
    End Sub

    Private Sub FrmContactOrGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.UseWaitCursor = True
    End Sub

    Public Sub OnReceiveContactsHandler(contacts As IList(Of Contact), ByVal sessionId As String)
        ' update UI dari thread yang berbeda
        lstContactOrGroup.Invoke(
            Sub()
                For Each contact As Contact In contacts

                    If Not (contact.id = "status@broadcast") Then
                        lstContactOrGroup.Items.Add(String.Format("{0}. {1} - {2}, {3}",
                            noUrut, contact.id, contact.name, contact.pushname))

                        noUrut = noUrut + 1

                    Else ' status@broadcast -> dummy contact, penanda load data contact selesai
                        If Me.IsHandleCreated Then Me.Invoke(Sub() Me.UseWaitCursor = False)
                    End If
                Next
            End Sub
        )
    End Sub

    Public Sub OnReceiveGroupsHandler(groups As IList(Of Group), ByVal sessionId As String)
        ' update UI dari thread yang berbeda
        lstContactOrGroup.Invoke(
            Sub()
                For Each group As Group In groups
                    If Not (group.id = "status@broadcast") Then
                        lstContactOrGroup.Items.Add(String.Format("{0}. {1} - {2}",
                            noUrut, group.id, group.name))

                        noUrut = noUrut + 1

                        Dim noUrutMember = 1

                        For Each member As Contact In group.members
                            lstContactOrGroup.Items.Add(String.Format("---> {0}. {1} - {2}",
                                noUrutMember, member.id, member.name))
                            noUrutMember = noUrutMember + 1
                        Next

                    Else ' status@broadcast -> dummy group, penanda load data group selesai
                        If Me.IsHandleCreated Then Me.Invoke(Sub() Me.UseWaitCursor = False)
                    End If
                Next
            End Sub
        )
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class