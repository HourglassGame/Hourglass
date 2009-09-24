<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SwitchForm
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
        Me.b_attach = New System.Windows.Forms.Button
        Me.t_attach = New System.Windows.Forms.TextBox
        Me.b_rotate = New System.Windows.Forms.Button
        Me.b_rotate2 = New System.Windows.Forms.Button
        Me.t_attach2 = New System.Windows.Forms.TextBox
        Me.b_attach2 = New System.Windows.Forms.Button
        Me.SwitchType = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.c_guy = New System.Windows.Forms.CheckBox
        Me.c_box = New System.Windows.Forms.CheckBox
        Me.c_plat = New System.Windows.Forms.CheckBox
        Me.c_wall = New System.Windows.Forms.CheckBox
        Me.c_visible = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'b_attach
        '
        Me.b_attach.Location = New System.Drawing.Point(12, 133)
        Me.b_attach.Name = "b_attach"
        Me.b_attach.Size = New System.Drawing.Size(103, 20)
        Me.b_attach.TabIndex = 0
        Me.b_attach.Text = "Attach"
        Me.b_attach.UseVisualStyleBackColor = True
        '
        't_attach
        '
        Me.t_attach.Location = New System.Drawing.Point(121, 134)
        Me.t_attach.Name = "t_attach"
        Me.t_attach.Size = New System.Drawing.Size(108, 20)
        Me.t_attach.TabIndex = 1
        '
        'b_rotate
        '
        Me.b_rotate.Location = New System.Drawing.Point(12, 159)
        Me.b_rotate.Name = "b_rotate"
        Me.b_rotate.Size = New System.Drawing.Size(103, 20)
        Me.b_rotate.TabIndex = 2
        Me.b_rotate.Text = "Rotate"
        Me.b_rotate.UseVisualStyleBackColor = True
        '
        'b_rotate2
        '
        Me.b_rotate2.Location = New System.Drawing.Point(12, 235)
        Me.b_rotate2.Name = "b_rotate2"
        Me.b_rotate2.Size = New System.Drawing.Size(103, 20)
        Me.b_rotate2.TabIndex = 5
        Me.b_rotate2.Text = "Rotate 2"
        Me.b_rotate2.UseVisualStyleBackColor = True
        '
        't_attach2
        '
        Me.t_attach2.Location = New System.Drawing.Point(121, 210)
        Me.t_attach2.Name = "t_attach2"
        Me.t_attach2.Size = New System.Drawing.Size(108, 20)
        Me.t_attach2.TabIndex = 4
        '
        'b_attach2
        '
        Me.b_attach2.Location = New System.Drawing.Point(12, 209)
        Me.b_attach2.Name = "b_attach2"
        Me.b_attach2.Size = New System.Drawing.Size(103, 20)
        Me.b_attach2.TabIndex = 3
        Me.b_attach2.Text = "Attach 2"
        Me.b_attach2.UseVisualStyleBackColor = True
        '
        'SwitchType
        '
        Me.SwitchType.FormattingEnabled = True
        Me.SwitchType.Items.AddRange(New Object() {"Temp Button", "Duel Toggle", "Laser"})
        Me.SwitchType.Location = New System.Drawing.Point(57, 11)
        Me.SwitchType.Name = "SwitchType"
        Me.SwitchType.Size = New System.Drawing.Size(121, 21)
        Me.SwitchType.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Type"
        '
        'c_guy
        '
        Me.c_guy.AutoSize = True
        Me.c_guy.Location = New System.Drawing.Point(12, 47)
        Me.c_guy.Name = "c_guy"
        Me.c_guy.Size = New System.Drawing.Size(89, 17)
        Me.c_guy.TabIndex = 10
        Me.c_guy.Text = "Collide Player"
        Me.c_guy.UseVisualStyleBackColor = True
        '
        'c_box
        '
        Me.c_box.AutoSize = True
        Me.c_box.Location = New System.Drawing.Point(12, 69)
        Me.c_box.Name = "c_box"
        Me.c_box.Size = New System.Drawing.Size(78, 17)
        Me.c_box.TabIndex = 11
        Me.c_box.Text = "Collide Box"
        Me.c_box.UseVisualStyleBackColor = True
        '
        'c_plat
        '
        Me.c_plat.AutoSize = True
        Me.c_plat.Location = New System.Drawing.Point(121, 47)
        Me.c_plat.Name = "c_plat"
        Me.c_plat.Size = New System.Drawing.Size(98, 17)
        Me.c_plat.TabIndex = 12
        Me.c_plat.Text = "Collide Platform"
        Me.c_plat.UseVisualStyleBackColor = True
        '
        'c_wall
        '
        Me.c_wall.AutoSize = True
        Me.c_wall.Location = New System.Drawing.Point(121, 70)
        Me.c_wall.Name = "c_wall"
        Me.c_wall.Size = New System.Drawing.Size(81, 17)
        Me.c_wall.TabIndex = 13
        Me.c_wall.Text = "Collide Wall"
        Me.c_wall.UseVisualStyleBackColor = True
        '
        'c_visible
        '
        Me.c_visible.AutoSize = True
        Me.c_visible.Location = New System.Drawing.Point(12, 101)
        Me.c_visible.Name = "c_visible"
        Me.c_visible.Size = New System.Drawing.Size(56, 17)
        Me.c_visible.TabIndex = 14
        Me.c_visible.Text = "Visible"
        Me.c_visible.UseVisualStyleBackColor = True
        '
        'SwitchForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(260, 292)
        Me.ControlBox = False
        Me.Controls.Add(Me.c_visible)
        Me.Controls.Add(Me.c_wall)
        Me.Controls.Add(Me.c_plat)
        Me.Controls.Add(Me.c_box)
        Me.Controls.Add(Me.c_guy)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SwitchType)
        Me.Controls.Add(Me.b_rotate2)
        Me.Controls.Add(Me.t_attach2)
        Me.Controls.Add(Me.b_attach2)
        Me.Controls.Add(Me.b_rotate)
        Me.Controls.Add(Me.t_attach)
        Me.Controls.Add(Me.b_attach)
        Me.Name = "SwitchForm"
        Me.Text = "Switch Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents b_attach As System.Windows.Forms.Button
    Friend WithEvents t_attach As System.Windows.Forms.TextBox
    Friend WithEvents b_rotate As System.Windows.Forms.Button
    Friend WithEvents b_rotate2 As System.Windows.Forms.Button
    Friend WithEvents t_attach2 As System.Windows.Forms.TextBox
    Friend WithEvents b_attach2 As System.Windows.Forms.Button
    Friend WithEvents SwitchType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents c_guy As System.Windows.Forms.CheckBox
    Friend WithEvents c_box As System.Windows.Forms.CheckBox
    Friend WithEvents c_plat As System.Windows.Forms.CheckBox
    Friend WithEvents c_wall As System.Windows.Forms.CheckBox
    Friend WithEvents c_visible As System.Windows.Forms.CheckBox
End Class
