using TipCalculatorPart2.Themes;

namespace TipCalculatorPart2;

public partial class PreferenceOptions : ContentPage
{
    SoundData soundData = new SoundData();
    ThemeData themes = new ThemeData();

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

    private void returnButton_Clicked(object sender, EventArgs e)
    {
        soundData.PlaySound();
        Navigation.PopModalAsync();
    }

    private void darkModeToggle_Toggled(object sender, ToggledEventArgs e)
    {
        soundData.PlaySound();
        Preferences.Set("DarkMode", darkModeToggle.IsToggled);                
        themes.ChangeTheme();        
    }

    private void exoButton_Clicked(object sender, EventArgs e)
    {
        if (Preferences.Get("DarkMode", true) == true)
        {
            themes.ChangeTheme();
        }
        else
        {
            themes.ChangeTheme();
        }

        Preferences.Set("Theme", "Exo");
    }

    private void earthButton_Clicked(object sender, EventArgs e)
    {
        if (Preferences.Get("DarkMode", true) == true)
        {
            themes.ChangeTheme();
        }
        else
        {
            themes.ChangeTheme();
        }

        Preferences.Set("Theme", "Earth");
    }
}