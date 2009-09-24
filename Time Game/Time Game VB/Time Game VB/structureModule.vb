Module StructureModule

    Public Structure guyStruct

        'Physics
        Dim x() As Single 'X position
        Dim y() As Single 'Y position
        Dim xspeed() As Single ' xspeed
        Dim yspeed() As Single ' yspeed

        'Storing data for paradox checking
        Dim x_par() As Single 'x at a paradox time, 0 means do not check for paradox
        Dim y_par() As Single 'y at a paradox time
        Dim xspeed_par() As Single 'xspeed at a paradox time
        Dim yspeed_par() As Single 'yspeed at a paradox time
        Dim carry_par() As Integer 'whether carring and ID at a paradox time
        Dim pickupCheck() As Integer 'whether to check pickup proximity and which type
        Dim pickup_id() As Integer 'id of the pickup
        Dim pickup_time() As Integer 'time that the pickup was picked up

        'store data for support paradox checking
        Dim y_par_s() As Single
        Dim x_par_s() As Single
        Dim xspeed_par_s() As Single
        Dim yspeed_par_s() As Single
        Dim dir_par_s() As Integer 'direction of that the object was collied with in
        Dim object_par_s() As Integer ' 1 = platform, 2 = box

        'Carrying box
        Dim carry() As Integer ' carry ID of box 

        'how old it is in relative terms
        'Dim order As Integer

        'Start/end time and existance. Time Tracking
        Dim t_start As Long 'creation time
        Dim t_end As Long 'end time
        Dim t_end_specific As Boolean ' for time gun paradox
        Dim t_end_type As Integer ' 1 = portal whenever, 2 = portal specific, 3 = time jump, 4 = shot, 5 = time reverse
        Dim t_end_portal_id As Integer ' id of portal if end type has to do with portals
        Dim exist() As Boolean 'stores whether the instance exists at a time
        Dim t_direction As Boolean 'Direction that this instance travels in time, true for forward, false for backwards

        'Graphics
        Dim subimage As Integer 'frame of animation
        Dim face As Boolean ' direction (left or right)
        Dim face_store() As Boolean ' direction (left or right) store
        Dim laser_x_draw() As Single 'x position to draw laser to 
        Dim laser_y_draw() As Single 'y position to draw laser to 

        'paradox checking for laser
        Dim laser_x() As Single 'x aimed at
        Dim laser_y() As Single 'y aimed at
        Dim laser_objx() As Single 'x position of object shot
        Dim laser_objy() As Single 'y position of object shot
        Dim laser_id() As Single 'ID of object shot
        Dim laser_type() As Single 'Type of object shot
        Dim laser_time() As Single 'Time that the laser was set to send objects to

        'Input
        Dim k_left() As Boolean 'A key pressed
        Dim k_right() As Boolean 'D key pressed
        Dim k_up() As Boolean 'W key pressed
        Dim k_down() As Boolean 'S key pressed

        'Is it touching floor or box?
        Dim supported_obj() As Integer ' 1 = platform, 2 = box
        Dim supported_id() As Integer

        'Audio
        Dim playSound() As String 'sound to play at that time

    End Structure

    Public guy(200) As guyStruct

    Public Structure boxStruct

        'Physics
        Dim x() As Single
        Dim y() As Single
        Dim xspeed() As Single
        Dim yspeed() As Single

        'Is it carried?
        Dim carry() As Boolean
        Dim carry_id As Integer

        'Start/end time and existance
        Dim t_start As Long
        Dim t_end As Long
        Dim t_end_type As Integer ' 0 = portal, 1 = killed
        Dim t_end_carry As Boolean 'true = carried back/forwards. false = own power
        Dim exist() As Boolean

        'Graphics
        Dim start_gfx As Boolean 'draw start portal effect?
        Dim end_gfx As Boolean 'draw end portal effect?

        'Is it touching floor or another box?
        Dim supported_obj() As Integer ' 1 = platform, 2 = box
        Dim supported_id() As Integer

        'Audio
        Dim playSound() As String 'sound to play at that time
       
    End Structure

    Public box(200) As boxStruct

    Public Structure platStruct

        'Physics
        Dim x() As Single
        Dim y() As Single
        Dim xspeed() As Single
        Dim yspeed() As Single

        'Size
        Dim width As Single
        Dim height As Single

        Dim image As String 'drawn image

        'sound info is unused
        Dim start_sound_forward As String
        Dim end_sound_forward As String
        Dim move_sound_forward As String
        Dim start_sound_backward As String
        Dim end_sound_backward As String
        Dim move_sound_backward As String

        'Aim positions, where it will move to when on/off and how fast
        Dim x_on As Single
        Dim y_on As Single
        Dim xspeed_on As Single
        Dim yspeed_on As Single

        Dim x_off As Single
        Dim y_off As Single
        Dim xspeed_off As Single
        Dim yspeed_off As Single

        'On/Off and switch
        Dim state As Boolean
        Dim switch_id As Integer
        Dim switch_type As Boolean

    End Structure

    Public plat(40) As platStruct

    Public Structure switchStruct

        Dim type As Integer '0 = push, 1 = toggle

        'Physics
        Dim x As Single
        Dim y As Single
        Dim xoff As Single 'x offset from attached platform
        Dim yoff As Single 'y offset from attached platform
        Dim width As Single
        Dim height As Single
        Dim attach As Integer 'platform attached to
        Dim key As Integer 'unused
        Dim rotation As Integer 'rotation to be drawn at

        'Physics for second switch if toggle
        Dim x2 As Single
        Dim y2 As Single
        Dim xoff2 As Single
        Dim yoff2 As Single
        Dim width2 As Single
        Dim height2 As Single
        Dim attach2 As Integer
        Dim rotation2 As Integer

        Dim visible As Boolean

        'Hit things, which things can trigger it
        Dim hit_guy As Boolean
        Dim hit_box As Boolean
        Dim hit_plat As Boolean
        Dim hit_wall As Boolean

        'On/Off
        Dim state As Boolean
        Dim t_state() As Boolean
        Dim laser_x() As Single
        Dim laser_y() As Single

        'Audio
        Dim playSound() As String 'sound to play at that time

    End Structure

    Public switch(40) As switchStruct

    Public Structure pickupStruct

        'Physics
        Dim x As Single
        Dim y As Single
        Dim xoff As Single 'x offset from attachment plaform
        Dim yoff As Single 'y offset from attachment plaform
        Dim width As Single
        Dim height As Single
        Dim type As Integer 'time jump = 0, time gun = 1, reverse time = 2
        Dim type2 As Integer 'unused
        Dim attach As Integer 'platform attached to
        Dim t_end As Integer 'when it was/is picked up

    End Structure

    Public pickup(40) As pickupStruct

    Public Structure portalStruct

        Dim x As Integer
        Dim y As Integer
        Dim xoff As Single 'x offset from attachment plaform
        Dim yoff As Single 'y offset from attachment plaform
        Dim type As Integer 'add = 0, add and flip = 1, set = 2 , whenever = 3
        Dim effect As Long 'how much it affects time on it's type
        Dim max_charges As Integer ' how many times it can be used before dissapearing
        Dim charges As Integer ' how many times it can be used before dissapearing
        Dim attach As Integer 'platform attached to
        Dim endSub As Integer 'subimage that the portal ran out of charges on

    End Structure

    Public portal(40) As portalStruct

    Public Structure endPortalStruct

        Dim x As Integer
        Dim y As Integer
        Dim xoff As Single 'x offset from attachment plaform
        Dim yoff As Single 'y offset from attachment plaform
        Dim attach As Integer
       
    End Structure

    Public endPortal As endPortalStruct

    Public Structure spikeStruct

        Dim x As Single
        Dim y As Single
        Dim width As Single
        Dim height As Single
        Dim xoff As Single
        Dim yoff As Single
        Dim size As Integer
        Dim attach As Integer
        Dim rotation As Integer

    End Structure

    Public spike(40) As spikeStruct

    Public Structure gatestruct

        Dim type As Integer
        Dim attach1 As Integer
        Dim attach2 As Integer
        Dim state As Boolean

    End Structure

    Public gate(40) As gatestruct

End Module