Imports Si_Pakar.DataSetProgramTableAdapters
Imports Si_Pakar.DataSetProgram

Module Sesi_Ujian
    Function BuatSesiUjian(kodePengguna As Integer) As Boolean
        Dim adapter As New Sesi_UjianTableAdapter()
        Try
            adapter.Insert(kodePengguna, DateTime.Now, Nothing, "Umum", "{}", "{}")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub PerbaruiSeknarioUjian(idSesi As Integer, seknario As String)
        Dim adapter As New Sesi_UjianTableAdapter()
        Dim table = adapter.GetData()
        Dim row As Sesi_UjianRow = table.FindById_sesi(idSesi)
        If row IsNot Nothing Then
            row.scenario_used = seknario
            adapter.Update(row)
        End If
    End Sub

    Function GetIdSesiUjian(kodePengguna As Integer) As Integer
        Dim adapter As New Sesi_UjianTableAdapter()
        Dim sesiData = adapter.GetData().Where(Function(s) s.Id_user = kodePengguna).ToList()
        If sesiData.Count > 0 Then
            Return sesiData(0).Id_sesi
        Else
            Return -1
        End If
    End Function
End Module
