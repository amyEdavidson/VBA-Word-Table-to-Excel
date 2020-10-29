Sub ImportWordTable()
Dim wdDoc As Object
Dim wdFileName As Variant
Dim TableNo As Integer 'table number in Word
Dim iRow As Long 'row index in Excel
Dim iCol As Integer 'column index in Excel

wdFileName = Application.GetOpenFilename("Word files (*.doc),*.doc", , _
"Browse for file containing table to be imported")

If wdFileName = False Then Exit Sub '(user cancelled import file browser)

Set wdDoc = GetObject(wdFileName) 'open Word file

With wdDoc
    TableNo = wdDoc.tables.Count
    If TableNo = 0 Then
        MsgBox "This document contains no tables", _
        vbExclamation, "Import Word Table"
    ElseIf TableNo > 1 Then
        TableNo = InputBox("This Word document contains " & TableNo & " tables." & vbCrLf & _
        "Enter table number of table to import", "Import Word Table", "1")
    End If
        With .tables(TableNo)
        'copy cell contents from Word table cells to Excel cells
        For iRow = 1 To .Rows.Count
            For iCol = 1 To .Columns.Count
                Cells(iRow, iCol) = WorksheetFunction.Clean(.cell(iRow, iCol).Range.Text)
            Next iCol
        Next iRow
    End With
End With

Set wdDoc = Nothing

End Sub