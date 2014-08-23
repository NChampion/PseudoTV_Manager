Imports System
Imports System.IO

Public Class Form6

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()

        Dim Filename = OpenFileDialog1.FileName
        If OpenFileDialog1.FileName <> "OpenFileDialog1" Then
            TextBox1.Text = Filename
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()

        Dim Filename = OpenFileDialog1.FileName

        If OpenFileDialog1.FileName <> "OpenFileDialog1" Then
            TextBox2.Text = Filename
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim SettingsFile As String = Application.StartupPath() & "\" & "Settings.txt"

        'See if there's already a text file in place, if not then create one.

        If System.IO.File.Exists(SettingsFile) = False Then
            System.IO.File.Create(SettingsFile)
        End If

        'Verify that both files indeed exist at least
        If System.IO.File.Exists(TextBox1.Text) = True And System.IO.File.Exists(TextBox2.Text) = True Then

            If TestMYSQLite(TextBox1.Text) = True Then

                'Save them to the settings file
                Dim FilePaths As String = "0" & " | " & TextBox1.Text & " | " & TextBox2.Text
                SaveFile(SettingsFile, FilePaths)

                'Now, update the variables in the Main form with the proper paths
                Form1.DatabaseType = 0
                Form1.VideoDatabaseLocation = TextBox1.Text
                Form1.PseudoTvSettingsLocation = TextBox2.Text

                'Refresh everything
                Form1.RefreshALL()
                Form1.RefreshTVGuide()

                Me.Visible = False
                Form1.Focus()
            End If
        ElseIf TextBox3.Text <> "" And TextBox4.Text <> "" And TextBox6.Text <> "" And System.IO.File.Exists(TextBox2.Text) = True Then

            'server=localhost; user id=mike; password=12345; database=in_out

            Dim ConnectionString = "server=" & TextBox3.Text & "; user id=" & TextBox4.Text & "; password=" & TextBox5.Text & "; database=" & TextBox6.Text & "; port=" & TextBox7.Text

            If TestMYSQL(ConnectionString) = True Then

                Dim FilePaths As String = "1" & " | " & ConnectionString & " | " & TextBox2.Text
                SaveFile(SettingsFile, FilePaths)

                'Now, update the variables in the Main form with the proper paths
                Form1.DatabaseType = 1
                Form1.MySQLConnectionString = ConnectionString
                Form1.PseudoTvSettingsLocation = TextBox2.Text

                'Refresh everything
                Form1.RefreshALL()
                Form1.RefreshTVGuide()

                Me.Visible = False
                Form1.Focus()
            End If
        End If



    End Sub

    Private Sub Form6_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Form1.VideoDatabaseLocation <> "" Then
            TextBox1.Text = Form1.VideoDatabaseLocation
            TextBox2.Text = Form1.PseudoTvSettingsLocation
        End If

        If Form1.MySQLConnectionString <> "" Then
            Dim SplitString() = Split(Form1.MySQLConnectionString, ";")

            TextBox2.Text = Form1.PseudoTvSettingsLocation
            TextBox3.Text = Split(SplitString(0), "server=")(1)
            TextBox4.Text = Split(SplitString(1), "user id=")(1)
            TextBox5.Text = Split(SplitString(2), "password=")(1)
            TextBox6.Text = Split(SplitString(3), "database=")(1)
            TextBox7.Text = Split(SplitString(4), "port=")(1)
        End If

    End Sub
End Class