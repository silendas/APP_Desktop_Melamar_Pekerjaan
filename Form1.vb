Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization

Public Class Form1

    Private Sub txt_show_CheckedChanged(sender As Object, e As EventArgs) Handles txt_show.CheckedChanged
        If txt_show.Checked = True Then
            txt_pass.PasswordChar = ""
        Else
            txt_pass.PasswordChar = "●"
        End If
    End Sub

    Public Sub Login()
        Try
            Dim url As String = "http://localhost:1323/api/admin/login/" & txt_username.Text & "/" & txt_pass.Text
            Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"

            Using response As System.Net.HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim responseText As String = reader.ReadToEnd()
                        Dim dict As List(Of Object) = JsonConvert.DeserializeObject(Of List(Of Object))(responseText)

                        If dict Is Nothing Then
                            MessageBox.Show("Login Failed")
                        Else
                            Dim loginSuccess As Boolean = False
                            For Each item As Object In dict
                                If txt_username.Text = item("username").ToString AndAlso txt_pass.Text = item("password").ToString Then
                                    admin.Label2.Text = item("nama")
                                    hrd.Label2.Text = item("nama")
                                    Riwayat.Label2.Text = item("nama")
                                    pelamar.Label2.Text = item("nama")
                                    loginSuccess = True
                                    Exit For
                                End If
                            Next

                            If loginSuccess Then
                                MessageBox.Show("Login Success")
                                Dasboard.ShowDialog()
                                Me.Hide()
                            Else
                                MessageBox.Show("Login Failed")
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Notification")
        End Try
    End Sub
    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Login()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class



