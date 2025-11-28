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
        GroupBox1 = New GroupBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        FlowLayoutPanelNomorSoal = New FlowLayoutPanel()
        Label2 = New Label()
        PanelWadah = New Panel()
        ButtonSubmit = New Button()
        PanelPertanyaann = New Panel()
        RadioButtonSangatSetuju = New RadioButton()
        RadioButton4 = New RadioButton()
        RadioButton3 = New RadioButton()
        RadioButton2 = New RadioButton()
        RadioButton1 = New RadioButton()
        LabelSoal = New Label()
        Button2 = New Button()
        Button1 = New Button()
        GroupBox1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        FlowLayoutPanelNomorSoal.SuspendLayout()
        PanelWadah.SuspendLayout()
        PanelPertanyaann.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TableLayoutPanel1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(864, 491)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Soft Skill"
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(FlowLayoutPanelNomorSoal, 3, 1)
        TableLayoutPanel1.Controls.Add(PanelWadah, 0, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(3, 23)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 83.3333359F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(858, 465)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' FlowLayoutPanelNomorSoal
        ' 
        FlowLayoutPanelNomorSoal.Controls.Add(Label2)
        FlowLayoutPanelNomorSoal.Location = New Point(3, 390)
        FlowLayoutPanelNomorSoal.Name = "FlowLayoutPanelNomorSoal"
        FlowLayoutPanelNomorSoal.RightToLeft = RightToLeft.No
        FlowLayoutPanelNomorSoal.Size = New Size(852, 72)
        FlowLayoutPanelNomorSoal.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(3, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(125, 20)
        Label2.TabIndex = 0
        Label2.Text = "[List Nomor Soal]"
        ' 
        ' PanelWadah
        ' 
        PanelWadah.Controls.Add(ButtonSubmit)
        PanelWadah.Controls.Add(PanelPertanyaann)
        PanelWadah.Controls.Add(Button2)
        PanelWadah.Controls.Add(Button1)
        PanelWadah.Dock = DockStyle.Fill
        PanelWadah.Location = New Point(3, 3)
        PanelWadah.Name = "PanelWadah"
        PanelWadah.Size = New Size(852, 381)
        PanelWadah.TabIndex = 1
        ' 
        ' ButtonSubmit
        ' 
        ButtonSubmit.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ButtonSubmit.Location = New Point(353, 344)
        ButtonSubmit.Name = "ButtonSubmit"
        ButtonSubmit.Size = New Size(115, 32)
        ButtonSubmit.TabIndex = 4
        ButtonSubmit.Text = "Submit"
        ButtonSubmit.UseVisualStyleBackColor = True
        ' 
        ' PanelPertanyaann
        ' 
        PanelPertanyaann.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelPertanyaann.Controls.Add(RadioButtonSangatSetuju)
        PanelPertanyaann.Controls.Add(RadioButton4)
        PanelPertanyaann.Controls.Add(RadioButton3)
        PanelPertanyaann.Controls.Add(RadioButton2)
        PanelPertanyaann.Controls.Add(RadioButton1)
        PanelPertanyaann.Controls.Add(LabelSoal)
        PanelPertanyaann.Location = New Point(0, 3)
        PanelPertanyaann.Name = "PanelPertanyaann"
        PanelPertanyaann.Size = New Size(849, 334)
        PanelPertanyaann.TabIndex = 3
        ' 
        ' RadioButtonSangatSetuju
        ' 
        RadioButtonSangatSetuju.Anchor = AnchorStyles.None
        RadioButtonSangatSetuju.AutoSize = True
        RadioButtonSangatSetuju.Location = New Point(632, 242)
        RadioButtonSangatSetuju.Name = "RadioButtonSangatSetuju"
        RadioButtonSangatSetuju.Size = New Size(118, 24)
        RadioButtonSangatSetuju.TabIndex = 5
        RadioButtonSangatSetuju.TabStop = True
        RadioButtonSangatSetuju.Text = "Sangat Setuju"
        RadioButtonSangatSetuju.UseVisualStyleBackColor = True
        ' 
        ' RadioButton4
        ' 
        RadioButton4.Anchor = AnchorStyles.None
        RadioButton4.AutoSize = True
        RadioButton4.Location = New Point(525, 242)
        RadioButton4.Name = "RadioButton4"
        RadioButton4.Size = New Size(68, 24)
        RadioButton4.TabIndex = 4
        RadioButton4.TabStop = True
        RadioButton4.Text = "Setuju"
        RadioButton4.UseVisualStyleBackColor = True
        ' 
        ' RadioButton3
        ' 
        RadioButton3.Anchor = AnchorStyles.None
        RadioButton3.AutoSize = True
        RadioButton3.Location = New Point(418, 242)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(68, 24)
        RadioButton3.TabIndex = 3
        RadioButton3.TabStop = True
        RadioButton3.Text = "Netral"
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.Anchor = AnchorStyles.None
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(271, 242)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(108, 24)
        RadioButton2.TabIndex = 2
        RadioButton2.TabStop = True
        RadioButton2.Text = "Tidak Setuju"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' RadioButton1
        ' 
        RadioButton1.Anchor = AnchorStyles.None
        RadioButton1.AutoSize = True
        RadioButton1.Location = New Point(74, 242)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(158, 24)
        RadioButton1.TabIndex = 1
        RadioButton1.TabStop = True
        RadioButton1.Text = "Sangat Tidak Setuju"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' LabelSoal
        ' 
        LabelSoal.Anchor = AnchorStyles.None
        LabelSoal.AutoSize = True
        LabelSoal.Location = New Point(120, 88)
        LabelSoal.Name = "LabelSoal"
        LabelSoal.Size = New Size(574, 40)
        LabelSoal.TabIndex = 0
        LabelSoal.Text = "Saya sangat menikmati tantangan yang melibatkan logika matematika hitungan atau " & vbCrLf & "memecahkan teka-teki algoritma yang rumit."
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button2.Location = New Point(3, 343)
        Button2.Name = "Button2"
        Button2.Size = New Size(344, 35)
        Button2.TabIndex = 2
        Button2.Text = "&<"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Location = New Point(474, 343)
        Button1.Name = "Button1"
        Button1.Size = New Size(372, 35)
        Button1.TabIndex = 1
        Button1.Text = "&>"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' FPertanyaan
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(864, 491)
        Controls.Add(GroupBox1)
        Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(3, 4, 3, 4)
        MinimumSize = New Size(880, 530)
        Name = "FPertanyaan"
        Text = "Si Sok Tau"
        GroupBox1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        FlowLayoutPanelNomorSoal.ResumeLayout(False)
        FlowLayoutPanelNomorSoal.PerformLayout()
        PanelWadah.ResumeLayout(False)
        PanelPertanyaann.ResumeLayout(False)
        PanelPertanyaann.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanelNomorSoal As FlowLayoutPanel
    Friend WithEvents PanelWadah As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents PanelPertanyaann As Panel
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents LabelSoal As Label
    Friend WithEvents RadioButtonSangatSetuju As RadioButton
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents ButtonSubmit As Button
    Friend WithEvents Label2 As Label
End Class
