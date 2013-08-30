Imports System.Windows.Forms.Form
Imports System.IO

Public Class Conv
    Dim WithEvents icod As New NotifyIcon
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If Cancel_Button.Text = "Cancel" Then
            If Form1.CheckBox4.Checked = True Then
                Form1.Timer1.Enabled = False
                For Each item In ListBox1.Items
                    IO.File.Delete(item)
                Next
                ListBox1.Items.Clear()
                Me.Close()
                Form1.Visible = True
                MsgBox("Conversion Canceled", MsgBoxStyle.Information)
            Else
                Form1.Timer1.Enabled = False


                Me.Close()
                Form1.Visible = True
                MsgBox("Conversion Canceled", MsgBoxStyle.Information)
            End If
            
        End If
        If Cancel_Button.Text = "OK" Then
            
            Me.Close()
            Cancel_Button.Text = "Cancel"
            Form1.Visible = True
            
        End If
        Form1.Panel1.Enabled = True


    End Sub

    Private Sub Conv_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Label1.Visible = False
        PictureBox2.Visible = True

    End Sub

    Private Sub Conv_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = My.Settings.bgColor
        Me.ForeColor = My.Settings.txtColor
        PictureBox1.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\Graphics\Animations\" & My.Settings.LoaderImg & ".gif")
        PictureBox2.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\Graphics\Animations\image_497768.gif")
        NotifyIcon1.Text = "Converting"
        NotifyIcon1.Icon = Me.Icon
    End Sub


    
   

    

    
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Conv_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
           NotifyIcon1.Visible = True
            Me.ShowInTaskbar = False
            NotifyIcon1.BalloonTipText = "Batch Image Converter has been minimized to the tray. To restore just double click this icon."
            NotifyIcon1.ShowBalloonTip(300)
        End If
    End Sub

    
   
    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
        Me.ShowInTaskbar = True
        Me.Show()
    End Sub
End Class
