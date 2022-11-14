Imports System.Data.SQLite
Imports System.Data
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.ComponentModel
Imports System.Reflection

Public Class Form1
    Dim lv As ListViewItem
    Dim selected As String

    Private Sub PopListView()

        ListView1.Clear()

        With ListView1
            .View = View.Details
            .GridLines = True
            .Columns.Add("Costumer ID", 60)
            .Columns.Add("Last Name", 130)
            .Columns.Add("First Name", 110)
            .Columns.Add("MI", 60)
            .Columns.Add("Gender", 130)
            .Columns.Add("Costumer", 130)
        End With

        openCon()
        sql = "Select * from tbl_costuinfo order by cosid"
        cmd = New SQLiteCommand(sql, cn)
        dr = cmd.ExecuteReader()

        Do While dr.Read() = True
            lv = New ListViewItem(dr("cosid").ToString)
            lv.SubItems.Add(dr("coslastname"))
            lv.SubItems.Add(dr("cosfirstname"))
            lv.SubItems.Add(dr("cosmi"))
            lv.SubItems.Add(dr("coscontact"))
            lv.SubItems.Add(dr("cosgender"))
            lv.SubItems.Add(dr("costumer"))
            ListView1.Items.Add(lv)

        Loop
        dr.Close()
        cn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopListView()
    End Sub

    Private Sub resetForm()
        txtid.Text = Nothing
        txtlastname.Text = Nothing
        txtfirstname.Text = Nothing
        txtmi.Text = Nothing
        txtcontact.Text = Nothing
        cmbCos.Text = Nothing
        cmbSex.Text = Nothing

        Me.txtlastname.Enabled = False
        Me.txtfirstname.Enabled = False
        Me.txtmi.Enabled = False
        Me.txtcontact.Enabled = False
        Me.cmbCos.Enabled = False
        Me.cmbSex.Enabled = False

        btnAdd.Text = "Add New"
        btnAdd.Enabled = True
        btnClose.Text = "Close"
        btnUpdate.Text = "Update"
        btnUpdate.Enabled = False
        btnDel.Text = "Delete"
        btnDel.ForeColor = Color.Red

        ListView1.Enabled = True
        ListView1.HideSelection = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ListView1.Enabled = False

        If btnAdd.Text = "Edit" Then
            btnAdd.Text = "Update"
            btnAdd.Enabled = False
        Else
            ListView1.HideSelection = True
            btnUpdate.Text = "Save"
            txtid.Text = ListView1.Items.Count + 1
        End If

        Me.txtlastname.Enabled = True
        Me.txtfirstname.Enabled = True
        Me.txtmi.Enabled = True
        Me.txtcontact.Enabled = True
        Me.cmbCos.Enabled = True
        Me.cmbSex.Enabled = True
        btnClose.Text = "Cancel"
        btnDel.Text = "Clear"
        btnDel.ForeColor = Color.Black
        btnAdd.Enabled = False
        btnUpdate.Enabled = True

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtid.Text = "" Or txtlastname.Text = "" Or txtfirstname.Text = "" Or txtmi.Text = "" Or txtcontact.Text = "" Or cmbSex.Text = "" Or cmbCos.Text = "" Then
            MsgBox("Please complete all data before saving it.", vbExclamation, Me.Text)

        Else
            If btnUpdate.Text = "Save" Then
                If MsgBox("Are you sure to save this record?", vbYesNo + vbQuestion, Me.Text) = vbYes Then
                    openCon()
                    sql = "INSERT INTO tbl_costuinfo (cosid, coslastname, cosfirstname, cosmi, coscontact, cosgender, costumer) VALUES ('" & Me.txtid.Text & "','" & Me.txtlastname.Text & "','" & Me.txtfirstname.Text & "','" & Me.txtmi.Text & "','" & Me.txtcontact.Text & "','" & Me.cmbSex.Text & "','" & Me.cmbCos.Text & "')"
                    cmd = New SQLiteCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record Saved!", 64, Me.Text)
                    PopListView()
                    resetForm()
                End If
            Else
                If MsgBox("Are you sure to save this record?", vbYesNo + vbQuestion, Me.Text) = vbYes Then
                    openCon()
                    sql = "UPDATE tbl_costuinfo SET coslastname = '" & txtlastname.Text & "', cosfirstname = '" & txtfirstname.Text & "', cosmi = '" & txtmi.Text & "', coscontact = '" & txtcontact.Text & "', cosgender = '" & cmbSex.Text & "', cosgender = '" & cmbCos.Text & "' where cosid = '" & selected & "'"
                    cmd = New SQLiteCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record Updated!", 64, Me.Text)

                    PopListView()
                    resetForm()
                End If
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Me.btnClose.Text = "Cancel" Then
            resetForm()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If btnDel.Text = "Delete" Then
            If MsgBox("Are you sure to delete this record?", vbYesNo + vbQuestion, Me.Text) = vbYes Then
                openCon()
                sql = "Delete from tbl_costuinfo where cosid = '" & selected & "'"
                cmd = New SQLiteCommand(sql, cn)
                cmd.ExecuteNonQuery()
                cn.Close()
                MsgBox("Record Deleted!", 64, Me.Text)
                PopListView()
                resetForm()
            End If
        Else
            txtlastname.Text = Nothing
            txtfirstname.Text = Nothing
            txtmi.Text = Nothing
            txtcontact.Text = Nothing
            cmbCos.Text = Nothing
            cmbSex.Text = Nothing
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        resetForm()
        Dim i As Integer
        For i = 0 To ListView1.SelectedItems.Count - 1

            selected = ListView1.SelectedItems(i).Text
            openCon()
            sql = "Select * from Tbl_costuinfo where cosid = '" & selected & "'"
            cmd = New SQLiteCommand(sql, cn)
            dr = cmd.ExecuteReader()

            dr.Read()

            txtid.Text = dr("cosid")
            txtid.Text = dr("coslastname")
            txtid.Text = dr("cosfirstname")
            txtid.Text = dr("cosmi")
            txtid.Text = dr("coscontact")
            txtid.Text = dr("cosgender")
            txtid.Text = dr("costumer")

            dr.Close()
            cn.Close()

        Next

        btnClose.Text = "Cancel"
        btnAdd.Text = "Edit"
        btnAdd.Enabled = True
        btnUpdate.Enabled = False
        btnDel.Enabled = True
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txtlastname.TextChanged, txtfirstname.TextChanged, txtmi.TextChanged, txtcontact.TextChanged, cmbSex.TextChanged, cmbCos.TextChanged
        If txtlastname.Text = "" And txtfirstname.Text = "" And txtmi.Text = "" And txtcontact.Text = "" And cmbSex.Text = "" And cmbCos.Text = "" Then
            btnDel.Enabled = False
        Else
            btnDel.Enabled = True
        End If

    End Sub
End Class