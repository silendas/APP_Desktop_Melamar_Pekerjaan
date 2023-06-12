Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Public Class Riwayat
    Sub ClearText()
        txt_search.Text = ""
    End Sub
    Private Sub Riwayat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            DataGridView1.Rows.Clear()
            Dim url As String = "http://localhost:1323/api/melamar/get"
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
                            DataGridView1.Rows.Add(i, item("id_melamar").ToString(),
                                item("nik").ToString(),
                                item("nama").ToString(),
                                item("posisi").ToString(),
                                item("cabang").ToString(),
                                item("kualifikasi").ToString(),
                                item("nilai_tst").ToString(),
                                item("tgl_melamar").ToString())
                            i = i + 1
                        Next

                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
    Private Sub carinik()
        Try
            DataGridView1.Rows.Clear()
            Dim url As String = "http://localhost:1323/api/melamar/get/" & txt_search.Text.Trim
            Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"

            Using response As System.Net.HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim responseText As String = reader.ReadToEnd()
                        Dim dict As List(Of Object) = JsonConvert.DeserializeObject(Of List(Of Object))(responseText)

                        For Each item As Object In dict
                            DataGridView1.Rows.Add(item("id_melamar").ToString(),
                                item("nik").ToString(),
                                item("nama").ToString(),
                                item("posisi").ToString(),
                                item("cabang").ToString(),
                                item("kualifikasi").ToString(),
                                item("nilai_tst").ToString(),
                                item("tgl_melamar").ToString())
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
        carinik()
    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
    Private Sub btn_close_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        Riwayat_Load(e, e)
        MsgBox("FORM DI REFRESH", vbInformation, "")
        Refresh()
    End Sub

    Private Sub btn_close_Click_1(sender As Object, e As EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub
End Class