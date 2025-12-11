<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPertanyaan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPertanyaan))
        GroupBoxTopik = New GroupBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        FlowLayoutPanelNomorSoal = New FlowLayoutPanel()
        PanelWadah = New Panel()
        PanelPertanyaann = New Panel()
        RadioButton5 = New RadioButton()
        RadioButton4 = New RadioButton()
        RadioButton3 = New RadioButton()
        RadioButton2 = New RadioButton()
        RadioButton1 = New RadioButton()
        LabelSoal = New Label()
        Panel1 = New Panel()
        ButtonSubmit = New Button()
        Button1 = New Button()
        Button2 = New Button()
        GroupBoxTopik.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        PanelWadah.SuspendLayout()
        PanelPertanyaann.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBoxTopik
        ' 
        GroupBoxTopik.Controls.Add(TableLayoutPanel1)
        GroupBoxTopik.Dock = DockStyle.Fill
        GroupBoxTopik.Location = New Point(0, 0)
        GroupBoxTopik.Name = "GroupBoxTopik"
        GroupBoxTopik.Size = New Size(864, 491)
        GroupBoxTopik.TabIndex = 1
        GroupBoxTopik.TabStop = False
        GroupBoxTopik.Text = "Pertanyaan - [Topik]"
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(FlowLayoutPanelNomorSoal, 0, 1)
        TableLayoutPanel1.Controls.Add(PanelWadah, 0, 0)
        TableLayoutPanel1.Controls.Add(Panel1, 0, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(3, 23)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 88.01051F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 11.9894905F))
        TableLayoutPanel1.Size = New Size(858, 465)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' FlowLayoutPanelNomorSoal
        ' 
        FlowLayoutPanelNomorSoal.Anchor = AnchorStyles.None
        FlowLayoutPanelNomorSoal.AutoSize = True
        FlowLayoutPanelNomorSoal.Location = New Point(429, 406)
        FlowLayoutPanelNomorSoal.Name = "FlowLayoutPanelNomorSoal"
        FlowLayoutPanelNomorSoal.RightToLeft = RightToLeft.No
        FlowLayoutPanelNomorSoal.Size = New Size(0, 0)
        FlowLayoutPanelNomorSoal.TabIndex = 0
        ' 
        ' PanelWadah
        ' 
        PanelWadah.Controls.Add(PanelPertanyaann)
        PanelWadah.Dock = DockStyle.Fill
        PanelWadah.Location = New Point(3, 3)
        PanelWadah.Name = "PanelWadah"
        PanelWadah.Size = New Size(852, 397)
        PanelWadah.TabIndex = 1
        ' 
        ' PanelPertanyaann
        ' 
        PanelPertanyaann.Controls.Add(RadioButton5)
        PanelPertanyaann.Controls.Add(RadioButton4)
        PanelPertanyaann.Controls.Add(RadioButton3)
        PanelPertanyaann.Controls.Add(RadioButton2)
        PanelPertanyaann.Controls.Add(RadioButton1)
        PanelPertanyaann.Controls.Add(LabelSoal)
        PanelPertanyaann.Dock = DockStyle.Fill
        PanelPertanyaann.Location = New Point(0, 0)
        PanelPertanyaann.Name = "PanelPertanyaann"
        PanelPertanyaann.Size = New Size(852, 397)
        PanelPertanyaann.TabIndex = 3
        ' 
        ' RadioButton5
        ' 
        RadioButton5.Anchor = AnchorStyles.Bottom
        RadioButton5.AutoSize = True
        RadioButton5.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        RadioButton5.Location = New Point(667, 310)
        RadioButton5.Name = "RadioButton5"
        RadioButton5.Size = New Size(144, 25)
        RadioButton5.TabIndex = 5
        RadioButton5.TabStop = True
        RadioButton5.Text = "Sangat Mampu"
        RadioButton5.UseVisualStyleBackColor = True
        ' 
        ' RadioButton4
        ' 
        RadioButton4.Anchor = AnchorStyles.Bottom
        RadioButton4.AutoSize = True
        RadioButton4.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        RadioButton4.Location = New Point(559, 310)
        RadioButton4.Name = "RadioButton4"
        RadioButton4.Size = New Size(87, 25)
        RadioButton4.TabIndex = 4
        RadioButton4.TabStop = True
        RadioButton4.Text = "Mampu"
        RadioButton4.UseVisualStyleBackColor = True
        ' 
        ' RadioButton3
        ' 
        RadioButton3.Anchor = AnchorStyles.Bottom
        RadioButton3.AutoSize = True
        RadioButton3.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        RadioButton3.Location = New Point(398, 310)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(140, 25)
        RadioButton3.TabIndex = 3
        RadioButton3.TabStop = True
        RadioButton3.Text = "Cukup Mampu"
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.Anchor = AnchorStyles.Bottom
        RadioButton2.AutoSize = True
        RadioButton2.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        RadioButton2.Location = New Point(244, 310)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(133, 25)
        RadioButton2.TabIndex = 2
        RadioButton2.TabStop = True
        RadioButton2.Text = "Tidak Mampu"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' RadioButton1
        ' 
        RadioButton1.Anchor = AnchorStyles.Bottom
        RadioButton1.AutoSize = True
        RadioButton1.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        RadioButton1.Location = New Point(33, 310)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(190, 25)
        RadioButton1.TabIndex = 1
        RadioButton1.TabStop = True
        RadioButton1.Text = "Sangat Tidak Mampu"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' LabelSoal
        ' 
        LabelSoal.Anchor = AnchorStyles.None
        LabelSoal.AutoSize = True
        LabelSoal.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LabelSoal.Location = New Point(352, 165)
        LabelSoal.Name = "LabelSoal"
        LabelSoal.Size = New Size(148, 25)
        LabelSoal.TabIndex = 0
        LabelSoal.Text = "[Pertanyaannya]"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ButtonSubmit)
        Panel1.Controls.Add(Button1)
        Panel1.Controls.Add(Button2)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(3, 412)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(852, 45)
        Panel1.TabIndex = 2
        ' 
        ' ButtonSubmit
        ' 
        ButtonSubmit.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ButtonSubmit.Location = New Point(354, 5)
        ButtonSubmit.Name = "ButtonSubmit"
        ButtonSubmit.Size = New Size(115, 32)
        ButtonSubmit.TabIndex = 1
        ButtonSubmit.Text = "Submit"
        ButtonSubmit.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Location = New Point(473, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(372, 32)
        Button1.TabIndex = 3
        Button1.Text = "&>"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button2.Location = New Point(6, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(344, 35)
        Button2.TabIndex = 2
        Button2.Text = "&<"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' FPertanyaan
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(864, 491)
        Controls.Add(GroupBoxTopik)
        Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 4, 3, 4)
        MinimumSize = New Size(880, 530)
        Name = "FPertanyaan"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Si Sok Tau"
        GroupBoxTopik.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        PanelWadah.ResumeLayout(False)
        PanelPertanyaann.ResumeLayout(False)
        PanelPertanyaann.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents GroupBoxTopik As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanelNomorSoal As FlowLayoutPanel
    Friend WithEvents PanelWadah As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents PanelPertanyaann As Panel
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents LabelSoal As Label
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents ButtonSubmit As Button
    Friend WithEvents Panel1 As Panel
End Class
