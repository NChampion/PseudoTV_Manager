Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If GenreList.SelectedIndices.Count > 0 Then

            Dim inlist As Boolean = False

            For x = 0 To Form1.ListTVGenres.Items.Count - 1
                If StrComp(Form1.ListTVGenres.Items(x).ToString, GenreList.Items(GenreList.SelectedIndices(0)).SubItems(0).Text) = 0 Then
                    inlist = True
                End If
            Next

            If inlist = False Then
                Form1.ListTVGenres.Items.Add(GenreList.Items(GenreList.SelectedIndices(0)).SubItems(0).Text)
            Else
                MsgBox("There's already a genre for this show named: " & GenreList.Items(GenreList.SelectedIndices(0)).SubItems(0).Text)
            End If
            'Add link to genre & show
            'Add it to the TVshow itself.
            'Dim GenreID As String = GenreList.Items(GenreList.SelectedIndices(0)).SubItems(1).Text
            'Dim ShowID As String = Form1.TVShowList.Items(Form1.TVShowList.SelectedIndices(0)).SubItems(2).Text


            'Insert the Link from the genre to the TV Show.
            'Form1.DbExecute("INSERT INTO genrelinktvshow (idGenre, idShow) VALUES ('" & GenreID & "','" & ShowID & "')")


            'Maybe remove
            'Insert the TV Genres into the TVShow table.
            'Dim AllGenres As String = Form1.ConvertTvGenres
            'Form1.DbExecute("UPDATE tvshow SET c08 = '" & AllGenres & "' WHERE idShow = '" & ShowID & "'")
            'Form1.RefreshGenres()
            'Me.Close()
        End If
    End Sub

    Public Sub Form2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GenreList.Items.Clear()
        GenreList.Columns.Add("Genre", 200, HorizontalAlignment.Left)
        GenreList.Columns.Add("ID", 0, HorizontalAlignment.Left)


        'Set an array with the columns you want returned
        Dim SelectArray(1)
        SelectArray(0) = 1
        SelectArray(1) = 0

        'Shoot it over to the ReadRecord sub, 
        Dim ReturnArray() As String = DbReadRecord(Form1.VideoDatabaseLocation, "SELECT * FROM genre", SelectArray)

        'Now, read the output of the array.
        'Loop through each of the Array items.
        For x = 0 To ReturnArray.Count - 1

            'Split them by ~'s.  This is how we seperate the rows in the single-element.
            Dim str() As String = Split(ReturnArray(x), "~")

            'Now take that split string and make it an item.
            Dim itm As ListViewItem
            itm = New ListViewItem(str)

            'Add the item to the TV genre list.
            GenreList.Items.Add(itm)

        Next

        GenreList.ListViewItemSorter = New clsListviewSorter(0, SortOrder.Ascending)
        GenreList.Sort()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim NewGenre = InputBox("New Genre Name", "New Genre Name")

        If NewGenre <> "" Then

            Dim AlreadyUsed As Boolean = False

            For x = 0 To GenreList.Items.Count - 1
                If StrComp(GenreList.Items(x).Text, NewGenre, CompareMethod.Text) = 0 Then
                    AlreadyUsed = True
                End If
            Next


            If AlreadyUsed = False Then
                DbExecute("INSERT INTO genre (strGenre) VALUES ('" & NewGenre & "')")
            Else
                MsgBox("You already have a genre labeled : " & NewGenre)
            End If
        End If

        Form2_Load(Nothing, Nothing)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If GenreList.SelectedItems.Count > 0 Then
            'Check the ID of the genre.

            'These are the columns we need back.  Just the ID
            Dim SelectArray(0)
            SelectArray(0) = 0

            'Grab the GenreID and store it in GenreID
            Dim ReturnArray() As String = DbReadRecord(Form1.VideoDatabaseLocation, "SELECT * FROM genre WHERE strGenre = '" & GenreList.SelectedItems(0).Text & "'", SelectArray)
            Dim GenreID = ReturnArray(0)

            'Remove it from the Genrelink table
            DbExecute("DELETE FROM genrelinktvshow WHERE idGenre = '" & GenreID & "'")
            'DbExecute("DELETE FROM genremovie WHERE idGenre = '" & GenreID & "'")

            'Remove it from the Genres table
            DbExecute("DELETE FROM genre WHERE idGenre = '" & GenreID & "'")

            'Now grab the TV table & remove the genre there. 

            Dim SelectArray2(1)
            SelectArray2(0) = 0
            SelectArray2(1) = 9

            ReturnArray = DbReadRecord(Form1.VideoDatabaseLocation, "SELECT * FROM tvshow WHERE c08 LIKE '%" & GenreList.SelectedItems(0).Text & "%'", SelectArray2)
            'Make sure there's not a null return for the genre items.
            If ReturnArray Is Nothing Then
            Else
                For X = 0 To ReturnArray.Count - 1
                    'Loop through all the returned shows

                    Dim AllGenres = Split(Split(ReturnArray(X), "~")(1), " / ")
                    Dim TVShowID = Split(ReturnArray(X), "~")(0)
                    Dim NewGenres() As String = Nothing
                    Dim NewGenresNum As Integer = 0


                    'Loop through all genres
                    For y = 0 To UBound(AllGenres)
                        'Don't add the genre if it matches
                        If StrComp(AllGenres(y), GenreList.SelectedItems(0).Text, CompareMethod.Text) <> 0 Then
                            ReDim Preserve NewGenres(NewGenresNum)
                            NewGenres(NewGenresNum) = AllGenres(y)
                            NewGenresNum = NewGenresNum + 1
                        End If
                    Next

                    'Now, add all of the genres back into the properly formatted: genre1 / genre2 / etc.
                    Dim ProperFormedGenres As String = ""

                    For y = 0 To UBound(NewGenres)
                        If y = 0 Then
                            ProperFormedGenres = NewGenres(y)
                        Else
                            ProperFormedGenres = ProperFormedGenres & " / " & NewGenres(y)
                        End If
                    Next


                    'Now update the actual Database.
                    DbExecute("UPDATE tvshow SET c08 = '" & ProperFormedGenres & "' WHERE idShow = '" & TVShowID & "'")


                    Form1.RefreshALL()
                Next
            End If
            MsgBox("The genre " & GenreList.SelectedItems(0).Text & " has also been removed from all tv shows")
            Form2_Load(Nothing, Nothing)
        End If
    End Sub
End Class