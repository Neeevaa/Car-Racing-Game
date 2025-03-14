Public Class Form1
    Dim speed As Integer
    Dim road(7) As PictureBox
    Dim score As Integer = 0
    Dim gameOver As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        speed = 3
        gameOver = False
        road(0) = PictureBox1
        road(1) = PictureBox2
        road(2) = PictureBox3
        road(3) = PictureBox4
        road(4) = PictureBox5
        road(5) = PictureBox6
        road(6) = PictureBox7
        road(7) = PictureBox8
    End Sub

    Private Sub RoadMover_Tick(sender As Object, e As EventArgs) Handles RoadMover.Tick
        If Not gameOver Then
            ' Move road
            For x As Integer = 0 To 7
                road(x).Top += speed
                If road(x).Top >= Me.Height Then
                    road(x).Top = -road(x).Height
                End If
            Next

            ' Adjust speed based on score
            If score > 10 And score < 20 Then
                speed = 5
            End If
            If score > 20 And score < 30 Then
                speed = 6
            End If
            If score > 40 And score < 50 Then
                speed = 7
            End If
            If score > 100 Then
                speed = 9
            End If

            Label2.Text = "Speed" & speed
        End If
    End Sub

    Private Sub CheckCollisions()
        ' Check all possible collisions
        Dim obstacles() As PictureBox = {whiteblue, blackwhite, red}

        For Each obstacle As PictureBox In obstacles
            If shinyblue.Bounds.IntersectsWith(obstacle.Bounds) Then
                endgame()
                Exit Sub
            End If
        Next
    End Sub

    Private Sub endgame()
        gameOver = True
        Button1.Visible = True
        Label3.Visible = True
        RoadMover.Stop()
        RaceMover1.Stop()
        RaceMover2.Stop()
        RaceMover3.Stop()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not gameOver Then
            If e.KeyCode = Keys.Right Then
                RightSide.Start()
            End If
            If e.KeyCode = Keys.Left Then
                LeftSide.Start()
            End If
        End If
    End Sub

    Private Sub RightSide_Tick(sender As Object, e As EventArgs) Handles RightSide.Tick
        If Not gameOver AndAlso (shinyblue.Location.X < 295) Then
            shinyblue.Left += 5
            CheckCollisions()
        End If
    End Sub

    Private Sub LeftSide_Tick(sender As Object, e As EventArgs) Handles LeftSide.Tick
        If Not gameOver AndAlso (shinyblue.Location.X > 0) Then
            shinyblue.Left -= 5
            CheckCollisions()
        End If
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        RightSide.Stop()
        LeftSide.Stop()
    End Sub

    Private Sub RaceMover1_Tick(sender As Object, e As EventArgs) Handles RaceMover1.Tick
        If Not gameOver Then
            whiteblue.Top += speed / 2
            If whiteblue.Top = Me.Height Then
                score += 1
                Label1.Text = "score" & score
                whiteblue.Top = -(CInt(Math.Ceiling(Rnd() * 250)) + whiteblue.Height)
                whiteblue.Left = CInt(Math.Ceiling(Rnd() * 120)) + 100
                CheckCollisions()
            End If
        End If
    End Sub

    Private Sub RaceMover2_Tick(sender As Object, e As EventArgs) Handles RaceMover2.Tick
        If Not gameOver Then
            blackwhite.Top += speed / 3
            If blackwhite.Top = Me.Height Then
                score += 1
                Label1.Text = "score" & score
                blackwhite.Top = -(CInt(Math.Ceiling(Rnd() * 250)) + blackwhite.Height)
                blackwhite.Left = CInt(Math.Ceiling(Rnd() * 100)) + 80
                CheckCollisions()
            End If
        End If
    End Sub

    Private Sub RaceMover3_Tick(sender As Object, e As EventArgs) Handles RaceMover3.Tick
        If Not gameOver Then
            red.Top += speed / 3
            If red.Top = Me.Height Then
                score += 1
                Label1.Text = "score" & score
                red.Top = -(CInt(Math.Ceiling(Rnd() * 250)) + red.Height)
                red.Left = CInt(Math.Ceiling(Rnd() * 120)) + 100
                CheckCollisions()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        score = 0
        Me.Controls.Clear()
        InitializeComponent()
        Form1_Load(e, e)
    End Sub
End Class