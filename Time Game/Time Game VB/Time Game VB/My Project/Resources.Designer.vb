﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3053
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Hourglass.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        Friend ReadOnly Property _throw() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("_throw", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property arial() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("arial", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        Friend ReadOnly Property arrow() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("arrow", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Background() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Background", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property blackadder() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("blackadder", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        Friend ReadOnly Property box() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("box", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property box_die() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("box_die", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property box_hit() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("box_hit", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property box_hit_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("box_hit_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property box_pickup() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("box_pickup", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property box_pickup_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("box_pickup_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property button0_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button0_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button0_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button0_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button1_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button1_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button1_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button1_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button2_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button2_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button2_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button2_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button3_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button3_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property button3_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("button3_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property cour() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("cour", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        Friend ReadOnly Property credits() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("credits", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property flip_numbers() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("flip_numbers", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property flip_numbers_big() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("flip_numbers_big", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property interfaceLeft() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("interfaceLeft", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property interfaceRight() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("interfaceRight", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property laser_shoot() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("laser_shoot", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property laser_shoot_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("laser_shoot_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property laserswitch0() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("laserswitch0", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property laserswitch1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("laserswitch1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property laserswitch2() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("laserswitch2", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property laserswitch3() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("laserswitch3", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property loader_empty() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("loader_empty", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property loader_full() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("loader_full", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property paradox_box() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("paradox_box", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property paradox_guy() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("paradox_guy", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property paradox_pickup() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("paradox_pickup", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property pickup_pickup() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("pickup_pickup", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property pickup_pickup_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("pickup_pickup_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property portal_black() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_black", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property portal_charge() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_charge", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property portal_charge_used() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_charge_used", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property portal_effect() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_effect", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property portal_end() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_end", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property portal_in() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("portal_in", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property portal_in_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("portal_in_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property portal_reverse() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_reverse", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property portal_sound() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("portal_sound", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property portal_white() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("portal_white", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property reverse_time() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("reverse_time", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property script() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("script", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        Friend ReadOnly Property spike0() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("spike0", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property spike1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("spike1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property spike2() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("spike2", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property spike3() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("spike3", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property spiked() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("spiked", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property SplashBack() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("SplashBack", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property squished() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("squished", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property story() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("story", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property SwinGameAni() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("SwinGameAni", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property switch_push_down() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_push_down", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_push_down_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_push_down_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_push_up() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_push_up", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_push_up_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_push_up_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_toggle() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_toggle", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_toggle_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_toggle_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_trip_laser() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_trip_laser", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property switch_trip_laser_rev() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("switch_trip_laser_rev", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property time_gun() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("time_gun", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property time_jump() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("time_jump", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property time_jump_sound() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("time_jump_sound", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property time_reverse_sound() As System.IO.UnmanagedMemoryStream
            Get
                Return ResourceManager.GetStream("time_reverse_sound", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property toggle0_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle0_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle0_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle0_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle1_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle1_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle1_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle1_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle2_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle2_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle2_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle2_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle3_down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle3_down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property toggle3_up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("toggle3_up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Upsypupsy() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Upsypupsy", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property whiteportalswirl() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("whiteportalswirl", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Whiteportalswirlblack() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Whiteportalswirlblack", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
