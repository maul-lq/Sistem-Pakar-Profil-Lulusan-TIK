Imports Microsoft.Data.SqlClient

' Model untuk menyimpan data pertanyaan
Public Class PertanyaanModel
    Public Property IdPertanyaan As String
    Public Property TeksPertanyaan As String
    Public Property KodeRumpun As String
    Public Property ExpertWeight As Double
    Public Property JawabanUser As Double

    Public Sub New()
        JawabanUser = -1.0 ' Default: belum dijawab
    End Sub
End Class
