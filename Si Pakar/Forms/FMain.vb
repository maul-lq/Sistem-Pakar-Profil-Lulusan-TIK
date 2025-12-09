Public Class FMain
    Private ErrorProviderMain As New ErrorProvider()

    Private Sub FMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'hapus data combobox
        ComboBoxProgramStudi.Items.Clear()
        ComboBoxProfesi.Items.Clear()

        'setup error provider
        ErrorProviderMain.ContainerControl = Me

        'isi data combobox dari database
        For Each prodi As String In GetProfesiList()
            ComboBoxProfesi.Items.Add(prodi)
        Next
        For Each prodi As String In GetProdiList()
            ComboBoxProgramStudi.Items.Add(prodi)
        Next

        ' initial validation handlers
        AddHandler TextBoxNama.TextChanged, AddressOf Inputs_Changed
        AddHandler TextBoxEmail.TextChanged, AddressOf Inputs_Changed
        AddHandler ComboBoxProgramStudi.SelectedIndexChanged, AddressOf Inputs_Changed

        ValidateInputs()
    End Sub

    Private Sub Inputs_Changed(sender As Object, e As EventArgs)
        ValidateInputs()
    End Sub

    Private Function ValidateInputs() As Boolean
        Dim isValid As Boolean = True

        ' clear previous errors
        ErrorProviderMain.SetError(TextBoxNama, "")
        ErrorProviderMain.SetError(TextBoxEmail, "")
        ErrorProviderMain.SetError(ComboBoxProgramStudi, "")

        Dim name = TextBoxNama.Text.Trim()
        Dim email = TextBoxEmail.Text.Trim()

        If String.IsNullOrEmpty(name) Then
            ErrorProviderMain.SetError(TextBoxNama, "Nama tidak boleh kosong")
            isValid = False
        End If

        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(email, pattern) Then
            ErrorProviderMain.SetError(TextBoxEmail, "Email tidak valid (format: user@domain.tld)")
            isValid = False
        End If

        If ComboBoxProgramStudi.SelectedItem Is Nothing OrElse String.IsNullOrEmpty(ComboBoxProgramStudi.SelectedItem.ToString().Trim()) Then
            ErrorProviderMain.SetError(ComboBoxProgramStudi, "Pilih program studi")
            isValid = False
        End If

        ButtonLanjut.Enabled = isValid
        Return isValid
    End Function

    Private Sub ButtonLanjut_Click(sender As Object, e As EventArgs) Handles ButtonLanjut.Click
        If Not ValidateInputs() Then
            Return
        End If

        Dim nama = TextBoxNama.Text.Trim()
        Dim email = TextBoxEmail.Text.Trim()

        Dim prodiText As String = If(ComboBoxProgramStudi.SelectedItem IsNot Nothing,
                                     ComboBoxProgramStudi.SelectedItem.ToString(), "")
        Dim profesiText As String = If(ComboBoxProfesi.SelectedItem IsNot Nothing,
                                      ComboBoxProfesi.SelectedItem.ToString(), "")

        Dim prodiKode = If(String.IsNullOrEmpty(prodiText), -1, GetKodeProdi(prodiText))
        Dim rumpunKode = If(String.IsNullOrEmpty(profesiText), -1, GetKodeRumpun(profesiText))

        Dim id = DaftarPengguna(nama, email, prodiKode, profesiText, rumpunKode)
        If id <> -1 Then
            BuatSesiUjian(id)
        Else
            Return
        End If

        Dim qForm As New FPertanyaan
        qForm.phase = 1
        qForm.sessionId = GetIdSesiUjian(id)
        qForm.Show()
        Hide()
    End Sub

    Private Sub FMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
End Class