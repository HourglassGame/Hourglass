Public Class EditingForm

    Private Sub EditingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub b_wall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        pressed_wall = True
    End Sub

    Private Sub b_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        pressed_select = True
    End Sub

    Private Sub b_portal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_portal.Click
        pressed_portal = True
    End Sub

    Private Sub b_box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_box.Click
        pressed_box = True
    End Sub

    Private Sub Bb_platform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_platform.Click
        pressed_platform = True
    End Sub

    Private Sub b_switch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_switch.Click
        pressed_switch = True
    End Sub

    Private Sub b_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_delete.Click
        pressed_delete = True
    End Sub

    Private Sub b_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_save.Click
        If SaveFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            save_string = SaveFile.FileName
            pressed_save = True
        End If
    End Sub

    Private Sub b_load_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_load.Click
        If LoadFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            load_string = LoadFile.FileName
            pressed_load = True
        End If
    End Sub

    Private Sub b_pickup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_pickup.Click
        pressed_pickup = True
    End Sub

    Private Sub b_spike_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_spike.Click
        pressed_spike = True
    End Sub

    Private Sub t_name_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_name.MouseLeave
        text_string = t_name
        end_text_string = True
    End Sub
    Private Sub t_name_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_name.MouseEnter
        request_text_string = True
    End Sub

    Private Sub t_background_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_background.MouseLeave
        text_string = t_background
        end_text_string = True
    End Sub
    Private Sub t_background_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_background.MouseEnter
        request_text_string = True
    End Sub

    Private Sub t_foreground_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_foreground.MouseLeave
        text_string = t_foreground
        end_text_string = True
    End Sub
    Private Sub t_foreground_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_foreground.MouseEnter
        request_text_string = True
    End Sub

    Private Sub b_export_fore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_export_fore.Click
        pressed_export = True
    End Sub

    Private Sub b_gate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_gate.Click
        pressed_gate = True
    End Sub

    Private Sub b_export_fore_tileset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_export_fore_tileset.Click
        pressed_export_tileset = True
    End Sub
End Class