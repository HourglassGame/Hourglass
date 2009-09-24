<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogicForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.t_attach1 = New System.Windows.Forms.TextBox
        Me.b_attach1 = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.GateType = New System.Windows.Forms.ComboBox
        Me.t_attach2 = New System.Windows.Forms.TextBox
        Me.b_attach2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        't_attach1
        '
        Me.t_attach1.Location = New System.Drawing.Point(125, 64)
        Me.t_attach1.Name = "t_attach1"
        Me.t_attach1.Size = New System.Drawing.Size(108, 20)
        Me.t_attach1.TabIndex = 11
        '
        'b_attach1
        '
        Me.b_attach1.Location = New System.Drawing.Point(16, 63)
        Me.b_attach1.Name = "b_attach1"
        Me.b_attach1.Size = New System.Drawing.Size(103, 20)
        Me.b_attach1.TabIndex = 10
        Me.b_attach1.Text = "Attach 1"
        Me.b_attach1.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(12, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(97, 24)
        Me.label1.TabIndex = 9
        Me.label1.Text = "Gate Type"
        '
        'GateType
        '
        Me.GateType.FormattingEnabled = True
        Me.GateType.Items.AddRange(New Object() {"AND", "OR", "XOR"})
        Me.GateType.Location = New System.Drawing.Point(16, 36)
        Me.GateType.Name = "GateType"
        Me.GateType.Size = New System.Drawing.Size(174, 21)
        Me.GateType.TabIndex = 8
        '
        't_attach2
        '
        Me.t_attach2.Location = New System.Drawing.Point(125, 90)
        Me.t_attach2.Name = "t_attach2"
        Me.t_attach2.Size = New System.Drawing.Size(108, 20)
        Me.t_attach2.TabIndex = 13
        '
        'b_attach2
        '
        Me.b_attach2.Location = New System.Drawing.Point(16, 89)
        Me.b_attach2.Name = "b_attach2"
        Me.b_attach2.Size = New System.Drawing.Size(103, 20)
        Me.b_attach2.TabIndex = 12
        Me.b_attach2.Text = "Attach 2"
        Me.b_attach2.UseVisualStyleBackColor = True
        '
        'LogicForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(243, 126)
        Me.Controls.Add(Me.t_attach2)
        Me.Controls.Add(Me.b_attach2)
        Me.Controls.Add(Me.t_attach1)
        Me.Controls.Add(Me.b_attach1)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.GateType)
        Me.Name = "LogicForm"
        Me.Text = "LogicEditor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents t_attach1 As System.Windows.Forms.TextBox
    Friend WithEvents b_attach1 As System.Windows.Forms.Button
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents GateType As System.Windows.Forms.ComboBox
    Friend WithEvents t_attach2 As System.Windows.Forms.TextBox
    Friend WithEvents b_attach2 As System.Windows.Forms.Button
End Class
