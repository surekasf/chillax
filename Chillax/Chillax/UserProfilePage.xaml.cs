namespace Chillax;

public partial class UserProfilePage : ContentPage
{
	public UserProfilePage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }
    private async void OnStartClicked(object sender, EventArgs e)
    {
        var username = UserNameEntry.Text;
        var selectedAge = Convert.ToInt32(AgePicker.SelectedItem);

        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(selectedAge.ToString()))
        {
           
            // Save user data in local preferences
            Preferences.Set("username", username);
            Preferences.Set("age", selectedAge);

            // Navigate to MainPage
            await Navigation.PushAsync(new MainPage(selectedAge));
        }
        else
        {
            await DisplayAlert("Error", "Please enter both your name and age.", "OK");
        }
    }
}
