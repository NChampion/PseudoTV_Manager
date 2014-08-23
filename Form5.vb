Public Class Form5

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Form1.TVGuideList.SelectedItems.Count > 0 Then
            Dim Days As String = ""
            Dim ErrorNum As Boolean = False

            If ComboBox1.SelectedIndex >= 0 Then

            Else
                MsgBox("Please choose a channel")
                ErrorNum = True
            End If

            'Convert the checked days into the UMTWHFS format.
            If CheckedListBox1.CheckedItems.Count > 0 Then
                For x = 0 To CheckedListBox1.CheckedItems.Count - 1
                    Dim DayName = CheckedListBox1.CheckedItems(x).ToString
                    Dim DayLetter = ""

                    If DayName = "Sunday" Then
                        DayLetter = "U"
                    ElseIf DayName = "Monday" Then
                        DayLetter = "M"
                    ElseIf DayName = "Tuesday" Then
                        DayLetter = "T"
                    ElseIf DayName = "Wednesday" Then
                        DayLetter = "W"
                    ElseIf DayName = "Thursday" Then
                        DayLetter = "H"
                    ElseIf DayName = "Friday" Then
                        DayLetter = "F"
                    ElseIf DayName = "Saturday" Then
                        DayLetter = "S"
                    End If

                    Days = Days & DayLetter

                Next
            Else
                ErrorNum = True
                MsgBox("Please enter at least one day")
            End If
            If IsNumeric(TextBox1.Text) Then
                If TextBox1.Text.Length > 1 Then
                    If TextBox1.Text.Substring(0, 1) <> 1 And TextBox1.Text.Substring(0, 1) <> 0 Then
                        MsgBox("Sorry, an invalid time was entered prior to : ")
                        ErrorNum = True
                    End If
                ElseIf TextBox1.Text.Length = 1 Then
                    TextBox1.Text = "0" & TextBox1.Text
                End If
            Else
                MsgBox("Please enter a valid number before : ")
                ErrorNum = True
            End If

            If IsNumeric(TextBox2.Text) Then
                If TextBox2.Text.Length = 1 Then
                    MsgBox("Incorrect time after :")
                    ErrorNum = True
                ElseIf TextBox2.Text < 0 Or TextBox2.Text > 59 Or TextBox2.Text = "" Then
                    MsgBox("Incorrect time after : it must be 1 - 59")
                    ErrorNum = True
                End If
            Else
                MsgBox("Please enter a valid number after : ")
                ErrorNum = True
            End If

            If IsNumeric(TextBox3.Text) Then

            Else
                MsgBox("Please enter a the number of episodes")
                ErrorNum = True
            End If

            Dim str(3) As String

            str(0) = ComboBox1.Text 'Channel #.
            str(1) = Days
            str(2) = TextBox1.Text & ":" & TextBox2.Text
            str(3) = TextBox3.Text

            If ErrorNum = False Then
                Dim itm As ListViewItem
                itm = New ListViewItem(str)
                'Add to list
                Form1.SchedulingList.Items.Add(itm)

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                ComboBox1.SelectedIndex = -1

                Me.Visible = False
            End If
        End If
    End Sub

    Private Sub Form5_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Load channel list
        ComboBox1.Items.Clear()
        For x = 0 To Form1.TVGuideList.Items.Count - 1
            ComboBox1.Items.Add(Form1.TVGuideList.Items(x).SubItems(0).Text)
        Next
    End Sub
End Class