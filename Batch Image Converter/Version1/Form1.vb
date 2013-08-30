Imports System.IO
Imports System.Net
Imports ICSharpCode.SharpZipLib.Zip.FastZip
Imports System.Threading
Imports System.Globalization
Imports BICConv.Conv
Public Class Form1
    Dim convert As New BICConv.Conv
    Dim counts As Integer
    Dim count2 As Integer = 0
    Dim count3 As Integer = 0
    Dim WithEvents dwncheck As New WebClient
    Dim WithEvents dwnupdate As New WebClient
    Dim zip As New ICSharpCode.SharpZipLib.Zip.FastZip
    Dim versiontxt As String = "https://dl.dropboxusercontent.com/s/7pqocx638x4f5jn/Version.txt"
    Dim updatezip As String = "https://dl.dropboxusercontent.com/s/te7i68h20ztyn01/BIC.zip"



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Try
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                For Each file In OpenFileDialog1.FileNames
                    For Each item In ListBox1.Items
                        If item = file Then
                            MsgBox(file.ToString & " Exists", MsgBoxStyle.Critical)
                            GoTo Done
                        End If

                    Next

                    ListBox1.Items.Add(file)

                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
Done:

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Button4.Visible = False
        Button7.Visible = True
        Button3.Visible = False
        ListBox1.SelectionMode = SelectionMode.MultiExtended


    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged



    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.Visible = True
            Label3.Visible = True
        End If
        If CheckBox1.Checked = False Then
            TextBox1.Visible = False
            Label3.Visible = False
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        TextBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        Button2.Enabled = False
        Button3.Enabled = False
        Me.BackColor = My.Settings.bgColor
        MenuStrip1.BackColor = My.Settings.bgColor
        ListBox1.BackColor = My.Settings.bgColor
        My.Settings.Registered = False
        My.Settings.Serial = Nothing

    End Sub


    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Conv.ProgressBar1.Maximum = Label8.Text
        Conv.Label1.Visible = False
        Me.Visible = False
        Panel1.Enabled = False
        Timer1.Enabled = True
        ListBox1.SelectedIndex = 0
        counts = 0
        Conv.PictureBox1.Visible = True
        Conv.PictureBox2.Visible = True
        count3 = 0
        Conv.ShowDialog()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            count2 = count2 + 1
            count3 = count3 + 1
            Conv.TextBox1.Text = count3
            Conv.TextBox2.Text = Label8.Text
            Conv.ProgressBar1.Value = Conv.ProgressBar1.Value + 1
            If counts = ListBox1.Items.Count - 1 Then
                convcode()
                If CheckBox2.Checked = True Then

                    Timer1.Enabled = False
                    Process.Start("explorer.exe", TextBox2.Text)
                End If


                Timer1.Enabled = False
                Panel1.Enabled = True
                count2 = 0
                counts = 0
                count3 = 0
                Conv.PictureBox1.Visible = False
                Conv.Label1.Visible = True
                Conv.PictureBox2.Visible = False
                Conv.Label1.Text = "Conversion Finished"
                Conv.Cancel_Button.Text = "OK"
                If Conv.WindowState = FormWindowState.Minimized Then
                    Conv.NotifyIcon1.BalloonTipText = "Conversion Finished"
                    Conv.NotifyIcon1.ShowBalloonTip(300)
                    Conv.WindowState = FormWindowState.Normal
                    Conv.NotifyIcon1.Visible = False
                    Conv.ShowInTaskbar = True
                    Conv.Show()
                End If

            Else
                convcode()
                ListBox1.SelectedIndex = counts + 1
                counts = ListBox1.SelectedIndex
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click

        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = FolderBrowserDialog1.SelectedPath

        End If
    End Sub
    Public Sub convcode()
        If ComboBox1.SelectedItem = ".jpg" Then
            If CheckBox1.Checked = True Then
                convert.convert(Imaging.ImageFormat.Jpeg, ListBox1.SelectedItem, TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                convert.convert(Imaging.ImageFormat.Jpeg, ListBox1.SelectedItem, TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
        If ComboBox1.SelectedItem = ".png" Then
            If CheckBox1.Checked = True Then
                Dim savepng = Image.FromFile(ListBox1.SelectedItem)
                savepng.Save(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem, Imaging.ImageFormat.Png)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                Dim savepng = Image.FromFile(ListBox1.SelectedItem)
                savepng.Save(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem, Imaging.ImageFormat.Png)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
        If ComboBox1.SelectedItem = ".bmp" Then
            If CheckBox1.Checked = True Then
                Dim savebmp = Image.FromFile(ListBox1.SelectedItem)
                savebmp.Save(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem, Imaging.ImageFormat.Bmp)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                Dim savebmp = Image.FromFile(ListBox1.SelectedItem)
                savebmp.Save(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem, Imaging.ImageFormat.Bmp)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
        If ComboBox1.SelectedItem = ".gif" Then
            If CheckBox1.Checked = True Then
                Dim savegif = Image.FromFile(ListBox1.SelectedItem)
                savegif.Save(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem, Imaging.ImageFormat.Gif)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                Dim savegif = Image.FromFile(ListBox1.SelectedItem)
                savegif.Save(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem, Imaging.ImageFormat.Gif)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
    End Sub


    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub



    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Try
            Dim imgbrs As New FolderBrowserDialog
            imgbrs.ShowNewFolderButton = False
            imgbrs.RootFolder = Environment.SpecialFolder.UserProfile
            If imgbrs.ShowDialog = Windows.Forms.DialogResult.OK Then
                If CheckBox3.Checked = True Then
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.jpg")
                        ListBox1.Items.Add(Image)
                    Next
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.png")
                        ListBox1.Items.Add(Image)
                    Next
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.gif")
                        ListBox1.Items.Add(Image)
                    Next
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.bmp")
                        ListBox1.Items.Add(Image)
                    Next
                End If
                If CheckBox3.Checked = False Then
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
                        ListBox1.Items.Add(Image)
                    Next
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.png")
                        ListBox1.Items.Add(Image)
                    Next
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.gif")
                        ListBox1.Items.Add(Image)
                    Next
                    For Each Image In My.Computer.FileSystem.GetFiles(imgbrs.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.bmp")
                        ListBox1.Items.Add(Image)
                    Next
                End If

                CheckBox3.Checked = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub




    Private Sub BackgroundToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BackgroundToolStripMenuItem.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.bgColor = ColorDialog1.Color
            Me.BackColor = ColorDialog1.Color
            MenuStrip1.BackColor = ColorDialog1.Color
            ListBox1.BackColor = ColorDialog1.Color
            My.Settings.Save()
        End If
    End Sub

    Private Sub TextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TextToolStripMenuItem.Click
        If ColorDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.txtColor = ColorDialog2.Color
            Me.ForeColor = ColorDialog2.Color
            MenuStrip1.ForeColor = ColorDialog2.Color
            ListBox1.ForeColor = ColorDialog2.Color
            My.Settings.Save()
        End If
    End Sub

    Private Sub ResetBackgroundToDefaultToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ResetBackgroundToDefaultToolStripMenuItem.Click
        My.Settings.bgColor = SystemColors.Control
        Me.BackColor = SystemColors.Control
        MenuStrip1.BackColor = SystemColors.Control
        ListBox1.BackColor = SystemColors.Control
        My.Settings.Save()

    End Sub

    Private Sub ResetTextToDefaultToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ResetTextToDefaultToolStripMenuItem.Click
        My.Settings.txtColor = Color.Black
        Me.ForeColor = Color.Black
        MenuStrip1.ForeColor = Color.Black
        ListBox1.ForeColor = Color.Black
        My.Settings.Save()
    End Sub




    Private Sub LoaderAnimationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LoaderAnimationToolStripMenuItem.Click
        LoaderSettings.ShowDialog()
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub



    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\Version.txt") > My.Application.Info.Version.ToString Then
            If MsgBox("An Update Is Available. Would you like to download the update now?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                dwnupdate.DownloadFileAsync(New Uri(updatezip), My.Application.Info.DirectoryPath & "\BIC.zip")
                zip.ExtractZip(My.Application.Info.DirectoryPath & "\BIC.zip", My.Application.Info.DirectoryPath, "*")
            End If
        Else

        End If
        If IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\Version.txt") <= My.Application.Info.Version.ToString Then
            MsgBox(My.Application.Info.Title & " Up To Date!", MsgBoxStyle.Information)
        End If
        IO.File.Delete(My.Application.Info.DirectoryPath & "\Version.txt")

    End Sub



    Private Sub dwncheck_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles dwncheck.DownloadFileCompleted
        BackgroundWorker1.RunWorkerAsync()
    End Sub


    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        If ListBox1.Items.Count = 1 Then
            For i As Integer = 0 To ListBox1.SelectedIndices.Count - 1
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            Next
            ListBox1.SelectedItem = Nothing

        Else
            For i As Integer = 0 To ListBox1.SelectedIndices.Count - 1
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            Next
            ListBox1.SelectedItem = Nothing


            Label8.Text = ListBox1.Items.Count
        End If
        ListBox1.SelectionMode = SelectionMode.One
        Button3.Visible = True
        Button7.Visible = False
        Button4.Visible = True
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If Label8.Text = "0" Then
            Panel1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Label8.Text = ListBox1.Items.Count
        Else
            Panel1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Label8.Text = ListBox1.Items.Count
        End If
    End Sub

    Private Sub UpdateToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles UpdateToolStripMenuItem1.Click
        dwncheck.DownloadFileAsync(New Uri(versiontxt), My.Application.Info.DirectoryPath & "\Version.txt")
    End Sub


    Private Sub BatchImageConverterToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles BatchImageConverterToolStripMenuItem1.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub CreditsToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CreditsToolStripMenuItem1.Click
        Credits.ShowDialog()
    End Sub

    Private Sub ViewHelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewHelpToolStripMenuItem.Click
        Process.Start(My.Application.Info.DirectoryPath & "\Batch Image Converter.chm")
    End Sub


End Class
