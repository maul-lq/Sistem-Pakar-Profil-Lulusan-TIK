Imports Microsoft.Data.SqlClient

Module Koneksi
    Public Function GetConnection()
        Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rizlrad Fz\source\repos\Sistem Pakar Profil Lulusan TIK\Si Pakar\Database\Database Sistem Pakar.mdf;Integrated Security=True")
    End Function
End Module
