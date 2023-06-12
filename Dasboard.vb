Public Class Dasboard
    Sub defaultShow()
        hrd.Hide()
        pelamar.Hide()
        admin.Hide()
        Riwayat.Hide()

    End Sub


    Private Sub btn_hrd_Click(sender As Object, e As EventArgs) Handles btn_hrd.Click
        defaultShow()
        hrd.Show()
        hrd.MdiParent = Me
    End Sub

    Private Sub btn_pelamar_Click(sender As Object, e As EventArgs) Handles btn_pelamar.Click
        defaultShow()
        pelamar.Show()
        pelamar.MdiParent = Me
    End Sub

    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        stime.Text = "Time :" & Format(Now, "H:mm:ss")
    End Sub

    Private Sub Dasboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stdate.Text = "Date : " & Format(Now, "dd/MM/yyy")
    End Sub

    Private Sub btn_admin_Click(sender As Object, e As EventArgs) Handles btn_admin.Click
        defaultShow()
        admin.Show()
        admin.MdiParent = Me
    End Sub

    Private Sub btn_riwayat_Click(sender As Object, e As EventArgs) Handles btn_riwayat.Click
        defaultShow()
        Riwayat.Show()
        Riwayat.MdiParent = Me
    End Sub
End Class