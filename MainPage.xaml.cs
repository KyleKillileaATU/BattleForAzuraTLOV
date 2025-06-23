

namespace BattleForAzuraTLOV
{
    public partial class MainPage : ContentPage
    {
        int CurrentPlayerPositionX = 0, CurrentPlayerPositionY = 0;
        int BackgroundCurrentPositionX = 0, BackgroundCurrentPositionY = 0;
        int RandomPositionX = 0, RandomPositionY = 0, rtime, projectilecycle01 = 0, weaponequipped = 0;
        int ei1curposX, ei2curposX, ei3curposX, ei4curposX, ei5curposX, ei6curposX, ei7curposX, ei8curposX;
        int ei1curposY, ei2curposY, ei3curposY, ei4curposY, ei5curposY, ei6curposY, ei7curposY, ei8curposY;
        int activeprojectilepositioni1X, activeprojectilepositioni1Y;
        int activeprojectilepositioni2X, activeprojectilepositioni2Y;
        int activeprojectilepositioni3X, activeprojectilepositioni3Y;
        int activeprojectilepositioni4X, activeprojectilepositioni4Y;
        int activeprojectilepositioni5X, activeprojectilepositioni5Y;
        int activeprojectilepositioni6X, activeprojectilepositioni6Y;
        int activeprojectilepositioni7X, activeprojectilepositioni7Y;
        int activeprojectilepositioni8X, activeprojectilepositioni8Y;
        int activeprojectilepositioni9X, activeprojectilepositioni9Y;
        int activeprojectilepositioni10X, activeprojectilepositioni10Y;
        int activeprojectilepositioni11X, activeprojectilepositioni11Y;
        int ammunition01 = 0;
        int gamelevelflag=0, gamestatus=0;
        Random RNGmove = new Random();


        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            setupallgamemenu();
            hideallgamecontent();
            //testcontent1();
            //Infinite_RNG_Movement();
            //Update_All_Position_Constant(); // to be activated on or off game start and end 
            await PlayerHPbar.TranslateTo(-98, 0, 40);
            await PlayerStaminabar.TranslateTo(-93, 0, 40);
            await PlayerMagicbar.TranslateTo(-102, 0, 40);
        }
        private void hideallgamecontent() // hides all content game assets (enemies, objects, etc )
        {
            hideplayer();
            hideenemyinstances01();
            hideprojectileinstances();
            hideplayerui();
            
        }
        
        async void hideplayer()
        {
            await BackgroundIMG.FadeTo(0, 5);
            await PlayerIMG.FadeTo(0, 5);
            await PlayerHitBox.FadeTo(0, 5);
            await PlayerCameraBox.FadeTo(0, 5);
        }
        async void hideenemyinstances01()
        {
            await e001.FadeTo(0, 5);
            await e002.FadeTo(0, 5);
            await e003.FadeTo(0, 5);
            await e004.FadeTo(0, 5);
            await e005.FadeTo(0, 5);
            await e006.FadeTo(0, 5);
            await e007.FadeTo(0, 5);
            await e008.FadeTo(0, 5);
        }
        async void hideprojectileinstances()
        {
            await Projectile01.FadeTo(0, 4);
            await Projectile02.FadeTo(0, 4);
            await Projectile03.FadeTo(0, 4);
            await Projectile04.FadeTo(0, 4);
            await Projectile05.FadeTo(0, 4);
            await Projectile06.FadeTo(0, 4);
            await Projectile07.FadeTo(0, 4);
            await Projectile08.FadeTo(0, 4);
            await Projectile09.FadeTo(0, 4);
            await Projectile10.FadeTo(0, 4);
            await Projectile11.FadeTo(0, 4);
        }
        async void hideplayerui()
        {
            await PlayerMagicbase.FadeTo(0, 5);
            await PlayerMagicbar.FadeTo(0, 5);
            await PlayerHPbase.FadeTo(0, 5);
            await PlayerHPbar.FadeTo(0, 5);
            await PlayerStaminabase.FadeTo(0, 5);
            await PlayerStaminabar.FadeTo(0, 5);
            await leftmovebutton.FadeTo(0, 5);
            await attackbutton.FadeTo(0, 5);
        }
        // menu set ups ( positionings and states )
        private void setupallgamemenu()
        {
            setupmainmenu();
            setupnewgamemenu();
            setupcontinuemenu();
            setupTestAcceptmenu();
            setupMissionsmenu();
            setupsupershopmenu();
            setupchallengesmenu();
            setupmusicmenu();
            setupsettingsmenu();
            setuplevelstatsmenu01();
            setuplevelstatsmenu02();
        }
        async void setupmainmenu()
        {
            await NewGamebutton.TranslateTo(-375, 185, 5);
            await Continuebutton.TranslateTo(-250, 187, 5);
            await Trainingbutton.TranslateTo(-125, 189, 5);
            await Missionbutton.TranslateTo(0, 191, 5);
            await SuperShopbutton.TranslateTo(125, 193, 5);
            await Brutalbutton.TranslateTo(250, 195, 5);
            await Challengebutton.TranslateTo(375, 197, 5);
            await Musicbutton.TranslateTo(400, -185, 5);
            await Settingsbutton.TranslateTo(400, -140, 5);
            await BattleForAzuraTitle.TranslateTo(-50, 0, 5);
            await NewGamebutton.RotateTo(1, 5);
            await Continuebutton.RotateTo(1, 5);
            await Trainingbutton.RotateTo(1, 5);
            await Missionbutton.RotateTo(1, 5);
            await SuperShopbutton.RotateTo(1, 5);
            await Brutalbutton.RotateTo(1, 5);
            await Challengebutton.RotateTo(1, 5);
            await BattleForAzuraTitle.RotateTo(-8, 5);
            this.Resources["ColourOfNewGameBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfContinueBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfTrainingBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMissionBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSuperShopBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfBrutalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfChallengeBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusicBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSettingsBTNClicked"] = Colors.DarkSlateGrey;
        }
        async void setupnewgamemenu()
        {
            await easydiffbutton.TranslateTo(-280, -1050, 5);
            await normaldiffbutton.TranslateTo(-280, -1020, 5);
            await harddiffbutton.TranslateTo(-280, -1010, 5);
            await veryharddiffbutton.TranslateTo(-280, -1040, 5);
            await accept01button.TranslateTo(125, (195 - 1000), 5);
            await leavebutton.TranslateTo(250, (195 - 1000), 5);
            await NewGameScreen01.TranslateTo(0, -1000, 5);
            await easydiffbutton.ScaleTo(0.6, 5);
            await normaldiffbutton.ScaleTo(0.6, 5);
            await harddiffbutton.ScaleTo(0.6, 5);
            await veryharddiffbutton.ScaleTo(0.6, 5);
            await accept01button.ScaleTo(0.6, 5);
            await leavebutton.ScaleTo(0.6, 5);
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
        }
        async void setupcontinuemenu()
        {
            await saveslot1button.TranslateTo(-325, -1050, 5);
            await saveslot2button.TranslateTo(-325, -1000, 5);
            await saveslot3button.TranslateTo(-325, -950, 5);
            await deletesavebutton.TranslateTo(0, (195-1000), 5);
            await accept02button.TranslateTo(125, (195 - 1000), 5);
            await leave02button.TranslateTo(250, (195 - 1000), 5);
            await ContinueScreen01.TranslateTo(0, -1000, 5); 
            await saveslot1button.RotateTo(-15, 5);
            await saveslot2button.RotateTo(-15, 5);
            await saveslot3button.RotateTo(-15, 5);
            await deletesavebutton.ScaleTo(0.6, 5);
            await accept02button.ScaleTo(0.6, 5);
            await leave02button.ScaleTo(0.6, 5);
            this.Resources["ColourOfSave1BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.DarkSlateGrey;
        }
        async void setupTestAcceptmenu()
        {

            await accept03button.TranslateTo(-75, (40 - 1000), 5);
            await leave03button.TranslateTo(75, (40 - 1000), 5);
            await GrayFilterScreen01.TranslateTo(0, 0, 5);
            await GrayFilterScreen01.FadeTo(0, 5);
        }
        async void setupMissionsmenu()
        {
            await previousmissionbutton.TranslateTo(-445, (30 - 1000), 5);
            await nextmissionbutton.TranslateTo(445, (30 - 1000), 5);
            await missionstatsbutton.TranslateTo(0, (195 - 1000), 5);
            await accept04button.TranslateTo(125, (195 - 1000), 5);
            await leave04button.TranslateTo(250, (195 - 1000), 5);
            await MissionScreen01.TranslateTo(0, -1000, 5);
            await missionstatsbutton.ScaleTo(0.6, 5);
            await accept04button.ScaleTo(0.6, 5);
            await leave04button.ScaleTo(0.6, 5);
        }
        async void setupsupershopmenu()
        {


        }
        async void setupchallengesmenu()
        {
          
        }
        async void setupmusicmenu()
        {
           
        }
        async void setupsettingsmenu()
        {
            await GrayFilterScreen01.TranslateTo(0, -1000, 5);
        }
        async void setuplevelstatsmenu01()
        {

            await LevelStatsScreen01.TranslateTo(0, -1000, 5);

        }
        async void setuplevelstatsmenu02()
        {
        

        }

        // button stuff
        private void Move_BindButton_Clicked(object sender, EventArgs e)
        {
            Move_player();
            Move_player_Hit_Box();
            Move_player_Camera_Box();


        }

        private void MoveBTN_Clicked(object sender, EventArgs e)
        {
            if (CurrentPlayerPositionY >= -290) {
                Move_player();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }

            else
            {
                // updates the positions, to move the world to simulate moving through expanded world
                if (gamelevelflag == 1)
                BackgroundCurrentPositionY = BackgroundCurrentPositionY + 15;
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
            CurrentPlayerPositionY = CurrentPlayerPositionY - 115;
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
                await e001.TranslateTo(ei1curposX, ei1curposY, (uint)rtime);
                await Task.Delay(2000);
            }
        }
        private void AttackBTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped == 0)
            {
                if (ammunition01 != 0)
                {
                    if (projectilecycle01 <= 10)
                    {
                        projectilecycle01++;
                    }
                    else if (projectilecycle01 == 11)
                    {
                        projectilecycle01 = 1;
                    }
                    bullet_animation01();

                }
            }
        }
        async void bullet_animation01()
        {
            switch (projectilecycle01)
            {
                case 1:
                    await Projectile01.FadeTo(1, 1);
                    activeprojectilepositioni1X = CurrentPlayerPositionX;
                    activeprojectilepositioni1Y = CurrentPlayerPositionY;
                    await Projectile01.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int i = 0; i < 60; i++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni1Y = activeprojectilepositioni1Y - 8;
                        await Projectile01.TranslateTo(activeprojectilepositioni1X, activeprojectilepositioni1Y, 1);
                    }
                    await Projectile01.FadeTo(0, 40);
                    activeprojectilepositioni1X = activeprojectilepositioni1X + 1000;
                    break;
                case 2:
                    await Projectile02.FadeTo(1, 1);
                    activeprojectilepositioni2X = CurrentPlayerPositionX;
                    activeprojectilepositioni2Y = CurrentPlayerPositionY;
                    await Projectile02.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int h = 0; h < 60; h++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni1Y = activeprojectilepositioni1Y - 8;
                        await Projectile02.TranslateTo(activeprojectilepositioni2X, activeprojectilepositioni2Y, 1);
                    }
                    await Projectile02.FadeTo(0, 40);
                    activeprojectilepositioni2X = activeprojectilepositioni2X+1000;
                    break;
                case 3:
                    await Projectile03.FadeTo(1, 1);
                    activeprojectilepositioni3X = CurrentPlayerPositionX;
                    activeprojectilepositioni3Y = CurrentPlayerPositionY;
                    await Projectile03.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int g = 0; g < 60; g++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni3Y = activeprojectilepositioni3Y - 8;
                        await Projectile03.TranslateTo(activeprojectilepositioni3X, activeprojectilepositioni3Y, 1);
                    }
                    await Projectile03.FadeTo(0, 40);
                    activeprojectilepositioni3X = activeprojectilepositioni3X + 1000;
                    break;
                case 4:
                    await Projectile04.FadeTo(1, 1);
                    activeprojectilepositioni4X = CurrentPlayerPositionX;
                    activeprojectilepositioni4Y = CurrentPlayerPositionY;
                    await Projectile04.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int b = 0; b < 60; b++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni4Y = activeprojectilepositioni4Y - 8;
                        await Projectile04.TranslateTo(activeprojectilepositioni4X, activeprojectilepositioni4Y, 1);
                    }
                    await Projectile04.FadeTo(0, 40);
                    activeprojectilepositioni4X = activeprojectilepositioni4X + 1000;
                    break;
                case 5:
                    await Projectile05.FadeTo(1, 1);
                    activeprojectilepositioni5X = CurrentPlayerPositionX;
                    activeprojectilepositioni5Y = CurrentPlayerPositionY;
                    await Projectile05.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int z = 0; z < 60; z++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni5Y = activeprojectilepositioni5Y - 8;
                        await Projectile05.TranslateTo(activeprojectilepositioni5X, activeprojectilepositioni5Y, 1);
                    }
                    await Projectile05.FadeTo(0, 40);
                    activeprojectilepositioni5X = activeprojectilepositioni5X + 1000;
                    break;
                case 6:
                    await Projectile06.FadeTo(1, 1);
                    activeprojectilepositioni6X = CurrentPlayerPositionX;
                    activeprojectilepositioni6Y = CurrentPlayerPositionY;
                    await Projectile06.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int x = 0; x < 60; x++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni6Y = activeprojectilepositioni6Y - 8;
                        await Projectile06.TranslateTo(activeprojectilepositioni6X, activeprojectilepositioni6Y, 1);
                    }
                    await Projectile06.FadeTo(0, 40);
                    activeprojectilepositioni6X = activeprojectilepositioni6X + 1000;
                    break;
                case 7:
                    await Projectile07.FadeTo(1, 1);
                    activeprojectilepositioni7X = CurrentPlayerPositionX;
                    activeprojectilepositioni7Y = CurrentPlayerPositionY;
                    await Projectile07.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int v = 0; v < 60; v++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni7Y = activeprojectilepositioni7Y - 8;
                        await Projectile07.TranslateTo(activeprojectilepositioni7X, activeprojectilepositioni7Y, 1);
                    }
                    await Projectile07.FadeTo(0, 40);
                    activeprojectilepositioni7X = activeprojectilepositioni7X + 1000;
                    break;
                case 8:
                    await Projectile08.FadeTo(1, 1);
                    activeprojectilepositioni8X = CurrentPlayerPositionX;
                    activeprojectilepositioni8Y = CurrentPlayerPositionY;
                    await Projectile08.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int q = 0; q < 60; q++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni8Y = activeprojectilepositioni8Y - 8;
                        await Projectile08.TranslateTo(activeprojectilepositioni8X, activeprojectilepositioni8Y, 1);
                    }
                    await Projectile08.FadeTo(0, 40);
                    activeprojectilepositioni8X = activeprojectilepositioni8X + 1000;
                    break;
                case 9:
                    await Projectile09.FadeTo(1, 1);
                    activeprojectilepositioni9X = CurrentPlayerPositionX;
                    activeprojectilepositioni9Y = CurrentPlayerPositionY;
                    await Projectile09.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int t = 0; t < 60; t++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni9Y = activeprojectilepositioni9Y - 8;
                        await Projectile09.TranslateTo(activeprojectilepositioni9X, activeprojectilepositioni9Y, 1);
                    }
                    await Projectile09.FadeTo(0, 40);
                    activeprojectilepositioni9X = activeprojectilepositioni9X + 1000;
                    break;
                case 10:
                    await Projectile10.FadeTo(1, 1);
                    activeprojectilepositioni10X = CurrentPlayerPositionX;
                    activeprojectilepositioni10Y = CurrentPlayerPositionY;
                    await Projectile10.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int j = 0; j < 60; j++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni10Y = activeprojectilepositioni10Y - 8;
                        await Projectile10.TranslateTo(activeprojectilepositioni10X, activeprojectilepositioni10Y, 1);
                    }
                    await Projectile10.FadeTo(0, 40);
                    activeprojectilepositioni10X = activeprojectilepositioni10X + 1000;
                    break;
                case 11:
                    await Projectile11.FadeTo(1, 1);
                    activeprojectilepositioni11X = CurrentPlayerPositionX;
                    activeprojectilepositioni11Y = CurrentPlayerPositionY;
                    await Projectile11.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    for (int k = 0; k < 60; k++)
                    {
                        //activeprojectilepositionX= activeprojectilepositionX + 5;
                        activeprojectilepositioni11Y = activeprojectilepositioni11Y - 8;
                        await Projectile11.TranslateTo(activeprojectilepositioni11X, activeprojectilepositioni11Y, 1);
                    }
                    await Projectile11.FadeTo(0, 40);
                    activeprojectilepositioni11X = activeprojectilepositioni11X + 1000;
                    break;
            }
        }
        // resets object positions
        async void Reset_All_Enemy_Position()
        {
            await e001.TranslateTo(0, 0, 40);
            await e002.TranslateTo(0, 0, 40);
            await e003.TranslateTo(0, 0, 40);
            await e004.TranslateTo(0, 0, 40);
            await e005.TranslateTo(0, 0, 40);
            await e006.TranslateTo(0, 0, 40);
            await e007.TranslateTo(0, 0, 40);
            await e008.TranslateTo(0, 0, 40);
        }
        // main menu buttons
        private void NGameBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
            newgameMenuReturnAnim();
        }
        private void ConBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
            ContinueMenuReturnAnim();
        }
        private void TrainBTN_Clicked(object sender, EventArgs e)
        {
            Training_ClickedAnim();
            TestingGMenuReturnAnim();
            this.Resources["ColourOfTrainingBTNClicked"] = Colors.White;
        }
        private void MissionBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
            MissionMenuReturnAnim();
        }
        private void SShopBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
        }
        private void BrutaBTN_Clicked(object sender, EventArgs e)
        {
            Brutal_ClickedAnim();
        }
        private void ChallBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
        }
        private void MusicBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
        }
        private void SettingsBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuRetreatAnim();
        }
        // new game menu buttons
        private void EasyDBTN_Clicked(object sender, EventArgs e)
        {
            Easy_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.White;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
        }
        private void NormDBTN_Clicked(object sender, EventArgs e)
        {
            Normal_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.White;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
        }
        private void HardDBTN_Clicked(object sender, EventArgs e)
        {
            Hard_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.White;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
        }
        private void VHardDBTN_Clicked(object sender, EventArgs e)
        {
            VeryHard_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.White;
        }
        private void PrevMissBTN_Clicked(object sender, EventArgs e)
        {

        }
        private void NextMissBTN_Clicked(object sender, EventArgs e)
        {

        }
        private void MissStatsBTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Accept01BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Accept02BTN_Clicked(object sender, EventArgs e)
        {
            
        }
        private void Accept03BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Accept04BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Save1BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Save2BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Save3BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void DelSaveBTN_Clicked(object sender, EventArgs e)
        {

        }
        private void EscapeBTN_Clicked(object sender, EventArgs e)
        {
            MainMenuReturnAnim();
            newgameMenuRetreatAnim();
            ContinueMenuRetreatAnim();
            TestingGMenuRetreatAnim();
            MissionMenuRetreatAnim();
            SuperShopMenuRetreatAnim();
            ChallengeMenuRetreatAnim();
            MusicMenuRetreatAnim();
            SettingsMenuRetreatAnim();
            LevelStatisticsMenuRetreatAnim();
            ResetButtonColours();
            ResetAll_Button_States_Anim();
        }
        private void ResetButtonColours()
        {
            // main
            this.Resources["ColourOfNewGameBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfContinueBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfTrainingBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMissionBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSuperShopBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfBrutalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfChallengeBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusicBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSettingsBTNClicked"] = Colors.DarkSlateGrey;
            // new
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
            // con
            this.Resources["ColourOfSave1BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.DarkSlateGrey;
            // miss

        }
        // menu animations
        // button anims
        async void Training_ClickedAnim()
        {
            await Trainingbutton.ScaleTo(1.3, 100);
        }
        async void Brutal_ClickedAnim()
        {
            await Brutalbutton.ScaleTo(1.3, 100);
        }
        async void Easy_ClickedAnim()
        {
            await easydiffbutton.ScaleTo(0.8, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
        }
        async void Normal_ClickedAnim()
        {
            await normaldiffbutton.ScaleTo(0.8, 100);
            await easydiffbutton.ScaleTo(0.6, 100);            
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
        }
        async void Hard_ClickedAnim()
        {
            await harddiffbutton.ScaleTo(0.8, 100);
            await easydiffbutton.ScaleTo(0.6, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
        }
        async void VeryHard_ClickedAnim()
        {
            await veryharddiffbutton.ScaleTo(0.8, 100);
            await easydiffbutton.ScaleTo(0.6, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await harddiffbutton.ScaleTo(0.6, 100);
            
        }
        async void ResetAll_Button_States_Anim()
        {
            await Trainingbutton.ScaleTo(1, 100);
            await easydiffbutton.ScaleTo(0.6, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);

        }
        // page turning
        // main menu
        private void MainMenuRetreatAnim() // seperated between multiples to all move in sync at once
        {
            SeperatedMenuRetreat01();
            SeperatedMenuRetreat02();
            SeperatedMenuRetreat03();
            SeperatedMenuRetreat04();
            SeperatedMenuRetreat05();
            SeperatedMenuRetreat06();
            SeperatedMenuRetreat07();
            SeperatedMenuRetreat08();
            SeperatedMenuRetreat09();
            SeperatedMenuRetreat10();
            SeperatedMenuRetreat18();
        }
        async void SeperatedMenuRetreat01()
        {
            await NewGamebutton.TranslateTo(-375, 1185, 500);
        }
        async void SeperatedMenuRetreat02()
        {
            await Continuebutton.TranslateTo(-250, 1187, 500);
        }
        async void SeperatedMenuRetreat03()
        {
            await Trainingbutton.TranslateTo(-125, 1189, 500);
        }
        async void SeperatedMenuRetreat04()
        {
            await Missionbutton.TranslateTo(0, 1191, 500);
        }
        async void SeperatedMenuRetreat05()
        {
            await SuperShopbutton.TranslateTo(125, 1193, 500);
        }
        async void SeperatedMenuRetreat06()
        {
            await Brutalbutton.TranslateTo(250, 1195, 500);
        }
        async void SeperatedMenuRetreat07()
        {
            await Challengebutton.TranslateTo(375, 1197, 500);
        }
        async void SeperatedMenuRetreat08()
        {
            await Musicbutton.TranslateTo(400, (-185 + 1000), 500);
        }
        async void SeperatedMenuRetreat09()
        {
            await Settingsbutton.TranslateTo(400, (-140 + 1000), 500);
        }
        async void SeperatedMenuRetreat10()
        {
            await TitleScreen01.TranslateTo(0, 1000, 500);
        }
        async void SeperatedMenuRetreat18()
        {
            await BattleForAzuraTitle.TranslateTo(-50, 1000, 500);
        }
        private void MainMenuReturnAnim() // seperated between multiples to all move in sync at once
        {
            SeperatedMenuReturn01();
            SeperatedMenuReturn02();
            SeperatedMenuReturn03();
            SeperatedMenuReturn04();
            SeperatedMenuReturn05();
            SeperatedMenuReturn06();
            SeperatedMenuReturn07();
            SeperatedMenuReturn08();
            SeperatedMenuReturn09();
            SeperatedMenuReturn10();
            SeperatedMenuReturn18();
        }
        async void SeperatedMenuReturn01()
        {
            await NewGamebutton.TranslateTo(-375, 185, 500);
        }
        async void SeperatedMenuReturn02()
        {
            await Continuebutton.TranslateTo(-250, 187, 500);
        }
        async void SeperatedMenuReturn03()
        {
            await Trainingbutton.TranslateTo(-125, 189, 500);
        }
        async void SeperatedMenuReturn04()
        {
            await Missionbutton.TranslateTo(0, 191, 500);
        }
        async void SeperatedMenuReturn05()
        {
            await SuperShopbutton.TranslateTo(125, 193, 500);
        }
        async void SeperatedMenuReturn06()
        {
            await Brutalbutton.TranslateTo(250, 195, 500);
        }
        async void SeperatedMenuReturn07()
        {
            await Challengebutton.TranslateTo(375, 197, 500);
        }
        async void SeperatedMenuReturn08()
        {
            await Musicbutton.TranslateTo(400, -185, 500);
        }
        async void SeperatedMenuReturn09()
        {
            await Settingsbutton.TranslateTo(400, -140, 500);
        }
        async void SeperatedMenuReturn10()
        {
            await TitleScreen01.TranslateTo(0, 0, 500);
        }
        async void SeperatedMenuReturn18()
        {
            await BattleForAzuraTitle.TranslateTo(-50, 0, 500);
        }
        // new game menu
        private void newgameMenuRetreatAnim() // seperated between multiples to all move in sync at once
        {
            SeperatedMenuRetreat11();
            SeperatedMenuRetreat12();
            SeperatedMenuRetreat13();
            SeperatedMenuRetreat14();
            SeperatedMenuRetreat15();
            SeperatedMenuRetreat16();
            SeperatedMenuRetreat17();
        }
        async void SeperatedMenuRetreat11()
        {
            await easydiffbutton.TranslateTo(-280, -1050, 500);

        }
        async void SeperatedMenuRetreat12()
        {
            await normaldiffbutton.TranslateTo(-280, -1020, 500);

        }
        async void SeperatedMenuRetreat13()
        {
            await harddiffbutton.TranslateTo(-280, -990, 500);

        }
        async void SeperatedMenuRetreat14()
        {
            await veryharddiffbutton.TranslateTo(-280, -960, 500);

        }
        async void SeperatedMenuRetreat15()
        {
            await accept01button.TranslateTo(125, (195-1000), 500);

        }
        async void SeperatedMenuRetreat16()
        {
            await leavebutton.TranslateTo(250, (195-1000), 500);
        }
        async void SeperatedMenuRetreat17()
        {
            await NewGameScreen01.TranslateTo(0,-1000, 500);
        }
        private void newgameMenuReturnAnim() // seperated between multiples to all move in sync at once
        {
            SeperatedMenuReturn11();
            SeperatedMenuReturn12();
            SeperatedMenuReturn13();
            SeperatedMenuReturn14();
            SeperatedMenuReturn15();
            SeperatedMenuReturn16();
            SeperatedMenuReturn17();
        }
        async void SeperatedMenuReturn11()
        {
            await easydiffbutton.TranslateTo(-280, -50, 500);
            
        }
        async void SeperatedMenuReturn12()
        {
            await normaldiffbutton.TranslateTo(-280, -20, 500);
            
        }
        async void SeperatedMenuReturn13()
        {
            await harddiffbutton.TranslateTo(-280, 10, 500);
            
        }
        async void SeperatedMenuReturn14()
        {
            await veryharddiffbutton.TranslateTo(-280, 40, 500);
            
        }
        async void SeperatedMenuReturn15()
        {
            await accept01button.TranslateTo(125, 195, 500);
            
        }
        async void SeperatedMenuReturn16()
        {
            await leavebutton.TranslateTo(250, 195, 500);
        }
        async void SeperatedMenuReturn17()
        {
            await NewGameScreen01.TranslateTo(0, 0, 500);
        }
        // continue menu
        private void ContinueMenuRetreatAnim()
        {
            SeperatedMenuRereat19();
            SeperatedMenuRereat20();
            SeperatedMenuRereat21();
            SeperatedMenuRereat22();
            SeperatedMenuRereat23();
            SeperatedMenuRereat24();
            SeperatedMenuRereat25();
        }
        async void SeperatedMenuRereat19()
        {
            await saveslot1button.TranslateTo(-325, -1050, 500);
            

        }
        async void SeperatedMenuRereat20()
        {
            await saveslot2button.TranslateTo(-325, -1000, 500);
            
        }
        async void SeperatedMenuRereat21()
        {
            await saveslot3button.TranslateTo(-325, -950, 500);
           
        }
        async void SeperatedMenuRereat22()
        {
            await deletesavebutton.TranslateTo(0, (195 - 1000), 500);
            
        }
        async void SeperatedMenuRereat23()
        {
            await accept02button.TranslateTo(125, (195 - 1000), 500);
            
        }
        async void SeperatedMenuRereat24()
        {
            await leave02button.TranslateTo(250, (195 - 1000), 500); 
        }
        async void SeperatedMenuRereat25()
        {
            await ContinueScreen01.TranslateTo(0, -1000, 500);
        }
        private void ContinueMenuReturnAnim()
        {
            SeperatedMenuReturn19();
            SeperatedMenuReturn20();
            SeperatedMenuReturn21();
            SeperatedMenuReturn22();
            SeperatedMenuReturn23();
            SeperatedMenuReturn24();
            SeperatedMenuReturn25();
        }
        async void SeperatedMenuReturn19()
        {
            await saveslot1button.TranslateTo(-325, -50, 500);
            
        }
        async void SeperatedMenuReturn20()
        {
            await saveslot2button.TranslateTo(-325, 0, 500);
            
        }
        async void SeperatedMenuReturn21()
        {
            await saveslot3button.TranslateTo(-325, 50, 500);
            
        }
        async void SeperatedMenuReturn22()
        {
            await deletesavebutton.TranslateTo(0, 195, 500);
            
        }
        async void SeperatedMenuReturn23()
        {
            await accept02button.TranslateTo(125, 195, 500);
            
        }
        async void SeperatedMenuReturn24()
        {
            await leave02button.TranslateTo(250, 195, 500); 
        }
        async void SeperatedMenuReturn25()
        {
            await ContinueScreen01.TranslateTo(0, 0, 500);
        }
        private void TestingGMenuRetreatAnim()
        {
            SeperatedMenuRetreat32();
            SeperatedMenuRetreat33();
            SeperatedMenuRetreat34();
            SeperatedMenuRetreat35();
            SeperatedMenuRetreat36();
        }
        async void SeperatedMenuRetreat32()
        {
            await GrayFilterScreen01.FadeTo(0, 5);
        }
        async void SeperatedMenuRetreat33()
        {
            await accept03button.TranslateTo(-75, (40 - 1000), 5);

        }
        async void SeperatedMenuRetreat34()
        {
            await leave03button.TranslateTo(75,(40-1000), 5);
        }
        async void SeperatedMenuRetreat35()
        {

            await accept03button.FadeTo(0, 5);
        }
        async void SeperatedMenuRetreat36()
        {

            await leave03button.FadeTo(0, 5);
        }
        private void TestingGMenuReturnAnim()
        {
            SeperatedMenuReturn32();
            SeperatedMenuReturn33();
            SeperatedMenuReturn34();
            SeperatedMenuReturn35();
            SeperatedMenuReturn36();
        }
        async void SeperatedMenuReturn32()
        {
            
            await GrayFilterScreen01.FadeTo(0.5, 300);
        }
        async void SeperatedMenuReturn33()
        {
            await accept03button.TranslateTo(-75, 40, 5);
            
        }
        async void SeperatedMenuReturn34()
        {
            await leave03button.TranslateTo(75, 40, 5);
        }
        async void SeperatedMenuReturn35()
        {

            await accept03button.FadeTo(1, 300);
        }
        async void SeperatedMenuReturn36()
        {

            await leave03button.FadeTo(1, 300);
        }
        private void MissionMenuRetreatAnim()
        {
            SeperatedMenuRetreat26();
            SeperatedMenuRetreat27();
            SeperatedMenuRetreat28();
            SeperatedMenuRetreat29();
            SeperatedMenuRetreat30();
            SeperatedMenuRetreat31();
        }
        async void SeperatedMenuRetreat26()
        {
            await previousmissionbutton.TranslateTo(-445, (30 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat27()
        {
            await nextmissionbutton.TranslateTo(445, (30 - 1000), 500);
            

        }
        async void SeperatedMenuRetreat28()
        {
            await missionstatsbutton.TranslateTo(0, (195 - 1000), 500);
            

        }
        async void SeperatedMenuRetreat29()
        {
            await accept04button.TranslateTo(125, (195 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat30()
        {
            await leave04button.TranslateTo(250, (195 - 1000), 500);
        }
        async void SeperatedMenuRetreat31()
        {
            await MissionScreen01.TranslateTo(0, -1000, 500);
        }
        private void MissionMenuReturnAnim()
        {
            SeperatedMenuReturn26();
            SeperatedMenuReturn27();
            SeperatedMenuReturn28();
            SeperatedMenuReturn29();
            SeperatedMenuReturn30();
            SeperatedMenuReturn31();
        }
        async void SeperatedMenuReturn26()
        {
            await previousmissionbutton.TranslateTo(-445, 30, 500);

        }
        async void SeperatedMenuReturn27()
        {
            await nextmissionbutton.TranslateTo(445, 30, 500);


        }
        async void SeperatedMenuReturn28()
        {
            await missionstatsbutton.TranslateTo(0, 195, 500);
        }
        async void SeperatedMenuReturn29()
        {
            await accept04button.TranslateTo(125, 195, 500);

        }
        async void SeperatedMenuReturn30()
        {
            await leave04button.TranslateTo(250, 195 , 500);
        }
        async void SeperatedMenuReturn31()
        {
            await MissionScreen01.TranslateTo(0, 0, 500);
        }
        private void SuperShopMenuRetreatAnim()
        {

        }
        private void SuperShopMenuReturnAnim()
        {

        }
        private void ChallengeMenuRetreatAnim()
        {

        }
        private void ChallengeMenuReturnAnim()
        {

        }
        private void MusicMenuRetreatAnim()
        {

        }
        private void MusicMenuReturnAnim()
        {

        }
        private void SettingsMenuRetreatAnim()
        {

        }
        private void SettingsMenuReturnAnim()
        {

        }
        // stats screens
        private void LevelStatisticsRetreatAnim()
        {

        }
        private void LevelStatisticsReturnAnim()
        {

        }
        private void LevelStatisticsMenuRetreatAnim()
        {

        }
        private void LevelStatisticsMenuReturnAnim()
        {

        }
        // death animations
        // player
        private void playerdeathanim()
        {
            playerdeathanim01();
            playerdeathanim02();
            playerdeathanim03();
        }
        async void playerdeathanim01()
        {
            await PlayerIMG.RotateTo(720, 500);
        }
        async void playerdeathanim02()
        {
            await PlayerIMG.FadeTo(0, 500);
        }
        async void playerdeathanim03()
        {
            await PlayerIMG.ScaleTo(1.4, 200);
            await PlayerIMG.ScaleTo(0.6, 300);
        }
        // enemies
        private void ei1deathanim()
        {
            e001deathanim01();
            e001deathanim02();
            e001deathanim03();
        }
        async void e001deathanim01()
        {
            await e001.RotateTo(720, 300);
        }
        async void e001deathanim02()
        {
            await e001.FadeTo(0, 300);
        }
        async void e001deathanim03()
        {
            await e001.ScaleTo(0.6, 300);
        }
        // destructables

        // constantly updates the positions of every game object ( except for player )
       async void Update_All_Position_Constant()
        {
            await Task.Delay(200);
            if (gamestatus != 0)
            {
                while (gamestatus != 0) // some unknown problematic bug found concerning this
                {
                    Update_backgrounds();
                    Update_enemys01();
                    Update_enemys02();
                    Update_enemys03();
                    Update_enemys04();
                    Update_enemys05();
                    Update_enemys06();
                    Update_enemys07();
                    Update_enemys08();
                    Update_enemys09();
                    Update_enemys10();
                }
            }
        }
        async void Update_backgrounds()
        {
            if (gamelevelflag == 1)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if(gamelevelflag == 2)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if (gamelevelflag == 3)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if (gamelevelflag == 4)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if (gamelevelflag == 5)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if (gamelevelflag == 6)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if (gamelevelflag == 7)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }
            else if (gamelevelflag == 8)
            {
                await BackgroundIMG.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
            }

        }
        
        async void Update_enemys01()
        {
                await e001.TranslateTo(ei1curposX, ei1curposY, 40);
                await e002.TranslateTo(ei2curposX, ei2curposY, 40);
                await e003.TranslateTo(ei3curposX, ei3curposY, 40);
                await e004.TranslateTo(ei4curposX, ei4curposY, 40);
                await e005.TranslateTo(ei5curposX, ei5curposY, 40);
                await e006.TranslateTo(ei6curposX, ei6curposY, 40);
                await e007.TranslateTo(ei7curposX, ei7curposY, 40);
                await e008.TranslateTo(ei8curposX, ei8curposY, 40);
        }
        async void Update_enemys02()
        {

        }
        async void Update_enemys03()
        {

        }
        async void Update_enemys04()
        {

        }
        async void Update_enemys05()
        {

        }
        async void Update_enemys06()
        {

        }
        async void Update_enemys07()
        {

        }
        async void Update_enemys08()
        {

        }
        async void Update_enemys09()
        {

        }
        async void Update_enemys10()
        {

        }
        
    }// end of all
}// end of all
