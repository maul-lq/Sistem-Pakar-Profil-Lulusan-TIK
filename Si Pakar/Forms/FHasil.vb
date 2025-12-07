Public Class FHasil
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonKembaliKeAwal.Click
        FStart.Show()
        Hide()
    End Sub

    Private Sub FHasil_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub FHasil_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class