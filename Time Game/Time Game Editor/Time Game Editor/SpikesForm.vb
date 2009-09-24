Public Class SpikeForm

    Private Sub b_attach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_attach.Click
        spike_attach = True
    End Sub

    Private Sub b_rotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_rotate.Click
        spike_rotate = True
    End Sub

    Private Sub t_attach_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_attach_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseLeave
        text_string = t_attach
        end_text_string = True
    End Sub

    Private Sub t_size_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_size.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_size_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_size.MouseLeave
        text_string = t_size
        end_text_string = True
    End Sub
End Class