Public Class FHasil
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FStart.Show()
        Hide()
    End Sub

    Private Sub FHasil_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class