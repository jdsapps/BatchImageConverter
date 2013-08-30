Imports System.Windows.Forms

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Form1.CheckBox4.Checked = False
        Me.Close()
    End Sub

    Private Sub Dialog1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = My.Settings.bgColor
        Me.ForeColor = My.Settings.txtColor
        RichTextBox1.BackColor = My.Settings.bgColor
        RichTextBox1.ForeColor = My.Settings.txtColor
    End Sub
End Class
