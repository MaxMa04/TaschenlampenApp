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
    private void Accelerometer_ShakeDetected(object sender, EventArgs e)
    {
        if(taschenlampeAn == false) 
        {
            hallo.Text = "Taschenlampe an";
            taschenlampeAn = true;
            timer();
        }
        else
        {
            hallo.Text = "Taschenlampe aus";
            taschenlampeAn = false;
            timer();
        }
        
    }
    private async void timer()
    {
        Accelerometer.Stop();
        stopWatch.Start();
        await Task.Delay(2000);
        if (stopWatch.ElapsedMilliseconds >= 2000)
        {
            stopWatch.Reset();
            Accelerometer.Start(SensorSpeed.Game);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Accelerometer.Start(SensorSpeed.Game);
    }
}