<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        TextBoxNama = New TextBox()
        GroupBox1 = New GroupBox()
        TextBoxEmail = New TextBox()
        Label4 = New Label()
        ButtonLanjut = New Button()
        Label3 = New Label()
        ComboBoxProfesi = New ComboBox()
        Label2 = New Label()
        ComboBoxProgramStudi = New ComboBox()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(36, 47)
        Label1.Name = "Label1"
        Label1.Size = New Size(105, 20)
        Label1.TabIndex = 0
        Label1.Text = "Nama Panjang"
        ' 
        ' TextBoxNama
        ' 
        TextBoxNama.Location = New Point(153, 44)
        TextBoxNama.Margin = New Padding(3, 4, 3, 4)
        TextBoxNama.Name = "TextBoxNama"
        TextBoxNama.Size = New Size(177, 27)
        TextBoxNama.TabIndex = 1
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBoxEmail)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(ButtonLanjut)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(ComboBoxProfesi)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(ComboBoxProgramStudi)
        GroupBox1.Controls.Add(TextBoxNama)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(363, 277)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "Form Data Diri"
        ' 
        ' TextBoxEmail
        ' 
        TextBoxEmail.Location = New Point(153, 89)
        TextBoxEmail.Margin = New Padding(3, 4, 3, 4)
        TextBoxEmail.Name = "TextBoxEmail"
        TextBoxEmail.Size = New Size(177, 27)
        TextBoxEmail.TabIndex = 10
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(95, 93)
        Label4.Name = "Label4"
        Label4.Size = New Size(46, 20)
        Label4.TabIndex = 9
        Label4.Text = "Email"
        ' 
        ' ButtonLanjut
        ' 
        ButtonLanjut.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonLanjut.Location = New Point(241, 237)
        ButtonLanjut.Name = "ButtonLanjut"
        ButtonLanjut.Size = New Size(89, 28)
        ButtonLanjut.TabIndex = 8
        ButtonLanjut.Text = "Lanjut"
        ButtonLanjut.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(51, 180)
        Label3.Name = "Label3"
        Label3.Size = New Size(90, 20)
        Label3.TabIndex = 7
        Label3.Text = "Karir Impian"
        ' 
        ' ComboBoxProfesi
        ' 
        ComboBoxProfesi.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBoxProfesi.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboBoxProfesi.FormattingEnabled = True
        ComboBoxProfesi.Items.AddRange(New Object() {"Backend Developer", "Frontend Developer", "Mobile Developer", "Data Analyst / AI", "Network Engineer", "SysAdmin / Cloud Enginer", "IoT Engineer", "UI/UX Designer", "Graphic Designer", "Multimedia Artist (Video/Motion/3D)"})
        ComboBoxProfesi.Location = New Point(153, 180)
        ComboBoxProfesi.Name = "ComboBoxProfesi"
        ComboBoxProfesi.Size = New Size(177, 28)
        ComboBoxProfesi.TabIndex = 6
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(37, 134)
        Label2.Name = "Label2"
        Label2.Size = New Size(104, 20)
        Label2.TabIndex = 5
        Label2.Text = "Program Studi"
        ' 
        ' ComboBoxProgramStudi
        ' 
        ComboBoxProgramStudi.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBoxProgramStudi.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboBoxProgramStudi.FormattingEnabled = True
        ComboBoxProgramStudi.Location = New Point(153, 134)
        ComboBoxProgramStudi.Name = "ComboBoxProgramStudi"
        ComboBoxProgramStudi.Size = New Size(177, 28)
        ComboBoxProgramStudi.TabIndex = 4
        ' 
        ' FMain
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(363, 277)
        Controls.Add(GroupBox1)
        Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(3, 4, 3, 4)
        Name = "FMain"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Si Sok Tau"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxNama As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxProfesi As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBoxProgramStudi As ComboBox
    Friend WithEvents ButtonLanjut As Button
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents Label4 As Label
End Class
