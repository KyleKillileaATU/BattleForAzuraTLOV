namespace BattleForAzuraTLOV
{
    public partial class MainPage : ContentPage
    {
      
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            hideallcontent();
        }
        async void hideallcontent()
        {
            await PlayerIMG.FadeTo(0, 50);
            await ei1.FadeTo(0, 50);
            await ei2.FadeTo(0, 50);
            await ei3.FadeTo(0, 50);
            await ei4.FadeTo(0, 50);
            await ei5.FadeTo(0, 50);
            await ei6.FadeTo(0, 50);
            await ei7.FadeTo(0, 50);
            await ei8.FadeTo(0, 50);
        }
    }

}
