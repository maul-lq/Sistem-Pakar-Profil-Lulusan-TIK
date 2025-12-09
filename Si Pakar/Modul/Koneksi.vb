Imports Microsoft.Data.SqlClient

Module Koneksi
    Public Function GetConnection()
#If Release Then
        Dim path = AppDomain.CurrentDomain.BaseDirectory
        Dim dbPAth = "Database\Database Sistem Pakar.mdf"
        Dim fullPath As String = IO.Path.Combine(path, dbPAth)
        Return New SqlConnection($"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fullPath};Integrated Security=True")
#Else
        Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rizlrad Fz\source\repos\Sistem Pakar Profil Lulusan TIK\Si Pakar\Database\Database Sistem Pakar.mdf;Integrated Security=True")
#End If
    End Function
End Module
