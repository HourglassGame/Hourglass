Public Class PlatformForm

    Private Sub b_width_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_width.Click
        plat_width = True
    End Sub

    Private Sub b_height_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_height.Click
        plat_height = True
    End Sub

    Private Sub b_on_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_on.Click
        plat_on = True
    End Sub

    Private Sub b_off_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_off.Click
        plat_off = True
    End Sub

    Private Sub b_start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_start.Click
        plat_start = True
    End Sub
    Private Sub b_switch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_switch.Click
        plat_switch = True
    End Sub

    Private Sub t_switch_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_switch.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_switch_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_switch.MouseLeave
        text_string = t_switch
        end_text_string = True
    End Sub
    Private Sub start_x_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start_x.MouseEnter
        request_text_string = True
    End Sub
    Private Sub start_x_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start_x.MouseLeave
        text_string = start_x
        end_text_string = True
    End Sub
    Private Sub start_y_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start_y.MouseEnter
        request_text_string = True
    End Sub
    Private Sub start_y_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start_y.MouseLeave
        text_string = start_y
        end_text_string = True
    End Sub
    Private Sub t_width_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_width.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_width_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_width.MouseLeave
        text_string = t_width
        end_text_string = True
    End Sub
    Private Sub t_height_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_height.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_height_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_height.MouseLeave
        text_string = t_height
        end_text_string = True
    End Sub
    Private Sub on_x_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_x.MouseEnter
        request_text_string = True
    End Sub
    Private Sub on_x_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_x.MouseLeave
        text_string = on_x
        end_text_string = True
    End Sub
    Private Sub on_y_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_y.MouseEnter
        request_text_string = True
    End Sub
    Private Sub on_y_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_y.MouseLeave
        text_string = on_y
        end_text_string = True
    End Sub
    Private Sub on_xspeed_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_xspeed.MouseEnter
        request_text_string = True
    End Sub
    Private Sub on_xspeed_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_xspeed.MouseLeave
        text_string = on_xspeed
        end_text_string = True
    End Sub
    Private Sub on_yspeed_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_yspeed.MouseEnter
        request_text_string = True
    End Sub
    Private Sub on_yspeed_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_yspeed.MouseLeave
        text_string = on_yspeed
        end_text_string = True
    End Sub
    Private Sub off_x_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_x.MouseEnter
        request_text_string = True
    End Sub
    Private Sub off_x_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_x.MouseLeave
        text_string = off_x
        end_text_string = True
    End Sub
    Private Sub off_y_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_y.MouseEnter
        request_text_string = True
    End Sub
    Private Sub off_y_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_y.MouseLeave
        text_string = off_y
        end_text_string = True
    End Sub
    Private Sub off_xspeed_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_xspeed.MouseEnter
        request_text_string = True
    End Sub
    Private Sub off_xspeed_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_xspeed.MouseLeave
        text_string = off_xspeed
        end_text_string = True
    End Sub
    Private Sub off_yspeed_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_yspeed.MouseEnter
        request_text_string = True
    End Sub
    Private Sub off_yspeed_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_yspeed.MouseLeave
        text_string = off_yspeed
        end_text_string = True
    End Sub

    Private Sub image_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles image.MouseEnter
        request_text_string = True
    End Sub
    Private Sub image_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles image.MouseLeave
        text_string = image
        end_text_string = True
    End Sub

    Private Sub PlatformForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub t_forwards_sound_start_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_start.TextChanged

    End Sub

    Private Sub t_forwards_sound_start_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_start.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_forwards_sound_start_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_start.MouseLeave
        text_string = t_forwards_sound_start
        end_text_string = True
    End Sub
    Private Sub t_forwards_sound_end_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_end.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_forwards_sound_end_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_end.MouseLeave
        text_string = t_forwards_sound_end
        end_text_string = True
    End Sub
    Private Sub t_forwards_sound_move_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_move.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_forwards_sound_move_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_forwards_sound_move.MouseLeave
        text_string = t_forwards_sound_move
        end_text_string = True
    End Sub

    Private Sub t_back_sound_start_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_back_sound_start.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_back_sound_start_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_back_sound_start.MouseLeave
        text_string = t_back_sound_start
        end_text_string = True
    End Sub
    Private Sub t_back_sound_end_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_back_sound_end.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_back_sound_end_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_back_sound_end.MouseLeave
        text_string = t_back_sound_end
        end_text_string = True
    End Sub
    Private Sub t_back_sound_move_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_back_sound_move.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_back_sound_move_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_back_sound_move.MouseLeave
        text_string = t_back_sound_move
        end_text_string = True
    End Sub
End Class