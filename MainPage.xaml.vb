' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
''' 
''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Imports Windows.UI
Public NotInheritable Class MainPage
    Inherits Page
    Public mytimer As New DispatcherTimer
    Public stopit As Boolean = True
    Public counter As Double = 0  'Public
    'SimpleTimer1st is only used to show how to set seconds and stop it
    '  sound and visuals can be used to show the time is reached
    '  also GPIO out can be used to turn on lights or do other things
    Public Sub New()
        InitializeComponent()
        AddHandler mytimer.Tick, AddressOf mytimer_tick

    End Sub

    Private Sub butstart_Click(sender As Object, e As RoutedEventArgs) Handles butstart.Click
        If txtseconds.text <> "" Then
            probar.Value = counter              ' progressbar control was shown to introduce you
            probar.Maximum = txtseconds.Text
        End If
        If stopit = False Then     '  Checks to see if someone tried to stop this
            stopit = True
            butstart.Content = "Start"
        Else
            'Routine below to allow for interupting timer with boolean stopit=false
            butstart.Content = "Stop"
            counter = 0
            stopit = False  ' stop the timer
            'below starts the timer and sets the amount of time between it 1000 Mili or 1 second
            mytimer.Interval = TimeSpan.FromMilliseconds(1000)
            mytimer.Start()
        End If

    End Sub
    'Used as a timer to count seconds
    'Public mytimer As New DispatcherTimer 
    '     opens this up as an event And 
    '     you will have To add the Private Sub mytimer it As I have below
    Private Sub mytimer_tick(ByVal sender As Object, ByVal e As EventArgs)
        counter = counter + 1
        Dim a As Long

        'Below is a conditional checking for an error
        '  There are many ways to do this but Files and things that can fail need 
        '  to have something to catch errors
        Try
            a = txtseconds.Text
        Catch ex As Exception
            'No numeric available so
            txtseconds.Text = "5"  ' if nothign is there we set 5 seconds
            Exit Sub
        End Try

        'Use the progress bar named probar and counter 
        probar.value = counter

        If counter > txtseconds.text Then
            mytimer.Stop()     'Tell clock to pause or halt
            butstart.Content = "Time Reached-Restart?" ' & txtseconds.Text
            stopit = True
            'Add Sound here
        End If

        If stopit = False Then
            mytimer.Start()

            'Calling a Function that returns a string not used here
            ' txttimer.Text = formattime()
            ' if you do not want conversion you can just use seconds
            txttimer.Text = counter
        Else
            '   butstart.Content = "Start"
            mytimer.Stop()
        End If

    End Sub

    Private Function isnumeric(text As String) As Boolean
        Throw New NotImplementedException()
    End Function

    Public Function formattime() As String
        'Not used in this but introduced here to return Minutes and Seconds
        'There are many ways you could do this but this is one
        Dim mod1 As Double = 0
        Dim whole1 As Double = 0
        Dim mods As String = "A"
        Dim wholes As String = "A"

        'If you can use the Global value Counter to determine a preset time.
        whole1 = counter \ 60      'whole number left over
        mod1 = counter Mod 60      'divisor 
        mods = mod1.ToString.Trim      'not needed introduced .trim
        wholes = whole1.ToString.Trim 'not needed introduced

        'keep it 00:00
        If wholes.Length < 2 Then
            wholes = "0" & wholes
        End If

        If mods.Length < 2 Then
            mods = "0" & mods
        End If
        'returns it to the caller
        formattime = wholes & ":" & mods

    End Function
End Class
