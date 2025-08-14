
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Xml.Linq;

namespace BattleForAzuraTLOV
{

    
    public partial class MainPage : ContentPage
    {

        int CurrentPlayerPositionX = 0, CurrentPlayerPositionY = 0;
        int BackgroundCurrentPositionX = 0, BackgroundCurrentPositionY = 0;
        int RandomPositionX = 0, RandomPositionY = 0, rtime, weaponEquipped = 0;

        int playercollisiontopleftX, playercollisiontoprightX, playercollisionbotleftX, playercollisionbotrightX;
        int playercollisiontopY;
        // projectile positions slug
        int[] canHit = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
        int[] enemytier = { 0, 1, 2, 3 };
        int[] isMoving = { 0, 0, 0, 0 };
        int[] canDrop = { 0, 0, 0, 0, 0, 0, 0, 0 };

        int ammunition01 = 0, ammunition02 = 0, ammunition03 = 0, ammunition04 = 0, ammunition05 = 0, ammunition06 = 0, ammunitioncurrent = 0;
        int projectilecycle01 = 0, projectilecycle02 = 0, projectilecycle03 = 0, projectilecycle04 = 0, projectilecycle05 = 0, projectilecycle06 = 0;
        int gamelevelflag = 0, gamestatus = 0, areascreenlock = 0, tutorialbclicked = 0;
        int newgamedifficulty = 0, difficultysetting = 0, saveselected = 0, save01exist = 0, save02exist = 0, save03exist = 0, missionselected = 1;
        int playerMoveamount = 0, playerDamageValue = 5, killCounter = 0, eCanMove = 0;
        int weaponMenuedSwitch = 0, weaponowned01 = 1, weaponowned02 = 0, weaponowned03 = 0, weaponowned04 = 0, weaponowned05 = 0, weaponowned06 = 0;
        int enemytype1hp = 8, enemytype2hp = 12, enemytype1dmg = 3, enemytype2dmg = 5, boss1hp = 100, boss1dmg = 10, testnumberT= 0, dropSwitch=0;
        float playerHealthPoints = 140, playerStaminaPoints = 140, playerMagicPoints = 140, sprintSwitch=0, delay = 0, bossactive = 0;
        Random RNGmove = new Random();


        // creating arrays of game objects
        EnemyObject[] enemyInstance = new EnemyObject[16];
        EnemyObject[] eliteEnemyInstance = new EnemyObject[4];
        EnemyObject[] bossInstance = new EnemyObject[1];
        ItemObject[] itemInstance = new ItemObject[8];

        public MainPage()
        {
            //new KeyboardAccelerator { Key = "X" };
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
            ItemOffSet();


            playerMoveamount = 1;

            for (int i = 0; i < enemyInstance.Length; i++)
            {
                enemyInstance[i] = new EnemyObject(0, 0, enemytype1hp, enemytype1dmg, false);
            }
            for (int j = 0; j < eliteEnemyInstance.Length; j++)
            {
                eliteEnemyInstance[j] = new EnemyObject(0, 0, enemytype2hp, enemytype2dmg, false);
            }
            for (int f = 0; f < bossInstance.Length; f++)
            {
                bossInstance[f] = new EnemyObject(0, 0, boss1hp, boss1dmg, false);
            }
            for (int c = 0; c < itemInstance.Length; c++)
            {
                itemInstance[c] = new ItemObject(-1000, 0, 0, false);
            }
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
            await item01.TranslateTo(-1000, 0, 4);
            await item02.TranslateTo(-1000, 0, 4);
            await item03.TranslateTo(-1000, 0, 4);
            await item04.TranslateTo(-1000, 0, 4);
            await item05.TranslateTo(-1000, 0, 4);
            await item06.TranslateTo(-1000, 0, 4);
            await item07.TranslateTo(-1000, 0, 4);
            await item08.TranslateTo(-1000, 0, 4);
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
            await sprintbutton.TranslateTo(-10, 50,4);
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
            await sprintbutton.ScaleTo(0.9, 4);
            await weaponswitchbutton.ScaleTo(0.8, 4);
            await backgroundammotext.ScaleTo(1.3, 4);
            await backgroundweaponmenu01.ScaleTo(4.5, 4);


            this.Resources["ItemImageR01"] = "ammunitionlooticon01.png";
            this.Resources["MagicBarValue"] = 140;
            this.Resources["HealthBarValue"] = 140;
            this.Resources["StaminaBarValue"] = 140;
            this.Resources["ColourOfSprintBTNClicked"] = Colors.LightBlue;
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
            hideiteminstances01();
            hideprojectileinstances();
            hideplayerui();
            
        }
        
        async void hideplayer()
        {
            await PlayerIMG.FadeTo(0, 5);
            await PlayerHitBox.FadeTo(0, 5);
            await PlayerCameraBox.FadeTo(0, 5);
            await PlayerHitBoxtopleft.FadeTo(0, 5);
            await PlayerHitBoxtopright.FadeTo(0, 5);
            await PlayerHitBoxbotleft.FadeTo(0, 5);
            await PlayerHitBoxbotright.FadeTo(0, 5);
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
            await e009.FadeTo(0, 5);
            await e010.FadeTo(0, 5);
            await e011.FadeTo(0, 5);
            await e012.FadeTo(0, 5);
            await e013.FadeTo(0, 5);
            await e014.FadeTo(0, 5);
            await e015.FadeTo(0, 5);
            await e016.FadeTo(0, 5);
            await b01.FadeTo(0, 5); 
        }
        async void hideiteminstances01()
        {
            await item01.FadeTo(0, 5);
            await item02.FadeTo(0, 5);
            await item03.FadeTo(0, 5);
            await item04.FadeTo(0, 5);
            await item05.FadeTo(0, 5);
            await item06.FadeTo(0, 5);
            await item07.FadeTo(0, 5);
            await item08.FadeTo(0, 5);
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
            await sprintbutton.FadeTo(0, 5);
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
            showiteminstances01();
            showprojectileinstances();
            showplayerui();

        }
        async void showplayer()
        {
            await PlayerIMG.FadeTo(1, 5);
            //await PlayerHitBox.FadeTo(1, 5);
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
            await e009.FadeTo(1, 5);
            await e010.FadeTo(1, 5);
            await e011.FadeTo(1, 5);
            await e012.FadeTo(1, 5);
            await e013.FadeTo(1, 5);
            await e014.FadeTo(1, 5);
            await e015.FadeTo(1, 5);
            await e016.FadeTo(1, 5);
            await b01.FadeTo(1, 5);
        }
        async void showiteminstances01()
        {
            await item01.FadeTo(1, 5);
            await item02.FadeTo(1, 5);
            await item03.FadeTo(1, 5);
            await item04.FadeTo(1, 5);
            await item05.FadeTo(1, 5);
            await item06.FadeTo(1, 5);
            await item07.FadeTo(1, 5);
            await item08.FadeTo(1, 5);
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
            await sprintbutton.FadeTo(1, 5);
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
            await OutOfOrderscreen.TranslateTo(0, -1000, 5);
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
            await blockadescreen01.TranslateTo(55, (55 - 1000), 5);
            await blockadescreen02.TranslateTo(290, (55 - 1000), 5);
            await blockadescreen03.TranslateTo(-255, (55 - 1000), 5);
            await levelportrait01.TranslateTo(-225, (35 - 1000), 5);
            await levelportrait02.TranslateTo(55, (55 - 1000), 5);
            await levelportrait03.TranslateTo(290, (55 - 1000), 5);
            await levelportrait04.TranslateTo(290, -1055, 5);
            await blockadescreen01.ScaleTo(0.8, 5);
            await blockadescreen02.ScaleTo(0.8, 5);
            await blockadescreen03.ScaleTo(1.1, 5);
            await levelportrait01.ScaleTo(0.7, 5);
            await levelportrait02.ScaleTo(0.7, 5);
            await levelportrait03.ScaleTo(0.7, 5);
            await levelportrait04.ScaleTo(0.7, 5);
            await blockadescreen01.FadeTo(0.5, 5);
            await blockadescreen02.FadeTo(0.5, 5);
            await blockadescreen03.FadeTo(0, 5);
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

        private void tutorialsetup()
        {
            tutorialsetup_01();
            tutorialdynamictext.Text = $"PlaceHolder Text";
        }
        async void tutorialsetup_01()
        {
            await TutorialBox01.TranslateTo(0, -1000, 5);
            await TutorialBox02.TranslateTo(0, -1000, 5);
            await tutorialdynamictext.TranslateTo(0, -1000, 5);
            await Tutorialbutton.TranslateTo(0, -1000, 5);
        }
        async void ItemOffSet()
        {
            for (int itemN = 0; itemN < itemInstance.Length; itemN++)
            {
                itemInstance[itemN].xposition += -1000;
                itemInstance[itemN].yposition += -1000;
                itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
                itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
                await Task.Delay(4);
            }
        }
        // button stuff
        private void SprintBTN_Clicked(object sender, EventArgs e) // while pressed
        {
            if (gamestatus != 0)
            {
                if (sprintSwitch == 0 && playerStaminaPoints >= 1 && delay == 0)
                {
                    this.Resources["ColourOfSprintBTNClicked"] = Colors.Navy;
                    playerMoveamount = 3;
                    sprintSwitch = 1;
                }
                else if (sprintSwitch == 1)
                {
                    this.Resources["ColourOfSprintBTNClicked"] = Colors.LightBlue;
                    playerMoveamount = 1;
                    sprintSwitch = 0;
                }
            }
        }
        private void MoveBTN_Clicked(object sender, EventArgs e) // while pressed
        {
            isMoving[0] = 1;

            if (gamestatus != 0)
            {
                this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.Navy;
                Move_player();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }

        }
        private void MoveBTN_UnClicked(object sender, EventArgs e) 
        {
            isMoving[0] = 0;
        }
        async void Move_player()
        {
            while (isMoving[0] == 1)
            {
                CurrentPlayerPositionY = CurrentPlayerPositionY - playerMoveamount;

                if (CurrentPlayerPositionY <= -220)
                {
                    CurrentPlayerPositionY = -219;

                }
                if (sprintSwitch == 1 && playerStaminaPoints >=1)
                {
                    playerStaminaPoints = (playerStaminaPoints - 2);
                }
                await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
            this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerLeft()
        {
            while (isMoving[1] == 1)
            {
                CurrentPlayerPositionX = CurrentPlayerPositionX - playerMoveamount;

                if (CurrentPlayerPositionX <= -440)
                {
                    CurrentPlayerPositionX = -439;
                }
                if (sprintSwitch == 1 && playerStaminaPoints >= 1)
                {
                    playerStaminaPoints = (playerStaminaPoints - 2);
                }
                await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
            this.Resources["ColourOfLeftMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerRight()
        {
            while (isMoving[2] == 1)
            {
                CurrentPlayerPositionX = CurrentPlayerPositionX + playerMoveamount;

                if (CurrentPlayerPositionX >= 440)
                {
                    CurrentPlayerPositionX = 439;
                }
                if (sprintSwitch == 1 && playerStaminaPoints >= 1)
                {
                    playerStaminaPoints = (playerStaminaPoints - 2);
                }
                await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
            this.Resources["ColourOfRightMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerBack()// split the 3 player parts moving seperately so they all move at once together
        {
            while (isMoving[3] == 1)
            {
                CurrentPlayerPositionY = CurrentPlayerPositionY + playerMoveamount;

                if (CurrentPlayerPositionY >= 220)
                {
                    CurrentPlayerPositionY = 219;
                }
                if (sprintSwitch == 1 && playerStaminaPoints >= 1)
                {
                    playerStaminaPoints = (playerStaminaPoints - 2);
                }
                await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
            this.Resources["ColourOfBackMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_player_Hit_Box()
        {
            while (isMoving[0] == 1 || isMoving[1] == 1 || isMoving[2] == 1 || isMoving[3] == 1 )
            {
                //CurrentPlayerPositionY = CurrentPlayerPositionY - 15;
                await PlayerHitBox.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
        }
        async void Move_player_Camera_Box()
        {
            while (isMoving[0] == 1 || isMoving[1] == 1 || isMoving[2] == 1 || isMoving[3] == 1)
            {
                //CurrentPlayerPositionY = CurrentPlayerPositionY - 15;
                await PlayerCameraBox.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
        }
        private void LeftMoveBTN_Clicked(object sender, EventArgs e)
        {
            isMoving[1] = 1;
            if (gamestatus != 0)
            {
                this.Resources["ColourOfLeftMoveBTNClicked"] = Colors.Navy;
                Move_playerLeft();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        private void LeftMoveBTN_UnClicked(object sender, EventArgs e) 
        {
            isMoving[1] = 0;
        }
        private void RightMoveBTN_Clicked(object sender, EventArgs e)
        {
            isMoving[2] = 1;
            if (gamestatus != 0)
            {
                this.Resources["ColourOfRightMoveBTNClicked"] = Colors.Navy;
                Move_playerRight();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        private void RightMoveBTN_UnClicked(object sender, EventArgs e)
        {
            isMoving[2] = 0;
        }
        private void BackMoveBTN_Clicked(object sender, EventArgs e)
        {
            isMoving[3] = 1;
            if (gamestatus != 0)
            {
                this.Resources["ColourOfBackMoveBTNClicked"] = Colors.Navy;
                Move_playerBack();
                Move_player_Hit_Box();
                Move_player_Camera_Box();
            }
        }
        private void BackMoveBTN_UnClicked(object sender, EventArgs e)
        {
            isMoving[3] = 0;
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
            if (weaponEquipped == 0) // gun 1
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
            if (weaponEquipped == 1) // gun 2
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
            if (weaponEquipped == 2) // gun 3
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
            if (weaponEquipped == 3) // gun 4
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
            if (weaponEquipped == 4) // gun 5
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
            if (weaponEquipped == 5) // gun 6
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
                    canHit[0] = 1;
                    --ammunition01;

                    activeprojectileposition01x[0] = CurrentPlayerPositionX;
                    activeprojectileposition01y[0] = CurrentPlayerPositionY;
                    await Projectile01.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile01.FadeTo(1, 1);
                    for (int i = 0; i < 100; i++)
                    {
                        for(int j = 0; j < enemyInstance.Length; j++)// hit tracking
                        {
                            bool hit1 = enemyInstance[j].ProjectileCollide(activeprojectileposition01x[0], activeprojectileposition01y[0]);
                            if (hit1 == true && canHit[0] == 1)
                            {
                                canHit[0] = 2;
                                int alive1 = enemyInstance[j].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(0);
                                if (alive1 == 0)
                                {
                                    
                                    EnemyDying(j);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[0], activeprojectileposition01y[0]);// boss specific
                        if (hit02 == true && canHit[0] == 1)
                        {
                            canHit[0] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(0);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }


                        if (activeprojectileposition01y[0] >= -390)// movement tracking
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
                    canHit[1] = 1;
                    --ammunition01;

                    activeprojectileposition01x[1] = CurrentPlayerPositionX;
                    activeprojectileposition01y[1] = CurrentPlayerPositionY;
                    await Projectile02.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile02.FadeTo(1, 1);
                    for (int h = 0; h < 100; h++)
                    {
                        for (int js = 0; js < enemyInstance.Length; js++)// hit tracking
                        {
                            bool hit2 = enemyInstance[js].ProjectileCollide(activeprojectileposition01x[1], activeprojectileposition01y[1]);

                            if (hit2 == true && canHit[1] == 1)
                            {
                                canHit[1] = 2;
                                int alive1 = enemyInstance[js].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(1);
                                if (alive1 == 0)
                                {
                                    EnemyDying(js);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[1], activeprojectileposition01y[1]);// boss specific
                        if (hit02 == true && canHit[1] == 1)
                        {
                            canHit[1] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(1);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }
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
                    canHit[2] = 1;
                    --ammunition01;

                    activeprojectileposition01x[2] = CurrentPlayerPositionX;
                    activeprojectileposition01y[2] = CurrentPlayerPositionY;
                    await Projectile03.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile03.FadeTo(1, 1);
                    for (int g = 0; g < 100; g++)
                    {
                        for (int jm = 0; jm < enemyInstance.Length; jm++)// hit tracking
                        {
                            bool hit3 = enemyInstance[jm].ProjectileCollide(activeprojectileposition01x[2], activeprojectileposition01y[2]);
                            if (hit3 == true && canHit[2] == 1)
                            {
                                canHit[2] = 2;
                                int alive1 = enemyInstance[jm].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(2);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jm);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[2], activeprojectileposition01y[2]);// boss specific
                        if (hit02 == true && canHit[2] == 1)
                        {
                            canHit[2] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(2);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    }// end move loop
                    await Projectile03.FadeTo(0, 40);
                    activeprojectileposition01x[2] = activeprojectileposition01x[2] + 1000;
                    await Projectile03.TranslateTo(activeprojectileposition01x[2], activeprojectileposition01y[2], 1);
                    break;
                case 4:
                    canHit[3] = 1;
                    --ammunition01;

                    activeprojectileposition01x[3] = CurrentPlayerPositionX;
                    activeprojectileposition01y[3] = CurrentPlayerPositionY;
                    await Projectile04.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile04.FadeTo(1, 1);
                    for (int b = 0; b < 100; b++)
                    {
                        for (int jgh = 0; jgh < enemyInstance.Length; jgh++)// hit tracking
                        {
                            bool hit4 = enemyInstance[jgh].ProjectileCollide(activeprojectileposition01x[3], activeprojectileposition01y[3]);
                            if (hit4 == true && canHit[3] == 1)
                            {
                                canHit[3] = 2;
                                int alive1 = enemyInstance[jgh].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(3);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jgh);
                                    break;
                                }
                                break;
                            }

                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[3], activeprojectileposition01y[3]);// boss specific
                        if (hit02 == true && canHit[3] == 1)
                        {
                            canHit[3] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(3);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[4] = 1;
                    --ammunition01;

                    activeprojectileposition01x[4] = CurrentPlayerPositionX;
                    activeprojectileposition01y[4] = CurrentPlayerPositionY;
                    await Projectile05.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile05.FadeTo(1, 1);
                    for (int z = 0; z < 100; z++)
                    {
                        for (int j2 = 0; j2 < enemyInstance.Length; j2++)// hit tracking
                        {
                            bool hit5 = enemyInstance[j2].ProjectileCollide(activeprojectileposition01x[4], activeprojectileposition01y[4]);
                            if (hit5 == true && canHit[4] == 1)
                            {
                                canHit[4] = 2;
                                int alive1 = enemyInstance[j2].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(4);
                                if (alive1 == 0)
                                {
                                    EnemyDying(j2);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[4], activeprojectileposition01y[4]);// boss specific
                        if (hit02 == true && canHit[4] == 1)
                        {
                            canHit[4] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(4);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[5] = 1;
                    --ammunition01;

                    activeprojectileposition01x[5] = CurrentPlayerPositionX;
                    activeprojectileposition01y[5] = CurrentPlayerPositionY;
                    await Projectile06.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile06.FadeTo(1, 1);
                    for (int x = 0; x < 100; x++)
                    {
                        for (int jy = 0; jy < enemyInstance.Length; jy++)
                        {
                            bool hit6 = enemyInstance[jy].ProjectileCollide(activeprojectileposition01x[5], activeprojectileposition01y[5]);
                            if (hit6 == true && canHit[5] == 1)
                            {
                                canHit[5] = 2;
                                int alive1 = enemyInstance[jy].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(5);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jy);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[5], activeprojectileposition01y[5]);// boss specific
                        if (hit02 == true && canHit[5] == 1)
                        {
                            canHit[5] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(5);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[6] = 1;
                    --ammunition01;

                    activeprojectileposition01x[6] = CurrentPlayerPositionX;
                    activeprojectileposition01y[6] = CurrentPlayerPositionY;
                    await Projectile07.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile07.FadeTo(1, 1);
                    for (int v = 0; v < 100; v++)
                    {
                        for (int jf = 0; jf < enemyInstance.Length; jf++)
                        {
                            bool hit7 = enemyInstance[jf].ProjectileCollide(activeprojectileposition01x[6], activeprojectileposition01y[6]);
                            if (hit7 == true && canHit[6] == 1)
                            {
                                canHit[6] = 2;
                                int alive1 = enemyInstance[jf].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(6);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jf);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[6], activeprojectileposition01y[6]);// boss specific
                        if (hit02 == true && canHit[6] == 1)
                        {
                            canHit[6] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(6);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[7] = 1;
                    --ammunition01;

                    activeprojectileposition01x[7] = CurrentPlayerPositionX;
                    activeprojectileposition01y[7] = CurrentPlayerPositionY;
                    await Projectile08.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile08.FadeTo(1, 1);
                    for (int q = 0; q < 100; q++)
                    {
                        for (int jv = 0; jv < enemyInstance.Length; jv++)
                        {
                            bool hit8 = enemyInstance[jv].ProjectileCollide(activeprojectileposition01x[7], activeprojectileposition01y[7]);
                            if (hit8 == true && canHit[7] == 1)
                            {
                                canHit[7] = 2;
                                int alive1 = enemyInstance[jv].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(7);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jv);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[7], activeprojectileposition01y[7]);// boss specific
                        if (hit02 == true && canHit[7] == 1)
                        {
                            canHit[7] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(7);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[8] = 1;
                    --ammunition01;

                    activeprojectileposition01x[8] = CurrentPlayerPositionX;
                    activeprojectileposition01y[8] = CurrentPlayerPositionY;
                    await Projectile09.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile09.FadeTo(1, 1);
                    for (int t = 0; t < 100; t++)
                    {
                        for (int jt = 0; jt < enemyInstance.Length; jt++)
                        {
                            bool hit9 = enemyInstance[jt].ProjectileCollide(activeprojectileposition01x[8], activeprojectileposition01y[8]);
                            if (hit9 == true && canHit[8] == 1)
                            {
                                canHit[8] = 2;
                                int alive1 = enemyInstance[jt].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(8);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jt);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[8], activeprojectileposition01y[8]);// boss specific
                        if (hit02 == true && canHit[8] == 1)
                        {
                            canHit[8] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(8);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[9] = 1;
                    --ammunition01;

                    activeprojectileposition01x[9] = CurrentPlayerPositionX;
                    activeprojectileposition01y[9] = CurrentPlayerPositionY;
                    await Projectile10.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile10.FadeTo(1, 1);
                    for (int j = 0; j < 100; j++)
                    {
                        for (int jk = 0; jk < enemyInstance.Length; jk++)
                        {
                            bool hit10 = enemyInstance[jk].ProjectileCollide(activeprojectileposition01x[9], activeprojectileposition01y[9]);
                            if (hit10 == true && canHit[9] == 1)
                            {
                                canHit[9] = 2;
                                int alive1 = enemyInstance[jk].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(9);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jk);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[9], activeprojectileposition01y[9]);// boss specific
                        if (hit02 == true && canHit[9] == 1)
                        {
                            canHit[9] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(9);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[10] = 1;
                    --ammunition01;

                    activeprojectileposition01x[10] = CurrentPlayerPositionX;
                    activeprojectileposition01y[10] = CurrentPlayerPositionY;
                    await Projectile11.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile11.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        for (int jl = 0; jl < enemyInstance.Length; jl++)
                        {
                            bool hit11 = enemyInstance[jl].ProjectileCollide(activeprojectileposition01x[10], activeprojectileposition01y[10]);
                            if (hit11 == true && canHit[10] == 1)
                            {
                                canHit[10] = 2;
                                int alive1 = enemyInstance[jl].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(10);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jl);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[10], activeprojectileposition01y[10]);// boss specific
                        if (hit02 == true && canHit[10] == 1)
                        {
                            canHit[10] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(10);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[11] = 1;
                    --ammunition01;

                    activeprojectileposition01x[11] = CurrentPlayerPositionX;
                    activeprojectileposition01y[11] = CurrentPlayerPositionY;
                    await Projectile12.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile12.FadeTo(1, 1);
                    for (int ha = 0; ha < 100; ha++)
                    {
                        for (int jp = 0; jp < enemyInstance.Length; jp++)
                        {
                            bool hit12 = enemyInstance[jp].ProjectileCollide(activeprojectileposition01x[11], activeprojectileposition01y[11]);
                            if (hit12 == true && canHit[11] == 1)
                            {
                                canHit[11] = 2;
                                int alive1 = enemyInstance[jp].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(11);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jp);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[11], activeprojectileposition01y[11]);// boss specific
                        if (hit02 == true && canHit[11] == 1)
                        {
                            canHit[11] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(11);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

                        if (activeprojectileposition01y[11] >= -390)
                        {
                            //activeprojectilepositionX= activeprojectilepositionX + 5;
                            activeprojectileposition01y[11] = activeprojectileposition01y[11] - 8;
                            await Projectile12.TranslateTo(activeprojectileposition01x[11], activeprojectileposition01y[11], 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    await Projectile12.FadeTo(0, 40);
                    activeprojectileposition01x[11] = activeprojectileposition01x[11] + 1000;
                    await Projectile12.TranslateTo(activeprojectileposition01x[11], activeprojectileposition01y[11], 1);
                    break;
                case 13:
                    canHit[12] = 1;
                    --ammunition01;

                    activeprojectileposition01x[12] = CurrentPlayerPositionX;
                    activeprojectileposition01y[12] = CurrentPlayerPositionY;
                    await Projectile13.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile13.FadeTo(1, 1);
                    for (int ga = 0; ga < 100; ga++)
                    {
                        for (int jq = 0; jq < enemyInstance.Length; jq++)
                        {
                            bool hit13 = enemyInstance[jq].ProjectileCollide(activeprojectileposition01x[12], activeprojectileposition01y[12]);
                            if (hit13 == true && canHit[12] == 1)
                            {
                                canHit[12] = 2;
                                int alive1 = enemyInstance[jq].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(12);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jq);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[12], activeprojectileposition01y[12]);// boss specific
                        if (hit02 == true && canHit[12] == 1)
                        {
                            canHit[12] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(12);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[13] = 1;
                    --ammunition01;

                    activeprojectileposition01x[13] = CurrentPlayerPositionX;
                    activeprojectileposition01y[13] = CurrentPlayerPositionY;
                    await Projectile14.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile14.FadeTo(1, 1);
                    for (int ba = 0; ba < 100; ba++)
                    {
                        for (int jr = 0; jr < enemyInstance.Length; jr++)
                        {
                            bool hit14 = enemyInstance[jr].ProjectileCollide(activeprojectileposition01x[13], activeprojectileposition01y[13]);
                            if (hit14 == true && canHit[13] == 1)
                            {
                                canHit[13] = 2;
                                int alive1 = enemyInstance[jr].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(13);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jr);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[13], activeprojectileposition01y[13]);// boss specific
                        if (hit02 == true && canHit[13] == 1)
                        {
                            canHit[13] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(13);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[14] = 1;
                    --ammunition01;

                    activeprojectileposition01x[14] = CurrentPlayerPositionX;
                    activeprojectileposition01y[14] = CurrentPlayerPositionY;
                    await Projectile15.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile15.FadeTo(1, 1);
                    for (int za = 0; za < 100; za++)
                    {
                        for (int ju = 0; ju < enemyInstance.Length; ju++)
                        {
                            bool hit15 = enemyInstance[ju].ProjectileCollide(activeprojectileposition01x[14], activeprojectileposition01y[14]);
                            if (hit15 == true && canHit[14] == 1)
                            {
                                canHit[14] = 2;
                                int alive1 = enemyInstance[ju].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(14);
                                if (alive1 == 0)
                                {
                                    EnemyDying(ju);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[14], activeprojectileposition01y[14]);// boss specific
                        if (hit02 == true && canHit[14] == 1)
                        {
                            canHit[14] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(14);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[15] = 1;
                    --ammunition01;

                    activeprojectileposition01x[15] = CurrentPlayerPositionX;
                    activeprojectileposition01y[15] = CurrentPlayerPositionY;
                    await Projectile16.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile16.FadeTo(1, 1);
                    for (int x = 0; x < 100; x++)
                    {
                        for (int zj = 0; zj < enemyInstance.Length; zj++)
                        {
                            bool hit16 = enemyInstance[zj].ProjectileCollide(activeprojectileposition01x[15], activeprojectileposition01y[15]);
                            if (hit16 == true && canHit[15] == 1)
                            {
                                canHit[15] = 2;
                                int alive1 = enemyInstance[zj].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(15);
                                if (alive1 == 0)
                                {
                                    EnemyDying(zj);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[15], activeprojectileposition01y[15]);// boss specific
                        if (hit02 == true && canHit[15] == 1)
                        {
                            canHit[15] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(15);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[16] = 1;
                    --ammunition01;

                    activeprojectileposition01x[16] = CurrentPlayerPositionX;
                    activeprojectileposition01y[16] = CurrentPlayerPositionY;
                    await Projectile17.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile17.FadeTo(1, 1);
                    for (int v = 0; v < 100; v++)
                    {
                        for (int nj = 0; nj < enemyInstance.Length; nj++)
                        {
                            bool hit17 = enemyInstance[nj].ProjectileCollide(activeprojectileposition01x[16], activeprojectileposition01y[16]);
                            if (hit17 == true && canHit[16] == 1)
                            {
                                canHit[16] = 2;
                                int alive1 = enemyInstance[nj].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(16);
                                if (alive1 == 0)
                                {
                                    EnemyDying(nj);
                                    break;
                                }
                                break;
                            }

                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[16], activeprojectileposition01y[16]);// boss specific
                        if (hit02 == true && canHit[16] == 1)
                        {
                            canHit[16] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(16);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[17] = 1;
                    --ammunition01;

                    activeprojectileposition01x[17] = CurrentPlayerPositionX;
                    activeprojectileposition01y[17] = CurrentPlayerPositionY;
                    await Projectile18.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile18.FadeTo(1, 1);
                    for (int q = 0; q < 100; q++)
                    {
                        for (int mj = 0; mj < enemyInstance.Length; mj++)
                        {
                            bool hit18 = enemyInstance[mj].ProjectileCollide(activeprojectileposition01x[17], activeprojectileposition01y[17]);
                            if (hit18 == true && canHit[17] == 1)
                            {
                                canHit[17] = 2;
                                int alive1 = enemyInstance[mj].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(17);
                                if (alive1 == 0)
                                {
                                    EnemyDying(mj);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[17], activeprojectileposition01y[17]);// boss specific
                        if (hit02 == true && canHit[17] == 1)
                        {
                            canHit[17] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(17);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[18] = 1;
                    --ammunition01;

                    activeprojectileposition01x[18] = CurrentPlayerPositionX;
                    activeprojectileposition01y[18] = CurrentPlayerPositionY;
                    await Projectile19.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile19.FadeTo(1, 1);
                    for (int t = 0; t < 100; t++)
                    {
                        for (int jw = 0; jw < enemyInstance.Length; jw++)
                        {
                            bool hit19 = enemyInstance[jw].ProjectileCollide(activeprojectileposition01x[18], activeprojectileposition01y[18]);
                            if (hit19 == true && canHit[18] == 1)
                            {
                                canHit[18] = 2;
                                int alive1 = enemyInstance[jw].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(18);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jw);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[18], activeprojectileposition01y[18]);// boss specific
                        if (hit02 == true && canHit[18] == 1)
                        {
                            canHit[18] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(18);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[19] = 1;
                    --ammunition01;

                    activeprojectileposition01x[19] = CurrentPlayerPositionX;
                    activeprojectileposition01y[19] = CurrentPlayerPositionY;
                    await Projectile20.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile20.FadeTo(1, 1);
                    for (int j = 0; j < 100; j++)
                    {
                        for (int jj = 0; jj < enemyInstance.Length; jj++)
                        {
                            bool hit20 = enemyInstance[jj].ProjectileCollide(activeprojectileposition01x[19], activeprojectileposition01y[19]);
                            if (hit20 == true && canHit[19] == 1)
                            {
                                canHit[19] = 2;
                                int alive1 = enemyInstance[jj].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(19);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jj);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[19], activeprojectileposition01y[19]);// boss specific
                        if (hit02 == true && canHit[19] == 1)
                        {
                            canHit[19] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(19);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[20] = 1;
                    --ammunition01;

                    activeprojectileposition01x[20] = CurrentPlayerPositionX;
                    activeprojectileposition01y[20] = CurrentPlayerPositionY;
                    await Projectile21.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile21.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        for (int jb = 0; jb < enemyInstance.Length; jb++)
                        {
                            bool hit21 = enemyInstance[jb].ProjectileCollide(activeprojectileposition01x[20], activeprojectileposition01y[20]);
                            if (hit21 == true && canHit[20] == 1)
                            {
                                canHit[20] = 2;
                                int alive1 = enemyInstance[jb].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(20);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jb);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[20], activeprojectileposition01y[20]);// boss specific
                        if (hit02 == true && canHit[20] == 1)
                        {
                            canHit[20] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(20);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
                    canHit[21] = 1;
                    --ammunition01;

                    activeprojectileposition01x[21] = CurrentPlayerPositionX;
                    activeprojectileposition01y[21] = CurrentPlayerPositionY;
                    await Projectile22.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                    await Projectile22.FadeTo(1, 1);
                    for (int k = 0; k < 100; k++)
                    {
                        for (int jg = 0; jg < enemyInstance.Length; jg++)
                        {
                            bool hit22 = enemyInstance[jg].ProjectileCollide(activeprojectileposition01x[21], activeprojectileposition01y[21]);
                            if (hit22 == true && canHit[21] == 1)
                            {
                                canHit[21] = 2;
                                int alive1 = enemyInstance[jg].TakeDamage(playerDamageValue);
                                Remove_Projectile_cur(21);
                                if (alive1 == 0)
                                {
                                    EnemyDying(jg);
                                    break;
                                }
                                break;
                            }
                        }

                        bool hit02 = bossInstance[0].ProjectileCollide(activeprojectileposition01x[21], activeprojectileposition01y[21]);// boss specific
                        if (hit02 == true && canHit[21] == 1)
                        {
                            canHit[21] = 2;
                            int alive1 = bossInstance[0].TakeDamage(playerDamageValue);
                            Remove_Projectile_cur(21);
                            if (alive1 == 0)
                            {

                                EnemyDying(101);
                                break;
                            }
                            break;
                        }

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
        private void GameMenuBTN_Clicked(object sender, EventArgs e)// ( does not pause the game )
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
                if (weaponMenuedSwitch == 0)
                {
                    Weapon_menu_Open();
                    weaponMenuedSwitch = 1;
                    this.Resources["ColourOfWeaponSwitchBTNClicked"] = Colors.DarkGoldenrod;
                }
                else if (weaponMenuedSwitch == 1)
                {
                    Weapon_menu_Close();
                    weaponMenuedSwitch = 0;
                    this.Resources["ColourOfWeaponSwitchBTNClicked"] = Colors.Yellow;
                }
            }

        }
        private void Weapon1BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponEquipped != 0)
            {
                if(weaponowned01 == 1)
                {
                    weaponEquipped = 0;
                }
                this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponEquipped == 0)
            {
                this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon2BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponEquipped != 1)
            {
                if (weaponowned02 == 1)
                {
                    weaponEquipped = 1;
                }
                this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponEquipped == 1)
            {
                this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon3BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponEquipped != 2)
            {
                if (weaponowned03 == 1)
                {
                    weaponEquipped = 2;
                }
                this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponEquipped == 2)
            {
                this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon4BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponEquipped != 3)
            {
                if (weaponowned04 == 1)
                {
                    weaponEquipped = 3;
                }
                this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponEquipped == 3)
            {
                this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon5BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponEquipped != 4)
            {
                if (weaponowned05 == 1)
                {
                    weaponEquipped = 4;
                }
                this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponEquipped == 4)
            {
                this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateBlue;
            }
            
        }
        private void Weapon6BTN_Clicked(object sender, EventArgs e)
        {
            if (weaponEquipped != 5)
            {
                if (weaponowned06 == 1)
                {
                    weaponEquipped = 5;
                }
                this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateGrey;
            }
            else if (weaponEquipped == 5)
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

            playercollisiontopY = (CurrentPlayerPositionY - 35);
            for (int i = 0; i < enemyInstance.Length; i++)
            {
                CheckEnemyColl01(i);
                //testnumberT++;
            }
            for (int j = 0;j < itemInstance.Length; j++)
            {
                CheckItemColl01(j);
            }
        }
        // player to enemy collision

        async void CheckEnemyColl01(int enemyN)
        {
            //testnumberT++;
            int canAtt = 1;
            float enemyDamage = 0;
            bool contact = false;
            contact = enemyInstance[enemyN].PlayerCollide(CurrentPlayerPositionX, CurrentPlayerPositionY);
            if (contact == true)
            {
                //testnumberT++;
                enemyDamage = enemyInstance[enemyN].EnemyDealDamage();
                if (canAtt == 1)
                {
                    playerHealthPoints += (-enemyDamage);
                    canAtt = 0;
                }

            }
        }
        async void CheckItemColl01(int itemN)
        {
            //testnumberT++;
            int canTouch = 1, itemD = 0;
            bool contact = false;
            contact = itemInstance[itemN].PlayerCollide(CurrentPlayerPositionX, CurrentPlayerPositionY);
            if (contact == true)
            {
                //testnumberT++;
                itemD = itemInstance[itemN].DroppedType();
                if (canTouch == 1)
                {
                    if (itemD == 2)
                    {
                        ammunition01 += 20;
                    }
                    else if (itemD == 3)
                    {
                        ammunition02 += 20;
                    }
                    else if (itemD == 4)
                    {
                        ammunition03 += 20;
                    }
                    else if (itemD == 5)
                    {
                        ammunition04 += 20;
                    }
                    else if (itemD == 6)
                    {
                        ammunition05 += 20;
                    }
                    else if (itemD == 7)
                    {
                        ammunition06 += 20;
                    }
                    else if (itemD == 8)
                    {
                        // non - ammo drop
                    }
                    else if (itemD == 9)
                    {
                        // non - ammo drop
                    }
                    else if (itemD == 1)
                    {
                        // non - ammo drop
                    }
                    canTouch = 0;
                    itemInstance[itemN].xposition += -1000;
                    itemInstance[itemN].yposition += -1000;
                    itemInstance[itemN].xleftposition = itemInstance[itemN].xposition-25;
                    itemInstance[itemN].xrightposition = itemInstance[itemN].xposition+25;
                }

            }
        }


        // resets object positions
        async void Remove_Projectile_cur(int imputprojectile)
        {
            switch (imputprojectile)
            {
                case 0:
                    Reset_PPos_01();
                    break;
                case 1:
                    Reset_PPos_02();
                    break;
                case 2:
                    Reset_PPos_03();
                    break;
                case 3:
                    Reset_PPos_04();
                    break;
                case 4:
                    Reset_PPos_05();
                    break;
                case 5:
                    Reset_PPos_06();
                    break;
                case 6:
                    Reset_PPos_07();
                    break;
                case 7:
                    Reset_PPos_08();
                    break;
                case 8:
                    Reset_PPos_09();
                    break;
                case 9:
                    Reset_PPos_10();
                    break;
                case 10:
                    Reset_PPos_11();
                    break;
                case 11:
                    Reset_PPos_12();
                    break;
                case 12:
                    Reset_PPos_13();
                    break;
                case 13:
                    Reset_PPos_14();
                    break;
                case 14:
                    Reset_PPos_15();
                    break;
                case 15:
                    Reset_PPos_16();
                    break;
                case 16:
                    Reset_PPos_17();
                    break;
                case 17:
                    Reset_PPos_18();
                    break;
                case 18:
                    Reset_PPos_19();
                    break;
                case 19:
                    Reset_PPos_20();
                    break;
                case 20:
                    Reset_PPos_21();
                    break;
                case 21:
                    Reset_PPos_22();
                    break;
            }
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
        private void DifficultyChanger()
        {
            if (difficultysetting == 1)
            {
                for (int i = 0; i < enemyInstance.Length; i++)
                {
                    enemyInstance[i].healthpoints = enemytype1hp;
                }
                for (int i = 0; i < eliteEnemyInstance.Length; i++)
                {
                    eliteEnemyInstance[i].healthpoints = enemytype2hp;
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp;
                }


            }
            else if (difficultysetting == 2)
            {
                for (int i = 0; i < enemyInstance.Length; i++)
                {
                    enemyInstance[i].healthpoints = enemytype1hp*2;
                }
                for (int i = 0; i < eliteEnemyInstance.Length; i++)
                {
                    eliteEnemyInstance[i].healthpoints = enemytype2hp + (enemytype2hp / 2);
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp+ (boss1hp/2);
                }
            }
            else if (difficultysetting == 3)
            {
                for (int i = 0; i < enemyInstance.Length; i++)
                {
                    enemyInstance[i].healthpoints = enemytype1hp * 3;
                }
                for (int i = 0; i < eliteEnemyInstance.Length; i++)
                {
                    eliteEnemyInstance[i].healthpoints = enemytype2hp * 2;
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp + (boss1hp / 2);
                    bossInstance[i].damagevar = boss1dmg + (boss1dmg / 2);
                }
            }
            else if (difficultysetting == 4)
            {
                for (int i = 0; i < enemyInstance.Length; i++)
                {
                    enemyInstance[i].healthpoints = enemytype1hp * 4;
                    enemyInstance[i].damagevar = enemytype1dmg * 2;
                }
                for (int i = 0; i < eliteEnemyInstance.Length; i++)
                {
                    eliteEnemyInstance[i].healthpoints = enemytype2hp * 3;
                    eliteEnemyInstance[i].damagevar = enemytype2dmg *2;
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp*2;
                    bossInstance[i].damagevar = boss1dmg*2;
                }
            }
            else if (difficultysetting == 5) // difficulty 5 == brutal mode (hardest)
            {
                for (int i = 0; i < enemyInstance.Length; i++)
                {
                    enemyInstance[i].healthpoints = enemytype1hp * 5;
                    enemyInstance[i].damagevar = enemytype1dmg * 6;
                }
                for (int i = 0; i < eliteEnemyInstance.Length; i++)
                {
                    eliteEnemyInstance[i].healthpoints = enemytype2hp * 5;
                    eliteEnemyInstance[i].damagevar = enemytype2dmg * 4;
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp * 3;
                    bossInstance[i].damagevar = boss1dmg * 4;
                }
            }


        }
        private void PrevMissBTN_Clicked(object sender, EventArgs e)// pos 0: (-450, 35), pos 1: (-225, 35),pos 2: (50, 55),pos 3: (290, 55), scale pos 1: (1.2), pos 2: (0.7)
        {
            if (missionselected == 1) 
            {
                // do nothing
                missionselected = 1;
            }
            else if (missionselected == 2) 
            {
                Previousmission1();
                missionselected = 1;
                Hideblock03();
            }
            else if (missionselected == 3) 
            {
                Previousmission2();
                missionselected = 2;
            }
            else if (missionselected == 4) 
            {
                Previousmission3();
                missionselected = 3;
            }
        }
        async void Hideblock03()
        {
            await blockadescreen03.FadeTo(0, 200);

        }
        private void Previousmission1()
        {
            Previous01();
            Previous02();
            Previous03();
            Previous04();
            Previous05();
            Previous06();
            Previous07();
        }
        async void Previous01()
        {
            await levelportrait01.TranslateTo(-225, 35, 200);

        }
        async void Previous02()
        {
            await levelportrait02.TranslateTo(50, 55, 200);

        }
        async void Previous03()
        {
            await levelportrait03.TranslateTo(290, 55, 200);

        }
        async void Previous04()
        {
            await levelportrait04.TranslateTo(290, 55, 200);
        }
        async void Previous05()
        {
            await levelportrait01.ScaleTo(1.2, 200);
        }
        async void Previous06()
        {
            await levelportrait01.FadeTo(1, 200);
        }
        async void Previous07()
        {
            await levelportrait02.ScaleTo(0.7, 200);
        }
        private void Previousmission2()
        {
            Previous08();
            Previous09();
            Previous10();
            Previous11();
            Previous12();
            Previous13();
            Previous14();
        }
        async void Previous08()
        {
            await levelportrait01.TranslateTo(-450, 55, 200);

        }
        async void Previous09()
        {
            await levelportrait02.TranslateTo(-225, 35, 200);

        }
        async void Previous10()
        {
            await levelportrait03.TranslateTo(50, 55, 200);

        }
        async void Previous11()
        {
            await levelportrait04.TranslateTo(290, 55, 200);
        }
        async void Previous12()
        {
            await levelportrait02.ScaleTo(1.2, 200);
        }
        async void Previous13()
        {
            await levelportrait02.FadeTo(1, 200);
        }
        async void Previous14()
        {
            await levelportrait03.ScaleTo(0.7, 200);
        }
        private void Previousmission3()
        {
            Previous15();
            Previous16();
            Previous17();
            Previous18();
            Previous19();
            Previous20();
            Previous21();
        }
        async void Previous15()
        {
            await levelportrait01.TranslateTo(-450, 55, 200);

        }
        async void Previous16()
        {
            await levelportrait02.TranslateTo(-450, 55, 200);

        }
        async void Previous17()
        {
            await levelportrait03.TranslateTo(-225, 35, 200);

        }
        async void Previous18()
        {
            await levelportrait04.TranslateTo(50, 55, 200);
        }
        async void Previous19()
        {
            await levelportrait03.ScaleTo(1.2, 200);
        }
        async void Previous20()
        {
            await levelportrait03.FadeTo(1, 200);
        }
        async void Previous21()
        {
            await levelportrait04.ScaleTo(0.7, 200);
        }
        private void NextMissBTN_Clicked(object sender, EventArgs e)
        {
            if (missionselected == 1)
            {
                NextMission1();
                missionselected = 2;
                Showblock03();
            }
            else if (missionselected == 2)
            {
                NextMission2();
                missionselected = 3;
                Showblock03();
            }
            else if (missionselected == 3)
            {
                NextMission3();
                missionselected = 4;
                Showblock03();
            }
            else if (missionselected == 4)
            {
                // do nothing
                missionselected = 4;
            }
        }
        async void Showblock03()
        {
            await blockadescreen03.FadeTo(0.5, 200);

        }
        private void NextMission1()
        {
            Next01();
            Next02();
            Next03();
            Next04();
            Next05();
            Next06();
            Next07();
        }

        async void Next01()
        {
            await levelportrait01.TranslateTo(-450, 55, 200);
           
        }
        async void Next02()
        {
            await levelportrait02.TranslateTo(-225, 35, 200);
           
        }
        async void Next03()
        {
            await levelportrait03.TranslateTo(50, 55, 200);
            
        }
        async void Next04()
        {
            await levelportrait04.TranslateTo(290, 55, 200);
        }
        async void Next05()
        {
            await levelportrait01.ScaleTo(0.2, 200);
        }
        async void Next06()
        {
            await levelportrait01.FadeTo(0, 200);
        }
        async void Next07()
        {
            await levelportrait02.ScaleTo(1.2, 200);
        }
        private void NextMission2()
        {
            Next08();
            Next09();
            Next10();
            Next11();
            Next12();
            Next13();
            Next14();
        }
        async void Next08()
        {
            await levelportrait01.TranslateTo(-450, 55, 200);

        }
        async void Next09()
        {
            await levelportrait02.TranslateTo(-450, 55, 200);

        }
        async void Next10()
        {
            await levelportrait03.TranslateTo(-225, 35, 200);

        }
        async void Next11()
        {
            await levelportrait04.TranslateTo(50, 55, 200);
        }
        async void Next12()
        {
            await levelportrait02.ScaleTo(0.2, 200);
        }
        async void Next13()
        {
            await levelportrait02.FadeTo(0, 200);
        }
        async void Next14()
        {
            await levelportrait03.ScaleTo(1.2, 200);
        }
        private void NextMission3()
        {
            Next15();
            Next16();
            Next17();
            Next18();
            Next19();
            Next20();
            Next21();
        }
        async void Next15()
        {
            await levelportrait01.TranslateTo(-450, 55, 200);

        }
        async void Next16()
        {
            await levelportrait02.TranslateTo(-450, 55, 200);

        }
        async void Next17()
        {
            await levelportrait03.TranslateTo(-450, 55, 200);

        }
        async void Next18()
        {
            await levelportrait04.TranslateTo(-225, 35, 200);
        }
        async void Next19()
        {
            await levelportrait03.ScaleTo(0.2, 200);
        }
        async void Next20()
        {
            await levelportrait03.FadeTo(0, 200);
        }
        async void Next21()
        {
            await levelportrait04.ScaleTo(1.2, 200);
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
                Enemy_AI_01();
                Enemy_AI_02();
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
                Enemy_AI_01();
                Enemy_AI_02();
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
                Enemy_AI_01();
                Enemy_AI_02();
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
                Enemy_AI_01();
                Enemy_AI_02();

            }
            difficultysetting = newgamedifficulty;
            gamelevelflag = 1;
            weaponEquipped = 0;
            DifficultyChanger();
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
            Reset_missions_states();
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
            Reset_missions_states();
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
        private void Reset_missions_states()
        {
            Resetmission01();
            missionselected = 1;
        }
        async void Resetmission01()
        {
            await blockadescreen01.ScaleTo(0.8, 5);
            await blockadescreen02.ScaleTo(0.8, 5);
            await blockadescreen03.FadeTo(0, 5);
            await levelportrait01.ScaleTo(0.7, 5);
            await levelportrait02.ScaleTo(0.7, 5);
            await levelportrait03.ScaleTo(0.7, 5);
            await levelportrait04.ScaleTo(0.7, 5);
            await blockadescreen01.FadeTo(0.5, 5);
            await blockadescreen02.FadeTo(0.5, 5);
            await levelportrait01.FadeTo(1, 5);
            await levelportrait02.FadeTo(1, 5);
            await levelportrait03.FadeTo(1, 5);
            await levelportrait04.FadeTo(1, 5);
        }
        // menu animations
        // tutorial stuff
        private void TutorialBTN_Clicked(object sender, EventArgs e)
        {
            tutorialbclicked++;
            if (tutorialbclicked == 1) 
            {
                tutorialdynamictext.Text = $"Your Goal is to get to \nthe end of the map. ";
            }
            if (tutorialbclicked == 2)
            {
                tutorialdynamictext.Text = $"Kill the area boss \nat the end of the map. ";
            }
            if (tutorialbclicked == 3)
            {
                tutorialdynamictext.Text = $"The map will move by itself \n\nGood Luck, Walter. ";
            }
            if (tutorialbclicked == 4)
            {
                this.Resources["TutorialBTNText"] = "Exit";
                Tutorial_dectivate05();
                Tutorial_dectivate06();
            }
            if (tutorialbclicked == 5) 
            {
                
                Tutorial_dectivate01();
                Tutorial_dectivate02();
                Tutorial_dectivate03();
                Tutorial_dectivate04();
            }
           
        }
        async void Tutorial_dectivate01()
        {
            await TutorialBox01.TranslateTo(1200, 100, 500);
            
        }
        async void Tutorial_dectivate02()
        {
            await TutorialBox02.TranslateTo(1000, 0, 500);
            
        }
        async void Tutorial_dectivate03()
        {
            await tutorialdynamictext.TranslateTo(1200, 100, 500);
            
        }
        async void Tutorial_dectivate04()
        {
            await Tutorialbutton.TranslateTo(1000, 160, 500);
        }
        async void Tutorial_dectivate05()
        {
            await TutorialBox01.FadeTo(0, 200);
        }
        async void Tutorial_dectivate06()
        {
            await tutorialdynamictext.FadeTo(0, 200);
        }
        async void Tutorial_activate()
        {
            this.Resources["TutorialBTNText"] = "Next";
            tutorialbclicked = 0;
            await TutorialBox01.TranslateTo(200, 100, 5);
            await TutorialBox02.TranslateTo(0, 0, 5);
            await tutorialdynamictext.TranslateTo(200, 100, 5);
            await Tutorialbutton.TranslateTo(0, 160, 5);
            tutorialdynamictext.Text = $"Welcome to Battle For Azura. ";
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
            await easydiffbutton.ScaleTo(0.75, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
        }
        async void Normal_ClickedAnim()
        {
            await normaldiffbutton.ScaleTo(0.75, 100);
            await easydiffbutton.ScaleTo(0.6, 100);            
            await harddiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
        }
        async void Hard_ClickedAnim()
        {
            await harddiffbutton.ScaleTo(0.75, 100);
            await easydiffbutton.ScaleTo(0.6, 100);
            await normaldiffbutton.ScaleTo(0.6, 100);
            await veryharddiffbutton.ScaleTo(0.6, 100);
        }
        async void VeryHard_ClickedAnim()
        {
            await veryharddiffbutton.ScaleTo(0.75, 100);
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
            SeperatedMenuRetreat37();
            SeperatedMenuRetreat38();
            SeperatedMenuRetreat39();
            SeperatedMenuRetreat40();
            SeperatedMenuRetreat41();
            SeperatedMenuRetreat42();
            SeperatedMenuRetreat43();
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
        async void SeperatedMenuRetreat37()
        {
            await blockadescreen01.TranslateTo(50, (55 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat38()
        {
            await blockadescreen02.TranslateTo(290, (55 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat39()
        {
            await levelportrait01.TranslateTo(-225, (35 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat40()
        {
            await levelportrait02.TranslateTo(55, (55 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat41()
        {
            await levelportrait03.TranslateTo(290, (55 - 1000), 500);
            
        }
        async void SeperatedMenuRetreat42()
        {
            await levelportrait04.TranslateTo(290, -1055, 500);
        }
        async void SeperatedMenuRetreat43()
        {
            await blockadescreen03.TranslateTo(-225, -1035, 500);
        }
        private void MissionMenuReturnAnim()
        {
            SeperatedMenuReturn26();
            SeperatedMenuReturn27();
            SeperatedMenuReturn28();
            SeperatedMenuReturn29();
            SeperatedMenuReturn30();
            SeperatedMenuReturn31();
            SeperatedMenuReturn37();
            SeperatedMenuReturn38();
            SeperatedMenuReturn39();
            SeperatedMenuReturn40();
            SeperatedMenuReturn41();
            SeperatedMenuReturn42();
            SeperatedMenuReturn42_1();
            SeperatedMenuReturn43();
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
        async void SeperatedMenuReturn37()
        {
            await blockadescreen01.TranslateTo(50, 55, 500);
        }
        async void SeperatedMenuReturn38()
        {
            await blockadescreen02.TranslateTo(290, 55, 500);

        }
        async void SeperatedMenuReturn39()// pos 1: (-225, 35),pos 2: (50, 55),pos 3: (290, 55)
        {
            await levelportrait01.TranslateTo(-225, 35, 500);

        }
        async void SeperatedMenuReturn40()
        {
            await levelportrait02.TranslateTo(50, 55, 500);

        }
        async void SeperatedMenuReturn41()
        {
            await levelportrait03.TranslateTo(290, 55, 500);

        }
        async void SeperatedMenuReturn42()
        {
            await levelportrait04.TranslateTo(290, 55, 500);
        }
        async void SeperatedMenuReturn42_1()
        {
            await levelportrait01.ScaleTo(1.2, 1);
        }
        async void SeperatedMenuReturn43()
        {
            await blockadescreen03.TranslateTo(-225, 35, 500);
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
        // RNG lootdrops function
        private void DropItemRNG(int enemyN)
        {
            int itemtype = 1;
            int RNGDropLoot = RNGmove.Next(1, 1000);
            testnumberT = RNGDropLoot;
            canDrop[dropSwitch] = 0;
            if (RNGDropLoot == 500) // 1 / 1000 chance drop / 0.1% chance
            {
                for (int i = 0; i < itemInstance.Length; ++i)
                {
                    if (canDrop[i] == 0)
                    {
                        itemtype = 1;
                        DropItem(i, enemyN, itemtype);
                        break;
                    }
                }
            }
            else if (RNGDropLoot >= 200 && RNGDropLoot <= 499) // 30% chance drop
            {
                for (int i = 0; i < itemInstance.Length; ++i)
                {
                    if (canDrop[i] == 0)
                    {
                        itemtype = RNGmove.Next(2, 7);
                        DropItem(i, enemyN, itemtype);
                        break;
                    }
                }
            }
            else if (RNGDropLoot >= 501 && RNGDropLoot <= 699) // 20% chance drop
            {
                for (int i = 0; i < itemInstance.Length; ++i)
                {
                    if (canDrop[i] == 8)
                    {
                        itemtype = 1;
                        DropItem(i, enemyN, itemtype);
                        break;
                    }
                }
            }
            else if (RNGDropLoot >= 700 && RNGDropLoot <= 799) // 10% chance drop
            {
                for (int i = 0; i < itemInstance.Length; ++i)
                {
                    if (canDrop[i] == 9)
                    {
                        itemtype = 1;
                        DropItem(i, enemyN, itemtype);
                        break;
                    }
                }
            }
            ++dropSwitch;
            if (dropSwitch >= itemInstance.Length) 
            {
            dropSwitch = 0;
            }
        }
        async void DropItem(int itemN, int enemyN, int itemT)
        {
            itemInstance[itemN].xposition= enemyInstance[enemyN].xposition;
            itemInstance[itemN].yposition =enemyInstance[enemyN].yposition;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = itemT;
            switch (itemT)
            {
                case 2:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon01.png";
                    break;
                case 3:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon02.png";
                    break;
                case 4:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon03.png";
                    break;
                case 5:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon04.png";
                    break;
                case 6:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon05.png";
                    break;
                case 7:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon06.png";
                    break;
                    // non ammo drops
                case 1:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon01.png";
                    break;
                case 8:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon01.png";
                    break;
                case 9:
                    this.Resources["ItemImageR01"] = "ammunitionlooticon01.png";
                    break;
            }
            switch (itemN) 
            {
                case 0:
                    itemim01(itemN);
                    break;
                case 1:
                    itemim02(itemN);
                    break;
                case 2:
                    itemim03(itemN);
                    break;
                case 3:
                    itemim04(itemN);
                    break;
                case 4:
                    itemim05(itemN);
                    break;
                case 5:
                    itemim06(itemN);
                    break;
                case 6:
                    itemim07(itemN);
                    break;
                case 7:
                    itemim08(itemN);
                    break;
            }
        }
        async void itemim01(int itemN)
        {
            await item01.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++) 
            {
                await item01.FadeTo(0.6, 500);
                await item01.FadeTo(1, 500);
            }
            await item01.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item01.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim02(int itemN)
        {
            await item02.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item02.FadeTo(0.6, 500);
                await item02.FadeTo(1, 500);
            }
            await item02.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item02.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim03(int itemN)
        {
            await item03.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item03.FadeTo(0.6, 500);
                await item03.FadeTo(1, 500);
            }
            await item03.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item03.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim04(int itemN)
        {
            await item04.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item04.FadeTo(0.6, 500);
                await item04.FadeTo(1, 500);
            }
            await item04.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item04.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim05(int itemN)
        {
            await item05.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item05.FadeTo(0.6, 500);
                await item05.FadeTo(1, 500);
            }
            await item05.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item05.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim06(int itemN)
        {
            await item06.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item06.FadeTo(0.6, 500);
                await item06.FadeTo(1, 500);
            }
            await item06.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item06.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim07(int itemN)
        {
            await item07.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item07.FadeTo(0.6, 500);
                await item07.FadeTo(1, 500);
            }
            await item07.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item07.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        async void itemim08(int itemN)
        {
            await item08.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await item08.FadeTo(0.6, 500);
                await item08.FadeTo(1, 500);
            }
            await item08.FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await item08.TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }
        // enemy ai
        async void Enemy_AI_01() // basic enemy ai { will move towards player after certain distance }
        {
            while (gamestatus !=0)
            {
                for (int i = 0; i < 50; i++)// move in chunks
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (eCanMove == 1 && enemyInstance[j].xposition > -500 && enemyInstance[j].yposition < 420)
                        {
                            
                            if (enemyInstance[j].yposition >= -410 && enemyInstance[j].xposition > CurrentPlayerPositionX)
                            {
                                enemyInstance[j].xposition = (enemyInstance[j].xposition + RNGmove.Next(-3, 1));
                                enemyInstance[j].yposition = (enemyInstance[j].yposition + RNGmove.Next(-0, 2));
                            } else if (enemyInstance[j].yposition >= -410 && enemyInstance[j].xposition < CurrentPlayerPositionX)
                            {
                                enemyInstance[j].xposition = (enemyInstance[j].xposition + RNGmove.Next(-1, 3));
                                enemyInstance[j].yposition = (enemyInstance[j].yposition + RNGmove.Next(-0, 2));
                            } else if (enemyInstance[j].yposition >= -410 && enemyInstance[j].xposition == CurrentPlayerPositionX)
                            {
                                enemyInstance[j].yposition = (enemyInstance[j].yposition + RNGmove.Next(-0, 2));
                            }// after threshold

                            if (enemyInstance[j].xposition >= 445)
                            {
                                enemyInstance[j].xposition = 444;
                            }
                            if (enemyInstance[j].xposition <= -445)
                            {
                                enemyInstance[j].xposition = -444;
                            }// border check

                            enemyInstance[j].xleftposition = enemyInstance[j].xposition - 25;
                            enemyInstance[j].xrightposition = enemyInstance[j].xposition + 25;
                        }// if
                        
                    }// for in
                    
                    Pushgamei01();
                    await Task.Delay(2);
                }// for EN
                await Task.Delay(1800);
            }
            
        }// ai 1
        async void Enemy_AI_02()
        {
            int j = 8;
            while (gamestatus != 0)// move at constant
            {
                for (j = 8; j < 16; j++)
                {
                    if (eCanMove == 1 && enemyInstance[j].xposition > -500 && enemyInstance[j].yposition < 420)
                    {
                        if (enemyInstance[j].yposition >= -410)
                        {
                            enemyInstance[j].xposition = (enemyInstance[j].xposition + RNGmove.Next(-1, 2));
                            enemyInstance[j].yposition = (enemyInstance[j].yposition + RNGmove.Next(-0, 2));
                        }
                            
                        if (enemyInstance[j].xposition >= 445)
                        {
                            enemyInstance[j].xposition = 444;
                        }
                        if (enemyInstance[j].xposition <= -445)
                        {
                            enemyInstance[j].xposition = -444;
                        }// border check

                        enemyInstance[j].xleftposition = enemyInstance[j].xposition - 25;
                        enemyInstance[j].xrightposition = enemyInstance[j].xposition + 25;
                    }
                }
                Pushgamei02();
                await Task.Delay(2);
            }
        }
        // death animations
        // player
        private void PlayerDeath()
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
        // enemy death ctrl
        private void EnemyDying(int enemyN)
        {
            DropItemRNG(enemyN); // played at every death
            switch (enemyN) // input enemyN decides which enemy dies
            {
                case 0:

                    ei1death();
                    break;
                case 1:

                    ei2death();
                    break;
                case 2:

                    ei3death();
                    break;
                case 3:

                    ei4death();
                    break;
                case 4:

                    ei5death();
                    break;
                case 5:

                    ei6death();
                    break;
                case 6:

                    ei7death();
                    break;
                case 7:

                    ei8death();
                    break;
                case 8:

                    ei9death();
                    break;
                case 9:

                    ei10death();
                    break;
                case 10:

                    ei11death();
                    break;
                case 11:

                    ei12death();
                    break;
                case 12:

                    ei13death();
                    break;
                case 13:

                    ei14death();
                    break;
                case 14:

                    ei15death();
                    break;
                case 15:

                    ei16death();
                    break;
                case 101:

                    bi01death();
                    break;

            }
        }
        private void ei1death()
        {
            e001deathanim01();
            e001deathanim02();
            e001deathanim03();
        }
        async void e001deathanim01()
        {
            await e001.RotateTo(720, 300);
            enemyInstance[0].xposition += -1000;
            enemyInstance[0].xleftposition += -1000;
            enemyInstance[0].xrightposition += -1000;
            if (enemyInstance[0].xposition < 1500)
            {
                killCounter++;
            }
        }
        async void e001deathanim02()
        {
            await e001.FadeTo(0, 300);
        }
        async void e001deathanim03()
        {
            await e001.ScaleTo(0.6, 300);
        }
        private void ei2death()
        {
            e002deathanim01();
            e002deathanim02();
            e002deathanim03();
        }
        async void e002deathanim01()
        {
            await e002.RotateTo(720, 300);
            enemyInstance[1].xposition += -1000;
            enemyInstance[1].xleftposition += -1000;
            enemyInstance[1].xrightposition += -1000;
            if (enemyInstance[1].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e002deathanim02()
        {
            await e002.FadeTo(0, 300);
        }
        async void e002deathanim03()
        {
            await e002.ScaleTo(0.6, 300);
        }
        private void ei3death()
        {
            e003deathanim01();
            e003deathanim02();
            e003deathanim03();
        }
        async void e003deathanim01()
        {
            await e003.RotateTo(720, 300);
            enemyInstance[2].xposition += -1000;
            enemyInstance[2].xleftposition += -1000;
            enemyInstance[2].xrightposition +=  -1000;
            if (enemyInstance[2].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e003deathanim02()
        {
            await e003.FadeTo(0, 300);
        }
        async void e003deathanim03()
        {
            await e003.ScaleTo(0.6, 300);
        }
        private void ei4death()
        {
            e004deathanim01();
            e004deathanim02();
            e004deathanim03();
        }
        async void e004deathanim01()
        {
            await e004.RotateTo(720, 300);
            enemyInstance[3].xposition += -1000;
            enemyInstance[3].xleftposition += -1000;
            enemyInstance[3].xrightposition += -1000;
            if (enemyInstance[3].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e004deathanim02()
        {
            await e004.FadeTo(0, 300);
        }
        async void e004deathanim03()
        {
            await e004.ScaleTo(0.6, 300);
        }
        private void ei5death()
        {
            e005deathanim01();
            e005deathanim02();
            e005deathanim03();
        }
        async void e005deathanim01()
        {
            await e005.RotateTo(720, 300);
            enemyInstance[4].xposition += -1000;
            enemyInstance[4].xleftposition += -1000;
            enemyInstance[4].xrightposition += -1000;
            if (enemyInstance[4].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e005deathanim02()
        {
            await e005.FadeTo(0, 300);
        }
        async void e005deathanim03()
        {
            await e005.ScaleTo(0.6, 300);
        }
        private void ei6death()
        {
            e006deathanim01();
            e006deathanim02();
            e006deathanim03();
        }
        async void e006deathanim01()
        {
            await e006.RotateTo(720, 300);
            enemyInstance[5].xposition += -1000;
            enemyInstance[5].xleftposition += -1000;
            enemyInstance[5].xrightposition += -1000;
            if (enemyInstance[5].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e006deathanim02()
        {
            await e006.FadeTo(0, 300);
        }
        async void e006deathanim03()
        {
            await e006.ScaleTo(0.6, 300);
        }
        private void ei7death()
        {
            e007deathanim01();
            e007deathanim02();
            e007deathanim03();
        }
        async void e007deathanim01()
        {
            await e007.RotateTo(720, 300);
            enemyInstance[6].xposition += -1000;
            enemyInstance[6].xleftposition += -1000;
            enemyInstance[6].xrightposition += -1000;
            if (enemyInstance[6].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e007deathanim02()
        {
            await e007.FadeTo(0, 300);
        }
        async void e007deathanim03()
        {
            await e007.ScaleTo(0.6, 300);
        }
        private void ei8death()
        {
            e008deathanim01();
            e008deathanim02();
            e008deathanim03();
        }
        async void e008deathanim01()
        {
            await e008.RotateTo(720, 300);
            enemyInstance[7].xposition += -1000;
            enemyInstance[7].xleftposition += -1000;
            enemyInstance[7].xrightposition += -1000;
            if (enemyInstance[7].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e008deathanim02()
        {
            await e008.FadeTo(0, 300);
        }
        async void e008deathanim03()
        {
            await e008.ScaleTo(0.6, 300);
        }
        private void ei9death()
        {
            e009deathanim01();
            e009deathanim02();
            e009deathanim03();
        }
        async void e009deathanim01()
        {
            await e009.RotateTo(720, 300);
            enemyInstance[8].xposition += -1000;
            enemyInstance[8].xleftposition += -1000;
            enemyInstance[8].xrightposition += -1000;
            if (enemyInstance[8].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e009deathanim02()
        {
            await e009.FadeTo(0, 300);
        }
        async void e009deathanim03()
        {
            await e009.ScaleTo(0.6, 300);
        }
        private void ei10death()
        {
            e010deathanim01();
            e010deathanim02();
            e010deathanim03();
        }
        async void e010deathanim01()
        {
            await e010.RotateTo(720, 300);
            enemyInstance[9].xposition += -1000;
            enemyInstance[9].xleftposition += -1000;
            enemyInstance[9].xrightposition += -1000;
            if (enemyInstance[9].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e010deathanim02()
        {
            await e010.FadeTo(0, 300);
        }
        async void e010deathanim03()
        {
            await e010.ScaleTo(0.6, 300);
        }
        private void ei11death()
        {
            e011deathanim01();
            e011deathanim02();
            e011deathanim03();
        }
        async void e011deathanim01()
        {
            await e011.RotateTo(720, 300);
            enemyInstance[10].xposition += -1000;
            enemyInstance[10].xleftposition += -1000;
            enemyInstance[10].xrightposition += -1000;
            if (enemyInstance[10].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e011deathanim02()
        {
            await e011.FadeTo(0, 300);
        }
        async void e011deathanim03()
        {
            await e011.ScaleTo(0.6, 300);
        }
        private void ei12death()
        {
            e012deathanim01();
            e012deathanim02();
            e012deathanim03();
        }
        async void e012deathanim01()
        {
            await e012.RotateTo(720, 300);
            enemyInstance[11].xposition += -1000;
            enemyInstance[11].xleftposition += -1000;
            enemyInstance[11].xrightposition += -1000;
            if (enemyInstance[11].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e012deathanim02()
        {
            await e012.FadeTo(0, 300);
        }
        async void e012deathanim03()
        {
            await e012.ScaleTo(0.6, 300);
        }
        private void ei13death()
        {
            e013deathanim01();
            e013deathanim02();
            e013deathanim03();
        }
        async void e013deathanim01()
        {
            await e013.RotateTo(720, 300);
            enemyInstance[12].xposition += -1000;
            enemyInstance[12].xleftposition += -1000;
            enemyInstance[12].xrightposition += -1000;
            if (enemyInstance[12].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e013deathanim02()
        {
            await e013.FadeTo(0, 300);
        }
        async void e013deathanim03()
        {
            await e013.ScaleTo(0.6, 300);
        }
        private void ei14death()
        {
            e014deathanim01();
            e014deathanim02();
            e014deathanim03();
        }
        async void e014deathanim01()
        {
            await e014.RotateTo(720, 300);
            enemyInstance[13].xposition += -1000;
            enemyInstance[13].xleftposition += -1000;
            enemyInstance[13].xrightposition += -1000;
            if (enemyInstance[13].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e014deathanim02()
        {
            await e014.FadeTo(0, 300);
        }
        async void e014deathanim03()
        {
            await e014.ScaleTo(0.6, 300);
        }
        private void ei15death()
        {
            e015deathanim01();
            e015deathanim02();
            e015deathanim03();
        }
        async void e015deathanim01()
        {
            await e015.RotateTo(720, 300);
            enemyInstance[14].xposition += -1000;
            enemyInstance[14].xleftposition += -1000;
            enemyInstance[14].xrightposition += -1000;
            if (enemyInstance[14].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e015deathanim02()
        {
            await e015.FadeTo(0, 300);
        }
        async void e015deathanim03()
        {
            await e015.ScaleTo(0.6, 300);
        }
        private void ei16death()
        {
            e016deathanim01();
            e016deathanim02();
            e016deathanim03();
        }
        async void e016deathanim01()
        {
            await e016.RotateTo(720, 300);
            enemyInstance[15].xposition += -1000;
            enemyInstance[15].xleftposition += -1000;
            enemyInstance[15].xrightposition += -1000;
            if (enemyInstance[15].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void e016deathanim02()
        {
            await e016.FadeTo(0, 300);
        }
        async void e016deathanim03()
        {
            await e016.ScaleTo(0.6, 300);
        }
        private void bi01death()
        {
            e016deathanim01();
            e016deathanim02();
            e016deathanim03();
        }
        async void b01deathanim01()
        {
            await b01.RotateTo(720, 300);
            await b01.RotateTo(-720, 300);
            bossInstance[0].xposition += -1000;
            bossInstance[0].xleftposition += -1000;
            bossInstance[0].xrightposition += -1000;
            if (bossInstance[0].xposition > -1500)
            {
                killCounter++;
            }
        }
        async void b01deathanim02()
        {
            await Task.Delay(300);
            await b01.FadeTo(0, 300);
        }
        async void b01deathanim03()
        {
            await b01.ScaleTo(0.6, 300);
            await b01.ScaleTo(1.6, 300);
        }
        private void EnemyRevive(int enemyN)
        {
            switch (enemyN) // input enemyN decides which enemy ress's
            {
                case 0:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei1Revive(enemyN);
                    }
                    break;
                case 1:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei2Revive(enemyN);
                    }    
                    break;
                case 2:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei3Revive(enemyN);
                    }
                    break;
                case 3:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei4Revive(enemyN);
                    }
                    break;
                case 4:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei5Revive(enemyN);
                    }
                    break;
                case 5:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei6Revive(enemyN);
                    }
                    break;
                case 6:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei7Revive(enemyN);
                    }
                    break;
                case 7:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei8Revive(enemyN);
                    }
                    break;
                case 8:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei9Revive(enemyN);
                    }
                    break;
                case 9:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei10Revive(enemyN);
                    }
                    break;
                case 10:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei11Revive(enemyN);
                    }
                    break;
                case 11:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei12Revive(enemyN);
                    }
                    break;
                case 12:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei13Revive(enemyN);
                    }
                    break;
                case 13:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei14Revive(enemyN);
                    }
                    break;
                case 14:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei15Revive(enemyN);
                    }
                    break;
                case 15:
                    if (BackgroundCurrentPositionY < 1400)
                    {
                        ei16Revive(enemyN);
                    }
                    break;

            }
        }
        async void ei1Revive(int EN)
        {
            await e001.FadeTo(1, 3);
            await e001.RotateTo(0, 3);
            await e001.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei2Revive(int EN)
        {
            await e002.FadeTo(1, 3);
            await e002.RotateTo(0, 3);
            await e002.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei3Revive(int EN)
        {
            await e003.FadeTo(1, 3);
            await e003.RotateTo(0, 3);
            await e003.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei4Revive(int EN)
        {
            await e004.FadeTo(1, 3);
            await e004.RotateTo(0, 3);
            await e004.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei5Revive(int EN)
        {
            await e005.FadeTo(1, 3);
            await e005.RotateTo(0, 3);
            await e005.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei6Revive(int EN)
        {
            await e006.FadeTo(1, 3);
            await e006.RotateTo(0, 3);
            await e006.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei7Revive(int EN)
        {
            await e007.FadeTo(1, 3);
            await e007.RotateTo(0, 3);
            await e007.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei8Revive(int EN)
        {
            await e008.FadeTo(1, 3);
            await e008.RotateTo(0, 3);
            await e008.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei9Revive(int EN)
        {
            await e009.FadeTo(1, 3);
            await e009.RotateTo(0, 3);
            await e009.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei10Revive(int EN)
        {
            await e010.FadeTo(1, 3);
            await e010.RotateTo(0, 3);
            await e010.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei11Revive(int EN)
        {
            await e011.FadeTo(1, 3);
            await e011.RotateTo(0, 3);
            await e011.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei12Revive(int EN)
        {
            await e012.FadeTo(1, 3);
            await e012.RotateTo(0, 3);
            await e012.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei13Revive(int EN)
        {
            await e013.FadeTo(1, 3);
            await e013.RotateTo(0, 3);
            await e013.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei14Revive(int EN)
        {
            await e014.FadeTo(1, 3);
            await e014.RotateTo(0, 3);
            await e014.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei15Revive(int EN)
        {
            await e015.FadeTo(1, 3);
            await e015.RotateTo(0, 3);
            await e015.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        async void ei16Revive(int EN)
        {
            await e016.FadeTo(1, 3);
            await e016.RotateTo(0, 3);
            await e016.ScaleTo(1, 3);
            enemyInstance[EN].xposition = RNGmove.Next(-220, 220);
            enemyInstance[EN].xleftposition = enemyInstance[EN].xposition - 25;
            enemyInstance[EN].xrightposition = enemyInstance[EN].xposition + 25;
            enemyInstance[EN].yposition = RNGmove.Next(-1400, -420);
            ResetEnemyHP(EN, 1);
        }
        private void ResetEnemyHP(int enemyN, int enemyT)// 1 = regular 2= elite
        {
            if (difficultysetting == 1)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp;
                }

            }
            else if (difficultysetting == 2)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp*2;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp + (enemytype2hp / 2);
                }

            }
            else if (difficultysetting == 3)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp*3;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp*2;
                }

            }
            else if (difficultysetting == 4)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp*4;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp*3;
                }

            }
            else if (difficultysetting == 5)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp*5;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp*5;
                }

            }

        }
        // starting positions
        private void enemy_instance_openpos01()
        {

            for (int i = 0; i < enemyInstance.Length; i++) 
            {
              enemyInstance[i].xposition = RNGmove.Next(-220, 220);
              enemyInstance[i].xleftposition = enemyInstance[i].xposition - 25;
              enemyInstance[i].xrightposition = enemyInstance[i].xposition + 25;
              enemyInstance[i].yposition = RNGmove.Next(-1400, -420);
            }
            bossInstance[0].xposition = 0;
            bossInstance[0].yposition = -550;
            bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
            bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
            e001startpos();
        }
        async void e001startpos()// batches of 8
        {
            await e001.TranslateTo(enemyInstance[0].xposition, enemyInstance[0].yposition, 4);
            await e002.TranslateTo(enemyInstance[1].xposition, enemyInstance[1].yposition, 4);
            await e003.TranslateTo(enemyInstance[2].xposition, enemyInstance[2].yposition, 4);
            await e004.TranslateTo(enemyInstance[3].xposition, enemyInstance[3].yposition, 4);
            await e005.TranslateTo(enemyInstance[4].xposition, enemyInstance[4].yposition, 4);
            await e006.TranslateTo(enemyInstance[5].xposition, enemyInstance[5].yposition, 4);
            await e007.TranslateTo(enemyInstance[6].xposition, enemyInstance[6].yposition, 4);
            await e008.TranslateTo(enemyInstance[7].xposition, enemyInstance[7].yposition, 4);

            await e009.TranslateTo(enemyInstance[8].xposition, enemyInstance[8].yposition, 4);
            await e010.TranslateTo(enemyInstance[9].xposition, enemyInstance[9].yposition, 4);
            await e011.TranslateTo(enemyInstance[10].xposition, enemyInstance[10].yposition, 4);
            await e012.TranslateTo(enemyInstance[11].xposition, enemyInstance[11].yposition, 4);
            await e013.TranslateTo(enemyInstance[12].xposition, enemyInstance[12].yposition, 4);
            await e014.TranslateTo(enemyInstance[13].xposition, enemyInstance[13].yposition, 4);
            await e015.TranslateTo(enemyInstance[14].xposition, enemyInstance[14].yposition, 4);
            await e016.TranslateTo(enemyInstance[15].xposition, enemyInstance[15].yposition, 4);

            await b01.TranslateTo(bossInstance[0].xposition, bossInstance[0].yposition, 4);
        }
        async void e002startpos()
        {
            // empty rn
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
            GameUniversalTimer();
            Player_collision_updater();
            Player_collision_object_updater();
            Player_weapon_updater();
            Update_backgrounds();
            PlayerUpdateBars();
            StaminaManage();
            testtext();
            while (gamestatus != 0)
            {
                await Task.Delay(200);
                if (weaponEquipped == 0)
                {
                    ammunitioncurrent = ammunition01;
                }
                else if (weaponEquipped == 1)
                {
                    ammunitioncurrent = ammunition02;
                }
                else if (weaponEquipped == 2)
                {
                    ammunitioncurrent = ammunition03;
                }
                else if (weaponEquipped == 3)
                {
                    ammunitioncurrent = ammunition04;
                }
                else if (weaponEquipped == 4)
                {
                    ammunitioncurrent = ammunition05;
                }
                else if (weaponEquipped == 5)
                {
                    ammunitioncurrent = ammunition06;
                }
         
                ammoqtext.Text = $"Current Ammo: {ammunitioncurrent}   ";
                //testnumber.Text = $"enemy1 x left= {enemyinstance[0].xleftposition}, x right={enemyinstance[0].xrightposition}, y={enemyinstance[0].yposition}";

            }// while 
        }// end of updatermain

        async void PlayerUpdateBars()
        {
            while (gamestatus != 0)
            {
                if (playerMagicPoints > 1)
                {
                    this.Resources["MagicBarValue"] = playerMagicPoints;
                }
                else
                {
                    this.Resources["MagicBarValue"] = 1;
                }
                if (playerHealthPoints > 1)
                {
                    this.Resources["HealthBarValue"] = playerHealthPoints;
                }
                else
                {
                    this.Resources["HealthBarValue"] = 1;
                }
                if (playerStaminaPoints > 1)
                {
                    this.Resources["StaminaBarValue"] = playerStaminaPoints;
                }
                else
                {
                    this.Resources["StaminaBarValue"] = 1;
                    
                }
                await Task.Delay(50);
                if (playerHealthPoints <= 0)// ends the game when conditions are met
                {
                    gamestatus = 0;
                    PlayerDeath();
                }

            }
        }
        async void StaminaManage()// split to not delay bar loop
        {
            while (gamestatus != 0)
            {
                if (playerStaminaPoints >= 0 && playerStaminaPoints <= 140)
                {
                    ++playerStaminaPoints;
                }
                else if (playerStaminaPoints <= 2)
                {
                    if (gamestatus != 0)
                    {
                        //testnumberT++;
                        delay = 1;
                        playerMoveamount = 1;
                        this.Resources["ColourOfSprintBTNClicked"] = Colors.LightBlue;
                        sprintSwitch = 0;
                        await Task.Delay(6500);
                        if (gamestatus != 0)// 3 stage check to make sure it doesn't run when status 0
                        {
                            for (int i = 0; i < 100; i++)
                            {
                                ++playerStaminaPoints;
                                this.Resources["StaminaBarValue"] = playerStaminaPoints;
                                await Task.Delay(5);
                                delay = 1;
                            }
                        }
                    }
                }
                delay = 0;
                await Task.Delay(50);
            }
        }
        async void testtext()// used for testing purposes only
        {
            while (gamestatus != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    testnumber.Text = $"KillCount= {killCounter}\nboss y = {bossInstance[0].yposition}\nboss x = {bossInstance[0].xposition}";
                    await Task.Delay(50);
                }
            }
        }
        async void GameUniversalTimer()
        {
            await Task.Delay(3000);
            while (gamestatus != 0) 
            {
                await Task.Delay(15000);
                if (gamestatus != 0)// extra check so it won't push game after player dies 
                {
                    PushGameObjects();
                    for (int i = 0; i < enemyInstance.Length; i++)
                    {
                        if (enemyInstance[i].yposition > 450)
                        {
                            EnemyRevive(i);
                        }
                    }
                }
                if (BackgroundCurrentPositionY >= 2300 && bossactive == 0)
                {
                    bossactive = 1;
                    await Task.Delay(1500);
                    // enter the boss
                    for (int y = 0; y < 120; y++)
                    {
                        bossInstance[0].yposition += 2;
                        bi01split();
                    }

                }
            }
        }
        async void PushGameObjects()
        {
            for (int k = 0; k < 100; k++)
            {
                eCanMove = 0;
                if (BackgroundCurrentPositionY < 2300)
                {
                    BackgroundCurrentPositionY += 2;
                    for (int i = 0; i < enemyInstance.Length; i++)
                    {
                        enemyInstance[i].yposition += 2;

                    }
                    Pushgamei01();
                    Pushgamei02();
                    Update_backgrounds();
                }
                
                await Task.Delay(25);
            }
            eCanMove = 1;
        }
        async void Pushgamei01()// 1- 8
        {
            ei01split();
            ei02split();
            ei03split();
            ei04split();
            ei05split();
            ei06split();
            ei07split();
            ei08split();
            itemi01split();
            itemi02split();
            itemi03split();
            itemi04split();
            itemi05split();
            itemi06split();
            itemi07split();
            itemi08split();
        }
        async void Pushgamei02()// 1- 16 split for different ai movements
        {
            ei09split();
            ei10split();
            ei11split();
            ei12split();
            ei13split();
            ei14split();
            ei15split();
            ei16split();
        }
        async void ei01split()// split to move in sync
        {
            await e001.TranslateTo(enemyInstance[0].xposition, enemyInstance[0].yposition, 1);
            
        }
        async void ei02split()
        {
            await e002.TranslateTo(enemyInstance[1].xposition, enemyInstance[1].yposition, 1);
            
        }
        async void ei03split()
        {
            await e003.TranslateTo(enemyInstance[2].xposition, enemyInstance[2].yposition, 1);
            
        }
        async void ei04split()
        {
            await e004.TranslateTo(enemyInstance[3].xposition, enemyInstance[3].yposition, 1);
            
        }
        async void ei05split()
        {
            await e005.TranslateTo(enemyInstance[4].xposition, enemyInstance[4].yposition, 1);
            
        }
        async void ei06split()
        {
            await e006.TranslateTo(enemyInstance[5].xposition, enemyInstance[5].yposition, 1);
            
        }
        async void ei07split()
        {
            await e007.TranslateTo(enemyInstance[6].xposition, enemyInstance[6].yposition, 1);
           
        }
        async void ei08split()
        {
            await e008.TranslateTo(enemyInstance[7].xposition, enemyInstance[7].yposition, 1);
        }
        async void ei09split()
        {
            await e009.TranslateTo(enemyInstance[8].xposition, enemyInstance[8].yposition, 1);

        }
        async void ei10split()
        {
            await e010.TranslateTo(enemyInstance[9].xposition, enemyInstance[9].yposition, 1);

        }
        async void ei11split()
        {
            await e011.TranslateTo(enemyInstance[10].xposition, enemyInstance[10].yposition, 1);

        }
        async void ei12split()
        {
            await e012.TranslateTo(enemyInstance[11].xposition, enemyInstance[11].yposition, 1);

        }
        async void ei13split()
        {
            await e013.TranslateTo(enemyInstance[12].xposition, enemyInstance[12].yposition, 1);

        }
        async void ei14split()
        {
            await e014.TranslateTo(enemyInstance[13].xposition, enemyInstance[13].yposition, 1);

        }
        async void ei15split()
        {
            await e015.TranslateTo(enemyInstance[14].xposition, enemyInstance[14].yposition, 1);

        }
        async void ei16split()
        {
            await e016.TranslateTo(enemyInstance[15].xposition, enemyInstance[15].yposition, 1);
        }
        async void bi01split()
        {
            await b01.TranslateTo(bossInstance[0].xposition, bossInstance[0].yposition, 1);
        }
        // item push split
        async void itemi01split()
        {
            await item01.TranslateTo(itemInstance[0].xposition, itemInstance[0].yposition, 5);
        }
        async void itemi02split()
        {
            await item02.TranslateTo(itemInstance[1].xposition, itemInstance[1].yposition, 5);
        }
        async void itemi03split()
        {
            await item03.TranslateTo(itemInstance[2].xposition, itemInstance[2].yposition, 5);
        }
        async void itemi04split()
        {
            await item04.TranslateTo(itemInstance[3].xposition, itemInstance[3].yposition, 5);
        }
        async void itemi05split()
        {
            await item05.TranslateTo(itemInstance[4].xposition, itemInstance[4].yposition, 5);
        }
        async void itemi06split()
        {
            await item06.TranslateTo(itemInstance[5].xposition, itemInstance[5].yposition, 5);
        }
        async void itemi07split()
        {
            await item07.TranslateTo(itemInstance[6].xposition, itemInstance[6].yposition, 5);
        }
        async void itemi08split()
        {
            await item08.TranslateTo(itemInstance[7].xposition, itemInstance[7].yposition, 5);
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
                await PlayerHitBoxtopleft.TranslateTo(playercollisiontopleftX, CurrentPlayerPositionY - 35, 1);
                await PlayerHitBoxtopright.TranslateTo(playercollisiontoprightX, CurrentPlayerPositionY - 35, 1);
                await PlayerHitBoxbotleft.TranslateTo(playercollisionbotleftX, CurrentPlayerPositionY + 35, 1);
                await PlayerHitBoxbotright.TranslateTo(playercollisionbotrightX, CurrentPlayerPositionY+35, 1);
            }
        }
        async void Player_weapon_updater()
        {
            while (gamestatus != 0) // split the update loop to stop crashing
            {
                await Task.Delay(200);
                if (weaponEquipped != 0)
                {
                    this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponEquipped == 0)
                {
                    this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponEquipped != 1)
                {
                    this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponEquipped == 1)
                {
                    this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponEquipped != 2)
                {
                    this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponEquipped == 2)
                {
                    this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponEquipped != 3)
                {
                    this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponEquipped == 3)
                {
                    this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponEquipped != 4)
                {
                    this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponEquipped == 4)
                {
                    this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateBlue;
                }
                if (weaponEquipped != 5)
                {
                    this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateGrey;
                }
                else if (weaponEquipped == 5)
                {
                    this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateBlue;
                }
            }
        }
        async void Update_backgrounds()
        {
            if (gamelevelflag == 1)
            {
                await BackgroundLevel01.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 2)
            {
                await BackgroundLevel02.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 3)
            {
                await BackgroundLevel03.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 4)
            {
                await BackgroundLevel04.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 5)
            {
                await BackgroundLevel05.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 6)
            {
                await BackgroundLevel06.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 7)
            {
                await BackgroundLevel07.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }
            else if (gamelevelflag == 8)
            {
                await BackgroundLevel08.TranslateTo(BackgroundCurrentPositionX, BackgroundCurrentPositionY, 1);
            }

        }


    }// end of all
}// end of all
