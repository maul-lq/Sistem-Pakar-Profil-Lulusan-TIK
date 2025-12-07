Imports Microsoft.Data.SqlClient

Module Koneksi
    Public Function GetConnection()
        Return New SqlConnection("Data Source=RIZLRADFZ\SQLEXPRESS;Initial Catalog=Database Sistem Pakar;Integrated Security=True;Trust Server Certificate=True")
    End Function
End Module
