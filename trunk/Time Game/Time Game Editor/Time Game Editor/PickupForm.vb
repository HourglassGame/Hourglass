Public Class PickupForm

    Private Sub PickupForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub t_attach_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_attach_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseLeave
        text_string = t_attach
        end_text_string = True
    End Sub

    Private Sub t_type2_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_type2.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_type2_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_type2.MouseLeave
        text_string = t_type2
        end_text_string = True
    End Sub

    Private Sub b_attach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_attach.Click
        pickup_attach = True
    End Sub

End Class