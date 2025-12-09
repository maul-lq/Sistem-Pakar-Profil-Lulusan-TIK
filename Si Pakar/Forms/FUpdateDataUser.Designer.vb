<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FUpdateDataUser
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FUpdateDataUser))
        TextBoxNama = New TextBox()
        TextBoxEmail = New TextBox()
        ButtonSimpan = New Button()
        ButtonBatal = New Button()
        Label1 = New Label()
        Label2 = New Label()
        ErrorProviderDataUser = New ErrorProvider(components)
        CType(ErrorProviderDataUser, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TextBoxNama
        ' 
        TextBoxNama.Location = New Point(12, 32)
        TextBoxNama.Name = "TextBoxNama"
        TextBoxNama.Size = New Size(215, 27)
        TextBoxNama.TabIndex = 0
        ' 
        ' TextBoxEmail
        ' 
        TextBoxEmail.Location = New Point(12, 106)
        TextBoxEmail.Name = "TextBoxEmail"
        TextBoxEmail.Size = New Size(215, 27)
        TextBoxEmail.TabIndex = 1
        ' 
        ' ButtonSimpan
        ' 
        ButtonSimpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonSimpan.Location = New Point(147, 165)
        ButtonSimpan.Name = "ButtonSimpan"
        ButtonSimpan.Size = New Size(80, 35)
        ButtonSimpan.TabIndex = 2
        ButtonSimpan.Text = "Simpan"
        ButtonSimpan.UseVisualStyleBackColor = True
        ' 
        ' ButtonBatal
        ' 
        ButtonBatal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ButtonBatal.Location = New Point(12, 165)
        ButtonBatal.Name = "ButtonBatal"
        ButtonBatal.Size = New Size(80, 35)
        ButtonBatal.TabIndex = 3
        ButtonBatal.Text = "Batal"
        ButtonBatal.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(49, 20)
        Label1.TabIndex = 4
        Label1.Text = "Nama"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 83)
        Label2.Name = "Label2"
        Label2.Size = New Size(46, 20)
        Label2.TabIndex = 5
        Label2.Text = "Email"
        ' 
        ' ErrorProviderDataUser
        ' 
        ErrorProviderDataUser.ContainerControl = Me
        ' 
        ' FUpdateDataUser
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(257, 212)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(ButtonBatal)
        Controls.Add(ButtonSimpan)
        Controls.Add(TextBoxEmail)
        Controls.Add(TextBoxNama)
        Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 4, 3, 4)
        Name = "FUpdateDataUser"
        StartPosition = FormStartPosition.CenterParent
        Text = "Update Data"
        CType(ErrorProviderDataUser, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBoxNama As TextBox
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents ButtonSimpan As Button
    Friend WithEvents ButtonBatal As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ErrorProviderDataUser As ErrorProvider
End Class
