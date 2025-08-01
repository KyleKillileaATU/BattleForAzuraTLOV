
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace BattleForAzuraTLOV
{
    public partial class MainPage : ContentPage
    {
        int CurrentPlayerPositionX = 0, CurrentPlayerPositionY = 0;
        int BackgroundCurrentPositionX = 0, BackgroundCurrentPositionY = 0;
        int RandomPositionX = 0, RandomPositionY = 0, rtime, weaponequipped = 0;
        // enemys slug
        int[] enemyinstancecurpos01x = { 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 1 - 8
        int[] enemyinstancecurpos02x = { 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 9 - 16
        int[] enemyinstancecurpos01y = { 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 1 - 8
        int[] enemyinstancecurpos02y = { 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 9 - 16
        // collissions slug
        // 0 = botleft, 1 = botright ei1 x's / same for y's (there is no need for top-down collissions) 
        int[] enemyinstancehitbox01x = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 }; // ei 1 - 8
        int[] enemyinstancehitbox02x = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 9 - 16
        int[] enemyinstancehitbox01y = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 1 - 8
        int[] enemyinstancehitbox02y = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ei 9 - 16
        int playercollisiontopleftX, playercollisiontoprightX, playercollisionbotleftX, playercollisionbotrightX;
        int playercollisiontopleftY, playercollisiontoprightY, playercollisionbotleftY, playercollisionbotrightY;
        // projectile positions slug
        int[] activeprojectileposition01x = { 0, 0, 0, 0, 5, 0, 0, 0, 0, 10, 0, 0, 0, 0, 15, 0, 0, 0, 0, 20, 0, 0 }; // gun 1
        int[] activeprojectileposition02x = { 0, 0, 0, 0 }; // gun 2
        int[] activeprojectileposition03x = { 0, 0, 0, 0 }; // gun 3
        int[] activeprojectileposition04x = { 0, 0, 0, 0 }; // gun 4
        int[] activeprojectileposition05x = { 0, 0, 0, 0 }; // gun 5
        int[] activeprojectileposition06x = { 0, 0, 0, 0 }; // gun 6
        int[] activeprojectileposition01y = { 0, 0, 0, 0, 5, 0, 0, 0, 0, 10, 0, 0, 0, 0, 15, 0, 0, 0, 0, 20, 0, 0 }; // gun 1
        int[] activeprojectileposition02y = { 0, 0, 0, 0 }; // gun 2
        int[] activeprojectileposition03y = { 0, 0, 0, 0 }; // gun 3
        int[] activeprojectileposition04y = { 0, 0, 0, 0 }; // gun 4
        int[] activeprojectileposition05y = { 0, 0, 0, 0 }; // gun 5
        int[] activeprojectileposition06y = { 0, 0, 0, 0 }; // gun 6

        int ammunition01 = 0, ammunition02 = 0, ammunition03 = 0, ammunition04 = 0, ammunition05 = 0, ammunition06 = 0, ammunitioncurrent=0;
        int projectilecycle01 = 0, projectilecycle02 = 0, projectilecycle03 = 0, projectilecycle04 = 0, projectilecycle05 = 0, projectilecycle06 = 0;
        int gamelevelflag=0, gamestatus=0, areascreenlock=0;
        int newgamedifficulty=0, difficultysetting=0, saveselected = 0, save01exist = 0, save02exist = 0, save03exist = 0;
        int playermoveamount = 0;
        int weaponmenuedswitch = 0, weaponowned01 = 1, weaponowned02 = 0, weaponowned03 = 0, weaponowned04 = 0, weaponowned05 = 0, weaponowned06 = 0;
        Random RNGmove = new Random();


        public MainPage()
        {
            new KeyboardAccelerator { Key = "X" };
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            setupallgamemenu();
            setupallgameprojectiles();
            setupallgameobjects();
            setupallgameui();
            hideallgamecontent();
            tutorialsetup();
            setupallenemyoffsets();
            //testcontent1();
            //Infinite_RNG_Movement();
            //Update_All_Position_Constant(); // to be activated on or off game start and end 
            playermoveamount = 20;
        }
        async void setupallgameobjects()
        {
            await BackgroundLevel01.TranslateTo(-4000, -1150, 4);
            await BackgroundLevel02.TranslateTo(-4000, 0, 4);
            await BackgroundLevel03.TranslateTo(-4000, 0, 4);
            await BackgroundLevel04.TranslateTo(-4000, 0, 4);
            await BackgroundLevel05.TranslateTo(-4000, 0, 4);
            await BackgroundLevel06.TranslateTo(-4000, 0, 4);
            await BackgroundLevel07.TranslateTo(-4000, 0, 4);
            await BackgroundLevel08.TranslateTo(-4000, 0, 4);
            await BackgroundLevel01.ScaleTo(8, 4);
            await BackgroundLevel02.ScaleTo(8, 4);
            await BackgroundLevel03.ScaleTo(8, 4);
            await BackgroundLevel04.ScaleTo(8, 4);
            await BackgroundLevel05.ScaleTo(8, 4);
            await BackgroundLevel06.ScaleTo(8, 4);
            await BackgroundLevel07.ScaleTo(8, 4);
            await BackgroundLevel08.ScaleTo(8, 4);
        }
        async void setupallgameui()
        {
            await PlayerHPbar.TranslateTo(-98, 0, 4);
            await PlayerStaminabar.TranslateTo(-93, 0, 4);
            await PlayerMagicbar.TranslateTo(-102, 0, 4);
            await backmovebutton.TranslateTo(50, 145, 4);
            await leftmovebutton.TranslateTo(0, 50, 4);
            await rightmovebutton.TranslateTo(105, 0, 4);
            await forwardmovebutton.TranslateTo(50, -95, 4);
            await attackbutton.TranslateTo(-40, -10, 4);
            await backgroundweaponmenu01.TranslateTo(1475, 0, 4);
            await backgroundweaponmenu02.TranslateTo(1475, -125, 4);
            await backgroundweaponmenu03.TranslateTo(1395, -40, 4);
            await backgroundweaponmenu04.TranslateTo(1395, 45, 4);
            await backgroundweaponmenu05.TranslateTo(1395, 130, 4);
            await backgroundweaponmenu06.TranslateTo(1555, -40, 4);
            await backgroundweaponmenu07.TranslateTo(1555, 45, 4);
            await backgroundweaponmenu08.TranslateTo(1555, 130, 4);
            await weaponmenu01.TranslateTo(1355, -40, 4);
            await weaponmenu02.TranslateTo(1355, 45, 4);
            await weaponmenu03.TranslateTo(1355, 130, 4);
            await weaponmenu04.TranslateTo(1515, -40, 4);
            await weaponmenu05.TranslateTo(1515, 45, 4);
            await weaponmenu06.TranslateTo(1515, 130, 4);
            await gamemenubutton.TranslateTo(0, 0, 4);
            await weaponswitchbutton.TranslateTo(0, 0, 4);
            await backgroundammotext.TranslateTo(-110, -95, 4);
            await ammoqtext.TranslateTo(-113, -135, 4);
            await attackbutton.ScaleTo(1.7, 4);
            await weaponswitchbutton.ScaleTo(0.8, 4);
            await backgroundammotext.ScaleTo(1.3, 4);
            await backgroundweaponmenu01.ScaleTo(4.5, 4);

            this.Resources["MagicBarValue"] = 140;
            this.Resources["HealthBarValue"] = 140;
            this.Resources["StaminaBarValue"] = 140;
            this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.LightBlue;
            this.Resources["ColourOfRightMoveBTNClicked"] = Colors.LightBlue;
            this.Resources["ColourOfLeftMoveBTNClicked"] = Colors.LightBlue;
            this.Resources["ColourOfBackMoveBTNClicked"] = Colors.LightBlue;
            this.Resources["ColourOfAttackBTNClicked"] = Colors.Red;
            this.Resources["ColourOfGameMenuBTNClicked"] = Colors.Purple;
            this.Resources["ColourOfWeaponSwitchBTNClicked"] = Colors.Yellow;
            this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateGray;
        }
        private void hideallgamecontent() // hides all content game assets (enemies, objects, etc )
        {
            hideplayer();
            hidebackground();
            hideenemyinstances01();
            hideprojectileinstances();
            hideplayerui();
            
        }
        
        async void hideplayer()
        {
            await PlayerIMG.FadeTo(0, 5);
            await PlayerHitBox.FadeTo(0, 5);
            await PlayerCameraBox.FadeTo(0, 5);
        }
        async void hidebackground()
        {
            await BackgroundIMG.FadeTo(0, 5);
            await BackgroundLevel01.FadeTo(0, 5);
            await BackgroundLevel02.FadeTo(0, 5);
            await BackgroundLevel03.FadeTo(0, 5);
            await BackgroundLevel04.FadeTo(0, 5);
            await BackgroundLevel05.FadeTo(0, 5);
            await BackgroundLevel06.FadeTo(0, 5);
            await BackgroundLevel07.FadeTo(0, 5);
            await BackgroundLevel08.FadeTo(0, 5);
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
            await Projectile12.FadeTo(0, 4);
            await Projectile13.FadeTo(0, 4);
            await Projectile14.FadeTo(0, 4);
            await Projectile15.FadeTo(0, 4);
            await Projectile16.FadeTo(0, 4);
            await Projectile17.FadeTo(0, 4);
            await Projectile18.FadeTo(0, 4);
            await Projectile19.FadeTo(0, 4);
            await Projectile20.FadeTo(0, 4);
            await Projectile21.FadeTo(0, 4);
            await Projectile22.FadeTo(0, 4);
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
            await forwardmovebutton.FadeTo(0, 5);
            await rightmovebutton.FadeTo(0, 5);
            await backmovebutton.FadeTo(0, 5);
            await attackbutton.FadeTo(0, 5);
            await gamemenubutton.FadeTo(0, 4);
            await weaponswitchbutton.FadeTo(0, 4);
            await backgroundammotext.FadeTo(0, 4);
            await ammoqtext.FadeTo(0, 4);
        }
        private void showallgamecontent() // shows all content game assets (enemies, objects, etc )
        {
            showplayer();
            showbackground();
            showenemyinstances01();
            showprojectileinstances();
            showplayerui();

        }
        async void showplayer()
        {
            //await PlayerIMG.FadeTo(1, 5);
            await PlayerHitBox.FadeTo(1, 5);
            //await PlayerCameraBox.FadeTo(1, 5);
        }
        async void showbackground()
        {
            //await BackgroundIMG.FadeTo(1, 5);
            await BackgroundLevel01.FadeTo(1, 5);
        }
        async void showenemyinstances01()
        {
            await e001.FadeTo(1, 5);
            await e002.FadeTo(1, 5);
            await e003.FadeTo(1, 5);
            await e004.FadeTo(1, 5);
            await e005.FadeTo(1, 5);
            await e006.FadeTo(1, 5);
            await e007.FadeTo(1, 5);
            await e008.FadeTo(1, 5);
        }
        async void showprojectileinstances()
        {
            await Projectile01.FadeTo(1, 4);
            await Projectile02.FadeTo(1, 4);
            await Projectile03.FadeTo(1, 4);
            await Projectile04.FadeTo(1, 4);
            await Projectile05.FadeTo(1, 4);
            await Projectile06.FadeTo(1, 4);
            await Projectile07.FadeTo(1, 4);
            await Projectile08.FadeTo(1, 4);
            await Projectile09.FadeTo(1, 4);
            await Projectile10.FadeTo(1, 4);
            await Projectile11.FadeTo(1, 4);
            await Projectile12.FadeTo(1, 4);
            await Projectile13.FadeTo(1, 4);
            await Projectile14.FadeTo(1, 4);
            await Projectile15.FadeTo(1, 4);
            await Projectile16.FadeTo(1, 4);
            await Projectile17.FadeTo(1, 4);
            await Projectile18.FadeTo(1, 4);
            await Projectile19.FadeTo(1, 4);
            await Projectile20.FadeTo(1, 4);
            await Projectile21.FadeTo(1, 4);
            await Projectile22.FadeTo(1, 4);
        }
        async void showplayerui()
        {
            await PlayerMagicbase.FadeTo(1, 5);
            await PlayerMagicbar.FadeTo(1, 5);
            await PlayerHPbase.FadeTo(1, 5);
            await PlayerHPbar.FadeTo(1, 5);
            await PlayerStaminabase.FadeTo(1, 5);
            await PlayerStaminabar.FadeTo(1, 5);
            await leftmovebutton.FadeTo(1, 5);
            await forwardmovebutton.FadeTo(1, 5);
            await rightmovebutton.FadeTo(1, 5);
            await backmovebutton.FadeTo(1, 5);
            await attackbutton.FadeTo(1, 5);
            await gamemenubutton.FadeTo(1, 4);
            await weaponswitchbutton.FadeTo(1, 4);
            await backgroundammotext.FadeTo(1, 4);
            await ammoqtext.FadeTo(1, 4);
        }
        // menu set ups ( positionings and states )
        private void setupallgamemenu()
        {
            setuptitlescreen();
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
        async void setuptitlescreen()
        {
            await EnterGamebutton.FadeTo(1, 5);
            await EnterGamebutton.TranslateTo(0, 100, 5);
            await TitleScreen02.TranslateTo(0, -50, 5);
            //this.Resources["ColourOfStartGameBTNClicked"] = Colors.DarkSlateGrey;
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
        private void setupallgameprojectiles()
        {
            Reset_All_Projectile_Position();  
        }
        private void setupallenemyoffsets()
        {
            for (int ex= 0; ex < (enemyinstancecurpos01x.Length); ex++)
            {
                enemyinstancecurpos01x[ex] = enemyinstancecurpos01x[ex] + 1000;
            }
            for (int ex = 0; ex < (enemyinstancecurpos02x.Length); ex++)
            {
                enemyinstancecurpos02x[ex] = enemyinstancecurpos02x[ex] + 1000;
            }

        }
        private void tutorialsetup()
        {
            tutorialsetup_01();
            tutorialdynamictext.Text = $"PlaceHolder Text";
        }
        async void tutorialsetup_01()
        {
            await TutorialBox01.TranslateTo(0, -1000, 5);
            await tutorialdynamictext.TranslateTo(0, -1000, 5);
        }
        private  void Mainpage_Keydown()
        {
            
        }
        // button stuff
        private void Move_BindButton_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {
                Move_player();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }

        private void MoveBTN_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {
                this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.Navy;
                Move_player();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        private void Moveobjects_level01()
        {
            // for area level 1
            for (int change = 0; change < 8; change++)
            {
                enemyinstancecurpos01y[change] += playermoveamount;
                enemyinstancecurpos02y[change] += playermoveamount;
            }
        }
        private void Moveobjects_level02()
        {
            // for area level 2
        }
        async void Move_player()// split the 3 moving seperately so they all move at once together
        {
            CurrentPlayerPositionY = CurrentPlayerPositionY - playermoveamount;

            if (CurrentPlayerPositionY <= -220)
            {
                CurrentPlayerPositionY = -219;

                if (areascreenlock == 0)
                {
                    // updates the positions, to move the world to simulate moving through expanded world
                    if (gamelevelflag == 1) // the level
                    {
                        BackgroundCurrentPositionY = BackgroundCurrentPositionY + playermoveamount;
                        Moveobjects_level01();
                    }
                    else if (gamelevelflag == 2) // the level
                    {
                        BackgroundCurrentPositionY = BackgroundCurrentPositionY + playermoveamount;
                        Moveobjects_level02();
                    }
                }
            }
            await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40);
            this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerLeft()// split the 3 moving seperately so they all move at once together
        {
            CurrentPlayerPositionX = CurrentPlayerPositionX - playermoveamount;

            if (CurrentPlayerPositionX <= -440)
            {
                CurrentPlayerPositionX = -439;
            }
            await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40);
            this.Resources["ColourOfLeftMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerRight()// split the 3 moving seperately so they all move at once together
        {
            
            CurrentPlayerPositionX = CurrentPlayerPositionX + playermoveamount;

            if (CurrentPlayerPositionX >= 440)
            {
                CurrentPlayerPositionX = 439;
            }
            await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40);
            this.Resources["ColourOfRightMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerBack()// split the 3 moving seperately so they all move at once together
        {
            CurrentPlayerPositionY = CurrentPlayerPositionY + playermoveamount;

            if (CurrentPlayerPositionY >= 220)
            {
                CurrentPlayerPositionY = 219;
            }
            await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 40);
            this.Resources["ColourOfBackMoveBTNClicked"] = Colors.LightBlue;
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
        private void LeftMoveBTN_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {
                this.Resources["ColourOfLeftMoveBTNClicked"] = Colors.Navy;
                Move_playerLeft();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        private void RightMoveBTN_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {
                this.Resources["ColourOfRightMoveBTNClicked"] = Colors.Navy;
                Move_playerRight();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        private void BackMoveBTN_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {
                this.Resources["ColourOfBackMoveBTNClicked"] = Colors.Navy;
                Move_playerBack();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        async void Infinite_RNG_Movement() // testing purposes of enemy ai
        {
            while (true)
            {
                enemyinstancecurpos01x[0] = (enemyinstancecurpos01x[0] + RNGmove.Next(-30, 30));
                enemyinstancecurpos01y[0] = (enemyinstancecurpos01y[0] + RNGmove.Next(-30, 30));
                rtime = RNGmove.Next(150, 750);
                if (enemyinstancecurpos01x[0] >=445) 
                {
                    enemyinstancecurpos01x[0] = 445;
                }
                if (enemyinstancecurpos01x[0] <= -445)
                {
                    enemyinstancecurpos01x[0] = -445;
                }
                await e001.TranslateTo(enemyinstancecurpos01x[0], enemyinstancecurpos01x[0], (uint)rtime);
                await Task.Delay(2000);
            }
        }
        private void AttackBTN_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {
                if (ammunition01 != 0)
                {
                    this.Resources["ColourOfAttackBTNClicked"] = Colors.DarkRed;
                }
                else if (ammunition01 == 0)
                {
                    this.Resources["ColourOfAttackBTNClicked"] = Colors.Black;
                }
                Attacking();
                AttackButtonDisplay();
            }
        }
        async void AttackButtonDisplay()
        {
            await Task.Delay(80);
            this.Resources["ColourOfAttackBTNClicked"] = Colors.Red;
        }
        // player attacking
        private void Attacking()
        {
            if (weaponequipped == 0) // gun 1
            {
                if (ammunition01 != 0)
                {
                    if (projectilecycle01 <= 20)
                    {
                        projectilecycle01++;
                    }
                    else if (projectilecycle01 == 21)
                    {
                        projectilecycle01 = 1;
                    }
                    bullet_lifecycle01();

                }
            }
            if (weaponequipped == 1) // gun 2
            {
                if (ammunition02 != 0)
                {
                    if (projectilecycle02 <= 20)
                    {
                        projectilecycle02++;
                    }
                    else if (projectilecycle02 == 21)
                    {
                        projectilecycle02 = 1;
                    }
                    bullet_lifecycle02();

                }
            }
            if (weaponequipped == 2) // gun 3
            {
                if (ammunition03 != 0)
                {
                    if (projectilecycle03 <= 20)
                    {
                        projectilecycle03++;
                    }
                    else if (projectilecycle03 == 21)
                    {
                        projectilecycle03 = 1;
                    }
                    bullet_lifecycle03();

                }
            }
            if (weaponequipped == 3) // gun 4
            {
                if (ammunition04 != 0)
                {
                    if (projectilecycle04 <= 20)
                    {
                        projectilecycle04++;
                    }
                    else if (projectilecycle04 == 21)
                    {
                        projectilecycle04 = 1;
                    }
                    bullet_lifecycle04();

                }
            }
            if (weaponequipped == 4) // gun 5
            {
                if (ammunition05 != 0)
                {
                    if (projectilecycle05 <= 20)
                    {
                        projectilecycle05++;
                    }
                    else if (projectilecycle05 == 21)
                    {
                        projectilecycle05 = 1;
                    }
                    bullet_lifecycle05();

                }
            }
            if (weaponequipped == 5) // gun 6
            {
                if (ammunition06 != 0)
                {
                    if (projectilecycle06 <= 20)
                    {
                        projectilecycle06++;
                    }
                    else if (projectilecycle06 == 21)
                    {
                        projectilecycle06 = 1;
                    }
                    bullet_lifecycle06();

                }
            }
        }
        // animations for each gun ( cycling the bullet instances )
        async void bullet_lifecycle01()
        {
            switch (projectilecycle01)// projectile cylcle == the gun equipped, 1 is for gunequipped ' 0 ' and so on
            {
                case 1:
                    --ammunition01;

                    activeprojectileposition01x[0] = CurrentPlayerPositionX;
                    activeprojectileposition01y[0] = CurrentPlayerPositionY;
                    await Projectile01.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile01.FadeTo(1, 1);
                    for (int i = 0; i < 100; i++)
                    {
                        Projectile_collision(0);
                        if (activeprojectileposition01y[0] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[0] = activeprojectileposition01y[0] - 8;
                            await Projectile01.TranslateTo(activeprojectileposition01x[0], activeprojectileposition01y[0], 1);
                        }
                        else
                        {
                            
                            break;
                        }
                        
                    }
                    await Projectile01.FadeTo(0, 40);
                    activeprojectileposition01x[0] = activeprojectileposition01x[0] + 1000;
                    await Projectile01.TranslateTo(activeprojectileposition01x[0], activeprojectileposition01y[0], 1);
                    break;
                case 2:
                    --ammunition01;

                    activeprojectileposition01x[1] = CurrentPlayerPositionX;
                    activeprojectileposition01y[1] = CurrentPlayerPositionY;
                    await Projectile02.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile02.FadeTo(1, 1);
                    for (int h = 0; h < 100; h++)
                    {
                        Projectile_collision(1);
                        if (activeprojectileposition01y[1] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[1] = activeprojectileposition01y[1] - 8;
                            await Projectile02.TranslateTo(activeprojectileposition01x[1], activeprojectileposition01y[1], 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile02.FadeTo(0, 40);
                    activeprojectileposition01x[1] = activeprojectileposition01x[1] + 1000;
                    await Projectile02.TranslateTo(activeprojectileposition01x[1], activeprojectileposition01y[1], 1);
                    break;
                case 3:
                    --ammunition01;

                    activeprojectileposition01x[2] = CurrentPlayerPositionX;
                    activeprojectileposition01y[2] = CurrentPlayerPositionY;
                    await Projectile03.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile03.FadeTo(1, 1);
                    for (int g = 0; g < 100; g++)
                    {
                        Projectile_collision(2);
                        if (activeprojectileposition01y[2] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[2] = activeprojectileposition01y[2] - 8;
                            await Projectile03.TranslateTo(activeprojectileposition01x[2], activeprojectileposition01y[2], 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile03.FadeTo(0, 40);
                    activeprojectileposition01x[2] = activeprojectileposition01x[2] + 1000;
                    await Projectile03.TranslateTo(activeprojectileposition01x[2], activeprojectileposition01y[2], 1);
                    break;
                case 4:
                    --ammunition01;

                    activeprojectileposition01x[3] = CurrentPlayerPositionX;
                    activeprojectileposition01y[3] = CurrentPlayerPositionY;
                    await Projectile04.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile04.FadeTo(1, 1);
                    for (int b = 0; b < 100; b++)
                    {
                        Projectile_collision(3);
                        if (activeprojectileposition01y[3] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[3] = activeprojectileposition01y[3] - 8;
                            await Projectile04.TranslateTo(activeprojectileposition01x[3], activeprojectileposition01y[3], 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile04.FadeTo(0, 40);
                    activeprojectileposition01x[3] = activeprojectileposition01x[3] + 1000;
                    await Projectile04.TranslateTo(activeprojectileposition01x[3], activeprojectileposition01y[3], 1);
                    break;
                case 5:
                    --ammunition01;

                    activeprojectileposition01x[4] = CurrentPlayerPositionX;
                    activeprojectileposition01y[4] = CurrentPlayerPositionY;
                    await Projectile05.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile05.FadeTo(1, 1);
                    for (int z = 0; z < 100; z++)
                    {
                        Projectile_collision(4);
                        if (activeprojectileposition01y[4] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[4] = activeprojectileposition01y[4] - 8;
                            await Projectile05.TranslateTo(activeprojectileposition01x[4], activeprojectileposition01y[4], 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile05.FadeTo(0, 40);
                    activeprojectileposition01x[4] = activeprojectileposition01x[4] + 1000;
                    await Projectile05.TranslateTo(activeprojectileposition01x[4], activeprojectileposition01y[4], 1);
                    break;
                case 6:
                    --ammunition01;

                    activeprojectileposition01x[5] = CurrentPlayerPositionX;
                    activeprojectileposition01y[5] = CurrentPlayerPositionY;
                    await Projectile06.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile06.FadeTo(1, 1);
                    for (int x = 0; x < 100; x++)
                    {
                        Projectile_collision(5);
                        if (activeprojectileposition01y[5] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[5] = activeprojectileposition01y[5] - 8;
                            await Projectile06.TranslateTo(activeprojectileposition01x[5], activeprojectileposition01y[5], 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile06.FadeTo(0, 40);
                    activeprojectileposition01x[5] = activeprojectileposition01x[5] + 1000;
                    await Projectile06.TranslateTo(activeprojectileposition01x[5], activeprojectileposition01y[5], 1);
                    break;
                case 7:
                    --ammunition01;

                    activeprojectileposition01x[6] = CurrentPlayerPositionX;
                    activeprojectileposition01y[6] = CurrentPlayerPositionY;
                    await Projectile07.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile07.FadeTo(1, 1);
                    for (int v = 0; v < 100; v++)
                    {
                        Projectile_collision(6);
                        if (activeprojectileposition01y[6] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[6] = activeprojectileposition01y[6] - 8;
                            await Projectile07.TranslateTo(activeprojectileposition01x[6], activeprojectileposition01y[6], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile07.FadeTo(0, 40);
                    activeprojectileposition01x[6] = activeprojectileposition01x[6] + 1000;
                    await Projectile07.TranslateTo(activeprojectileposition01x[6], activeprojectileposition01y[6], 1);
                    break;
                case 8:
                    --ammunition01;

                    activeprojectileposition01x[7] = CurrentPlayerPositionX;
                    activeprojectileposition01y[7] = CurrentPlayerPositionY;
                    await Projectile08.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile08.FadeTo(1, 1);
                    for (int q = 0; q < 100; q++)
                    {
                        Projectile_collision(7);
                        if (activeprojectileposition01y[7] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[7] = activeprojectileposition01y[7] - 8;
                            await Projectile08.TranslateTo(activeprojectileposition01x[7], activeprojectileposition01y[7], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile08.FadeTo(0, 40);
                    activeprojectileposition01x[7] = activeprojectileposition01x[7] + 1000;
                    await Projectile08.TranslateTo(activeprojectileposition01x[7], activeprojectileposition01y[7], 1);
                    break;
                case 9:
                    --ammunition01;

                    activeprojectileposition01x[8] = CurrentPlayerPositionX;
                    activeprojectileposition01y[8] = CurrentPlayerPositionY;
                    await Projectile09.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile09.FadeTo(1, 1);
                    for (int t = 0; t < 100; t++)
                    {
                        Projectile_collision(8);
                        if (activeprojectileposition01y[8] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[8] = activeprojectileposition01y[8] - 8;
                            await Projectile09.TranslateTo(activeprojectileposition01x[8], activeprojectileposition01y[8], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile09.FadeTo(0, 40);
                    activeprojectileposition01x[8] = activeprojectileposition01x[8] + 1000;
                    await Projectile09.TranslateTo(activeprojectileposition01x[8], activeprojectileposition01y[8], 1);
                    break;
                case 10:
                    --ammunition01;

                    activeprojectileposition01x[9] = CurrentPlayerPositionX;
                    activeprojectileposition01y[9] = CurrentPlayerPositionY;
                    await Projectile10.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile10.FadeTo(1, 1);
                    for (int j = 0; j < 100; j++)
                    {
                        Projectile_collision(9);
                        if (activeprojectileposition01y[9] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[9] = activeprojectileposition01y[9] - 8;
                            await Projectile10.TranslateTo(activeprojectileposition01x[9], activeprojectileposition01y[9], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile10.FadeTo(0, 40);
                    activeprojectileposition01x[9] = activeprojectileposition01x[9] + 1000;
                    await Projectile10.TranslateTo(activeprojectileposition01x[9], activeprojectileposition01y[9], 1);
                    break;
                case 11:
                    --ammunition01;

                    activeprojectileposition01x[10] = CurrentPlayerPositionX;
                    activeprojectileposition01y[10] = CurrentPlayerPositionY;
                    await Projectile11.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile11.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        Projectile_collision(10);
                        if (activeprojectileposition01y[10] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[10] = activeprojectileposition01y[10] - 8;
                            await Projectile11.TranslateTo(activeprojectileposition01x[10], activeprojectileposition01y[10], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile11.FadeTo(0, 40);
                    activeprojectileposition01x[10] = activeprojectileposition01x[10] + 1000;
                    await Projectile11.TranslateTo(activeprojectileposition01x[10], activeprojectileposition01y[10], 1);
                    break;
                case 12:
                    --ammunition01;

                    activeprojectileposition01x[11] = CurrentPlayerPositionX;
                    activeprojectileposition01y[11] = CurrentPlayerPositionY;
                    await Projectile12.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile12.FadeTo(1, 1);
                    for (int ha = 0; ha < 100; ha++)
                    {
                        Projectile_collision(11);
                        if (activeprojectileposition01y[11] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[11] = activeprojectileposition01y[11] - 8;
                            await Projectile02.TranslateTo(activeprojectileposition01x[11], activeprojectileposition01y[11], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile12.FadeTo(0, 40);
                    activeprojectileposition01x[11] = activeprojectileposition01x[11] + 1000;
                    await Projectile02.TranslateTo(activeprojectileposition01x[11], activeprojectileposition01y[11], 1);
                    break;
                case 13:
                    --ammunition01;

                    activeprojectileposition01x[12] = CurrentPlayerPositionX;
                    activeprojectileposition01y[12] = CurrentPlayerPositionY;
                    await Projectile13.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile13.FadeTo(1, 1);
                    for (int ga = 0; ga < 100; ga++)
                    {
                        Projectile_collision(12);
                        if (activeprojectileposition01y[12] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[12] = activeprojectileposition01y[12] - 8;
                            await Projectile13.TranslateTo(activeprojectileposition01x[12], activeprojectileposition01y[12], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile13.FadeTo(0, 40);
                    activeprojectileposition01x[12] = activeprojectileposition01x[12] + 1000;
                    await Projectile13.TranslateTo(activeprojectileposition01x[12], activeprojectileposition01y[12], 1);
                    break;
                case 14:
                    --ammunition01;

                    activeprojectileposition01x[13] = CurrentPlayerPositionX;
                    activeprojectileposition01y[13] = CurrentPlayerPositionY;
                    await Projectile14.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile14.FadeTo(1, 1);
                    for (int ba = 0; ba < 100; ba++)
                    {
                        Projectile_collision(13);
                        if (activeprojectileposition01y[13] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[13] = activeprojectileposition01y[13] - 8;
                            await Projectile14.TranslateTo(activeprojectileposition01x[13], activeprojectileposition01y[13], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile14.FadeTo(0, 40);
                    activeprojectileposition01x[13] = activeprojectileposition01x[13] + 1000;
                    await Projectile14.TranslateTo(activeprojectileposition01x[13], activeprojectileposition01y[13], 1);
                    break;
                case 15:
                    --ammunition01;

                    activeprojectileposition01x[14] = CurrentPlayerPositionX;
                    activeprojectileposition01y[14] = CurrentPlayerPositionY;
                    await Projectile15.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile15.FadeTo(1, 1);
                    for (int za = 0; za < 100; za++)
                    {
                        Projectile_collision(14);
                        if (activeprojectileposition01y[14] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[14] = activeprojectileposition01y[14] - 8;
                            await Projectile15.TranslateTo(activeprojectileposition01x[14], activeprojectileposition01y[14], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile15.FadeTo(0, 40);
                    activeprojectileposition01x[14] = activeprojectileposition01x[14] + 1000;
                    await Projectile15.TranslateTo(activeprojectileposition01x[14], activeprojectileposition01y[14], 1);
                    break;
                case 16:
                    --ammunition01;

                    activeprojectileposition01x[15] = CurrentPlayerPositionX;
                    activeprojectileposition01y[15] = CurrentPlayerPositionY;
                    await Projectile16.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile16.FadeTo(1, 1);
                    for (int x = 0; x < 100; x++)
                    {
                        Projectile_collision(15);
                        if (activeprojectileposition01y[15] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[15] = activeprojectileposition01y[15] - 8;
                            await Projectile16.TranslateTo(activeprojectileposition01x[15], activeprojectileposition01y[15], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile16.FadeTo(0, 40);
                    activeprojectileposition01x[15] = activeprojectileposition01x[15] + 1000;
                    await Projectile16.TranslateTo(activeprojectileposition01x[15], activeprojectileposition01y[15], 1);
                    break;
                case 17:
                    --ammunition01;

                    activeprojectileposition01x[16] = CurrentPlayerPositionX;
                    activeprojectileposition01y[16] = CurrentPlayerPositionY;
                    await Projectile17.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile17.FadeTo(1, 1);
                    for (int v = 0; v < 100; v++)
                    {
                        Projectile_collision(16);
                        if (activeprojectileposition01y[16] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[16] = activeprojectileposition01y[16] - 8;
                            await Projectile17.TranslateTo(activeprojectileposition01x[16], activeprojectileposition01y[16], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile17.FadeTo(0, 40);
                    activeprojectileposition01x[16] = activeprojectileposition01x[16] + 1000;
                    await Projectile17.TranslateTo(activeprojectileposition01x[16], activeprojectileposition01y[16], 1);
                    break;
                case 18:
                    --ammunition01;

                    activeprojectileposition01x[17] = CurrentPlayerPositionX;
                    activeprojectileposition01y[17] = CurrentPlayerPositionY;
                    await Projectile18.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile18.FadeTo(1, 1);
                    for (int q = 0; q < 100; q++)
                    {
                        Projectile_collision(17);
                        if (activeprojectileposition01y[17] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[17] = activeprojectileposition01y[17] - 8;
                            await Projectile18.TranslateTo(activeprojectileposition01x[17], activeprojectileposition01y[17], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile18.FadeTo(0, 40);
                    activeprojectileposition01x[17] = activeprojectileposition01x[17] + 1000;
                    await Projectile18.TranslateTo(activeprojectileposition01x[17], activeprojectileposition01y[17], 1);
                    break;
                case 19:
                    --ammunition01;

                    activeprojectileposition01x[18] = CurrentPlayerPositionX;
                    activeprojectileposition01y[18] = CurrentPlayerPositionY;
                    await Projectile19.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile19.FadeTo(1, 1);
                    for (int t = 0; t < 100; t++)
                    {
                        Projectile_collision(18);
                        if (activeprojectileposition01y[18] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[18] = activeprojectileposition01y[18] - 8;
                            await Projectile19.TranslateTo(activeprojectileposition01x[18], activeprojectileposition01y[18], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile19.FadeTo(0, 40);
                    activeprojectileposition01x[18] = activeprojectileposition01x[18] + 1000;
                    await Projectile19.TranslateTo(activeprojectileposition01x[18], activeprojectileposition01y[18], 1);
                    break;
                case 20:
                    --ammunition01;

                    activeprojectileposition01x[19] = CurrentPlayerPositionX;
                    activeprojectileposition01y[19] = CurrentPlayerPositionY;
                    await Projectile20.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile20.FadeTo(1, 1);
                    for (int j = 0; j < 100; j++)
                    {
                        Projectile_collision(19);
                        if (activeprojectileposition01y[19] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[19] = activeprojectileposition01y[19] - 8;
                            await Projectile20.TranslateTo(activeprojectileposition01x[19], activeprojectileposition01y[19], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile20.FadeTo(0, 40);
                    activeprojectileposition01x[19] = activeprojectileposition01x[19] + 1000;
                    await Projectile20.TranslateTo(activeprojectileposition01x[19], activeprojectileposition01y[19], 1);
                    break;
                case 21:
                    --ammunition01;

                    activeprojectileposition01x[20] = CurrentPlayerPositionX;
                    activeprojectileposition01y[20] = CurrentPlayerPositionY;
                    await Projectile21.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile21.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        Projectile_collision(20);
                        if (activeprojectileposition01y[20] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[20] = activeprojectileposition01y[20] - 8;
                            await Projectile21.TranslateTo(activeprojectileposition01x[20], activeprojectileposition01y[20], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile21.FadeTo(0, 40);
                    activeprojectileposition01x[20] = activeprojectileposition01x[20] + 1000;
                    await Projectile21.TranslateTo(activeprojectileposition01x[20], activeprojectileposition01y[20], 1);
                    break;
                case 22:
                    --ammunition01;

                    activeprojectileposition01x[21] = CurrentPlayerPositionX;
                    activeprojectileposition01y[21] = CurrentPlayerPositionY;
                    await Projectile22.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile22.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        Projectile_collision(21);
                        if (activeprojectileposition01y[21] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[21] = activeprojectileposition01y[21] - 8;
                            await Projectile22.TranslateTo(activeprojectileposition01x[21], activeprojectileposition01y[21], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile22.FadeTo(0, 40);
                    activeprojectileposition01x[21] = activeprojectileposition01x[21] + 1000;
                    await Projectile22.TranslateTo(activeprojectileposition01x[21], activeprojectileposition01y[21], 1);
                    break;
            }
        }
        async void bullet_lifecycle02()
        {
            switch (projectilecycle02)
            {
                case 1:


                break;
            }
        }
        async void bullet_lifecycle03()
        {
            switch (projectilecycle03)
            {
                case 1:


                break;
            }
        }
        async void bullet_lifecycle04()
        {
            switch (projectilecycle04)
            {
                case 1:


                break;
            }
        }
        async void bullet_lifecycle05()
        {
            switch (projectilecycle05)
            {
                case 1:


                break;
            }
        }
        async void bullet_lifecycle06()
        {
            switch (projectilecycle06)
            {
                case 1:


                break;
            }
        }
        private void GameMenuBTN_Clicked(object sender, EventArgs e)
        {
            if (gamestatus != 0)
            {

            }
        }
        private void WeaponBTN_Clicked(object sender, EventArgs e)
        {
            //Weapon_menu_Open();
            if (gamestatus != 0)
            {
                if (weaponmenuedswitch == 0)
                {
                    Weapon_menu_Open();
                    weaponmenuedswitch = 1;
                    this.Resources["ColourOfWeaponSwitchBTNClicked"] = Colors.DarkGoldenrod;
                }
                else if (weaponmenuedswitch == 1)
                {
                    Weapon_menu_Close();
                    weaponmenuedswitch = 0;
                    this.Resources["ColourOfWeaponSwitchBTNClicked"] = Colors.Yellow;
                }
            }

        }
        private void Weapon1BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped != 0)
            {
                if(weaponowned01 == 1)
                {
                    weaponequipped = 0;
                }
                this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponequipped == 0)
            {
                this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon2BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped != 1)
            {
                if (weaponowned02 == 1)
                {
                    weaponequipped = 1;
                }
                this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponequipped == 1)
            {
                this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon3BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped != 2)
            {
                if (weaponowned03 == 1)
                {
                    weaponequipped = 2;
                }
                this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponequipped == 2)
            {
                this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon4BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped != 3)
            {
                if (weaponowned04 == 1)
                {
                    weaponequipped = 3;
                }
                this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponequipped == 3)
            {
                this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon5BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped != 4)
            {
                if (weaponowned05 == 1)
                {
                    weaponequipped = 4;
                }
                this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponequipped == 4)
            {
                this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon6BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponequipped != 5)
            {
                if (weaponowned06 == 1)
                {
                    weaponequipped = 5;
                }
                this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponequipped == 5)
            {
                this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateBlue;
            }
        }
        // collission detection
        // player to object collision
        private void Player_collision()
        {
            playercollisiontopleftX = (CurrentPlayerPositionX -35);
            playercollisiontoprightX = (CurrentPlayerPositionX + 35);
            playercollisionbotleftX = (CurrentPlayerPositionX - 35);
            playercollisionbotrightX = (CurrentPlayerPositionX + 35);

            playercollisiontopleftY = (CurrentPlayerPositionY - 35);
            playercollisiontoprightY = (CurrentPlayerPositionY - 35);
            playercollisionbotleftY = (CurrentPlayerPositionY + 35);
            playercollisionbotrightY = (CurrentPlayerPositionY + 35);

            if (true)
            {

            }
        }
        // player to enemy collision

        // enemy collission pos constant
        async void enemyinstance_collision01() // ei 1 - 8
        {
            while (gamestatus !=0)
            {
                await Task.Delay(50);
                int changel0 = 0, changer1 = 1, changepos = 0;
                // 0 = botleft, 1 = botright ei1 x's / same for y's (there is no need for top-down collissions) 
                for (int i = 0; i < 8; i++)
                {
                    enemyinstancehitbox01x[changel0] = (enemyinstancecurpos01x[changepos] - 20);
                    enemyinstancehitbox01x[changer1] = (enemyinstancecurpos01x[changepos] + 20);

                    enemyinstancehitbox01y[changel0] = (enemyinstancecurpos01y[changepos] + 20);
                    enemyinstancehitbox01y[changer1] = (enemyinstancecurpos01y[changepos] + 20);

                    changel0 = changel0 + 2;
                    changer1 = changer1 + 2;
                    changepos++;

                }
            }
            
        }
       
        // projectile collision
        async void Projectile_collision(int imputprojectile)
        {
            int enemyright=1,enemyleft=0,projectile=0,enemyy=0;
            // x within xleft collide parametre corner / x within xright collide parametre corner / the y within y parametre


            // up to ei 1-8
            for (int ti = 0; ti < 8; ti++) // cycle through enemy
            {
                if (activeprojectileposition01y[imputprojectile] == enemyinstancehitbox01y[enemyy])
                {

                    if (activeprojectileposition01x[imputprojectile] >= enemyinstancehitbox01x[enemyleft] && activeprojectileposition01x[imputprojectile] <= enemyinstancehitbox01x[enemyright])
                    {
                        // difficulty dependant  
                        //--> easy / normal
                        if (enemyleft == 0)
                        {
                            ei1deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 2)
                        {
                            ei2deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 4)
                        {
                            ei3deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 6)
                        {
                            ei4deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 8)
                        {
                            ei5deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 10)
                        {
                            ei6deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 12)
                        {
                            ei7deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        if (enemyleft == 14)
                        {
                            ei8deathanim();
                            Remove_Projectile_cur(imputprojectile);
                        }
                        // up to ei 9-16
                        if (activeprojectileposition01x[projectile] >= enemyinstancehitbox02x[enemyleft] && activeprojectileposition01x[projectile] <= enemyinstancehitbox02x[enemyright] && activeprojectileposition01x[projectile] == enemyinstancehitbox02y[enemyy])
                        {
                            // ei 9 - 16 not added yet
                        }

                        //--> hard / very hard diff

                        enemyright += 2;
                        enemyleft += 2;
                        enemyy++;
                    }// x
                }// y
            }// for
        }// end pcm
        // resets object positions
        async void Reset_All_Enemy_Position()
        {
            await e001.TranslateTo(0, 0, 4);
            await e002.TranslateTo(0, 0, 4);
            await e003.TranslateTo(0, 0, 4);
            await e004.TranslateTo(0, 0, 4);
            await e005.TranslateTo(0, 0, 4);
            await e006.TranslateTo(0, 0, 4);
            await e007.TranslateTo(0, 0, 4);
            await e008.TranslateTo(0, 0, 4);
        }
        async void Remove_Projectile_cur(int imputprojectile)
        {
            activeprojectileposition01x[imputprojectile] = activeprojectileposition01x[imputprojectile] + 1000;
            await Projectile01.TranslateTo(activeprojectileposition01x[imputprojectile], 0, 4);
        }
        async void Reset_All_Projectile_Position()
        {
            Reset_PPos_01(); // split to be done all at once equally
            Reset_PPos_02();
            Reset_PPos_03();
            Reset_PPos_04();
            Reset_PPos_05();
            Reset_PPos_06();
            Reset_PPos_07();
            Reset_PPos_08();
            Reset_PPos_09();
            Reset_PPos_10();
            Reset_PPos_11();
            Reset_PPos_12();
            Reset_PPos_13();
            Reset_PPos_14();
            Reset_PPos_15();
            Reset_PPos_16();
            Reset_PPos_17();
            Reset_PPos_18();
            Reset_PPos_19();
            Reset_PPos_20();
            Reset_PPos_21();
            Reset_PPos_22();
        }
        async void Reset_PPos_01()
        {
            activeprojectileposition01x[0] = activeprojectileposition01x[0] + 1000;
            await Projectile01.TranslateTo(activeprojectileposition01x[0], 0, 4);
            
        }
        async void Reset_PPos_02()
        {
            activeprojectileposition01x[1] = activeprojectileposition01x[1] + 1000;
            await Projectile02.TranslateTo(activeprojectileposition01x[1], 0, 4);
            
        }
        async void Reset_PPos_03()
        {
            activeprojectileposition01x[2] = activeprojectileposition01x[2] + 1000;
            await Projectile03.TranslateTo(activeprojectileposition01x[2], 0, 4);
            
        }
        async void Reset_PPos_04()
        {
            activeprojectileposition01x[3] = activeprojectileposition01x[3] + 1000;
            await Projectile04.TranslateTo(activeprojectileposition01x[3], 0, 4);
            
        }
        async void Reset_PPos_05()
        {
            activeprojectileposition01x[4] = activeprojectileposition01x[4] + 1000;
            await Projectile05.TranslateTo(activeprojectileposition01x[4], 0, 4);
            
        }
        async void Reset_PPos_06()
            {
                activeprojectileposition01x[5] = activeprojectileposition01x[5] + 1000;
            await Projectile06.TranslateTo(activeprojectileposition01x[5], 0, 4);
            
        }
        async void Reset_PPos_07()
        {
            activeprojectileposition01x[6] = activeprojectileposition01x[6] + 1000;
            await Projectile07.TranslateTo(activeprojectileposition01x[6], 0, 4);
            
        }
        async void Reset_PPos_08()
        {
            activeprojectileposition01x[7] = activeprojectileposition01x[7] + 1000;
            await Projectile08.TranslateTo(activeprojectileposition01x[7], 0, 4);
            
        }
        async void Reset_PPos_09()
        {
            activeprojectileposition01x[8] = activeprojectileposition01x[8] + 1000;
            await Projectile09.TranslateTo(activeprojectileposition01x[8], 0, 4);
            
        }
        async void Reset_PPos_10()
        {
            activeprojectileposition01x[9] = activeprojectileposition01x[9] + 1000;
            await Projectile10.TranslateTo(activeprojectileposition01x[9], 0, 4);
            
        }
        async void Reset_PPos_11()
        {
            activeprojectileposition01x[10] = activeprojectileposition01x[10] + 1000;
            await Projectile11.TranslateTo(activeprojectileposition01x[10], 0, 4);
            
        }
        async void Reset_PPos_12()
        {
            activeprojectileposition01x[11] = activeprojectileposition01x[11] + 1000;
            await Projectile12.TranslateTo(activeprojectileposition01x[11], 0, 4);
            
        }
        async void Reset_PPos_13()
        {
            activeprojectileposition01x[12] = activeprojectileposition01x[12] + 1000;
            await Projectile13.TranslateTo(activeprojectileposition01x[12], 0, 4);
            
        }
        async void Reset_PPos_14()
        {
            activeprojectileposition01x[13] = activeprojectileposition01x[13] + 1000;
            await Projectile14.TranslateTo(activeprojectileposition01x[13], 0, 4);
            
        }
        async void Reset_PPos_15()
        {
            activeprojectileposition01x[14] = activeprojectileposition01x[14] + 1000;
            await Projectile15.TranslateTo(activeprojectileposition01x[14], 0, 4);
            
        }
        async void Reset_PPos_16()
        {
            activeprojectileposition01x[15] = activeprojectileposition01x[15] + 1000;
            await Projectile16.TranslateTo(activeprojectileposition01x[15], 0, 4);
            
        }
        async void Reset_PPos_17()
        {
            activeprojectileposition01x[16] = activeprojectileposition01x[16] + 1000;
            await Projectile17.TranslateTo(activeprojectileposition01x[16], 0, 4);
            
        }
        async void Reset_PPos_18()
        {
            activeprojectileposition01x[17] = activeprojectileposition01x[17] + 1000;
            await Projectile18.TranslateTo(activeprojectileposition01x[17], 0, 4);
            
        }
        async void Reset_PPos_19()
        {
            activeprojectileposition01x[18] = activeprojectileposition01x[18] + 1000;
            await Projectile19.TranslateTo(activeprojectileposition01x[18], 0, 4);
            
        }
        async void Reset_PPos_20()
        {
            activeprojectileposition01x[19] = activeprojectileposition01x[19] + 1000;
            await Projectile20.TranslateTo(activeprojectileposition01x[19], 0, 4);
           
        }
        async void Reset_PPos_21()
        {
            activeprojectileposition01x[20] = activeprojectileposition01x[20] + 1000;
            await Projectile21.TranslateTo(activeprojectileposition01x[20], 0, 4);
            
        }
        async void Reset_PPos_22()
        {
            activeprojectileposition01x[21] = activeprojectileposition01x[21] + 1000;
            await Projectile22.TranslateTo(activeprojectileposition01x[21], 0, 4);
        }
        // main menu buttons
        private void StartBTN_Clicked(object sender, EventArgs e)
        {
            TitleScreenRetreatAnim();
            MainMenuReturnAnim();
            this.Resources["ColourOfStartGameBTNClicked"] = Colors.White;
        }
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
            this.Resources["ColourOfBrutalBTNClicked"] = Colors.White;
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
            newgamedifficulty = 1;
        }
        private void NormDBTN_Clicked(object sender, EventArgs e)
        {
            Normal_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.White;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
            newgamedifficulty = 2;
        }
        private void HardDBTN_Clicked(object sender, EventArgs e)
        {
            Hard_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.White;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
            newgamedifficulty = 3;
        }
        private void VHardDBTN_Clicked(object sender, EventArgs e)
        {
            VeryHard_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.White;
            newgamedifficulty = 4;
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
        private void Accept01BTN_Clicked(object sender, EventArgs e)// new game accept
        {
            if (newgamedifficulty == 1) // easy
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 = 50;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                Tutorial_activate();
                Update_All_Position_Constant();
            }
            else if (newgamedifficulty == 2) // normal
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 = 30;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                Tutorial_activate();
                Update_All_Position_Constant();
            }
            else if (newgamedifficulty == 3) // hard
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 = 20;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                Tutorial_activate();
                Update_All_Position_Constant();
            }
            else if (newgamedifficulty == 4) // very hard
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 = 10;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                Tutorial_activate();
                Update_All_Position_Constant();
            }
            difficultysetting = newgamedifficulty;
            gamelevelflag = 1;
            weaponequipped = 0;
            
        }
        private void MainMenu_Exit()
        {
            MainMenuRetreatAnim();
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
        private void Accept02BTN_Clicked(object sender, EventArgs e)
        {
            if (saveselected == 1) // 1
            {
                MainMenu_Exit();
            }
            else if (saveselected == 2) // 2
            {
                MainMenu_Exit();
            }
            else if (saveselected == 3) // 3
            {
                MainMenu_Exit();
            }
        }
        private void Accept03BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Accept04BTN_Clicked(object sender, EventArgs e)
        {

        }
        private void Save1BTN_Clicked(object sender, EventArgs e)
        {
            Save1_ClickedAnim();
            this.Resources["ColourOfSave1BTNClicked"] = Colors.White;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.DarkSlateGrey;
            saveselected = 1;
        }
        private void Save2BTN_Clicked(object sender, EventArgs e)
        {
            Save2_ClickedAnim();
            this.Resources["ColourOfSave1BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.White;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.DarkSlateGrey;
            saveselected = 2;
        }
        private void Save3BTN_Clicked(object sender, EventArgs e)
        {
            Save3_ClickedAnim();
            this.Resources["ColourOfSave1BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.White;
            saveselected = 3;
        }
        private void DelSaveBTN_Clicked(object sender, EventArgs e)
        {
            if (saveselected == 1) // 1
            {
                if (save01exist == 1) 
                {
                    MainMenu_Exit();
                }
            }
            else if (saveselected == 2) // 2
            {
                if (save02exist == 1)
                {
                    MainMenu_Exit();
                }
            }
            else if (saveselected == 3) // 3
            {
                if (save03exist == 1)
                {
                    MainMenu_Exit();
                }
            }
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
            newgamedifficulty = 0;
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
        // tutorial stuff
        async void Tutorial_activate()
        {
            await TutorialBox01.TranslateTo(200, 100, 5);
            await tutorialdynamictext.TranslateTo(200, 100, 5);
            tutorialdynamictext.Text = $"PlaceHolder Text 2";
        }
        // game menus
        private void Weapon_menu_Open()
        {
            Weaponmenuanim01();
            Weaponmenuanim02();
            Weaponmenuanim03();
            Weaponmenuanim04();
            Weaponmenuanim05();
            Weaponmenuanim06();
            Weaponmenuanim07();
            Weaponmenuanim08();
            Weaponmenuanim17();
            Weaponmenuanim18();
            Weaponmenuanim19();
            Weaponmenuanim20();
            Weaponmenuanim21();
            Weaponmenuanim22();
        }
        async void Weaponmenuanim01()
        {
            await backgroundweaponmenu01.TranslateTo(475, 0, 400);
            

        }
        async void Weaponmenuanim02()
        {
            await backgroundweaponmenu02.TranslateTo(475, -125, 400);
            

        }
        async void Weaponmenuanim03()
        {
            await backgroundweaponmenu03.TranslateTo(395, -40, 400);
            

        }
        async void Weaponmenuanim04()
        {
            await backgroundweaponmenu04.TranslateTo(395, 45, 400);
            

        }
        async void Weaponmenuanim05()
        {
            await backgroundweaponmenu05.TranslateTo(395, 130, 400);
            

        }
        async void Weaponmenuanim06()
        {
            await backgroundweaponmenu06.TranslateTo(555, -40, 400);
            

        }
        async void Weaponmenuanim07()
        {
            await backgroundweaponmenu07.TranslateTo(555, 45, 400);

            
        }
        async void Weaponmenuanim08()
        {
            await backgroundweaponmenu08.TranslateTo(555, 130, 400);
        }
        async void Weaponmenuanim17()
        {
            await weaponmenu01.TranslateTo(355, -40, 400);
            
        }
        async void Weaponmenuanim18()
        {
            await weaponmenu02.TranslateTo(355, 45, 400);
            
        }
        async void Weaponmenuanim19()
        {
            await weaponmenu03.TranslateTo(355, 130, 400);
            
        }
        async void Weaponmenuanim20()
        {
            await weaponmenu04.TranslateTo(515, -40, 400);
            
        }
        async void Weaponmenuanim21()
        {
            await weaponmenu05.TranslateTo(515, 45, 400);
            
        }
        async void Weaponmenuanim22()
        {
            await weaponmenu06.TranslateTo(515, 130, 400);
        }

        private void Weapon_menu_Close()
        {
            Weaponmenuanim09();
            Weaponmenuanim10();
            Weaponmenuanim11();
            Weaponmenuanim12();
            Weaponmenuanim13();
            Weaponmenuanim14();
            Weaponmenuanim15();
            Weaponmenuanim16();
            Weaponmenuanim23();
            Weaponmenuanim24();
            Weaponmenuanim25();
            Weaponmenuanim26();
            Weaponmenuanim27();
            Weaponmenuanim28();
        }
        async void Weaponmenuanim09()
        {
            await backgroundweaponmenu01.TranslateTo(1475, 0, 400);


        }
        async void Weaponmenuanim10()
        {
            await backgroundweaponmenu02.TranslateTo(1475, -125, 400);


        }
        async void Weaponmenuanim11()
        {
            await backgroundweaponmenu03.TranslateTo(1395, -40, 400);


        }
        async void Weaponmenuanim12()
        {
            await backgroundweaponmenu04.TranslateTo(1395, 45, 400);


        }
        async void Weaponmenuanim13()
        {
            await backgroundweaponmenu05.TranslateTo(1395, 130, 400);


        }
        async void Weaponmenuanim14()
        {
            await backgroundweaponmenu06.TranslateTo(1555, -40, 400);


        }
        async void Weaponmenuanim15()
        {
            await backgroundweaponmenu07.TranslateTo(1555, 45, 400);


        }
        async void Weaponmenuanim16()
        {
            await backgroundweaponmenu08.TranslateTo(1155, 130, 400);
        }
        async void Weaponmenuanim23()
        {
            await weaponmenu01.TranslateTo(1355, -40, 400);

        }
        async void Weaponmenuanim24()
        {
            await weaponmenu02.TranslateTo(1355, 45, 400);

        }
        async void Weaponmenuanim25()
        {
            await weaponmenu03.TranslateTo(1355, 130, 400);

        }
        async void Weaponmenuanim26()
        {
            await weaponmenu04.TranslateTo(1515, -40, 400);

        }
        async void Weaponmenuanim27()
        {
            await weaponmenu05.TranslateTo(1515, 45, 400);

        }
        async void Weaponmenuanim28()
        {
            await weaponmenu06.TranslateTo(1515, 130, 400);
        }

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
        async void Save1_ClickedAnim()
        {
            await saveslot1button.ScaleTo(1.2, 100);
            await saveslot2button.ScaleTo(1, 100);
            await saveslot3button.ScaleTo(1, 100);
        }
        async void Save2_ClickedAnim()
        {
            await saveslot1button.ScaleTo(1, 100);
            await saveslot2button.ScaleTo(1.2, 100);
            await saveslot3button.ScaleTo(1, 100);
        }
        async void Save3_ClickedAnim()
        {
            await saveslot1button.ScaleTo(1, 100);
            await saveslot2button.ScaleTo(1, 100);
            await saveslot3button.ScaleTo(1.2, 100);
        }
        async void ResetAll_Button_States_Anim()
        {
            await Trainingbutton.ScaleTo(1, 100);
            await Brutalbutton.ScaleTo(1, 100);
            await easydiffbutton.ScaleTo(0.6, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
            await saveslot1button.ScaleTo(1, 100);
            await saveslot2button.ScaleTo(1, 100);
            await saveslot3button.ScaleTo(1, 100);
        }
        // page turning
        // title screen
        private void TitleScreenRetreatAnim()
        {
            TitleMenuRetreat01();
            TitleMenuRetreat02();
            TitleMenuRetreat03();
            TitleMenuRetreat04();
        }
        async void TitleMenuRetreat01()
        {
            await EnterGamebutton.FadeTo(0, 1200);
            await EnterGamebutton.TranslateTo(0, 1000, 5);
        }
        async void TitleMenuRetreat02()
        {
            await Task.Delay(900);
            await BigTitleScreen01.FadeTo(0, 300);
            await BigTitleScreen01.TranslateTo(0, 1000, 5);
        }
        async void TitleMenuRetreat03()
        {
            await EnterGamebutton.ScaleTo(1.3, 350);
        }
        async void TitleMenuRetreat04()
        {
            await Task.Delay(900);
            await TitleScreen02.FadeTo(0, 300);
            await TitleScreen02.TranslateTo(0, 1000, 5);
        }
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
            enemyinstancecurpos01x[0] = enemyinstancecurpos01x[0] + 1000;
        }
        async void e001deathanim02()
        {
            await e001.FadeTo(0, 300);
        }
        async void e001deathanim03()
        {
            await e001.ScaleTo(0.6, 300);
        }
        private void ei2deathanim()
        {
            e002deathanim01();
            e002deathanim02();
            e002deathanim03();
        }
        async void e002deathanim01()
        {
            await e002.RotateTo(720, 300);
            enemyinstancecurpos01x[1] = enemyinstancecurpos01x[1] + 1000;
        }
        async void e002deathanim02()
        {
            await e002.FadeTo(0, 300);
        }
        async void e002deathanim03()
        {
            await e002.ScaleTo(0.6, 300);
        }
        private void ei3deathanim()
        {
            e003deathanim01();
            e003deathanim02();
            e003deathanim03();
        }
        async void e003deathanim01()
        {
            await e003.RotateTo(720, 300);
            enemyinstancecurpos01x[2] = enemyinstancecurpos01x[2] + 1000;
        }
        async void e003deathanim02()
        {
            await e003.FadeTo(0, 300);
        }
        async void e003deathanim03()
        {
            await e003.ScaleTo(0.6, 300);
        }
        private void ei4deathanim()
        {
            e004deathanim01();
            e004deathanim02();
            e004deathanim03();
        }
        async void e004deathanim01()
        {
            await e004.RotateTo(720, 300);
            enemyinstancecurpos01x[3] = enemyinstancecurpos01x[3] + 1000;
        }
        async void e004deathanim02()
        {
            await e004.FadeTo(0, 300);
        }
        async void e004deathanim03()
        {
            await e004.ScaleTo(0.6, 300);
        }
        private void ei5deathanim()
        {
            e005deathanim01();
            e005deathanim02();
            e005deathanim03();
        }
        async void e005deathanim01()
        {
            await e005.RotateTo(720, 300);
            enemyinstancecurpos01x[4] = enemyinstancecurpos01x[4] + 1000;
        }
        async void e005deathanim02()
        {
            await e005.FadeTo(0, 300);
        }
        async void e005deathanim03()
        {
            await e005.ScaleTo(0.6, 300);
        }
        private void ei6deathanim()
        {
            e006deathanim01();
            e006deathanim02();
            e006deathanim03();
        }
        async void e006deathanim01()
        {
            await e006.RotateTo(720, 300);
            enemyinstancecurpos01x[5] = enemyinstancecurpos01x[5] + 1000;
        }
        async void e006deathanim02()
        {
            await e006.FadeTo(0, 300);
        }
        async void e006deathanim03()
        {
            await e006.ScaleTo(0.6, 300);
        }
        private void ei7deathanim()
        {
            e007deathanim01();
            e007deathanim02();
            e007deathanim03();
        }
        async void e007deathanim01()
        {
            await e007.RotateTo(720, 300);
            enemyinstancecurpos01x[6] = enemyinstancecurpos01x[6] + 1000;
        }
        async void e007deathanim02()
        {
            await e007.FadeTo(0, 300);
        }
        async void e007deathanim03()
        {
            await e007.ScaleTo(0.6, 300);
        }
        private void ei8deathanim()
        {
            e008deathanim01();
            e008deathanim02();
            e008deathanim03();
        }
        async void e008deathanim01()
        {
            await e008.RotateTo(720, 300);
            enemyinstancecurpos01x[7] = enemyinstancecurpos01x[7] + 1000;
        }
        async void e008deathanim02()
        {
            await e008.FadeTo(0, 300);
        }
        async void e008deathanim03()
        {
            await e008.ScaleTo(0.6, 300);
        }
        // destructables
        // enemy focus section --------------------/
        // starting positions
        private void enemy_instance_openpos01()
        {
            e001startpos();
            e002startpos();
            e003startpos();
            e004startpos();
            e005startpos();
            e006startpos();
            e007startpos();
            e008startpos();

        }
        async void e001startpos()
        {
            enemyinstancecurpos01x[0] =  RNGmove.Next(-30, 80);
            enemyinstancecurpos01y[0] =  RNGmove.Next(-600, -400);
            await e001.TranslateTo(enemyinstancecurpos01x[0], enemyinstancecurpos01y[0], 4);
        }
        async void e002startpos()
        {
            enemyinstancecurpos01x[1] =  RNGmove.Next(300, 450);
            enemyinstancecurpos01y[1] =  RNGmove.Next(-1200, -1100);
            await e002.TranslateTo(enemyinstancecurpos01x[1], enemyinstancecurpos01y[1], 4);
        }
        async void e003startpos()
        {
            enemyinstancecurpos01x[2] =  RNGmove.Next(-30, 80);
            enemyinstancecurpos01y[2] =  RNGmove.Next(-600, -400);
            enemyinstancecurpos01x[2] = -25;
            enemyinstancecurpos01y[2] = -400;
            await e003.TranslateTo(enemyinstancecurpos01x[2], enemyinstancecurpos01y[2], 4);
        }
        async void e004startpos()
        {
            enemyinstancecurpos01x[3] =  RNGmove.Next(100, 380);
            enemyinstancecurpos01y[3] =  RNGmove.Next(-840, -400);
            await e004.TranslateTo(enemyinstancecurpos01x[3], enemyinstancecurpos01y[3], 4);
        }
        async void e005startpos()
        {
            enemyinstancecurpos01x[4] =  RNGmove.Next(100, 280);
            enemyinstancecurpos01y[4] =  RNGmove.Next(-140, -80);
            await e002.TranslateTo(enemyinstancecurpos01x[4], enemyinstancecurpos01y[4], 4);
        }
        async void e006startpos()
        {
            enemyinstancecurpos01x[5] =  RNGmove.Next(10, 120);
            enemyinstancecurpos01y[5] =  RNGmove.Next(-300, -100);
            await e006.TranslateTo(enemyinstancecurpos01x[5], enemyinstancecurpos01y[5], 4);
        }
        async void e007startpos()
        {
            enemyinstancecurpos01x[6] =  RNGmove.Next(270, 480);
            enemyinstancecurpos01y[6] =  RNGmove.Next(-800, -600);
            await e007.TranslateTo(enemyinstancecurpos01x[6], enemyinstancecurpos01y[6], 4);
        }
        async void e008startpos()
        {
            enemyinstancecurpos01x[7] =  RNGmove.Next(40, 130);
            enemyinstancecurpos01y[7] =  RNGmove.Next(-700, -500);
            await e008.TranslateTo(enemyinstancecurpos01x[7], enemyinstancecurpos01y[7], 4);
        }
        // level set ups / --------------------------------- 1 ---------------------------------/
        private void Level_Activate_01()
        {
            enemy_instance_openpos01();
        }
        // constantly updates the positions of every game object ( except for player )
        async void Update_All_Position_Constant()
        {
            await Task.Delay(200);

            Player_collision_updater();
            Player_collision_object_updater();
            Player_weapon_updater();
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
            enemyinstance_collision01();
            testtext();
            while (gamestatus != 0)
            {
                await Task.Delay(200);
                if (weaponequipped == 0)
                {
                    ammunitioncurrent = ammunition01;
                }
                else if (weaponequipped == 1)
                {
                    ammunitioncurrent = ammunition02;
                }
                else if (weaponequipped == 2)
                {
                    ammunitioncurrent = ammunition03;
                }
                else if (weaponequipped == 3)
                {
                    ammunitioncurrent = ammunition04;
                }
                else if (weaponequipped == 4)
                {
                    ammunitioncurrent = ammunition05;
                }
                else if (weaponequipped == 5)
                {
                    ammunitioncurrent = ammunition06;
                }
                ammoqtext.Text = $"Current Ammo: {ammunitioncurrent}   ";
                
            }// while 
        }// end of updatermain
        async void testtext()
        {
            while (gamestatus != 0)
            {
                await Task.Delay(50);
                int changel0 = 0, changer1 = 1, changepos = 0;
                // 0 = botleft, 1 = botright ei1 x's / same for y's (there is no need for top-down collissions) 
                for (int i = 0; i < 8; i++)
                {
                    testnumber.Text = $"test number: left = {enemyinstancehitbox01x[changel0]} right = {enemyinstancehitbox01x[changer1]} eix = {enemyinstancecurpos01x[changepos]} bot = {enemyinstancehitbox01y[changel0]} eiy = {enemyinstancecurpos01y[changepos]} enemynumber = {(changepos+1)}";
                    await Task.Delay(2000);
                    changel0 = changel0 + 2;
                    changer1 = changer1 + 2;
                    changepos++;

                }
            }// while 2
        }
        async void Player_collision_updater()
        {

            while (gamestatus != 0) // split the update loop to stop crashing
            {
                await Task.Delay(20);
                Player_collision();
            }
        }
        async void Player_collision_object_updater()
        {
            while (gamestatus != 0) // split the update loop to stop crashing
            {
                await PlayerHitBoxtopleft.TranslateTo(playercollisiontopleftX, playercollisiontopleftY, 5);
                await PlayerHitBoxtopright.TranslateTo(playercollisiontoprightX, playercollisiontoprightY, 5);
                await PlayerHitBoxbotleft.TranslateTo(playercollisionbotleftX, playercollisionbotleftY, 5);
                await PlayerHitBoxbotright.TranslateTo(playercollisionbotrightX, playercollisionbotrightY, 5);
            }
        }
        async void Player_weapon_updater()
        {
            while (gamestatus != 0) // split the update loop to stop crashing
            {
                await Task.Delay(200);
                if (weaponequipped != 0)
                {
                    this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponequipped == 0)
                {
                    this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponequipped != 1)
                {
                    this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponequipped == 1)
                {
                    this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponequipped != 2)
                {
                    this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponequipped == 2)
                {
                    this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponequipped != 3)
                {
                    this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponequipped == 3)
                {
                    this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponequipped != 4)
                {
                    this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponequipped == 4)
                {
                    this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponequipped != 5)
                {
                    this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponequipped == 5)
                {
                    this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateBlue;
                }
            }
        }
        async void Update_backgrounds()
        {
            while (gamestatus != 0) // split the update loop to stop crashing
            {
                if (gamelevelflag == 1)
                {
                    await BackgroundLevel01.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 2)
                {
                    await BackgroundLevel02.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 3)
                {
                    await BackgroundLevel03.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 4)
                {
                    await BackgroundLevel04.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 5)
                {
                    await BackgroundLevel05.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 6)
                {
                    await BackgroundLevel06.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 7)
                {
                    await BackgroundLevel07.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                else if (gamelevelflag == 8)
                {
                    await BackgroundLevel08.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 40);
                }
                
            }

        }
        
        async void Update_enemys01()
        {
            while (gamestatus != 0) // split the update loop to stop crashing
            {
                
                await e001.TranslateTo(enemyinstancecurpos01x[0], enemyinstancecurpos01y[0], 40);
                await e002.TranslateTo(enemyinstancecurpos01x[1], enemyinstancecurpos01y[1], 40);
                await e003.TranslateTo(enemyinstancecurpos01x[2], enemyinstancecurpos01y[2], 40);
                await e004.TranslateTo(enemyinstancecurpos01x[3], enemyinstancecurpos01y[3], 40);
                await e005.TranslateTo(enemyinstancecurpos01x[4], enemyinstancecurpos01y[4], 40);
                await e006.TranslateTo(enemyinstancecurpos01x[5], enemyinstancecurpos01y[5], 40);
                await e007.TranslateTo(enemyinstancecurpos01x[6], enemyinstancecurpos01y[6], 40);
                await e008.TranslateTo(enemyinstancecurpos01x[7], enemyinstancecurpos01y[7], 40);

            }
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
