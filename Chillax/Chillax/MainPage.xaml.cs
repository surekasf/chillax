
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Chillax
{
    public partial class MainPage : ContentPage
    {
        private int _userAge;

        public MainPage(int userAge)
        {
            InitializeComponent();
            _userAge = userAge;
            LoadCardData(_userAge);
           
        }
        public MainPage() : this(0) // Default age to 0 or some other default value
        {
            // You can handle any additional initialization here if needed
        }
        private async void LoadCardData(int ageGroup)
        {

            Riddle riddle = await DataManager.GetRandomRiddleForAge(ageGroup);
            Puzzle puzzle = await DataManager.GetRandomPuzzleForAge(ageGroup);
            FunFact funfact = await DataManager.GetRandomFunFactForAge(ageGroup);
            if (riddle != null)
            {
                // Assuming you have a method to display the riddle
                RiddleLabel.Text = riddle.Question.ToString()+ " Ans: "+  riddle.Answer.ToString();
            }
            if (puzzle != null)
            {
                // Assuming you have a method to display the riddle
                PuzzleLabel.Text = puzzle.Challenge.ToString()+"  Solution: "+ puzzle.Solution.ToString();
            }
            if (funfact != null)
            {
                // Assuming you have a method to display the riddle
                FunFactLabel.Text = funfact.Fact.ToString();
            }
           
        }


        private async void OnCardTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter == null) return;

            var cardNumber = e.Parameter.ToString(); // Identify which card was tapped

            switch (cardNumber)
            {
                case "1":
                    await FlipCard(Card1Front, Card1Back);
                    break;
                case "2":
                    await FlipCard(Card2Front, Card2Back);
                    break;
                case "3":
                    await FlipCard(Card3Front, Card3Back);
                    break;
            }
        }

        private async Task FlipCard(Frame front, Frame back)
        {
            if (front.IsVisible)
            {
                // Flip to back
                await front.RotateYTo(90, 250); // Halfway flip
                front.IsVisible = false;
                back.IsVisible = true;
                back.RotationY = -90; // Reset rotation for back side
                await back.RotateYTo(0, 250); // Complete flip
            }
            else
            {
                // Flip to front
                await back.RotateYTo(90, 250); // Halfway flip
                back.IsVisible = false;
                front.IsVisible = true;
                front.RotationY = -90; // Reset rotation for front side
                await front.RotateYTo(0, 250); // Complete flip
            }
        }

        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Navigate to the UserProfilePage
         
            await Navigation.PushAsync(new UserProfilePage());
           
        }

        private async void OntrymoreClicked(object sender, EventArgs e)
        {
            // Example of a tap handler for another action
            LoadCardData(_userAge);
        }
    }
}
