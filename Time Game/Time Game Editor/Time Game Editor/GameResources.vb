Imports SwinGame
Imports System.Collections.Generic

Imports System
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Drawing


Public Module GameResources

    Private Sub LoadFonts()
        NewFont("ArialLarge", "arial.ttf", 80)
        NewFont("Courier", "cour.ttf", 16)
    End Sub

    Private Sub LoadImages()

        NewTransparentColorImage("guy_left", "guy_left.png", Color.White)
        NewTransparentColorImage("portal_white", "portal_white.png", Color.Black)
        NewTransparentColorImage("portal_black", "portal_black.png", Color.White)
        NewTransparentColorImage("button0_up", "button0_up.png", Color.White)
        NewTransparentColorImage("button1_up", "button1_up.png", Color.White)
        NewTransparentColorImage("button2_up", "button2_up.png", Color.White)
        NewTransparentColorImage("button3_up", "button3_up.png", Color.White)
        NewTransparentColorImage("button0_off_red", "button0_off_red.png", Color.White)
        NewTransparentColorImage("button1_off_red", "button1_off_red.png", Color.White)
        NewTransparentColorImage("button2_off_red", "button2_off_red.png", Color.White)
        NewTransparentColorImage("button3_off_red", "button3_off_red.png", Color.White)
        NewTransparentColorImage("button0_off_green", "button0_off_green.png", Color.White)
        NewTransparentColorImage("button1_off_green", "button1_off_green.png", Color.White)
        NewTransparentColorImage("button2_off_green", "button2_off_green.png", Color.White)
        NewTransparentColorImage("button3_off_green", "button3_off_green.png", Color.White)
        NewTransparentColorImage("spike0", "spike0.png", Color.White)
        NewTransparentColorImage("spike1", "spike1.png", Color.White)
        NewTransparentColorImage("spike2", "spike2.png", Color.White)
        NewTransparentColorImage("spike3", "spike3.png", Color.White)
        NewTransparentColorImage("laserswitch0", "laserswitch0.png", Color.White)
        NewTransparentColorImage("laserswitch1", "laserswitch1.png", Color.White)
        NewTransparentColorImage("laserswitch2", "laserswitch2.png", Color.White)
        NewTransparentColorImage("laserswitch3", "laserswitch3.png", Color.White)
        NewTransparentColorImage("arrow", "arrow.png", Color.White)
        NewImage("box", "box.png")
        NewTransparentColorImage("time_jump", "time_jump.png", Color.White)
        NewTransparentColorImage("time_gun", "time_gun.png", Color.White)
        NewTransparentColorImage("reverse_time", "reverse_time.png", Color.Magenta)
        NewTransparentColorImage("portal_end", "portal_end.png", Color.Black)
        NewTransparentColorImage("and", "and.png", Color.White)
        NewTransparentColorImage("or", "or.png", Color.White)
        NewTransparentColorImage("xor", "xor.png", Color.White)

    End Sub

    Private Sub LoadSounds()

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
    Public Function GameFont(ByVal font As String) As SwinGame.Font
        Return _Fonts(font.ToLower())
    End Function

    ''' <summary>
    ''' Gets an Image loaded in the Resources
    ''' </summary>
    ''' <param name="image">Name of image</param>
    ''' <returns>The image loaded with this name</returns>
    ''' <remarks></remarks>
    Public Function GameImage(ByVal image As String) As SwinGame.Bitmap
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

    Private _Images As New Dictionary(Of String, SwinGame.Bitmap)
    Private _Fonts As New Dictionary(Of String, SwinGame.Font)
    Private _Sounds As New Dictionary(Of String, SoundEffect)
    Private _Music As New Dictionary(Of String, Music)
    Private _Maps As New Dictionary(Of String, Map)

    Private _Background As SwinGame.Bitmap
    Private _Animation As SwinGame.Bitmap
    Private _LoaderFull As SwinGame.Bitmap
    Private _LoaderEmpty As SwinGame.Bitmap
    Private _LoadingFont As SwinGame.Font
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
        Core.RefreshScreen()
        Core.ProcessEvents()

        _Animation = SwinGame.Graphics.LoadBitmap(Core.GetPathToResource("SwinGameAni.png", ResourceKind.ImageResource))
        _LoadingFont = SwinGame.Text.LoadFont(Core.GetPathToResource("arial.ttf", ResourceKind.FontResource), 12)
        _StartSound = Audio.LoadSoundEffect(Core.GetPathToResource("SwinGameStart.ogg", ResourceKind.SoundResource))

        _LoaderFull = SwinGame.Graphics.LoadBitmap(Core.GetPathToResource("loader_full.png", ResourceKind.ImageResource))
        _LoaderEmpty = SwinGame.Graphics.LoadBitmap(Core.GetPathToResource("loader_empty.png", ResourceKind.ImageResource))

    End Sub

    Private Sub ShowMessage(ByVal message As String, ByVal number As Integer)
        Const TX = 310, TY = 493, TW = 200, TH = 25, STEPS = 5, BG_X = 279, BG_Y = 453

        Dim fullW As Integer

        fullW = 260 * number \ STEPS
        SwinGame.Graphics.DrawBitmap(_LoaderEmpty, BG_X, BG_Y)
        SwinGame.Graphics.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y)

        SwinGame.Text.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH)

        Core.RefreshScreen()
        Core.ProcessEvents()
    End Sub

    Private Sub EndLoadingScreen(ByVal width As Integer, ByVal height As Integer)
        Core.ProcessEvents()
        Core.Sleep(500)
        SwinGame.Graphics.ClearScreen()
        Core.RefreshScreen()
        SwinGame.Text.FreeFont(_LoadingFont)
        SwinGame.Graphics.FreeBitmap(_Animation)
        SwinGame.Graphics.FreeBitmap(_LoaderEmpty)
        SwinGame.Graphics.FreeBitmap(_LoaderFull)
        Audio.FreeSoundEffect(_StartSound)
        Core.ChangeScreenSize(width, height)
    End Sub

    Private Sub NewMap(ByVal mapName As String)
        _Maps.Add(mapName.ToLower(), MappyLoader.LoadMap(mapName))
    End Sub

    Private Sub NewFont(ByVal fontName As String, ByVal filename As String, ByVal size As Integer)
        _Fonts.Add(fontName.ToLower(), SwinGame.Text.LoadFont(Core.GetPathToResource(filename, ResourceKind.FontResource), size))
    End Sub

    Private Sub NewImage(ByVal imageName As String, ByVal filename As String)
        _Images.Add(imageName.ToLower(), SwinGame.Graphics.LoadBitmap(Core.GetPathToResource(filename, ResourceKind.ImageResource)))
    End Sub

    Private Sub NewTransparentColorImage(ByVal imageName As String, ByVal fileName As String, ByVal transColor As Color)
        _Images.Add(imageName.ToLower(), SwinGame.Graphics.LoadBitmap(Core.GetPathToResource(fileName, ResourceKind.ImageResource), True, transColor))
    End Sub

    Private Sub NewTransparentColourImage(ByVal imageName As String, ByVal fileName As String, ByVal transColor As Color)
        NewTransparentColorImage(imageName.ToLower(), fileName, transColor)
    End Sub

    Private Sub NewSound(ByVal soundName As String, ByVal filename As String)
        _Sounds.Add(soundName.ToLower(), Audio.LoadSoundEffect(Core.GetPathToResource(filename, ResourceKind.SoundResource)))
    End Sub

    Private Sub NewMusic(ByVal musicName As String, ByVal filename As String)
        _Music.Add(musicName.ToLower(), Audio.LoadMusic(Core.GetPathToResource(filename, ResourceKind.SoundResource)))
    End Sub

    Private Sub FreeFonts()
        Dim obj As SwinGame.Font
        For Each obj In _Fonts.Values
            SwinGame.Text.FreeFont(obj)
        Next
    End Sub

    Private Sub FreeImages()
        Dim obj As SwinGame.Bitmap
        For Each obj In _Images.Values
            SwinGame.Graphics.FreeBitmap(obj)
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
