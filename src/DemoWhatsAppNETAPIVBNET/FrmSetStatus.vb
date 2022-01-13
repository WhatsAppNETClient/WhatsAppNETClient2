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

Public Class FrmSetStatus

    Dim wa As IWhatsAppNETAPI

    Public Sub New(ByVal title As String, ByVal wa As IWhatsAppNETAPI)

        ' This call is required by the designer.
        InitializeComponent()
        Me.wa = wa

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = title
    End Sub

    Private Sub btnSetStatus_Click(sender As Object, e As EventArgs) Handles btnSetStatus.Click
        Dim status As StatusArgs

        If String.IsNullOrEmpty(txtStatus.Text) Then

            MessageBox.Show("Status belum diisi", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtStatus.Focus()

            Return
        End If

        If chkGambar.Checked Then
            If String.IsNullOrEmpty(txtFileGambar.Text) Then
                status = New StatusArgs(txtStatus.Text)
            Else
                status = New StatusArgs(txtStatus.Text, MsgArgsType.Image, txtFileGambar.Text)
            End If

        ElseIf chkUrl.Checked Then
            status = New StatusArgs(txtStatus.Text, MsgArgsType.Url, txtUrl.Text)
        Else
            status = New StatusArgs(txtStatus.Text)
        End If

        wa.SetStatus(status)
        Me.Close()
    End Sub

    Private Sub btnCariGambar_Click(sender As Object, e As EventArgs) Handles btnCariGambar.Click
        Dim fileName = ShowDialogOpen("Lokasi file gambar", True)

        If Not String.IsNullOrEmpty(fileName) Then txtFileGambar.Text = fileName
    End Sub

    Private Sub chkGambar_CheckedChanged(sender As Object, e As EventArgs) Handles chkGambar.CheckedChanged
        btnCariGambar.Enabled = chkGambar.Checked

        If chkGambar.Checked Then
            chkUrl.Checked = False
        Else
            txtFileGambar.Clear()
        End If
    End Sub

    Private Sub chkUrl_CheckedChanged(sender As Object, e As EventArgs) Handles chkUrl.CheckedChanged
        If chkUrl.Checked Then
            chkGambar.Checked = False
        End If
    End Sub
End Class