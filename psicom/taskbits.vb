Module taskbits
    Friend Function processtasklist(ByRef p_a_bool() As Boolean, ByVal p_str As String) As Boolean
        Dim l_char As Char
        Dim l_byte As Byte
        Dim l_index_str As Integer
        Dim l_count As Integer
        Dim l_index_a_bool As Integer = 0
        Dim l_bool As Boolean
        Dim l_is_valid As Boolean

        If Not p_str Is Nothing AndAlso p_str <> "" Then

            p_str = p_str.ToUpper
            ReDim p_a_bool(-1)
            l_index_str = p_str.Length - 1

            While True
                If (l_index_str) < 0 Then Exit While

                l_char = p_str.Chars(l_index_str)

                If l_char >= Chr(Asc("A")) And l_char <= Chr(Asc("F")) Then
                    l_is_valid = True
                    l_byte = CByte(10 + Asc(l_char) - Asc("A"))
                ElseIf l_char >= Chr(Asc("0")) And l_char <= Chr(Asc("9")) Then
                    l_is_valid = True
                    l_byte = CByte(Asc(l_char) - Asc("0"))
                Else
                    l_is_valid = False
                End If

                If l_is_valid Then

                    For l_count = 1 To 4
                        l_bool = True

                        If Not CBool(l_byte And 1) Then
                            l_bool = False
                        End If
                        ReDim Preserve p_a_bool(l_index_a_bool)
                        p_a_bool(l_index_a_bool) = l_bool
                        l_index_a_bool += 1

                        l_byte = l_byte >> 1
                    Next
                End If

                l_index_str -= 1
            End While

            For l_count = l_index_a_bool To G_TASKLISTLENGTH - 1
                ReDim Preserve p_a_bool(l_count)
                p_a_bool(l_count) = False
            Next
        End If

        Return l_bool
    End Function

    Friend Function processtasklists() As Boolean
        Dim l_Bool As Boolean = True
        processtasklist(g_tasklistsched, GetSetting("MIFSEND", "Addrs", "TASKLISTSCHED"))
        processtasklist(g_tasklistcont, GetSetting("MIFSEND", "Addrs", "TASKLISTCONT"))
        processtasklist(g_tasklistmanual, GetSetting("MIFSEND", "Addrs", "TASKLISTMANUAL"))

        ' Do not allow a lockout situation
        If Not g_tasklistsched(G_TASKLIST_ENABLED) AndAlso Not g_tasklistcont(G_TASKLIST_ENABLED) Then
            g_tasklistsched(G_TASKLIST_ENABLED) = True
        End If

        Return l_Bool
    End Function

    Friend Function tasklistindex(ByVal p_block As Integer, ByVal p_indiv As Integer) As Integer
        Return (p_block << 3) + p_indiv
    End Function

    Friend Function tasklistenabled(ByRef p_tasklist() As Boolean) As Boolean
        Dim l_stat As Boolean = True

        If Not p_tasklist Is Nothing Then
            l_stat = p_tasklist(G_TASKLIST_ENABLED)
        End If

        Return l_stat
    End Function

    Friend Function tasklistbitenabled(ByRef p_tasklist() As Boolean, ByVal p_block As Integer, ByVal p_indiv As Integer) As Boolean
        Dim l_stat As Boolean = False

        If Not p_tasklist Is Nothing Then
            Dim l_index As Integer = tasklistindex(p_block, p_indiv)

            If Not l_index >= p_tasklist.Length Then
                l_stat = p_tasklist(l_index)
            End If
        End If

        Return l_stat
    End Function

End Module
