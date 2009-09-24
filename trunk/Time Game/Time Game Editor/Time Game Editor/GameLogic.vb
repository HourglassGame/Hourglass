Module GameLogic

    Dim wall(31, 21) As Boolean

    Dim mouse As Point2D

    Dim backgroundImage As String
    Dim foregroundImage As String
    Dim levelName As String

    Dim selected As Integer
    Dim queryType As Integer
    '0 = normal, 1 = selecting switch, 2 = selecting platform, 3 = alternate selecting platform
    Dim selectedType As Integer
    '0 = guy, 1 = box, 2 = portal, 3 = platform, 4 = switch, 5 = end portal, 6 = pickup, 7 = spike, 8 = gate
    Dim moveType As Integer
    'platform: 0 = object, 1 = on, 2 = off
    'switch: 0 = on, 1 = off

    Dim level_path As String

    'Dim keyTypes As Integer = 5 KEY

    Dim boxCount As Integer = 0
    Dim portalCount As Integer = 0
    Dim platCount As Integer = 0
    Dim switchCount As Integer = 0
    Dim pickupCount As Integer = 0
    Dim spikeCount As Integer = 0
    Dim gateCount As Integer = 0

    Dim i, j As Integer

    Public pressed_box As Boolean
    Public pressed_wall As Boolean
    Public pressed_select As Boolean
    Public pressed_portal As Boolean
    Public pressed_platform As Boolean
    Public pressed_switch As Boolean
    Public pressed_pickup As Boolean
    Public pressed_spike As Boolean
    Public pressed_gate As Boolean
    Public pressed_delete As Boolean
    Public pressed_export As Boolean
    Public pressed_export_tileset As Boolean
    Public pressed_save As Boolean
    Public pressed_load As Boolean
    Public save_string As String
    Public load_string As String

    Public plat_width As Boolean
    Public plat_height As Boolean
    Public plat_start As Boolean
    Public plat_on As Boolean
    Public plat_off As Boolean
    Public plat_switch As Boolean

    Public switch_attach As Boolean
    Public switch_rotate As Boolean
    Public switch_attach2 As Boolean
    Public switch_rotate2 As Boolean

    Public pickup_attach As Boolean

    Public portal_attach As Boolean

    Public spike_attach As Boolean
    Public spike_rotate As Boolean

    Public gate_attach1 As Boolean
    Public gate_attach2 As Boolean

    Public text_string As System.Object
    Public request_text_string As Boolean
    Public end_text_string As Boolean
    Dim reading_text As Boolean

    Dim gamePath As String

    Public Sub Main()
        'Opens a new Graphics Window
        Core.OpenGraphicsWindow("Game", 1024, 704)

        'CompadibilityRun()

        'Open Audio Device
        Audio.OpenAudio()

        'Load Resources
        LoadResources()

        gamePath = Directory.GetCurrentDirectory()

        selected = -1

        For i = 0 To 31
            wall(i, 0) = True
            For j = 18 To 21
                wall(i, j) = True
            Next
        Next
        For i = 1 To 21
            wall(0, i) = True
            
            wall(31, i) = True
        Next

        EditingForm.Enabled = True
        EditingForm.Visible = True

        PortalForm.Enabled = True
        guy.x = 500
        guy.y = 350

        endPortal.x = 600
        endPortal.y = 350
        endPortal.attach = -1

        'Game Loop
        Do
            'Clears the Screen to Black
            SwinGame.Graphics.ClearScreen()

            'Draws rectangle on the screen
            Graphics.FillRectangle(Color.LightGray, 0, 0, 1024, 704)

            ProcessEditForm()
            ProcessPlatformForm()
            ProcessSwitchForm()
            ProcessPickupForm()
            ProcessPortalForm()
            ProcessSpikeForm()
            ProcessGateForm()

            mouse = Input.GetMousePosition()

            UpdateSelectedInstanceStatusFromForm()

            If EditingForm.c_wall.Checked Then
                If Input.IsMouseDown(MouseButton.RightButton) Then
                    If Not (Math.Floor(mouse.X / 32) = 0 Or Math.Floor(mouse.X / 32) = 31 Or Math.Floor(mouse.Y / 32) = 0 Or Math.Floor(mouse.Y / 32) > 17) Then
                        wall(Math.Floor(mouse.X / 32), Math.Floor(mouse.Y / 32)) = False
                    End If
                End If
                If Input.IsMouseDown(MouseButton.LeftButton) Then
                    wall(Math.Floor(mouse.X / 32), Math.Floor(mouse.Y / 32)) = True
                End If
            ElseIf EditingForm.c_select.Checked Then
                If Input.MouseWasClicked(MouseButton.LeftButton) Then
                    LeftMouseQuery()
                End If

                If Input.IsMouseDown(MouseButton.RightButton) And Not selected = -1 Then
                    MoveSelectedObject()
                End If


            End If

            'If request_text_string Then
            '    EnterNumber.Enabled = True
            '    EnterNumber.Visible = True
            '    request_text_string = False
            'End If

            If request_text_string Then
                If reading_text = True Then
                    Input.EndReadingText()
                End If
                Input.StartReadingText(Color.Red, 20, GameFont("Courier"), 32, 32)
                request_text_string = False
                reading_text = True
            End If
            If end_text_string Then
                Dim text As String = Input.EndReadingText()
                If Not text = "" Then
                    text_string.text = text
                End If
                end_text_string = False
                reading_text = False
            End If

            DrawEverything()

            'Refreshes the Screen and Processes Input Events
            Core.RefreshScreen()
            Core.ProcessEvents()
        Loop Until SwinGame.Core.WindowCloseRequested() = True

        'Free Resources and Close Audio, to end the program.
        FreeResources()
        Audio.CloseAudio()
    End Sub

    Private Sub ProcessEditForm()

        If pressed_delete Then  '0 = guy, 1 = box, 2 = portal, 3 = platform, 4 = switch
            If Not selected = -1 Then
                If selectedType = 1 Then
                    DestroyBox(selected)
                    selected = -1
                ElseIf selectedType = 2 Then
                    DestroyPortal(selected)
                    selected = -1
                ElseIf selectedType = 3 Then
                    DestroyPlatform(selected)
                    selected = -1
                ElseIf selectedType = 4 Then
                    DestroySwitch(selected)
                    selected = -1
                ElseIf selectedType = 6 Then
                    DestroyPickup(selected)
                    selected = -1
                ElseIf selectedType = 7 Then
                    DestroySpike(selected)
                    selected = -1
                ElseIf selectedType = 8 Then
                    DestroyGate(selected)
                    selected = -1
                End If
                selectedType = -1
            End If
            pressed_delete = False
        End If

        If pressed_box Then
            CreateBox()
            pressed_box = False
        End If

        If pressed_portal Then
            CreatePortal()
            pressed_portal = False
        End If

        If pressed_platform Then
            CreatePlatform()
            pressed_platform = False
        End If

        If pressed_switch Then
            CreateSwitch()
            pressed_switch = False
        End If

        If pressed_pickup Then
            CreatePickup()
            pressed_pickup = False
        End If

        If pressed_spike Then
            CreateSpike()
            pressed_spike = False
        End If

        If pressed_gate Then
            CreateGate()
            pressed_gate = False
        End If

        If pressed_export Then
            ExportForeground()
            pressed_export = False
        End If

        If pressed_export_tileset Then
            ExportForegroundTileset()
            pressed_export_tileset = False
        End If

        If pressed_save Then
            level_path = EditingForm.SaveFile.FileName
            If (Not level_path = "") Then
                SaveLevel()
            End If
            pressed_save = False
        End If

        If pressed_load Then
            level_path = EditingForm.LoadFile.FileName
            If (Not level_path = "") Then
                LoadLevel()
            End If
            pressed_load = False
        End If

    End Sub

    Private Sub ProcessPlatformForm()

        If plat_switch Then
            queryType = 1
            plat_switch = False
        End If

        If plat_start Then
            moveType = 0
            plat_start = False
        End If

        If plat_on Then
            moveType = 1
            plat_on = False
        End If

        If plat_off Then
            moveType = 2
            plat_off = False
        End If

    End Sub
    Private Sub ProcessSwitchForm()

        If switch_attach Then
            queryType = 2
            switch_attach = False
        End If

        If switch_rotate Then
            switch(selected).rotation = switch(selected).rotation + 1
            If switch(selected).rotation > 3 Then
                switch(selected).rotation = 0
            End If
            switch_rotate = False
        End If

        If switch_attach2 Then
            queryType = 3
            switch_attach2 = False
        End If

        If switch_rotate2 Then
            switch(selected).rotation2 = switch(selected).rotation2 + 1
            If switch(selected).rotation2 > 3 Then
                switch(selected).rotation2 = 0
            End If
            switch_rotate2 = False
        End If

    End Sub

    Private Sub ProcessPickupForm()

        If pickup_attach Then
            queryType = 2
            pickup_attach = False
        End If

    End Sub

    Private Sub ProcessPortalForm()

        If portal_attach Then
            queryType = 2
            portal_attach = False
        End If

    End Sub

    Private Sub ProcessSpikeForm()

        If spike_attach Then
            queryType = 2
            spike_attach = False
        End If

        If spike_rotate Then
            spike(selected).rotation = spike(selected).rotation + 1
            If spike(selected).rotation > 3 Then
                spike(selected).rotation = 0
            End If
            spike_rotate = False
        End If

    End Sub

    Private Sub ProcessGateForm()

        If gate_attach1 Then
            queryType = 1
            gate_attach1 = False
        End If

        If gate_attach2 Then
            queryType = 2
            gate_attach2 = False
        End If

    End Sub

    Private Sub UpdateSelectedInstanceStatusFromForm()

        levelName = EditingForm.t_name.Text
        backgroundImage = EditingForm.t_background.Text
        foregroundImage = EditingForm.t_foreground.Text

        If selectedType = 2 Then
            portal(selected).type = PortalForm.PortalType.SelectedIndex
            PortalForm.PortalTime.Text = Val(PortalForm.PortalTime.Text)
            portal(selected).effect = PortalForm.PortalTime.Text
            PortalForm.Charges.Text = Val(PortalForm.Charges.Text)
            portal(selected).charges = PortalForm.Charges.Text
            portal(selected).attach = Val(PortalForm.t_attach.Text)
            If portal(selected).attach >= platCount Then
                portal(selected).attach = platCount - 1
                PortalForm.t_attach.Text = platCount - 1
            End If
            If portal(selected).attach < -1 Then
                portal(selected).attach = -1
                PortalForm.t_attach.Text = -1
            End If
        End If

        If selectedType = 3 Then
            PlatformForm.start_x.Text = Val(PlatformForm.start_x.Text)
            plat(selected).x = PlatformForm.start_x.Text
            PlatformForm.start_y.Text = Val(PlatformForm.start_y.Text)
            plat(selected).y = PlatformForm.start_y.Text
            PlatformForm.t_width.Text = Math.Floor(Math.Abs(Val(PlatformForm.t_width.Text)))
            plat(selected).width = PlatformForm.t_width.Text
            PlatformForm.t_height.Text = Math.Floor(Math.Abs(Val(PlatformForm.t_height.Text)))
            plat(selected).height = PlatformForm.t_height.Text
            PlatformForm.t_switch.Text = Math.Floor(Val(PlatformForm.t_switch.Text))
            plat(selected).switch_id = PlatformForm.t_switch.Text
            If plat(selected).switch_id < 1000 Then
                If plat(selected).switch_id >= switchCount Then
                    plat(selected).switch_id = switchCount - 1
                    PlatformForm.t_switch.Text = switchCount - 1
                End If
                If plat(selected).switch_id < -1 Then
                    plat(selected).switch_id = -1
                    PlatformForm.t_switch.Text = -1
                End If
            Else
                If plat(selected).switch_id >= gateCount + 1000 Then
                    plat(selected).switch_id = gateCount - 999
                    PlatformForm.t_switch.Text = gateCount - 999
                End If
                If plat(selected).switch_id < -1 Then
                    plat(selected).switch_id = -1
                    PlatformForm.t_switch.Text = -1
                End If
            End If
            PlatformForm.on_x.Text = Val(PlatformForm.on_x.Text)
            plat(selected).x_on = PlatformForm.on_x.Text
            PlatformForm.on_y.Text = Val(PlatformForm.on_y.Text)
            plat(selected).y_on = PlatformForm.on_y.Text
            PlatformForm.on_xspeed.Text = Val(PlatformForm.on_xspeed.Text)
            plat(selected).xspeed_on = PlatformForm.on_xspeed.Text
            PlatformForm.on_yspeed.Text = Val(PlatformForm.on_yspeed.Text)
            plat(selected).yspeed_on = PlatformForm.on_yspeed.Text
            PlatformForm.off_x.Text = Val(PlatformForm.off_x.Text)
            plat(selected).x_off = PlatformForm.off_x.Text
            PlatformForm.off_y.Text = Val(PlatformForm.off_y.Text)
            plat(selected).y_off = PlatformForm.off_y.Text
            PlatformForm.off_xspeed.Text = Val(PlatformForm.off_xspeed.Text)
            plat(selected).xspeed_off = PlatformForm.off_xspeed.Text
            PlatformForm.off_yspeed.Text = Val(PlatformForm.off_yspeed.Text)
            plat(selected).yspeed_off = PlatformForm.off_yspeed.Text
            PlatformForm.image.Text = PlatformForm.image.Text
            plat(selected).image = PlatformForm.image.Text
            plat(selected).start_sound_forward = PlatformForm.t_forwards_sound_start.Text
            plat(selected).end_sound_forward = PlatformForm.t_forwards_sound_end.Text
            plat(selected).move_sound_forward = PlatformForm.t_forwards_sound_move.Text
            plat(selected).start_sound_backward = PlatformForm.t_back_sound_start.Text
            plat(selected).end_sound_backward = PlatformForm.t_back_sound_end.Text
            plat(selected).move_sound_backward = PlatformForm.t_back_sound_move.Text
        End If

        If selectedType = 4 Then
            switch(selected).type = SwitchForm.SwitchType.SelectedIndex

            switch(selected).attach = Val(SwitchForm.t_attach.Text)
            If switch(selected).attach >= platCount Then
                switch(selected).attach = platCount - 1
                SwitchForm.t_attach.Text = platCount - 1
            End If
            If switch(selected).attach < -1 Then
                switch(selected).attach = -1
                SwitchForm.t_attach.Text = -1
            End If

            switch(selected).attach2 = Val(SwitchForm.t_attach2.Text)
            If switch(selected).attach2 >= platCount Then
                switch(selected).attach2 = platCount - 1
                SwitchForm.t_attach.Text = platCount - 1
            End If
            If switch(selected).attach2 < -1 Then
                switch(selected).attach2 = -1
                SwitchForm.t_attach.Text = -1
            End If

            switch(selected).hit_guy = SwitchForm.c_guy.Checked
            switch(selected).hit_box = SwitchForm.c_box.Checked
            switch(selected).hit_plat = SwitchForm.c_plat.Checked
            switch(selected).hit_wall = SwitchForm.c_wall.Checked
            switch(selected).visible = SwitchForm.c_visible.Checked
        End If

        If selectedType = 5 Then
            If endPortal.attach >= platCount Then
                endPortal.attach = platCount - 1
                PortalForm.t_attach.Text = platCount - 1
            End If
            If endPortal.attach < -1 Then
                endPortal.attach = -1
                PortalForm.t_attach.Text = -1
            End If
        End If

        If selectedType = 6 Then
            pickup(selected).type = PickupForm.PickupType.SelectedIndex
            pickup(selected).type2 = Math.Floor(Val(PickupForm.t_type2.Text))
            'If pickup(selected).type = 2 Then KEY
            '    If pickup(selected).type2 > keyTypes - 1 Then
            '        pickup(selected).type2 = keyTypes - 1
            '        PickupForm.t_type2.Text = keyTypes - 1
            '    End If
            '    If pickup(selected).type2 < 0 Then
            '        pickup(selected).type2 = 0
            '        PickupForm.t_type2.Text = 0
            '    End If
            'End If
            pickup(selected).attach = Val(PickupForm.t_attach.Text)
            If pickup(selected).attach >= platCount Then
                pickup(selected).attach = platCount - 1
                PickupForm.t_attach.Text = platCount - 1
            End If
            If pickup(selected).attach < -1 Then
                pickup(selected).attach = -1
                PickupForm.t_attach.Text = -1
            End If
        End If

        If selectedType = 7 Then
            spike(selected).attach = Val(SpikeForm.t_attach.Text)
            If spike(selected).attach >= platCount Then
                spike(selected).attach = platCount - 1
                SpikeForm.t_attach.Text = platCount - 1
            End If
            If spike(selected).attach < -1 Then
                spike(selected).attach = -1
                SpikeForm.t_attach.Text = -1
            End If
            SpikeForm.t_size.Text = Math.Floor(Val(SpikeForm.t_size.Text) / 16) * 16
            If SpikeForm.t_size.Text < 16 Then
                SpikeForm.t_size.Text = 16
            End If
            spike(selected).size = Val(SpikeForm.t_size.Text)
        End If

        If selectedType = 8 Then
            gate(selected).type = LogicForm.GateType.SelectedIndex
            gate(selected).attach1 = LogicForm.t_attach1.Text
            If gate(selected).attach1 >= switchCount Then
                gate(selected).attach1 = switchCount - 1
                LogicForm.t_attach1.Text = switchCount - 1
            End If
            If gate(selected).attach1 < -1 Then
                gate(selected).attach1 = -1
                LogicForm.t_attach1.Text = -1
            End If
            gate(selected).attach2 = LogicForm.t_attach2.Text
            If gate(selected).attach2 >= switchCount Then
                gate(selected).attach2 = switchCount - 1
                LogicForm.t_attach1.Text = switchCount - 1
            End If
            If gate(selected).attach2 < -1 Then
                gate(selected).attach2 = -1
                LogicForm.t_attach1.Text = -1
            End If
        End If

    End Sub

    Private Sub LeftMouseQuery()

        If queryType = 0 Then
            selected = -1
            CheckSelect()
            If selectedType = 2 Then
                PortalForm.Visible = True
                PortalForm.PortalType.SelectedIndex = portal(selected).type
                PortalForm.PortalTime.Text = (portal(selected).effect)
                PortalForm.t_attach.Text = (portal(selected).attach)
            ElseIf selectedType = 5 Then
                PortalForm.t_attach.Text = (endPortal.attach)
                PortalForm.Visible = True
            Else
                PortalForm.Visible = False
            End If
            If selectedType = 3 Then
                moveType = 0
                PlatformForm.Visible = True
                PlatformForm.start_x.Text = plat(selected).x
                PlatformForm.start_y.Text = plat(selected).y
                PlatformForm.t_width.Text = plat(selected).width
                PlatformForm.t_height.Text = plat(selected).height
                PlatformForm.t_switch.Text = plat(selected).switch_id
                PlatformForm.on_x.Text = plat(selected).x_on
                PlatformForm.on_y.Text = plat(selected).y_on
                PlatformForm.on_xspeed.Text = plat(selected).xspeed_on
                PlatformForm.on_yspeed.Text = plat(selected).yspeed_on
                PlatformForm.off_x.Text = plat(selected).x_off
                PlatformForm.off_y.Text = plat(selected).y_off
                PlatformForm.off_xspeed.Text = plat(selected).xspeed_off
                PlatformForm.off_yspeed.Text = plat(selected).yspeed_off
                PlatformForm.image.Text = plat(selected).image
                PlatformForm.t_forwards_sound_start.Text = plat(selected).start_sound_forward
                PlatformForm.t_forwards_sound_end.Text = plat(selected).end_sound_forward
                PlatformForm.t_forwards_sound_move.Text = plat(selected).move_sound_forward
                PlatformForm.t_back_sound_start.Text = plat(selected).start_sound_backward
                PlatformForm.t_back_sound_end.Text = plat(selected).end_sound_backward
                PlatformForm.t_back_sound_move.Text = plat(selected).move_sound_backward
            Else
                PlatformForm.Visible = False
            End If

            If selectedType = 4 Then
                SwitchForm.Visible = True
                SwitchForm.SwitchType.SelectedIndex = switch(selected).type
                SwitchForm.t_attach.Text = switch(selected).attach
                SwitchForm.t_attach2.Text = switch(selected).attach2
                SwitchForm.c_guy.Checked = switch(selected).hit_guy
                SwitchForm.c_box.Checked = switch(selected).hit_box
                SwitchForm.c_plat.Checked = switch(selected).hit_plat
                SwitchForm.c_wall.Checked = switch(selected).hit_wall
                SwitchForm.c_visible.Checked = switch(selected).visible
            Else
                SwitchForm.Visible = False
            End If

            If selectedType = 6 Then
                PickupForm.Visible = True
                PickupForm.PickupType.SelectedIndex = pickup(selected).type
                PickupForm.t_type2.Text = pickup(selected).type2
                'If pickup(selected).type = 2 Then KEY
                '    PickupForm.l_type2.Text = "Key Number"
                'Else
                '    PickupForm.l_type2.Text = ""
                'End If
                PickupForm.t_attach.Text = pickup(selected).attach
            Else
                PickupForm.Visible = False
            End If
            If selectedType = 7 Then
                SpikeForm.Visible = True
                SpikeForm.t_attach.Text = spike(selected).attach
                SpikeForm.t_size.Text = spike(selected).size
            Else
                SpikeForm.Visible = False
            End If

            If selectedType = 8 Then
                LogicForm.Visible = True
                LogicForm.GateType.SelectedIndex = gate(selected).type
                LogicForm.t_attach1.Text = gate(selected).attach1
                LogicForm.t_attach2.Text = gate(selected).attach2
            Else
                LogicForm.Visible = False
            End If
        ElseIf queryType = 1 Then 'platform/gate finding switch
            queryType = 0
            If selectedType = 3 Then
                For i = 0 To switchCount - 1
                    If switch(i).type = 0 Then
                        If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                            If switch(i).x < mouse.X And switch(i).x + 32 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 12 > mouse.Y Then
                                plat(selected).switch_id = i
                                Exit For
                            End If
                        Else
                            If switch(i).x < mouse.X And switch(i).x + 12 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 32 > mouse.Y Then
                                plat(selected).switch_id = i
                                Exit For
                            End If
                        End If
                    ElseIf switch(i).type = 1 Then
                        If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                            plat(selected).switch_id = i
                            Exit For
                        End If
                        If switch(i).x2 < mouse.X And switch(i).x2 + 16 > mouse.X And switch(i).y2 < mouse.Y And switch(i).y2 + 16 > mouse.Y Then
                            plat(selected).switch_id = i
                            Exit For
                        End If
                    ElseIf switch(i).type = 2 Then
                        If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                            plat(selected).switch_id = i
                            Exit For
                        End If
                    End If
                    plat(selected).switch_id = -1
                Next
                For i = 0 To gateCount - 1
                    If gate(i).x < mouse.X And gate(i).x + 45 > mouse.X And gate(i).y < mouse.Y And gate(i).y + 45 > mouse.Y Then
                        plat(selected).switch_id = i + 1000
                        Exit For
                    End If
                    plat(selected).switch_id = -1
                Next
                PlatformForm.t_switch.Text = plat(selected).switch_id
            ElseIf selectedType = 8 Then
                For i = 0 To switchCount - 1
                    If switch(i).type = 0 Then
                        If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                            If switch(i).x < mouse.X And switch(i).x + 32 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 12 > mouse.Y Then
                                gate(selected).attach1 = i
                                Exit For
                            End If
                        Else
                            If switch(i).x < mouse.X And switch(i).x + 12 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 32 > mouse.Y Then
                                gate(selected).attach1 = i
                                Exit For
                            End If
                        End If
                    ElseIf switch(i).type = 1 Then
                        If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                            gate(selected).attach1 = i
                            Exit For
                        End If
                        If switch(i).x2 < mouse.X And switch(i).x2 + 16 > mouse.X And switch(i).y2 < mouse.Y And switch(i).y2 + 16 > mouse.Y Then
                            gate(selected).attach1 = i
                            Exit For
                        End If
                    ElseIf switch(i).type = 2 Then
                        If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                            gate(selected).attach1 = i
                            Exit For
                        End If
                    End If
                    gate(selected).attach1 = -1
                Next
                LogicForm.t_attach1.Text = gate(selected).attach1
            End If
        ElseIf queryType = 2 Then 'finding platform
            queryType = 0
            If selectedType = 2 Then
                For i = 0 To platCount - 1
                    If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                        portal(selected).attach = i
                        Exit For
                    End If
                    portal(selected).attach = -1
                Next
                PortalForm.t_attach.Text = portal(selected).attach
            End If
            If selectedType = 4 Then
                For i = 0 To platCount - 1
                    If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                        switch(selected).attach = i
                        Exit For
                    End If
                    switch(selected).attach = -1
                Next
                SwitchForm.t_attach.Text = switch(selected).attach
            End If
            If selectedType = 5 Then
                For i = 0 To platCount - 1
                    If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                        endPortal.attach = i
                        Exit For
                    End If
                    endPortal.attach = -1
                Next
                PortalForm.t_attach.Text = endPortal.attach
            End If
            If selectedType = 6 Then
                For i = 0 To platCount - 1
                    If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                        pickup(selected).attach = i
                        Exit For
                    End If
                    pickup(selected).attach = -1
                Next
                PickupForm.t_attach.Text = pickup(selected).attach
            End If
            If selectedType = 7 Then
                For i = 0 To platCount - 1
                    If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                        spike(selected).attach = i
                        Exit For
                    End If
                    spike(selected).attach = -1
                Next
                SpikeForm.t_attach.Text = spike(selected).attach
            End If
            If selectedType = 8 Then
                For i = 0 To switchCount - 1
                    If switch(i).type = 0 Then
                        If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                            If switch(i).x < mouse.X And switch(i).x + 32 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 12 > mouse.Y Then
                                gate(selected).attach2 = i
                                Exit For
                            End If
                        Else
                            If switch(i).x < mouse.X And switch(i).x + 12 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 32 > mouse.Y Then
                                gate(selected).attach2 = i
                                Exit For
                            End If
                        End If
                    ElseIf switch(i).type = 1 Then
                        If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                            gate(selected).attach2 = i
                            Exit For
                        End If
                        If switch(i).x2 < mouse.X And switch(i).x2 + 16 > mouse.X And switch(i).y2 < mouse.Y And switch(i).y2 + 16 > mouse.Y Then
                            gate(selected).attach2 = i
                            Exit For
                        End If
                    ElseIf switch(i).type = 2 Then
                        If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                            gate(selected).attach2 = i
                            Exit For
                        End If
                    End If
                    gate(selected).attach2 = -1
                Next
                LogicForm.t_attach2.Text = gate(selected).attach2
            End If
        ElseIf queryType = 3 Then 'another finding platform
            queryType = 0
            If selectedType = 4 Then
                For i = 0 To platCount - 1
                    If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                        switch(selected).attach2 = i
                        Exit For
                    End If
                    switch(selected).attach2 = -1
                Next
                SwitchForm.t_attach2.Text = switch(selected).attach2
            End If
        End If

    End Sub

    Private Sub CheckSelect()

        selectedType = -1

        For i = 0 To pickupCount - 1
            If pickup(i).x < mouse.X And pickup(i).x + 17 > mouse.X And pickup(i).y < mouse.Y And pickup(i).y + 17 > mouse.Y Then
                selected = i
                selectedType = 6
                Exit Sub
            End If
        Next

        For i = 0 To switchCount - 1
            If switch(i).type = 0 Then
                If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                    If switch(i).x < mouse.X And switch(i).x + 32 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 12 > mouse.Y Then
                        selected = i
                        selectedType = 4
                        moveType = 0
                        Exit Sub
                    End If
                Else
                    If switch(i).x < mouse.X And switch(i).x + 12 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 32 > mouse.Y Then
                        selected = i
                        selectedType = 4
                        moveType = 0
                        Exit Sub
                    End If
                End If
            ElseIf switch(i).type = 1 Then
                If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                    selected = i
                    selectedType = 4
                    moveType = 0
                    Exit Sub
                End If
                If switch(i).x2 < mouse.X And switch(i).x2 + 16 > mouse.X And switch(i).y2 < mouse.Y And switch(i).y2 + 16 > mouse.Y Then
                    selected = i
                    selectedType = 4
                    moveType = 1
                    Exit Sub
                End If
            ElseIf switch(i).type = 2 Then
                If switch(i).x < mouse.X And switch(i).x + 16 > mouse.X And switch(i).y < mouse.Y And switch(i).y + 16 > mouse.Y Then
                    selected = i
                    selectedType = 4
                    moveType = 0
                    Exit Sub
                End If
            End If
        Next

        If guy.x < mouse.X And guy.x + 23 > mouse.X And guy.y < mouse.Y And guy.y + 32 > mouse.Y Then
            selected = 1
            selectedType = 0
            Exit Sub
        End If

        For i = 0 To boxCount - 1
            If box(i).x < mouse.X And box(i).x + 32 > mouse.X And box(i).y < mouse.Y And box(i).y + 32 > mouse.Y Then
                selected = i
                selectedType = 1
                Exit Sub
            End If
        Next

        For i = 0 To spikeCount - 1
            If spike(i).rotation = 0 Or spike(i).rotation = 2 Then
                If spike(i).x < mouse.X And spike(i).x + spike(i).size > mouse.X And spike(i).y < mouse.Y And spike(i).y + 16 > mouse.Y Then
                    selected = i
                    selectedType = 7
                    Exit Sub
                End If
            Else
                If spike(i).x < mouse.X And spike(i).x + 16 > mouse.X And spike(i).y < mouse.Y And spike(i).y + spike(i).size > mouse.Y Then
                    selected = i
                    selectedType = 7
                    Exit Sub
                End If
            End If
        Next

        For i = 0 To gateCount - 1
            If gate(i).x < mouse.X And gate(i).x + 45 > mouse.X And gate(i).y < mouse.Y And gate(i).y + 45 > mouse.Y Then
                selected = i
                selectedType = 8
                Exit Sub
            End If
        Next

        For i = 0 To portalCount - 1
            If portal(i).x - 32 < mouse.X And portal(i).x + 32 > mouse.X And portal(i).y - 32 < mouse.Y And portal(i).y + 32 > mouse.Y Then
                selected = i
                selectedType = 2
                Exit Sub
            End If
        Next

        If endPortal.x - 32 < mouse.X And endPortal.x + 32 > mouse.X And endPortal.y - 32 < mouse.Y And endPortal.y + 32 > mouse.Y Then
            selected = 0
            selectedType = 5
            Exit Sub
        End If

        For i = 0 To platCount - 1
            If plat(i).x < mouse.X And plat(i).x + plat(i).width > mouse.X And plat(i).y < mouse.Y And plat(i).y + plat(i).height > mouse.Y Then
                selected = i
                selectedType = 3
                Exit Sub
            End If
        Next

    End Sub

    Private Sub MoveSelectedObject()
        '0 = guy, 1 = box, 2 = portal, 3 = platform, 4 = switch

        If selectedType = 0 Then
            If EditingForm.b_snap.Checked Then
                guy.x = Math.Round((mouse.X - 11) / 16) * 16
                guy.y = Math.Round((mouse.Y - 16) / 16) * 16
            Else
                guy.x = mouse.X - 11
                guy.y = mouse.Y - 16
            End If

        End If

        If selectedType = 1 Then
            If EditingForm.b_snap.Checked Then
                box(selected).x = Math.Round((mouse.X - 16) / 16) * 16
                box(selected).y = Math.Round((mouse.Y - 16) / 16) * 16
            Else
                box(selected).x = mouse.X - 16
                box(selected).y = mouse.Y - 16
            End If
        End If

        If selectedType = 2 Then
            If EditingForm.b_snap.Checked Then
                portal(selected).x = Math.Round(mouse.X / 16) * 16
                portal(selected).y = Math.Round(mouse.Y / 16) * 16
            Else
                portal(selected).x = mouse.X
                portal(selected).y = mouse.Y
            End If
        End If

        If selectedType = 3 Then
            If moveType = 0 Then
                If EditingForm.b_snap.Checked Then
                    plat(selected).x = Math.Round(mouse.X / 16) * 16
                    plat(selected).y = Math.Round(mouse.Y / 16) * 16
                Else
                    plat(selected).x = mouse.X
                    plat(selected).y = mouse.Y
                End If
                PlatformForm.start_x.Text = plat(selected).x
                PlatformForm.start_y.Text = plat(selected).y
            ElseIf moveType = 1 Then
                If EditingForm.b_snap.Checked Then
                    plat(selected).x_on = Math.Round(mouse.X / 16) * 16
                    plat(selected).y_on = Math.Round(mouse.Y / 16) * 16
                Else
                    plat(selected).x_on = mouse.X
                    plat(selected).y_on = mouse.Y
                End If
                PlatformForm.on_x.Text = plat(selected).x_on
                PlatformForm.on_y.Text = plat(selected).y_on
            ElseIf moveType = 2 Then
                If EditingForm.b_snap.Checked Then
                    plat(selected).x_off = Math.Round(mouse.X / 16) * 16
                    plat(selected).y_off = Math.Round(mouse.Y / 16) * 16
                Else
                    plat(selected).x_off = mouse.X
                    plat(selected).y_off = mouse.Y
                End If
                PlatformForm.off_x.Text = plat(selected).x_off
                PlatformForm.off_y.Text = plat(selected).y_off
            End If
        End If

        If selectedType = 4 Then
            If EditingForm.b_snap.Checked Then
                If switch(selected).type = 0 Then
                    If switch(selected).rotation = 0 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16 - 7
                    ElseIf switch(selected).rotation = 1 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16 - 7
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16
                    ElseIf switch(selected).rotation = 2 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16
                    ElseIf switch(selected).rotation = 3 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16
                    End If
                ElseIf switch(selected).type = 1 Then
                    If moveType = 0 Then
                        If switch(selected).rotation = 0 Then
                            switch(selected).x = Math.Round(mouse.X / 16) * 16
                            switch(selected).y = Math.Round(mouse.Y / 16) * 16 - 10
                        ElseIf switch(selected).rotation = 1 Then
                            switch(selected).x = Math.Round(mouse.X / 16) * 16 - 10
                            switch(selected).y = Math.Round(mouse.Y / 16) * 16
                        ElseIf switch(selected).rotation = 2 Then
                            switch(selected).x = Math.Round(mouse.X / 16) * 16
                            switch(selected).y = Math.Round(mouse.Y / 16) * 16
                        ElseIf switch(selected).rotation = 3 Then
                            switch(selected).x = Math.Round(mouse.X / 16) * 16
                            switch(selected).y = Math.Round(mouse.Y / 16) * 16
                        End If
                    Else
                        If switch(selected).rotation2 = 0 Then
                            switch(selected).x2 = Math.Round(mouse.X / 16) * 16
                            switch(selected).y2 = Math.Round(mouse.Y / 16) * 16 - 10
                        ElseIf switch(selected).rotation2 = 1 Then
                            switch(selected).x2 = Math.Round(mouse.X / 16) * 16 - 10
                            switch(selected).y2 = Math.Round(mouse.Y / 16) * 16
                        ElseIf switch(selected).rotation2 = 2 Then
                            switch(selected).x2 = Math.Round(mouse.X / 16) * 16
                            switch(selected).y2 = Math.Round(mouse.Y / 16) * 16
                        ElseIf switch(selected).rotation2 = 3 Then
                            switch(selected).x2 = Math.Round(mouse.X / 16) * 16
                            switch(selected).y2 = Math.Round(mouse.Y / 16) * 16
                        End If
                    End If
                ElseIf switch(selected).type = 2 Then
                    If switch(selected).rotation = 0 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16 - 10
                    ElseIf switch(selected).rotation = 1 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16 - 10
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16
                    ElseIf switch(selected).rotation = 2 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16
                    ElseIf switch(selected).rotation = 3 Then
                        switch(selected).x = Math.Round(mouse.X / 16) * 16
                        switch(selected).y = Math.Round(mouse.Y / 16) * 16
                    End If
                End If
            Else
                If switch(selected).type = 0 Then
                    switch(selected).x = mouse.X
                    switch(selected).y = mouse.Y
                ElseIf switch(selected).type = 1 Then
                    If moveType = 0 Then
                        switch(selected).x = mouse.X
                        switch(selected).y = mouse.Y
                    Else
                        switch(selected).x2 = mouse.X
                        switch(selected).y2 = mouse.Y
                    End If
                ElseIf switch(selected).type = 2 Then
                    switch(selected).x = mouse.X
                    switch(selected).y = mouse.Y
                End If
            End If
        End If

        If selectedType = 5 Then
            If EditingForm.b_snap.Checked Then
                endPortal.x = Math.Round(mouse.X / 16) * 16
                endPortal.y = Math.Round(mouse.Y / 16) * 16
            Else
                endPortal.x = mouse.X
                endPortal.y = mouse.Y
            End If
        End If

        If selectedType = 6 Then
            If EditingForm.b_snap.Checked Then
                pickup(selected).x = Math.Round(mouse.X / 16) * 16
                pickup(selected).y = Math.Round(mouse.Y / 16) * 16
            Else
                pickup(selected).x = mouse.X
                pickup(selected).y = mouse.Y
            End If
        End If

        If selectedType = 7 Then
            If EditingForm.b_snap.Checked Then
                spike(selected).x = Math.Round(mouse.X / 16) * 16
                spike(selected).y = Math.Round(mouse.Y / 16) * 16
            Else
                spike(selected).x = mouse.X
                spike(selected).y = mouse.Y
            End If
        End If

        If selectedType = 8 Then
            gate(selected).x = mouse.X
            gate(selected).y = mouse.Y
        End If

    End Sub

    Private Sub DrawEverything()

        ' Draw Walls
        For i = 0 To 31
            For j = 0 To 21
                If wall(i, j) = True Then
                    Graphics.FillRectangle(Color.Black, i * 32, j * 32, 32, 32)
                End If
            Next
        Next

        For i = 0 To platCount - 1
            Graphics.FillRectangle(Color.DarkGray, plat(i).x, plat(i).y, plat(i).width, plat(i).height)
        Next

        Graphics.DrawBitmap(GameImage("portal_end"), endPortal.x - 32, endPortal.y - 32)

        For i = 0 To portalCount - 1
            Graphics.DrawBitmap(GameImage("portal_black"), portal(i).x - 32, portal(i).y - 32)
        Next

        For i = 0 To spikeCount - 1
            If spike(i).rotation = 0 Then
                For j = 0 To spike(i).size - 1 Step 16
                    Graphics.DrawBitmap(GameImage("spike0"), spike(i).x + j, spike(i).y)
                Next
            ElseIf spike(i).rotation = 1 Then
                For j = 0 To spike(i).size - 1 Step 16
                    Graphics.DrawBitmap(GameImage("spike1"), spike(i).x, spike(i).y + j)
                Next
            ElseIf spike(i).rotation = 2 Then
                For j = 0 To spike(i).size - 1 Step 16
                    Graphics.DrawBitmap(GameImage("spike2"), spike(i).x + j, spike(i).y)
                Next
            ElseIf spike(i).rotation = 3 Then
                For j = 0 To spike(i).size - 1 Step 16
                    Graphics.DrawBitmap(GameImage("spike3"), spike(i).x, spike(i).y + j)
                Next
            End If
        Next

        For i = 0 To boxCount - 1
            Graphics.DrawBitmap(GameImage("box"), box(i).x, box(i).y)
        Next

        For i = 0 To gateCount - 1
            If gate(i).type = 0 Then
                Graphics.DrawBitmap(GameImage("and"), gate(i).x, gate(i).y)
            ElseIf gate(i).type = 1 Then
                Graphics.DrawBitmap(GameImage("or"), gate(i).x, gate(i).y)
            ElseIf gate(i).type = 2 Then
                Graphics.DrawBitmap(GameImage("xor"), gate(i).x, gate(i).y)
            End If
        Next

        For i = 0 To switchCount - 1
            If switch(i).type = 0 Then
                If switch(i).rotation = 0 Then
                    Graphics.DrawBitmap(GameImage("button0_up"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 1 Then
                    Graphics.DrawBitmap(GameImage("button1_up"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 2 Then
                    Graphics.DrawBitmap(GameImage("button2_up"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 3 Then
                    Graphics.DrawBitmap(GameImage("button3_up"), switch(i).x, switch(i).y)
                End If
            ElseIf switch(i).type = 1 Then
                If switch(i).rotation = 0 Then
                    Graphics.DrawBitmap(GameImage("button0_off_green"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 1 Then
                    Graphics.DrawBitmap(GameImage("button1_off_green"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 2 Then
                    Graphics.DrawBitmap(GameImage("button2_off_green"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 3 Then
                    Graphics.DrawBitmap(GameImage("button3_off_green"), switch(i).x, switch(i).y)
                End If
                If switch(i).rotation2 = 0 Then
                    Graphics.DrawBitmap(GameImage("button0_off_red"), switch(i).x2, switch(i).y2)
                ElseIf switch(i).rotation2 = 1 Then
                    Graphics.DrawBitmap(GameImage("button1_off_red"), switch(i).x2, switch(i).y2)
                ElseIf switch(i).rotation2 = 2 Then
                    Graphics.DrawBitmap(GameImage("button2_off_red"), switch(i).x2, switch(i).y2)
                ElseIf switch(i).rotation2 = 3 Then
                    Graphics.DrawBitmap(GameImage("button3_off_red"), switch(i).x2, switch(i).y2)
                End If
            ElseIf switch(i).type = 2 Then
                If switch(i).rotation = 0 Then
                    Graphics.DrawBitmap(GameImage("laserswitch0"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 1 Then
                    Graphics.DrawBitmap(GameImage("laserswitch1"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 2 Then
                    Graphics.DrawBitmap(GameImage("laserswitch2"), switch(i).x, switch(i).y)
                ElseIf switch(i).rotation = 3 Then
                    Graphics.DrawBitmap(GameImage("laserswitch3"), switch(i).x, switch(i).y)
                End If
            End If
        Next
        For i = 0 To pickupCount - 1
            If pickup(i).type = 0 Then
                Graphics.DrawBitmap(GameImage("time_jump"), pickup(i).x, pickup(i).y)
            ElseIf pickup(i).type = 1 Then
                Graphics.DrawBitmap(GameImage("time_gun"), pickup(i).x, pickup(i).y)
            ElseIf pickup(i).type = 2 Then
                Graphics.DrawBitmap(GameImage("reverse_time"), pickup(i).x, pickup(i).y)
            End If
        Next

        Graphics.DrawBitmap(GameImage("guy_left"), guy.x, guy.y)

        If Not selected = -1 Then

            If selectedType = 0 Then
                Graphics.DrawRectangle(Color.Red, False, guy.x, guy.y, 23, 32)
            End If

            If selectedType = 1 Then
                Graphics.DrawRectangle(Color.Red, False, box(selected).x, box(selected).y, 32, 32)
            End If

            If selectedType = 2 Then
                Graphics.DrawRectangle(Color.Red, False, portal(selected).x - 32, portal(selected).y - 32, 64, 64)
                If Not portal(selected).attach = -1 Then
                    Graphics.DrawRectangle(Color.Yellow, False, plat(portal(selected).attach).x, plat(portal(selected).attach).y, plat(portal(selected).attach).width, plat(portal(selected).attach).height)
                    Graphics.DrawLine(Color.Yellow, plat(portal(selected).attach).x, plat(portal(selected).attach).y, portal(selected).x, portal(selected).y)
                End If
            End If

            If selectedType = 3 Then
                Graphics.DrawRectangle(Color.Red, False, plat(selected).x, plat(selected).y, plat(selected).width, plat(selected).height)
                Graphics.DrawRectangle(Color.Green, False, plat(selected).x_on, plat(selected).y_on, plat(selected).width, plat(selected).height)
                Graphics.DrawRectangle(Color.Yellow, False, plat(selected).x_off, plat(selected).y_off, plat(selected).width, plat(selected).height)
                If Not plat(selected).switch_id = -1 Then
                    If plat(selected).switch_id < 1000 Then
                        If switch(plat(selected).switch_id).type = 0 Then
                            If switch(plat(selected).switch_id).rotation = 0 Or switch(plat(selected).switch_id).rotation = 2 Then
                                Graphics.DrawRectangle(Color.LightBlue, False, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y, 32, 7)
                            Else
                                Graphics.DrawRectangle(Color.LightBlue, False, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y, 7, 32)
                            End If
                        ElseIf switch(plat(selected).switch_id).type = 1 Then
                            If switch(plat(selected).switch_id).rotation = 0 Or switch(plat(selected).switch_id).rotation = 2 Then
                                Graphics.DrawRectangle(Color.LightBlue, False, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y, 16, 10)
                            Else
                                Graphics.DrawRectangle(Color.LightBlue, False, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y, 10, 16)
                            End If
                        ElseIf switch(plat(selected).switch_id).type = 2 Then
                            If switch(plat(selected).switch_id).rotation = 0 Or switch(plat(selected).switch_id).rotation = 2 Then
                                Graphics.DrawRectangle(Color.LightBlue, False, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y, 16, 10)
                            Else
                                Graphics.DrawRectangle(Color.LightBlue, False, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y, 10, 16)
                            End If
                        End If
                        Graphics.DrawLine(Color.LightBlue, plat(selected).x, plat(selected).y, switch(plat(selected).switch_id).x, switch(plat(selected).switch_id).y)
                    Else
                        Graphics.DrawRectangle(Color.LightBlue, False, gate(plat(selected).switch_id - 1000).x, gate(plat(selected).switch_id - 1000).y, 45, 45)
                        Graphics.DrawLine(Color.LightBlue, plat(selected).x, plat(selected).y, gate(plat(selected).switch_id - 1000).x, gate(plat(selected).switch_id - 1000).y)
                    End If
                End If
            End If

            If selectedType = 4 Then
                If switch(selected).type = 0 Then
                    If switch(selected).rotation = 0 Or switch(selected).rotation = 2 Then
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x, switch(selected).y, 32, 7)
                    Else
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x, switch(selected).y, 7, 32)
                    End If
                    If Not switch(selected).attach = -1 Then
                        Graphics.DrawRectangle(Color.Yellow, False, plat(switch(selected).attach).x, plat(switch(selected).attach).y, plat(switch(selected).attach).width, plat(switch(selected).attach).height)
                        Graphics.DrawLine(Color.Yellow, plat(switch(selected).attach).x, plat(switch(selected).attach).y, switch(selected).x, switch(selected).y)
                    End If
                ElseIf switch(selected).type = 1 Then
                    If switch(selected).rotation = 0 Or switch(selected).rotation = 2 Then
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x, switch(selected).y, 16, 10)
                    Else
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x, switch(selected).y, 10, 16)
                    End If
                    If switch(selected).rotation2 = 0 Or switch(selected).rotation2 = 2 Then
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x2, switch(selected).y2, 16, 10)
                    Else
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x2, switch(selected).y2, 10, 16)
                    End If
                    If Not switch(selected).attach = -1 Then
                        Graphics.DrawRectangle(Color.Yellow, False, plat(switch(selected).attach).x, plat(switch(selected).attach).y, plat(switch(selected).attach).width, plat(switch(selected).attach).height)
                        Graphics.DrawLine(Color.Yellow, plat(switch(selected).attach).x, plat(switch(selected).attach).y, switch(selected).x, switch(selected).y)
                    End If
                    If Not switch(selected).attach2 = -1 Then
                        Graphics.DrawRectangle(Color.Yellow, False, plat(switch(selected).attach2).x, plat(switch(selected).attach2).y, plat(switch(selected).attach2).width, plat(switch(selected).attach2).height)
                        Graphics.DrawLine(Color.Yellow, plat(switch(selected).attach2).x, plat(switch(selected).attach2).y, switch(selected).x2, switch(selected).y2)
                    End If
                ElseIf switch(selected).type = 2 Then
                    If switch(selected).rotation = 0 Or switch(selected).rotation = 2 Then
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x, switch(selected).y, 16, 10)
                    Else
                        Graphics.DrawRectangle(Color.Red, False, switch(selected).x, switch(selected).y, 10, 16)
                    End If
                    If Not switch(selected).attach = -1 Then
                        Graphics.DrawRectangle(Color.Yellow, False, plat(switch(selected).attach).x, plat(switch(selected).attach).y, plat(switch(selected).attach).width, plat(switch(selected).attach).height)
                        Graphics.DrawLine(Color.Yellow, plat(switch(selected).attach).x, plat(switch(selected).attach).y, switch(selected).x, switch(selected).y)
                    End If
                End If
            End If

            If selectedType = 5 Then
                Graphics.DrawRectangle(Color.Red, False, endPortal.x - 32, endPortal.y - 32, 64, 64)
                If Not endPortal.attach = -1 Then
                    Graphics.DrawRectangle(Color.Yellow, False, plat(endPortal.attach).x, plat(endPortal.attach).y, plat(endPortal.attach).width, plat(endPortal.attach).height)
                    Graphics.DrawLine(Color.Yellow, plat(endPortal.attach).x, plat(endPortal.attach).y, endPortal.x, endPortal.y)
                End If
            End If

            If selectedType = 6 Then
                Graphics.DrawRectangle(Color.Red, False, pickup(selected).x, pickup(selected).y, 17, 17)
                If Not pickup(selected).attach = -1 Then
                    Graphics.DrawRectangle(Color.Yellow, False, plat(pickup(selected).attach).x, plat(pickup(selected).attach).y, plat(pickup(selected).attach).width, plat(pickup(selected).attach).height)
                    Graphics.DrawLine(Color.Yellow, plat(pickup(selected).attach).x, plat(pickup(selected).attach).y, pickup(selected).x, pickup(selected).y)
                End If
            End If

            If selectedType = 7 Then
                If spike(selected).rotation = 0 Or spike(selected).rotation = 2 Then
                    Graphics.DrawRectangle(Color.Red, False, spike(selected).x, spike(selected).y, spike(selected).size, 16)
                Else
                    Graphics.DrawRectangle(Color.Red, False, spike(selected).x, spike(selected).y, 16, spike(selected).size)
                End If
                If Not spike(selected).attach = -1 Then
                    Graphics.DrawRectangle(Color.Yellow, False, plat(spike(selected).attach).x, plat(spike(selected).attach).y, plat(spike(selected).attach).width, plat(spike(selected).attach).height)
                    Graphics.DrawLine(Color.Yellow, plat(spike(selected).attach).x, plat(spike(selected).attach).y, spike(selected).x, spike(selected).y)
                End If
            End If

            If selectedType = 8 Then
                Graphics.DrawRectangle(Color.Red, False, gate(selected).x, gate(selected).y, 45, 45)
                If Not gate(selected).attach1 = -1 Then
                    If switch(gate(selected).attach1).type = 0 Then
                        If switch(gate(selected).attach1).rotation = 0 Or switch(gate(selected).attach1).rotation = 2 Then
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y, 32, 7)
                        Else
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y, 7, 32)
                        End If
                    ElseIf switch(gate(selected).attach1).type = 1 Then
                        If switch(gate(selected).attach1).rotation = 0 Or switch(gate(selected).attach1).rotation = 2 Then
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y, 16, 10)
                        Else
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y, 10, 16)
                        End If
                    ElseIf switch(gate(selected).attach1).type = 2 Then
                        If switch(gate(selected).attach1).rotation = 0 Or switch(gate(selected).attach1).rotation = 2 Then
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y, 16, 10)
                        Else
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y, 10, 16)
                        End If
                    End If
                    Graphics.DrawLine(Color.LightBlue, gate(selected).x, gate(selected).y, switch(gate(selected).attach1).x, switch(gate(selected).attach1).y)
                End If
                If Not gate(selected).attach2 = -1 Then
                    If switch(gate(selected).attach2).type = 0 Then
                        If switch(gate(selected).attach2).rotation = 0 Or switch(gate(selected).attach2).rotation = 2 Then
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y, 32, 7)
                        Else
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y, 7, 32)
                        End If
                    ElseIf switch(gate(selected).attach2).type = 1 Then
                        If switch(gate(selected).attach2).rotation = 0 Or switch(gate(selected).attach2).rotation = 2 Then
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y, 16, 10)
                        Else
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y, 10, 16)
                        End If
                    ElseIf switch(gate(selected).attach2).type = 2 Then
                        If switch(gate(selected).attach2).rotation = 0 Or switch(gate(selected).attach2).rotation = 2 Then
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y, 16, 10)
                        Else
                            Graphics.DrawRectangle(Color.LightBlue, False, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y, 10, 16)
                        End If
                    End If
                    Graphics.DrawLine(Color.LightBlue, gate(selected).x, gate(selected).y, switch(gate(selected).attach2).x, switch(gate(selected).attach2).y)
                End If
            End If
        End If
    End Sub

    Private Sub ExportForeground()

        Dim bm As New System.Drawing.Bitmap(1024, 608)
        ' Draw on it.
        Dim gr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bm)

        gr.Clear(Color.Magenta)

        ' Draw Walls
        For i = 0 To 31
            For j = 0 To 18
                If wall(i, j) = True Then
                    gr.DrawRectangle(System.Drawing.Pens.Black, i * 32, j * 32, 32, 32)
                End If
            Next
        Next

        ' Save the result as a JPEG file.
        bm.Save(foregroundImage + ".png", System.Drawing.Imaging.ImageFormat.Png)

    End Sub

    Private Sub ExportForegroundTileset()

        Dim bm As New System.Drawing.Bitmap(1024, 608)
        ' Draw on it.
        Dim gr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bm)

        gr.Clear(Color.Magenta)

        Dim wall1111 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1111.png")

        Dim wall0111 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0111.png")
        Dim wall1011 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1011.png")
        Dim wall1101 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1101.png")
        Dim wall1110 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1110.png")

        Dim wall1100 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1100.png")
        Dim wall1010 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1010.png")
        Dim wall1001 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1001.png")
        Dim wall0110 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0110.png")
        Dim wall0101 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0101.png")
        Dim wall0011 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0011.png")

        Dim wallBR As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wallBR.png")
        Dim wallTR As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wallTR.png")
        Dim wallBL As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wallBL.png")
        Dim wallTL As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wallTL.png")

        Dim wall1000 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall1000.png")
        Dim wall0100 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0100.png")
        Dim wall0010 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0010.png")
        Dim wall0001 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0001.png")

        Dim wall0000 As System.Drawing.Image = System.Drawing.Image.FromFile(gamePath + "\resources\tileset\wall0000.png")

        ' Draw Walls
        gr.DrawImage(wall1111, 0, 0)
        If Not wall(1, 1) Then
            gr.DrawImage(wallBR, 16, 16)
        End If
        gr.DrawImage(wall1111, 0, 576)
        If Not wall(1, 17) Then
            gr.DrawImage(wallTR, 16, 576)
        End If
        gr.DrawImage(wall1111, 992, 0)
        If Not wall(30, 1) Then
            gr.DrawImage(wallBL, 992, 16)
        End If
        gr.DrawImage(wall1111, 992, 576)
        If Not wall(30, 17) Then
            gr.DrawImage(wallTL, 992, 576)
        End If

        For i = 1 To 30
            If wall(i, 1) = True Then
                gr.DrawImage(wall1111, i * 32, 0)
                If Not wall(i - 1, 1) Then
                    gr.DrawImage(wallBL, i * 32, 16)
                End If
                If Not wall(i + 1, 1) Then
                    gr.DrawImage(wallBR, i * 32 + 16, 16)
                End If
            Else
                gr.DrawImage(wall1110, i * 32, 0)
            End If
            If wall(i, 17) = True Then
                gr.DrawImage(wall1111, i * 32, 576)
                If Not wall(i - 1, 17) Then
                    gr.DrawImage(wallTL, i * 32, 576)
                End If
                If Not wall(i + 1, 17) Then
                    gr.DrawImage(wallTR, i * 32 + 16, 576)
                End If
            Else
                gr.DrawImage(wall1011, i * 32, 576)
            End If
        Next

        For j = 1 To 17
            If wall(1, j) = True Then
                gr.DrawImage(wall1111, 0, j * 32)
                If Not wall(1, j - 1) Then
                    gr.DrawImage(wallTR, 16, j * 32)
                End If
                If Not wall(1, j + 1) Then
                    gr.DrawImage(wallBR, 16, j * 32 + 16)
                End If
            Else
                gr.DrawImage(wall0111, 0, j * 32)
            End If
            If wall(30, j) = True Then
                gr.DrawImage(wall1111, 992, j * 32)
                If Not wall(30, j - 1) Then
                    gr.DrawImage(wallTL, 992, j * 32)
                End If
                If Not wall(30, j + 1) Then
                    gr.DrawImage(wallBL, 992, j * 32 + 16)
                End If
            Else
                gr.DrawImage(wall1101, 992, j * 32)
            End If
        Next

        For i = 1 To 30
            For j = 1 To 17
                If wall(i, j) = True Then
                    If wall(i - 1, j) Then
                        If wall(i + 1, j) Then
                            If wall(i, j - 1) Then
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall1111, i * 32, j * 32)
                                    If Not wall(i - 1, j - 1) Then
                                        gr.DrawImage(wallTL, i * 32, j * 32)
                                    End If
                                    If Not wall(i + 1, j - 1) Then
                                        gr.DrawImage(wallTR, i * 32 + 16, j * 32)
                                    End If
                                    If Not wall(i - 1, j + 1) Then
                                        gr.DrawImage(wallBL, i * 32, j * 32 + 16)
                                    End If
                                    If Not wall(i + 1, j + 1) Then
                                        gr.DrawImage(wallBR, i * 32 + 16, j * 32 + 16)
                                    End If
                                Else
                                    gr.DrawImage(wall1110, i * 32, j * 32)
                                    If Not wall(i - 1, j - 1) Then
                                        gr.DrawImage(wallTL, i * 32, j * 32)
                                    End If
                                    If Not wall(i + 1, j - 1) Then
                                        gr.DrawImage(wallTR, i * 32 + 16, j * 32)
                                    End If
                                End If
                            Else
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall1011, i * 32, j * 32)
                                    If Not wall(i - 1, j + 1) Then
                                        gr.DrawImage(wallBL, i * 32, j * 32 + 16)
                                    End If
                                    If Not wall(i + 1, j + 1) Then
                                        gr.DrawImage(wallBR, i * 32 + 16, j * 32 + 16)
                                    End If
                                Else
                                    gr.DrawImage(wall1010, i * 32, j * 32)
                                End If
                            End If
                        Else
                            If wall(i, j - 1) Then
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall0111, i * 32, j * 32)
                                    If Not wall(i - 1, j - 1) Then
                                        gr.DrawImage(wallTL, i * 32, j * 32)
                                    End If
                                    If Not wall(i - 1, j + 1) Then
                                        gr.DrawImage(wallBL, i * 32, j * 32 + 16)
                                    End If
                                Else
                                    gr.DrawImage(wall0110, i * 32, j * 32)
                                    If Not wall(i - 1, j - 1) Then
                                        gr.DrawImage(wallTL, i * 32, j * 32)
                                    End If
                                End If
                            Else
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall0011, i * 32, j * 32)
                                    If Not wall(i - 1, j + 1) Then
                                        gr.DrawImage(wallBL, i * 32, j * 32 + 16)
                                    End If
                                Else
                                    gr.DrawImage(wall0010, i * 32, j * 32)
                                End If
                            End If
                        End If
                    Else
                        If wall(i + 1, j) Then
                            If wall(i, j - 1) Then
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall1101, i * 32, j * 32)
                                    If Not wall(i + 1, j - 1) Then
                                        gr.DrawImage(wallTR, i * 32 + 16, j * 32)
                                    End If
                                    If Not wall(i + 1, j + 1) Then
                                        gr.DrawImage(wallBR, i * 32 + 16, j * 32 + 16)
                                    End If
                                Else
                                    gr.DrawImage(wall1100, i * 32, j * 32)
                                    If Not wall(i + 1, j - 1) Then
                                        gr.DrawImage(wallTR, i * 32 + 16, j * 32)
                                    End If
                                End If
                            Else
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall1001, i * 32, j * 32)
                                    If Not wall(i + 1, j + 1) Then
                                        gr.DrawImage(wallBR, i * 32 + 16, j * 32 + 16)
                                    End If
                                Else
                                    gr.DrawImage(wall1000, i * 32, j * 32)
                                End If
                            End If
                        Else
                            If wall(i, j - 1) Then
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall0101, i * 32, j * 32)
                                Else
                                    gr.DrawImage(wall0100, i * 32, j * 32)
                                End If
                            Else
                                If wall(i, j + 1) Then
                                    gr.DrawImage(wall0001, i * 32, j * 32)
                                Else
                                    gr.DrawImage(wall0000, i * 32, j * 32)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Next

        ' Save the result as a JPEG file.
        bm.Save(foregroundImage + ".png", System.Drawing.Imaging.ImageFormat.Png)

    End Sub

    Private Sub SaveLevel()
        Dim oWrite As System.IO.StreamWriter
        oWrite = File.CreateText(level_path)
        For i = 0 To 31
            For j = 0 To 21
                oWrite.WriteLine(wall(i, j))
            Next
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(levelName)
        oWrite.WriteLine(backgroundImage)
        oWrite.WriteLine(foregroundImage)
        oWrite.WriteLine("")
        oWrite.WriteLine(guy.x)
        oWrite.WriteLine(guy.y)
        oWrite.WriteLine("")
        oWrite.WriteLine(platCount)
        For i = 0 To platCount - 1
            oWrite.WriteLine(plat(i).x)
            oWrite.WriteLine(plat(i).y)
            oWrite.WriteLine(plat(i).width)
            oWrite.WriteLine(plat(i).height)
            oWrite.WriteLine(plat(i).image)
            oWrite.WriteLine(plat(i).switch_id)
            oWrite.WriteLine(plat(i).x_on)
            oWrite.WriteLine(plat(i).y_on)
            oWrite.WriteLine(plat(i).xspeed_on)
            oWrite.WriteLine(plat(i).yspeed_on)
            oWrite.WriteLine(plat(i).x_off)
            oWrite.WriteLine(plat(i).y_off)
            oWrite.WriteLine(plat(i).xspeed_off)
            oWrite.WriteLine(plat(i).yspeed_off)
            oWrite.WriteLine(plat(i).start_sound_forward)
            oWrite.WriteLine(plat(i).end_sound_forward)
            oWrite.WriteLine(plat(i).move_sound_forward)
            oWrite.WriteLine(plat(i).start_sound_backward)
            oWrite.WriteLine(plat(i).end_sound_backward)
            oWrite.WriteLine(plat(i).move_sound_backward)
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(endPortal.x)
        oWrite.WriteLine(endPortal.y)
        oWrite.WriteLine(endPortal.attach)
        oWrite.WriteLine("")
        oWrite.WriteLine(boxCount)
        For i = 0 To boxCount - 1
            oWrite.WriteLine(box(i).x)
            oWrite.WriteLine(box(i).y)
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(portalCount)
        For i = 0 To portalCount - 1
            oWrite.WriteLine(portal(i).x)
            oWrite.WriteLine(portal(i).y)
            oWrite.WriteLine(portal(i).effect)
            oWrite.WriteLine(portal(i).type)
            oWrite.WriteLine(portal(i).charges)
            oWrite.WriteLine(portal(i).attach)
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(switchCount)
        For i = 0 To switchCount - 1
            oWrite.WriteLine(switch(i).x)
            oWrite.WriteLine(switch(i).y)
            oWrite.WriteLine(switch(i).visible)
            oWrite.WriteLine(switch(i).attach)
            oWrite.WriteLine(switch(i).rotation)
            oWrite.WriteLine(switch(i).type)
            If switch(i).type = 1 Then
                oWrite.WriteLine(switch(i).x2)
                oWrite.WriteLine(switch(i).y2)
                oWrite.WriteLine(switch(i).attach2)
                oWrite.WriteLine(switch(i).rotation2)
            End If
            oWrite.WriteLine(switch(i).hit_guy)
            oWrite.WriteLine(switch(i).hit_box)
            oWrite.WriteLine(switch(i).hit_plat)
            oWrite.WriteLine(switch(i).hit_wall)
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(pickupCount)
        For i = 0 To pickupCount - 1
            oWrite.WriteLine(pickup(i).x)
            oWrite.WriteLine(pickup(i).y)
            oWrite.WriteLine(pickup(i).type)
            oWrite.WriteLine(pickup(i).type2)
            oWrite.WriteLine(pickup(i).attach)
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(spikeCount)
        For i = 0 To spikeCount - 1
            oWrite.WriteLine(spike(i).x)
            oWrite.WriteLine(spike(i).y)
            oWrite.WriteLine(spike(i).rotation)
            oWrite.WriteLine(spike(i).size)
            oWrite.WriteLine(spike(i).attach)
        Next
        oWrite.WriteLine("")
        oWrite.WriteLine(gateCount)
        For i = 0 To gateCount - 1
            oWrite.WriteLine(gate(i).x)
            oWrite.WriteLine(gate(i).y)
            oWrite.WriteLine(gate(i).type)
            oWrite.WriteLine(gate(i).attach1)
            oWrite.WriteLine(gate(i).attach2)
        Next
        oWrite.Close()

    End Sub

    Private Sub LoadLevel()

        Dim oRead As System.IO.StreamReader
        oRead = File.OpenText(level_path)
        For i = 0 To 31
            For j = 0 To 21
                wall(i, j) = oRead.ReadLine()
            Next
        Next
        oRead.ReadLine()
        levelName = oRead.ReadLine()
        EditingForm.t_name.Text = levelName
        backgroundImage = oRead.ReadLine()
        EditingForm.t_background.Text = backgroundImage
        foregroundImage = oRead.ReadLine()
        EditingForm.t_foreground.Text = foregroundImage
        oRead.ReadLine()
        guy.x = oRead.ReadLine()
        guy.y = oRead.ReadLine()
        oRead.ReadLine()
        platCount = oRead.ReadLine()
        For i = 0 To platCount - 1
            plat(i).x = oRead.ReadLine()
            plat(i).y = oRead.ReadLine()
            plat(i).width = oRead.ReadLine()
            plat(i).height = oRead.ReadLine()
            plat(i).image = oRead.ReadLine()
            plat(i).switch_id = oRead.ReadLine()
            plat(i).x_on = oRead.ReadLine()
            plat(i).y_on = oRead.ReadLine()
            plat(i).xspeed_on = oRead.ReadLine()
            plat(i).yspeed_on = oRead.ReadLine()
            plat(i).x_off = oRead.ReadLine()
            plat(i).y_off = oRead.ReadLine()
            plat(i).xspeed_off = oRead.ReadLine()
            plat(i).yspeed_off = oRead.ReadLine()
            plat(i).start_sound_forward = oRead.ReadLine()
            plat(i).end_sound_forward = oRead.ReadLine()
            plat(i).move_sound_forward = oRead.ReadLine()
            plat(i).start_sound_backward = oRead.ReadLine()
            plat(i).end_sound_backward = oRead.ReadLine()
            plat(i).move_sound_backward = oRead.ReadLine()
        Next
        oRead.ReadLine()
        endPortal.x = oRead.ReadLine()
        endPortal.y = oRead.ReadLine()
        endPortal.attach = oRead.ReadLine()
        oRead.ReadLine()
        boxCount = oRead.ReadLine()
        For i = 0 To boxCount - 1
            box(i).x = oRead.ReadLine()
            box(i).y = oRead.ReadLine()
        Next
        oRead.ReadLine()
        portalCount = oRead.ReadLine()
        For i = 0 To portalCount - 1
            portal(i).x = oRead.ReadLine()
            portal(i).y = oRead.ReadLine()
            portal(i).effect = oRead.ReadLine()
            portal(i).type = oRead.ReadLine()
            portal(i).charges = oRead.ReadLine()
            portal(i).attach = oRead.ReadLine()
        Next
        oRead.ReadLine()
        switchCount = oRead.ReadLine()
        For i = 0 To switchCount - 1
            switch(i).x = oRead.ReadLine()
            switch(i).y = oRead.ReadLine()
            switch(i).visible = oRead.ReadLine()
            switch(i).attach = oRead.ReadLine()
            switch(i).rotation = oRead.ReadLine()
            switch(i).type = oRead.ReadLine()
            If switch(i).type = 1 Then
                switch(i).x2 = oRead.ReadLine()
                switch(i).y2 = oRead.ReadLine()
                switch(i).attach2 = oRead.ReadLine()
                switch(i).rotation2 = oRead.ReadLine()
            End If
            switch(i).hit_guy = oRead.ReadLine()
            switch(i).hit_box = oRead.ReadLine()
            switch(i).hit_plat = oRead.ReadLine()
            switch(i).hit_wall = oRead.ReadLine()
        Next
        oRead.ReadLine()
        pickupCount = oRead.ReadLine()
        For i = 0 To pickupCount - 1
            pickup(i).x = oRead.ReadLine()
            pickup(i).y = oRead.ReadLine()
            pickup(i).type = oRead.ReadLine()
            pickup(i).type2 = oRead.ReadLine()
            pickup(i).attach = oRead.ReadLine()
        Next
        oRead.ReadLine()
        spikeCount = oRead.ReadLine()
        For i = 0 To spikeCount - 1
            spike(i).x = oRead.ReadLine()
            spike(i).y = oRead.ReadLine()
            spike(i).rotation = oRead.ReadLine()
            spike(i).size = oRead.ReadLine()
            spike(i).attach = oRead.ReadLine()
        Next
        oRead.ReadLine()
        gateCount = oRead.ReadLine()
        For i = 0 To gateCount - 1
            gate(i).x = oRead.ReadLine()
            gate(i).y = oRead.ReadLine()
            gate(i).type = oRead.ReadLine()
            gate(i).attach1 = oRead.ReadLine()
            gate(i).attach2 = oRead.ReadLine()
        Next
        oRead.Close()

        selected = -1
        selectedType = -1
    End Sub

    Private Sub CreateBox()

        box(boxCount).x = 500
        box(boxCount).y = 350
        boxCount = boxCount + 1
    End Sub

    Private Sub DestroyBox(ByVal id As Integer)

        If Not id = boxCount - 1 Then
            box(id) = box(boxCount - 1)
        End If
        boxCount = boxCount - 1
    End Sub

    Private Sub CreatePortal()

        portal(portalCount).x = 500
        portal(portalCount).y = 350
        portal(portalCount).type = 0
        portal(portalCount).effect = 0
        portal(portalCount).attach = -1
        portalCount = portalCount + 1
    End Sub

    Private Sub DestroyPortal(ByVal id As Integer)

        If Not id = portalCount - 1 Then
            portal(id) = portal(portalCount - 1)
        End If
        portalCount = portalCount - 1
    End Sub

    Private Sub CreatePlatform()

        plat(platCount).x = 500
        plat(platCount).y = 350
        plat(platCount).width = 32
        plat(platCount).height = 32
        plat(platCount).image = ""
        plat(platCount).switch_id = -1
        platCount = platCount + 1
    End Sub

    Private Sub DestroyPlatform(ByVal id As Integer)

        If Not id = platCount - 1 Then
            plat(id) = plat(platCount - 1)
        End If
        platCount = platCount - 1
    End Sub

    Private Sub CreateSwitch()

        switch(switchCount).x = 500
        switch(switchCount).y = 350
        switch(switchCount).attach = -1

        switch(switchCount).x2 = 600
        switch(switchCount).y2 = 350
        switch(switchCount).attach2 = -1

        switch(switchCount).hit_guy = True
        switch(switchCount).hit_box = True
        switch(switchCount).hit_plat = True
        switch(switchCount).hit_wall = False

        switch(switchCount).visible = True

        switchCount = switchCount + 1
    End Sub

    Private Sub DestroySwitch(ByVal id As Integer)

        If Not id = switchCount - 1 Then
            switch(id) = switch(switchCount - 1)
            For i = 0 To platCount
                If plat(platCount).switch_id = switchCount - 1 Then
                    plat(platCount).switch_id = id
                End If
            Next
        End If
        For i = 0 To platCount
            If plat(platCount).switch_id = id Then
                plat(platCount).switch_id = -1
            End If
        Next
        switchCount = switchCount - 1


    End Sub

    Private Sub CreatePickup()

        pickup(pickupCount).x = 500
        pickup(pickupCount).y = 350
        pickup(pickupCount).type = 0
        pickup(pickupCount).type2 = 0
        pickup(pickupCount).attach = -1
        pickupCount = pickupCount + 1
    End Sub

    Private Sub DestroyPickup(ByVal id As Integer)

        If Not id = pickupCount - 1 Then
            pickup(id) = pickup(pickupCount - 1)
        End If

        pickupCount = pickupCount - 1

    End Sub

    Private Sub CreateSpike()

        spike(spikeCount).x = 500
        spike(spikeCount).y = 350
        spike(spikeCount).size = 16
        spike(spikeCount).rotation = 0
        spike(spikeCount).attach = -1
        spikeCount = spikeCount + 1
    End Sub

    Private Sub DestroySpike(ByVal id As Integer)

        If Not id = spikeCount - 1 Then
            spike(id) = spike(spikeCount - 1)
        End If

        spikeCount = spikeCount - 1

    End Sub

    Private Sub CreateGate()
        gate(gateCount).type = 0
        gate(gateCount).x = 500
        gate(gateCount).y = 350
        gate(gateCount).attach1 = -1
        gate(gateCount).attach2 = -1
        gateCount = gateCount + 1
    End Sub

    Private Sub DestroyGate(ByVal id As Integer)

        If Not id = gateCount - 1 Then
            gate(id) = gate(gateCount - 1)
        End If

        gateCount = gateCount - 1

        For i = 0 To platCount
            If plat(platCount).switch_id = id + 1000 Then
                plat(platCount).switch_id = -1
            End If
        Next

    End Sub

    Private Sub CompadibilityRun()
        'level_path = "a/Elevator II"
        'LoadLevel()
        'SaveLevel()
        level_path = "a/Enclosed"
        LoadLevel()
        SaveLevel()
        Dim i As Integer
        For i = 1 To 22
            level_path = String.Format("a/Level{0}", i)
            LoadLevel()
            SaveLevel()
        Next
        For i = 25 To 27
            level_path = String.Format("a/Level{0}", i)
            LoadLevel()
            SaveLevel()
        Next
        level_path = "a/lolwhat"
        LoadLevel()
        SaveLevel()
        level_path = "a/Tedium"
        LoadLevel()
        SaveLevel()
        level_path = "a/Test4"
        LoadLevel()
        SaveLevel()
        level_path = "a/trap"
        LoadLevel()
        SaveLevel()
    End Sub

End Module





