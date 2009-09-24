Module StructureModule

    Public Structure guyStruct

        Dim x As Single
        Dim y As Single

    End Structure

    Public guy As guyStruct

    Public Structure endPortalStruct

        Dim x As Single
        Dim y As Single
        Dim attach As Integer

    End Structure

    Public endPortal As endPortalStruct

    Public Structure boxStruct

        Dim x As Single
        Dim y As Single

    End Structure

    Public box(100) As boxStruct

    Public Structure platStruct

        Dim x As Single
        Dim y As Single
        Dim width As Single
        Dim height As Single

        Dim image As String

        Dim start_sound_forward As String
        Dim end_sound_forward As String
        Dim move_sound_forward As String
        Dim start_sound_backward As String
        Dim end_sound_backward As String
        Dim move_sound_backward As String

        'Aim positions
        Dim x_on As Single
        Dim y_on As Single
        Dim xspeed_on As Single
        Dim yspeed_on As Single

        Dim x_off As Single
        Dim y_off As Single
        Dim xspeed_off As Single
        Dim yspeed_off As Single

        'On/Off and switch
        Dim switch_id As Long

    End Structure

    Public plat(50) As platStruct

    Public Structure switchStruct

        Dim type As Integer 'button = 1, duel toggle = 2

        Dim x As Single
        Dim y As Single
        Dim attach As Integer
        Dim rotation As Integer

        Dim x2 As Single
        Dim y2 As Single
        Dim attach2 As Integer
        Dim rotation2 As Integer

        Dim visible As Boolean

        'Hit things
        Dim hit_guy As Boolean
        Dim hit_box As Boolean
        Dim hit_plat As Boolean
        Dim hit_wall As Boolean

    End Structure

    Public switch(50) As switchStruct

    Public Structure portalStruct

        Dim x As Long
        Dim y As Long
        Dim type As Integer 'add = 1, set = 2, add and flip = 3, whenever = 4
        Dim effect As Long 'how much it affects time on it's type
        Dim charges As Integer
        Dim attach As Integer

    End Structure

    Public portal(50) As portalStruct

    Public Structure pickupStruct

        Dim x As Single
        Dim y As Single
        Dim type As Integer 'time jump = 1, time gun = 2, key = 3
        Dim type2 As Integer
        Dim attach As Integer

    End Structure

    Public pickup(50) As pickupStruct

    Public Structure spikeStruct

        Dim x As Single
        Dim y As Single
        Dim size As Integer
        Dim attach As Integer
        Dim rotation As Integer

    End Structure

    Public spike(50) As spikeStruct

    Public Structure gateStruct
        Dim type As Integer '0 = and, 1 = or, 2 = xor
        Dim x As Single
        Dim y As Single
        Dim attach1 As Integer
        Dim attach2 As Integer
    End Structure

    Public gate(50) As gateStruct

End Module