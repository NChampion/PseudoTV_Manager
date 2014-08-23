Public Class Form8
    Private Sub Form3_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'Add the item name, then make it the selected option.
        'MsgBox("leaving")
        Form1.RefreshAllStudios()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim NetworkName = InputBox("Please Enter the Studio Name", "Studio Name")

        If NetworkName <> "" Then
            Dim AlreadyUsed As Boolean = False

            For x = 0 To ListBox1.Items.Count - 1

                If StrComp(ListBox1.Items(x).ToString, NetworkName, CompareMethod.Text) = 0 Then
                    AlreadyUsed = True
                End If
            Next
            If AlreadyUsed = False Then
                DbExecute("INSERT INTO studio (strStudio) VALUES ('" & NetworkName & "')")
                Form1.RefreshAllStudios()
            Else
                MsgBox("You already have a Studio labeled : " & NetworkName)
            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Remove the studio links from both the TV show table & the studiotvshowlink area.



        'Grab the ID 
        Dim SelectArray(0)
        SelectArray(0) = 0

        Dim ReturnArray() As String = DbReadRecord(Form1.VideoDatabaseLocation, "SELECT * FROM movie WHERE c18 = '" & ListBox1.Text.ToString() & "'", SelectArray)
        'Make sure there's not a null return for the genre items.
        If ReturnArray Is Nothing Then
        Else

            'Loop through each & remove the Network.
            For x = 0 To UBound(ReturnArray)
                Dim ShowID = ReturnArray(x)
                DbExecute("UPDATE movie SET c18 = '' WHERE idMovie = '" & ShowID & "'")
            Next


        End If

        'Now grab the Studio's ID
        Dim ReturnArray2() As String = DbReadRecord(Form1.VideoDatabaseLocation, "SELECT * FROM studio WHERE strStudio = '" & ListBox1.Text.ToString() & "'", SelectArray)
        If ReturnArray Is Nothing Then
        Else
            Dim StudioID = ReturnArray2(0)
            'Remove all links in the studiotvlink table
            DbExecute("DELETE FROM studiolinkmovie WHERE idStudio = '" & StudioID & "'")

            'Now remove the studio completely

        End If
        DbExecute("DELETE FROM studio WHERE strStudio = '" & ListBox1.Text.ToString() & "'")
        Form1.RefreshAllStudios()
    End Sub
End Class