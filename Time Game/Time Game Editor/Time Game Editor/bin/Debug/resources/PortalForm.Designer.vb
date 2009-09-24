<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PortalForm
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
        Me.label1 = New System.Windows.Forms.Label
        Me.PortalType = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Prompt = New System.Windows.Forms.SaveFileDialog
        Me.PortalTime = New System.Windows.Forms.TextBox
        Me.Charges = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.t_attach = New System.Windows.Forms.TextBox
        Me.b_attach = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(9, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(61, 13)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Portal Type"
        '
        'PortalType
        '
        Me.PortalType.FormattingEnabled = True
        Me.PortalType.Items.AddRange(New Object() {"Addition Portal", "Flipping Addition Portal", "Fixed Portal", "Whenever Portal", "Reverse"})
        Me.PortalType.Location = New System.Drawing.Point(12, 25)
        Me.PortalType.Name = "PortalType"
        Me.PortalType.Size = New System.Drawing.Size(166, 21)
        Me.PortalType.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Time Parameter"
        '
        'PortalTime
        '
        Me.PortalTime.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.PortalTime.Location = New System.Drawing.Point(12, 78)
        Me.PortalTime.Name = "PortalTime"
        Me.PortalTime.Size = New System.Drawing.Size(166, 20)
        Me.PortalTime.TabIndex = 4
        '
        'Charges
        '
        Me.Charges.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.Charges.Location = New System.Drawing.Point(12, 124)
        Me.Charges.Name = "Charges"
        Me.Charges.Size = New System.Drawing.Size(166, 20)
        Me.Charges.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Charges"
        '
        't_attach
        '
        Me.t_attach.Location = New System.Drawing.Point(121, 165)
        Me.t_attach.Name = "t_attach"
        Me.t_attach.Size = New System.Drawing.Size(57, 20)
        Me.t_attach.TabIndex = 8
        '
        'b_attach
        '
        Me.b_attach.Location = New System.Drawing.Point(12, 164)
        Me.b_attach.Name = "b_attach"
        Me.b_attach.Size = New System.Drawing.Size(103, 20)
        Me.b_attach.TabIndex = 7
        Me.b_attach.Text = "Attach"
        Me.b_attach.UseVisualStyleBackColor = True
        '
        'PortalForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(191, 208)
        Me.ControlBox = False
        Me.Controls.Add(Me.t_attach)
        Me.Controls.Add(Me.b_attach)
        Me.Controls.Add(Me.Charges)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PortalTime)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PortalType)
        Me.Controls.Add(Me.label1)
        Me.Name = "PortalForm"
        Me.Text = "Portal Editer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents PortalType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Prompt As System.Windows.Forms.SaveFileDialog
    Friend WithEvents PortalTime As System.Windows.Forms.TextBox
    Friend WithEvents Charges As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents t_attach As System.Windows.Forms.TextBox
    Friend WithEvents b_attach As System.Windows.Forms.Button
End Class
