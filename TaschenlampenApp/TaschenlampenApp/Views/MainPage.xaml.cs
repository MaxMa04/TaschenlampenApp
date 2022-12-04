using System.Diagnostics;
namespace TaschenlampenApp.Views;

public partial class MainPage : ContentPage
{
    public Stopwatch stopWatch = new Stopwatch();
    public bool taschenlampeAn = false;
	public MainPage()
	{
		InitializeComponent();
        Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
	}
    private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
    {
        if(taschenlampeAn == false) 
        {
            Accelerometer.Stop();
            hallo.Text = "Taschenlampe an";
            await Flashlight.TurnOnAsync();
            taschenlampeAn = true;
            timer();
        }
        else
        {
            Accelerometer.Stop();
            hallo.Text = "Taschenlampe aus";
            await Flashlight.TurnOffAsync();
            taschenlampeAn = false;
            timer();
        }
        
    }
    // Verhindert, dass die Taschenlampe direkt wieder aus/angeschaltet wird
    private async void timer()
    {
        stopWatch.Start();
        await Task.Delay(4000);
        if(stopWatch.ElapsedMilliseconds >= 4000)
        {
            Accelerometer.Start(SensorSpeed.Game);

        }

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Accelerometer.IsMonitoring == false)
        {
            Accelerometer.Start(SensorSpeed.Game);
        }
        
    }
}