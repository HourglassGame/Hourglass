<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditingForm
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
        Me.b_box = New System.Windows.Forms.Button
        Me.b_portal = New System.Windows.Forms.Button
        Me.b_platform = New System.Windows.Forms.Button
        Me.b_switch = New System.Windows.Forms.Button
        Me.b_save = New System.Windows.Forms.Button
        Me.b_load = New System.Windows.Forms.Button
        Me.b_delete = New System.Windows.Forms.Button
        Me.LoadFile = New System.Windows.Forms.OpenFileDialog
        Me.SaveFile = New System.Windows.Forms.SaveFileDialog
        Me.b_snap = New System.Windows.Forms.CheckBox
        Me.c_wall = New System.Windows.Forms.RadioButton
        Me.c_select = New System.Windows.Forms.RadioButton
        Me.lable1 = New System.Windows.Forms.Label
        Me.b_pickup = New System.Windows.Forms.Button
        Me.b_spike = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.t_name = New System.Windows.Forms.TextBox
        Me.t_background = New System.Windows.Forms.TextBox
        Me.t_foreground = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.b_export_fore = New System.Windows.Forms.Button
        Me.b_gate = New System.Windows.Forms.Button
        Me.b_export_fore_tileset = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'b_box
        '
        Me.b_box.Location = New System.Drawing.Point(10, 162)
        Me.b_box.Name = "b_box"
        Me.b_box.Size = New System.Drawing.Size(96, 28)
        Me.b_box.TabIndex = 1
        Me.b_box.Text = "Add Box"
        Me.b_box.UseVisualStyleBackColor = True
        '
        'b_portal
        '
        Me.b_portal.Location = New System.Drawing.Point(10, 129)
        Me.b_portal.Name = "b_portal"
        Me.b_portal.Size = New System.Drawing.Size(96, 27)
        Me.b_portal.TabIndex = 2
        Me.b_portal.Text = "Add Portal" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.b_portal.UseVisualStyleBackColor = True
        '
        'b_platform
        '
        Me.b_platform.Location = New System.Drawing.Point(10, 196)
        Me.b_platform.Name = "b_platform"
        Me.b_platform.Size = New System.Drawing.Size(96, 28)
        Me.b_platform.TabIndex = 3
        Me.b_platform.Text = "Add Platform"
        Me.b_platform.UseVisualStyleBackColor = True
        '
        'b_switch
        '
        Me.b_switch.Location = New System.Drawing.Point(112, 128)
        Me.b_switch.Name = "b_switch"
        Me.b_switch.Size = New System.Drawing.Size(96, 28)
        Me.b_switch.TabIndex = 4
        Me.b_switch.Text = "Add Switch"
        Me.b_switch.UseVisualStyleBackColor = True
        '
        'b_save
        '
        Me.b_save.Location = New System.Drawing.Point(10, 455)
        Me.b_save.Name = "b_save"
        Me.b_save.Size = New System.Drawing.Size(96, 28)
        Me.b_save.TabIndex = 6
        Me.b_save.Text = "Save"
        Me.b_save.UseVisualStyleBackColor = True
        '
        'b_load
        '
        Me.b_load.Location = New System.Drawing.Point(114, 455)
        Me.b_load.Name = "b_load"
        Me.b_load.Size = New System.Drawing.Size(96, 28)
        Me.b_load.TabIndex = 7
        Me.b_load.Text = "Load"
        Me.b_load.UseVisualStyleBackColor = True
        '
        'b_delete
        '
        Me.b_delete.Location = New System.Drawing.Point(114, 69)
        Me.b_delete.Name = "b_delete"
        Me.b_delete.Size = New System.Drawing.Size(96, 28)
        Me.b_delete.TabIndex = 8
        Me.b_delete.Text = "Delete Selected"
        Me.b_delete.UseVisualStyleBackColor = True
        '
        'LoadFile
        '
        Me.LoadFile.Filter = "Level Files|*.lvl"
        '
        'SaveFile
        '
        Me.SaveFile.DefaultExt = "lvl"
        '
        'b_snap
        '
        Me.b_snap.AutoSize = True
        Me.b_snap.Checked = True
        Me.b_snap.CheckState = System.Windows.Forms.CheckState.Checked
        Me.b_snap.Location = New System.Drawing.Point(119, 46)
        Me.b_snap.Name = "b_snap"
        Me.b_snap.Size = New System.Drawing.Size(89, 17)
        Me.b_snap.TabIndex = 9
        Me.b_snap.Text = "Snap To Grid"
        Me.b_snap.UseVisualStyleBackColor = True
        '
        'c_wall
        '
        Me.c_wall.AutoSize = True
        Me.c_wall.Location = New System.Drawing.Point(21, 52)
        Me.c_wall.Name = "c_wall"
        Me.c_wall.Size = New System.Drawing.Size(46, 17)
        Me.c_wall.TabIndex = 10
        Me.c_wall.Text = "Wall"
        Me.c_wall.UseVisualStyleBackColor = True
        '
        'c_select
        '
        Me.c_select.AutoSize = True
        Me.c_select.Checked = True
        Me.c_select.Location = New System.Drawing.Point(21, 75)
        Me.c_select.Name = "c_select"
        Me.c_select.Size = New System.Drawing.Size(55, 17)
        Me.c_select.TabIndex = 11
        Me.c_select.TabStop = True
        Me.c_select.Text = "Select"
        Me.c_select.UseVisualStyleBackColor = True
        '
        'lable1
        '
        Me.lable1.AutoSize = True
        Me.lable1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lable1.Location = New System.Drawing.Point(19, 16)
        Me.lable1.Name = "lable1"
        Me.lable1.Size = New System.Drawing.Size(48, 24)
        Me.lable1.TabIndex = 12
        Me.lable1.Text = "Tool"
        '
        'b_pickup
        '
        Me.b_pickup.Location = New System.Drawing.Point(112, 162)
        Me.b_pickup.Name = "b_pickup"
        Me.b_pickup.Size = New System.Drawing.Size(96, 28)
        Me.b_pickup.TabIndex = 13
        Me.b_pickup.Text = "Add Pickup"
        Me.b_pickup.UseVisualStyleBackColor = True
        '
        'b_spike
        '
        Me.b_spike.Location = New System.Drawing.Point(112, 196)
        Me.b_spike.Name = "b_spike"
        Me.b_spike.Size = New System.Drawing.Size(96, 28)
        Me.b_spike.TabIndex = 14
        Me.b_spike.Text = "Add Spikes"
        Me.b_spike.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 24)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Create"
        '
        't_name
        '
        Me.t_name.Location = New System.Drawing.Point(107, 301)
        Me.t_name.Name = "t_name"
        Me.t_name.Size = New System.Drawing.Size(100, 20)
        Me.t_name.TabIndex = 18
        '
        't_background
        '
        Me.t_background.Location = New System.Drawing.Point(108, 327)
        Me.t_background.Name = "t_background"
        Me.t_background.Size = New System.Drawing.Size(100, 20)
        Me.t_background.TabIndex = 19
        '
        't_foreground
        '
        Me.t_foreground.Location = New System.Drawing.Point(107, 353)
        Me.t_foreground.Name = "t_foreground"
        Me.t_foreground.Size = New System.Drawing.Size(100, 20)
        Me.t_foreground.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 302)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 16)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Level Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 331)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 16)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Background"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 357)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 16)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Foreground"
        '
        'b_export_fore
        '
        Me.b_export_fore.Location = New System.Drawing.Point(10, 393)
        Me.b_export_fore.Name = "b_export_fore"
        Me.b_export_fore.Size = New System.Drawing.Size(96, 28)
        Me.b_export_fore.TabIndex = 24
        Me.b_export_fore.Text = "Export"
        Me.b_export_fore.UseVisualStyleBackColor = True
        '
        'b_gate
        '
        Me.b_gate.Location = New System.Drawing.Point(12, 230)
        Me.b_gate.Name = "b_gate"
        Me.b_gate.Size = New System.Drawing.Size(96, 28)
        Me.b_gate.TabIndex = 25
        Me.b_gate.Text = "Add Gate"
        Me.b_gate.UseVisualStyleBackColor = True
        '
        'b_export_fore_tileset
        '
        Me.b_export_fore_tileset.Location = New System.Drawing.Point(112, 393)
        Me.b_export_fore_tileset.Name = "b_export_fore_tileset"
        Me.b_export_fore_tileset.Size = New System.Drawing.Size(96, 28)
        Me.b_export_fore_tileset.TabIndex = 26
        Me.b_export_fore_tileset.Text = "Export Tileset"
        Me.b_export_fore_tileset.UseVisualStyleBackColor = True
        '
        'EditingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 495)
        Me.ControlBox = False
        Me.Controls.Add(Me.b_export_fore_tileset)
        Me.Controls.Add(Me.b_gate)
        Me.Controls.Add(Me.b_export_fore)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.t_foreground)
        Me.Controls.Add(Me.t_background)
        Me.Controls.Add(Me.t_name)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.b_spike)
        Me.Controls.Add(Me.b_pickup)
        Me.Controls.Add(Me.lable1)
        Me.Controls.Add(Me.c_select)
        Me.Controls.Add(Me.c_wall)
        Me.Controls.Add(Me.b_snap)
        Me.Controls.Add(Me.b_delete)
        Me.Controls.Add(Me.b_load)
        Me.Controls.Add(Me.b_save)
        Me.Controls.Add(Me.b_switch)
        Me.Controls.Add(Me.b_platform)
        Me.Controls.Add(Me.b_portal)
        Me.Controls.Add(Me.b_box)
        Me.Name = "EditingForm"
        Me.Text = "Tools"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents b_box As System.Windows.Forms.Button
    Friend WithEvents b_portal As System.Windows.Forms.Button
    Friend WithEvents b_platform As System.Windows.Forms.Button
    Friend WithEvents b_switch As System.Windows.Forms.Button
    Friend WithEvents b_save As System.Windows.Forms.Button
    Friend WithEvents b_load As System.Windows.Forms.Button
    Friend WithEvents b_delete As System.Windows.Forms.Button
    Friend WithEvents LoadFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents b_snap As System.Windows.Forms.CheckBox
    Friend WithEvents c_wall As System.Windows.Forms.RadioButton
    Friend WithEvents c_select As System.Windows.Forms.RadioButton
    Friend WithEvents lable1 As System.Windows.Forms.Label
    Friend WithEvents b_pickup As System.Windows.Forms.Button
    Friend WithEvents b_spike As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents t_name As System.Windows.Forms.TextBox
    Friend WithEvents t_background As System.Windows.Forms.TextBox
    Friend WithEvents t_foreground As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents b_export_fore As System.Windows.Forms.Button
    Friend WithEvents b_gate As System.Windows.Forms.Button
    Friend WithEvents b_export_fore_tileset As System.Windows.Forms.Button
End Class
