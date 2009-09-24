<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PickupForm
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
        Me.PickupType = New System.Windows.Forms.ComboBox
        Me.label1 = New System.Windows.Forms.Label
        Me.t_attach = New System.Windows.Forms.TextBox
        Me.b_attach = New System.Windows.Forms.Button
        Me.t_type2 = New System.Windows.Forms.TextBox
        Me.l_type2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PickupType
        '
        Me.PickupType.FormattingEnabled = True
        Me.PickupType.Items.AddRange(New Object() {"Time Jump Charge", "Time Gun Charge", "Reverse Time Charge"})
        Me.PickupType.Location = New System.Drawing.Point(12, 36)
        Me.PickupType.Name = "PickupType"
        Me.PickupType.Size = New System.Drawing.Size(174, 21)
        Me.PickupType.TabIndex = 3
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(12, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(115, 24)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Pickup Type"
        '
        't_attach
        '
        Me.t_attach.Location = New System.Drawing.Point(121, 64)
        Me.t_attach.Name = "t_attach"
        Me.t_attach.Size = New System.Drawing.Size(108, 20)
        Me.t_attach.TabIndex = 6
        '
        'b_attach
        '
        Me.b_attach.Location = New System.Drawing.Point(12, 63)
        Me.b_attach.Name = "b_attach"
        Me.b_attach.Size = New System.Drawing.Size(103, 20)
        Me.b_attach.TabIndex = 5
        Me.b_attach.Text = "Attach"
        Me.b_attach.UseVisualStyleBackColor = True
        '
        't_type2
        '
        Me.t_type2.Location = New System.Drawing.Point(121, 101)
        Me.t_type2.Name = "t_type2"
        Me.t_type2.Size = New System.Drawing.Size(108, 20)
        Me.t_type2.TabIndex = 7
        '
        'l_type2
        '
        Me.l_type2.AutoSize = True
        Me.l_type2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_type2.Location = New System.Drawing.Point(12, 99)
        Me.l_type2.Name = "l_type2"
        Me.l_type2.Size = New System.Drawing.Size(0, 18)
        Me.l_type2.TabIndex = 8
        '
        'PickupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(257, 154)
        Me.ControlBox = False
        Me.Controls.Add(Me.l_type2)
        Me.Controls.Add(Me.t_type2)
        Me.Controls.Add(Me.t_attach)
        Me.Controls.Add(Me.b_attach)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.PickupType)
        Me.Name = "PickupForm"
        Me.Text = "Pickup Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PickupType As System.Windows.Forms.ComboBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents t_attach As System.Windows.Forms.TextBox
    Friend WithEvents b_attach As System.Windows.Forms.Button
    Friend WithEvents t_type2 As System.Windows.Forms.TextBox
    Friend WithEvents l_type2 As System.Windows.Forms.Label
End Class
