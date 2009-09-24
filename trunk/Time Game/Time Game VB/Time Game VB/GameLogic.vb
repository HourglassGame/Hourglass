Module GameLogic

    'Time stuff
    Dim time As Long = 1 'current time
    Dim time_speed As Integer = 1 'speed through time time
    Dim fastMode As Boolean = False 'fast mode; no player guy and max FPS
    Dim winCheck As Boolean = False 'win check; fast mode enabled, no guy and win at the end of the check
    Dim time_aim As Long = 0 'time that is aimed for by fast mode
    Dim maxCheckTime As Integer = 50 'max time seen so far, this is all that needs checking to
    Dim maxTime As Integer = 9000 'Maximun time that the level can go for
    Dim won As Integer = False 'has the player won, displays win and ends game

    Dim gravity As Single = 0.12 'acceleration due to gravity

    Dim portalSubimage() As Integer 'stored subimage for portals, linked to time
    Dim gearSubimage() As Integer 'stored subimage for interface gears, linked to time

    Dim stepTimer As New SwinGame.Timer 'timer to keep FPS at 50

    Dim mousePosition As Point2D

    Dim TimelineScale As Single = 0.1 'scale that the timeline is drawn to
    Dim TimelineMode As Integer = 0 '0 = absolute, 1 = relative

    Dim screenWidth As Integer = 1024
    Dim screenHeight As Integer = 704

    Dim wall(31, 21) As Boolean 'array of walls

    Dim playerGuy As Integer = 0 'ID of the guy that is player controlled, also the oldest instance(in time travel terms)
    Dim playerGuyStore As Integer = -1 'player guy storage for when the player is around but temportarilly an NPC, time gun check uses it

    'Keeps track of how many of each instance there is
    Dim guyCount As Integer = 0
    Dim portalCount As Integer = 0
    Dim boxCount As Integer = 0
    Dim platCount As Integer = 0
    Dim switchCount As Integer = 0
    Dim pickupCount As Integer = 0
    Dim spikeCount As Integer = 0
    Dim gatecount As Integer = 0

    'Keeps track of weapon numbers and current weapon
    Dim weapon As Integer = 1 ' 1 = jump, 2 = gun, 3 = reverse
    Dim weapons As Integer = 3 ' number of weapons
    Dim timeJumps As Integer = 0
    Dim timeGuns As Integer = 0
    Dim timeReverses As Integer = 0
    Dim shootTime As Integer = -1 ' Time aimed for by time laser

    Dim shootPause As Boolean

    'sound, length of reverse sounds
    Dim box_hit_length As Integer
    Dim box_pickup_length As Integer
    Dim laser_shoot_length As Integer
    Dim pickup_pickup_length As Integer
    Dim switch_push_down_length As Integer
    Dim switch_push_up_length As Integer
    Dim switch_toggle_length As Integer
    Dim switch_trip_laser_length As Integer
    Dim portal_in_length As Integer
    Dim guy_hit_length As Integer

    'guy variables
    Dim guy_width As Integer = 23
    Dim guy_width_left As Integer = 32 - guy_width

    'levels and art
    Dim file_string As String 'Load file string
    Dim restartLevel As Boolean 'Tag to restart the level at the start of the Do loop

    'loaded images on the fly, sounds unused
    Dim loadedImages As Integer 'how many images have been loaded
    Dim loadedImage() As String 'array of already loaded images
    Dim loadedSounds As Integer 'how many sounds have been loaded
    Dim loadedSound() As String 'array of already loaded sounds

    Dim backgroundImage As String 'image used as the background
    Dim foregroundImage As String 'image used as the foreground
    Dim levelName As String 'level name

    Dim runGame As Boolean ' false = menu, true = game

    Dim pauseGame As Boolean
    Dim manualPauseGame As Boolean

    'MENU STUFF
    Dim subimageX As Integer
    Dim subimageY As Integer

    Dim level As Integer

    Dim levelX As Integer
    Dim levelY As Integer
    Dim levelWidth As Integer
    Dim levelHeight As Integer
    Dim levelSize As Integer

    Dim storyX As Integer
    Dim storyY As Integer
    Dim storyWidth As Integer
    Dim storyHeight As Integer

    Dim creditsX As Integer
    Dim creditsY As Integer
    Dim creditsWidth As Integer
    Dim creditsHeight As Integer

    Dim loadX As Integer
    Dim loadY As Integer
    Dim loadWidth As Integer
    Dim loadHeight As Integer

    Dim exitX As Integer
    Dim exitY As Integer
    Dim exitWidth As Integer
    Dim exitHeight As Integer

    Dim subMenu As Integer '0 = menu, 1 = story, 2 = credits
    Dim exitGame As Boolean

    Dim gamePath As String

    'Interface
    'stored subimage for the time display at bottom left
    Dim timeP1image() As Integer
    Dim time1image() As Integer
    Dim time10image() As Integer
    Dim time100image() As Integer

    Dim jump1image As Integer
    Dim jump10image As Integer

    Dim gun1image As Integer
    Dim gun10image As Integer

    Dim rev1image As Integer
    Dim rev10image As Integer

    'extra variables to return from a function
    Dim return1
    Dim return2
    Dim return3

    Public Sub Main()

        Dim i, j As Integer
        'Opens a new Graphics Window
        Core.OpenGraphicsWindow("Game", 1024, 704)

        'Open Audio Device
        Audio.OpenAudio()

        'Load Resources
        LoadResources()

        'creates array of loaded images
        ReDim loadedImage(100)

        gamePath = Directory.GetCurrentDirectory()

        'set sound lengths for reverse sound purposes
        box_hit_length = 7
        box_pickup_length = 13
        laser_shoot_length = 20
        pickup_pickup_length = 7
        switch_push_down_length = 9
        switch_push_up_length = 9
        switch_toggle_length = 5
        switch_trip_laser_length = 10
        portal_in_length = 2
        guy_hit_length = 30

        'step timer to set FPS to 50

        stepTimer.Start()

        'portal animation pre-calc
        ReDim portalSubimage(maxTime + 50)
        j = 0
        For i = 0 To maxTime + 50
            portalSubimage(i) = j * 65
            j += 1
            If j > 49 Then
                j = 0
            End If
        Next

        'gear animation pre-calc
        ReDim gearSubimage(maxTime + 60)
        j = 0
        For i = 0 To maxTime + 50 Step 2
            gearSubimage(i) = j * 96
            gearSubimage(i + 1) = j * 96
            j += 1
            If j > 98 Then
                j = 0
            End If
        Next

        'time display animation pre-calc
        ReDim timeP1image(maxTime + 50)
        j = 0
        For i = 0 To maxTime + 50
            timeP1image(i) = j * 30
            j += 1
            If j > 49 Then
                j = 0
            End If
        Next

        ReDim time1image(maxTime + 50)
        j = 0
        For i = 0 To maxTime + 50
            time1image(i) = j * 30
            If (Math.Floor(i / 50) * 50 > i - 3 Or Math.Floor(i / 50) * 50 < i - 47) And i > 25 Then
                j += 1
            End If
            If j > 49 Then
                j = 0
            End If
        Next

        ReDim time10image(maxTime + 50)
        j = 0
        For i = 0 To maxTime + 50
            time10image(i) = j * 30
            If (Math.Floor(i / 500) * 500 > i - 3 Or Math.Floor(i / 500) * 500 < i - 497) And i > 25 Then
                j += 1
            End If
            If j > 49 Then
                j = 0
            End If
        Next

        ReDim time100image(maxTime + 50)
        j = 0
        For i = 0 To maxTime + 50
            time100image(i) = j * 30
            If (Math.Floor(i / 5000) * 5000 > i - 3 Or Math.Floor(i / 5000) * 5000 < i - 4997) And i > 25 Then
                j += 1
            End If
            If j > 49 Then
                j = 0
            End If
        Next

        'Menu numbers
        runGame = False
        levelX = 49
        levelY = 462
        levelWidth = 9
        levelHeight = 5
        levelSize = 42

        creditsX = 613
        creditsY = 377
        creditsWidth = 250
        creditsHeight = 105

        storyX = 613
        storyY = 240
        storyWidth = 250
        storyHeight = 105

        loadX = 613
        loadY = 102
        loadWidth = 250
        loadHeight = 105

        exitX = 615
        exitY = 517
        exitWidth = 250
        exitHeight = 105

        'Game Loop
        Do

            If runGame Then
                Game()
            Else
                Menu()
            End If

            'Refreshes the Screen and Processes Input Events 
            Core.RefreshScreen()
            Core.ProcessEvents()

        Loop Until SwinGame.Core.WindowCloseRequested() = True Or exitGame = True

        'Free Resources and Close Audio, to end the program.
        FreeResources()
        Audio.CloseAudio()
    End Sub

    Private Sub Menu()

        While stepTimer.Ticks < 20 And fastMode = False
            'While stepTimer.Ticks < 80 And fastMode = False
        End While
        stepTimer.Stop()
        stepTimer.Start()

        mousePosition = Input.GetMousePosition()

        If Input.MouseWasClicked(SwinGame.MouseButton.LeftButton) Then

            If subMenu = 0 Then
                'play an inbult level based on mouse position
                If mousePosition.X >= levelX And mousePosition.Y >= levelY And mousePosition.X <= levelX + levelWidth * levelSize And mousePosition.Y <= levelY + levelHeight * levelSize Then
                    level = Math.Floor((mousePosition.X - levelX) / levelSize) + Math.Floor((mousePosition.Y - levelY) / levelSize) * levelWidth + 1
                    file_string = String.Format(gamePath + "/levels/Level{0}.lvl", level)
                    LoadLevel(file_string)
                    runGame = True
                    Exit Sub
                End If

                If mousePosition.X >= loadX And mousePosition.Y >= loadY And mousePosition.X <= loadX + loadWidth And mousePosition.Y <= loadY + loadHeight Then
                    'load a level
                    Form.LoadFile.ShowDialog()
                    file_string = Form.LoadFile.FileName
                    If (Not file_string = "") Then
                        LoadLevel(file_string)
                        runGame = True
                        level = -1
                        Exit Sub
                    End If
                End If

                If mousePosition.X >= exitX And mousePosition.Y >= exitY And mousePosition.X <= exitX + exitWidth And mousePosition.Y <= exitY + exitHeight Then
                    'exits game
                    exitGame = True
                    Exit Sub
                End If

                If mousePosition.X >= storyX And mousePosition.Y >= storyY And mousePosition.X <= storyX + storyWidth And mousePosition.Y <= storyY + storyHeight Then
                    'displays story screen
                    subMenu = 1
                End If

                If mousePosition.X >= creditsX And mousePosition.Y >= creditsY And mousePosition.X <= creditsX + creditsWidth And mousePosition.Y <= creditsY + creditsHeight Then
                    'display credits screen
                    subMenu = 2
                End If
            Else
                subMenu = 0
            End If

        End If

        SwinGame.Graphics.ClearScreen()

        'draw image based on selected menu
        If subMenu = 0 Then
            subimageX += 1
            If subimageX > 4 Then
                subimageX = 0
                subimageY += 1
                If subimageY > 9 Then
                    subimageY = 0
                End If
            End If
            Graphics.DrawBitmap(GameImage("menu_left"), 0, 0)
            'Graphics.DrawBitmapPart(GameImage("menu_animate"), subimageX * 483, subimageY * 290, 483, 290, 58, 211) Menu animation not working
            'Light up the right buttons
            If mousePosition.X >= loadX And mousePosition.Y >= loadY And mousePosition.X <= loadX + loadWidth And mousePosition.Y <= loadY + loadHeight Then
                Graphics.DrawBitmap(GameImage("menuload"), 560, 0)
            ElseIf mousePosition.X >= exitX And mousePosition.Y >= exitY And mousePosition.X <= exitX + exitWidth And mousePosition.Y <= exitY + exitHeight Then
                Graphics.DrawBitmap(GameImage("menuexit"), 560, 0)
            ElseIf mousePosition.X >= storyX And mousePosition.Y >= storyY And mousePosition.X <= storyX + storyWidth And mousePosition.Y <= storyY + storyHeight Then
                Graphics.DrawBitmap(GameImage("menustory"), 560, 0)
            ElseIf mousePosition.X >= creditsX And mousePosition.Y >= creditsY And mousePosition.X <= creditsX + creditsWidth And mousePosition.Y <= creditsY + creditsHeight Then
                Graphics.DrawBitmap(GameImage("menucredits"), 560, 0)
            Else
                Graphics.DrawBitmap(GameImage("menunone"), 560, 0)
            End If
        ElseIf subMenu = 1 Then
            Graphics.DrawBitmap(GameImage("storyImage"), 0, 0)
        ElseIf subMenu = 2 Then
            Graphics.DrawBitmap(GameImage("creditsImage"), 0, 0)
        End If

        'Load level
        If Input.WasKeyTyped(SwinGame.Keys.VK_L) Then
            Form.LoadFile.ShowDialog()
            file_string = Form.LoadFile.FileName
            If (Not file_string = "") Then
                LoadLevel(file_string)
                runGame = True
                level = -1
                Exit Sub
            End If
        End If

    End Sub

    Private Sub Game()

        'Set FPS to 50, ignore for fast mode
        While stepTimer.Ticks < 20 And fastMode = False
            'While stepTimer.Ticks < 80 And fastMode = False
        End While
        stepTimer.Stop()
        stepTimer.Start()

        'Clears the Screen to Black
        'SwinGame.Graphics.ClearScreen()

        'Gets someKeyboard input (W,S,A,D, mouse and Q,E)
        'moving and weapon changing
        GetInput()

        'Main execution bits, different for forward or backwards
        'FastMode checking is always done forward
        If restartLevel Then
            Core.RefreshScreen()
            'Text.DrawText(restartText, Color.Red, GameFont("Large"), 300, 300)
            If Input.WasKeyTyped(SwinGame.Keys.VK_SPACE) Then ' restart level
                LoadLevel(file_string)
            End If
        Else
            If time_speed > 0 Or fastMode Then
                RunTimeForward()
            Else
                RunTimeBackward()
            End If
        End If

        CheckInterface()

        'Draws the win text
        If won Then
            Text.DrawText("Level Completed", Color.Green, GameFont("Large"), 220, 280)
            If Not level = -1 Then
                Text.DrawText("Press Space to Continue", Color.Green, GameFont("Medium"), 350, 380)
                'go to next level if space is pressed
                If Input.WasKeyTyped(SwinGame.Keys.VK_SPACE) Then
                    level += 1
                    file_string = String.Format(gamePath + "/levels/Level{0}.lvl", level)
                    LoadLevel(file_string)
                End If
            Else
                Text.DrawText("Press L to Load", Color.Green, GameFont("Medium"), 350, 380)
            End If
        End If

    End Sub

    Private Sub RunTimeForward()

        Dim i As Integer

        If Not pauseGame Then

            'Check state of switch instances (on or off)
            For i = 0 To switchCount - 1
                SwitchCheck(i, True)
            Next

            'Check logic gates
            For i = 0 To gatecount - 1
                GateLogic(i)
            Next

            'Move platform instances
            For i = 0 To platCount - 1
                MovePlatform(i)
            Next

            'Move Portals
            For i = 0 To portalCount - 1
                MovePortal(i)
            Next
            MoveEndPortal()

            'Move switch instances
            For i = 0 To switchCount - 1
                MoveSwitch(i)
            Next

            'DMove pickup instances
            For i = 0 To pickupCount - 1
                MovePickup(i)
            Next

            'Move spike
            For i = 0 To spikeCount - 1
                MoveSpike(i)
            Next

            ' move guy instances
            For i = 0 To guyCount
                'Check guy exsistance at the current time
                If (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) Then
                    If guy(i).t_direction Then
                        MoveGuy(i)
                        If playerGuy = i Then
                            guy(i).exist(time) = True
                        End If
                    Else
                        'Check reverse time instances for paradoxes
                        CheckBackwardsGuy(i)
                    End If
                End If
            Next

            'Move box instances
            For i = 1 To boxCount
                'Check box exsistance at the current time
                If (box(i).t_start <= time And (box(i).t_end >= time Or box(i).t_end = 0)) Then
                    MoveBox(i)
                End If
            Next
        End If

        'pause
        If Input.WasKeyTyped(SwinGame.Keys.VK_P) Then
            pauseGame = Not pauseGame
            manualPauseGame = pauseGame
        End If
        'pause
        If Input.MouseWasClicked(SwinGame.MouseButton.LeftButton) Then
            If mousePosition.X >= 190 And mousePosition.X <= 290 And mousePosition.Y >= 613 And mousePosition.Y <= 658 Then
                pauseGame = Not pauseGame
                manualPauseGame = pauseGame
            End If
        End If

        'play reverse sounds
        PlaySoundsForwards()

        'draw
        If Not restartLevel Then
            DrawEverything(False)
        End If

        'Draw player marker, prevents some confusion
        If (Not fastMode) And (Not winCheck) Then
            Graphics.DrawBitmap(GameImage("arrow"), guy(playerGuy).x(time) + 1, guy(playerGuy).y(time) - 30)
        End If

        'check for paradoxes
        For i = 0 To guyCount
            If Not playerGuy = i Then
                'check end time paradox
                If guy(i).t_end = time Then
                    'checks position and carry paradox at the end of an instances time
                    CheckEndParadoxGuy(i)
                End If
                'check shoot and target
                If Not guy(i).laser_x(time) = 0 Then
                    CheckGunShootGuy(i)
                End If
            End If
        Next

        'Draw interface
        DrawInterface()

        'Draw Timeline
        If TimelineMode = 0 Then
            DrawTimelineAbsolute()
        ElseIf TimelineMode = 1 Then
            DrawTimelineRelative()
        End If

        ' do time editing after object actions
        If fastMode Or won Then
            If time_aim <= time And Not winCheck Then
                'disable fast mode if aim is reached
                fastMode = False
                If (Not playerGuyStore = -1) Then
                    're-active player control over playerGuy
                    playerGuy = playerGuyStore
                    playerGuyStore = -1
                End If

            End If

            ' increase time  
            If Not pauseGame Then
                If fastMode Then
                    time = (time + 1) 'add time regardless of direction
                Else
                    time = (time + time_speed)
                End If

                If time < 1 Then
                    time = 1
                End If
                If time > maxTime Then
                    time = maxTime
                End If
            End If

        Else

            If Input.WasKeyTyped(SwinGame.Keys.VK_SPACE) Then ' check portal travel if space is pressed
                CheckPortalTravel()
                CheckEndPortal()
            End If

            If Input.MouseWasClicked(SwinGame.MouseButton.LeftButton) And mousePosition.Y < 608 Then 'check pickup time travel
                If weapon = 1 Then
                    CheckTimeJumpTravel()
                ElseIf weapon = 2 Then
                    CheckGunShootPlayerGuy()
                    pauseGame = manualPauseGame
                ElseIf weapon = 3 Then
                    CheckReverseTime()
                End If
            End If

            If weapon = 2 Then
                If Input.IsMouseDown(SwinGame.MouseButton.LeftButton) Then
                    If weapon = 2 Then
                        CollisionLine(playerGuy, guy(playerGuy).x(time) + 12, guy(playerGuy).y(time) + 12, mousePosition.X, mousePosition.Y)
                        Graphics.DrawLine(Color.Yellow, guy(playerGuy).x(time) + 12, guy(playerGuy).y(time) + 12, return2, return3)
                        If (Not pauseGame) And shootPause Then
                            pauseGame = True
                        End If
                    End If
                End If

                If Input.IsMouseDown(SwinGame.MouseButton.RightButton) Then 'Aim the time gun in the time dimension

                    If TimelineMode = 0 Then
                        shootTime = ((mousePosition.X - 48) / TimelineScale)
                    ElseIf TimelineMode = 1 Then
                        shootTime = ((mousePosition.X - 512) / TimelineScale + time)
                    End If
                End If
            End If

            ' increase time  
            If Not pauseGame Then
                time = time + time_speed
                If time < 1 Then
                    time = 1
                End If
                If time > maxTime Then
                    time = maxTime
                End If
            End If

        End If

        If time > maxCheckTime Then
            maxCheckTime = time + 50 'set max check time
            If maxCheckTime > maxTime Then
                maxCheckTime = maxTime
            End If
            If winCheck = True Then
                won = True 'if max check time is reached and it's a win check then win
            End If
        End If

    End Sub

    Private Sub RunTimeBackward()

        Dim i As Integer

        If Not pauseGame Then
            'Switches, Portals, Pickups and Spikes do not remember position info. 
            'It is updated from platform position if they are attached
            For i = 0 To switchCount - 1
                MoveSwitch(i)
            Next

            For i = 0 To portalCount - 1
                MovePortal(i)
            Next
            MoveEndPortal()

            For i = 0 To pickupCount - 1
                MovePickup(i)
            Next

            'Move spike
            For i = 0 To spikeCount - 1
                MoveSpike(i)
            Next

            ' move guy instances
            For i = 0 To guyCount
                'Only moves backwards guys, forward guys are purely recalled from past
                If (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) And Not guy(i).t_direction Then
                    MoveGuyBackwards(i)
                    If playerGuy = i Then
                        guy(i).exist(time) = True
                    End If
                End If
            Next
        End If

        'pause
        If Input.WasKeyTyped(SwinGame.Keys.VK_P) Then
            pauseGame = Not pauseGame
            manualPauseGame = pauseGame
        End If
        'Pause
        If Input.MouseWasClicked(SwinGame.MouseButton.LeftButton) Then
            If mousePosition.X >= 190 And mousePosition.X <= 290 And mousePosition.Y >= 613 And mousePosition.Y <= 658 Then
                pauseGame = Not pauseGame
                manualPauseGame = pauseGame
            End If
        End If

        'draws everything
        If Not restartLevel Then
            DrawEverything(True)
        End If

        'sounds
        PlaySoundsBackwards()

        'check for paradoxes
        For i = 0 To guyCount
            If Not playerGuy = i Then
                'check end time paradox
                If guy(i).t_end = time Then
                    'checks position and carry paradox at the end of an instances time
                    CheckEndParadoxGuy(i)
                End If
                'check shoot and target
                If Not guy(i).laser_x(time) = 0 Then
                    CheckGunShootGuy(i)
                End If
            End If
        Next

        'Draw interface
        DrawInterface()

        'Draw Timeline
        If TimelineMode = 0 Then
            DrawTimelineAbsolute()
        ElseIf TimelineMode = 1 Then
            DrawTimelineRelative()
        End If

        'Draw player marker, prevents some confusion
        If (Not fastMode) And (Not winCheck) Then
            Graphics.DrawBitmap(GameImage("arrow"), guy(playerGuy).x(time) + 1, guy(playerGuy).y(time) - 30)
        End If

        ' do time editing after object actions

        If Not (fastMode Or winCheck) Then
            If Input.WasKeyTyped(SwinGame.Keys.VK_SPACE) Then ' check portal travel if space is pressed
                CheckPortalTravel()
                CheckEndPortal()
            End If
            If Input.MouseWasClicked(SwinGame.MouseButton.LeftButton) And mousePosition.Y < 608 Then 'check pickup time travel
                If weapon = 1 Then
                    CheckTimeJumpTravel()
                ElseIf weapon = 2 Then
                    CheckGunShootPlayerGuy()
                    pauseGame = manualPauseGame
                ElseIf weapon = 3 Then
                    CheckReverseTime()
                End If
            End If

            If weapon = 2 Then
                If Input.IsMouseDown(SwinGame.MouseButton.LeftButton) Then
                    If weapon = 2 Then
                        CollisionLine(playerGuy, guy(playerGuy).x(time) + 12, guy(playerGuy).y(time) + 12, mousePosition.X, mousePosition.Y)
                        Graphics.DrawLine(Color.Yellow, guy(playerGuy).x(time) + 12, guy(playerGuy).y(time) + 12, return2, return3)
                        If (Not pauseGame) And shootPause Then
                            pauseGame = True
                        End If
                    End If
                End If

                If Input.IsMouseDown(SwinGame.MouseButton.RightButton) Then 'Aim the time gun in the time dimension

                    If TimelineMode = 0 Then
                        shootTime = ((mousePosition.X - 48) / TimelineScale)
                    ElseIf TimelineMode = 1 Then
                        shootTime = ((mousePosition.X - 512) / TimelineScale + time)
                    End If
                End If
            End If

            ' increase time  
            If Not pauseGame Then
                time = time + time_speed
                If time < 1 Then
                    time = 1
                End If
                If time > maxTime Then
                    time = maxTime
                End If
            End If

        End If

    End Sub

    Private Sub GetInput()

        mousePosition = Input.GetMousePosition()

        ' input for the player guy
        If fastMode = False And pauseGame = False Then
            'set the move input for the player instance
            If Input.IsKeyPressed(SwinGame.Keys.VK_A) Then
                guy(playerGuy).k_left(time) = True
            End If
            If Input.IsKeyPressed(SwinGame.Keys.VK_D) Then
                guy(playerGuy).k_right(time) = True
            End If
            If Input.IsKeyPressed(SwinGame.Keys.VK_W) Then
                guy(playerGuy).k_up(time) = True
            End If
            If Input.WasKeyTyped(SwinGame.Keys.VK_S) Then
                guy(playerGuy).k_down(time) = True
            End If
        End If

        'cycle weapons
        If Input.WasKeyTyped(SwinGame.Keys.VK_Q) Then
            weapon = weapon - 1
            If weapon < 1 Then
                weapon = weapons
            End If
        End If
        If Input.WasKeyTyped(SwinGame.Keys.VK_E) Then
            weapon = weapon + 1
            If weapon > weapons Then
                weapon = 1
            End If
        End If

    End Sub

    Private Sub MoveGuy(ByVal id As Integer)

        If time = guy(id).t_start + 1 Then
            'if they guy portal travelled set position realative to current portal postion
            If (Not id = 0) Then
                If guy(id - 1).t_end_type = 1 Or guy(id - 1).t_end_type = 2 Then
                    guy(id).x(time - 1) = guy(id - 1).x_par(guy(id - 1).t_end) + portal(guy(id - 1).t_end_portal_id).x
                    guy(id).y(time - 1) = guy(id - 1).y_par(guy(id - 1).t_end) + portal(guy(id - 1).t_end_portal_id).y
                End If
            End If
            'check creation inside platforms/walls, ie chronofragging
            CheckChronofragGuy(id, 1)
        End If

        Dim j As Integer
        Dim jump As Boolean 'allowed to jump? active if collided with wall or top of a platform
        Dim squishCheck = False 'should squish be checked, active if collided with platform or box

        'reset audio
        If guy(id).playSound(time + pickup_pickup_length) = "pickup_pickup_rev" Then
            guy(id).playSound(time + pickup_pickup_length) = ""
        End If
        If guy(id).playSound(time + box_pickup_length) = "box_pickup_rev" Then
            guy(id).playSound(time + box_pickup_length) = ""
        End If
        If guy(id).playSound(time + guy_hit_length) = "guy_hit_rev" Then
            guy(id).playSound(time + guy_hit_length) = ""
        End If

        '' SET PROPROSED SPEED
        'check left/right input
        If guy(id).k_left(time) Then
            guy(id).xspeed(time) = -2.2
            guy(id).face = True
        ElseIf guy(id).k_right(time) Then
            guy(id).xspeed(time) = 2.2
            guy(id).face = False
        Else
            guy(id).xspeed(time) = 0
        End If

        'store animation sub for reverse travel
        guy(id).face_store(time) = guy(id).face 'stores face for backwards drawing
        'sitting on box/platform
        guy(id).supported_obj(time) = 0

        'add gravity 
        guy(id).yspeed(time) = guy(id).yspeed(time - 1) + gravity

        Dim newX = guy(id).x(time - 1)
        Dim newY = guy(id).y(time - 1)

        'This bit stops Y directional Platform launches
        If guy(id).supported_obj(time - 1) = 1 Then
            If plat(guy(id).supported_id(time - 1)).y(time) - 33 - plat(guy(id).supported_id(time - 1)).yspeed(time) < newY Then
                guy(id).yspeed(time) = (plat(guy(id).supported_id(time - 1)).yspeed(time) + guy(id).yspeed(time))
            End If
        End If

        'CHECK COLLISIONS USING PROPOSED SPEED
        jump = False

        ' check platform collision in y direction
        For j = 0 To platCount - 1
            If guy(id).yspeed(time) - plat(j).yspeed(time) > 0 Then
                If newX + guy_width > plat(j).x(time) And newX < plat(j).x(time) + plat(j).width And newY + guy(id).yspeed(time) + 32 > plat(j).y(time) And newY + guy(id).yspeed(time) + 32 < plat(j).y(time) + plat(j).height Then
                    If plat(j).yspeed(time) > 0 Then
                        If guy(id).yspeed(time) > gravity + plat(j).yspeed(time) + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                            Audio.PlaySoundEffect(GameSound("guy_hit"))
                            guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                        End If
                        newY = (plat(j).y(time) - 32) - plat(j).yspeed(time)
                        guy(id).yspeed(time) = plat(j).yspeed(time)
                    Else
                        If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                            Audio.PlaySoundEffect(GameSound("guy_hit"))
                            guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                        End If
                        newY = (plat(j).y(time) - 32)
                        guy(id).yspeed(time) = 0
                    End If
                    guy(id).supported_obj(time) = 1
                    guy(id).supported_id(time) = j
                    guy(id).xspeed(time) = guy(id).xspeed(time) + plat(j).xspeed(time)
                    squishCheck = True
                    jump = True
                End If
            ElseIf guy(id).yspeed(time) - plat(j).yspeed(time) < 0 Then
                If newX + guy_width > plat(j).x(time) And newX < plat(j).x(time) + plat(j).width And newY + guy(id).yspeed(time) > plat(j).y(time) And newY + guy(id).yspeed(time) < plat(j).y(time) + plat(j).height Then
                    newY = plat(j).y(time) + plat(j).height
                    If plat(j).yspeed(time) > 0 Then
                        guy(id).yspeed(time) = plat(j).yspeed(time)
                    Else
                        guy(id).yspeed(time) = 0
                    End If
                    squishCheck = True
                End If
            End If
        Next

        ' check wall collision in y direction
        If guy(id).yspeed(time) > 0 Then
            If (newX) - Math.Floor((newX) / 32) * 32 <= guy_width_left Then
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time) + 32) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32
                    If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                        Audio.PlaySoundEffect(GameSound("guy_hit"))
                        guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                    End If
                    guy(id).yspeed(time) = 0
                    jump = True
                End If
            Else
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time) + 32) / 32)) Or wall(Math.Floor((newX + guy_width) / 32), Math.Floor((newY + guy(id).yspeed(time) + 32) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32
                    If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                        Audio.PlaySoundEffect(GameSound("guy_hit"))
                        guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                    End If
                    guy(id).yspeed(time) = 0
                    jump = True
                End If
            End If
        ElseIf guy(id).yspeed(time) < 0 Then
            If (newX) - Math.Floor((newX) / 32) * 32 <= guy_width_left Then
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time)) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32 + 32
                    guy(id).yspeed(time) = 0
                End If
            Else
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time)) / 32)) Or wall(Math.Floor((newX + guy_width) / 32), Math.Floor((newY + guy(id).yspeed(time)) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32 + 32
                    guy(id).yspeed(time) = 0
                End If
            End If
        End If

        'check box collision in y direction
        For j = 1 To boxCount
            If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                If guy(id).yspeed(time) > box(j).yspeed(time - 1) Then
                    If newX + guy_width > box(j).x(time - 1) And newX < box(j).x(time - 1) + 32 And newY + guy(id).yspeed(time) + 32 > box(j).y(time - 1) + box(j).yspeed(time - 1) And newY + guy(id).yspeed(time - 1) + 22 < box(j).y(time - 1) + box(j).yspeed(time) Then
                        If box(j).supported_obj(time - 1) = 1 Or box(j).supported_obj(time - 1) = 2 Then
                            If plat(box(j).supported_id(time - 1)).yspeed(time) > 0 Then
                                If guy(id).yspeed(time) > gravity + plat(guy(j).supported_id(time - 1)).yspeed(time) + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                                    Audio.PlaySoundEffect(GameSound("guy_hit"))
                                    guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                                End If
                                newY = box(j).y(time - 1) - 32
                                guy(id).yspeed(time) = plat(box(j).supported_id(time - 1)).yspeed(time)
                            Else
                                If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                                    Audio.PlaySoundEffect(GameSound("guy_hit"))
                                    guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                                End If
                                newY = box(j).y(time - 1) - 32 + plat(box(j).supported_id(time - 1)).yspeed(time)
                                guy(id).yspeed(time) = 0
                            End If
                        Else
                            If guy(id).yspeed(time) > gravity + 0.05 Then
                                Audio.PlaySoundEffect(GameSound("guy_hit"))
                                guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                            End If
                            If box(j).yspeed(time - 1) > 0 Then
                                newY = box(j).y(time - 1) - 32
                                guy(id).yspeed(time) = box(j).yspeed(time - 1)
                            Else
                                newY = box(j).y(time - 1) - 32
                                guy(id).yspeed(time) = 0
                            End If
                        End If
                        guy(id).xspeed(time) = box(j).xspeed(time - 1) + guy(id).xspeed(time)
                        squishCheck = True
                        jump = True
                    End If
                End If
            End If
        Next

        ' check wall collision in x direction 
        If guy(id).xspeed(time) > 0 Then ' only check for wall on right
            If Math.Floor((newY) / 32) = (newY) / 32 Then ' check for snapping to grid
                If wall(Math.Floor((newX + guy(id).xspeed(time) + guy_width) / 32), Math.Floor((newY) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + guy_width_left
                    guy(id).xspeed(time) = 0
                End If
            Else
                If wall(Math.Floor((newX + guy(id).xspeed(time) + guy_width) / 32), Math.Floor((newY) / 32)) Or wall(Math.Floor((newX + guy(id).xspeed(time) + guy_width) / 32), Math.Floor((newY + 32) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + guy_width_left
                    guy(id).xspeed(time) = 0
                End If
            End If
        ElseIf guy(id).xspeed(time) < 0 Then
            If Math.Floor((newY) / 32) = (newY) / 32 Then
                If wall(Math.Floor((newX + guy(id).xspeed(time)) / 32), Math.Floor((newY) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + 32
                    guy(id).xspeed(time) = 0
                End If
            Else
                If wall(Math.Floor((newX + guy(id).xspeed(time)) / 32), Math.Floor((newY) / 32)) Or wall(Math.Floor((newX + guy(id).xspeed(time)) / 32), Math.Floor((newY + 32) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + 32
                    guy(id).xspeed(time) = 0
                End If
            End If
        End If

        ' check platform collision in x direction
        For j = 0 To platCount - 1
            If guy(id).xspeed(time) - plat(j).xspeed(time) > 0 Then
                If newX + guy(id).xspeed(time) + guy_width > plat(j).x(time) And newX + guy(id).xspeed(time) + guy_width < plat(j).x(time) + plat(j).width And newY < plat(j).y(time) + plat(j).height And newY + 32 > plat(j).y(time) Then
                    newX = plat(j).x(time) - guy_width - plat(j).xspeed(time)
                    guy(id).xspeed(time) = plat(j).xspeed(time)
                    squishCheck = True
                End If
            ElseIf guy(id).xspeed(time) - plat(j).xspeed(time) < 0 Then
                If newX + guy(id).xspeed(time) > plat(j).x(time) And newX + guy(id).xspeed(time) < plat(j).x(time) + plat(j).width And newY < plat(j).y(time) + plat(j).height And newY + 32 > plat(j).y(time) Then
                    guy(id).xspeed(time) = plat(j).xspeed(time)
                    newX = plat(j).x(time) + plat(j).width - plat(j).xspeed(time)
                    squishCheck = True
                End If
            End If
        Next

        ' jump
        If jump Then
            If guy(id).k_up(time) Then
                guy(id).yspeed(time) = -4.2
                'play jump sound AUDIO
            End If
        End If

        ' apply the position changes
        guy(id).x(time) = newX + guy(id).xspeed(time)
        guy(id).y(time) = newY + guy(id).yspeed(time)

        '' TIME EVENTS, PARADOXES
        'check paradox caused by pickups that are not there
        If Not (time = 1 Or time = maxTime) Then
            'Check if a pickup picked up in the past is still there
            If Not id = playerGuy Then
                If Not guy(id).pickupCheck(time) = 0 Then
                    If CheckPickupParadox(id, guy(id).pickupCheck(time) - 1, guy(id).pickup_id(time)) Then
                        Audio.PlaySoundEffect(GameSound("pickup_pickup"))
                        guy(id).playSound(time + pickup_pickup_length) = "pickup_pickup_rev"
                    End If
                End If
            End If
            'check new pickup collision
            For j = 0 To pickupCount - 1
                If (pickup(j).t_end > time Or pickup(j).t_end = 0) And (guy(id).pickup_time(j) = 0 Or guy(id).pickup_time(j) < time) Then
                    If guy(id).x(time) + guy_width > pickup(j).x + pickup(j).width / 2 And guy(id).x(time - 1) < pickup(j).x + pickup(j).width / 2 And guy(id).y(time) + 32 > pickup(j).y + pickup(j).height / 2 And guy(id).y(time - 1) < pickup(j).y + pickup(j).height / 2 Then
                        Audio.PlaySoundEffect(GameSound("pickup_pickup"))
                        guy(id).playSound(time + pickup_pickup_length) = "pickup_pickup_rev"
                        pickup(j).t_end = time + 1 'end pickup
                        guy(id).pickupCheck(time) = pickup(j).type + 1 'set paradox checking for pickups
                        guy(id).pickup_id(time) = j 'store ID to prevent future pickups of the same type by the same instance
                        guy(id).pickup_time(j) = time 'store Time to prevent future pickups of the same type by the same instance
                        If pickup(j).type = 0 Then
                            timeJumps = timeJumps + 1
                        ElseIf pickup(j).type = 1 Then
                            timeGuns = timeGuns + 1
                        ElseIf pickup(j).type = 2 Then
                            timeReverses = timeReverses + 1
                        End If
                        Exit For 'makes sure that only 1 pickup is collected per frame.
                    End If
                End If
            Next
        End If

        ''HANDEL BOX CARRYING
        'Check Box pickup/putdown
        If guy(id).k_down(time) Then
            If guy(id).carry(time - 1) = 0 Then
                For j = 1 To boxCount
                    'check for nearby boxes
                    If guy(id).x(time) + guy_width > box(j).x(time - 1) And guy(id).x(time) < box(j).x(time - 1) + 32 And guy(id).y(time) + 16 > box(j).y(time - 1) + 10 And guy(id).y(time) + 16 < box(j).y(time - 1) + 22 Then
                        'check for un-held boxes
                        If box(j).carry(time - 1) = False Then
                            'pickup box
                            Audio.PlaySoundEffect(GameSound("box_pickup"))
                            guy(id).playSound(time + box_pickup_length) = "box_pickup_rev"
                            box(j).carry(time - 1) = True
                            box(j).carry_id = id
                            box(j).x(time) = guy(id).x(time) - 4
                            box(j).y(time) = guy(id).y(time) - 32
                            box(j).carry(time - 1) = True
                            guy(id).carry(time) = j
                            Exit For
                        End If
                    End If
                    guy(id).carry(time) = guy(id).carry(time - 1)
                Next
            Else
                'drop box
                If CheckBoxDrop(guy(id).x(time) - 4, guy(id).y(time) - 32, guy(id).carry(time - 1)) Then
                    box(guy(id).carry(time - 1)).x(time - 1) = return1
                    box(guy(id).carry(time - 1)).y(time - 1) = return2
                    box(guy(id).carry(time - 1)).carry(time - 1) = False
                    box(guy(id).carry(time - 1)).carry_id = -1
                    guy(id).carry(time) = 0
                Else
                    'if down is not pressed update box/carry status
                    guy(id).carry(time) = guy(id).carry(time - 1)
                    If Not guy(id).carry(time) = 0 Then
                        box(guy(id).carry(time)).carry_id = id
                        box(guy(id).carry(time)).x(time) = guy(id).x(time) - 4
                        box(guy(id).carry(time)).y(time) = guy(id).y(time) - 32
                        box(guy(id).carry(time)).carry(time - 1) = True
                    End If
                End If
            End If
        Else
            'if down is not pressed update box/carry status
            guy(id).carry(time) = guy(id).carry(time - 1)
            If Not guy(id).carry(time) = 0 Then
                box(guy(id).carry(time)).carry_id = id
                box(guy(id).carry(time)).x(time) = guy(id).x(time) - 4
                box(guy(id).carry(time)).y(time) = guy(id).y(time) - 32
                box(guy(id).carry(time)).carry(time - 1) = True
            End If
        End If

        ' end box if it's portal time
        If guy(id).t_end = time Then
            If Not guy(id).carry(time) = 0 Then
                box(guy(id).carry(time)).t_end = time
            End If
        End If

        ''CHECK DEATH
        'sharp and pointy 
        For j = 0 To spikeCount - 1
            If guy(id).x(time) + guy_width > spike(j).x And guy(id).x(time) < spike(j).x + spike(j).width And guy(id).y(time) + 32 > spike(j).y And guy(id).y(time) < spike(j).y + spike(j).height Then
                Graphics.DrawRectangle(Color.Red, False, guy(id).x(time), guy(id).y(time), guy_width, 32)
                Graphics.DrawRectangle(Color.Red, False, guy(id).x(time) - 1, guy(id).y(time) - 1, guy_width + 2, 34)
                Text.DrawText("Oh No! You've been", Color.Red, GameFont("Medium"), 260, 260)
                Text.DrawText("Spiked", Color.Red, GameFont("Large"), 300, 300)
                Text.DrawText("Press Space or R to continue", Color.Red, GameFont("Medium"), 350, 400)
                Audio.PlaySoundEffect(GameSound("spiked"))
                restartLevel = True
                Exit Sub
            End If
        Next

        'squishy
        If squishCheck Then
            'Check poking into walls which means a platform pushed it there
            If Math.Floor((guy(id).y(time)) / 32) = (guy(id).y(time)) / 32 Then
                If (guy(id).x(time - 1)) - Math.Floor((guy(id).x(time - 1)) / 32) * 32 <= guy_width_left Then
                    If wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Then
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1) - 1, guy(id).y(time - 1) - 1)
                        Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                        Audio.PlaySoundEffect(GameSound("squished"))
                        restartLevel = True
                        Exit Sub
                    End If

                Else
                    If wall(Math.Floor((guy(id).x(time) + guy_width) / 32), Math.Floor((guy(id).y(time)) / 32)) Or wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Then
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1) - 1, guy(id).y(time - 1) - 1)
                        Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                        Audio.PlaySoundEffect(GameSound("squished"))
                        restartLevel = True
                        Exit Sub
                    End If
                End If
            Else
                If (guy(id).x(time - 1)) - Math.Floor((guy(id).x(time - 1)) / 32) * 32 <= guy_width_left Then
                    If wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32 + 1)) Or wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Then
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1) - 1, guy(id).y(time - 1) - 1)
                        Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                        Audio.PlaySoundEffect(GameSound("squished"))
                        restartLevel = True
                        Exit Sub
                    End If
                Else
                    If wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Or wall(Math.Floor((guy(id).x(time) + guy_width) / 32), Math.Floor((guy(id).y(time)) / 32)) Or wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32) + 1) Or wall(Math.Floor((guy(id).x(time) + guy_width) / 32), Math.Floor((guy(id).y(time)) / 32) + 1) Then
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1) - 1, guy(id).y(time - 1) - 1)
                        Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                        Audio.PlaySoundEffect(GameSound("squished"))
                        restartLevel = True
                        Exit Sub
                    End If
                End If
            End If

            'Check poking into platforms this should only be caused by other platforms
            For j = 0 To platCount - 1
                If guy(id).x(time) + guy_width > plat(j).x(time) And guy(id).x(time) < plat(j).x(time) + plat(j).width And guy(id).y(time) < plat(j).y(time) + plat(j).height And guy(id).y(time) + 32 > plat(j).y(time) Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1) - 1, guy(id).y(time - 1) - 1)
                    Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                    Audio.PlaySoundEffect(GameSound("squished"))
                    restartLevel = True
                    Exit Sub
                End If
            Next

        End If

    End Sub

    Private Sub MoveGuyBackwards(ByVal id As Integer)
        'Moves when travelling backwards
        'Main distance is refferencing (time + 1) instead of (time - 1)
        'and more stringent paradox check storing

        If time = guy(id).t_start - 1 Then
            'spawn at the new portal postion if the travel method was a portal
            If (Not id = 0) Then
                If guy(id - 1).t_end_type = 1 Or guy(id - 1).t_end_type = 2 Then
                    guy(id).x(time + 1) = guy(id - 1).x_par(guy(id - 1).t_end) + portal(guy(id - 1).t_end_portal_id).x
                    guy(id).y(time + 1) = guy(id - 1).y_par(guy(id - 1).t_end) + portal(guy(id - 1).t_end_portal_id).y
                End If
            End If
            'check creation inside platforms
            CheckChronofragGuy(id, -1)
        End If

        Dim j As Integer
        Dim jump As Boolean 'allowed to jump? active if collided with wall or platform on the bottom
        Dim squishCheck = False 'should squish be checked, active is collided with platform

        'reset audio
        If time - pickup_pickup_length > 1 Then
            If guy(id).playSound(time - pickup_pickup_length) = "pickup_pickup_rev" Then
                guy(id).playSound(time - pickup_pickup_length) = ""
            End If
        Else
            If guy(id).playSound(1) = "pickup_pickup_rev" Then
                guy(id).playSound(1) = ""
            End If
        End If
        If time - box_pickup_length > 1 Then
            If guy(id).playSound(time - box_pickup_length) = "box_pickup_rev" Then
                guy(id).playSound(time - box_pickup_length) = ""
            End If
        Else
            If guy(id).playSound(1) = "box_pickup_rev" Then
                guy(id).playSound(1) = ""
            End If
        End If
        If time - guy_hit_length > 1 Then
            If guy(id).playSound(time + guy_hit_length) = "guy_hit_rev" Then
                guy(id).playSound(time + guy_hit_length) = ""
            End If
        Else
            If guy(id).playSound(time + guy_hit_length) = "guy_hit_rev" Then
                guy(id).playSound(time + guy_hit_length) = ""
            End If
        End If
        

        '' SET PROPROSED SPEED
        'check left/right input
        If guy(id).k_left(time) Then
            guy(id).xspeed(time) = -2.2
            guy(id).face = True
        ElseIf guy(id).k_right(time) Then
            guy(id).xspeed(time) = 2.2
            guy(id).face = False
        Else
            guy(id).xspeed(time) = 0
        End If

        guy(id).face_store(time) = guy(id).face 'store face for forwards drawing
        guy(id).supported_obj(time) = 0

        'add gravity 
        guy(id).yspeed(time) = guy(id).yspeed(time + 1) + gravity

        Dim newX = guy(id).x(time + 1)
        Dim newY = guy(id).y(time + 1)

        'Stop Y dir platform launch
        If guy(id).supported_obj(time + 1) = 1 Then
            If plat(guy(id).supported_id(time + 1)).y(time) - 33 + plat(guy(id).supported_id(time + 1)).yspeed(time) < newY Then
                guy(id).yspeed(time) = -plat(guy(id).supported_id(time + 1)).yspeed(time) + guy(id).yspeed(time)
            End If
        End If

        'CHECK COLLISIONS USING PROPOSED SPEED
        jump = False

        ' check platform collision in y direction
        For j = 0 To platCount - 1
            If guy(id).yspeed(time) + plat(j).yspeed(time) > 0 Then
                If newX + guy_width > plat(j).x(time) And newX < plat(j).x(time) + plat(j).width And newY + guy(id).yspeed(time) + 32 > plat(j).y(time) And newY + guy(id).yspeed(time) + 32 < plat(j).y(time) + plat(j).height Then
                    If (plat(j).yspeed(time) < 0) Then
                        If guy(id).yspeed(time) > gravity - plat(j).yspeed(time) + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                            Audio.PlaySoundEffect(GameSound("guy_hit"))
                            guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                        End If
                        newY = plat(j).y(time) - 32 + plat(j).yspeed(time)
                        guy(id).yspeed(time) = -plat(j).yspeed(time)
                    Else
                        If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                            Audio.PlaySoundEffect(GameSound("guy_hit"))
                            guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                        End If
                        newY = plat(j).y(time) - 32
                        guy(id).yspeed(time) = 0
                    End If
                    guy(id).xspeed(time) = guy(id).xspeed(time) - plat(j).xspeed(time)
                    guy(id).supported_obj(time) = 1
                    guy(id).supported_id(time) = j
                    jump = True
                    squishCheck = True
                    guy(id).x_par_s(time) = newX
                    guy(id).y_par_s(time) = plat(j).y(time)
                    guy(id).xspeed_par_s(time) = plat(j).xspeed(time)
                    guy(id).yspeed_par_s(time) = plat(j).yspeed(time)
                    guy(id).dir_par_s(time) = 1
                    guy(id).object_par_s(time) = 1
                End If
            ElseIf guy(id).yspeed(time) + plat(j).yspeed(time) < 0 Then
                If newX + guy_width > plat(j).x(time) And newX < plat(j).x(time) + plat(j).width And newY + guy(id).yspeed(time) > plat(j).y(time) And newY + guy(id).yspeed(time) < plat(j).y(time) + plat(j).height Then
                    newY = plat(j).y(time) + plat(j).height
                    If plat(j).yspeed(time) > 0 Then
                        guy(id).yspeed(time) = -plat(j).yspeed(time)
                    Else
                        guy(id).yspeed(time) = 0
                    End If
                    squishCheck = True
                    guy(id).x_par_s(time) = newX
                    guy(id).y_par_s(time) = (plat(j).y(time) + plat(j).height)
                    guy(id).xspeed_par_s(time) = plat(j).xspeed(time)
                    guy(id).yspeed_par_s(time) = plat(j).yspeed(time)
                    guy(id).object_par_s(time) = 1
                    guy(id).dir_par_s(time) = 3
                End If
            End If
        Next

        ' check wall collision in y direction
        If guy(id).yspeed(time) > 0 Then
            If (newX) - Math.Floor((newX) / 32) * 32 <= guy_width_left Then
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time) + 32) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32
                    If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                        Audio.PlaySoundEffect(GameSound("guy_hit"))
                        guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                    End If
                    guy(id).yspeed(time) = 0
                    jump = True
                End If
            Else
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time) + 32) / 32)) Or wall(Math.Floor((newX + guy_width) / 32), Math.Floor((newY + guy(id).yspeed(time) + 32) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32
                    If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                        Audio.PlaySoundEffect(GameSound("guy_hit"))
                        guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                    End If
                    guy(id).yspeed(time) = 0
                    jump = True
                End If
            End If
        ElseIf guy(id).yspeed(time) < 0 Then
            If (newX) - Math.Floor((newX) / 32) * 32 <= guy_width_left Then
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time)) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32 + 32
                    guy(id).yspeed(time) = 0
                End If
            Else
                If wall(Math.Floor((newX) / 32), Math.Floor((newY + guy(id).yspeed(time)) / 32)) Or wall(Math.Floor((newX + guy_width) / 32), Math.Floor((newY + guy(id).yspeed(time)) / 32)) Then
                    newY = Math.Floor((newY + guy(id).yspeed(time)) / 32) * 32 + 32
                    guy(id).yspeed(time) = 0
                End If
            End If
        End If

        'check box collision in y direction
        For j = 1 To boxCount
            If box(j).carry(time) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                If guy(id).yspeed(time) > -box(j).yspeed(time) Then
                    If newX + guy_width > box(j).x(time) And newX < box(j).x(time) + 32 And newY + guy(id).yspeed(time) + 32 > box(j).y(time) + box(j).yspeed(time) And newY + guy(id).yspeed(time) + 22 < box(j).y(time) + box(j).yspeed(time) Then
                        If box(j).supported_obj(time) = 1 Or box(j).supported_obj(time) = 2 Then
                            If plat(box(j).supported_id(time)).yspeed(time) < 0 Then
                                If guy(id).yspeed(time) > gravity - plat(guy(j).supported_id(time - 1)).yspeed(time) + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                                    Audio.PlaySoundEffect(GameSound("guy_hit"))
                                    guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                                End If
                                newY = box(j).y(time) - 32
                                guy(id).yspeed(time) = -plat(box(j).supported_id(time)).yspeed(time)
                            Else
                                If guy(id).yspeed(time) > gravity + 0.05 And guy(id).supported_obj(time - 1) = 0 Then
                                    Audio.PlaySoundEffect(GameSound("guy_hit"))
                                    guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                                End If
                                newY = box(j).y(time) - 32 + plat(box(j).supported_id(time)).yspeed(time)
                                guy(id).yspeed(time) = 0
                            End If
                        Else
                            If guy(id).yspeed(time) > gravity + 0.05 Then
                                Audio.PlaySoundEffect(GameSound("guy_hit"))
                                guy(id).playSound(time + guy_hit_length) = "guy_hit_rev"
                            End If
                            If box(j).yspeed(time) < 0 Then
                                newY = box(j).y(time) - 32
                                guy(id).yspeed(time) = -box(j).yspeed(time)
                            Else
                                newY = box(j).y(time) - 32
                                guy(id).yspeed(time) = -box(j).yspeed(time)
                            End If
                        End If
                        guy(id).xspeed(time) = -box(j).xspeed(time) + guy(id).xspeed(time)
                        squishCheck = True
                        jump = True
                        guy(id).x_par_s(time) = newX
                        guy(id).y_par_s(time) = box(j).y(time)
                        guy(id).yspeed_par_s(time) = box(j).yspeed(time)
                        guy(id).object_par_s(time) = 2
                    End If
                End If
            End If
        Next

        ' check wall collision in x direction 
        If guy(id).xspeed(time) > 0 Then ' only check for wall on right
            If Math.Floor((newY) / 32) = (newY) / 32 Then ' check for snapping to grid
                If wall(Math.Floor((newX + guy(id).xspeed(time) + guy_width) / 32), Math.Floor((newY) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + guy_width_left
                    guy(id).xspeed(time) = 0
                End If
            Else
                If wall(Math.Floor((newX + guy(id).xspeed(time) + guy_width) / 32), Math.Floor((newY) / 32)) Or wall(Math.Floor((newX + guy(id).xspeed(time) + guy_width) / 32), Math.Floor((newY + 32) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + guy_width_left
                    guy(id).xspeed(time) = 0
                End If
            End If
        ElseIf guy(id).xspeed(time) < 0 Then
            If Math.Floor((newY) / 32) = (newY) / 32 Then
                If wall(Math.Floor((newX + guy(id).xspeed(time)) / 32), Math.Floor((newY) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + 32
                    guy(id).xspeed(time) = 0
                End If
            Else
                If wall(Math.Floor((newX + guy(id).xspeed(time)) / 32), Math.Floor((newY) / 32)) Or wall(Math.Floor((newX + guy(id).xspeed(time)) / 32), Math.Floor((newY + 32) / 32)) Then
                    newX = Math.Floor((newX + guy(id).xspeed(time)) / 32) * 32 + 32
                    guy(id).xspeed(time) = 0
                End If
            End If
        End If

        ' check platform collision in x direction
        For j = 0 To platCount - 1
            If guy(id).xspeed(time) + plat(j).xspeed(time) > 0 Then
                If newX + guy(id).xspeed(time) + guy_width > plat(j).x(time) And newX + guy(id).xspeed(time) + guy_width < plat(j).x(time) + plat(j).width And newY < plat(j).y(time) + plat(j).height And newY + 32 > plat(j).y(time) Then
                    newX = plat(j).x(time) - guy_width - plat(j).xspeed(time)
                    guy(id).xspeed(time) = plat(j).xspeed(time)
                    squishCheck = True
                    guy(id).x_par_s(time) = plat(j).x(time)
                    guy(id).y_par_s(time) = newY
                    guy(id).xspeed_par_s(time) = plat(j).xspeed(time)
                    guy(id).yspeed_par_s(time) = plat(j).yspeed(time)
                    guy(id).dir_par_s(time) = 2
                    guy(id).object_par_s(time) = 1
                End If
            ElseIf guy(id).xspeed(time) + plat(j).xspeed(time) < 0 Then
                If newX + guy(id).xspeed(time) > plat(j).x(time) And newX + guy(id).xspeed(time) < plat(j).x(time) + plat(j).width And newY < plat(j).y(time) + plat(j).height And newY + 32 > plat(j).y(time) Then
                    guy(id).xspeed(time) = plat(j).xspeed(time)
                    newX = plat(j).x(time) + plat(j).width - plat(j).xspeed(time)
                    squishCheck = True
                    guy(id).x_par_s(time) = (plat(j).x(time) + plat(j).width)
                    guy(id).y_par_s(time) = newY
                    guy(id).xspeed_par_s(time) = plat(j).xspeed(time)
                    guy(id).yspeed_par_s(time) = plat(j).yspeed(time)
                    guy(id).dir_par_s(time) = 0
                    guy(id).object_par_s(time) = 1
                End If
            End If
        Next

        ' jump
        If jump Then
            If guy(id).k_up(time) Then
                guy(id).yspeed(time) = -4.2
                'play jump sound AUDIO
            End If
        End If

        ' apply the position changes
        guy(id).x(time) = newX + guy(id).xspeed(time)
        guy(id).y(time) = newY + guy(id).yspeed(time)

        '' TIME EVENTS, PARADOXES
        If Not (time = 1 Or time = maxTime) Then
            'check paradox caused by pickups that were there in the past but not anymore
            If Not id = playerGuy Then
                If Not guy(id).pickupCheck(time) = 0 Then
                    CheckPickupParadox(id, guy(id).pickupCheck(time) - 1, guy(id).pickup_id(time))
                    Audio.PlaySoundEffect(GameSound("pickup_pickup"))
                    If time - pickup_pickup_length > 1 Then
                        guy(id).playSound(time - pickup_pickup_length) = "pickup_pickup_rev"
                    Else
                        guy(id).playSound(1) = "pickup_pickup_rev"
                    End If
                End If
            End If
            'check if an object is being picked up by a guy
            For j = 0 To pickupCount - 1
                'check if the pickup exists and it hasn't been pickedup by the same instance in the future
                If (pickup(j).t_end > time Or pickup(j).t_end = 0) And (guy(id).pickup_time(j) = 0 Or guy(id).pickup_time(j) < time) Then
                    If guy(id).x(time) + guy_width > pickup(j).x + pickup(j).width / 2 And guy(id).x(time + 1) < pickup(j).x + pickup(j).width / 2 And guy(id).y(time) + 32 > pickup(j).y + pickup(j).height / 2 And guy(id).y(time + 1) < pickup(j).y + pickup(j).height / 2 Then
                        Audio.PlaySoundEffect(GameSound("pickup_pickup"))
                        If time - pickup_pickup_length > 1 Then
                            guy(id).playSound(time - pickup_pickup_length) = "pickup_pickup_rev"
                        Else
                            guy(id).playSound(1) = "pickup_pickup_rev"
                        End If
                        pickup(j).t_end = time + 1
                        guy(id).pickupCheck(time) = pickup(j).type + 1
                        guy(id).pickup_id(time) = j
                        guy(id).pickup_time(j) = time
                        If pickup(j).type = 0 Then
                            timeJumps = timeJumps + 1
                        ElseIf pickup(j).type = 1 Then
                            timeGuns = timeGuns + 1
                        ElseIf pickup(j).type = 2 Then
                            timeReverses = timeReverses + 1
                        End If
                        Exit For 'makes sure that only 1 pickup is collected per frame.
                    End If
                End If
            Next
        End If

        ''HANDEL BOX CARRYING
        'Check Box pickup/putdown
        'This is different from Forwards travel, carried boxes do not exist
        'Pickedup boxes are ended because they reverse the time stream
        'When a box should be dropped a box is created as it re-enters the forward time stream
        'Carry has 4 states:
        '-1 just dropped a box
        '0 not carrying a box
        '1 carrying box
        '2 just picked up box
        If guy(id).k_down(time) Then
            If id = playerGuy Then
                If (guy(id).carry(time + 1) = 0) Then
                    For j = 1 To boxCount
                        If (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) And guy(id).x(time) + guy_width > box(j).x(time - 1) And guy(id).x(time) < box(j).x(time - 1) + 32 And guy(id).y(time) + 16 > box(j).y(time - 1) + 10 And guy(id).y(time) + 16 < box(j).y(time - 1) + 22 And Not box(j).carry(time - 1) Then
                            box(j).t_end = time 'ends the box
                            guy(id).carry(time) = 2
                            Audio.PlaySoundEffect(GameSound("box_pickup"))
                            If time - box_pickup_length > 1 Then
                                guy(id).playSound(time - box_pickup_length) = "box_pickup_rev"
                            Else
                                guy(id).playSound(1) = "box_pickup_rev"
                            End If
                            Exit For
                        End If
                        guy(id).carry(time) = guy(id).carry(time + 1)
                    Next
                Else
                    If CheckBoxDrop(guy(id).x(time) - 4, guy(id).y(time) - 32, 0) Then
                        'create a 
                        boxCount += 1
                        ReDimBox(boxCount)
                        box(boxCount).x((time - 1)) = return1
                        box(boxCount).y((time - 1)) = return2
                        box(boxCount).xspeed((time - 1)) = 0
                        box(boxCount).yspeed((time - 1)) = 0
                        box(boxCount).start_gfx = True
                        box(boxCount).end_gfx = True
                        box(boxCount).exist((time - 1)) = True
                        box(boxCount).t_start = time
                        box(boxCount).t_end = 0
                        guy(id).carry(time) = -1
                    End If
                End If
            Else
                If (guy(id).carry(time) = 2) Then
                    Dim paradox As Boolean = True
                    For j = 1 To boxCount
                        If (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) And guy(id).x(time) + guy_width > box(j).x(time - 1) And guy(id).x(time) < box(j).x(time - 1) + 32 And guy(id).y(time) + 16 > box(j).y(time - 1) + 10 And guy(id).y(time) + 16 < box(j).y(time - 1) + 22 And Not box(j).carry(time - 1) Then
                            box(j).t_end = time 'ends the box
                            Audio.PlaySoundEffect(GameSound("box_pickup"))
                            If time - box_pickup_length > 1 Then
                                guy(id).playSound(time - box_pickup_length) = "box_pickup_rev"
                            Else
                                guy(id).playSound(1) = "box_pickup_rev"
                            End If
                            paradox = False
                            Exit For
                        End If
                    Next
                    If paradox Then
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                        Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                        Text.DrawText("Your reverse-time past self did not find a box to pickup", Color.Red, GameFont("Medium"), 140, 400)
                        restartLevel = True
                    End If
                End If
            End If
        Else
            guy(id).carry(time) = guy(id).carry(time + 1)
            If (guy(id).carry(time) = 2) Then
                guy(id).carry(time) = 1
            End If
            If (guy(id).carry(time) = -1) Then
                guy(id).carry(time) = 0
            End If
        End If


            ''CHECK DEATH
            'sharp and pointy 
            For j = 0 To spikeCount - 1
                If guy(id).x(time) + guy_width > spike(j).x And guy(id).x(time) < spike(j).x + spike(j).width And guy(id).y(time) + 32 > spike(j).y And guy(id).y(time) < spike(j).y + spike(j).height Then
                    Graphics.DrawRectangle(Color.Red, False, guy(id).x(time), guy(id).y(time), guy_width, 32)
                    Graphics.DrawRectangle(Color.Red, False, guy(id).x(time) - 1, guy(id).y(time) - 1, guy_width + 2, 34)
                    Text.DrawText("Oh No! You've been", Color.Red, GameFont("Medium"), 260, 260)
                    Text.DrawText("Spiked", Color.Red, GameFont("Large"), 300, 300)
                    Text.DrawText("Press Space or R to continue", Color.Red, GameFont("Medium"), 350, 400)
                    Audio.PlaySoundEffect(GameSound("spiked"))
                    restartLevel = True
                    Exit Sub
                End If
            Next

            'squishy
            If squishCheck Then
                'check pushed into a wall
                If Math.Floor((guy(id).y(time)) / 32) = (guy(id).y(time)) / 32 Then
                    If (guy(id).x(time - 1)) - Math.Floor((guy(id).x(time - 1)) / 32) * 32 <= guy_width_left Then
                        If wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Then
                            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time + 1) - 1, guy(id).y(time + 1) - 1)
                            Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                            Audio.PlaySoundEffect(GameSound("squished"))
                            restartLevel = True
                            Exit Sub
                        End If

                    Else
                        If wall(Math.Floor((guy(id).x(time) + guy_width) / 32), Math.Floor((guy(id).y(time)) / 32)) Or wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Then
                            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time + 1) - 1, guy(id).y(time + 1) - 1)
                            Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                            Audio.PlaySoundEffect(GameSound("squished"))
                            restartLevel = True
                            Exit Sub
                        End If
                    End If
                Else
                    If (guy(id).x(time - 1)) - Math.Floor((guy(id).x(time - 1)) / 32) * 32 <= guy_width_left Then
                        If wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32 + 1)) Or wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Then
                            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time + 1) - 1, guy(id).y(time + 1) - 1)
                            Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                            Audio.PlaySoundEffect(GameSound("squished"))
                            restartLevel = True
                            Exit Sub
                        End If
                    Else
                        If wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32)) Or wall(Math.Floor((guy(id).x(time) + guy_width) / 32), Math.Floor((guy(id).y(time)) / 32)) Or wall(Math.Floor((guy(id).x(time)) / 32), Math.Floor((guy(id).y(time)) / 32) + 1) Or wall(Math.Floor((guy(id).x(time) + guy_width) / 32), Math.Floor((guy(id).y(time)) / 32) + 1) Then
                            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time + 1) - 1, guy(id).y(time + 1) - 1)
                            Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                            Audio.PlaySoundEffect(GameSound("squished"))
                            restartLevel = True
                            Exit Sub
                        End If
                    End If
                End If

                'check pushed into a platform
                For j = 0 To platCount - 1
                    If guy(id).x(time) + guy_width > plat(j).x(time) And guy(id).x(time) < plat(j).x(time) + plat(j).width And guy(id).y(time) < plat(j).y(time) + plat(j).height And guy(id).y(time) + 32 > plat(j).y(time) Then
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time + 1) - 1, guy(id).y(time + 1) - 1)
                        Text.DrawText("Squished", Color.Red, GameFont("Large"), 300, 300)
                        Audio.PlaySoundEffect(GameSound("squished"))
                        restartLevel = True
                        Exit Sub
                    End If
                Next
            End If

    End Sub

    Private Sub CheckEndParadoxGuy(ByVal id As Integer)
        'Checks postion, speed and box carrying against stored paradox info to see if a paradox has occured
        'This one is called at the end time of a guy and checks realative postitions on portal
        If restartLevel Then
            Exit Sub
        End If
        'End Types; 
        '1 = portal whenever and fixed (from any time)
        '2 = portal add and portal reverse (from specific time)
        '3 = time jump (pickup)
        '4 = shot by laser
        '5 = time reverse (pickup)

        'Check if a box is being chronoported that shouldn't be or vise versa
        If (guy(id).carry(time) = 0 Xor guy(id).carry_par(time) = 0) Then
            Graphics.DrawBitmap(GameImage("paradox_box"), guy(id).x(time) - 5, guy(id).y(time) - 33)
            Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
            If guy(id).carry(time) = 0 Then
                Text.DrawText("Your past self failed to take a box with them", Color.Red, GameFont("Medium"), 140, 400)
            Else
                Text.DrawText("Your past self took a box with them that they shouldn't have", Color.Red, GameFont("Medium"), 140, 400)
            End If
            restartLevel = True
            Exit Sub
        End If

        If guy(id).t_end_type = 3 Or guy(id).t_end_type = 4 Or guy(id).t_end_type = 5 Then

            'check X position
            If Math.Abs(guy(id).x(time) - guy(id).x_par(time)) > 10 Then
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x_par(time) - 1, guy(id).y_par(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                Text.DrawText("Your past self did not depart from the same position as they did before", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
                Exit Sub
            End If

            'check Y position
            If Math.Abs(guy(id).y(time) - guy(id).y_par(time)) > 10 Then
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x_par(time) - 1, guy(id).y_par(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                Text.DrawText("Your past self did not depart from the same position as they did before", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
                Exit Sub
            End If

        Else

            If guy(id).t_end_portal_id = -1 Then
                'check X position
                If Math.Abs(guy(id).x(time) - (endPortal.x + guy(id).x_par(time))) > 10 Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), endPortal.x + guy(id).x_par(time) - 1, endPortal.y + guy(id).y_par(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                    Text.DrawText("Your past self did not reach the End Portal", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                    Exit Sub
                End If

                'check Y position
                If Math.Abs(guy(id).y(time) - (endPortal.y + guy(id).y_par(time))) > 10 Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), endPortal.x + guy(id).x_par(time) - 1, endPortal.y + guy(id).y_par(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                    Text.DrawText("Your past self did not reach the End Portal", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                    Exit Sub
                End If
            Else
                'check X position
                If Math.Abs(guy(id).x(time) - (portal(guy(id).t_end_portal_id).x + guy(id).x_par(time))) > 10 Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), portal(guy(id).t_end_portal_id).x + guy(id).x_par(time) - 1, portal(guy(id).t_end_portal_id).y + guy(id).y_par(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                    Text.DrawText("Your past self did not reach the right Portal", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                    Exit Sub
                End If

                'check Y position
                If Math.Abs(guy(id).y(time) - (portal(guy(id).t_end_portal_id).y + guy(id).y_par(time))) > 10 Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), portal(guy(id).t_end_portal_id).x + guy(id).x_par(time) - 1, portal(guy(id).t_end_portal_id).y + guy(id).y_par(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                    Text.DrawText("Your past self did not reach the right Portal", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                    Exit Sub
                End If
            End If

        End If

        'check xspeed 
        If Math.Abs(guy(id).xspeed(time) - guy(id).xspeed_par(time)) > 1 Then
            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
            Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
            Text.DrawText("Your past self did not depart with the same speed as they did before", Color.Red, GameFont("Medium"), 140, 400)
            restartLevel = True
            Exit Sub
        End If

        'check yspeed 
        If Math.Abs(guy(id).yspeed(time) - guy(id).yspeed_par(time)) > 1 Then
            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
            Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
            Text.DrawText("Your past self did not depart with the same speed as they did before", Color.Red, GameFont("Medium"), 140, 400)
            restartLevel = True
            Exit Sub
        End If


    End Sub


    Private Function CheckPickupParadox(ByVal id As Integer, ByVal type As Integer, ByVal pid As Integer)
        'Checks if a pickup that was picked up in personal past is not there now.
        'It then goes on to check if the missed pickup was used before the current time

        Dim i, j As Integer
        'Check if the pickup is availible for collection
        For j = 0 To pickupCount - 1
            If (pickup(j).t_end > time Or pickup(j).t_end = 0) And pickup(j).type = type Then
                If guy(id).x(time) + guy_width > pickup(j).x And guy(id).x(time - 1) < pickup(j).x + pickup(j).width And guy(id).y(time) + 32 > pickup(j).y And guy(id).y(time - 1) < pickup(j).y + pickup(j).height Then
                    pickup(j).t_end = time + 1
                    'The pickup is still there and is now ended, no further paradox checking required
                    Return True
                End If
            End If
        Next

        'If it is not there now check if missing the pickup would cause a paradox
        'set pickup info
        guy(id).pickupCheck(time) = 0
        guy(id).pickup_time(pid) = 0

        'remember that if a future self picks it up and it was used in the past this causes a paradox

        If type = 0 Then ' jump

            Dim count As Integer = 0
            'checks all previous selfs in order
            For i = 0 To guyCount
                'direction of checking, it may matter
                Dim dir As Integer = 1
                If Not guy(i).t_direction Then
                    dir = -1
                End If
                'current player does not have an end time so it must be set from the current time
                Dim t_end As Integer = guy(i).t_end
                If i = playerGuy Then
                    t_end = time
                    If Not playerGuyStore = -1 Then
                        t_end = time_aim
                    End If
                End If
                'If a pickup of this type was collected add one to the counter
                For j = guy(i).t_start To t_end Step dir
                    If guy(i).pickupCheck(j) = 1 Then
                        count = count + 1
                    End If
                Next
                'If the instance used this pickup then subtract one
                If guy(i).t_end_type = 3 Then
                    count = count - 1
                    'If the count goes below 0 then the pickup was needed and now causes a paradox
                    If count < 0 Then
                        Graphics.DrawBitmap(GameImage("paradox_pickup"), pickup(pid).x, pickup(pid).y)
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1), guy(id).y(time - 1))
                        Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                        Text.DrawText("Your past self missed a ChronoBelt that has been used", Color.Red, GameFont("Medium"), 200, 400)
                        restartLevel = True
                        Return False
                    End If
                End If
            Next
            timeJumps = count

        ElseIf type = 1 Then ' gun

            Dim count As Integer = 0
            For i = 0 To guyCount
                'direction of checking matters for gun because it is not a self travel method
                Dim dir As Integer = 1
                If Not guy(i).t_direction Then
                    dir = -1
                End If
                'current player does not have an end time so it must be set from the current time
                Dim t_end As Integer = guy(i).t_end
                If i = playerGuy Then
                    t_end = time
                    If Not playerGuyStore = -1 Then
                        t_end = time_aim
                    End If
                End If
                'Check pickup collection and useage at the same time to see if there was ever a time when the count is less than 0
                For j = guy(i).t_start To t_end Step dir
                    If guy(i).pickupCheck(j) = 2 Then
                        count = count + 1
                    End If
                    If Not guy(i).laser_x(j) = 0 Then
                        count = count - 1
                        If count < 0 Then
                            Graphics.DrawBitmap(GameImage("paradox_pickup"), pickup(pid).x, pickup(pid).y)
                            Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1), guy(id).y(time - 1))
                            Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                            Text.DrawText("Your past self missed a pair of Time Goggles that have been used", Color.Red, GameFont("Medium"), 200, 400)
                            restartLevel = True
                            Return False
                        End If
                    End If
                Next

            Next
            timeGuns = count

        ElseIf type = 2 Then ' reverse

            Dim count As Integer = 0
            For i = 0 To guyCount
                'direction of checking, it may matter
                Dim dir As Integer = 1
                If Not guy(i).t_direction Then
                    dir = -1
                End If
                'current player does not have an end time so it must be set from the current time
                Dim t_end As Integer = guy(i).t_end
                If i = playerGuy Then
                    t_end = time
                    If Not playerGuyStore = -1 Then
                        t_end = time_aim
                    End If
                End If
                'If a pickup of this type was collected add one to the counter
                For j = guy(i).t_start To t_end Step dir
                    If guy(i).pickupCheck(j) = 3 Then
                        count = count + 1
                    End If
                Next
                'If the instance used this pickup then subtract one
                If guy(i).t_end_type = 5 Then
                    count = count - 1
                    If count < 0 Then
                        Graphics.DrawBitmap(GameImage("paradox_pickup"), pickup(pid).x, pickup(pid).y)
                        Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - 1), guy(id).y(time - 1))
                        Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300, 300)
                        Text.DrawText("Your past self missed an Hourglass that has been used", Color.Red, GameFont("Medium"), 200, 400)
                        restartLevel = True
                        Return False
                    End If
                End If

            Next
            timeReverses = count
        End If

        Return False

    End Function

    Private Sub CheckChronofragGuy(ByVal id As Integer, ByVal dir As Integer)
        Audio.PlaySoundEffect(GameSound("portal_in"))
        If guy(id).t_direction Then
            guy(id).playSound(time + portal_in_length) = "portal_in_rev"
        Else
            If time - portal_in_length > 1 Then
                guy(id).playSound(time - portal_in_length) = "portal_in_rev"
            Else
                guy(id).playSound(1) = "portal_in_rev"
            End If
        End If
        'check chronoporting into platforms and walls, mobile platforms can chronoport into walls
        If restartLevel Then
            Exit Sub
        End If

        Dim j As Integer
        For j = 0 To platCount - 1
            If guy(id).x(time - dir) + guy_width > plat(j).x(time) And guy(id).x(time - dir) < plat(j).x(time) + plat(j).width And guy(id).y(time - dir) < plat(j).y(time) + plat(j).height And guy(id).y(time - dir) + 32 > plat(j).y(time) Then
                DrawEverything(True)
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - dir) - 1, guy(id).y(time - dir) - 1)
                Text.DrawText("Chronofragged", Color.Red, GameFont("Large"), 100, 300)
                Text.DrawText("You Chronoported into a platform", Color.Red, GameFont("Medium"), 320, 400)
                restartLevel = True
                Exit Sub
            End If
        Next

        If Math.Floor((guy(id).y(time - dir)) / 32) = (guy(id).y(time - dir)) / 32 Then
            If (guy(id).x(time - dir)) - Math.Floor((guy(id).x(time - dir)) / 32) * 32 <= guy_width_left Then
                If wall(Math.Floor((guy(id).x(time - dir)) / 32), Math.Floor((guy(id).y(time - dir)) / 32)) Then
                    DrawEverything(True)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - dir) - 1, guy(id).y(time - dir) - 1)
                    Text.DrawText("Chronofragged", Color.Red, GameFont("Large"), 100, 300)
                    Text.DrawText("You Chronoported into a wall", Color.Red, GameFont("Medium"), 320, 400)
                    restartLevel = True
                    Exit Sub
                End If

            Else
                If wall(Math.Floor((guy(id).x(time - dir) + guy_width) / 32), Math.Floor((guy(id).y(time - dir)) / 32)) Or wall(Math.Floor((guy(id).x(time - dir)) / 32), Math.Floor((guy(id).y(time - dir)) / 32)) Then
                    DrawEverything(True)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - dir) - 1, guy(id).y(time - dir) - 1)
                    Text.DrawText("Chronofragged", Color.Red, GameFont("Large"), 100, 300)
                    Text.DrawText("You Chronoported into a wall", Color.Red, GameFont("Medium"), 320, 400)
                    restartLevel = True
                    Exit Sub
                End If
            End If
        Else
            If (guy(id).x(time - dir)) - Math.Floor((guy(id).x(time - dir)) / 32) * 32 <= guy_width_left Then
                If wall(Math.Floor((guy(id).x(time - dir)) / 32), Math.Floor((guy(id).y(time - dir)) / 32 + 1)) Or wall(Math.Floor((guy(id).x(time - dir)) / 32), Math.Floor((guy(id).y(time - dir)) / 32)) Then
                    DrawEverything(True)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - dir) - 1, guy(id).y(time - dir) - 1)
                    Text.DrawText("Chronofragged", Color.Red, GameFont("Large"), 100, 300)
                    Text.DrawText("You Chronoported into a wall", Color.Red, GameFont("Medium"), 320, 400)
                    restartLevel = True
                    Exit Sub
                End If
            Else
                If wall(Math.Floor((guy(id).x(time - dir)) / 32), Math.Floor((guy(id).y(time - dir)) / 32)) Or wall(Math.Floor((guy(id).x(time - dir) + guy_width) / 32), Math.Floor((guy(id).y(time - dir)) / 32)) Or wall(Math.Floor((guy(id).x(time - dir)) / 32), Math.Floor((guy(id).y(time - dir)) / 32) + 1) Or wall(Math.Floor((guy(id).x(time - dir) + guy_width) / 32), Math.Floor((guy(id).y(time - dir)) / 32) + 1) Then
                    DrawEverything(True)
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time - dir) - 1, guy(id).y(time - dir) - 1)
                    Text.DrawText("Chronofragged", Color.Red, GameFont("Large"), 100, 300)
                    Text.DrawText("You Chronoported into a wall", Color.Red, GameFont("Medium"), 320, 400)
                    restartLevel = True
                    Exit Sub
                End If
            End If
        End If

    End Sub

    Private Sub CheckBackwardsGuy(ByVal id As Integer)
        'Checks paradoxes for reverse time guys when the current direction is forwards
        'Very stringent checking currently. Anything less would require some kind of backwards FastMode

        Dim j As Integer
        Dim par_id As Integer = 0

        'Check if objects which should be there are not
        If guy(id).object_par_s(time) = 1 Then 'Check platforms

            If guy(id).dir_par_s(time) = 0 Then 'check platform paradox on right
                Dim paradox As Boolean = True
                For j = 0 To platCount - 1
                    'checks platforms against stored paradox info
                    If guy(id).y_par_s(time) + 32 > plat(j).y(time) And guy(id).y_par_s(time) < plat(j).y(time) + plat(j).height And guy(id).xspeed_par_s(time) = plat(j).xspeed(time) And guy(id).x_par_s(time) = plat(j).x(time) + plat(j).width Then
                        par_id = j
                        paradox = False
                        Exit For
                    End If
                Next
                If paradox Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                    Text.DrawText("Your reverse-time past self did not collide with a platform", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                End If
            ElseIf (guy(id).dir_par_s((time)) = 1) Then 'check platform paradox on top
                Dim paradox As Boolean = True
                For j = 0 To platCount - 1
                    If guy(id).x_par_s(time) + guy_width > plat(j).x(time) And guy(id).x_par_s(time) < plat(j).x(time) + plat(j).width And guy(id).xspeed_par_s(time) = plat(j).xspeed(time) And guy(id).yspeed_par_s(time) = plat(j).yspeed(time) And guy(id).y_par_s(time) = plat(j).y(time) Then
                        par_id = j
                        paradox = False
                        Exit For
                    End If
                Next
                If paradox Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                    Text.DrawText("Your reverse-time past self did not collide with a platform", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                End If
            ElseIf guy(id).dir_par_s(time) = 2 Then 'check platform paradox on left
                Dim paradox As Boolean = True
                For j = 0 To platCount - 1
                    If guy(id).y_par_s(time) + 32 > plat(j).y(time) And guy(id).y_par_s(time) < plat(j).y(time) + plat(j).height And guy(id).xspeed_par_s(time) = plat(j).xspeed(time) And guy(id).x_par_s(time) = plat(j).x(time) Then
                        par_id = j
                        paradox = False
                        Exit For
                    End If
                Next
                If paradox Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                    Text.DrawText("Your reverse-time past self did not collide with a platform", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                End If
            ElseIf guy(id).dir_par_s(time) = 3 Then 'check platform paradox on bottom
                Dim paradox As Boolean = True
                For j = 0 To platCount - 1
                    If guy(id).x_par_s(time) + guy_width > plat(j).x(time) And guy(id).x_par_s(time) < plat(j).x(time) + plat(j).width And guy(id).yspeed_par_s(time) = plat(j).yspeed(time) And guy(id).y_par_s(time) = plat(j).y(time) + plat(j).height Then
                        par_id = j
                        paradox = False
                        Exit For
                    End If
                Next
                If paradox Then
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                    Text.DrawText("Your reverse-time past self did not collide with a platform", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                End If
            End If
        ElseIf (guy(id).object_par_s(time) = 2) Then 'check boxes
            'only checks box position when standing on top
            Dim paradox As Boolean = True
            For j = 1 To boxCount
                If (box(j).t_start < time And (box(j).t_end > time Or box(j).t_end = 0)) And guy(id).yspeed_par_s(time) = box(j).yspeed(time) And guy(id).x_par_s(time) - 10 <= box(j).x(time) + 32 And guy(id).x(time) + guy_width + 10 >= box(j).x(time) Then
                    par_id = j
                    paradox = False
                    Exit For
                End If
            Next
            If paradox Then
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                Text.DrawText("Your reverse-time past self did not collide with a box", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
            End If
        End If

        'Check the exsistance of box to pickup
        'Note dropping does not ever need to be checked
        If guy(id).carry(time) = 2 Then
            Dim paradox As Boolean = True
            For j = 1 To boxCount
                If (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) And guy(id).x(time) + guy_width > box(j).x(time - 1) And guy(id).x(time) < box(j).x(time - 1) + 32 And guy(id).y(time) + 16 > box(j).y(time - 1) + 10 And guy(id).y(time) + 16 < box(j).y(time - 1) + 22 And (Not box(j).carry(time - 1)) Then
                    box(j).t_end = time
                    paradox = False
                    Exit For
                End If
            Next
            If paradox Then
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                Text.DrawText("Your reverse-time past self did not find a box to pickup", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
            End If
        End If

        'check pickup existance
        'Same checking that all other NPCs do
        If Not guy(id).pickupCheck(time) = 0 Then
            CheckPickupParadox(id, guy(id).pickupCheck(time) - 1, guy(id).pickup_id(time))
        End If

        'Check spiked
        For j = 0 To spikeCount - 1
            If guy(id).x(time) + guy_width > spike(j).x And guy(id).x(time) < spike(j).x + spike(j).width And guy(id).y(time) + 32 > spike(j).y And guy(id).y(time) < spike(j).y + spike(j).height Then
                Graphics.DrawRectangle(Color.Red, False, guy(id).x(time), guy(id).y(time), &H17, &H20)
                Graphics.DrawRectangle(Color.Red, False, (guy(id).x(time) - 1), (guy(id).y(time) - 1.0!), &H19, &H22)
                Text.DrawText("Oh No! You've been", Color.Red, GameFont("Medium"), 260, 260)
                Text.DrawText("Spiked", Color.Red, GameFont("Large"), 300, 300)
                Text.DrawText("Press Space or R to continue", Color.Red, GameFont("Medium"), 320, 400)
                Audio.PlaySoundEffect(GameSound("spiked"))
                restartLevel = True
                Exit For
            End If
        Next

        'Check walking into platforms that weren't there
        For j = 0 To platCount - 1
            If guy(id).object_par_s(time) = 1 And par_id = j Then
                'Do not check for platforms that were collided with in the past
                'This is a stopgap solution as the paradox checking should work for all platforms
                Continue For
            End If
            If guy(id).x(time) + guy_width > plat(j).x(time) And guy(id).x(time) < plat(j).x(time) + plat(j).width And guy(id).y(time) < plat(j).y(time) + plat(j).height And guy(id).y(time) + 32 > plat(j).y(time) And plat(j).xspeed(time) = plat(j).xspeed(time - 1) And plat(j).xspeed(time) = plat(j).xspeed(time + 1) And plat(j).yspeed(time) = plat(j).yspeed(time - 1) And plat(j).yspeed(time) = plat(j).yspeed(time + 1) Then
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).x(time) - 1, guy(id).y(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                Text.DrawText("Your reverse-time past self collided with a platform", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
                Exit For
            End If
        Next

    End Sub

    Private Sub DrawGuy(ByVal id As Integer)

        'DEBUG
        'Text.DrawText(id, Color.Red, GameFont("Small"), guy(id).x(time) - 10, guy(id).y(time) - 20)
        'Text.DrawText("Exists", Color.Black, GameFont("Small"), 350, 100 + id * 60)

        If (Not guy(id).k_left(time) And Not guy(id).k_right(time)) Then
            'draw the player as stationary
            guy(id).subimage = 5
            If guy(id).face_store(time) Then
                Graphics.DrawBitmap(GameImage("guy_left_stop"), guy(id).x(time), guy(id).y(time))
            Else
                Graphics.DrawBitmap(GameImage("guy_right_stop"), guy(id).x(time), guy(id).y(time))
            End If
        ElseIf guy(id).t_direction Xor time_speed < 0 Then
            'if time direction = personal time direction animate forwards
            If time < maxTime And time > 1 And (Not pauseGame) Then
                guy(id).subimage += 1
                If (guy(id).subimage >= 14) Then
                    guy(id).subimage = 0
                End If
            End If
            If guy(id).face_store(time) Then
                Graphics.DrawBitmapPart(GameImage("guy_left"), Math.Floor(guy(id).subimage) * 36, 0, 36, 33, guy(id).x(time) - 6, guy(id).y(time) - 1)
            Else
                Graphics.DrawBitmapPart(GameImage("guy_right"), Math.Floor(guy(id).subimage) * 36, 0, 36, 33, guy(id).x(time) - 6, guy(id).y(time) - 1)
            End If
        Else
            'backwards animation for travelling backwards relative to time speed
            If Not pauseGame Then
                guy(id).subimage -= 1
                If (guy(id).subimage < 0) Then
                    guy(id).subimage = 14
                End If
            End If
            If guy(id).face_store(time) Then
                Graphics.DrawBitmapPart(GameImage("guy_left"), Math.Floor(guy(id).subimage) * 36, 0, 36, 33, guy(id).x(time) - 6, guy(id).y(time) - 1)
            Else
                Graphics.DrawBitmapPart(GameImage("guy_right"), Math.Floor(guy(id).subimage) * 36, 0, 36, 33, guy(id).x(time) - 6, guy(id).y(time) - 1)
            End If
        End If

        If (Not guy(id).t_direction And (guy(id).carry(time) > 0)) Then
            'draws the box that doesn't exist
            Graphics.DrawBitmap(GameImage("box"), guy(id).x(time) - 4, (guy(id).y(time) - 32.0!))
        End If

    End Sub

    Private Sub DrawGuyEffects(ByVal id As Integer)

        'Text.DrawText("ID", Color.Black, GameFont("Small"), 100, 70)
        'Text.DrawText("EndSpec", Color.Black, GameFont("Small"), 180, 70)
        'Text.DrawText("Start", Color.Black, GameFont("Small"), 260, 70)
        'Text.DrawText("End X", Color.Black, GameFont("Small"), 340, 70)
        'Text.DrawText("End Portal", Color.Black, GameFont("Small"), 420, 70)
        'Text.DrawText("End Type", Color.Black, GameFont("Small"), 540, 70)

        'Text.DrawText(playerGuy, Color.Black, GameFont("Small"), 666, 100)
        'Text.DrawText(guyCount, Color.Black, GameFont("Small"), 666, 150)

        ''DEBUG
        'Text.DrawText(id, Color.Black, GameFont("Small"), 100, 100 + id * 30)
        'Text.DrawText(guy(id).t_end_specific, Color.Black, GameFont("Small"), 180, 100 + id * 30)
        'Text.DrawText(guy(id).t_start, Color.Black, GameFont("Small"), 260, 100 + id * 30)
        'Text.DrawText(guy(id).x_par(guy(id).t_end), Color.Black, GameFont("Small"), 340, 100 + id * 30)
        'Text.DrawText(guy(id).t_end_portal_id, Color.Black, GameFont("Small"), 420, 100 + id * 30)
        'Text.DrawText(guy(id).t_end_type, Color.Black, GameFont("Small"), 540, 100 + id * 30)

        If guy(id).t_direction Then
            'draw start and end portal effect for forwards guys
            If (time > guy(id).t_start) And ((time - guy(id).t_start) < 50) Then
                Graphics.DrawBitmapPart(GameImage("portal_effect"), Math.Floor((time - guy(id).t_start) / 5) * 50, 0, 50, 50, guy(id).x(guy(id).t_start) - 14, guy(id).y(guy(id).t_start) - 9)
            End If
            If (time > guy(id).t_end) And ((time - guy(id).t_end) < 50) Then
                Graphics.DrawBitmapPart(GameImage("portal_effect"), Math.Floor((time - guy(id).t_end) / 5) * 50, 0, 50, 50, guy(id).x(guy(id).t_end) - 14, guy(id).y(guy(id).t_end) - 9)
            End If
        Else
            'draw start and end portal effect for backwards guys
            If (time < guy(id).t_start) And ((guy(id).t_start - time) < 50) Then
                Graphics.DrawBitmapPart(GameImage("portal_effect"), Math.Floor((guy(id).t_start - time) / 5) * 50, 0, 50, 50, guy(id).x(guy(id).t_start) - 14, guy(id).y(guy(id).t_start) - 9)
            End If
            If (time < guy(id).t_end) And ((guy(id).t_end - time) < 50) Then
                Graphics.DrawBitmapPart(GameImage("portal_effect"), Math.Floor((guy(id).t_end - time) / 5) * 50, 0, 50, 50, guy(id).x(guy(id).t_end) - 14, guy(id).y(guy(id).t_end) - 9)
            End If
        End If

        If (Not guy(id).laser_x_draw(time) = 0) And (Not guy(id).x(time) = 0) Then
            'draw laser gun
            Graphics.DrawLine(Color.Red, guy(id).x(time) + 12, guy(id).y(time) + 12, guy(id).laser_x_draw(time), guy(id).laser_y_draw(time))
        End If

    End Sub

    Private Sub MoveBox(ByVal id As Integer)

        If box(id).carry(time - 1) = False Then 'do not do anything while being carried
            'DEBUG
            'Text.DrawText("exist", Color.White, GameFont("Small"), 250, 100 + id * 30)

            If time = box(id).t_start + 1 Then
                'checks if the box is created in a platform
                CheckChronofragBox(id)
            End If

            Dim squishCheck = False  'should squish be checked, active if collided with platform or box
            Dim j As Integer

            box(id).xspeed(time) = 0
            'add gravity
            box(id).yspeed(time) = box(id).yspeed(time - 1) + gravity
            Dim platSupport As Integer = -1

            Dim newX = box(id).x(time - 1)
            Dim newY = box(id).y(time - 1)
            'stops Y direction platform launching
            If box(id).supported_obj(time - 1) = 1 Then
                If plat(box(id).supported_id(time - 1)).y(time) - 33 - plat(box(id).supported_id(time - 1)).yspeed(time) < newY Then
                    box(id).yspeed(time) = (plat(box(id).supported_id(time - 1)).yspeed(time) + box(id).yspeed(time))
                End If
            End If

            'reset audio
            If box(id).playSound(time + box_hit_length) = "box_hit_rev" Then
                box(id).playSound(time + box_hit_length) = ""
            End If

            ' check wall collision in y direction
            If box(id).yspeed(time) > 0 Then
                If Math.Floor((newX) / 32) = (newX) / 32 Then
                    If wall(Math.Floor((newX) / 32), Math.Floor((newY + box(id).yspeed(time) + 32) / 32)) Then
                        newY = Math.Floor((newY + box(id).yspeed(time)) / 32) * 32
                        If box(id).yspeed(time) > gravity + 0.05 And box(id).supported_obj(time - 1) = 0 Then
                            Audio.PlaySoundEffect(GameSound("box_hit"))
                            box(id).playSound(time + box_hit_length) = "box_hit_rev"
                        End If
                        box(id).yspeed(time) = 0
                        box(id).xspeed(time) = 0
                    End If
                Else
                    If wall(Math.Floor((newX) / 32), Math.Floor((newY + box(id).yspeed(time) + 32) / 32)) Or wall(Math.Floor((newX + 32) / 32), Math.Floor((newY + box(id).yspeed(time) + 32) / 32)) Then
                        newY = Math.Floor((newY + box(id).yspeed(time)) / 32) * 32
                        If box(id).yspeed(time) > gravity + 0.05 And box(id).supported_obj(time - 1) = 0 Then
                            Audio.PlaySoundEffect(GameSound("box_hit"))
                            box(id).playSound(time + box_hit_length) = "box_hit_rev"
                        End If
                        box(id).yspeed(time) = 0
                        box(id).xspeed(time) = 0
                    End If
                End If
            ElseIf box(id).yspeed(time) < 0 Then
                If Math.Floor((newX) / 32) = (newX) / 32 Then
                    If wall(Math.Floor((newX) / 32), Math.Floor((newY + box(id).yspeed(time)) / 32)) Then
                        newY = Math.Floor((newY + box(id).yspeed(time)) / 32) * 32 + 32
                        box(id).yspeed(time) = 0
                        box(id).xspeed(time) = 0
                    End If
                Else
                    If wall(Math.Floor((newX) / 32), Math.Floor((newY + box(id).yspeed(time)) / 32)) Or wall(Math.Floor((newX + 32) / 32), Math.Floor((newY + box(id).yspeed(time)) / 32)) Then
                        newY = Math.Floor((newY + box(id).yspeed(time)) / 32) * 32 + 32
                        box(id).yspeed(time) = 0
                        box(id).xspeed(time) = 0
                    End If
                End If
            End If

            ' check platform collision in y direction
            For j = 0 To platCount - 1
                If box(id).yspeed(time) - plat(j).yspeed(time) > 0 Then
                    If newX + 32 > plat(j).x(time) And newX < plat(j).x(time) + plat(j).width And newY + box(id).yspeed(time) + 32 > plat(j).y(time) And newY + box(id).yspeed(time) + 32 < plat(j).y(time - 1) + plat(j).height Then
                        If plat(j).yspeed(time) > 0 Then
                            If box(id).yspeed(time) > gravity + plat(j).yspeed(time) + 0.05 And box(id).supported_obj(time - 1) = 0 Then
                                Audio.PlaySoundEffect(GameSound("box_hit"))
                                box(id).playSound(time + box_hit_length) = "box_hit_rev"
                            End If
                            newY = plat(j).y(time) - 32 - plat(j).yspeed(time)
                            box(id).yspeed(time) = plat(j).yspeed(time)
                        Else
                            If box(id).yspeed(time) > gravity + 0.05 And box(id).supported_obj(time - 1) = 0 Then
                                Audio.PlaySoundEffect(GameSound("box_hit"))
                                box(id).playSound(time + box_hit_length) = "box_hit_rev"
                            End If
                            newY = plat(j).y(time) - 32
                            box(id).yspeed(time) = 0
                        End If
                        box(id).supported_obj(time) = 1
                        box(id).supported_id(time) = j
                        box(id).xspeed(time) = plat(j).xspeed(time)
                        squishCheck = True
                    End If
                ElseIf box(id).yspeed(time) - plat(j).yspeed(time) < 0 Then
                    If newX + 32 > plat(j).x(time) And newX < plat(j).x(time) + plat(j).width And newY + box(id).yspeed(time) > plat(j).y(time) And newY + box(id).yspeed(time) < plat(j).y(time - 1) + plat(j).height Then
                        newY = plat(j).y(time) + plat(j).height
                        If plat(j).yspeed(time) > 0 Then
                            box(id).yspeed(time) = plat(j).yspeed(time)
                        Else
                            box(id).yspeed(time) = 0
                        End If
                        squishCheck = True
                    End If
                End If
            Next

            'check box collision in y direction
            For j = 1 To boxCount
                If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                    If Not j = id Then
                        If box(id).yspeed(time) > box(j).yspeed(time - 1) Then
                            'If newX + 32 > box(j).x(time - 1) And newX < box(j).x(time - 1) + 32 And newY + box(id).yspeed(time) + 32 > box(j).y(time - 1) + box(j).yspeed(time - 1) * 2 And newY + box(id).yspeed(time) < box(j).y(time - 1) + box(j).yspeed(time - 1) * 2 Then
                            If newX + 32 > box(j).x(time - 1) And newX < box(j).x(time - 1) + 32 And newY + box(id).yspeed(time) + 32 > box(j).y(time - 1) + box(j).yspeed(time - 1) And newY + box(id).yspeed(time - 1) < box(j).y(time - 1) + box(j).yspeed(time) Then
                                If box(j).supported_obj(time - 1) = 1 Or box(j).supported_obj(time - 1) = 2 Then
                                    If plat(box(j).supported_id(time - 1)).yspeed(time) > 0 Then
                                        If box(id).yspeed(time) > gravity + plat(box(j).supported_id(time - 1)).yspeed(time) + 0.05 And box(id).supported_obj(time - 1) = 0 Then
                                            Audio.PlaySoundEffect(GameSound("box_hit"))
                                            box(id).playSound(time + box_hit_length) = "box_hit_rev"
                                        End If
                                        newY = box(j).y(time - 1) - 32
                                        box(id).yspeed(time) = plat(box(j).supported_id(time - 1)).yspeed(time)
                                    Else
                                        If box(id).yspeed(time) > gravity + 0.05 And box(id).supported_obj(time - 1) = 0 Then
                                            Audio.PlaySoundEffect(GameSound("box_hit"))
                                            box(id).playSound(time + box_hit_length) = "box_hit_rev"
                                        End If
                                        newY = box(j).y(time - 1) - 32 + plat(box(j).supported_id(time - 1)).yspeed(time)
                                        box(id).yspeed(time) = 0
                                    End If
                                    box(id).supported_obj(time) = 2
                                    box(id).supported_id(time) = box(j).supported_id(time - 1)
                                Else
                                    If box(id).yspeed(time) > gravity + 0.05 Then
                                        Audio.PlaySoundEffect(GameSound("box_hit"))
                                        box(id).playSound(time + box_hit_length) = "box_hit_rev"
                                    End If
                                    If box(j).yspeed(time - 1) > 0 Then
                                        newY = box(j).y(time - 1) - 32
                                        box(id).yspeed(time) = box(j).yspeed(time - 1)
                                    Else
                                        newY = box(j).y(time - 1) - 32
                                        box(id).yspeed(time) = 0
                                    End If
                                End If
                                box(id).xspeed(time) = box(j).xspeed(time - 1) + box(id).xspeed(time)
                                squishCheck = True
                            End If
                        End If
                    End If
                End If
            Next

            ' check wall collision in x direction
            If box(id).xspeed(time) > 0 Then
                If Math.Floor((newY) / 32) = (newY) / 32 Then
                    If wall(Math.Floor((newX + box(id).xspeed(time) + 32) / 32), Math.Floor((newY) / 32)) Then
                        newX = Math.Floor((newX + box(id).xspeed(time)) / 32) * 32
                        box(id).xspeed(time) = 0
                    End If
                Else
                    If wall(Math.Floor((newX + box(id).xspeed(time) + 32) / 32), Math.Floor((newY) / 32)) Or wall(Math.Floor((newX + box(id).xspeed(time) + 32) / 32), Math.Floor((newY + 32) / 32)) Then
                        newX = Math.Floor((newX + box(id).xspeed(time)) / 32) * 32
                        box(id).xspeed(time) = 0
                    End If
                End If
            ElseIf box(id).xspeed(time) < 0 Then
                If Math.Floor((newY) / 32) = (newY) / 32 Then
                    If wall(Math.Floor((newX + box(id).xspeed(time)) / 32), Math.Floor((newY) / 32)) Then
                        newX = Math.Floor((newX + box(id).xspeed(time)) / 32) * 32 + 32
                        box(id).xspeed(time) = 0
                    End If
                Else
                    If wall(Math.Floor((newX + box(id).xspeed(time)) / 32), Math.Floor((newY) / 32)) Or wall(Math.Floor((newX + box(id).xspeed(time)) / 32), Math.Floor((newY + 32) / 32)) Then
                        newX = Math.Floor((newX + box(id).xspeed(time)) / 32) * 32 + 32
                        box(id).xspeed(time) = 0
                    End If
                End If
            End If

            ' check platform collision in x direction
            For j = 0 To platCount - 1
                If box(id).xspeed(time) - plat(j).xspeed(time) > 0 Then
                    If newX + box(id).xspeed(time) + 32 > plat(j).x(time) And newX + box(id).xspeed(time) + 32 < plat(j).x(time) + plat(j).width And newY < plat(j).y(time) + plat(j).height And newY + 32 > plat(j).y(time - 1) Then
                        newX = plat(j).x(time) - 32 - plat(j).xspeed(time)
                        box(id).xspeed(time) = plat(j).xspeed(time)
                        squishCheck = True
                    End If
                ElseIf box(id).xspeed(time) - plat(j).xspeed(time) < 0 Then
                    If newX + box(id).xspeed(time) > plat(j).x(time) And newX + box(id).xspeed(time) < plat(j).x(time) + plat(j).width And newY < plat(j).y(time) + plat(j).height And newY + 32 > plat(j).y(time - 1) Then
                        box(id).xspeed(time) = plat(j).xspeed(time)
                        newX = plat(j).x(time) + plat(j).width - plat(j).xspeed(time)
                        squishCheck = True
                    End If
                End If
            Next

            'FIXME box collision in X postition needed!!

            ' apply the position changes
            box(id).x(time) = newX + box(id).xspeed(time)
            box(id).y(time) = newY + box(id).yspeed(time)

            ' check for squish
            If squishCheck Then

                'check for pushed into walls
                If Math.Floor((box(id).y(time)) / 32) = (box(id).y(time)) / 32 Then
                    If Math.Floor((box(id).x(time)) / 32) = (box(id).x(time)) / 32 Then
                        If wall(Math.Floor((box(id).x(time)) / 32), Math.Floor((box(id).y(time)) / 32)) Then
                            If box(id).carry(time - 1) Then
                                guy(box(id).carry_id).carry(time) = 0
                            End If
                            box(id).carry(time) = False
                            box(id).t_end = time
                            box(id).t_end_type = 1
                            Exit Sub
                        End If

                    Else
                        If wall(Math.Floor((box(id).x(time)) / 32 + 1), Math.Floor((box(id).y(time)) / 32)) Or wall(Math.Floor((box(id).x(time)) / 32), Math.Floor((box(id).y(time)) / 32)) Then
                            If box(id).carry(time - 1) Then
                                guy(box(id).carry_id).carry(time) = 0
                            End If
                            box(id).carry(time) = False
                            box(id).t_end = time
                            box(id).t_end_type = 1
                            Exit Sub
                        End If
                    End If
                Else
                    If Math.Floor((box(id).x(time)) / 32) = (box(id).x(time)) / 32 Then
                        If wall(Math.Floor((box(id).x(time)) / 32), Math.Floor((box(id).y(time)) / 32 + 1)) Or wall(Math.Floor((box(id).x(time)) / 32), Math.Floor((box(id).y(time)) / 32)) Then
                            If box(id).carry(time - 1) Then
                                guy(box(id).carry_id).carry(time) = 0
                            End If
                            box(id).carry(time) = False
                            box(id).t_end = time
                            box(id).t_end_type = 1
                            Exit Sub
                        End If
                    Else
                        If wall(Math.Floor((box(id).x(time)) / 32), Math.Floor((box(id).y(time)) / 32)) Or wall(Math.Floor((box(id).x(time)) / 32) + 1, Math.Floor((box(id).y(time)) / 32)) Or wall(Math.Floor((box(id).x(time)) / 32), Math.Floor((box(id).y(time)) / 32) + 1) Or wall(Math.Floor((box(id).x(time)) / 32) + 1, Math.Floor((box(id).y(time)) / 32) + 1) Then
                            If box(id).carry(time - 1) Then
                                guy(box(id).carry_id).carry(time) = 0
                            End If
                            box(id).carry(time) = False
                            box(id).t_end = time
                            box(id).t_end_type = 1
                            Exit Sub
                        End If
                    End If
                End If

                'check for pushed into platforms
                For j = 0 To platCount - 1
                    If box(id).x(time) + 32 > plat(j).x(time) And box(id).x(time) < plat(j).x(time) + plat(j).width And box(id).y(time) < plat(j).y(time) + plat(j).height And box(id).y(time) + 32 > plat(j).y(time) Then
                        If box(id).carry(time - 1) Then
                            guy(box(id).carry_id).carry(time) = 0
                        End If
                        box(id).carry(time) = False
                        box(id).t_end = time
                        box(id).t_end_type = 1
                        Exit Sub
                    End If
                Next

            End If

        End If

        box(id).carry(time) = box(id).carry(time - 1)

    End Sub

    Private Function CheckBoxDrop(ByVal x As Single, ByVal y As Single, ByVal id As Integer)
        'Stops boxes becoming stuck in walls, platforms or other boxes when they are dropped
        'Returns false if no such position is found
        Dim j As Integer
        Dim drop As Boolean = True

        'Check wall collisions and shunt new box position if need be
        If wall(Math.Floor(x / 32), Math.Floor(y / 32)) And wall(Math.Floor(x / 32) + 1, Math.Floor(y / 32)) Then
            y = Math.Floor(y / 32) * 32 + 32
        End If
        If Math.Floor(y / 32) = y / 32 Then
            If wall(Math.Floor(x / 32), Math.Floor(y / 32)) Then
                x = Math.Floor(x / 32) * 32 + 32
            ElseIf wall(Math.Floor(x / 32) + 1, Math.Floor(y / 32)) Then
                x = Math.Floor(x / 32) * 32
            End If
        Else
            If wall(Math.Floor(x / 32), Math.Floor(y / 32)) Or wall(Math.Floor(x / 32), Math.Floor(y / 32) + 1) Then
                x = Math.Floor(x / 32) * 32 + 32
            ElseIf wall(Math.Floor(x / 32) + 1, Math.Floor(y / 32)) Or wall(Math.Floor(x / 32) + 1, Math.Floor(y / 32) + 1) Then
                x = Math.Floor(x / 32) * 32
            End If
        End If

        'check for boxes in the requested drop space
        For j = 1 To boxCount
            If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) And (Not j = id) Then
                If x + 32 > box(j).x(time - 1) And x < box(j).x(time - 1) + 32 And y + 32 > box(j).y(time - 1) And y < box(j).y(time - 1) + 32 Then
                    drop = False
                    Exit For
                End If
            End If
        Next

        'check for platforms in the requested drop space
        For j = 0 To platCount - 1
            If x + 32 > plat(j).x(time) And x < plat(j).x(time) + plat(j).width And y + 32 > plat(j).y(time) And y < plat(j).y(time) + plat(j).height Then
                drop = False
                Exit For
            End If
        Next

        'returns the new drop positon
        return1 = x
        return2 = y
        'false if no position is found
        Return drop

    End Function

    Private Sub CheckChronofragBox(ByVal id As Integer)
        Audio.PlaySoundEffect(GameSound("portal_in"))
        box(id).playSound(time + portal_in_length) = "portal_in_rev"
        'check chronoporting into platforms
        Dim j As Integer
        For j = 0 To platCount - 1
            If box(id).x(time - 1) + 32 > plat(j).x(time) And box(id).x(time - 1) < plat(j).x(time) + plat(j).width And box(id).y(time - 1) < plat(j).y(time) + plat(j).height And box(id).y(time - 1) + 32 > plat(j).y(time) Then
                box(id).carry(time) = False
                box(id).t_end = time
                box(id).t_end_type = 1
                Exit Sub
            End If
        Next
        'check chronoporting into boxes
        For j = 1 To boxCount
            If (Not j = id) And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                If box(id).x(time - 1) + 32 > box(j).x(time - 1) And box(id).x(time - 1) < box(j).x(time - 1) + 32 And box(id).y(time - 1) < box(j).y(time - 1) + 32 And box(id).y(time - 1) + 32 > box(j).y(time - 1) Then
                    box(id).carry(time) = False
                    box(id).t_end = time
                    box(id).t_end_type = 1
                    Exit Sub
                End If
            End If
        Next
        box(id).t_end = 0
        box(id).t_end_type = 0
    End Sub

    Private Sub DrawBox(ByVal id As Integer)
        Graphics.DrawBitmap(GameImage("box"), box(id).x(time), box(id).y(time))
    End Sub

    Private Sub DrawBoxEffects(ByVal id As Integer)

        'Text.DrawText(box(id).t_start, Color.Black, GameFont("Small"), 250, 100 + id * 30) 'DEBUG
        'Text.DrawText(box(id).t_end, Color.Black, GameFont("Small"), 300, 100 + id * 30) 'DEBUG

        'Text.DrawText(box(id).x(time), Color.Black, GameFont("Small"), 350, 100 + id * 30) 'DEBUG
        'Text.DrawText(box(id).y(time), Color.Black, GameFont("Small"), 450, 100 + id * 30) 'DEBUG

        'Text.DrawText(id, Color.Black, GameFont("Medium"), box(id).x(time) - 30, box(id).y(time)) 'DEBUG

        'Draws portal effects for boxes, start_gfx and end_gfx can be changed to stop the drawing
        If time - box(id).t_start < 50 And box(id).start_gfx And (Not box(id).x(box(id).t_start) = 0) Then
            Graphics.DrawBitmapPart(GameImage("portal_effect"), Math.Floor((time - box(id).t_start) / 5) * 50, 0, 50, 50, box(id).x(box(id).t_start) - 9, box(id).y(box(id).t_start) - 9)
        End If
        If box(id).t_end_type = 0 Then
            If time - box(id).t_end < 50 And (Not box(id).t_end = 0) And (Not box(id).x(box(id).t_end) = 0) Then
                Graphics.DrawBitmapPart(GameImage("portal_effect"), Math.Floor((time - box(id).t_end) / 5) * 50, 0, 50, 50, box(id).x(box(id).t_end) - 9, box(id).y(box(id).t_end) - 9)
            End If
        ElseIf box(id).t_end_type = 1 Then
            If time - box(id).t_end < 28 And box(id).t_end > 0 Then
                Graphics.DrawBitmapPart(GameImage("box_die"), Math.Floor((time - box(id).t_end) / 2) * 64, 0, 64, 100, box(id).x(box(id).t_end - 1) - 15, box(id).y(box(id).t_end - 1) - 7)
            End If
        End If

    End Sub

    Private Sub SwitchCheck(ByVal id As Integer, ByVal changeState As Boolean)
        'Checks the state (on/off) of the switch for this frame

        If switch(id).type = 0 Then 'push button type switch

            Dim j As Integer
            Dim oldState As Boolean = switch(id).t_state(time - 1)
            switch(id).t_state(time) = False
            switch(id).state = False

            'reset audio
            If switch(id).visible Then
                If switch(id).playSound(time + switch_push_down_length) = "switch_push_down" Then
                    switch(id).playSound(time + switch_push_down_length) = ""
                End If
                If switch(id).playSound(time + switch_push_up_length) = "switch_push_up" Then
                    switch(id).playSound(time + switch_push_up_length) = ""
                End If
            End If

            'check collision with box
            If switch(id).hit_box Then
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).x + switch(id).width > box(j).x(time - 1) And switch(id).x < box(j).x(time - 1) + 32 And switch(id).y + switch(id).height > box(j).y(time - 1) And switch(id).y < box(j).y(time - 1) + 32 Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If (Not oldState) And switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_push_down"))
                                switch(id).playSound(time + switch_push_down_length) = "switch_push_down_rev"
                            End If
                            Exit Sub
                        End If
                    End If
                Next
            End If

            'check collision with platform
            If switch(id).hit_plat Then
                For j = 0 To platCount - 1
                    If Not switch(id).attach = j And (switch(id).x + switch(id).width > plat(j).x(time - 1) And switch(id).x < plat(j).x(time - 1) + plat(j).width And switch(id).y + switch(id).height > plat(j).y(time - 1) And switch(id).y < plat(j).y(time - 1) + plat(j).height) Then
                        switch(id).state = True
                        switch(id).t_state(time) = True
                    If (Not oldState) And switch(id).visible Then
                        Audio.PlaySoundEffect(GameSound("switch_push_down"))
                        switch(id).playSound(time + switch_push_down_length) = "switch_push_down_rev"
                    End If
                    Exit Sub
                End If
            Next
            End If

            'check collision with guy
            If switch(id).hit_guy Then
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).x + switch(id).width > guy(j).x(time - 1) And switch(id).x < guy(j).x(time - 1) + guy_width And switch(id).y + switch(id).height > guy(j).y(time - 1) And switch(id).y < guy(j).y(time - 1) + 32 Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If (Not oldState) And switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_push_down"))
                                switch(id).playSound(time + switch_push_down_length) = "switch_push_down_rev"
                            End If
                            Exit Sub
                        End If
                    End If
                Next
            End If

            'check collision with wall
            If switch(id).hit_wall Then
                If wall(Math.Floor((switch(id).x) / 32), Math.Floor((switch(id).y) / 32)) Or wall(Math.Floor((switch(id).x + switch(id).width) / 32), Math.Floor((switch(id).y) / 32)) Or wall(Math.Floor((switch(id).x) / 32), Math.Floor((switch(id).y + switch(id).height) / 32)) Or wall(Math.Floor((switch(id).x + switch(id).width) / 32), Math.Floor((switch(id).y + switch(id).height) / 32)) Then
                    switch(id).state = True
                    switch(id).t_state(time) = True
                    If (Not oldState) And switch(id).visible Then
                        Audio.PlaySoundEffect(GameSound("switch_push_down"))
                        switch(id).playSound(time + switch_push_down_length) = "switch_push_down_rev"
                    End If
                    Exit Sub
                End If
            End If

            If oldState And switch(id).visible Then
                Audio.PlaySoundEffect(GameSound("switch_push_up"))
                switch(id).playSound(time + switch_push_up_length) = "switch_push_up_rev"
            End If

        ElseIf switch(id).type = 1 Then 'duel toggle type

            Dim j As Integer
            switch(id).t_state(time) = switch(id).t_state(time - 1)

            'reset audio
            switch(id).playSound(time + switch_toggle_length) = ""

            'keep state from previous step

            ''CHECKS FOR ON SWITCH
            'check collision with box
            If (Not switch(id).t_state(time)) And switch(id).hit_box Then
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).x + switch(id).width > box(j).x(time - 1) And switch(id).x < box(j).x(time - 1) + 32 And switch(id).y + switch(id).height > box(j).y(time - 1) And switch(id).y < box(j).y(time - 1) + 32 Then
                            switch(id).t_state(time) = True
                        End If
                    End If
                Next
            End If

            'check collision with platform
            If (Not switch(id).t_state(time)) And switch(id).hit_plat Then
                For j = 0 To platCount - 1
                    If Not switch(id).attach = j And (switch(id).x + switch(id).width > plat(j).x(time - 1) And switch(id).x < plat(j).x(time - 1) + plat(j).width And switch(id).y + switch(id).height > plat(j).y(time - 1) And switch(id).y < plat(j).y(time - 1) + plat(j).height) Then
                        switch(id).t_state(time) = True
                    End If
                Next
            End If

            'check collision with box
            If (Not switch(id).t_state(time)) And switch(id).hit_guy Then
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).x + switch(id).width > guy(j).x(time - 1) And switch(id).x < guy(j).x(time - 1) + guy_width And switch(id).y + switch(id).height > guy(j).y(time - 1) And switch(id).y < guy(j).y(time - 1) + 32 Then
                            switch(id).t_state(time) = True
                        End If
                    End If
                Next
            End If

            'check collision with wall
            If (Not switch(id).t_state(time)) And switch(id).hit_wall Then
                If wall(Math.Floor((switch(id).x) / 32), Math.Floor((switch(id).y) / 32)) Or wall(Math.Floor((switch(id).x + switch(id).width) / 32), Math.Floor((switch(id).y) / 32)) Or wall(Math.Floor((switch(id).x) / 32), Math.Floor((switch(id).y + switch(id).height) / 32)) Or wall(Math.Floor((switch(id).x + switch(id).width) / 32), Math.Floor((switch(id).y + switch(id).height) / 32)) Then
                    switch(id).t_state(time) = True
                End If
            End If

            ''CHECKS FOR OFF SWITCH
            'check collision with box
            If switch(id).t_state(time) And switch(id).hit_box Then
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).x2 + switch(id).width > box(j).x(time - 1) And switch(id).x2 < box(j).x(time - 1) + 32 And switch(id).y2 + switch(id).height > box(j).y(time - 1) And switch(id).y2 < box(j).y(time - 1) + 32 Then
                            switch(id).t_state(time) = False
                        End If
                    End If
                Next
            End If

            'check collision with platform
            If switch(id).t_state(time) And switch(id).hit_plat Then
                For j = 0 To platCount - 1
                    If Not switch(id).attach2 = j And (switch(id).x2 + switch(id).width > plat(j).x(time - 1) And switch(id).x2 < plat(j).x(time - 1) + plat(j).width And switch(id).y2 + switch(id).height > plat(j).y(time - 1) And switch(id).y2 < plat(j).y(time - 1) + plat(j).height) Then
                        switch(id).t_state(time) = False
                    End If
                Next
            End If

            'check collision with guy
            If switch(id).t_state(time) And switch(id).hit_guy Then
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).x2 + switch(id).width > guy(j).x(time - 1) And switch(id).x2 < guy(j).x(time - 1) + guy_width And switch(id).y2 + switch(id).height > guy(j).y(time - 1) And switch(id).y2 < guy(j).y(time - 1) + 32 Then
                            switch(id).t_state(time) = False
                        End If
                    End If
                Next
            End If

            'check collision with wall
            If switch(id).t_state(time) And switch(id).hit_wall Then
                If wall(Math.Floor((switch(id).x2) / 32), Math.Floor((switch(id).y2) / 32)) Or wall(Math.Floor((switch(id).x2 + switch(id).width) / 32), Math.Floor((switch(id).y2) / 32)) Or wall(Math.Floor((switch(id).x2) / 32), Math.Floor((switch(id).y2 + switch(id).height) / 32)) Or wall(Math.Floor((switch(id).x2 + switch(id).width) / 32), Math.Floor((switch(id).y2 + switch(id).height) / 32)) Then
                    switch(id).t_state(time) = False
                End If
            End If

            Dim oldState As Boolean = switch(id).state
            switch(id).state = switch(id).t_state(time)

            If (oldState Xor switch(id).state) And switch(id).visible Then
                Audio.PlaySoundEffect(GameSound("switch_toggle"))
                switch(id).playSound(time + switch_toggle_length) = "switch_toggle_rev"
            End If

        ElseIf switch(id).type = 2 Then 'trip laser switch

            'reset audio
            switch(id).playSound(time + switch_trip_laser_length) = ""

            switch(id).state = switch(id).t_state(time - 1)

            'If the laser is tripped it will never be untripped
            If switch(id).state Then
                switch(id).t_state(time) = True
                Exit Sub
            End If
            switch(id).t_state(time) = False

            'keep state from previous step
            Dim i, j As Integer
            Dim dis As Integer = 1000 'distance that the laser reaches
            Dim type As Integer = 0 'type of object hit. 1 = box, 2 = plat, 3 = guy, 4 = wall

            If switch(id).rotation = 0 Then 'Check Up pointing laser

                'check collision with box
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).x + 8 > box(j).x(time - 1) And switch(id).x + 8 < box(j).x(time - 1) + 32 Then
                            If switch(id).y + 8 > box(j).y(time - 1) And (switch(id).y) - (box(j).y(time - 1) + 32) < dis Then
                                dis = (switch(id).y) - (box(j).y(time - 1) + 32)
                                type = 1
                            End If
                        End If
                    End If
                Next

                'check collision with platform
                For j = 0 To platCount - 1
                    If switch(id).x + 8 > plat(j).x(time - 1) And switch(id).x + 8 < plat(j).x(time - 1) + plat(j).width Then
                        If switch(id).y + 8 > plat(j).y(time - 1) And (switch(id).y) - (plat(j).y(time - 1) + plat(i).height) < dis Then
                            dis = (switch(id).y) - (plat(j).y(time - 1) + plat(i).height)
                            type = 2
                        End If
                    End If
                Next

                'check collision with guy
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).x + 8 > guy(j).x(time - 1) And switch(id).x + 8 < guy(j).x(time - 1) + guy_width Then
                            If switch(id).y + 8 > guy(j).y(time - 1) And (switch(id).y) - (guy(j).y(time - 1) + 32) < dis Then
                                dis = (switch(id).y) - (guy(j).y(time - 1) + 32)
                                type = 3
                            End If
                        End If
                    End If
                Next

                'check collision with wall
                i = Math.Floor(switch(id).x / 32)
                For j = Math.Floor(switch(id).y / 32) To Math.Floor((switch(id).y - dis) / 32) Step -1
                    If wall(i, j) Then
                        dis = (switch(id).y) - (j + 1) * 32
                        type = 4
                        Exit For
                    End If
                Next

                'drawing the laser
                switch(id).laser_x(time) = switch(id).x + 8
                switch(id).laser_y(time) = switch(id).y - dis

                'Check the detected type against the lasers trigger types
                If type = 1 Then
                    If switch(id).hit_box Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 2 Then
                    If switch(id).hit_plat Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 3 Then
                    If switch(id).hit_guy Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 4 Then
                    If switch(id).hit_wall Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                End If

            ElseIf switch(id).rotation = 1 Then 'check left pointing laser

                'check collision with box
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).y + 8 > box(j).y(time - 1) And switch(id).y + 8 < box(j).y(time - 1) + 32 Then
                            If switch(id).x + 8 > box(j).x(time - 1) And (switch(id).x) - (box(j).x(time - 1) + 32) < dis Then
                                dis = (switch(id).x) - (box(j).x(time - 1) + 32)
                                type = 1
                            End If
                        End If
                    End If
                Next

                'check collision with platform
                For j = 0 To platCount - 1
                    If switch(id).y + 8 > plat(j).y(time - 1) And switch(id).y + 8 < plat(j).y(time - 1) + plat(j).height Then
                        If switch(id).x + 8 > plat(j).x(time - 1) And (switch(id).x) - (plat(j).x(time - 1) + plat(j).width) < dis Then
                            dis = (switch(id).x) - (plat(j).x(time - 1) + plat(j).width)
                            type = 2
                        End If
                    End If
                Next

                'check collision with guy
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).y + 8 > guy(j).y(time - 1) And switch(id).y + 8 < guy(j).y(time - 1) + 32 Then
                            If switch(id).x + 8 > guy(j).x(time - 1) And (switch(id).x) - (guy(j).x(time - 1) + guy_width) < dis Then
                                dis = (switch(id).x) - (guy(j).x(time - 1) + guy_width)
                                type = 3
                            End If
                        End If
                    End If
                Next

                'check collision with wall
                j = Math.Floor((switch(id).y) / 32)
                For i = Math.Floor(switch(id).x / 32) To Math.Floor((switch(id).x - dis) / 32) Step -1
                    If wall(i, j) Then
                        dis = (switch(id).x) - (i + 1) * 32
                        type = 4
                        Exit For
                    End If
                Next

                'drawing the laser
                switch(id).laser_x(time) = switch(id).x - dis
                switch(id).laser_y(time) = switch(id).y + 8

                'Check the detected type against the lasers trigger types
                If type = 1 Then
                    If switch(id).hit_box Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 2 Then
                    If switch(id).hit_plat Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 3 Then
                    If switch(id).hit_guy Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 4 Then
                    If switch(id).hit_wall Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                End If

            ElseIf switch(id).rotation = 2 Then 'check down pointing laser

                'check collision with box
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).x + 8 > box(j).x(time - 1) And switch(id).x + 8 < box(j).x(time - 1) + 32 Then
                            If switch(id).y + 8 < box(j).y(time - 1) + 32 And box(j).y(time - 1) - switch(id).y < dis Then
                                dis = (box(j).y(time - 1)) - (switch(id).y)
                                type = 1
                            End If
                        End If
                    End If
                Next

                'check collision with platform
                For j = 0 To platCount - 1
                    If switch(id).x + 8 > plat(j).x(time - 1) And switch(id).x + 8 < plat(j).x(time - 1) + plat(j).width Then
                        If switch(id).y + 8 < plat(j).y(time - 1) + plat(j).height And plat(j).y(time - 1) - switch(id).y < dis Then
                            dis = plat(j).y(time - 1) - switch(id).y
                            type = 2
                        End If
                    End If
                Next

                'check collision with guy
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).x + 8 > guy(j).x(time - 1) And switch(id).x + 8 < guy(j).x(time - 1) + guy_width Then
                            If switch(id).y + 8 < guy(j).y(time - 1) + 32 And guy(j).y(time - 1) - switch(id).y < dis Then
                                dis = guy(j).y(time - 1) - switch(id).y
                                type = 3
                            End If
                        End If
                    End If
                Next

                'check collision with wall
                i = Math.Floor((switch(id).x) / 32)
                For j = Math.Floor(switch(id).y / 32) To Math.Floor((switch(id).y + dis) / 32)
                    If wall(i, j) Then
                        dis = (j + 1) * 32 - switch(id).y
                        type = 4
                        Exit For
                    End If
                Next

                'drawing the laser
                switch(id).laser_x(time) = switch(id).x + 8
                switch(id).laser_y(time) = switch(id).y + dis

                'Check the detected type against the lasers trigger types
                If type = 1 Then
                    If switch(id).hit_box Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 2 Then
                    If switch(id).hit_plat Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 3 Then
                    If switch(id).hit_guy Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 4 Then
                    If switch(id).hit_wall Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                End If

            ElseIf switch(id).rotation = 3 Then 'check right pointing laser

                'check collision with box
                For j = 1 To boxCount
                    If box(j).carry(time - 1) = False And (box(j).t_start <= time And (box(j).t_end >= time Or box(j).t_end = 0)) Then
                        If switch(id).y + 8 > box(j).y(time - 1) And switch(id).y + 8 < box(j).y(time - 1) + 32 Then
                            If switch(id).x + 8 < box(j).x(time - 1) + 32 And box(j).x(time - 1) - switch(id).x < dis Then
                                dis = box(j).x(time - 1) - switch(id).x
                                type = 1
                            End If
                        End If
                    End If
                Next

                'check collision with platform
                For j = 0 To platCount - 1
                    If switch(id).y + 8 > plat(j).y(time - 1) And switch(id).y + 8 < plat(j).y(time - 1) + plat(j).height Then
                        If switch(id).x + 8 < plat(j).x(time - 1) + plat(j).width And (plat(j).x(time - 1)) - (switch(id).x) < dis Then
                            dis = (plat(j).x(time - 1)) - (switch(id).x)
                            type = 2
                        End If
                    End If
                Next

                'check collision with guy
                For j = 0 To guyCount
                    If guy(j).exist(time - 1) Then
                        If switch(id).y + 8 > guy(j).y(time - 1) And switch(id).y + 8 < guy(j).y(time - 1) + 32 Then
                            If switch(id).x + 8 < guy(j).x(time - 1) + guy_width And guy(j).x(time - 1) - (switch(id).x) < dis Then
                                dis = guy(j).x(time - 1) - (switch(id).x)
                                type = 3
                            End If
                        End If
                    End If
                Next

                'check collision with wall
                j = Math.Floor((switch(id).y) / 32)
                For i = Math.Floor(switch(id).x / 32) To Math.Floor((switch(id).x + dis) / 32)
                    If wall(i, j) Then
                        dis = (i + 1) * 32 - switch(id).x
                        type = 4
                        Exit For
                    End If
                Next

                'drawing
                switch(id).laser_x(time) = switch(id).x + dis
                switch(id).laser_y(time) = switch(id).y + 8

                'Check the detected type against the lasers trigger types
                If type = 1 Then
                    If switch(id).hit_box Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 2 Then
                    If switch(id).hit_plat Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 3 Then
                    If switch(id).hit_guy Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                ElseIf type = 4 Then
                    If switch(id).hit_wall Then
                        If changeState Then
                            switch(id).state = True
                            switch(id).t_state(time) = True
                            If switch(id).visible Then
                                Audio.PlaySoundEffect(GameSound("switch_trip_laser"))
                                switch(id).playSound(time + switch_trip_laser_length) = "switch_trip_laser_rev"
                            End If
                        End If
                    End If
                End If

            End If
            End If

    End Sub

    Private Sub GateLogic(ByVal id As Integer)
        'some gate logic between switches and platforms. only allows 2 switches

        gate(id).state = False

        If gate(id).type = 0 Then 'and
            If switch(gate(id).attach1).state And switch(gate(id).attach2).state Then
                gate(id).state = True
            End If
        ElseIf gate(id).type = 1 Then 'or
            If switch(gate(id).attach1).state Or switch(gate(id).attach2).state Then
                gate(id).state = True
            End If
        ElseIf gate(id).type = 2 Then 'xor
            If switch(gate(id).attach1).state Xor switch(gate(id).attach2).state Then
                gate(id).state = True
            End If
        End If

    End Sub

    Private Sub MoveSwitch(ByVal id As Integer)
        'Sets switch position if attached to platform

        If Not switch(id).attach = -1 Then
            switch(id).x = switch(id).xoff + plat(switch(id).attach).x(time)
            switch(id).y = switch(id).yoff + plat(switch(id).attach).y(time)
        End If

        If switch(id).type = 1 Then
            'moves the off toggle if it's a toggle switch
            If Not switch(id).attach2 = -1 Then
                switch(id).x2 = switch(id).xoff2 + plat(switch(id).attach2).x(time)
                switch(id).y2 = switch(id).yoff2 + plat(switch(id).attach2).y(time)
            End If
        End If
    End Sub

    Private Sub DrawSwitch(ByVal id As Integer)
        'Draws the switch

        If switch(id).type = 0 Then 'push button

            If switch(id).visible Then
                If switch(id).t_state(time) Then
                    If switch(id).rotation = 0 Then
                        Graphics.DrawBitmap(GameImage("button0_down"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 1 Then
                        Graphics.DrawBitmap(GameImage("button1_down"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 2 Then
                        Graphics.DrawBitmap(GameImage("button2_down"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 3 Then
                        Graphics.DrawBitmap(GameImage("button3_down"), switch(id).x, switch(id).y)
                    End If
                Else
                    If switch(id).rotation = 0 Then
                        Graphics.DrawBitmap(GameImage("button0_up"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 1 Then
                        Graphics.DrawBitmap(GameImage("button1_up"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 2 Then
                        Graphics.DrawBitmap(GameImage("button2_up"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 3 Then
                        Graphics.DrawBitmap(GameImage("button3_up"), switch(id).x, switch(id).y)
                    End If
                End If
            End If
        ElseIf switch(id).type = 1 Then 'Toggle

            If switch(id).visible Then
                If switch(id).t_state(time) Then

                    If switch(id).rotation = 0 Then
                        Graphics.DrawBitmap(GameImage("toggle0_down"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 1 Then
                        Graphics.DrawBitmap(GameImage("toggle1_down"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 2 Then
                        Graphics.DrawBitmap(GameImage("toggle2_down"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 3 Then
                        Graphics.DrawBitmap(GameImage("toggle3_down"), switch(id).x, switch(id).y)
                    End If

                    If switch(id).rotation2 = 0 Then
                        Graphics.DrawBitmap(GameImage("toggle0_up"), switch(id).x2, switch(id).y2)
                    ElseIf switch(id).rotation2 = 1 Then
                        Graphics.DrawBitmap(GameImage("toggle1_up"), switch(id).x2, switch(id).y2)
                    ElseIf switch(id).rotation2 = 2 Then
                        Graphics.DrawBitmap(GameImage("toggle2_up"), switch(id).x2, switch(id).y2)
                    ElseIf switch(id).rotation2 = 3 Then
                        Graphics.DrawBitmap(GameImage("toggle3_up"), switch(id).x2, switch(id).y2)
                    End If

                Else

                    If switch(id).rotation = 0 Then
                        Graphics.DrawBitmap(GameImage("toggle0_up"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 1 Then
                        Graphics.DrawBitmap(GameImage("toggle1_up"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 2 Then
                        Graphics.DrawBitmap(GameImage("toggle2_up"), switch(id).x, switch(id).y)
                    ElseIf switch(id).rotation = 3 Then
                        Graphics.DrawBitmap(GameImage("toggle3_up"), switch(id).x, switch(id).y)
                    End If

                    If switch(id).rotation2 = 0 Then
                        Graphics.DrawBitmap(GameImage("toggle0_down"), switch(id).x2, switch(id).y2)
                    ElseIf switch(id).rotation2 = 1 Then
                        Graphics.DrawBitmap(GameImage("toggle1_down"), switch(id).x2, switch(id).y2)
                    ElseIf switch(id).rotation2 = 2 Then
                        Graphics.DrawBitmap(GameImage("toggle2_down"), switch(id).x2, switch(id).y2)
                    ElseIf switch(id).rotation2 = 3 Then
                        Graphics.DrawBitmap(GameImage("toggle3_down"), switch(id).x2, switch(id).y2)
                    End If

                End If
            End If
        ElseIf switch(id).type = 2 Then 'trip laser

            If switch(id).visible Then
                If switch(id).rotation = 0 Then
                    Graphics.DrawBitmap(GameImage("laserswitch0"), switch(id).x, switch(id).y)
                    If Not switch(id).t_state(time) Then
                        Graphics.DrawLine(Color.Red, switch(id).x + 8, switch(id).y + 5, switch(id).laser_x(time), switch(id).laser_y(time))
                    End If
                ElseIf switch(id).rotation = 1 Then
                    Graphics.DrawBitmap(GameImage("laserswitch1"), switch(id).x, switch(id).y)
                    If Not switch(id).t_state(time) Then
                        Graphics.DrawLine(Color.Red, switch(id).x + 5, switch(id).y + 8, switch(id).laser_x(time), switch(id).laser_y(time))
                    End If
                ElseIf switch(id).rotation = 2 Then
                    Graphics.DrawBitmap(GameImage("laserswitch2"), switch(id).x, switch(id).y)
                    If Not switch(id).t_state(time) Then
                        Graphics.DrawLine(Color.Red, switch(id).x + 8, switch(id).y + 5, switch(id).laser_x(time), switch(id).laser_y(time))
                    End If
                ElseIf switch(id).rotation = 3 Then
                    Graphics.DrawBitmap(GameImage("laserswitch3"), switch(id).x, switch(id).y)
                    If Not switch(id).t_state(time) Then
                        Graphics.DrawLine(Color.Red, switch(id).x + 5, switch(id).y + 8, switch(id).laser_x(time), switch(id).laser_y(time))
                    End If
                End If
            End If

        End If

    End Sub

    Private Sub MovePlatform(ByVal id As Integer)

        If Not plat(id).switch_id = -1 Then 'check state of activation switch
            If plat(id).switch_type Then 'true = switch, false = gate
                plat(id).state = switch(plat(id).switch_id).state
            Else
                plat(id).state = gate(plat(id).switch_id).state
            End If
        End If
        Dim mobile As Boolean = False
        If Not (plat(id).xspeed(time) = 0 And plat(id).yspeed(time) = 0) Then
            mobile = True
        End If
        plat(id).xspeed(time) = 0
        plat(id).yspeed(time) = 0

        If plat(id).state Then 'IF THE PLATFORM IS ON
            If plat(id).xspeed_on < 0 Then
                If plat(id).x(time - 1) > plat(id).x_on Then
                    plat(id).xspeed(time) = plat(id).xspeed_on
                End If
            Else
                If plat(id).x(time - 1) < plat(id).x_on Then
                    plat(id).xspeed(time) = plat(id).xspeed_on
                End If
            End If
            If plat(id).yspeed_on < 0 Then
                If plat(id).y(time - 1) > plat(id).y_on Then
                    plat(id).yspeed(time) = plat(id).yspeed_on
                End If
            Else
                If plat(id).y(time - 1) < plat(id).y_on Then
                    plat(id).yspeed(time) = plat(id).yspeed_on
                End If
            End If
        Else ' IF THE PLATFORM IS OFF
            If plat(id).xspeed_off < 0 Then
                If plat(id).x(time - 1) > plat(id).x_off Then
                    plat(id).xspeed(time) = plat(id).xspeed_off
                End If
            Else
                If plat(id).x(time - 1) < plat(id).x_off Then
                    plat(id).xspeed(time) = plat(id).xspeed_off
                End If
            End If
            If plat(id).yspeed_off < 0 Then
                If plat(id).y(time - 1) > plat(id).y_off Then
                    plat(id).yspeed(time) = plat(id).yspeed_off
                End If
            Else
                If plat(id).y(time - 1) < plat(id).y_off Then
                    plat(id).yspeed(time) = plat(id).yspeed_off
                End If
            End If
        End If

        plat(id).x(time) = plat(id).x(time - 1) + plat(id).xspeed(time)
        plat(id).y(time) = plat(id).y(time - 1) + plat(id).yspeed(time)

        If plat(id).state Then 'IF THE PLATFORM IS ON
            If plat(id).xspeed_on < 0 Then
                If plat(id).x(time) < plat(id).x_on Then
                    plat(id).xspeed(time) = plat(id).x_on - plat(id).x(time - 1)
                    plat(id).x(time) = plat(id).x_on
                End If
            Else
                If plat(id).x(time) > plat(id).x_on Then
                    plat(id).xspeed(time) = plat(id).x_on - plat(id).x(time - 1)
                    plat(id).x(time) = plat(id).x_on
                End If
            End If
            If plat(id).yspeed_on < 0 Then
                If plat(id).y(time) < plat(id).y_on Then
                    plat(id).yspeed(time) = plat(id).y_on - plat(id).y(time - 1)
                    plat(id).y(time) = plat(id).y_on
                End If
            Else
                If plat(id).y(time) > plat(id).y_on Then
                    plat(id).yspeed(time) = plat(id).y_on - plat(id).y(time - 1)
                    plat(id).y(time) = plat(id).y_on
                End If
            End If
        Else ' IF THE PLATFORM IS OFF
            If plat(id).xspeed_off < 0 Then
                If plat(id).x(time) < plat(id).x_off Then
                    plat(id).xspeed(time) = plat(id).x_off - plat(id).x(time - 1)
                    plat(id).x(time) = plat(id).x_off
                End If
            Else
                If plat(id).x(time) > plat(id).x_off Then
                    plat(id).xspeed(time) = plat(id).x_off - plat(id).x(time - 1)
                    plat(id).x(time) = plat(id).x_off
                End If
            End If
            If plat(id).yspeed_off < 0 Then
                If plat(id).y(time) < plat(id).y_off Then
                    plat(id).yspeed(time) = plat(id).y_off - plat(id).y(time - 1)
                    plat(id).y(time) = plat(id).y_off
                End If
            Else
                If plat(id).y(time) > plat(id).y_off Then
                    plat(id).yspeed(time) = plat(id).y_off - plat(id).y(time - 1)
                    plat(id).y(time) = plat(id).y_off
                End If
            End If
        End If

        If Not (plat(id).xspeed(time) = 0 And plat(id).yspeed(time) = 0) Then
            If mobile = False Then
                'play start sound AUDIO
            Else
                'play platform whir AUDIO
            End If
        Else
            If mobile = True Then
                'play end sound AUDIO
            End If
        End If

    End Sub

    Private Sub DrawPlatform(ByVal id As Integer)

        If (plat(id).image = "") Then
            Graphics.FillRectangle(Color.DarkGray, plat(id).x(time), plat(id).y(time), plat(id).width, plat(id).height)
        Else
            Graphics.DrawBitmap(GameImage(plat(id).image), plat(id).x(time), plat(id).y(time))
        End If

    End Sub

    Private Sub MoveSpike(ByVal id As Integer)
        'sets the spikes to their attachment position
        If Not spike(id).attach = -1 Then
            spike(id).x = spike(id).xoff + plat(spike(id).attach).x(time)
            spike(id).y = spike(id).yoff + plat(spike(id).attach).y(time)
        End If
    End Sub

    Private Sub DrawSpike(ByVal id As Integer)
        Dim j As Integer
        If spike(id).rotation = 0 Then
            For j = 0 To spike(id).size - 1 Step 16
                Graphics.DrawBitmap(GameImage("spike0"), spike(id).x + j, spike(id).y)
            Next
        ElseIf spike(id).rotation = 1 Then
            For j = 0 To spike(id).size - 1 Step 16
                Graphics.DrawBitmap(GameImage("spike1"), spike(id).x, spike(id).y + j)
            Next
        ElseIf spike(id).rotation = 2 Then
            For j = 0 To spike(id).size - 1 Step 16
                Graphics.DrawBitmap(GameImage("spike2"), spike(id).x + j, spike(id).y)
            Next
        ElseIf spike(id).rotation = 3 Then
            For j = 0 To spike(id).size - 1 Step 16
                Graphics.DrawBitmap(GameImage("spike3"), spike(id).x, spike(id).y + j)
            Next
        End If

    End Sub

    Private Sub MovePickup(ByVal id As Integer)
        'sets the pickup to it's attachment position
        'If time < pickup(id).t_end Or pickup(id).t_end = 0 Then
        If Not pickup(id).attach = -1 Then
            pickup(id).x = pickup(id).xoff + plat(pickup(id).attach).x(time)
            pickup(id).y = pickup(id).yoff + plat(pickup(id).attach).y(time)
        End If
        'End If
    End Sub

    Private Sub DrawPickup(ByVal id As Integer)

        If time < pickup(id).t_end Or pickup(id).t_end = 0 Then
            If pickup(id).type = 0 Then
                Graphics.DrawBitmap(GameImage("time_jump"), pickup(id).x, pickup(id).y)
            ElseIf pickup(id).type = 1 Then
                Graphics.DrawBitmap(GameImage("time_gun"), pickup(id).x, pickup(id).y)
            ElseIf (pickup(id).type = 2) Then
                Graphics.DrawBitmap(GameImage("reverse_time"), pickup(id).x, pickup(id).y)
            End If
        End If
    End Sub

    Private Sub DrawPortal(ByVal id As Integer)
        Dim subimage As Integer = portalSubimage(time)
        If portal(id).charges = 0 Then
            subimage = portal(id).endSub
        End If
        Dim i
        If portal(id).type = 2 Then
            If time > portal(id).effect Then
                Graphics.DrawBitmapPart(GameImage("portal_white"), subimage, 0, 65, 65, portal(id).x - 32, portal(id).y - 32)
            Else
                Graphics.DrawBitmapPart(GameImage("portal_black"), subimage, 0, 65, 65, portal(id).x - 32, portal(id).y - 32)
            End If
            Text.DrawText(portal(id).effect / 50, Color.Black, GameFont("Medium"), portal(id).x - 10, portal(id).y - 60)
        ElseIf portal(id).type < 2 Then
            If portal(id).effect < 0 Then
                Graphics.DrawBitmapPart(GameImage("portal_white"), subimage, 0, 65, 65, portal(id).x - 32, portal(id).y - 32)
                Text.DrawText(portal(id).effect / 50, Color.Black, GameFont("Medium"), portal(id).x - 10, portal(id).y - 60)
            Else
                Graphics.DrawBitmapPart(GameImage("portal_black"), subimage, 0, 65, 65, portal(id).x - 32, portal(id).y - 32)
                Text.DrawText("+", Color.Black, GameFont("Medium"), portal(id).x - 20, portal(id).y - 60)
                Text.DrawText(portal(id).effect / 50, Color.Black, GameFont("Medium"), portal(id).x - 10, portal(id).y - 60)
            End If
        ElseIf portal(id).type = 3 Then
            Graphics.DrawBitmapPart(GameImage("portal_black"), subimage, 0, 65, 65, portal(id).x - 32, portal(id).y - 32)
            Text.DrawText("mouseX", Color.Black, GameFont("Medium"), portal(id).x - 15, portal(id).y - 60)
        ElseIf portal(id).type = 4 Then
            Graphics.DrawBitmapPart(GameImage("portal_reverse"), subimage, 0, 65, 65, portal(id).x - 32, portal(id).y - 32)
            Text.DrawText("*-1", Color.Black, GameFont("Medium"), portal(id).x - 15, portal(id).y - 60)
        End If

        If portal(id).charges > -1 Then
            Dim used As Boolean = False
            For i = 0 To portal(id).max_charges - 1
                If portal(id).charges <= i Then
                    used = True
                End If
                If used Then
                    Graphics.DrawBitmap(GameImage("portal_charge_used"), portal(id).x + 33, portal(id).y - 32 + 8 * i)
                Else
                    Graphics.DrawBitmap(GameImage("portal_charge"), portal(id).x + 33, portal(id).y - 32 + 8 * i)
                End If
            Next
        End If

    End Sub

    Private Sub MovePortal(ByVal id As Integer)
        'sets the portal to it's attachment position
        If Not portal(id).attach = -1 Then
            portal(id).x = portal(id).xoff + plat(portal(id).attach).x(time)
            portal(id).y = portal(id).yoff + plat(portal(id).attach).y(time)
        End If
    End Sub

    Private Sub DrawEndPortal()
        Graphics.DrawBitmapPart(GameImage("portal_end"), portalSubimage(time), 0, 65, 65, endPortal.x - 32, endPortal.y - 32)
    End Sub

    Private Sub MoveEndPortal()
        'sets the portal to it's attachment position
        If Not endPortal.attach = -1 Then
            endPortal.x = endPortal.xoff + plat(endPortal.attach).x(time)
            endPortal.y = endPortal.yoff + plat(endPortal.attach).y(time)
        End If
    End Sub

    Private Sub CheckPortalTravel()
        'Called when the player hits space
        'Checks time travel with portals for player guy

        Dim i As Integer
        For i = 0 To portalCount - 1
            ' check distance to every portal for collision checking
            If (Not portal(i).charges = 0) And Distance(portal(i).x, portal(i).y, guy(playerGuy).x(time) + 11, guy(playerGuy).y(time) + 16) < 35 Then
                'Remove a portal charge
                If portal(i).charges > 0 Then
                    portal(i).charges -= 1
                    If portal(i).charges = 0 Then
                        portal(i).endSub = portalSubimage(time)
                    End If
                End If

                'Tpyes:
                '0 = add
                '1 = add+flip
                '2 = fixed
                '3 = whenever
                '4 = reverse
                If portal(i).type < 2 Then ' add or add+flip
                    'check if the added time is within the time bounds
                    guy(playerGuy).t_end_specific = True 'emerge time is related to enter time
                    guy(playerGuy).t_end_type = 2
                    guy(playerGuy).t_end_portal_id = i
                    JumpToTimePlayerGuy(portal(i).effect + time, i)
                    Audio.PlaySoundEffect(GameSound("portal_sound"))
                    If portal(i).type = 1 Then
                        'flip the portal if it's a flip portal
                        portal(i).effect = portal(i).effect * -1
                    End If
                    Exit For
                ElseIf portal(i).type = 2 Then 'absolute time
                    guy(playerGuy).t_end_specific = False
                    guy(playerGuy).t_end_type = 1
                    guy(playerGuy).t_end_portal_id = i
                    JumpToTimePlayerGuy(portal(i).effect, i)
                    Audio.PlaySoundEffect(GameSound("portal_sound"))
                    Exit For
                ElseIf portal(i).type = 3 Then 'chose time with mouse
                    guy(playerGuy).t_end_specific = False
                    guy(playerGuy).t_end_type = 1
                    guy(playerGuy).t_end_portal_id = i
                    Audio.PlaySoundEffect(GameSound("portal_sound"))
                    If TimelineMode = 0 Then
                        JumpToTimePlayerGuy((mousePosition.X - 48) / TimelineScale, i)
                    ElseIf TimelineMode = 1 Then
                        JumpToTimePlayerGuy((mousePosition.X - 512) / TimelineScale + time, i)
                    End If
                ElseIf portal(i).type = 4 Then 'reverse time
                    guy(playerGuy).t_end_specific = True
                    guy(playerGuy).t_end_type = 2
                    guy(playerGuy).t_end_portal_id = i
                    ReverseTimePlayerGuy(i)
                    Audio.PlaySoundEffect(GameSound("portal_sound"))
                End If
            End If
        Next

    End Sub

    Private Sub CheckEndPortal()
        'Checks for nearby end portals when space is pressed for player guy

        ' check distance to  portal
        If Distance(endPortal.x, endPortal.y, guy(playerGuy).x(time) + 11, guy(playerGuy).y(time) + 16) < 35 Then
            guy(playerGuy).t_end = time
            'drop box
            If Not guy(playerGuy).carry(time) = 0 Then
                box(guy(playerGuy).carry(time)).carry(time - 1) = False
                box(guy(playerGuy).carry(time)).t_end = time
            End If

            'set paradox info
            guy(playerGuy).x_par(time) = guy(playerGuy).x(time) - endPortal.x
            guy(playerGuy).y_par(time) = guy(playerGuy).y(time) - endPortal.y
            guy(playerGuy).xspeed_par(time) = guy(playerGuy).xspeed(time)
            guy(playerGuy).yspeed_par(time) = guy(playerGuy).yspeed(time)
            guy(playerGuy).carry_par(time) = guy(playerGuy).carry(time)

            guy(playerGuy).t_end_specific = False
            guy(playerGuy).t_end_type = 1
            guy(playerGuy).t_end_portal_id = -1

            'Do final paradox checking from start
            time = 1
            fastMode = True
            winCheck = True
            time_aim = maxCheckTime
            BackwardsTimeJumpEvents()
            playerGuy = guyCount + 1 'set last guy to NPC
        End If

    End Sub

    Private Sub CheckTimeJumpTravel()
        'Called when the time jump powerup is activated
        If timeJumps > 0 Then
            guy(playerGuy).t_end_specific = False
            guy(playerGuy).t_end_type = 3
            Audio.PlaySoundEffect(GameSound("time_jump_sound"))
            If TimelineMode = 0 Then
                JumpToTimePlayerGuy((mousePosition.X - 48) / TimelineScale, -1)
            ElseIf TimelineMode = 1 Then
                JumpToTimePlayerGuy((mousePosition.X - 512) / TimelineScale + time, -1)
            End If
            timeJumps = timeJumps - 1
        End If

    End Sub

    Private Sub CheckReverseTime()
        'Called when the time reverse powerup is activated
        If (timeReverses > 0) Then
            guy(playerGuy).t_end_specific = True
            guy(playerGuy).t_end_type = 5
            Audio.PlaySoundEffect(GameSound("time_reverse_sound"))
            ReverseTimePlayerGuy(-1)
            timeReverses -= 1
        End If
    End Sub

    Private Sub CheckGunShootPlayerGuy()
        'Called when the time gun powerup is activated
        If timeGuns > 0 Then
            'get the ID and type of the shot object
            Dim type As Integer, cid As Integer, x As Integer, y As Integer, i As Integer
            type = CollisionLine(playerGuy, guy(playerGuy).x(time) + 12, guy(playerGuy).y(time) + 12, mousePosition.X, mousePosition.Y)
            'type: 0 = wall, 1 = platform, 2 = guy,3 = box
            cid = return1
            x = return2
            y = return3
            'set the laser to be drawn for the next 7 frames
            If guy(playerGuy).t_direction Then
                For i = time + 1 To time + 7
                    guy(playerGuy).laser_x_draw(i) = x
                    guy(playerGuy).laser_y_draw(i) = y
                Next
            Else
                For i = time - 7 To time - 1
                    If i > 1 Then
                        guy(playerGuy).laser_x_draw(i) = x
                        guy(playerGuy).laser_y_draw(i) = y
                    End If
                Next
            End If
            'draw a laser this frame
            Graphics.DrawLine(Color.Red, guy(playerGuy).x(time) + 12, guy(playerGuy).y(time) + 12, x, y)
            Audio.PlaySoundEffect(GameSound("laser_shoot"))
            If guy(playerGuy).t_direction Then
                guy(playerGuy).playSound(time + laser_shoot_length) = "laser_shoot_rev"
            Else
                If time - laser_shoot_length > 1 Then
                    guy(playerGuy).playSound(time - laser_shoot_length) = "laser_shoot_rev"
                Else
                    guy(playerGuy).playSound(1) = "laser_shoot_rev"
                End If
            End If
            'set paradox information
            guy(playerGuy).laser_x(time) = x
            guy(playerGuy).laser_y(time) = y
            guy(playerGuy).laser_type(time) = type
            guy(playerGuy).laser_id(time) = cid
            guy(playerGuy).laser_time(time) = shootTime

            'send the target to the set time
            If type = 2 Then
                guy(playerGuy).laser_objx(time) = guy(cid).x(time)
                guy(playerGuy).laser_objy(time) = guy(cid).y(time)
                JumpToTimeGuy(cid, shootTime)
                guy(cid).t_end_specific = False
                guy(cid).t_end_type = 4
            ElseIf type = 3 Then
                guy(playerGuy).laser_objx(time) = box(cid).x(time)
                guy(playerGuy).laser_objy(time) = box(cid).y(time)
                JumpToTimeBox(cid, shootTime)
            End If
            timeGuns = timeGuns - 1
        End If

    End Sub

    Private Sub CheckGunShootGuy(ByVal id As Integer)
        'checks time gun shooting for past selves

        'get the ID and type of the shot object
        Dim type As Integer, cid As Integer, x As Integer, y As Integer, i As Integer
        type = CollisionLine(id, guy(id).x(time) + 12, guy(id).y(time) + 12, guy(id).laser_x(time), guy(id).laser_y(time))
        'type: 0 = wall, 1 = platform, 2 = guy,3 = box
        cid = return1
        x = return2
        y = return3
        'set the laser to be drawn for the next 7 frames
        If guy(id).t_direction Then
            For i = time + 1 To time + 7
                guy(id).laser_x_draw(i) = x
                guy(id).laser_y_draw(i) = y
            Next
        Else
            For i = time - 7 To time - 1
                If i > 1 Then
                    guy(id).laser_x_draw(i) = x
                    guy(id).laser_y_draw(i) = y
                End If
            Next
        End If
        'draw laser this frame
        Graphics.DrawLine(Color.Red, guy(id).x(time) + 12, guy(id).y(time) + 12, x, y)
        If guy(id).t_direction Xor time_speed = -1 Then
            Audio.PlaySoundEffect(GameSound("laser_shoot"))
        End If
        'Check the ID and type of object shot this time against the one shot origionally
        If guy(id).laser_type(time) = 0 Or guy(id).laser_type(time) = 1 Then
            'If the orgional gun shot a wall or platform the gun is free to shoot another type with no parados
            If type = 2 Then 'send new guy instance to a time
                'update paradox info
                guy(id).laser_x(time) = x
                guy(id).laser_y(time) = y
                guy(id).laser_objx(time) = guy(cid).x(time)
                guy(id).laser_objy(time) = guy(cid).y(time)
                guy(id).laser_type(time) = type
                guy(id).laser_id(time) = cid

                'send guy instance to the aimed at time
                If cid = playerGuy Then
                    JumpToTimePlayerGuy(guy(id).laser_time(time), -1)
                    guy(cid).t_end_type = 4
                    guy(cid).t_end_specific = False
                Else
                    JumpToTimeGuy(cid, guy(id).laser_time(time))
                    guy(cid).t_end_type = 4
                    guy(cid).t_end_specific = False
                End If
            ElseIf type = 3 Then 'send new box instance to a time
                'update paradox info
                guy(id).laser_x(time) = x
                guy(id).laser_y(time) = y
                guy(id).laser_objx(time) = box(cid).x(time)
                guy(id).laser_objy(time) = box(cid).y(time)
                guy(id).laser_type(time) = type
                guy(id).laser_id(time) = cid

                'send box to time
                JumpToTimeBox(cid, guy(id).laser_time(time))
            End If
        ElseIf guy(id).laser_type(time) = 2 Then 'If the laser hit a guy origionally
            If type = 2 Then
                'check guy positions and end times against the old one. The IDs may have changed
                If Not (guy(cid).t_end = time And Math.Abs(guy(id).laser_objx(time) - guy(cid).x(time)) < 10 And Math.Abs(guy(id).laser_objy(time) - guy(cid).y(time)) < 10) Then
                    'paradox if the incorrect guy instance or position was shot
                    Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).laser_objx(time) - 1, guy(id).laser_objy(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                    Text.DrawText("Your past self failed to shoot the right other past self at the right place and time", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                End If
            Else 'must be a paradox if a guy was not shot
                Graphics.DrawBitmap(GameImage("paradox_guy"), guy(id).laser_objx(time) - 1, guy(id).laser_objy(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                Text.DrawText("Your past self failed to shoot the right other past self at the right place and time", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
            End If
        ElseIf guy(id).laser_type(time) = 3 Then 'if a box was shot origionally
            If type = 3 Then
                'check position of the instance shot this time
                If Math.Abs(guy(id).laser_objx(time) - box(cid).x(time)) < 10 And Math.Abs(guy(id).laser_objy(time) - box(cid).y(time)) < 10 Then
                    box(cid).t_end = time
                Else
                    'paradox if the wrong position box instance was shot
                    Graphics.DrawBitmap(GameImage("paradox_box"), guy(id).laser_objx(time) - 1, guy(id).laser_objy(time) - 1)
                    Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                    Text.DrawText("Your past self failed to shoot a box at the right place and time", Color.Red, GameFont("Medium"), 140, 400)
                    restartLevel = True
                End If
            Else 'paradox if a box was not shot
                Graphics.DrawBitmap(GameImage("paradox_box"), guy(id).laser_objx(time) - 1, guy(id).laser_objy(time) - 1)
                Text.DrawText("Paradox", Color.Red, GameFont("Large"), 300.0!, 300.0!)
                Text.DrawText("Your past self failed to shoot a box at the right place and time", Color.Red, GameFont("Medium"), 140, 400)
                restartLevel = True
            End If
        End If

    End Sub

    Private Sub DrawInterface()
        'draws time and weapon info
        Graphics.DrawBitmapPart(GameImage("interfaceImageLeft"), 0, gearSubimage(time), 340, 96, 0, 608)

        'Text.DrawFramerate(700, 5, GameFont("Small"))

        Graphics.DrawBitmapPart(GameImage("interfaceImageRight"), 0, 96 * (weapon - 1), 684, 96, 340, 608)

        'draw time
        Graphics.DrawBitmapPart(GameImage("flip_numbers_big"), time100image(time), 0, 30, 48, 45, 622)
        Graphics.DrawBitmapPart(GameImage("flip_numbers_big"), time10image(time), 0, 30, 48, 75, 622)
        Graphics.DrawBitmapPart(GameImage("flip_numbers_big"), time1image(time), 0, 30, 48, 105, 622)
        Graphics.DrawBitmapPart(GameImage("flip_numbers_big"), timeP1image(time), 0, 30, 48, 140, 622)

        'draw level name
        If Not level = -1 Then
            Text.DrawText(level, Color.Black, GameFont("Interface"), 10, 566)
            Text.DrawText(levelName, Color.Black, GameFont("Interface"), 50, 566)
        Else
            Text.DrawText(levelName, Color.Black, GameFont("Interface"), 20, 566)
        End If

        'draw fast mode text
        If fastMode Then

            If portalSubimage(time) < 1105 Then
                Text.DrawText("Propagating Causality.", Color.Black, GameFont("Interface"), 600, 566)
            ElseIf portalSubimage(time) < 2210 Then
                Text.DrawText("Propagating Causality..", Color.Black, GameFont("Interface"), 600, 566)
            Else
                Text.DrawText("Propagating Causality...", Color.Black, GameFont("Interface"), 600, 566)
            End If
        End If

        'draw time jumps
        If jump1image > 5 * (timeJumps - Math.Floor(timeJumps / 10) * 10) + 1 Then
            jump1image -= 1
        ElseIf jump1image < 5 * (timeJumps - Math.Floor(timeJumps / 10) * 10) + 1 Then
            jump1image += 1
        End If
        If jump10image > 5 * Math.Floor(timeJumps / 10) + 1 Then
            jump10image -= 1
        ElseIf jump10image < 5 * Math.Floor(timeJumps / 10) + 1 Then
            jump10image += 1
        End If
        Graphics.DrawBitmapPart(GameImage("flip_numbers"), 25 * jump10image, 0, 25, 40, 440, 613)
        Graphics.DrawBitmapPart(GameImage("flip_numbers"), 25 * jump1image, 0, 25, 40, 467, 613)

        'draw time guns
        If gun1image > 5 * (timeGuns - Math.Floor(timeGuns / 10) * 10) + 1 Then
            gun1image -= 1
        ElseIf gun1image < 5 * (timeGuns - Math.Floor(timeGuns / 10) * 10) + 1 Then
            gun1image += 1
        End If
        If gun10image > 5 * Math.Floor(timeGuns / 10) + 1 Then
            gun10image -= 1
        ElseIf gun10image < 5 * Math.Floor(timeGuns / 10) + 1 Then
            gun10image += 1
        End If
        Graphics.DrawBitmapPart(GameImage("flip_numbers"), 25 * gun10image, 0, 25, 40, 605, 613)
        Graphics.DrawBitmapPart(GameImage("flip_numbers"), 25 * gun1image, 0, 25, 40, 632, 613)

        'draw time reverses
        If rev1image > 5 * (timeReverses - Math.Floor(timeReverses / 10) * 10) + 1 Then
            rev1image -= 1
        ElseIf rev1image < 5 * (timeReverses - Math.Floor(timeReverses / 10) * 10) + 1 Then
            rev1image += 1
        End If
        If rev10image > 5 * Math.Floor(timeReverses / 10) + 1 Then
            rev10image -= 1
        ElseIf rev10image < 5 * Math.Floor(timeReverses / 10) + 1 Then
            rev10image += 1
        End If
        Graphics.DrawBitmapPart(GameImage("flip_numbers"), 25 * rev10image, 0, 25, 40, 770, 613)
        Graphics.DrawBitmapPart(GameImage("flip_numbers"), 25 * rev1image, 0, 25, 40, 797, 613)

    End Sub

    Private Sub CheckInterface()

        If Input.MouseWasClicked(SwinGame.MouseButton.LeftButton) And mousePosition.Y >= 608 Then 'check interface things

            'Restart level
            If mousePosition.X >= 200 And mousePosition.X <= 290 And mousePosition.Y >= 663 And mousePosition.Y <= 700 Then
                LoadLevel(file_string)
                Exit Sub
            End If

            'Menu
            If mousePosition.X >= 875 And mousePosition.X <= 995 And mousePosition.Y >= 633 And mousePosition.Y <= 680 Then
                runGame = False
                Exit Sub
            End If

            'Time Jump
            If mousePosition.X >= 412 And mousePosition.X <= 527 And mousePosition.Y >= 663 And mousePosition.Y <= 700 Then
                weapon = 1
                Exit Sub
            End If

            'Time Gun
            If mousePosition.X >= 578 And mousePosition.X <= 693 And mousePosition.Y >= 663 And mousePosition.Y <= 700 Then
                weapon = 2
                Exit Sub
            End If

            'Time Reverse
            If mousePosition.X >= 745 And mousePosition.X <= 860 And mousePosition.Y >= 663 And mousePosition.Y <= 700 Then
                weapon = 3
                Exit Sub
            End If

            'Pause is in time events!!
        End If

        'Load level
        If Input.WasKeyTyped(SwinGame.Keys.VK_L) Then
            Form.LoadFile.ShowDialog()
            file_string = Form.LoadFile.FileName
            If (Not file_string = "") Then
                LoadLevel(file_string)
                runGame = True
                level = -1
                Exit Sub
            End If
            Exit Sub
        End If

        'Restart level
        If Input.WasKeyTyped(SwinGame.Keys.VK_R) Then
            LoadLevel(file_string)
            Exit Sub
        End If

    End Sub

    Private Sub DrawTimelineRelative()
        'draw the timeline with current time stationary

        Dim i, j As Integer
        If winCheck Then
            For i = 0 To guyCount
                'draw past guy time lines
                Graphics.FillRectangle(Color.Yellow, 512 + (guy(i).t_start - time) * TimelineScale, 64 + 15 * i, (guy(i).t_end - guy(i).t_start) * TimelineScale, 5)
            Next
        Else
            For i = 0 To guyCount
                If Not i = playerGuy Then
                    ' draw past guy time lines
                    Graphics.FillRectangle(Color.Yellow, 512 + (guy(i).t_start - time) * TimelineScale, 64 + 15 * i, (guy(i).t_end - guy(i).t_start) * TimelineScale, 5)
                End If
            Next

            If playerGuyStore = -1 Then
                ' draw player guy timeline
                Graphics.FillRectangle(Color.Yellow, 512 + (guy(playerGuy).t_start - time) * TimelineScale, 64 + 15 * playerGuy, (time - guy(playerGuy).t_start) * TimelineScale, 5)
                For j = 0 To portalCount - 1
                    ' check distance to every portal
                    If Distance(portal(j).x, portal(j).y, guy(playerGuy).x(time) + 11, guy(playerGuy).y(time) + 16) < 35 Then
                        If portal(j).type = 2 Then
                            'type 1 = set time
                            Graphics.DrawVerticalLine(Color.Blue, 512 + (portal(j).effect - time) * TimelineScale, 64, 74 + guyCount * 15)
                            Exit For
                        ElseIf portal(j).type = 3 Then
                            Graphics.DrawVerticalLine(Color.Blue, mousePosition.X, 64, 74 + guyCount * 15)
                        Else
                            'add/subtract time
                            Graphics.DrawVerticalLine(Color.Blue, 512 + portal(j).effect * TimelineScale, 64, 74 + guyCount * 15)
                            Exit For
                        End If
                    End If
                Next
            Else
                Graphics.FillRectangle(Color.Yellow, 512 + (guy(playerGuyStore).t_start - time) * TimelineScale, 64 + (15 * playerGuyStore), (time_aim - guy(playerGuyStore).t_start) * GameLogic.TimelineScale, 5)
            End If
            If fastMode Then
                Graphics.DrawVerticalLine(Color.Orange, 48 + (time_aim - time) * TimelineScale, 64, 74 + guyCount * 15)
            End If
        End If

        For i = -22 To 22
            If time + i * 250 >= 0 Then
                'draw helpful time marks 
                Graphics.DrawVerticalLine(Color.DarkBlue, 512 + (i - time / 250 + Math.Floor(time / 250)) * 250 * TimelineScale, 52, 62)
            End If
        Next

        'draw current time
        Graphics.DrawVerticalLine(Color.LightGreen, 512, 64, 74 + guyCount * 15)

        If weapon = 1 Then
            'draw time jump aim
            Graphics.DrawVerticalLine(Color.Red, mousePosition.X, 64, 74 + guyCount * 15)
        ElseIf weapon = 2 Then
            'draw time gun aim
            Graphics.DrawVerticalLine(Color.Red, 512 + (shootTime - time) * TimelineScale, 64, 74 + guyCount * 15)
        End If

    End Sub

    Private Sub DrawTimelineAbsolute()
        'draw the timeline with time line stationary
        Dim i, j As Integer
        If winCheck Then
            For i = 0 To guyCount
                ' draw past guy time lines
                Graphics.FillRectangle(Color.Yellow, 48 + guy(i).t_start * TimelineScale, 64 + 15 * i, (guy(i).t_end - guy(i).t_start) * TimelineScale, 5)
            Next
        Else
            For i = 0 To guyCount
                If Not i = playerGuy Then
                    ' draw past guy time lines
                    Graphics.FillRectangle(Color.Yellow, 48 + guy(i).t_start * TimelineScale, 64 + 15 * i, (guy(i).t_end - guy(i).t_start) * TimelineScale, 5)
                End If
            Next
            If playerGuyStore = -1 Then
                ' draw player guy timeline
                Graphics.FillRectangle(Color.Yellow, 48 + guy(playerGuy).t_start * TimelineScale, 64 + 15 * playerGuy, (time - guy(playerGuy).t_start) * TimelineScale, 5)
                For j = 0 To portalCount - 1
                    ' check distance to every portal
                    If Distance(portal(j).x, portal(j).y, guy(playerGuy).x(time) + 11, guy(playerGuy).y(time) + 16) < 35 Then
                        If portal(j).type = 2 Then
                            'type 2 = set time
                            Graphics.DrawVerticalLine(Color.Blue, 48 + portal(j).effect * TimelineScale, 64, 74 + guyCount * 15)
                            Exit For
                        ElseIf portal(j).type = 3 Then
                            'type = 3, whenever
                            Graphics.DrawVerticalLine(Color.Blue, mousePosition.X, 64, 74 + guyCount * 15)
                        Else
                            'add/subtract time
                            Graphics.DrawVerticalLine(Color.Blue, 48 + (time + portal(j).effect) * TimelineScale, 64, 74 + guyCount * 15)
                            Exit For
                        End If
                    End If
                Next
            Else
                Graphics.FillRectangle(Color.Yellow, 48 + (guy(playerGuyStore).t_start * TimelineScale), 64 + (15 * playerGuyStore), (time_aim - guy(playerGuyStore).t_start) * TimelineScale, 5)
            End If
            If fastMode Then
                Graphics.DrawVerticalLine(Color.Orange, 48 + (time_aim) * TimelineScale, 64, 74 + guyCount * 15)
            End If
        End If

        For i = 0 To 36
            'draw helpful time marks 
            Graphics.DrawVerticalLine(Color.DarkBlue, 48 + i * 250 * TimelineScale, 52, 62)
        Next

        'draw current time
        Graphics.DrawVerticalLine(Color.LightGreen, 48 + time * TimelineScale, 64, 74 + guyCount * 15)
        If weapon = 1 Then
            'draw time jump aim
            Graphics.DrawVerticalLine(Color.Red, mousePosition.X, 64, 74 + guyCount * 15)
            Text.DrawText(Math.Ceiling(((mousePosition.X - 48) / TimelineScale) / 5) / 10, Color.Black, GameFont("small"), mousePosition.X, 25)
        ElseIf weapon = 2 Then
            'draw time gun aim
            Graphics.DrawVerticalLine(Color.Red, 48 + shootTime * TimelineScale, 64, 74 + guyCount * 15)
            Text.DrawText(Math.Ceiling((shootTime) / 5) / 10, Color.Black, GameFont("small"), mousePosition.X, 10)
        End If

    End Sub

    Private Sub DrawEverything(ByVal oldBox As Boolean)

        'Draws background image or rectangle
        If backgroundImage = "" Then
            Graphics.FillRectangle(Color.LightGray, 0, 0, 1024, 704)
        Else
            Graphics.DrawBitmap(GameImage(backgroundImage), 32, 32)
        End If

        'draw portals
        Dim i, j As Integer
        For i = 0 To portalCount - 1
            DrawPortal(i)
        Next
        DrawEndPortal()

        'Draw switch instances
        For i = 0 To switchCount - 1
            DrawSwitch(i)
        Next

        'Draw  pickup
        For i = 0 To pickupCount - 1
            DrawPickup(i)
        Next

        'Draw box instances
        For i = 1 To boxCount
            If (box(i).t_start <= time And (box(i).t_end > time Or box(i).t_end = 0)) Then
                If oldBox Then
                    Graphics.DrawBitmap(GameImage("box"), box(i).x(time - 1), box(i).y(time - 1))
                Else
                    Graphics.DrawBitmap(GameImage("box"), box(i).x(time), box(i).y(time))
                End If
            End If
        Next

        ' Draw guy instances
        For i = 0 To guyCount
            DrawGuyEffects(i)
            If (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) Then
                DrawGuy(i)
            End If
        Next

        'Draw spike instances
        For i = 0 To spikeCount - 1
            DrawSpike(i)
        Next

        'Draw platform instances
        For i = 0 To platCount - 1
            DrawPlatform(i)
        Next

        ' Draw Wall
        If foregroundImage = "" Then
            For i = 0 To 31
                For j = 0 To 21
                    If wall(i, j) = True Then
                        Graphics.FillRectangle(Color.Black, i * 32, j * 32, 32, 32)
                    End If
                Next
            Next
        Else
            Graphics.DrawBitmap(GameImage(foregroundImage), 0, 0)
        End If

        'Draw box instances
        For i = 1 To boxCount
            DrawBoxEffects(i)
        Next

    End Sub

    Private Sub PlaySoundsForwards()
        Dim i As Integer

        For i = 0 To guyCount - 1
            If (Not guy(i).playSound(time) = "") Then
                If Not guy(i).t_direction Then
                    Audio.PlaySoundEffect(GameSound(guy(i).playSound(time)))
                End If
            End If
        Next

    End Sub

    Private Sub PlaySoundsBackwards()
        Dim i As Integer

        For i = 1 To boxCount
            If (Not box(i).playSound(time) = "") Then
                Audio.PlaySoundEffect(GameSound(box(i).playSound(time)))
            End If
        Next

        For i = 0 To guyCount - 1
            If (Not guy(i).playSound(time) = "") Then
                If guy(i).t_direction Then
                    Audio.PlaySoundEffect(GameSound(guy(i).playSound(time)))
                End If
            End If
        Next

        For i = 0 To switchCount - 1
            If (Not switch(i).playSound(time) = "") Then
                Audio.PlaySoundEffect(GameSound(switch(i).playSound(time)))
            End If
        Next

    End Sub

    Private Sub JumpToTimePlayerGuy(ByVal newTime As Integer, ByVal portal_id As Integer)
        'Jumps player to a time

        If newTime < 1 Then
            newTime = 1
        End If
        If newTime > maxTime Then
            newTime = maxTime
        End If

        If guy(playerGuy).t_direction Then
            If newTime > maxTime - 3 Then
                newTime = maxTime - 3
            End If
        Else
            If newTime < 3 Then
                newTime = 3
            End If
        End If

        ' set old player instance end time
        guy(playerGuy).t_end = time

        'set end time paradox information
        If portal_id = -1 Then
            guy(playerGuy).x_par(time) = guy(playerGuy).x(time)
            guy(playerGuy).y_par(time) = guy(playerGuy).y(time)
        Else
            'set position paradox info relative to portal position if the jump is due to a portal
            guy(playerGuy).x_par(time) = guy(playerGuy).x(time) - portal(portal_id).x
            guy(playerGuy).y_par(time) = guy(playerGuy).y(time) - portal(portal_id).y
        End If
        guy(playerGuy).xspeed_par(time) = guy(playerGuy).xspeed(time)
        guy(playerGuy).yspeed_par(time) = guy(playerGuy).yspeed(time)
        guy(playerGuy).carry_par(time) = guy(playerGuy).carry(time)

        'create new guy
        ReDimGuy(guyCount + 1)

        'transfere box info
        If Not guy(playerGuy).carry(time) = 0 Then
            If guy(playerGuy).t_direction Then
                'tell old box to end
                box(guy(playerGuy).carry(time)).carry(time - 1) = False
                box(guy(playerGuy).carry(time)).t_end = time

                boxCount = boxCount + 1

                'create new box for the new guy to carry
                ReDimBox(boxCount)

                box(boxCount).x(newTime) = guy(playerGuy).x(time) - 4
                box(boxCount).y(newTime) = guy(playerGuy).y(time) - 32
                box(boxCount).xspeed(newTime) = 0
                box(boxCount).yspeed(newTime) = 0
                box(boxCount).exist(newTime) = True
                box(boxCount).carry(time - 1) = True 'idk why this is here???
                box(boxCount).t_start = newTime
                box(boxCount).t_end = 0

                guy(guyCount + 1).carry(newTime) = boxCount
            Else
                guy(guyCount + 1).carry(newTime) = 1
            End If

        Else
            guy(guyCount + 1).carry(newTime) = 0
        End If

        guyCount = guyCount + 1

        ' set up new guy instance
        guy(guyCount).x(newTime) = guy(playerGuy).x(time)
        guy(guyCount).y(newTime) = guy(playerGuy).y(time)
        guy(guyCount).xspeed(newTime) = guy(playerGuy).xspeed(time)
        guy(guyCount).yspeed(newTime) = guy(playerGuy).yspeed(time)
        guy(guyCount).subimage = 0

        guy(guyCount).t_start = newTime
        guy(guyCount).t_direction = guy(playerGuy).t_direction
        guy(guyCount).face = guy(playerGuy).face
        guy(guyCount).k_left(newTime) = False
        guy(guyCount).k_right(newTime) = False
        guy(guyCount).k_up(newTime) = False
        guy(guyCount).k_down(newTime) = False

        playerGuy = guyCount

        If newTime < time Then ' remove the or true to change back to fast portal mode instead of jump
            time = newTime
            BackwardsTimeJumpEvents()
        Else
            fastMode = True
            time_aim = newTime
        End If

    End Sub


    Private Sub JumpToTimeGuy(ByVal id As Integer, ByVal newTime As Integer)
        'Time Gun prop needs fix.
        'jumps an NPC to a different time

        Dim i, j As Integer
        Dim start As Integer
        Dim finish As Integer

        If newTime < 3 Then
            newTime = 3
        End If
        If newTime > maxTime - 3 Then
            newTime = maxTime - 3
        End If

        Dim maxId As Integer = id

        'the flow on effects my cause the guy to go beyond the start/end
        'this sets the min/max jump time
        For i = id To guyCount
            maxId = i

            If guy(i).t_direction Then
                If guy(i).t_start - (time - newTime) < 1 And Not (i = id) Then
                    newTime = time - guy(i).t_start
                End If
                If Not playerGuy = i Then
                    If guy(i).t_end - (time - newTime) > maxTime Then
                        newTime = time - guy(i).t_end
                    End If
                Else
                    If newTime > maxTime - 3 Then
                        newTime = maxTime - 3
                    End If
                End If
            Else
                If guy(i).t_start - (time - newTime) > maxTime And (Not i = id) Then
                    newTime = time - guy(i).t_start
                End If
                If Not playerGuy = i Then
                    If guy(i).t_end - (time - newTime) < 1 Then
                        newTime = time - guy(i).t_end
                    End If
                Else
                    If newTime < 3 Then
                        newTime = 3
                    End If
                End If
            End If

            If Not guy(i).t_end_specific Then
                Exit For
            End If
        Next

        'difference in times between current and new time
        Dim timeDiff As Integer = time - newTime - 1

        Dim minTime As Integer
        If guy(id).t_direction Then
            minTime = newTime
        Else
            minTime = guy(guyCount).t_end
        End If

        'find min time
        For i = id To guyCount
            maxId = i
            If guy(i).t_direction Then
                If guy(i).t_start - timeDiff < minTime And i <> id Then
                    minTime = guy(i).t_start - timeDiff
                    If minTime < 3 Then
                        minTime = 3
                    End If
                End If
            Else
                If guy(i).t_end - timeDiff < minTime Then
                    minTime = guy(i).t_end - timeDiff
                    If minTime < 3 Then
                        minTime = 3
                    End If
                End If
            End If
            If Not guy(i).t_end_specific Then
                Exit For
            End If
        Next

        ' Move guy instances up one to create room for the new instance
        ReDimGuy(guyCount + 1)
        guyCount = guyCount + 1

        For i = guyCount - 1 To maxId + 1 Step -1
            CopyGuy(i, i + 1)
        Next

        For i = maxId To id + 1 Step -1
            CleanGuy(i + 1)
            guy(i + 1).t_start = guy(i).t_start - timeDiff
            guy(i + 1).t_end = guy(i).t_end - timeDiff

            guy(i + 1).t_end_specific = guy(i).t_end_specific
            guy(i + 1).t_end_type = guy(i).t_end_type
            guy(i + 1).t_end_portal_id = guy(i).t_end_portal_id

            guy(i + 1).t_direction = guy(i).t_direction
            guy(i + 1).face = guy(i).face
            If guy(i).t_direction Then
                start = guy(i).t_start - timeDiff
                If i = playerGuy Then
                    finish = time - timeDiff
                Else
                    finish = guy(i).t_end - timeDiff
                End If
            Else
                If i = playerGuy Then
                    start = time - timeDiff
                Else
                    start = guy(i).t_end - timeDiff
                End If
                finish = guy(i).t_start - timeDiff
            End If
            For j = 0 To 40
                guy(i + 1).pickup_time(j) = guy(i).pickup_time(j)
            Next
            For j = start - 1 To finish + 1
                guy(i + 1).x(j) = guy(i).x(j + timeDiff)
                guy(i + 1).y(j) = guy(i).y(j + timeDiff)
                guy(i + 1).xspeed(j) = guy(i).xspeed(j + timeDiff)
                guy(i + 1).yspeed(j) = guy(i).yspeed(j + timeDiff)
                guy(i + 1).carry(j) = guy(i).carry(j + timeDiff)
                guy(i + 1).k_left(j) = guy(i).k_left(j + timeDiff)
                guy(i + 1).k_right(j) = guy(i).k_right(j + timeDiff)
                guy(i + 1).k_up(j) = guy(i).k_up(j + timeDiff)
                guy(i + 1).k_down(j) = guy(i).k_down(j + timeDiff)
                guy(i + 1).x_par(j) = guy(i).x_par(j + timeDiff)
                guy(i + 1).y_par(j) = guy(i).y_par(j + timeDiff)
                guy(i + 1).xspeed_par(j) = guy(i).xspeed(j + timeDiff)
                guy(i + 1).yspeed_par(j) = guy(i).yspeed(j + timeDiff)
                guy(i + 1).carry_par(j) = guy(i).carry(j + timeDiff)
                guy(i + 1).pickupCheck(j) = guy(i).pickupCheck(j + timeDiff)
                guy(i + 1).pickup_id(j) = guy(i).pickup_id(j + timeDiff)
                guy(i + 1).laser_x(j) = guy(i).laser_x(j + timeDiff)
                guy(i + 1).laser_y(j) = guy(i).laser_y(j + timeDiff)
                guy(i + 1).laser_objx(j) = guy(i).laser_objx(j + timeDiff)
                guy(i + 1).laser_objy(j) = guy(i).laser_objy(j + timeDiff)
                guy(i + 1).laser_id(j) = guy(i).laser_id(j + timeDiff)
                guy(i + 1).laser_type(j) = guy(i).laser_type(j + timeDiff)
                guy(i + 1).laser_time(j) = guy(i).laser_time(j + timeDiff)
                guy(i + 1).y_par_s(j) = guy(i).y_par_s(j + timeDiff)
                guy(i + 1).x_par_s(j) = guy(i).x_par_s(j + timeDiff)
                guy(i + 1).xspeed_par_s(j) = guy(i).xspeed_par_s(j + timeDiff)
                guy(i + 1).yspeed_par_s(j) = guy(i).yspeed_par_s(j + timeDiff)
                guy(i + 1).object_par_s(j) = guy(i).object_par_s(j + timeDiff)
                guy(i + 1).dir_par_s(j) = guy(i).dir_par_s(j + timeDiff)
                guy(i + 1).face_store(j) = guy(i).face_store(j + timeDiff)
                guy(i + 1).laser_x_draw(j) = guy(i).laser_x_draw(j + timeDiff)
                guy(i + 1).laser_y_draw(j) = guy(i).laser_y_draw(j + timeDiff)
                guy(i + 1).playSound(j) = guy(i).playSound(j + timeDiff)
                guy(i + 1).supported_obj(j) = guy(i).supported_obj(j + timeDiff)
                guy(i + 1).supported_id(j) = guy(i).supported_id(j + timeDiff)
                If guy(i).t_direction Then
                    If j > start Then
                        guy(i + 1).exist(j) = True
                    End If
                Else
                    If j < finish Then
                        guy(i + 1).exist(j) = True
                    End If
                End If
            Next
        Next

        CleanGuy(id + 1)

        'Box handeling/creating
        If Not guy(id).carry(time) = 0 Then
            If guy(id).t_direction Then
                box(guy(id).carry(time)).carry(time - 1) = False
                box(guy(id).carry(time)).t_end = time

                boxCount = boxCount + 1

                ReDimBox(boxCount)

                box(boxCount).x(newTime) = guy(id).x(time) - 4
                box(boxCount).y(newTime) = guy(id).y(time) - 32
                box(boxCount).xspeed(newTime) = 0
                box(boxCount).yspeed(newTime) = 0
                box(boxCount).exist(newTime) = True
                box(boxCount).carry(time - 1) = True '???
                box(boxCount).t_start = newTime
                box(boxCount).t_end = 0

                guy(id + 1).carry(newTime) = boxCount
            Else
                guy(id + 1).carry(newTime) = 1
            End If
        Else
            guy(id + 1).carry(newTime) = 0
        End If

        'set up new guy instance
        guy(id + 1).x(newTime) = guy(id).x(time)
        guy(id + 1).y(newTime) = guy(id).y(time)
        guy(id + 1).t_direction = guy(id).t_direction
        guy(id + 1).xspeed(newTime) = guy(id).xspeed(time)
        guy(id + 1).yspeed(newTime) = guy(id).yspeed(time)
        guy(id + 1).subimage = 0

        guy(id + 1).t_start = newTime + 1
        guy(id + 1).t_end = newTime + 1 + guy(id).t_end - time
        If guy(id + 1).t_end > maxCheckTime Then
            maxCheckTime = guy(id + 1).t_end + 50
        End If

        guy(id + 1).t_end_specific = guy(id).t_end_specific
        guy(id + 1).t_end_type = guy(id).t_end_type
        guy(id + 1).t_end_portal_id = guy(id).t_end_portal_id

        guy(id + 1).face = guy(id).face

        If guy(id).t_direction Then
            start = guy(id + 1).t_start
            finish = guy(id + 1).t_end
        Else
            start = guy(id + 1).t_end
            finish = guy(id + 1).t_start
        End If

        For i = 0 To 40
            guy(id + 1).pickup_time(i) = guy(id).pickup_time(i)
        Next

        For i = start To finish
            guy(id + 1).x(i) = guy(id).x(i + timeDiff)
            guy(id + 1).y(i) = guy(id).y(i + timeDiff)
            guy(id + 1).xspeed(i) = guy(id).xspeed(i + timeDiff)
            guy(id + 1).yspeed(i) = guy(id).yspeed(i + timeDiff)
            guy(id + 1).carry(i) = guy(id).carry(i + timeDiff)
            guy(id + 1).k_left(i) = guy(id).k_left(i + timeDiff)
            guy(id + 1).k_right(i) = guy(id).k_right(i + timeDiff)
            guy(id + 1).k_up(i) = guy(id).k_up(i + timeDiff)
            guy(id + 1).k_down(i) = guy(id).k_down(i + timeDiff)
            guy(id + 1).x_par(i) = guy(id).x_par(i + timeDiff)
            guy(id + 1).y_par(i) = guy(id).y_par(i + timeDiff)
            guy(id + 1).xspeed_par(i) = guy(id).xspeed(i + timeDiff)
            guy(id + 1).yspeed_par(i) = guy(id).yspeed(i + timeDiff)
            guy(id + 1).carry_par(i) = guy(id).carry(i + timeDiff)
            guy(id + 1).pickupCheck(i) = guy(id).pickupCheck(i + timeDiff)
            guy(id + 1).pickup_id(i) = guy(id).pickup_id(i + timeDiff)
            guy(id + 1).laser_x(i) = guy(id).laser_x(i + timeDiff)
            guy(id + 1).laser_y(i) = guy(id).laser_y(i + timeDiff)
            guy(id + 1).laser_objx(i) = guy(id).laser_objx(i + timeDiff)
            guy(id + 1).laser_objy(i) = guy(id).laser_objy(i + timeDiff)
            guy(id + 1).laser_id(i) = guy(id).laser_id(i + timeDiff)
            guy(id + 1).laser_type(i) = guy(id).laser_type(i + timeDiff)
            guy(id + 1).laser_time(i) = guy(id).laser_time(i + timeDiff)
            guy(id + 1).y_par_s(i) = guy(id).y_par_s(i + timeDiff)
            guy(id + 1).x_par_s(i) = guy(id).x_par_s(i + timeDiff)
            guy(id + 1).xspeed_par_s(i) = guy(id).xspeed_par_s(i + timeDiff)
            guy(id + 1).yspeed_par_s(i) = guy(id).yspeed_par_s(i + timeDiff)
            guy(id + 1).object_par_s(i) = guy(id).object_par_s(i + timeDiff)
            guy(id + 1).dir_par_s(i) = guy(id).dir_par_s(i + timeDiff)
            guy(id + 1).face_store(i) = guy(id).face_store(i + timeDiff)
            guy(id + 1).laser_x_draw(i) = guy(id).laser_x_draw(i + timeDiff)
            guy(id + 1).laser_y_draw(i) = guy(id).laser_y_draw(i + timeDiff)
            guy(id + 1).playSound(i) = guy(id).playSound(i + timeDiff)
            guy(id + 1).supported_obj(i) = guy(id).supported_obj(i + timeDiff)
            guy(id + 1).supported_id(i) = guy(id).supported_id(i + timeDiff)

            guy(id + 1).exist(i) = True
            guy(id).exist(i + timeDiff) = False
        Next

        'set old player instance end time
        guy(id).t_end = time
        guy(id).exist(guy(id + 1).t_start + timeDiff) = True

        guy(id).x_par(time) = guy(id).x(time)
        guy(id).y_par(time) = guy(id).y(time)
        guy(id).xspeed_par(time) = guy(id).xspeed(time)
        guy(id).yspeed_par(time) = guy(id).yspeed(time)
        guy(id).carry_par(time) = guy(id).carry(time)


        If playerGuyStore = -1 Then
            playerGuy += 1
        Else
            playerGuyStore += 1
        End If

        If maxId = playerGuy - 1 Then
            If timeDiff > 0 Then
                time = time - timeDiff
                BackwardsTimeJumpEvents()
            Else
                time_aim = time - timeDiff
                fastMode = True
                If playerGuyStore = -1 Then
                    playerGuyStore = playerGuy
                    playerGuy = playerGuy + 10
                End If
            End If
        End If

        If minTime < time Then
            If playerGuyStore = -1 Then
                playerGuyStore = playerGuy
                playerGuy = playerGuy + 10
            End If
            If maxId = playerGuy - 1 Then
                time_aim = time - timeDiff
            Else
                If time_speed = 1 Then
                    time_aim = time
                Else
                    time_aim = time + 1
                End If
            End If
            time = minTime
            fastMode = True
            BackwardsTimeJumpEvents()
        End If

        If guy(id).t_direction Then
            CleanGuyBounds(id, guy(id).t_end + 1, maxTime + 50)
        Else
            CleanGuyBounds(id, 0, guy(id).t_start - 1)
        End If

    End Sub

    Private Sub JumpToTimeBox(ByVal id As Integer, ByVal newTime As Integer)

        If newTime < 3 Then
            newTime = 3
        End If
        If newTime > maxTime - 3 Then
            newTime = maxTime - 3
        End If

        ' set old box instance end time
        box(id).t_end = time

        boxCount = boxCount + 1
        ReDimBox(boxCount)

        ' set up new box instance
        box(boxCount).x(newTime) = box(id).x(time)
        box(boxCount).y(newTime) = box(id).y(time)
        box(boxCount).xspeed(newTime) = box(id).xspeed(time)
        box(boxCount).yspeed(newTime) = box(id).yspeed(time)
        box(boxCount).exist(newTime) = True

        box(boxCount).start_gfx = True
        box(boxCount).end_gfx = True

        box(boxCount).t_start = newTime + 1
        box(boxCount).t_end = 0

        If newTime < time Then
            If playerGuyStore = -1 Then
                playerGuyStore = playerGuy
                playerGuy = playerGuy + 10
            End If
            time_aim = time
            time = newTime
            fastMode = True
            BackwardsTimeJumpEvents()
        End If

    End Sub

    Private Sub ReverseTimePlayerGuy(ByVal portal_id As Integer)

        guy(playerGuy).t_end = time

        'set end time paradox information
        If portal_id = -1 Then
            guy(playerGuy).x_par(time) = guy(playerGuy).x(time)
            guy(playerGuy).y_par(time) = guy(playerGuy).y(time)
        Else
            guy(playerGuy).x_par(time) = guy(playerGuy).x(time) - portal(portal_id).x
            guy(playerGuy).y_par(time) = guy(playerGuy).y(time) - portal(portal_id).y
        End If
        guy(playerGuy).xspeed_par(time) = guy(playerGuy).xspeed(time)
        guy(playerGuy).yspeed_par(time) = guy(playerGuy).yspeed(time)
        guy(playerGuy).carry_par(time) = guy(playerGuy).carry(time)

        ReDimGuy(guyCount + 1)

        If (Not guy(playerGuy).carry(time) = 0) And (Not guy(playerGuy).carry(time) = -1) Then
            If (time_speed > 0) Then
                box(guy(playerGuy).carry(time)).carry(time - 1) = False
                box(guy(playerGuy).carry(time)).t_end = time
                guy(guyCount + 1).carry(time) = 1
            Else
                boxCount += 1
                ReDimBox(boxCount)
                box(boxCount).x(time - 1) = (guy(playerGuy).x(time) - 4.0!)
                box(boxCount).y(time - 1) = (guy(playerGuy).y(time) - 32.0!)
                box(boxCount).xspeed(time - 1) = 0.0!
                box(boxCount).yspeed(time - 1) = 0.0!
                box(boxCount).start_gfx = True
                box(boxCount).end_gfx = True
                box(boxCount).exist(time - 1) = True
                box(boxCount).carry(time - 1) = True
                box(boxCount).t_start = time
                box(boxCount).t_end = 0
                guy(guyCount + 1).carry(time) = boxCount
            End If
        Else
            guy(guyCount + 1).carry(time) = 0
        End If

        guyCount = guyCount + 1

        guy(guyCount).x(time) = guy(playerGuy).x(time)
        guy(guyCount).y(time) = guy(playerGuy).y(time)
        guy(guyCount).xspeed(time) = guy(playerGuy).xspeed(time)
        guy(guyCount).yspeed(time) = guy(playerGuy).yspeed(time)

        guy(guyCount).subimage = 0

        guy(guyCount).t_start = time
        guy(guyCount).t_direction = Not guy(playerGuy).t_direction
        guy(guyCount).face = guy(playerGuy).face

        guy(guyCount).k_left(time) = False
        guy(guyCount).k_right(time) = False
        guy(guyCount).k_up(time) = False
        guy(guyCount).k_down(time) = False

        playerGuy = guyCount

        time_speed = time_speed * -1
    End Sub

    Private Sub BackwardsTimeJumpEvents()
        Dim i, j As Integer
        For j = 1 To boxCount
            If box(j).t_end >= time Then
                box(j).t_end = 0
                box(j).t_end_type = 0
            End If
        Next

        For j = 1 To pickupCount
            If pickup(j).t_end >= time Then
                pickup(j).t_end = 0
            End If
        Next

        'calculates the position of things that do not remember their positon
        For i = 0 To switchCount - 1
            MoveSwitch(i)
        Next

        For i = 0 To portalCount - 1
            MovePortal(i)
        Next
        MoveEndPortal()

        For i = 0 To pickupCount - 1
            MovePickup(i)
        Next

        'Move spike
        For i = 0 To spikeCount - 1
            MoveSpike(i)
        Next

    End Sub

    Private Function Distance(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)

        Return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))

    End Function

    Private Function CollisionLine(ByVal shoot_id As Integer, ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)

        Dim type = 0 '0 = wall, 1 = platform, 2 = guy,3 = box
        Dim id = 0 ' ID of the found type

        Dim i As Integer
        Dim grad
        If (x1 - x2) = 0 Then
            If (y1 - y2) = 0 Then
                return1 = 0
                return2 = 0
                return3 = 0
                Return -1
            ElseIf y1 > y2 Then
                grad = -1000
            Else
                grad = 1000
            End If
        ElseIf (y1 - y2) = 0 Then
            grad = 0.001
        Else
            grad = (y1 - y2) / (x1 - x2)
        End If

        Dim xDistance = 1000
        Dim yDistance = 700

        If x1 > x2 Then
            If y1 > y2 Then 'north west quadrant

                For i = 0 To guyCount 'check guy
                    If (Not shoot_id = i) And (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) And guy(i).x(time) < x1 And guy(i).y(time) < y1 And x1 - guy(i).x(time) - guy_width < xDistance And y1 - guy(i).y(time) - 32 < yDistance And grad * (guy(i).x(time) - x1) + y1 < guy(i).y(time) + 32 And grad * (guy(i).x(time) + guy_width - x1) + y1 > guy(i).y(time) Then
                        If grad * (guy(i).x(time) + guy_width - x1) + y1 > guy(i).y(time) + 32 Then
                            Dim xInt = (guy(i).y(time) + 32 - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = x1 - xInt
                                yDistance = y1 - (guy(i).y(time) + 32)
                                x2 = xInt
                                y2 = y1 - yDistance
                                type = 2
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (guy(i).x(time) + guy_width - x1) + y1
                            If y1 - yInt < yDistance Then
                                xDistance = x1 - (guy(i).x(time) + guy_width)
                                yDistance = y1 - yInt
                                x2 = x1 - xDistance
                                y2 = yInt
                                type = 2
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 1 To boxCount 'check box
                    If (box(i).t_start <= time And (box(i).t_end >= time Or box(i).t_end = 0)) And box(i).carry(time) = False And box(i).y(time) < y1 And box(i).x(time) < x1 And x1 - box(i).x(time) - 32 < xDistance And y1 - box(i).y(time) - 32 < yDistance And grad * (box(i).x(time) - x1) + y1 < box(i).y(time) + 32 And grad * (box(i).x(time) + 32 - x1) + y1 > box(i).y(time) Then
                        If grad * (box(i).x(time) + 32 - x1) + y1 > box(i).y(time) + 32 Then
                            Dim xInt = (box(i).y(time) + 32 - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = x1 - xInt
                                yDistance = y1 - (box(i).y(time) + 32)
                                x2 = xInt
                                y2 = y1 - yDistance
                                type = 3
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (box(i).x(time) + 32 - x1) + y1
                            If y1 - yInt < yDistance Then
                                xDistance = x1 - (box(i).x(time) + 32)
                                yDistance = y1 - yInt
                                x2 = x1 - xDistance
                                y2 = yInt
                                type = 3
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 0 To platCount - 1 'check platform
                    If plat(i).y(time) < y1 And plat(i).x(time) < x1 And x1 - plat(i).x(time) - plat(i).width < xDistance And y1 - plat(i).y(time) - plat(i).height < yDistance And grad * (plat(i).x(time) - x1) + y1 < plat(i).y(time) + plat(i).height And grad * (plat(i).x(time) + plat(i).width - x1) + y1 > plat(i).y(time) Then
                        If grad * (plat(i).x(time) + plat(i).width - x1) + y1 > plat(i).y(time) + plat(i).height Then
                            Dim xInt = (plat(i).y(time) + plat(i).height - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = x1 - xInt
                                yDistance = y1 - (plat(i).y(time) + plat(i).height)
                                x2 = xInt
                                y2 = y1 - yDistance
                                type = 1
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (plat(i).x(time) + plat(i).width - x1) + y1
                            If y1 - yInt < yDistance Then
                                xDistance = x1 - (plat(i).x(time) + plat(i).width)
                                yDistance = y1 - yInt
                                x2 = x1 - xDistance
                                y2 = yInt
                                type = 1
                                id = i
                            End If
                        End If
                    End If
                Next

                Dim xp = Math.Floor(x1 / 32)
                Dim yp = Math.Floor(y1 / 32)
                Dim x, y
                If grad < 1 Then 'check wall
                    For i = x1 To x1 - xDistance Step -3 ' check wall with x eqn
                        x = Math.Floor(i / 32)
                        y = Math.Floor((grad * (i - x1) + y1) / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = i
                            y2 = grad * (i - x1) + y1
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y + 1) And wall(x + 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                Else
                    For i = y1 To y1 - yDistance Step -3 ' check wall with y eqn
                        x = Math.Floor(((i - y1) / grad + x1) / 32)
                        y = Math.Floor(i / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = (i - y1) / grad + x1
                            y2 = i
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y + 1) And wall(x + 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                End If

            Else 'south west quadrant

                For i = 0 To guyCount 'check guy
                    If (Not shoot_id = i) And (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) And guy(i).x(time) < x1 And guy(i).y(time) + 32 > y1 And x1 - guy(i).x(time) - guy_width < xDistance And guy(i).y(time) - y1 < yDistance And grad * (guy(i).x(time) - x1) + y1 > guy(i).y(time) And grad * (guy(i).x(time) + guy_width - x1) + y1 < guy(i).y(time) + 32 Then
                        If grad * (guy(i).x(time) + guy_width - x1) + y1 < guy(i).y(time) Then
                            Dim xInt = (guy(i).y(time) - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = x1 - xInt
                                yDistance = guy(i).y(time) - y1
                                x2 = xInt
                                y2 = y1 + yDistance
                                type = 2
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (guy(i).x(time) + guy_width - x1) + y1
                            If yInt - y1 < yDistance Then
                                xDistance = x1 - (guy(i).x(time) + guy_width)
                                yDistance = yInt - y1
                                x2 = x1 - xDistance
                                y2 = yInt
                                type = 2
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 1 To boxCount 'check box
                    If (box(i).t_start <= time And (box(i).t_end >= time Or box(i).t_end = 0)) And box(i).carry(time) = False And box(i).y(time) + 32 > y1 And box(i).x(time) < x1 And x1 - box(i).x(time) - 32 < xDistance And box(i).y(time) - y1 < yDistance And grad * (box(i).x(time) - x1) + y1 > box(i).y(time) And grad * (box(i).x(time) + 32 - x1) + y1 < box(i).y(time) + 32 Then
                        If grad * (box(i).x(time) + 32 - x1) + y1 < box(i).y(time) Then
                            Dim xInt = (box(i).y(time) - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = x1 - xInt
                                yDistance = box(i).y(time) - y1
                                x2 = xInt
                                y2 = y1 + yDistance
                                type = 3
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (box(i).x(time) + 32 - x1) + y1
                            If yInt - y1 < yDistance Then
                                xDistance = x1 - (box(i).x(time) + 32)
                                yDistance = yInt - y1
                                x2 = x1 - xDistance
                                y2 = yInt
                                type = 3
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 0 To platCount - 1 'check platform
                    If plat(i).y(time) + plat(i).height > y1 And plat(i).x(time) < x1 And x1 - plat(i).x(time) - plat(i).width < xDistance And plat(i).y(time) - y1 < yDistance And grad * (plat(i).x(time) - x1) + y1 > plat(i).y(time) And grad * (plat(i).x(time) + plat(i).width - x1) + y1 < plat(i).y(time) + plat(i).height Then
                        If grad * (plat(i).x(time) + plat(i).width - x1) + y1 < plat(i).y(time) Then
                            Dim xInt = (plat(i).y(time) - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = x1 - xInt
                                yDistance = plat(i).y(time) - y1
                                x2 = xInt
                                y2 = y1 + yDistance
                                type = 1
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (plat(i).x(time) + plat(i).width - x1) + y1
                            If yInt - y1 < yDistance Then
                                xDistance = x1 - (plat(i).x(time) + plat(i).width)
                                yDistance = yInt - y1
                                x2 = x1 - xDistance
                                y2 = yInt
                                type = 1
                                id = i
                            End If
                        End If
                    End If
                Next

                Dim xp = Math.Floor(x1 / 32)
                Dim yp = Math.Floor(y1 / 32)
                Dim x, y
                If grad > -1 Then 'check wall
                    For i = x1 To x1 - xDistance Step -3 ' check wall with x eqn
                        x = Math.Floor(i / 32)
                        y = Math.Floor((grad * (i - x1) + y1) / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = i
                            y2 = grad * (i - x1) + y1
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y - 1) And wall(x + 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                Else
                    For i = y1 To y1 + yDistance Step 3 ' check wall with y eqn
                        x = Math.Floor(((i - y1) / grad + x1) / 32)
                        y = Math.Floor(i / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = (i - y1) / grad + x1
                            y2 = i
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y - 1) And wall(x + 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                End If

            End If
        Else
            If y1 > y2 Then 'north east quadrant

                For i = 0 To guyCount 'check guy
                    If (Not shoot_id = i) And (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) And guy(i).x(time) + guy_width > x1 And guy(i).y(time) < y1 And guy(i).x(time) - x1 < xDistance And y1 - guy(i).y(time) - 32 < yDistance And grad * (guy(i).x(time) + guy_width - x1) + y1 < guy(i).y(time) + 32 And grad * (guy(i).x(time) - x1) + y1 > guy(i).y(time) Then
                        If grad * (guy(i).x(time) - x1) + y1 > guy(i).y(time) + 32 Then
                            Dim xInt = (guy(i).y(time) + 32 - y1) / grad + x1
                            If xInt - x1 < xDistance Then
                                xDistance = xInt - x1
                                yDistance = y1 - (guy(i).y(time) + 32)
                                x2 = xInt
                                y2 = y1 - yDistance
                                type = 2
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (guy(i).x(time) - x1) + y1
                            If y1 - yInt < yDistance Then
                                xDistance = guy(i).x(time) - x1
                                yDistance = y1 - yInt
                                x2 = x1 + xDistance
                                y2 = yInt
                                type = 2
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 1 To boxCount 'check box
                    If (box(i).t_start <= time And (box(i).t_end >= time Or box(i).t_end = 0)) And box(i).carry(time) = False And box(i).y(time) < y1 And box(i).x(time) + 32 > x1 And box(i).x(time) - x1 < xDistance And y1 - box(i).y(time) - 32 < yDistance And grad * (box(i).x(time) + 32 - x1) + y1 < box(i).y(time) + 32 And grad * (box(i).x(time) - x1) + y1 > box(i).y(time) Then
                        If grad * (box(i).x(time) - x1) + y1 > box(i).y(time) + 32 Then
                            Dim xInt = (box(i).y(time) + 32 - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = xInt - x1
                                yDistance = y1 - (box(i).y(time) + 32)
                                x2 = xInt
                                y2 = y1 - yDistance
                                type = 3
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (box(i).x(time) - x1) + y1
                            If y1 - yInt < yDistance Then
                                xDistance = box(i).x(time) - x1
                                yDistance = y1 - yInt
                                x2 = x1 + xDistance
                                y2 = yInt
                                type = 3
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 0 To platCount - 1 'check platform
                    If plat(i).y(time) < y1 And plat(i).x(time) + plat(i).width > x1 And plat(i).x(time) - x1 < xDistance And y1 - plat(i).y(time) - plat(i).height < yDistance And grad * (plat(i).x(time) + plat(i).width - x1) + y1 < plat(i).y(time) + plat(i).height And grad * (plat(i).x(time) - x1) + y1 > plat(i).y(time) Then
                        If grad * (plat(i).x(time) - x1) + y1 > plat(i).y(time) + plat(i).height Then
                            Dim xInt = (plat(i).y(time) + plat(i).height - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = xInt - x1
                                yDistance = y1 - (plat(i).y(time) + plat(i).height)
                                x2 = xInt
                                y2 = y1 - yDistance
                                type = 1
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (plat(i).x(time) - x1) + y1
                            If y1 - yInt < yDistance Then
                                xDistance = plat(i).x(time) - x1
                                yDistance = y1 - yInt
                                x2 = x1 + xDistance
                                y2 = yInt
                                type = 1
                                id = i
                            End If
                        End If
                    End If
                Next

                Dim xp = Math.Floor(x1 / 32)
                Dim yp = Math.Floor(y1 / 32)
                Dim x, y
                If grad > -1 Then 'check wall
                    For i = x1 To x1 + xDistance Step 3 ' check wall with x eqn
                        x = Math.Floor(i / 32)
                        y = Math.Floor((grad * (i - x1) + y1) / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = i
                            y2 = grad * (i - x1) + y1
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y + 1) And wall(x - 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                Else
                    For i = y1 To y1 - yDistance Step -3 ' check wall with y eqn
                        x = Math.Floor(((i - y1) / grad + x1) / 32)
                        y = Math.Floor(i / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = (i - y1) / grad + x1
                            y2 = i
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y + 1) And wall(x + 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                End If

            Else 'south east quadrant

                For i = 0 To guyCount 'check guy
                    If (Not shoot_id = i) And (guy(i).exist(time) Or (playerGuy = i And fastMode = False)) And guy(i).x(time) + guy_width > x1 And guy(i).y(time) + 32 > y1 And guy(i).x(time) - x1 < xDistance And guy(i).y(time) - y1 < yDistance And grad * (guy(i).x(time) + guy_width - x1) + y1 > guy(i).y(time) And grad * (guy(i).x(time) - x1) + y1 < guy(i).y(time) + 32 Then
                        If grad * (guy(i).x(time) - x1) + y1 < guy(i).y(time) Then
                            Dim xInt = (guy(i).y(time) - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = xInt - x1
                                yDistance = guy(i).y(time) - y1
                                x2 = xInt
                                y2 = y1 + yDistance
                                type = 2
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (guy(i).x(time) - x1) + y1
                            If yInt - y1 < yDistance Then
                                xDistance = guy(i).x(time) - x1
                                yDistance = yInt - y1
                                x2 = x1 + xDistance
                                y2 = yInt
                                type = 2
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 1 To boxCount 'check box
                    If (box(i).t_start <= time And (box(i).t_end >= time Or box(i).t_end = 0)) And box(i).carry(time) = False And box(i).y(time) + 32 > y1 And box(i).x(time) + 32 > x1 And box(i).x(time) - x1 < xDistance And box(i).y(time) - y1 < yDistance And grad * (box(i).x(time) + 32 - x1) + y1 > box(i).y(time) And grad * (box(i).x(time) - x1) + y1 < box(i).y(time) + 32 Then
                        If grad * (box(i).x(time) - x1) + y1 < box(i).y(time) Then
                            Dim xInt = (box(i).y(time) - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = xInt - x1
                                yDistance = box(i).y(time) - y1
                                x2 = xInt
                                y2 = y1 + yDistance
                                type = 3
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (box(i).x(time) - x1) + y1
                            If yInt - y1 < yDistance Then
                                xDistance = box(i).x(time) - x1
                                yDistance = yInt - y1
                                x2 = x1 + xDistance
                                y2 = yInt
                                type = 3
                                id = i
                            End If
                        End If
                    End If
                Next

                For i = 0 To platCount - 1 'check platform
                    If plat(i).y(time) + plat(i).height > y1 And plat(i).x(time) + plat(i).width > x1 And plat(i).x(time) - x1 < xDistance And plat(i).y(time) - y1 < yDistance And grad * (plat(i).x(time) + plat(i).width - x1) + y1 > plat(i).y(time) And grad * (plat(i).x(time) - x1) + y1 < plat(i).y(time) + plat(i).height Then
                        If grad * (plat(i).x(time) - x1) + y1 < plat(i).y(time) Then
                            Dim xInt = (plat(i).y(time) - y1) / grad + x1
                            If x1 - xInt < xDistance Then
                                xDistance = xInt - x1
                                yDistance = plat(i).y(time) - y1
                                x2 = xInt
                                y2 = y1 + yDistance
                                type = 1
                                id = i
                            End If
                        Else
                            Dim yInt = grad * (plat(i).x(time) - x1) + y1
                            If yInt - y1 < yDistance Then
                                xDistance = plat(i).x(time) - x1
                                yDistance = yInt - y1
                                x2 = x1 + xDistance
                                y2 = yInt
                                type = 1
                                id = i
                            End If
                        End If
                    End If
                Next

                Dim xp = Math.Floor(x1 / 32)
                Dim yp = Math.Floor(y1 / 32)
                Dim x, y
                If grad < 1 Then 'check wall
                    For i = x1 To x1 + xDistance Step 3 ' check wall with x eqn
                        x = Math.Floor(i / 32)
                        y = Math.Floor((grad * (i - x1) + y1) / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = i
                            y2 = grad * (i - x1) + y1
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y - 1) And wall(x - 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                Else
                    For i = y1 To y1 + yDistance Step 3 ' check wall with y eqn
                        x = Math.Floor(((i - y1) / grad + x1) / 32)
                        y = Math.Floor(i / 32)
                        If x > 31 Or x < 0 Or y > 21 Or y < 0 Then
                            Exit For
                        End If
                        If wall(x, y) Then
                            type = 0
                            id = 0
                            x2 = (i - y1) / grad + x1
                            y2 = i
                            Exit For
                        End If
                        If Not x = xp And Not y = yp Then
                            If wall(x, y - 1) And wall(x - 1, y) Then
                                type = 0
                                id = 0
                                x2 = i
                                y2 = grad * (i - x1) + y1
                                Exit For
                            End If
                        End If
                        xp = x
                        yp = y
                    Next
                End If

            End If
        End If

        return1 = id
        return2 = x2
        return3 = y2
        Return type
    End Function

    Private Sub CopyGuy(ByVal from As Integer, ByVal id As Integer)

        guy(id).t_start = guy(from).t_start 'creation time
        guy(id).t_end = guy(from).t_end 'end time
        guy(id).t_end_specific = guy(from).t_end_specific 'is the chronoport time dependant, for paradoxes
        guy(id).t_end_type = guy(from).t_end_type ' 1 = portal whenever, 2 = portal specific, 3 = time jump, 4 = shot, 5 = time reverse
        guy(id).t_end_portal_id = guy(from).t_end_portal_id ' id of portal if end type has to do with portals

        guy(id).t_direction = guy(from).t_direction 'Direction that this instance travels in time, true for forward, false for backwards
        guy(id).subimage = guy(from).subimage 'frame of animation
        guy(id).face = guy(from).face ' direction (left or right)

        Dim i

        For i = 0 To 40
            guy(id).pickup_time(i) = guy(from).pickup_time(i)
        Next

        For i = 0 To maxTime
            guy(id).x(i) = guy(from).x(i) 'X position
            guy(id).y(i) = guy(from).y(i) 'Y position
            guy(id).xspeed(i) = guy(from).xspeed(i) ' xspeed
            guy(id).yspeed(i) = guy(from).yspeed(i) ' yspeed

            'Storing data for paradox checking
            guy(id).x_par(i) = guy(from).x_par(i) 'x at a paradox time, 0 means do not check for paradox
            guy(id).y_par(i) = guy(from).y_par(i) 'y at a paradox time
            guy(id).xspeed_par(i) = guy(from).xspeed_par(i) 'xspeed at a paradox time
            guy(id).yspeed_par(i) = guy(from).yspeed_par(i) 'yspeed at a paradox time
            guy(id).carry_par(i) = guy(from).carry_par(i) 'whether carring and ID at a paradox time
            guy(id).pickupCheck(i) = guy(from).pickupCheck(i) 'whether to check pickup proximity and which type
            guy(id).pickup_id(i) = guy(from).pickup_id(i)

            'store data for support paradox checking
            guy(id).y_par_s(i) = guy(from).y_par_s(i)
            guy(id).x_par_s(i) = guy(from).x_par_s(i)
            guy(id).xspeed_par_s(i) = guy(from).xspeed_par_s(i)
            guy(id).yspeed_par_s(i) = guy(from).yspeed_par_s(i)
            guy(id).dir_par_s(i) = guy(from).dir_par_s(i)
            guy(id).object_par_s(i) = guy(from).object_par_s(i) ' 1 = platform, 2 = box

            'Carrying box
            guy(id).carry(i) = guy(from).carry(i) ' carry ID of box 

            'Start/end time and existance. Time Tracking

            guy(id).exist(i) = guy(from).exist(i) 'stores whether the instance exists at a time


            'Graphics
            guy(id).face_store(i) = guy(from).face_store(i) ' direction (left or right) store
            guy(id).laser_x_draw(i) = guy(from).laser_x_draw(i) 'x position to draw laser to 
            guy(id).laser_y_draw(i) = guy(from).laser_y_draw(i) 'y position to draw laser to 

            'paradox checking for laser
            guy(id).laser_x(i) = guy(from).laser_x(i) 'x aimed at
            guy(id).laser_y(i) = guy(from).laser_y(i) 'y aimed at
            guy(id).laser_objx(i) = guy(from).laser_objx(i) 'x position of object shot
            guy(id).laser_objy(i) = guy(from).laser_objy(i) 'y position of object shot
            guy(id).laser_id(i) = guy(from).laser_id(i) 'ID of object shot
            guy(id).laser_type(i) = guy(from).laser_type(i) 'Type of object shot
            guy(id).laser_time(i) = guy(from).laser_time(i) 'Time that the laser w= guy(from).set to send objects to

            'Input
            guy(id).k_left(i) = guy(from).k_left(i) 'A key pressed
            guy(id).k_right(i) = guy(from).k_right(i) 'D key pressed
            guy(id).k_up(i) = guy(from).k_up(i) 'W key pressed
            guy(id).k_down(i) = guy(from).k_down(i) 'S key pressed

            'Is it touching floor or box?
            guy(id).supported_obj(i) = guy(from).supported_obj(i) ' 1 = platform, 2 = box
            guy(id).supported_id(i) = guy(from).supported_id(i)
        Next

    End Sub

    Private Sub CleanGuy(ByVal id As Integer)

        Dim i As Integer

        guy(id).t_start = 1
        guy(id).t_end = 0
        guy(id).t_end_specific = False
        guy(id).t_end_type = 0

        guy(id).t_direction = True

        For i = 0 To maxTime + 50
            guy(id).carry(i) = False
            guy(id).exist(i) = False
            guy(id).k_left(i) = False
            guy(id).k_right(i) = False
            guy(id).k_up(i) = False
            guy(id).k_down(i) = False
            guy(id).pickupCheck(i) = 0
            guy(id).laser_x(i) = 0
            guy(id).x_par(i) = 0
            guy(id).object_par_s(i) = 0
            guy(id).supported_obj(i) = 0
            guy(id).laser_x_draw(i) = 0
            guy(id).laser_y_draw(i) = 0
            guy(id).playSound(i) = ""
        Next

    End Sub

    Private Sub CleanGuyBounds(ByVal id As Integer, ByVal min As Integer, ByVal max As Integer)

        Dim i As Integer

        For i = min To max
            guy(id).carry(i) = False
            guy(id).exist(i) = False
            guy(id).k_left(i) = False
            guy(id).k_right(i) = False
            guy(id).k_up(i) = False
            guy(id).k_down(i) = False
            guy(id).pickupCheck(i) = 0
            guy(id).laser_x(i) = 0
            guy(id).x_par(i) = 0
            guy(id).object_par_s(i) = 0
            guy(id).supported_obj(i) = 0
            guy(id).laser_x_draw(i) = 0
            guy(id).laser_y_draw(i) = 0
            guy(id).playSound(i) = ""
        Next

    End Sub

    Private Sub ReDimGuy(ByVal id As Integer)

        guy(id).t_direction = True

        ReDim guy(id).x(maxTime + 50)
        ReDim guy(id).y(maxTime + 50)
        ReDim guy(id).xspeed(maxTime + 50)
        ReDim guy(id).yspeed(maxTime + 50)
        ReDim guy(id).carry(maxTime + 50)
        ReDim guy(id).exist(maxTime + 50)

        ReDim guy(id).k_left(maxTime + 50)
        ReDim guy(id).k_right(maxTime + 50)
        ReDim guy(id).k_up(maxTime + 50)
        ReDim guy(id).k_down(maxTime + 50)

        ReDim guy(id).pickupCheck(maxTime + 50)
        ReDim guy(id).pickup_id(maxTime + 50)
        ReDim guy(id).pickup_time(40)

        ReDim guy(id).laser_x(maxTime + 50)
        ReDim guy(id).laser_y(maxTime + 50)
        ReDim guy(id).laser_objx(maxTime + 50)
        ReDim guy(id).laser_objy(maxTime + 50)
        ReDim guy(id).laser_id(maxTime + 50)
        ReDim guy(id).laser_type(maxTime + 50)
        ReDim guy(id).laser_time(maxTime + 50)

        ReDim guy(id).x_par(maxTime + 50)
        ReDim guy(id).y_par(maxTime + 50)
        ReDim guy(id).xspeed_par(maxTime + 50)
        ReDim guy(id).yspeed_par(maxTime + 50)
        ReDim guy(id).carry_par(maxTime + 50)

        ReDim guy(id).y_par_s(maxTime + 50)
        ReDim guy(id).x_par_s(maxTime + 50)
        ReDim guy(id).xspeed_par_s(maxTime + 50)
        ReDim guy(id).yspeed_par_s(maxTime + 50)
        ReDim guy(id).object_par_s(maxTime + 50)
        ReDim guy(id).dir_par_s(maxTime + 50)

        ReDim guy(id).laser_x_draw(maxTime + 50)
        ReDim guy(id).laser_y_draw(maxTime + 50)
        ReDim guy(id).face_store(maxTime + 50)

        ReDim guy(id).supported_obj(maxTime + 50)
        ReDim guy(id).supported_id(maxTime + 50)

        ReDim guy(id).playSound(maxTime + 50)

        CleanGuy(id)

    End Sub

    Private Sub ReDimBox(ByVal id As Integer)

        ReDim box(id).x(maxTime + 50)
        ReDim box(id).y(maxTime + 50)
        ReDim box(id).xspeed(maxTime + 50)
        ReDim box(id).yspeed(maxTime + 50)
        ReDim box(id).exist(maxTime + 50)
        ReDim box(id).carry(maxTime + 50)

        ReDim box(id).playSound(maxTime + 50)
        ReDim box(id).supported_obj(maxTime + 50)
        ReDim box(id).supported_id(maxTime + 50)

    End Sub

    Private Sub ReDimPlat(ByVal id As Integer)

        ReDim plat(id).x(maxTime + 50)
        ReDim plat(id).y(maxTime + 50)
        ReDim plat(id).xspeed(maxTime + 50)
        ReDim plat(id).yspeed(maxTime + 50)

    End Sub

    Private Sub ReDimSwitch(ByVal id As Integer)

        ReDim switch(id).t_state(maxTime + 50)
        If switch(id).type = 2 Then
            ReDim switch(id).t_state(maxTime + 50)
            ReDim switch(id).laser_x(maxTime + 50)
            ReDim switch(id).laser_y(maxTime + 50)
        End If
        ReDim switch(id).playSound(maxTime + 50)

    End Sub

    Private Sub LoadImageOTF(ByVal name As String, ByVal transparent As Boolean) ' on the fly
        Dim j As Integer

        If Not name = "" Then
            For j = 0 To loadedImages - 1
                If (loadedImage(j) = name) Then
                    Exit Sub
                End If
            Next
            loadedImage(loadedImages) = name
            loadedImages += 1
            If transparent Then
                NewTransparentColorImage(name, name & ".png", Color.Magenta)
            Else
                NewImage(name, name & ".png")
            End If
        End If

    End Sub

    Private Sub LoadSoundOTF(ByVal name As String) ' on the fly
        Dim j As Integer

        If Not name = "" Then
            For j = 0 To loadedSounds - 1
                If (loadedSound(j) = name) Then
                    Exit Sub
                End If
            Next
            loadedSound(loadedSounds) = name
            loadedSounds += 1

            NewSound(name, name & ".wav")
        End If

    End Sub

    Private Sub LoadLevel(ByVal fileToLoad As String)
        'loads a level from a text file
        Dim i, j As Integer
        Dim oRead As System.IO.StreamReader
        oRead = File.OpenText(fileToLoad)
        'opens file for loading
        For i = 0 To 31
            For j = 0 To 21
                wall(i, j) = oRead.ReadLine()
            Next
        Next
        oRead.ReadLine()

        levelName = oRead.ReadLine()
        backgroundImage = oRead.ReadLine()
        foregroundImage = oRead.ReadLine()

        LoadImageOTF(backgroundImage, False)
        LoadImageOTF(foregroundImage, True)

        oRead.ReadLine()

        ReDimGuy(0)
        playerGuy = 0
        guyCount = 0
        guy(0).x(0) = oRead.ReadLine()
        guy(0).y(0) = oRead.ReadLine()
        guy(0).xspeed(0) = 0
        guy(0).yspeed(0) = 0
        guy(0).exist(0) = True
        guy(0).carry(0) = 0
        guy(0).t_start = 1
        guy(0).t_end = 0
        guy(0).subimage = 0
        guy(0).face = False
        guy(0).k_left(0) = False
        guy(0).k_right(0) = False
        guy(0).k_up(0) = False
        guy(0).k_down(0) = False
        oRead.ReadLine()

        platCount = oRead.ReadLine()
        For i = 0 To platCount - 1
            ReDimPlat(i)
            plat(i).x(0) = oRead.ReadLine()
            plat(i).y(0) = oRead.ReadLine()
            plat(i).width = oRead.ReadLine()
            plat(i).height = oRead.ReadLine()
            plat(i).image = oRead.ReadLine()
            plat(i).switch_id = oRead.ReadLine()
            If plat(i).switch_id < 1000 Then
                plat(i).switch_type = True
            Else
                plat(i).switch_type = False
                plat(i).switch_id = plat(i).switch_id - 1000
            End If
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
            LoadImageOTF(plat(i).image, True)
        Next
        oRead.ReadLine()

        endPortal.x = oRead.ReadLine()
        endPortal.y = oRead.ReadLine()
        endPortal.attach = oRead.ReadLine()
        If Not endPortal.attach = -1 Then
            endPortal.xoff = endPortal.x - plat(endPortal.attach).x(0)
            endPortal.yoff = endPortal.y - plat(endPortal.attach).y(0)
        End If
        oRead.ReadLine()

        boxCount = oRead.ReadLine()
        For i = 1 To boxCount
            ReDimBox(i)
            box(i).x(0) = oRead.ReadLine()
            box(i).y(0) = oRead.ReadLine()
            box(i).xspeed(0) = 0
            box(i).yspeed(0) = 0
            box(i).exist(0) = True
            box(i).carry(0) = False
            box(i).start_gfx = False
            box(i).t_start = 1
            box(i).t_end = 0
            box(i).t_end_type = 0
            For j = 0 To maxTime + 50
                box(i).playSound(j) = ""
            Next
        Next
        oRead.ReadLine()

        portalCount = oRead.ReadLine()
        For i = 0 To portalCount - 1
            portal(i).x = oRead.ReadLine()
            portal(i).y = oRead.ReadLine()
            portal(i).effect = oRead.ReadLine()
            portal(i).type = oRead.ReadLine()
            portal(i).charges = oRead.ReadLine()
            If portal(i).charges = 0 Then
                portal(i).charges = -1
            End If
            portal(i).max_charges = portal(i).charges
            portal(i).attach = oRead.ReadLine()
            If Not portal(i).attach = -1 Then
                portal(i).xoff = portal(i).x - plat(portal(i).attach).x(0)
                portal(i).yoff = portal(i).y - plat(portal(i).attach).y(0)
            End If
        Next
        oRead.ReadLine()

        switchCount = oRead.ReadLine()
        For i = 0 To switchCount - 1
            switch(i).x = oRead.ReadLine()
            switch(i).y = oRead.ReadLine()
            switch(i).visible = oRead.ReadLine()
            switch(i).state = False
            switch(i).attach = oRead.ReadLine()
            switch(i).rotation = oRead.ReadLine()
            switch(i).type = oRead.ReadLine()
            If switch(i).type = 0 Then
                If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                    switch(i).width = 32
                    switch(i).height = 7
                Else
                    switch(i).width = 7
                    switch(i).height = 32
                End If
                If Not switch(i).attach = -1 Then
                    switch(i).xoff = switch(i).x - plat(switch(i).attach).x(0)
                    switch(i).yoff = switch(i).y - plat(switch(i).attach).y(0)
                End If
            ElseIf switch(i).type = 1 Then
                switch(i).x2 = oRead.ReadLine()
                switch(i).y2 = oRead.ReadLine()
                switch(i).attach2 = oRead.ReadLine()
                switch(i).rotation2 = oRead.ReadLine()
                If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                    switch(i).width = 16
                    switch(i).height = 10
                Else
                    switch(i).width = 10
                    switch(i).height = 16
                End If
                If Not switch(i).attach = -1 Then
                    switch(i).xoff = switch(i).x - plat(switch(i).attach).x(0)
                    switch(i).yoff = switch(i).y - plat(switch(i).attach).y(0)
                End If
                If Not switch(i).attach2 = -1 Then
                    switch(i).xoff2 = switch(i).x2 - plat(switch(i).attach2).x(0)
                    switch(i).yoff2 = switch(i).y2 - plat(switch(i).attach2).y(0)
                End If
            ElseIf switch(i).type = 2 Then
                If switch(i).rotation = 0 Or switch(i).rotation = 2 Then
                    switch(i).width = 16
                    switch(i).height = 10
                Else
                    switch(i).width = 10
                    switch(i).height = 16
                End If
                If Not switch(i).attach = -1 Then
                    switch(i).xoff = switch(i).x - plat(switch(i).attach).x(0)
                    switch(i).yoff = switch(i).y - plat(switch(i).attach).y(0)
                End If
            End If
            ReDimSwitch(i)
            switch(i).hit_guy = oRead.ReadLine()
            switch(i).hit_box = oRead.ReadLine()
            switch(i).hit_plat = oRead.ReadLine()
            switch(i).hit_wall = oRead.ReadLine()
            For j = 0 To maxTime + 50
                switch(i).playSound(j) = ""
            Next
        Next
        oRead.ReadLine()

        pickupCount = oRead.ReadLine()
        For i = 0 To pickupCount - 1
            pickup(i).x = oRead.ReadLine()
            pickup(i).y = oRead.ReadLine()
            pickup(i).type = oRead.ReadLine()
            pickup(i).type2 = oRead.ReadLine()
            pickup(i).attach = oRead.ReadLine()
            pickup(i).t_end = 0
            pickup(i).width = 16
            pickup(i).height = 16
            If Not pickup(i).attach = -1 Then
                pickup(i).xoff = pickup(i).x - plat(pickup(i).attach).x(0)
                pickup(i).yoff = pickup(i).y - plat(pickup(i).attach).y(0)
            End If
        Next
        oRead.ReadLine()

        spikeCount = oRead.ReadLine()
        For i = 0 To spikeCount - 1
            spike(i).x = oRead.ReadLine()
            spike(i).y = oRead.ReadLine()
            spike(i).rotation = oRead.ReadLine()
            spike(i).size = oRead.ReadLine()
            spike(i).attach = oRead.ReadLine()
            If Not spike(i).attach = -1 Then
                spike(i).xoff = spike(i).x - plat(spike(i).attach).x(0)
                spike(i).yoff = spike(i).y - plat(spike(i).attach).y(0)
            End If
            If spike(i).rotation = 0 Or spike(i).rotation = 2 Then
                spike(i).width = spike(i).size
                spike(i).height = 16
            Else
                spike(i).width = 16
                spike(i).height = spike(i).size
            End If
        Next
        oRead.ReadLine()

        gatecount = oRead.ReadLine()
        For i = 0 To gatecount - 1
            oRead.ReadLine()
            oRead.ReadLine()
            gate(i).type = oRead.ReadLine()
            gate(i).attach1 = oRead.ReadLine()
            gate(i).attach2 = oRead.ReadLine()
        Next
        oRead.Close()

        oRead.Close()
        'resets game information for the new level
        playerGuyStore = -1
        time = 1
        time_speed = 1
        time_aim = 0
        timeJumps = 0
        timeGuns = 0
        timeReverses = 0
        fastMode = False
        winCheck = False
        maxCheckTime = 50
        won = False
        restartLevel = False

        pauseGame = False
        manualPauseGame = False
    End Sub

End Module


