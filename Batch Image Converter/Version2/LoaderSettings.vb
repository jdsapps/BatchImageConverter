Public Class LoaderSettings
    Dim imgpath = My.Application.Info.DirectoryPath & "\Graphics\Animations\"
    Private Sub LoaderSettings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.ForeColor = My.Settings.txtColor
        Me.BackColor = My.Settings.bgColor
        ComboBox1.SelectedIndex = My.Settings.LoaderImg - 1
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles ComboBox1.SelectedValueChanged
        PictureBox1.Image = Image.FromFile(imgpath & ComboBox1.SelectedItem & ".gif")
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        My.Settings.LoaderImg = ComboBox1.SelectedItem
        My.Settings.Save()
        MsgBox(ComboBox1.SelectedItem & " Set!")
        Me.Close()
    End Sub
End Class