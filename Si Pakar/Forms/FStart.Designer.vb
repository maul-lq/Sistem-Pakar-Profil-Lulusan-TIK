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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FStart))
        Panel1 = New Panel()
        ButtonLakukanTes = New Button()
        LabelSubtitle = New Label()
        LabelQuote = New Label()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ButtonLakukanTes)
        Panel1.Controls.Add(LabelSubtitle)
        Panel1.Controls.Add(LabelQuote)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(859, 427)
        Panel1.TabIndex = 1
        ' 
        ' ButtonLakukanTes
        ' 
        ButtonLakukanTes.Anchor = AnchorStyles.None
        ButtonLakukanTes.BackColor = Color.FromArgb(CByte(253), CByte(206), CByte(223))
        ButtonLakukanTes.Location = New Point(376, 321)
        ButtonLakukanTes.Name = "ButtonLakukanTes"
        ButtonLakukanTes.Size = New Size(107, 39)
        ButtonLakukanTes.TabIndex = 2
        ButtonLakukanTes.Text = "Lakukan &Tes"
        ButtonLakukanTes.UseVisualStyleBackColor = False
        ' 
        ' LabelSubtitle
        ' 
        LabelSubtitle.Anchor = AnchorStyles.None
        LabelSubtitle.AutoSize = True
        LabelSubtitle.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LabelSubtitle.Location = New Point(393, 264)
        LabelSubtitle.Name = "LabelSubtitle"
        LabelSubtitle.Size = New Size(73, 21)
        LabelSubtitle.TabIndex = 1
        LabelSubtitle.Text = "[Subtitle]"
        ' 
        ' LabelQuote
        ' 
        LabelQuote.Anchor = AnchorStyles.None
        LabelQuote.AutoSize = True
        LabelQuote.Font = New Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LabelQuote.Location = New Point(356, 105)
        LabelQuote.Name = "LabelQuote"
        LabelQuote.Size = New Size(147, 50)
        LabelQuote.TabIndex = 0
        LabelQuote.Text = "[Quote]"
        ' 
        ' FStart
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(249), CByte(245), CByte(246))
        ClientSize = New Size(859, 427)
        Controls.Add(Panel1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "FStart"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Si Sok Tau"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonLakukanTes As Button
    Friend WithEvents LabelSubtitle As Label
    Friend WithEvents LabelQuote As Label
End Class
