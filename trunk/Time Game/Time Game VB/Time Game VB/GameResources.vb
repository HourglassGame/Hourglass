Imports SwinGame
Imports System.Collections.Generic

'Imports System
'Imports System.Windows.Forms
'Imports System.ComponentModel
'Imports System.Drawing.Imaging
'Imports System.Drawing

Public Module GameResources

    Private Sub LoadFonts()
        'NewFont("ArialLarge", "arial.ttf", 80)
        'NewFont("Courier", "cour.ttf", 16)
        'NewFont("Courier20", "cour.ttf", 20)

        NewFont("Large", "script.ttf", 80)
        NewFont("Medium", "script.ttf", 26)
        NewFont("Interface", "script.ttf", 36)
        NewFont("Small", "script.ttf", 20)
    End Sub


    Private Sub LoadImages()

        Dim transColor As Color = Core.GetColor(50, 100, 150)

        NewTransparentColorImage("guy_left", "rhino_left.png", Color.White)
        NewTransparentColorImage("guy_right", "rhino_right.png", Color.White)
        NewTransparentColorImage("guy_left_stop", "rhino_left_stop.png", Color.White)
        NewTransparentColorImage("guy_right_stop", "rhino_right_stop.png", Color.White)
        NewTransparentColorImage("portal_effect", "portal_effect.png", Color.White)
        NewTransparentColorImage("arrow", "arrow.png", Color.White)
        NewImage("box", "box.png")
        NewTransparentColorImage("box_die", "box_die.png", Color.Black)
        NewTransparentColorImage("paradox_guy", "paradox_guy.png", Color.White)
        NewTransparentColorImage("paradox_box", "paradox_box.png", Color.White)
        NewTransparentColorImage("paradox_pickup", "paradox_pickup.png", Color.White)
        'push buton
        NewTransparentColorImage("button0_up", "button0_up.png", Color.White)
        NewTransparentColorImage("button0_down", "button0_down.png", Color.White)
        NewTransparentColorImage("button1_up", "button1_up.png", Color.White)
        NewTransparentColorImage("button1_down", "button1_down.png", Color.White)
        NewTransparentColorImage("button2_up", "button2_up.png", Color.White)
        NewTransparentColorImage("button2_down", "button2_down.png", Color.White)
        NewTransparentColorImage("button3_up", "button3_up.png", Color.White)
        NewTransparentColorImage("button3_down", "button3_down.png", Color.White)
        'toggle buttons
        NewTransparentColorImage("toggle0_up", "toggle0_up.png", Color.White)
        NewTransparentColorImage("toggle1_up", "toggle1_up.png", Color.White)
        NewTransparentColorImage("toggle2_up", "toggle2_up.png", Color.White)
        NewTransparentColorImage("toggle3_up", "toggle3_up.png", Color.White)
        NewTransparentColorImage("toggle0_down", "toggle0_down.png", Color.White)
        NewTransparentColorImage("toggle1_down", "toggle1_down.png", Color.White)
        NewTransparentColorImage("toggle2_down", "toggle2_down.png", Color.White)
        NewTransparentColorImage("toggle3_down", "toggle3_down.png", Color.White)
        'spikes
        NewTransparentColorImage("spike0", "spike0.png", Color.White)
        NewTransparentColorImage("spike1", "spike1.png", Color.White)
        NewTransparentColorImage("spike2", "spike2.png", Color.White)
        NewTransparentColorImage("spike3", "spike3.png", Color.White)
        'laser switch
        NewTransparentColorImage("laserswitch0", "laserswitch0.png", Color.White)
        NewTransparentColorImage("laserswitch1", "laserswitch1.png", Color.White)
        NewTransparentColorImage("laserswitch2", "laserswitch2.png", Color.White)
        NewTransparentColorImage("laserswitch3", "laserswitch3.png", Color.White)
        'pickups
        NewTransparentColorImage("time_jump", "time_jump.png", Color.White)
        NewTransparentColorImage("time_gun", "time_gun.png", Color.White)
        NewTransparentColorImage("reverse_time", "reverse_time.png", Color.Magenta)
        'interface
        NewImage("storyImage", "story.png")
        NewImage("creditsImage", "credits.png")
        NewImage("interfaceImageLeft", "interfaceLeft.png")
        NewImage("interfaceImageRight", "interfaceRight.png")
        NewImage("flip_numbers", "flip_numbers.png")
        NewImage("flip_numbers_big", "flip_numbers_big.png")
        'menu
        NewImage("menu_left", "menu_left.png")
        'NewImage("menu_animate", "menu_animate.png")
        NewImage("menunone", "menunone.png")
        NewImage("menustory", "menustory.png")
        NewImage("menucredits", "menucredits.png")
        NewImage("menuload", "menuload.png")
        NewImage("menuexit", "menuexit.png")
        'portals
        NewTransparentColorImage("portal_white", "portal_white.png", Color.Black)
        NewTransparentColorImage("portal_black", "portal_black.png", Color.White)
        NewTransparentColorImage("portal_end", "portal_end.png", Color.White)
        NewTransparentColorImage("portal_reverse", "portal_reverse.png", Color.Red)
        NewTransparentColorImage("portal_charge", "portal_charge.png", Color.White)
        NewTransparentColorImage("portal_charge_used", "portal_charge_used.png", Color.White)

    End Sub

    Private Sub LoadSounds()

        NewSound("switch_push_down", "switch_push_down.wav")
        NewSound("switch_push_down_rev", "switch_push_down_rev.wav")
        NewSound("switch_push_up", "switch_push_up.wav")
        NewSound("switch_push_up_rev", "switch_push_up_rev.wav")
        NewSound("switch_toggle", "switch_toggle.wav")
        NewSound("switch_toggle_rev", "switch_toggle_rev.wav")
        NewSound("switch_trip_laser", "switch_trip_laser.wav")
        NewSound("switch_trip_laser_rev", "switch_trip_laser_rev.wav")

        NewSound("box_pickup", "box_pickup.wav")
        NewSound("box_pickup_rev", "box_pickup_rev.wav")
        NewSound("box_hit", "box_hit.wav")
        NewSound("box_hit_rev", "box_hit_rev.wav")
        'NewSound("box_die", "box_die.wav")

        NewSound("portal_in", "portal_in.wav")
        NewSound("portal_in_rev", "portal_in_rev.wav")
        ' NewSound("portal_out", "portal_out.wav")

        NewSound("guy_hit", "rhino_land.wav")
        NewSound("guy_hit_rev", "rhino_land_rev.wav")

        NewSound("pickup_pickup", "pickup_pickup.wav")
        NewSound("pickup_pickup_rev", "pickup_pickup_rev.wav")
        NewSound("laser_shoot", "laser_shoot.wav")
        NewSound("laser_shoot_rev", "laser_shoot_rev.wav")

        'NON BACKWARDS
        NewSound("portal_sound", "portal_sound.wav")
        NewSound("time_jump_sound", "time_jump_sound.wav")
        NewSound("time_reverse_sound", "time_reverse_sound.wav")
        NewSound("squished", "squished.wav")
        NewSound("spiked", "spiked.wav")
        'NewSound("chronofraged", "chronofraged.wav")
        'NewSound("paradox", "paradox.wav")
        

        'NewSound("platform_start", "platform_start.wav")
        'NewSound("platform_move", "platform_move.wav")
        'NewSound("platform_end", "platform_end.wav")

    End Sub

    Private Sub LoadMusic()

    End Sub

    Private Sub LoadMaps()

    End Sub

#Region "Resource Management Code"
    ''' <summary>
    ''' Gets a Font Loaded in the Resources
    ''' </summary>
    ''' <param name="font">Name of Font</param>
    ''' <returns>The Font Loaded with this Name</returns>
    ''' <remarks></remarks>
    Public Function GameFont(ByVal font As String) As Font
        Return _Fonts(font.ToLower())
    End Function

    ''' <summary>
    ''' Gets an Image loaded in the Resources
    ''' </summary>
    ''' <param name="image">Name of image</param>
    ''' <returns>The image loaded with this name</returns>
    ''' <remarks></remarks>
    Public Function GameImage(ByVal image As String) As Bitmap
        Return _Images(image.ToLower())
    End Function

    ''' <summary>
    ''' Gets an sound loaded in the Resources
    ''' </summary>
    ''' <param name="sound">Name of sound</param>
    ''' <returns>The sound with this name</returns>
    ''' <remarks></remarks>
    Public Function GameSound(ByVal sound As String) As SoundEffect
        Return _Sounds(sound.ToLower())
    End Function

    ''' <summary>
    ''' Gets the music loaded in the Resources
    ''' </summary>
    ''' <param name="music">Name of music</param>
    ''' <returns>The music with this name</returns>
    ''' <remarks></remarks>
    Public Function GameMusic(ByVal music As String) As Music
        Return _Music(music.ToLower())
    End Function

    ''' <summary>
    ''' Gets a map loaded in the Resources
    ''' </summary>
    ''' <param name="map">Name of map</param>
    ''' <returns>The map with this name</returns>
    ''' <remarks></remarks>
    Public Function GameMap(ByVal map As String) As Map
        Return _Maps(map.ToLower())
    End Function

    Private _Images As New Dictionary(Of String, Bitmap)
    Private _Fonts As New Dictionary(Of String, Font)
    Private _Sounds As New Dictionary(Of String, SoundEffect)
    Private _Music As New Dictionary(Of String, Music)
    Private _Maps As New Dictionary(Of String, Map)

    Private _Background As Bitmap
    Private _Animation As Bitmap
    Private _LoaderFull As Bitmap
    Private _LoaderEmpty As Bitmap
    Private _LoadingFont As Font
    Private _StartSound As SoundEffect

    ''' <summary>
    ''' The Resources Class stores all of the Games Media Resources, such as Images, Fonts
    ''' Sounds, Music, and Maps.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadResources()
        Dim width, height As Integer

        width = Core.ScreenWidth()
        height = Core.ScreenHeight()

        Core.ChangeScreenSize(800, 600)

        ShowLoadingScreen()

        ShowMessage("Loading fonts...", 0)
        LoadFonts()

        ShowMessage("Loading images...", 1)
        LoadImages()

        ShowMessage("Loading sounds...", 2)
        LoadSounds()

        ShowMessage("Loading music...", 3)
        LoadMusic()

        ShowMessage("Loading maps...", 4)
        LoadMaps()

        ShowMessage("Game loaded...", 5)

        EndLoadingScreen(width, height)
    End Sub

    Private Sub ShowLoadingScreen()
        _Background = Graphics.LoadBitmap(Core.GetPathToResource("SplashBack.png", ResourceKind.ImageResource))
        Core.RefreshScreen()
        Core.ProcessEvents()

        _Animation = Graphics.LoadBitmap(Core.GetPathToResource("SwinGameAni.png", ResourceKind.ImageResource))
        _LoadingFont = Text.LoadFont(Core.GetPathToResource("arial.ttf", ResourceKind.FontResource), 12)
        _StartSound = Audio.LoadSoundEffect(Core.GetPathToResource("SwinGameStart.ogg", ResourceKind.SoundResource))

        _LoaderFull = Graphics.LoadBitmap(Core.GetPathToResource("loader_full.png", ResourceKind.ImageResource))
        _LoaderEmpty = Graphics.LoadBitmap(Core.GetPathToResource("loader_empty.png", ResourceKind.ImageResource))

    End Sub


    Private Sub ShowMessage(ByVal message As String, ByVal number As Integer)
        Const TX = 310, TY = 493, TW = 200, TH = 25, STEPS = 5, BG_X = 279, BG_Y = 453

        Dim fullW As Integer

        fullW = 260 * number \ STEPS
        Graphics.DrawBitmap(_LoaderEmpty, BG_X, BG_Y)
        Graphics.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y)

        Text.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH)

        Core.RefreshScreen()
        Core.ProcessEvents()
    End Sub

    Private Sub EndLoadingScreen(ByVal width As Integer, ByVal height As Integer)
        Core.ProcessEvents()
        Core.Sleep(500)
        Graphics.ClearScreen()
        Core.RefreshScreen()
        Text.FreeFont(_LoadingFont)
        Graphics.FreeBitmap(_Background)
        Graphics.FreeBitmap(_Animation)
        Graphics.FreeBitmap(_LoaderEmpty)
        Graphics.FreeBitmap(_LoaderFull)
        Audio.FreeSoundEffect(_StartSound)
        Core.ChangeScreenSize(width, height)
    End Sub

    Private Sub NewMap(ByVal mapName As String)
        _Maps.Add(mapName.ToLower(), MappyLoader.LoadMap(mapName))
    End Sub

    Private Sub NewFont(ByVal fontName As String, ByVal filename As String, ByVal size As Integer)
        _Fonts.Add(fontName.ToLower(), Text.LoadFont(Core.GetPathToResource(filename, ResourceKind.FontResource), size))
    End Sub

    Public Sub NewImage(ByVal imageName As String, ByVal filename As String)
        _Images.Add(imageName.ToLower(), Graphics.LoadBitmap(Core.GetPathToResource(filename, ResourceKind.ImageResource)))
    End Sub

    Public Sub NewTransparentColorImage(ByVal imageName As String, ByVal fileName As String, ByVal transColor As Color)
        _Images.Add(imageName.ToLower(), Graphics.LoadBitmap(Core.GetPathToResource(fileName, ResourceKind.ImageResource), True, transColor))
    End Sub

    Public Sub NewTransparentColourImage(ByVal imageName As String, ByVal fileName As String, ByVal transColor As Color)
        NewTransparentColorImage(imageName.ToLower(), fileName, transColor)
    End Sub

    Public Sub NewSound(ByVal soundName As String, ByVal filename As String)
        _Sounds.Add(soundName.ToLower(), Audio.LoadSoundEffect(Core.GetPathToResource(filename, ResourceKind.SoundResource)))
    End Sub

    Private Sub NewMusic(ByVal musicName As String, ByVal filename As String)
        _Music.Add(musicName.ToLower(), Audio.LoadMusic(Core.GetPathToResource(filename, ResourceKind.SoundResource)))
    End Sub

    Private Sub FreeFonts()
        Dim obj As Font
        For Each obj In _Fonts.Values
            Text.FreeFont(obj)
        Next
    End Sub

    Public Sub FreeImages()
        Dim obj As Bitmap
        For Each obj In _Images.Values
            Graphics.FreeBitmap(obj)
        Next
    End Sub

    Private Sub FreeSounds()
        Dim obj As SoundEffect
        For Each obj In _Sounds.Values
            Audio.FreeSoundEffect(obj)
        Next
    End Sub

    Private Sub FreeMusic()
        Dim obj As Music
        For Each obj In _Music.Values
            Audio.FreeMusic(obj)
        Next
    End Sub

    Private Sub FreeMaps()
        Dim obj As Map
        For Each obj In _Maps.Values
            MappyLoader.FreeMap(obj)
        Next
    End Sub

    Public Sub FreeResources()
        FreeFonts()
        FreeImages()
        FreeMusic()
        FreeSounds()
        FreeMaps()
        Core.ProcessEvents()
    End Sub

#End Region
End Module