Imports System.IO
Imports System.Net

Imports BICConv.Conv
Public Class Form1
    Dim convert As New BICConv.Conv
    Dim counts As Integer
    Dim count2 As Integer = 0
    Dim count3 As Integer = 0
   




    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)

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

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        ListBox1.Items.Clear()

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        PictureBox4.Visible = False
        PictureBox7.Visible = True
        PictureBox6.Visible = False
        ListBox1.SelectionMode = SelectionMode.MultiExtended


    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim newimage As Image
        Using str As Stream = File.OpenRead(ListBox1.SelectedItem)
            newimage = Image.FromStream(str)
            PictureBox1.Image = newimage
        End Using



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
        PictureBox5.Enabled = False
        PictureBox6.Enabled = False
        Me.BackColor = My.Settings.bgColor
        MenuStrip1.BackColor = My.Settings.bgColor
        ListBox1.BackColor = My.Settings.bgColor
        My.Settings.Registered = False
        My.Settings.Serial = Nothing

    End Sub


   

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            count2 = count2 + 1
            count3 = count3 + 1

            Conv.Label3.Text = count2 & "/" & Label8.Text
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
                If CheckBox5.Checked = True Then

                    For Each item In ListBox1.Items

                        IO.File.Delete(item.ToString)
                    Next
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
                convert.convert(Imaging.ImageFormat.Png, ListBox1.SelectedItem, TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                convert.convert(Imaging.ImageFormat.Png, ListBox1.SelectedItem, TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
        If ComboBox1.SelectedItem = ".bmp" Then
            If CheckBox1.Checked = True Then
                convert.convert(Imaging.ImageFormat.Bmp, ListBox1.SelectedItem, TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                convert.convert(Imaging.ImageFormat.Bmp, ListBox1.SelectedItem, TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
        If ComboBox1.SelectedItem = ".gif" Then
            If CheckBox1.Checked = True Then
                convert.convert(Imaging.ImageFormat.Gif, ListBox1.SelectedItem, TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                convert.convert(Imaging.ImageFormat.Gif, ListBox1.SelectedItem, TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If
        If ComboBox1.SelectedItem = ".tiff" Then
            If CheckBox1.Checked = True Then
                convert.convert(Imaging.ImageFormat.Tiff, ListBox1.SelectedItem, TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & TextBox1.Text & count2 & ComboBox1.SelectedItem)
            Else
                convert.convert(Imaging.ImageFormat.Tiff, ListBox1.SelectedItem, TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
                Conv.ListBox1.Items.Add(TextBox2.Text & "\" & Path.GetFileNameWithoutExtension(ListBox1.SelectedItem) & ComboBox1.SelectedItem)
            End If
        End If

    End Sub


    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub



    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs)
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



  


    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs)
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
        PictureBox6.Visible = True
        PictureBox7.Visible = False
        PictureBox4.Visible = True
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If Label8.Text = "0" Then
            Panel1.Enabled = False
            PictureBox5.Enabled = False
            PictureBox6.Enabled = False
            PictureBox4.Enabled = False
            PictureBox4.BorderStyle = BorderStyle.Fixed3D
            PictureBox5.BorderStyle = BorderStyle.Fixed3D
            PictureBox6.BorderStyle = BorderStyle.Fixed3D
            Label8.Text = ListBox1.Items.Count
        Else
            Panel1.Enabled = True
            PictureBox5.Enabled = True
            PictureBox6.Enabled = True
            PictureBox4.Enabled = True
            PictureBox4.BorderStyle = BorderStyle.None
            PictureBox5.BorderStyle = BorderStyle.None
            PictureBox6.BorderStyle = BorderStyle.None
            Label8.Text = ListBox1.Items.Count
        End If
    End Sub

    Private Sub UpdateToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles UpdateToolStripMenuItem1.Click
        Process.Start(My.Application.Info.DirectoryPath & "\Updater.exe")
       

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


    Private Sub PictureBox3_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox3.Click
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

    Private Sub PictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox2.Click
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

    Private Sub PictureBox5_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox5.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub PictureBox6_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox6.Click
        PictureBox4.Visible = False
        PictureBox7.Visible = True
        PictureBox6.Visible = False
        ListBox1.SelectionMode = SelectionMode.MultiExtended
    End Sub

    Private Sub PictureBox7_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox7.Click
        If ListBox1.Items.Count = ListBox1.Items.Count - 1 Then
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
        ListBox1.SelectedItem = Nothing
        PictureBox7.Visible = False
        PictureBox6.Visible = True
        PictureBox4.Visible = True
    End Sub

    Private Sub PictureBox4_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox4.Click
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

    Private Sub BackgroundWorker2_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        convcode()
    End Sub

    



    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MsgBox("In order for the change's to appear Batch Image Converter must be restarted")
        Process.Start(My.Application.Info.DirectoryPath & "\Batch Image Converter.exe")
        Me.Close()
    End Sub
End Class