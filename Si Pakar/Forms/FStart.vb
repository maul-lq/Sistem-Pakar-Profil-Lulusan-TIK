Public Class FStart
    Private Sub ButtonLakukanTes_Click(sender As Object, e As EventArgs) Handles ButtonLakukanTes.Click
        Dim fmain As New FMain
        fmain.startpage = Me
        fmain.Show()
        Me.Hide()
    End Sub
End Class