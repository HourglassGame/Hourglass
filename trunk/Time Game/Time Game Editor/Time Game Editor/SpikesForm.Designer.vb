<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpikeForm
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
        Me.b_rotate = New System.Windows.Forms.Button
        Me.t_attach = New System.Windows.Forms.TextBox
        Me.b_attach = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.t_size = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'b_rotate
        '
        Me.b_rotate.Location = New System.Drawing.Point(12, 69)
        Me.b_rotate.Name = "b_rotate"
        Me.b_rotate.Size = New System.Drawing.Size(103, 20)
        Me.b_rotate.TabIndex = 5
        Me.b_rotate.Text = "Rotate"
        Me.b_rotate.UseVisualStyleBackColor = True
        '
        't_attach
        '
        Me.t_attach.Location = New System.Drawing.Point(121, 44)
        Me.t_attach.Name = "t_attach"
        Me.t_attach.Size = New System.Drawing.Size(101, 20)
        Me.t_attach.TabIndex = 4
        '
        'b_attach
        '
        Me.b_attach.Location = New System.Drawing.Point(12, 43)
        Me.b_attach.Name = "b_attach"
        Me.b_attach.Size = New System.Drawing.Size(103, 20)
        Me.b_attach.TabIndex = 3
        Me.b_attach.Text = "Attach"
        Me.b_attach.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Size"
        '
        't_size
        '
        Me.t_size.Location = New System.Drawing.Point(50, 9)
        Me.t_size.Name = "t_size"
        Me.t_size.Size = New System.Drawing.Size(101, 20)
        Me.t_size.TabIndex = 19
        '
        'SpikeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(230, 101)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.t_size)
        Me.Controls.Add(Me.b_rotate)
        Me.Controls.Add(Me.t_attach)
        Me.Controls.Add(Me.b_attach)
        Me.Name = "SpikeForm"
        Me.Text = "Spike Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents b_rotate As System.Windows.Forms.Button
    Friend WithEvents t_attach As System.Windows.Forms.TextBox
    Friend WithEvents b_attach As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents t_size As System.Windows.Forms.TextBox
End Class
