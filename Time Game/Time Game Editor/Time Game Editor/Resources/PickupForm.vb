Public Class PortalForm
    Private Sub PortalForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PortalTime_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        request_text_string = True
    End Sub
    Private Sub PortalTime_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        text_string = PortalTime
        end_text_string = True
    End Sub
End Class