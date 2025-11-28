Imports System.Drawing.Drawing2D

Public Class FMain
    Public startpage As FStart

    Private Sub FMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ButtonLanjut_Click(sender As Object, e As EventArgs) Handles ButtonLanjut.Click
        Dim qForm As New FPertanyaan
        qForm.startpage = startpage
        qForm.Show()
    End Sub
End Class