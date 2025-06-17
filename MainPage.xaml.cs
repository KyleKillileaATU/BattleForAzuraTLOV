namespace BattleForAzuraTLOV
{
    public partial class MainPage : ContentPage
    {
        int CurrentPlayerPositionX=0, CurrentPlayerPositionY=0;
        int RandomPositionX = 0, RandomPositionY = 0,rtime;
        int ei1curposX, ei2curposX, ei3curposX, ei4curposX, ei5curposX, ei6curposX, ei7curposX, ei8curposX;
        int ei1curposY, ei2curposY, ei3curposY, ei4curposY, ei5curposY, ei6curposY, ei7curposY, ei8curposY;
        Random RNGmove = new Random();


        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            //hideallcontent();
            //testcontent1();
            Infinite_RNG_Movement();
            Update_All_Position_Constant();
        }
        async void hideallcontent() // for testing will be kept out of use FN
        {
            await PlayerIMG.FadeTo(0, 50);
            await PlayerHitBox.FadeTo(0, 50);
            await PlayerCameraBox.FadeTo(0, 50);
            await ei1.FadeTo(0, 50);
            await ei2.FadeTo(0, 50);
            await ei3.FadeTo(0, 50);
            await ei4.FadeTo(0, 50);
            await ei5.FadeTo(0, 50);
            await ei6.FadeTo(0, 50);
            await ei7.FadeTo(0, 50);
            await ei8.FadeTo(0, 50);
        }
        async void testcontent1()
        {
            await PlayerIMG.TranslateTo(500, 500, 1000); // set pos
            await ei1.TranslateTo(500, 500, 1000);
            await ei2.TranslateTo(100, 500, 1000);
            await ei3.TranslateTo(-500, 500, 1000);
            await ei4.TranslateTo(250, -500, 1000);
            await ei5.TranslateTo(500, -100, 1000);
            await ei6.TranslateTo(50, 50, 1000);
            await ei7.TranslateTo(-500, -500, 1000);
            await ei8.TranslateTo(-100, -200, 1000);
            await ei1.TranslateTo(340, -350, 1000);
        }
        private void Move_BindButton_Clicked(object sender, EventArgs e)
        {
            Move_player();
            Move_player_Hit_Box();
            Move_player_Camera_Box();


        }
        private void MoveBTN_Clicked(object sender, EventArgs e)
        {
            if (CurrentPlayerPositionY >=-290) {
                Move_player();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
            else
            {
                ei1curposY = ei1curposY + 15;
                ei2curposY = ei2curposY + 15; 
                ei3curposY = ei3curposY + 15; 
                ei4curposY = ei4curposY + 15; 
                ei5curposY = ei5curposY + 15;
                ei6curposY = ei6curposY + 15;
                ei7curposY = ei7curposY + 15; 
                ei8curposY = ei8curposY + 15;

                
            }
        }
        async void Move_player()// split the 3 moving seperately so they all move at once together
        {
            CurrentPlayerPositionY=CurrentPlayerPositionY - 115;
            await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40); 

        }
        async void Move_player_Hit_Box()
        {
            //CurrentPlayerPositionY = CurrentPlayerPositionY - 15;
            await PlayerHitBox.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40);
        }
        async void Move_player_Camera_Box()
        {
            //CurrentPlayerPositionY = CurrentPlayerPositionY - 15;
            await PlayerCameraBox.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40);
        }
        async void Infinite_RNG_Movement()
        {
            while (true)
            {
                ei1curposX = (ei1curposX + RNGmove.Next(-30, 30));
                ei1curposY = (ei1curposY + RNGmove.Next(-30, 30));
                rtime = RNGmove.Next(150, 750);
                await ei1.TranslateTo(ei1curposX, ei1curposY, (uint)rtime);
                await Task.Delay(2000);
            }
        }
        async void Reset_All_Enemy_Position()
        {
            await ei1.TranslateTo(0, 0, 40);
            await ei2.TranslateTo(0, 0, 40);
            await ei3.TranslateTo(0, 0, 40);
            await ei4.TranslateTo(0, 0, 40);
            await ei5.TranslateTo(0, 0, 40);
            await ei6.TranslateTo(0, 0, 40);
            await ei7.TranslateTo(0, 0, 40);
            await ei8.TranslateTo(0, 0, 40);
        }
        async void Update_All_Position_Constant()
        {

            while (true)
            {
                await ei1.TranslateTo(ei1curposX, ei1curposY, 40);
                await ei2.TranslateTo(ei2curposX, ei2curposY, 40);
                await ei3.TranslateTo(ei3curposX, ei3curposY, 40);
                await ei4.TranslateTo(ei4curposX, ei4curposY, 40);
                await ei5.TranslateTo(ei5curposX, ei5curposY, 40);
                await ei6.TranslateTo(ei6curposX, ei6curposY, 40);
                await ei7.TranslateTo(ei7curposX, ei7curposY, 40);
                await ei8.TranslateTo(ei8curposX, ei8curposY, 40);
            }
        }
    }

}
