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
        TextBox1 = New TextBox()
        GroupBox1 = New GroupBox()
        ButtonLanjut = New Button()
        Label3 = New Label()
        ComboBox2 = New ComboBox()
        Label2 = New Label()
        ComboBox1 = New ComboBox()
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
        ' TextBox1
        ' 
        TextBox1.Location = New Point(153, 44)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(177, 27)
        TextBox1.TabIndex = 1
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(ButtonLanjut)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(ComboBox2)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(ComboBox1)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(363, 226)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "Form Data Diri"
        ' 
        ' ButtonLanjut
        ' 
        ButtonLanjut.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonLanjut.Location = New Point(241, 186)
        ButtonLanjut.Name = "ButtonLanjut"
        ButtonLanjut.Size = New Size(89, 28)
        ButtonLanjut.TabIndex = 8
        ButtonLanjut.Text = "Lanjut"
        ButtonLanjut.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(51, 142)
        Label3.Name = "Label3"
        Label3.Size = New Size(90, 20)
        Label3.TabIndex = 7
        Label3.Text = "Karir Impian"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Backend Developer", "Frontend Developer", "Mobile Developer", "Data Analyst / AI", "Network Engineer", "SysAdmin / Cloud Enginer", "IoT Engineer", "UI/UX Designer", "Graphic Designer", "Multimedia Artist (Video/Motion/3D)"})
        ComboBox2.Location = New Point(153, 139)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(177, 28)
        ComboBox2.TabIndex = 6
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(37, 93)
        Label2.Name = "Label2"
        Label2.Size = New Size(104, 20)
        Label2.TabIndex = 5
        Label2.Text = "Program Studi"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Teknik Informatika (TI)", "Teknik Multimedia Desain (TMD)", "Teknik Multimedia Jaringan (TMJ)"})
        ComboBox1.Location = New Point(153, 90)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(177, 28)
        ComboBox1.TabIndex = 4
        ' 
        ' FMain
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(363, 226)
        Controls.Add(GroupBox1)
        Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(3, 4, 3, 4)
        Name = "FMain"
        Text = "Si Sok Tau"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ButtonLanjut As Button
End Class
