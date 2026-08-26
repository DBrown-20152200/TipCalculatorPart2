namespace TipCalculatorPart2;

public partial class PreferenceOptions : ContentPage
{
    SoundData soundData = new SoundData();

	public PreferenceOptions()
	{
		InitializeComponent();
        soundData.InitialiseSound();
	}

    private void soundToggleSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set("SoundToggle", soundToggleSwitch.IsToggled);
        soundData.PlaySound();
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        soundToggleSwitch.IsToggled = Preferences.Get("SoundToggle", true);
        darkModeToggle.IsToggled = Preferences.Get("DarkMode", true);
        
    }

    private void ContentPage_Disappearing(object sender, EventArgs e)
    {
        
    }

    private void returnButton_Clicked(object sender, EventArgs e)
    {
        soundData.PlaySound();
        Navigation.PopModalAsync();
    }

    private void darkModeToggle_Toggled(object sender, ToggledEventArgs e)
    {
        soundData.PlaySound();
        Preferences.Set("DarkMode", darkModeToggle.IsToggled);
    }
}