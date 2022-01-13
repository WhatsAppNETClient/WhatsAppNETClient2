Module Helper

    Public Function ShowDialogOpen(ByVal title As String, Optional ByVal fileImageOnly As Boolean = False) As String

        Dim fileName As String = String.Empty

        Using dlgOpen As New OpenFileDialog

            If fileImageOnly Then
                dlgOpen.Filter = "File gambar (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png"
            Else
                dlgOpen.Filter = "File dokumen (*.*)|*.*"
            End If

            dlgOpen.Title = title

            Dim result = dlgOpen.ShowDialog()
            If (result = DialogResult.OK) Then fileName = dlgOpen.FileName

        End Using

        Return fileName
    End Function

    Public Function ShowDialogOpenFolder() As String

        Dim folderName As String = String.Empty

        Using dlgOpen As New FolderBrowserDialog

            Dim result = dlgOpen.ShowDialog()

            If result = DialogResult.OK AndAlso (Not String.IsNullOrWhiteSpace(dlgOpen.SelectedPath)) Then
                folderName = dlgOpen.SelectedPath
            End If
        End Using

        Return folderName

    End Function

End Module
