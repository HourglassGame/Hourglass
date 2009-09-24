Public Class PortalForm

    Private Sub PortalTime_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PortalTime.MouseEnter
        request_text_string = True
    End Sub
    Private Sub PortalTime_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PortalTime.MouseLeave
        text_string = PortalTime
        end_text_string = True
    End Sub

    Private Sub Charges_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Charges.MouseEnter
        request_text_string = True
    End Sub
    Private Sub Charges_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Charges.MouseLeave
        text_string = Charges
        end_text_string = True
    End Sub

    Private Sub t_attach_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseEnter
        request_text_string = True
    End Sub
    Private Sub t_attach_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t_attach.MouseLeave
        text_string = t_attach
        end_text_string = True
    End Sub

    Private Sub b_attach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_attach.Click
        portal_attach = True
    End Sub
End Class