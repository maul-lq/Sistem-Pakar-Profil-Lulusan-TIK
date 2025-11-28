<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FStart
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
        Panel1 = New Panel()
        ButtonLakukanTes = New Button()
        Label2 = New Label()
        Label1 = New Label()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ButtonLakukanTes)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(859, 427)
        Panel1.TabIndex = 1
        ' 
        ' ButtonLakukanTes
        ' 
        ButtonLakukanTes.Anchor = AnchorStyles.None
        ButtonLakukanTes.Location = New Point(376, 252)
        ButtonLakukanTes.Name = "ButtonLakukanTes"
        ButtonLakukanTes.Size = New Size(107, 39)
        ButtonLakukanTes.TabIndex = 2
        ButtonLakukanTes.Text = "Lakukan &Tes"
        ButtonLakukanTes.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.None
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(146, 195)
        Label2.Name = "Label2"
        Label2.Size = New Size(566, 42)
        Label2.TabIndex = 1
        Label2.Text = "Hanya 10 menit untuk mendapatkan deskripsi yang sangat akurat tentang siapa " & vbCrLf & "kamu dan mengapa kamu melakukan sesuatu dengan cara yang kamu lakukan." & vbCrLf
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.None
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(356, 129)
        Label1.Name = "Label1"
        Label1.Size = New Size(147, 50)
        Label1.TabIndex = 0
        Label1.Text = "[Quote]"
        ' 
        ' FStart
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(859, 427)
        Controls.Add(Panel1)
        Name = "FStart"
        Text = "Si Sok Tau"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonLakukanTes As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
