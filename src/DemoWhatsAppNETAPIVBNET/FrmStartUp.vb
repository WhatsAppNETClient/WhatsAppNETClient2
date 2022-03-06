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

Imports System.IO

Public Class FrmStartUp

    Public Sub OnScanMeHandler(ByVal qrcodePath As String, ByVal sessionId As String)

        Try

            If File.Exists(qrcodePath) Then

                Dim qrCode As Image

                Using originalImage As New Bitmap(qrcodePath)
                    qrCode = New Bitmap(originalImage)
                End Using

                ' update UI dari thread yang berbeda
                picQRCode.Invoke(
                    Sub()
                        picQRCode.Visible = True
                        picQRCode.Image = qrCode
                    End Sub
                )
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub OnStartupHandler(ByVal message As String, ByVal sessionId As String)

        If message.IndexOf("Ready") >= 0 OrElse message.IndexOf("Failure") >= 0 _
                    OrElse message.IndexOf("Timeout") >= 0 OrElse message.IndexOf("ERR_NAME") >= 0 _
                    OrElse message.IndexOf("ERR_CONNECTION") >= 0 Then

            If Me.IsHandleCreated Then Me.Invoke(Sub() Me.Close())

        Else
            ' update UI dari thread yang berbeda
            lstLog.Invoke(
                Sub()
                    lstLog.Items.Add(message)
                    lstLog.SelectedIndex = lstLog.Items.Count - 1
                End Sub
            )
        End If

    End Sub

End Class