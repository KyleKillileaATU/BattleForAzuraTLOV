
using Microsoft.Maui.Controls;

namespace BattleForAzuraTLOV
{
    public partial class MainPage : ContentPage
    {
        int CurrentPlayerPositionX = 0, CurrentPlayerPositionY = 0;
        int BackgroundCurrentPositionX = 0, BackgroundCurrentPositionY = 0;
        int RandomPositionX = 0, RandomPositionY = 0, rtime, weaponequipped = 0;
        int ei1curposX, ei2curposX, ei3curposX, ei4curposX, ei5curposX, ei6curposX, ei7curposX, ei8curposX;
        int ei1curposY, ei2curposY, ei3curposY, ei4curposY, ei5curposY, ei6curposY, ei7curposY, ei8curposY;
        int playercollisiontopleftX, playercollisiontoprightX, playercollisionbotleftX, playercollisionbotrightX;
        int playercollisiontopleftY, playercollisiontoprightY, playercollisionbotleftY, playercollisionbotrightY;
        // projectile positions slog
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
        int activeprojectilepositioni12X, activeprojectilepositioni12Y;
        int activeprojectilepositioni13X, activeprojectilepositioni13Y;
        int activeprojectilepositioni14X, activeprojectilepositioni14Y;
        int activeprojectilepositioni15X, activeprojectilepositioni15Y;
        int activeprojectilepositioni16X, activeprojectilepositioni16Y;
        int activeprojectilepositioni17X, activeprojectilepositioni17Y;
        int activeprojectilepositioni18X, activeprojectilepositioni18Y;
        int activeprojectilepositioni19X, activeprojectilepositioni19Y;
        int activeprojectilepositioni20X, activeprojectilepositioni20Y;
        int activeprojectilepositioni21X, activeprojectilepositioni21Y;
        int activeprojectilepositioni22X, activeprojectilepositioni22Y;

        int ammunition01 = 0, ammunition02 = 0, ammunition03 = 0, ammunition04 = 0, ammunition05 = 0, ammunition06 = 0, ammunitioncurrent=0;
        int projectilecycle01 = 0, projectilecycle02 = 0, projectilecycle03 = 0, projectilecycle04 = 0, projectilecycle05 = 0, projectilecycle06 = 0;
        int gamelevelflag=0, gamestatus=0, areascreenlock=0;
        int newgamedifficulty=0, difficultysetting=0;
        int playermoveamount = 0;
        int weaponmenuedswitch = 0, weaponowned01 = 1, weaponowned02 = 0, weaponowned03 = 0, weaponowned04 = 0, weaponowned05 = 0, weaponowned06 = 0;
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
            setupallgameprojectiles();
            setupallgameobjects();
            setupallgameui();
            hideallgamecontent();
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
            await BackgroundLevel01.ScaleTo(4.4, 4);
            await BackgroundLevel02.ScaleTo(4.4, 4);
            await BackgroundLevel03.ScaleTo(4.4, 4);
            await BackgroundLevel04.ScaleTo(4.4, 4);
            await BackgroundLevel05.ScaleTo(4.4, 4);
            await BackgroundLevel06.ScaleTo(4.4, 4);
            await BackgroundLevel07.ScaleTo(4.4, 4);
            await BackgroundLevel08.ScaleTo(4.4, 4);
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
            await gamemenubutton.TranslateTo(0, 0, 4);
            await weaponswitchbutton.TranslateTo(0, 0, 4);
            await backgroundammotext.TranslateTo(-110, -95, 4);
            await backgroundweaponmenu01.TranslateTo(1475, 0, 4);
            await backgroundweaponmenu02.TranslateTo(1475, -125, 4);
            await backgroundweaponmenu03.TranslateTo(1395, -40, 4);
            await backgroundweaponmenu04.TranslateTo(1395, 45, 4);
            await backgroundweaponmenu05.TranslateTo(1395, 130, 4);
            await backgroundweaponmenu06.TranslateTo(1555, -40, 4);
            await backgroundweaponmenu07.TranslateTo(1555, 45, 4);
            await backgroundweaponmenu08.TranslateTo(1555, 130, 4);
            await weaponmenu01.TranslateTo(1395, -40, 4);
            await weaponmenu02.TranslateTo(1395, 45, 4);
            await weaponmenu03.TranslateTo(1395, 130, 4);
            await weaponmenu04.TranslateTo(1555, -40, 4);
            await weaponmenu05.TranslateTo(1555, 45, 4);
            await weaponmenu06.TranslateTo(1555, 130, 4);
            await ammoqtext.TranslateTo(-113, -135, 4);
            await attackbutton.ScaleTo(1.7, 4);
            await weaponswitchbutton.ScaleTo(0.8, 4);
            await backgroundammotext.ScaleTo(1.3, 4);
            await backgroundweaponmenu01.ScaleTo(4.5, 4);

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
            await PlayerIMG.FadeTo(1, 5);
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
            setupprojectiles01();
            setupprojectiles02();
        }
        async void setupprojectiles01()
        {
            await Projectile01.TranslateTo(-1000, 0, 4);
            await Projectile02.TranslateTo(-1000, 0, 4);
            await Projectile03.TranslateTo(-1000, 0, 4);
            await Projectile04.TranslateTo(-1000, 0, 4);
            await Projectile05.TranslateTo(-1000, 0, 4);
            await Projectile06.TranslateTo(-1000, 0, 4);
            await Projectile07.TranslateTo(-1000, 0, 4);
            await Projectile08.TranslateTo(-1000, 0, 4);
            await Projectile09.TranslateTo(-1000, 0, 4);
            await Projectile10.TranslateTo(-1000, 0, 4);
            await Projectile11.TranslateTo(-1000, 0, 4);
            await Projectile12.TranslateTo(-1000, 0, 4);
            await Projectile13.TranslateTo(-1000, 0, 4);
            await Projectile14.TranslateTo(-1000, 0, 4);
            await Projectile15.TranslateTo(-1000, 0, 4);
            await Projectile16.TranslateTo(-1000, 0, 4);
            await Projectile17.TranslateTo(-1000, 0, 4);
            await Projectile18.TranslateTo(-1000, 0, 4);
            await Projectile19.TranslateTo(-1000, 0, 4);
            await Projectile20.TranslateTo(-1000, 0, 4);
            await Projectile21.TranslateTo(-1000, 0, 4);
            await Projectile22.TranslateTo(-1000, 0, 4);
        }
        async void setupprojectiles02()
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
            this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.Navy;
            Move_player();
            Move_player_Hit_Box();
            Move_player_Camera_Box();  
        }
        private void Moveobjects_level01()
        {
            ei1curposY = ei1curposY + playermoveamount;
            ei2curposY = ei2curposY + playermoveamount;
            ei3curposY = ei3curposY + playermoveamount;
            ei4curposY = ei4curposY + playermoveamount;
            ei5curposY = ei5curposY + playermoveamount;
            ei6curposY = ei6curposY + playermoveamount;
            ei7curposY = ei7curposY + playermoveamount;
            ei8curposY = ei8curposY + playermoveamount;
        }
        private void Moveobjects_level02()
        {

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
            this.Resources["ColourOfLeftMoveBTNClicked"] = Colors.Navy;
            Move_playerLeft();
            Move_player_Hit_Box();
            Move_player_Camera_Box();
        }
        private void RightMoveBTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfRightMoveBTNClicked"] = Colors.Navy;
            Move_playerRight();
            Move_player_Hit_Box();
            Move_player_Camera_Box();            
        }
        private void BackMoveBTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfBackMoveBTNClicked"] = Colors.Navy;
            Move_playerBack();
            Move_player_Hit_Box();
            Move_player_Camera_Box();
        }
        async void Infinite_RNG_Movement()
        {
            while (true)
            {
                ei1curposX = (ei1curposX + RNGmove.Next(-30, 30));
                ei1curposY = (ei1curposY + RNGmove.Next(-30, 30));
                rtime = RNGmove.Next(150, 750);
                if (ei1curposX >=445) 
                {
                    ei1curposX = 445;
                }
                if (ei1curposX <= -445)
                {
                    ei1curposX = -445;
                }
                await e001.TranslateTo(ei1curposX, ei1curposY, (uint)rtime);
                await Task.Delay(2000);
            }
        }
        private void AttackBTN_Clicked(object sender, EventArgs e)
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
                    bullet_animation01();

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
                    bullet_animation02();

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
                    bullet_animation03();

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
                    bullet_animation04();

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
                    bullet_animation05();

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
                    bullet_animation06();

                }
            }
        }
        // animations for each gun ( cycling the bullet instances )
        async void bullet_animation01()
        {
            switch (projectilecycle01)// projectile cylcle == the gun equipped, 1 is for gunequipped ' 0 ' and so on
            {
                case 1:
                    --ammunition01;
                    
                    activeprojectilepositioni1X = CurrentPlayerPositionX;
                    activeprojectilepositioni1Y = CurrentPlayerPositionY;
                    await Projectile01.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile01.FadeTo(1, 1);
                    for (int i = 0; i < 100; i++)
                    {
                        if(activeprojectilepositioni1Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni1Y = activeprojectilepositioni1Y - 8;
                            await Projectile01.TranslateTo(activeprojectilepositioni1X, activeprojectilepositioni1Y, 1);
                        }
                        else
                        {
                            
                            break;
                        }
                        
                    }
                    await Projectile01.FadeTo(0, 40);
                    activeprojectilepositioni1X = activeprojectilepositioni1X + 1000;
                    await Projectile01.TranslateTo(activeprojectilepositioni1X, activeprojectilepositioni1Y, 1);
                    break;
                case 2:
                    --ammunition01;
                    
                    activeprojectilepositioni2X = CurrentPlayerPositionX;
                    activeprojectilepositioni2Y = CurrentPlayerPositionY;
                    await Projectile02.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile02.FadeTo(1, 1);
                    for (int h = 0; h < 100; h++)
                    {
                        if (activeprojectilepositioni2Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni2Y = activeprojectilepositioni2Y - 8;
                            await Projectile02.TranslateTo(activeprojectilepositioni2X, activeprojectilepositioni2Y, 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile02.FadeTo(0, 40);
                    activeprojectilepositioni2X = activeprojectilepositioni2X+1000;
                    await Projectile02.TranslateTo(activeprojectilepositioni2X, activeprojectilepositioni2Y, 1);
                    break;
                case 3:
                    --ammunition01;
                    
                    activeprojectilepositioni3X = CurrentPlayerPositionX;
                    activeprojectilepositioni3Y = CurrentPlayerPositionY;
                    await Projectile03.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile03.FadeTo(1, 1);
                    for (int g = 0; g < 100; g++)
                    {
                        if (activeprojectilepositioni3Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni3Y = activeprojectilepositioni3Y - 8;
                            await Projectile03.TranslateTo(activeprojectilepositioni3X, activeprojectilepositioni3Y, 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile03.FadeTo(0, 40);
                    activeprojectilepositioni3X = activeprojectilepositioni3X + 1000;
                    await Projectile03.TranslateTo(activeprojectilepositioni3X, activeprojectilepositioni3Y, 1);
                    break;
                case 4:
                    --ammunition01;
                    
                    activeprojectilepositioni4X = CurrentPlayerPositionX;
                    activeprojectilepositioni4Y = CurrentPlayerPositionY;
                    await Projectile04.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile04.FadeTo(1, 1);
                    for (int b = 0; b < 100; b++)
                    {
                        if (activeprojectilepositioni4Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni4Y = activeprojectilepositioni4Y - 8;
                            await Projectile04.TranslateTo(activeprojectilepositioni4X, activeprojectilepositioni4Y, 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile04.FadeTo(0, 40);
                    activeprojectilepositioni4X = activeprojectilepositioni4X + 1000;
                    await Projectile04.TranslateTo(activeprojectilepositioni4X, activeprojectilepositioni4Y, 1);
                    break;
                case 5:
                    --ammunition01;
                    
                    activeprojectilepositioni5X = CurrentPlayerPositionX;
                    activeprojectilepositioni5Y = CurrentPlayerPositionY;
                    await Projectile05.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile05.FadeTo(1, 1);
                    for (int z = 0; z < 100; z++)
                    {
                        if (activeprojectilepositioni5Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni5Y = activeprojectilepositioni5Y - 8;
                            await Projectile05.TranslateTo(activeprojectilepositioni5X, activeprojectilepositioni5Y, 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile05.FadeTo(0, 40);
                    activeprojectilepositioni5X = activeprojectilepositioni5X + 1000;
                    await Projectile05.TranslateTo(activeprojectilepositioni5X, activeprojectilepositioni5Y, 1);
                    break;
                case 6:
                    --ammunition01;
                    
                    activeprojectilepositioni6X = CurrentPlayerPositionX;
                    activeprojectilepositioni6Y = CurrentPlayerPositionY;
                    await Projectile06.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile06.FadeTo(1, 1);
                    for (int x = 0; x < 100; x++)
                    {
                        if (activeprojectilepositioni6Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni6Y = activeprojectilepositioni6Y - 8;
                            await Projectile06.TranslateTo(activeprojectilepositioni6X, activeprojectilepositioni6Y, 1);
                        }
                        else
                        {
                            
                            break;
                        }
                    }
                    await Projectile06.FadeTo(0, 40);
                    activeprojectilepositioni6X = activeprojectilepositioni6X + 1000;
                    await Projectile06.TranslateTo(activeprojectilepositioni6X, activeprojectilepositioni6Y, 1);
                    break;
                case 7:
                    --ammunition01;
                    
                    activeprojectilepositioni7X = CurrentPlayerPositionX;
                    activeprojectilepositioni7Y = CurrentPlayerPositionY;
                    await Projectile07.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile07.FadeTo(1, 1);
                    for (int v = 0; v < 100; v++)
                    {
                        if (activeprojectilepositioni7Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni7Y = activeprojectilepositioni7Y - 8;
                            await Projectile07.TranslateTo(activeprojectilepositioni7X, activeprojectilepositioni7Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile07.FadeTo(0, 40);
                    activeprojectilepositioni7X = activeprojectilepositioni7X + 1000;
                    await Projectile07.TranslateTo(activeprojectilepositioni7X, activeprojectilepositioni7Y, 1);
                    break;
                case 8:
                    --ammunition01;
                    
                    activeprojectilepositioni8X = CurrentPlayerPositionX;
                    activeprojectilepositioni8Y = CurrentPlayerPositionY;
                    await Projectile08.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile08.FadeTo(1, 1);
                    for (int q = 0; q < 100; q++)
                    {
                        if (activeprojectilepositioni8Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni8Y = activeprojectilepositioni8Y - 8;
                            await Projectile08.TranslateTo(activeprojectilepositioni8X, activeprojectilepositioni8Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile08.FadeTo(0, 40);
                    activeprojectilepositioni8X = activeprojectilepositioni8X + 1000;
                    await Projectile08.TranslateTo(activeprojectilepositioni8X, activeprojectilepositioni8Y, 1);
                    break;
                case 9:
                    --ammunition01;
                    
                    activeprojectilepositioni9X = CurrentPlayerPositionX;
                    activeprojectilepositioni9Y = CurrentPlayerPositionY;
                    await Projectile09.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile09.FadeTo(1, 1);
                    for (int t = 0; t < 100; t++)
                    {
                        if (activeprojectilepositioni9Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni9Y = activeprojectilepositioni9Y - 8;
                            await Projectile09.TranslateTo(activeprojectilepositioni9X, activeprojectilepositioni9Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile09.FadeTo(0, 40);
                    activeprojectilepositioni9X = activeprojectilepositioni9X + 1000;
                    await Projectile09.TranslateTo(activeprojectilepositioni9X, activeprojectilepositioni9Y, 1);
                    break;
                case 10:
                    --ammunition01;
                    
                    activeprojectilepositioni10X = CurrentPlayerPositionX;
                    activeprojectilepositioni10Y = CurrentPlayerPositionY;
                    await Projectile10.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile10.FadeTo(1, 1);
                    for (int j = 0; j < 100; j++)
                    {
                        if (activeprojectilepositioni10Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni10Y = activeprojectilepositioni10Y - 8;
                            await Projectile10.TranslateTo(activeprojectilepositioni10X, activeprojectilepositioni10Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile10.FadeTo(0, 40);
                    activeprojectilepositioni10X = activeprojectilepositioni10X + 1000;
                    await Projectile10.TranslateTo(activeprojectilepositioni10X, activeprojectilepositioni10Y, 1);
                    break;
                case 11:
                    --ammunition01;
                    
                    activeprojectilepositioni11X = CurrentPlayerPositionX;
                    activeprojectilepositioni11Y = CurrentPlayerPositionY;
                    await Projectile11.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile11.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        if (activeprojectilepositioni11Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni11Y = activeprojectilepositioni11Y - 8;
                            await Projectile11.TranslateTo(activeprojectilepositioni11X, activeprojectilepositioni11Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile11.FadeTo(0, 40);
                    activeprojectilepositioni11X = activeprojectilepositioni11X + 1000;
                    await Projectile11.TranslateTo(activeprojectilepositioni11X, activeprojectilepositioni11Y, 1);
                    break;
                case 12:
                    --ammunition01;
                    
                    activeprojectilepositioni12X = CurrentPlayerPositionX;
                    activeprojectilepositioni12Y = CurrentPlayerPositionY;
                    await Projectile12.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile12.FadeTo(1, 1);
                    for (int ha = 0; ha < 100; ha++)
                    {
                        if (activeprojectilepositioni12Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni12Y = activeprojectilepositioni12Y - 8;
                            await Projectile02.TranslateTo(activeprojectilepositioni12X, activeprojectilepositioni12Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile12.FadeTo(0, 40);
                    activeprojectilepositioni12X = activeprojectilepositioni2X + 1000;
                    await Projectile02.TranslateTo(activeprojectilepositioni12X, activeprojectilepositioni12Y, 1);
                    break;
                case 13:
                    --ammunition01;
                    
                    activeprojectilepositioni13X = CurrentPlayerPositionX;
                    activeprojectilepositioni13Y = CurrentPlayerPositionY;
                    await Projectile13.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile13.FadeTo(1, 1);
                    for (int ga = 0; ga < 100; ga++)
                    {
                        if (activeprojectilepositioni13Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni13Y = activeprojectilepositioni13Y - 8;
                            await Projectile13.TranslateTo(activeprojectilepositioni13X, activeprojectilepositioni13Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile13.FadeTo(0, 40);
                    activeprojectilepositioni13X = activeprojectilepositioni13X + 1000;
                    await Projectile13.TranslateTo(activeprojectilepositioni13X, activeprojectilepositioni13Y, 1);
                    break;
                case 14:
                    --ammunition01;
                    
                    activeprojectilepositioni14X = CurrentPlayerPositionX;
                    activeprojectilepositioni14Y = CurrentPlayerPositionY;
                    await Projectile14.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile14.FadeTo(1, 1);
                    for (int ba = 0; ba < 100; ba++)
                    {
                        if (activeprojectilepositioni14Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni14Y = activeprojectilepositioni14Y - 8;
                            await Projectile14.TranslateTo(activeprojectilepositioni14X, activeprojectilepositioni14Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile14.FadeTo(0, 40);
                    activeprojectilepositioni14X = activeprojectilepositioni14X + 1000;
                    await Projectile14.TranslateTo(activeprojectilepositioni14X, activeprojectilepositioni14Y, 1);
                    break;
                case 15:
                    --ammunition01;
                    
                    activeprojectilepositioni15X = CurrentPlayerPositionX;
                    activeprojectilepositioni15Y = CurrentPlayerPositionY;
                    await Projectile15.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile15.FadeTo(1, 1);
                    for (int za = 0; za < 100; za++)
                    {
                        if (activeprojectilepositioni15Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni15Y = activeprojectilepositioni15Y - 8;
                            await Projectile15.TranslateTo(activeprojectilepositioni15X, activeprojectilepositioni15Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile15.FadeTo(0, 40);
                    activeprojectilepositioni15X = activeprojectilepositioni15X + 1000;
                    await Projectile15.TranslateTo(activeprojectilepositioni15X, activeprojectilepositioni15Y, 1);
                    break;
                case 16:
                    --ammunition01;
                    
                    activeprojectilepositioni16X = CurrentPlayerPositionX;
                    activeprojectilepositioni16Y = CurrentPlayerPositionY;
                    await Projectile16.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile16.FadeTo(1, 1);
                    for (int x = 0; x < 100; x++)
                    {
                        if (activeprojectilepositioni16Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni16Y = activeprojectilepositioni16Y - 8;
                            await Projectile16.TranslateTo(activeprojectilepositioni16X, activeprojectilepositioni16Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile16.FadeTo(0, 40);
                    activeprojectilepositioni16X = activeprojectilepositioni16X + 1000;
                    await Projectile16.TranslateTo(activeprojectilepositioni16X, activeprojectilepositioni16Y, 1);
                    break;
                case 17:
                    --ammunition01;
                    
                    activeprojectilepositioni17X = CurrentPlayerPositionX;
                    activeprojectilepositioni17Y = CurrentPlayerPositionY;
                    await Projectile17.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile17.FadeTo(1, 1);
                    for (int v = 0; v < 100; v++)
                    {
                        if (activeprojectilepositioni17Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni17Y = activeprojectilepositioni17Y - 8;
                            await Projectile17.TranslateTo(activeprojectilepositioni17X, activeprojectilepositioni17Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile17.FadeTo(0, 40);
                    activeprojectilepositioni17X = activeprojectilepositioni17X + 1000;
                    await Projectile17.TranslateTo(activeprojectilepositioni17X, activeprojectilepositioni17Y, 1);
                    break;
                case 18:
                    --ammunition01;
                    
                    activeprojectilepositioni18X = CurrentPlayerPositionX;
                    activeprojectilepositioni18Y = CurrentPlayerPositionY;
                    await Projectile18.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile18.FadeTo(1, 1);
                    for (int q = 0; q < 100; q++)
                    {
                        if (activeprojectilepositioni18Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni18Y = activeprojectilepositioni18Y - 8;
                            await Projectile18.TranslateTo(activeprojectilepositioni18X, activeprojectilepositioni18Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile18.FadeTo(0, 40);
                    activeprojectilepositioni18X = activeprojectilepositioni18X + 1000;
                    await Projectile18.TranslateTo(activeprojectilepositioni18X, activeprojectilepositioni18Y, 1);
                    break;
                case 19:
                    --ammunition01;
                    
                    activeprojectilepositioni19X = CurrentPlayerPositionX;
                    activeprojectilepositioni19Y = CurrentPlayerPositionY;
                    await Projectile19.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile19.FadeTo(1, 1);
                    for (int t = 0; t < 100; t++)
                    {
                        if (activeprojectilepositioni19Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni19Y = activeprojectilepositioni19Y - 8;
                            await Projectile19.TranslateTo(activeprojectilepositioni19X, activeprojectilepositioni19Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile19.FadeTo(0, 40);
                    activeprojectilepositioni19X = activeprojectilepositioni19X + 1000;
                    await Projectile19.TranslateTo(activeprojectilepositioni19X, activeprojectilepositioni19Y, 1);
                    break;
                case 20:
                    --ammunition01;
                    
                    activeprojectilepositioni20X = CurrentPlayerPositionX;
                    activeprojectilepositioni20Y = CurrentPlayerPositionY;
                    await Projectile20.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile20.FadeTo(1, 1);
                    for (int j = 0; j < 100; j++)
                    {
                        if (activeprojectilepositioni20Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni20Y = activeprojectilepositioni20Y - 8;
                            await Projectile20.TranslateTo(activeprojectilepositioni20X, activeprojectilepositioni20Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile20.FadeTo(0, 40);
                    activeprojectilepositioni20X = activeprojectilepositioni20X + 1000;
                    await Projectile20.TranslateTo(activeprojectilepositioni20X, activeprojectilepositioni20Y, 1);
                    break;
                case 21:
                    --ammunition01;
                    
                    activeprojectilepositioni21X = CurrentPlayerPositionX;
                    activeprojectilepositioni21Y = CurrentPlayerPositionY;
                    await Projectile21.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile21.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        if (activeprojectilepositioni21Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni21Y = activeprojectilepositioni21Y - 8;
                            await Projectile21.TranslateTo(activeprojectilepositioni21X, activeprojectilepositioni21Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile21.FadeTo(0, 40);
                    activeprojectilepositioni21X = activeprojectilepositioni21X + 1000;
                    await Projectile21.TranslateTo(activeprojectilepositioni21X, activeprojectilepositioni21Y, 1);
                    break;
                case 22:
                    --ammunition01;
                   
                    activeprojectilepositioni22X = CurrentPlayerPositionX;
                    activeprojectilepositioni22Y = CurrentPlayerPositionY;
                    await Projectile22.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile22.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        if (activeprojectilepositioni22Y >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectilepositioni22Y = activeprojectilepositioni22Y - 8;
                            await Projectile22.TranslateTo(activeprojectilepositioni22X, activeprojectilepositioni22Y, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile22.FadeTo(0, 40);
                    activeprojectilepositioni22X = activeprojectilepositioni22X + 1000;
                    await Projectile22.TranslateTo(activeprojectilepositioni22X, activeprojectilepositioni22Y, 1);
                    break;
            }
        }
        async void bullet_animation02()
        {
            switch (projectilecycle02)
            {
                case 1:


                break;
            }
        }
        async void bullet_animation03()
        {
            switch (projectilecycle03)
            {
                case 1:


                break;
            }
        }
        async void bullet_animation04()
        {
            switch (projectilecycle04)
            {
                case 1:


                break;
            }
        }
        async void bullet_animation05()
        {
            switch (projectilecycle05)
            {
                case 1:


                break;
            }
        }
        async void bullet_animation06()
        {
            switch (projectilecycle06)
            {
                case 1:


                break;
            }
        }
        private void GameMenuBTN_Clicked(object sender, EventArgs e)
        {

        }
        private void WeaponBTN_Clicked(object sender, EventArgs e)
        {
            //Weapon_menu_Open();
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
            playercollisiontopleftX = (CurrentPlayerPositionX -10);
            playercollisiontoprightX = (CurrentPlayerPositionX + 10);
            playercollisionbotleftX = (CurrentPlayerPositionX - 10);
            playercollisionbotrightX = (CurrentPlayerPositionX + 10);
            playercollisiontopleftY = (CurrentPlayerPositionY - 10);
            playercollisiontoprightY = (CurrentPlayerPositionY + 10);
            playercollisionbotleftY = (CurrentPlayerPositionY - 10);
            playercollisionbotrightY = (CurrentPlayerPositionY + 10);

            if (true)
            {

            }
        }
        // player to enemy collision

        // projectile collision

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
        private void Accept01BTN_Clicked(object sender, EventArgs e)// n ew game accept
        {
            if (newgamedifficulty == 1) // easy
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -1150;
                ammunition01 = 50;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Update_All_Position_Constant();
            }
            else if (newgamedifficulty == 2) // normal
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -1150;
                ammunition01 = 30;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Update_All_Position_Constant();
            }
            else if (newgamedifficulty == 3) // hard
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -1150;
                ammunition01 = 20;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Update_All_Position_Constant();
            }
            else if (newgamedifficulty == 4) // very hard
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -1150;
                ammunition01 = 10;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
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
            await weaponmenu01.TranslateTo(395, -40, 400);
            
        }
        async void Weaponmenuanim18()
        {
            await weaponmenu02.TranslateTo(395, 45, 400);
            
        }
        async void Weaponmenuanim19()
        {
            await weaponmenu03.TranslateTo(395, 130, 400);
            
        }
        async void Weaponmenuanim20()
        {
            await weaponmenu04.TranslateTo(555, -40, 400);
            
        }
        async void Weaponmenuanim21()
        {
            await weaponmenu05.TranslateTo(555, 45, 400);
            
        }
        async void Weaponmenuanim22()
        {
            await weaponmenu06.TranslateTo(555, 130, 400);
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
            await weaponmenu01.TranslateTo(1395, -40, 400);

        }
        async void Weaponmenuanim24()
        {
            await weaponmenu02.TranslateTo(1395, 45, 400);

        }
        async void Weaponmenuanim25()
        {
            await weaponmenu03.TranslateTo(1395, 130, 400);

        }
        async void Weaponmenuanim26()
        {
            await weaponmenu04.TranslateTo(1555, -40, 400);

        }
        async void Weaponmenuanim27()
        {
            await weaponmenu05.TranslateTo(1555, 45, 400);

        }
        async void Weaponmenuanim28()
        {
            await weaponmenu06.TranslateTo(1555, 130, 400);
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
        async void ResetAll_Button_States_Anim()
        {
            await Trainingbutton.ScaleTo(1, 100);
            await Brutalbutton.ScaleTo(1, 100);
            await easydiffbutton.ScaleTo(0.6, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);

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
                testnumber.Text = $"test number: {projectilecycle01}   ";
            }
        }
        async void Player_collision_updater()
        {

            while (gamestatus != 0) // split the update loop to stop crashing
            {
                await Task.Delay(20);
                playercollisiontopleftX = (CurrentPlayerPositionX - 10);
                playercollisiontoprightX = (CurrentPlayerPositionX + 10);
                playercollisionbotleftX = (CurrentPlayerPositionX - 10);
                playercollisionbotrightX = (CurrentPlayerPositionX + 10);
                playercollisiontopleftY = (CurrentPlayerPositionY - 10);
                playercollisiontoprightY = (CurrentPlayerPositionY + 10);
                playercollisionbotleftY = (CurrentPlayerPositionY - 10);
                playercollisionbotrightY = (CurrentPlayerPositionY + 10);
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
                await e001.TranslateTo(ei1curposX, ei1curposY, 40);
                await e002.TranslateTo(ei2curposX, ei2curposY, 40);
                await e003.TranslateTo(ei3curposX, ei3curposY, 40);
                await e004.TranslateTo(ei4curposX, ei4curposY, 40);
                await e005.TranslateTo(ei5curposX, ei5curposY, 40);
                await e006.TranslateTo(ei6curposX, ei6curposY, 40);
                await e007.TranslateTo(ei7curposX, ei7curposY, 40);
                await e008.TranslateTo(ei8curposX, ei8curposY, 40);
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
