Public Class Form4

    Private Sub Form4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ComboBox1.Items.Clear()
        For x = 0 To Form1.TVGuideList.Items.Count - 1
            ComboBox1.Items.Add(Form1.TVGuideList.Items(x).SubItems(0).Text)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim Errornum As Boolean = False

        If IsNumeric(TextBox1.Text) = False Then
            Errornum = True
            MsgBox("Please enter a valid minimum interleave count")
        End If

        If IsNumeric(TextBox2.Text) = False Then
            Errornum = True
            MsgBox("Please enter a valid minimum interleave count")
        End If

        If IsNumeric(TextBox3.Text) = False Then
            Errornum = True
            MsgBox("Please enter a valid starting episode number")
        End If

        If ComboBox1.SelectedIndex = -1 Then
            Errornum = True
            MsgBox("Please select a valid channel")
        End If

        If Errornum = False Then
            Dim str(6) As String

            str(0) = ComboBox1.Text
            str(1) = TextBox1.Text
            str(2) = TextBox2.Text
            str(3) = TextBox3.Text

            Dim itm As ListViewItem
            itm = New ListViewItem(str)
            'Add to list
            Form1.InterleavedList.Items.Add(itm)

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.Text = ""
            Me.Visible = False
        End If
    End Sub
End Class