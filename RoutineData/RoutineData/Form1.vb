Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Static Dim c As Integer = 0
        c += 1
        Dim li As New ListViewItem
        li.Text = InputBox("Enter Professor Name", "Professor", "N. K. Jaiswal")
        li.SubItems.Add(c)
        ListView1.Items.Add(li)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Static Dim c As Integer = 0
        c += 1
        Dim li As New ListViewItem
        li.Text = InputBox("Enter Course/Subject Name", "Subject", "Data Structure")
        li.SubItems.Add(c)
        ListView2.Items.Add(li)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim r As New Room
        r.ShowDialog()
        Dim li As New ListViewItem
        li.Text = r.TextBox1.Text
        li.SubItems.Add(r.TextBox2.Text)
        li.SubItems.Add(IIf(r.CheckBox1.Checked = True, "Yes", "No"))
        ListView3.Items.Add(li)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Static Dim c As Integer = 0
        c += 1
        Dim li As New ListViewItem
        li.Text = c
        li.SubItems.Add(InputBox("Enter Group Name", "Name", "BTECH_CSE_Yr4_GR3"))
        li.SubItems.Add(InputBox("Entre Group Size", "Size", "60"))
        ListView4.Items.Add(li)
    End Sub

    Private Sub ListView5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView5.SelectedIndexChanged

    End Sub

    Private Sub ListView4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView4.SelectedIndexChanged

    End Sub

    Private Sub ListView3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView3.SelectedIndexChanged

    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Function getProfessorId(ByVal p As String) As Integer
        Dim l As ListViewItem
        For Each l In ListView1.Items
            If l.Text = p Then
                Return l.SubItems(1).Text
            End If
        Next
        Return -1
    End Function
    Public Function getGroupId(ByVal g As String) As Integer
        Dim l As ListViewItem
        For Each l In ListView4.Items
            If l.SubItems(1).Text = g Then
                Return l.Text
            End If
        Next
        Return -1
    End Function
    Public Function getCourseId(ByVal g As String) As Integer
        Dim l As ListViewItem
        For Each l In ListView2.Items
            If l.Text = g Then
                Return l.SubItems(1).Text
            End If
        Next
        Return -1
    End Function
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim c As New clas()
        Dim li As ListViewItem
        For Each li In ListView1.Items
            c.ComboBox1.Items.Add(li.Text)
        Next
        For Each li In ListView2.Items
            c.ComboBox2.Items.Add(li.Text)
        Next
        For Each li In ListView4.Items
            c.ListBox1.Items.Add(li.SubItems(1).Text)
        Next

        c.ShowDialog()

        Dim item As New ListViewItem
        item.Text = c.ComboBox1.SelectedItem.ToString
        item.SubItems.Add(getProfessorId(item.Text))
        item.SubItems.Add(c.NumericUpDown1.Value)
        item.SubItems.Add(IIf(c.CheckBox1.Checked = True, "Yes", "No"))
        Dim grp As String = ""
        Dim lb As String
        For Each lb In c.ListBox2.Items
            grp &= getGroupId(lb) & ","
        Next
        item.SubItems.Add(grp)
        item.SubItems.Add(c.ComboBox2.SelectedItem.ToString)
        item.SubItems.Add(getCourseId(c.ComboBox2.SelectedItem.ToString))
        ListView5.Items.Add(item)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim str As String = ""
        Dim l As ListViewItem
        For Each l In ListView1.Items
            str &= "#prof" & vbNewLine & "  id = " & l.SubItems(1).Text & vbNewLine & "  name = " & l.Text & vbNewLine & "#end" & vbNewLine & vbNewLine
        Next
        For Each l In ListView2.Items
            str &= "#course" & vbNewLine & "  id = " & l.SubItems(1).Text & vbNewLine & "  name = " & l.Text & vbNewLine & "#end" & vbNewLine & vbNewLine
        Next
        For Each l In ListView3.Items
            str &= "#room" & vbNewLine & "  name = " & l.Text & vbNewLine & "  lab = " & IIf(l.SubItems(2).Text = "Yes", "true", "false") & vbNewLine & "   size = " & l.SubItems(1).Text & vbNewLine & "#end" & vbNewLine & vbNewLine
        Next
        For Each l In ListView4.Items
            str &= "#group" & vbNewLine & "  id = " & l.Text & vbNewLine & "  name = " & l.SubItems(1).Text & vbNewLine & "   size = " & l.SubItems(2).Text & vbNewLine & "#end" & vbNewLine & vbNewLine
        Next
        For Each l In ListView5.Items
            str &= "#class" & vbNewLine & "  professor = " & l.SubItems(1).Text & vbNewLine & "  course = " & l.SubItems(6).Text & vbNewLine & "   duration = " & l.SubItems(2).Text & vbNewLine
            Dim g As String
            For Each g In l.SubItems(4).Text.Split(",")
                If Trim(g) <> "" Then
                    str &= "    group = " & g & vbNewLine
                End If
            Next
            str &= "    lab = " & IIf(l.SubItems(3).Text = "Yes", "true", "false") & vbNewLine & "#end" & vbNewLine & vbNewLine
        Next
        IO.File.WriteAllText(SaveFileDialog1.FileName, str)
        MessageBox.Show("Saved")
    End Sub
End Class
