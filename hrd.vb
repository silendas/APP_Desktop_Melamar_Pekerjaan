Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Public Class hrd
    Sub ClearText()
        txt_id.Text = ""
        txt_nama.Text = ""
        txt_cabang.Text = ""
        txt_phone.Text = ""
        txt_email.Text = ""
        txt_pass.Text = ""
        txt_cari.Text = ""
    End Sub
    Sub getAdmin()
        Try
            DataGridView1.Rows.Clear()
            Dim url As String = "http://localhost:1323/api/hrd/get"
            Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"

            Using response As System.Net.HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim responseText As String = reader.ReadToEnd()
                        Dim dict As List(Of Object) = JsonConvert.DeserializeObject(Of List(Of Object))(responseText)
                        Dim i As Integer
                        i = 1
                        For Each item As Object In dict
                            DataGridView1.Rows.Add(i, item("id_hrd").ToString(),
                                item("nama").ToString(),
                                item("cabang").ToString(),
                                 item("nohp").ToString(),
                                  item("email").ToString(),
                                item("password").ToString())
                            i = i + 1
                        Next
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Private Sub hrd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getAdmin()
    End Sub

    Private Sub btn_insert_Click(sender As Object, e As EventArgs) Handles btn_insert.Click
        Dim nama As String = txt_nama.Text
        Dim cabang As String = txt_cabang.Text
        Dim nohp As String = txt_phone.Text
        Dim email As String = txt_email.Text
        Dim password As String = txt_pass.Text
        'panggil data'
        SaveData(nama, cabang, nohp, email, password)
    End Sub
    Private Sub SaveData(nama As String, cabang As String, nohp As String, email As String, password As String)
        Try

            Dim requestData As New Dictionary(Of String, String)()
            requestData.Add("nama", nama)
            requestData.Add("cabang", cabang)
            requestData.Add("nohp", nohp)
            requestData.Add("email", email)
            requestData.Add("password", password)

            ' Ubah data permintaan menjadi JSON
            Dim jsonRequestData As String = JsonConvert.SerializeObject(requestData)

            'url API localhost'
            Dim apiUrl As String = "http://localhost:1323/api/hrd/post/" & nama & "/" & cabang & "/" & nohp & "/" & email & "/" & password

            ' coding Buat permintaan HTTP
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            ' Tulis data JSON ke tubuh permintaan
            Using writer As StreamWriter = New StreamWriter(request.GetRequestStream())
                writer.Write(jsonRequestData)
            End Using

            ' untuk mendapatkan respon dari API
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()

                    ' Proses respons sesuai kebutuhan
                    If response.StatusCode = HttpStatusCode.Created Then
                        MsgBox("Data berhasil disimpan", MsgBoxStyle.Information, "Sukses")
                    Else
                        MsgBox("Gagal menyimpan data", MsgBoxStyle.Exclamation, "Error")
                    End If
                End Using
            End Using
            getAdmin()
            ClearText()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Private Sub UpdateData(id As String, nama As String, cabang As String, nohp As String, email As String, password As String)
        Try
            Dim requestData As New Dictionary(Of String, String)()
            requestData.Add("id", id)
            requestData.Add("nama", nama)
            requestData.Add("cabang", cabang)
            requestData.Add("nohp", nohp)
            requestData.Add("email", email)
            requestData.Add("password", password)

            ' Url API localhost
            Dim jsonRequestData As String = JsonConvert.SerializeObject(requestData)

            ' Tentukan URL endpoint API
            Dim apiUrl As String = "http://localhost:1323/api/hrd/put/" & id & "/" & nama & "/" & cabang & "/" & nohp & "/" & email & "/" & password

            ' coding Buat permintaan HTTP
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "PUT"
            request.ContentType = "application/json"

            ' 
            Using writer As StreamWriter = New StreamWriter(request.GetRequestStream())
                writer.Write(jsonRequestData)
            End Using

            'coding untuk Dapatkan respons dari API
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()

                    ' Pesan berhasil/tidak
                    If response.StatusCode = HttpStatusCode.Created Then
                        MsgBox("Data berhasil diperbarui", MsgBoxStyle.Information, "Sukses")
                    Else
                        MsgBox("Gagal memperbarui data", MsgBoxStyle.Exclamation, "Error")
                    End If
                End Using
            End Using
            getAdmin()
            ClearText()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub
    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Dim id As String = txt_id.Text
        Dim nama As String = txt_nama.Text
        Dim cabang As String = txt_cabang.Text
        Dim nohp As String = txt_phone.Text
        Dim email As String = txt_email.Text
        Dim password As String = txt_pass.Text

        'panggil data'
        UpdateData(id, nama, cabang, nohp, email, password)
    End Sub

    Private Sub DeleteData(id As String)
        Try
            ' url API localhost
            Dim apiUrl As String = "http://localhost:1323/api/hrd/delete/" & id

            ' Buat permintaan HTTP
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "DELETE"

            ' mendapatkan respons dari API
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()

                    ' Proses respons sesuai kebutuhan
                    If response.StatusCode = HttpStatusCode.Created Then
                        MsgBox("Data berhasil dihapus", MsgBoxStyle.Information, "Sukses")
                    Else
                        MsgBox("Gagal menghapus data", MsgBoxStyle.Exclamation, "Error")
                    End If
                End Using
            End Using
            getAdmin()
            ClearText()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        Dim id As String = txt_id.Text
        DeleteData(id)
    End Sub

    Private Sub carinama()
        Try
            DataGridView1.Rows.Clear()
            Dim url As String = "http://localhost:1323/api/hrd/get/nama/" & txt_cari.Text.Trim
            Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"

            Using response As System.Net.HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim responseText As String = reader.ReadToEnd()
                        Dim dict As List(Of Object) = JsonConvert.DeserializeObject(Of List(Of Object))(responseText)

                        For Each item As Object In dict
                            DataGridView1.Rows.Add(item("id_hrd").ToString(),
                                item("nama").ToString(),
                                item("cabang").ToString(),
                                item("nohp").ToString(),
                                item("email").ToString(),
                                item("password").ToString())
                        Next
                    End Using
                End Using
            End Using
            ClearText()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Notifikasi")
        End Try
    End Sub

    Private Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        carinama()
    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Pastikan pengguna mengklik pada kolom NIK (misalnya, kolom dengan indeks 1)
        If e.ColumnIndex = 1 Then
            Dim nama As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
            carinama()
        End If
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        hrd_Load(e, e)
        MsgBox("FORM DI REFRESH", vbInformation, "")
        Refresh()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txt_id.Enabled = False
        txt_id.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txt_nama.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txt_cabang.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txt_phone.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        txt_email.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        txt_pass.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
    End Sub

    Private Sub btn_close_Click_1(sender As Object, e As EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub
End Class