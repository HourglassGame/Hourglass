Public Class SwitchForm

    Private Sub t_attach_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_attach_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseLeave
        text_string = t_attach
        end_text_string = True
    End Sub

    Private Sub t_attach2_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach2.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_attach2_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach2.MouseLeave
        text_string = t_attach2
        end_text_string = True
    End Sub

    Private Sub b_attach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_attach.Click
        switch_attach = True
    End Sub

    Private Sub b_rotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_rotate.Click
        switch_rotate = True
    End Sub

    Private Sub b_rotate2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_rotate2.Click
        switch_rotate2 = True
    End Sub

    Private Sub b_attach2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_attach2.Click
        switch_attach2 = True
    End Sub
End Class