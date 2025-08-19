using Microsoft.Maui.Storage;
using Microsoft.Maui.Devices;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Maui.Audio;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Xml.Linq;
using Microsoft.Maui.Controls.PlatformConfiguration;
//using Windows.Media.Playback;


namespace BattleForAzuraTLOV
{


    public partial class MainPage : ContentPage
    {

        int CurrentPlayerPositionX = 0, CurrentPlayerPositionY = 0;
        int BackgroundCurrentPositionX = 0, BackgroundCurrentPositionY = 0, playermovelock = 0;
        int RandomPositionX = 0, RandomPositionY = 0, rtime, weaponEquipped = 0;
        int PlayerDamagePerma = 5, PermaShopToken = 0, PermaPlayerSpeed = 1, PermaDiscount=0, PlayerATKSpeedPerma=1500, PermaAtKUpg=0, bossoverstate = 0;
        int PermaATKSUpg = 0, PermaSPDUpg=0, PermaGun1Upg=0, PermaGun2Upg=0, PermaGun3Upg=0, PermaGun4Upg=0, PermaGun5Upg=0, PermaGun6Upg=0, PermaAmmoUpg=0;
        int playercollisiontopleftX, playercollisiontoprightX, playercollisionbotleftX, playercollisionbotrightX, tutorialactivated = 0;
        int playercollisiontopY;

        int[] canHit = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] activeprojectileposition01x = { 0, 0, 0, 0, 5, 0, 0, 0, 0, 10, 0, 0, 0, 0, 15, 0, 0, 0, 0, 20, 0, 0 };
        int[] activeprojectileposition01y = { 0, 0, 0, 0, 5, 0, 0, 0, 0, 10, 0, 0, 0, 0, 15, 0, 0, 0, 0, 20, 0, 0 };
        int[] isMoving = { 0, 0, 0, 0 };
        int[] canDrop = { 0, 0, 0, 0, 0, 0, 0, 0 };

        int[] levelCompleted = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] levelStatistics01 = { 63, 3, 1, 5, 3, 0, 0, 5, 0, 1, 2, 7 };
        int[] levelStatistics02 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] levelStatistics03 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] levelStatistics04 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] purchaseAmount = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] purchaseAmount2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] purchaseConfirmed = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics05 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics06 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics07 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics08 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics09 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics10 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics11 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] levelStatistics12 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        int levelsCompleted = 0;
        int ammunition01 = 0, ammunition02 = 0, ammunition03 = 0, ammunition04 = 0, ammunition05 = 0, ammunition06 = 0, ammunitioncurrent = 0;
        int projectilecycle01 = 0, projectilecycle02 = 0, projectilecycle03 = 0, projectilecycle04 = 0, projectilecycle05 = 0, projectilecycle06 = 0;
        int gamelevelflag = 0, gamestatus = 0, areascreenlock = 0, tutorialbclicked = 0, SFXVolumeN = 1, cartTotal;
        int newgamedifficulty = 0, difficultysetting = 0, saveselected = 0, save01exist = 0, save02exist = 0, save03exist = 0, missionselected = 1;
        int playerMoveamount = 0, playerDamageValue = 5, killCounter = 0, eCanMove = 0, killcounter2 =0, killcounter3 = 0, musicLockKL = 0;
        int weaponMenuedSwitch = 0, weaponowned01 = 1, weaponowned02 = 0, weaponowned03 = 0, weaponowned04 = 0, weaponowned05 = 0, weaponowned06 = 0;
        int enemytype1hp = 8, enemytype2hp = 12, enemytype1dmg = 3, enemytype2dmg = 5, boss1hp = 100, boss1dmg = 10, testnumberT = 0, dropSwitch = 0, startSwitch = 0, gameMenuSwitch = 0, currentMTrack = 0, tracklock =0;
        int settingsVolume = 0, settingsVolume2 = 0, settingsEnhancedGamePlay = 1, settingsItalienVoiceActing = 1, settingsQuitgame = 1, settingsGameOs = 1, settingsEnhancedAI = 1, settingsGraphics = 0, settingsSpiderMode = 1;
        int item1cost = 1, item2cost = 1, item3cost = 1, item4cost = 1, item5cost = 1, item6cost = 1, item7cost = 1, item8cost = 1, item9cost = 1;
        float playerHealthPoints = 140, playerStaminaPoints = 140, playerMagicPoints = 140, sprintSwitch = 0, delay = 0, bossactive = 0;
        double musicVolume = 0.50;

        Random RNGmove = new Random();

        private IAudioManager audioManager;
        private readonly Dictionary<int, IAudioPlayer> _players = new();
        private IAudioPlayer _currentPlayer;
        private IAudioPlayer _currentSFX;
        private readonly SemaphoreSlim _gate = new(1, 1);
        // creating arrays of game objects
        EnemyObject[] enemyInstance = new EnemyObject[16];
        EnemyObject[] eliteEnemyInstance = new EnemyObject[4];
        EnemyObject[] bossInstance = new EnemyObject[1];
        ItemObject[] itemInstance = new ItemObject[8];
        List<Image> imageCollection = new List<Image>();
        List<Image> enemyCollection = new List<Image>();

        public MainPage(IAudioManager audioManager)
        {
            //new KeyboardAccelerator { Key = "X" };
            InitializeComponent();
            this.audioManager = audioManager;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            setuptitlescreen();
            GameAppear();
            items();
            enemies();
        }
        private void GameAppear()
        {
            // re-used for starting / restarting the game back at the main menu screen ( not title)
            // has a split between platforms for platform specific needs
 
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                //MainActivity.Instance.RequestedOrientation = ScreenOrientation.Landscape;
                setupallgamemenuANDR();
                setupallgameui();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {

                setupallgamemenu();
                setupallgameui();
            }

            setupallgameobjects();
            setupallgameprojectiles();
            hideallgamecontent();
            Setting_updater();
            tutorialsetup();
            // set up perma
            playerDamageValue = PlayerDamagePerma;
            playerDamageValue += PermaAtKUpg;
            // setup objects
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
            playerMoveamount = PermaPlayerSpeed;
            ItemOffSet();
        }
        async void setupallgameobjects()
        {
            // backgrounds, items
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
            // setup positions of all player game ui
            await PlayerHPbar.TranslateTo(-98, 0, 4);
            await PlayerStaminabar.TranslateTo(-93, 0, 4);
            await PlayerMagicbar.TranslateTo(-102, 0, 4);
            await backmovebutton.TranslateTo(30, 125, 4);
            await leftmovebutton.TranslateTo(-20, 30, 4);
            await rightmovebutton.TranslateTo(85, -20, 4);
            await forwardmovebutton.TranslateTo(30, -115, 4);
            await sprintbutton.TranslateTo(-10, 30, 4);
            await attackbutton.TranslateTo(60, -10, 4);
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
            await backgroundmenumenu01.TranslateTo(280, 0, 4);
            await menumenubutton01.TranslateTo(280, -200, 4);
            await menumenubutton02.TranslateTo(280, -100, 4);
            await menumenubutton03.TranslateTo(280, 0, 4);
            await menumenubutton04.TranslateTo(280, 100, 4);
            await menumenubutton05.TranslateTo(280, 200, 4);
            await deathscreenbutton.TranslateTo(0, 1140, 4);
            await gamemenubutton.TranslateTo(0, 0, 4);
            await weaponswitchbutton.TranslateTo(0, 70, 4);
            await backgroundammotext.TranslateTo(-20, -35, 4);
            await ammoqtext.TranslateTo(-23, -75, 4);
            await attackbutton.ScaleTo(1.7, 4);
            await sprintbutton.ScaleTo(0.9, 4);
            await weaponswitchbutton.ScaleTo(0.8, 4);
            await backgroundammotext.ScaleTo(1.3, 4);
            await backgroundweaponmenu01.ScaleTo(4.5, 4);
            await deathscreenbutton.FadeTo(0, 4);
            await backgroundmenumenu01.FadeTo(0, 4);
            await menumenubutton01.FadeTo(0, 4);
            await menumenubutton02.FadeTo(0, 4);
            await menumenubutton03.FadeTo(0, 4);
            await menumenubutton04.FadeTo(0, 4);
            await menumenubutton05.FadeTo(0, 4);


            this.Resources["Settings1BTNText"] = " -> Music Volume (On) <- ";
            this.Resources["Settings2BTNText"] = " -> SFX Volume (On) <- ";
            this.Resources["Settings3BTNText"] = " -> Enhanced Gameplay (Off) <- ";
            this.Resources["Settings4BTNText"] = " -> Italian Voice Acting (Off) <- ";
            this.Resources["Settings5BTNText"] = " -> Quit Game <- ";
            this.Resources["Settings6BTNText"] = " -> Game Osmosis (Off) <- ";
            this.Resources["Settings7BTNText"] = " -> Enhanced AI Features (Off) <- ";
            this.Resources["Settings8BTNText"] = " -> Graphics (On) <- ";
            this.Resources["Settings9BTNText"] = " -> Spider-phobia Mode (Off) <- ";
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
            this.Resources["ColourOfGameMenuBTNClicked"] = Colors.DarkViolet;
            this.Resources["ColourOfWeaponSwitchBTNClicked"] = Colors.Yellow;
            this.Resources["ColourOfWeapon1BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon2BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon3BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon4BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon5BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfWeapon6BTNClicked"] = Colors.DarkSlateGray;
            this.Resources["ColourOfSetting1BTNClicked"] = Colors.Ivory;
            this.Resources["ColourOfSetting2BTNClicked"] = Colors.Ivory;
            this.Resources["ColourOfSetting3BTNClicked"] = Colors.Ivory;
            this.Resources["ColourOfSetting4BTNClicked"] = Colors.Ivory;
            this.Resources["ColourOfSetting5BTNClicked"] = Colors.Ivory;
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
            await Pdeathscreen01.FadeTo(0, 5);
            await Pdeathscreen02.FadeTo(0, 5);
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
            await LevelStatsScreen01.FadeTo(0, 4);
        }
        private void hideplayerui02()
        {
            hideplayerui_winscre_01();
            hideplayerui_winscre_02();
            hideplayerui_winscre_03();
            hideplayerui_winscre_04();
            hideplayerui_winscre_05();
            hideplayerui_winscre_06();
            hideplayerui_winscre_07();
            hideplayerui_winscre_08();
            hideplayerui_winscre_09();
            hideplayerui_winscre_10();
            hideplayerui_winscre_11();
            hideplayerui_winscre_12();
            hideplayerui_winscre_13();
            hideplayerui_winscre_14();
            hideplayerui_winscre_15();
            hideplayerui_winscre_16();
        }
        async void hideplayerui_winscre_01()
        {
            await PlayerMagicbase.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_02()
        {
            await PlayerMagicbar.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_03()
        {
            await PlayerHPbase.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_04()
        {
            await PlayerHPbar.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_05()
        {
            await PlayerStaminabase.FadeTo(0, 500);
            

        }
        async void hideplayerui_winscre_06()
        {
            await PlayerStaminabar.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_07()
        {
            await leftmovebutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_08()
        {
            await forwardmovebutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_09()
        {
            await rightmovebutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_10()
        {
            await backmovebutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_11()
        {
            await sprintbutton.FadeTo(0, 500);

        }
        async void hideplayerui_winscre_12()
        {
            await attackbutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_13()
        {
            await gamemenubutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_14()
        {
            await weaponswitchbutton.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_15()
        {
            await backgroundammotext.FadeTo(0, 500);
            
        }
        async void hideplayerui_winscre_16()
        {
            await ammoqtext.FadeTo(0, 500);
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
            playerHealthPoints = 140;
            playerStaminaPoints = 140;
            await PlayerIMG.FadeTo(1, 5);
            await PlayerIMG.ScaleTo(1, 5);
            await PlayerIMG.RotateTo(0, 5);
            CurrentPlayerPositionX = 0;
            CurrentPlayerPositionY = 0;
            bossoverstate = 0;
            await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 5);
        }
        async void showbackground()
        {
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
            setupmainmenu();
            setupnewgamemenu();
            setupcontinuemenu();
            setupTestAcceptmenu();
            setupMissionsmenu();
            setupsupershopmenu();
            setupmusicmenu();
            setupsettingsmenu();
            setuplevelstatsmenu01();
        }
        async void setuptitlescreen()
        {
            await EnterGamebutton.FadeTo(1, 5);
            await EnterGamebutton.TranslateTo(0, 100, 5);
            await TitleScreen02.TranslateTo(0, -50, 5);
        }
        async void setupmainmenu()
        {
            await TitleScreen01.TranslateTo(0, 0, 5);
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
            await deletesavebutton.TranslateTo(0, (195 - 1000), 5);
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
            await Permasupershopscreen.TranslateTo(0, -1000, 5);
            await Attackupimage.TranslateTo(-260, (-150 - 1000), 5);
            await Speedupimage.TranslateTo(-140, (-150 - 1000), 5);
            await gun1image.TranslateTo(70, (-175 - 1000), 5);
            await gun2image.TranslateTo(70, (-110 - 1000), 5);
            await gun3image.TranslateTo(70, (-45 - 1000), 5);
            await gun4image.TranslateTo(70, (20 - 1000), 5);
            await gun5image.TranslateTo(70, (85 - 1000), 5);
            await gun6image.TranslateTo(70, (150 - 1000), 5);
            await Ammoupitemimage.TranslateTo(-380, (-150 - 1000), 5);
            await permastoretextbutton.TranslateTo(-400, (-210 - 1000), 5);
            await buyitem01button.TranslateTo(-380, (-60 - 1000), 5);
            await buyitem02button.TranslateTo(-260, (-60 - 1000), 5);
            await buyitem03button.TranslateTo(-140, (-60 - 1000), 5);
            await buyitem04button.TranslateTo(270, (-175 - 1000), 5);
            await buyitem05button.TranslateTo(270, (-110 - 1000), 5);
            await buyitem06button.TranslateTo(270, (-45 - 1000), 5);
            await buyitem07button.TranslateTo(270, (20 - 1000), 5);
            await buyitem08button.TranslateTo(270, (85 - 1000), 5);
            await buyitem09button.TranslateTo(270, (150 - 1000), 5);
            await buyconfirmbutton.TranslateTo(390, (-200 - 1000), 5);
            await leave05button.TranslateTo(390, (-200 - 1000), 5);


            this.Resources["itembuy1BTNText"] = "Upgrade";
            this.Resources["itembuy2BTNText"] = "Upgrade";
            this.Resources["itembuy3BTNText"] = "Upgrade";
            this.Resources["itembuy4BTNText"] = "Buy Gun 1";
            this.Resources["itembuy5BTNText"] = "Buy Gun 2";
            this.Resources["itembuy6BTNText"] = "Buy Gun 3";
            this.Resources["itembuy7BTNText"] = "Buy Gun 4";
            this.Resources["itembuy8BTNText"] = "Buy Gun 5";
            this.Resources["itembuy9BTNText"] = "Buy Gun 6";
            this.Resources["permatokensBTNText"] = "Orbs: 0";
            this.Resources["ColourOfItemBuy01BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy02BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy03BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy04BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy05BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy06BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy07BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy08BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy09BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfBuyConfirmBTNClicked"] = Colors.White;

        }
        async void setupmusicmenu()
        {
            await Settingmusicscreen.TranslateTo(0, -1000, 5);
            await musicbutton01.TranslateTo(-250, (-150 - 1000), 5);
            await musicbutton02.TranslateTo(-250, (-50 - 1000), 5);
            await musicbutton03.TranslateTo(-250, (50 - 1000), 5);
            await musicbutton04.TranslateTo(-250, (150 - 1000), 5);
            await musicbutton05.TranslateTo(250, (-150 - 1000), 5);
            await musicbutton06.TranslateTo(250, (-50 - 1000), 5);
            await musicbutton07.TranslateTo(250, (50 - 1000), 5);
            await musictextbutton.TranslateTo(0, (-220 - 1000), 5);
            await leave07button.TranslateTo(350, (220 - 1000), 5);

            this.Resources["Music1BTNText"] = "Track 1";
            this.Resources["Music2BTNText"] = "Track 2";
            this.Resources["Music3BTNText"] = "Track 3";
            this.Resources["Music4BTNText"] = "Track 4";
            this.Resources["Music5BTNText"] = "Track 5";
            this.Resources["Music6BTNText"] = "Track 6";
            this.Resources["Music7BTNText"] = "Track 7";
            this.Resources["ColourOfMusic1BTNClicked"] = Colors.LightGray;
            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
        }
        async void setupsettingsmenu()
        {
            //await Settingmusicscreen.TranslateTo(0, -1000, 5);
            await mainmenubutton01.TranslateTo(-260, (-150 - 1000), 5);
            await mainmenubutton02.TranslateTo(-260, (-50 - 1000), 5);
            await mainmenubutton03.TranslateTo(-260, (50 - 1000), 5);
            await mainmenubutton04.TranslateTo(-260, (150 - 1000), 5);
            await mainmenubutton05.TranslateTo(0, (220 - 1000), 5);
            await mainmenubutton06.TranslateTo(260, (-150 - 1000), 5);
            await mainmenubutton07.TranslateTo(260, (-50 - 1000), 5);
            await mainmenubutton08.TranslateTo(260, (50 - 1000), 5);
            await mainmenubutton09.TranslateTo(260, (150 - 1000), 5);
            await mainmenutextbutton.TranslateTo(0, (-220 - 1000), 5);
            await leave06button.TranslateTo(350, (220 - 1000), 5);

        }
        async void setuplevelstatsmenu01()
        {
            await LevelStatsScreen01.TranslateTo(0, -1000, 5);
        }

        private void setupallgamemenuANDR()
        {
            setupmainmenuANDR();
            setupnewgamemenuANDR();
            setupcontinuemenuANDR();
            setupTestAcceptmenuANDR();
            setupMissionsmenuANDR();
            setupsupershopmenuANDR();
            setupmusicmenuANDR();
            setupsettingsmenuANDR();
        }
        async void setuptitlescreenANDR()
        {
            await EnterGamebutton.FadeTo(1, 5);
            await EnterGamebutton.TranslateTo(0, 50, 5);
            await TitleScreen02.TranslateTo(0, -50, 5);
        }
        async void setupmainmenuANDR()
        {
            await TitleScreen01.TranslateTo(0, 0, 5);
            await NewGamebutton.TranslateTo(-150, 120, 5);
            await Continuebutton.TranslateTo(-0, 122, 5);
            await Trainingbutton.TranslateTo(-125, 1189, 5);
            await Missionbutton.TranslateTo(0, 1191, 5);
            await SuperShopbutton.TranslateTo(125, 1193, 5);
            await Brutalbutton.TranslateTo(250, 1195, 5);
            await Challengebutton.TranslateTo(375, 1197, 5);
            await Musicbutton.TranslateTo(400, -1185, 5);
            await Settingsbutton.TranslateTo(320, -124, 5);
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
        async void setupnewgamemenuANDR()
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
        async void setupcontinuemenuANDR()
        {
            await saveslot1button.TranslateTo(-325, -1050, 5);
            await saveslot2button.TranslateTo(-325, -1000, 5);
            await saveslot3button.TranslateTo(-325, -950, 5);
            await deletesavebutton.TranslateTo(0, (195 - 1000), 5);
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
        // there to keep off screen 
        async void setupTestAcceptmenuANDR()
        {

            await accept03button.TranslateTo(-75, (40 - 1000), 5);
            await leave03button.TranslateTo(75, (40 - 1000), 5);
            await GrayFilterScreen01.TranslateTo(0, 0, 5);
            await GrayFilterScreen01.FadeTo(0, 5);
        }
        async void setupMissionsmenuANDR()
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
           
        }
        async void setupsupershopmenuANDR()
        {
            await Permasupershopscreen.TranslateTo(0, -1000, 5);
            await Attackupimage.TranslateTo(-260, (-150 - 1000), 5);
            await Speedupimage.TranslateTo(-140, (-150 - 1000), 5);
            await gun1image.TranslateTo(70, (-175 - 1000), 5);
            await gun2image.TranslateTo(70, (-110 - 1000), 5);
            await gun3image.TranslateTo(70, (-45 - 1000), 5);
            await gun4image.TranslateTo(70, (20 - 1000), 5);
            await gun5image.TranslateTo(70, (85 - 1000), 5);
            await gun6image.TranslateTo(70, (150 - 1000), 5);
            await Ammoupitemimage.TranslateTo(-380, (-150 - 1000), 5);
            await permastoretextbutton.TranslateTo(-400, (-210 - 1000), 5);
            await buyitem01button.TranslateTo(-380, (-60 - 1000), 5);
            await buyitem02button.TranslateTo(-260, (-60 - 1000), 5);
            await buyitem03button.TranslateTo(-140, (-60 - 1000), 5);
            await buyitem04button.TranslateTo(270, (-175 - 1000), 5);
            await buyitem05button.TranslateTo(270, (-110 - 1000), 5);
            await buyitem06button.TranslateTo(270, (-45 - 1000), 5);
            await buyitem07button.TranslateTo(270, (20 - 1000), 5);
            await buyitem08button.TranslateTo(270, (85 - 1000), 5);
            await buyitem09button.TranslateTo(270, (150 - 1000), 5);
            await buyconfirmbutton.TranslateTo(390, (-200 - 1000), 5);
            await leave05button.TranslateTo(390, (-200 - 1000), 5);

        }
        async void setupmusicmenuANDR()
        {
            await Settingmusicscreen.TranslateTo(0, -1000, 5);
            await musicbutton01.TranslateTo(-250, (-150 - 1000), 5);
            await musicbutton02.TranslateTo(-250, (-50 - 1000), 5);
            await musicbutton03.TranslateTo(-250, (50 - 1000), 5);
            await musicbutton04.TranslateTo(-250, (150 - 1000), 5);
            await musicbutton05.TranslateTo(250, (-150 - 1000), 5);
            await musicbutton06.TranslateTo(250, (-50 - 1000), 5);
            await musicbutton07.TranslateTo(250, (50 - 1000), 5);
            await musictextbutton.TranslateTo(0, (-220 - 1000), 5);
            await leave07button.TranslateTo(350, (220 - 1000), 5);
        }
        async void setupsettingsmenuANDR()
        {
            await mainmenubutton01.TranslateTo(-260, (-150 - 1000), 5);
            await mainmenubutton02.TranslateTo(-260, (-50 - 1000), 5);
            await mainmenubutton03.TranslateTo(-260, (50 - 1000), 5);
            await mainmenubutton04.TranslateTo(-260, (150 - 1000), 5);
            await mainmenubutton05.TranslateTo(0, (220 - 1000), 5);
            await mainmenubutton06.TranslateTo(260, (-150 - 1000), 5);
            await mainmenubutton07.TranslateTo(260, (-50 - 1000), 5);
            await mainmenubutton08.TranslateTo(260, (50 - 1000), 5);
            await mainmenubutton09.TranslateTo(260, (150 - 1000), 5);
            await mainmenutextbutton.TranslateTo(0, (-220 - 1000), 5);
            await leave06button.TranslateTo(350, (220 - 1000), 5);
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

        // buttons
        private void SprintBTN_Clicked(object sender, EventArgs e) // while pressed
        {
            if (gamestatus != 0)
            {
                if (sprintSwitch == 0 && playerStaminaPoints >= 1 && delay == 0)
                {
                    this.Resources["ColourOfSprintBTNClicked"] = Colors.Navy;
                    playerMoveamount += 2;
                    sprintSwitch = 1;

                }
                else if (sprintSwitch == 1)
                {
                    this.Resources["ColourOfSprintBTNClicked"] = Colors.LightBlue;
                    playerMoveamount = PermaPlayerSpeed;
                    sprintSwitch = 0;
                }
            }
        }
        private void MoveBTN_Clicked(object sender, EventArgs e) // while pressed
        {
            isMoving[0] = 1;
            if (gamestatus != 0 && gamestatus != 0)
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
            while (isMoving[0] == 1 && gamestatus != 0)
            {
                CurrentPlayerPositionY = CurrentPlayerPositionY - playerMoveamount;

                if (CurrentPlayerPositionY <= -220)
                {
                    CurrentPlayerPositionY = -219;

                }
                if (sprintSwitch == 1 && playerStaminaPoints >= 1)
                {
                    playerStaminaPoints = (playerStaminaPoints - 2);
                }
                await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
            this.Resources["ColourOfForwardMoveBTNClicked"] = Colors.LightBlue;
        }
        async void Move_playerLeft()
        {
            while (isMoving[1] == 1 && gamestatus != 0)
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
            while (isMoving[2] == 1 && gamestatus != 0)
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
            while (isMoving[3] == 1 && gamestatus != 0)
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
            while (isMoving[0] == 1 || isMoving[1] == 1 || isMoving[2] == 1 || isMoving[3] == 1 && gamestatus != 0)
            {
                await PlayerHitBox.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
            }
        }
        async void Move_player_Camera_Box()
        {
            while (isMoving[0] == 1 || isMoving[1] == 1 || isMoving[2] == 1 || isMoving[3] == 1 && gamestatus != 0)
            {
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
            SoundBoard(3);
            if (gamestatus != 0 && bossoverstate == 0)
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

        // player attacking (when attack button is clicked)
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
        // animations for each gun, cycling the bullet instances,
        // positions for each projectile, activating collissions,
        // and reseting after use.
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
                        for (int j = 0; j < enemyInstance.Length; j++)// hit tracking
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[0] >= -390)// movement tracking
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[1] >= -390)
                        {
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

                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[2] >= -390)
                        {
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

                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[3] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[4] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[5] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[6] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[7] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[8] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }

                        if (activeprojectileposition01y[9] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[10] >= -390)
                        {
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

                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[11] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[12] >= -390)
                        {
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

                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[13] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[14] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[15] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[16] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[17] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[18] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[19] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[20] >= -390)
                        {
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
                                bi01death();
                                break;
                            }
                            break;
                        }
                        if (activeprojectileposition01y[21] >= -390)
                        {
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
                SoundBoard(8);
                if (gameMenuSwitch == 0)
                {
                    GameMenuAct();
                    gameMenuSwitch = 1;
                    this.Resources["ColourOfGameMenuBTNClicked"] = Colors.Purple;
                }
                else if (gameMenuSwitch == 1)
                {
                    GameMenuBck();
                    gameMenuSwitch = 0;
                    this.Resources["ColourOfGameMenuBTNClicked"] = Colors.DarkViolet;
                }
            }
        }
        async void GameMenuAct()
        {
            GameMenuA01();
            GameMenuA02();
            GameMenuA03();
            GameMenuA04();
            GameMenuA05();
            GameMenuA06();
        }
        async void GameMenuA01()
        {
            await backgroundmenumenu01.FadeTo(1, 200);
        }
        async void GameMenuA02()
        {
            await menumenubutton01.FadeTo(1, 200);
        }
        async void GameMenuA03()
        {
            await menumenubutton02.FadeTo(1, 200);
        }
        async void GameMenuA04()
        {
            await menumenubutton03.FadeTo(1, 200);  
        }
        async void GameMenuA05()
        {
            await menumenubutton04.FadeTo(1, 200);   
        }
        async void GameMenuA06()
        {
            await menumenubutton05.FadeTo(1, 200);
        }
        async void GameMenuBck()
        {
            GameMenuB01();
            GameMenuB02();
            GameMenuB03();
            GameMenuB04();
            GameMenuB05();
            GameMenuB06();  
        }
        async void GameMenuB01()
        {
            await backgroundmenumenu01.FadeTo(0, 200);
        }
        async void GameMenuB02()
        {
            await menumenubutton01.FadeTo(0, 200);
        }
        async void GameMenuB03()
        {
            await menumenubutton02.FadeTo(0, 200);
        }
        async void GameMenuB04()
        {
            await menumenubutton03.FadeTo(0, 200);
        }
        async void GameMenuB05()
        {
            await menumenubutton04.FadeTo(0, 200);
        }
        async void GameMenuB06()
        {
            await menumenubutton05.FadeTo(0, 200);
        }
        private void Setting1BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsVolume == 0)
            {
                MusicPlayer01(0, 0, 0);
                settingsVolume = 1;
                musicVolume = 0;              
            }
            else if (settingsVolume == 1)
            {
                MusicPlayer01(0, 0, musicVolume);
                settingsVolume = 0;
                musicVolume = 0.50;
            }
            SoundBoard(8);
        }
        private void Setting2BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsVolume2 == 0)
            {
                SFXVolumeN = 0;
                settingsVolume2 = 1;
            }
            else if (settingsVolume2 == 1)
            {
                SFXVolumeN = 1;
                settingsVolume2 = 0;
            }
            SoundBoard(8);
        }
        private void Setting3BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsEnhancedGamePlay == 0)
            {
                settingsEnhancedGamePlay = 1;
            }
            else if (settingsEnhancedGamePlay == 1)
            {
                settingsEnhancedGamePlay = 0;
            }
            SoundBoard(8);
        }
        private void Setting4BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsItalienVoiceActing == 0)
            {
                settingsItalienVoiceActing = 1;
            }
            else if (settingsItalienVoiceActing == 1)
            {
                settingsItalienVoiceActing = 0;
            }
            SoundBoard(8);
        }
        private void Setting5BTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            if (settingsQuitgame == 0)
            {
                settingsQuitgame = 1;
                SaveGame();
                Application.Current.Quit();
            }
            else if (settingsQuitgame == 1)
            {
                settingsQuitgame = 0;
                SaveGame();
                Application.Current.Quit();
            }
        }
        private void Setting6BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsGameOs == 0)
            {
                settingsGameOs = 1;
            }
            else if (settingsGameOs == 1)
            {
                settingsGameOs = 0;
            }
            SoundBoard(8);
        }
        private void Setting7BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsEnhancedAI == 0)
            {
                settingsEnhancedAI = 1;
            }
            else if (settingsEnhancedAI == 1)
            {
                settingsEnhancedAI = 0;
            }
            SoundBoard(8);
        }
        private void Setting8BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsGraphics == 0)
            {
                settingsGraphics = 1;
            }
            else if (settingsGraphics == 1)
            {
                settingsGraphics = 0;
            }
            SoundBoard(8);
        }
        private void Setting9BTN_Clicked(object sender, EventArgs e)
        {
            if (settingsSpiderMode == 0)
            {
                settingsSpiderMode = 1;
            }
            else if (settingsSpiderMode == 1)
            {
                settingsSpiderMode = 0;
            }
            SoundBoard(8);       
        }
        private void WeaponBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
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
            SoundBoard(8);
            if (weaponEquipped != 0)
            {
                if (weaponowned01 == 1)
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
            SoundBoard(8);
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
            SoundBoard(8);
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
            SoundBoard(8);
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
            SoundBoard(8);
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
            SoundBoard(8);
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
            playercollisiontopleftX = (CurrentPlayerPositionX - 35);
            playercollisiontoprightX = (CurrentPlayerPositionX + 35);
            playercollisionbotleftX = (CurrentPlayerPositionX - 35);
            playercollisionbotrightX = (CurrentPlayerPositionX + 35);
            playercollisiontopY = (CurrentPlayerPositionY - 35);
            for (int i = 0; i < enemyInstance.Length; i++)
            {
                CheckEnemyColl01(i);
            }
            for (int j = 0; j < itemInstance.Length; j++)
            {
                CheckItemColl01(j);
            }
            // moved outside to reduce duplicate hits
            int canAtt2 = 1;
            float enemyDamage2 = 0;
            bool contact02 = false;
            contact02 = bossInstance[0].PlayerCollide(CurrentPlayerPositionX, CurrentPlayerPositionY);
            if (contact02 == true)
            {
                testnumberT++;
                enemyDamage2 = bossInstance[0].EnemyDealDamage();
                if (canAtt2 == 1)
                {
                    playerHealthPoints += (-enemyDamage2);
                    canAtt2 = 0;
                }
            }
        }
        // player to enemy collision
        async void CheckEnemyColl01(int enemyN)
        {
            int canAtt = 1;
            float enemyDamage = 0;
            bool contact = false;
            contact = enemyInstance[enemyN].PlayerCollide(CurrentPlayerPositionX, CurrentPlayerPositionY);
            if (contact == true)
            {
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
            int canTouch = 1, itemD = 0;
            bool contact = false;
            contact = itemInstance[itemN].PlayerCollide(CurrentPlayerPositionX, CurrentPlayerPositionY);
            if (contact == true)
            {
                itemD = itemInstance[itemN].DroppedType();
                if (canTouch == 1)
                {
                    if (itemD != 0)
                    {
                        SoundBoard(4);
                    }
                    if (itemD == 2)
                    {
                        ammunition01 += 20;
                    }
                    else if (itemD == 3)
                    {
                        ammunition02 += 20;
                        if (weaponowned02 == 0)
                        {
                            ammunition01 += 10;
                        }
                    }
                    else if (itemD == 4)
                    {
                        ammunition03 += 20;
                        if (weaponowned03 == 0)
                        {
                            ammunition01 += 10;
                        }
                    }
                    else if (itemD == 5)
                    {
                        ammunition04 += 20;
                        if (weaponowned04 == 0)
                        {
                            ammunition01 += 10;
                        }
                    }
                    else if (itemD == 6)
                    {
                        ammunition05 += 20;
                        if (weaponowned05 == 0)
                        {
                            ammunition01 += 10;
                        }
                    }
                    else if (itemD == 7)
                    {
                        ammunition06 += 20;
                        if (weaponowned06 == 0)
                        {
                            ammunition01 += 10;
                        }
                    }
                    else if (itemD == 8)
                    {
                        playerHealthPoints += 75;
                        if (playerHealthPoints >= 140)
                        {
                            playerHealthPoints = 140;
                        }
                        // hp
                    }
                    else if (itemD == 9)
                    {
                        playerDamageValue += 10;
                        // temp atk
                    }
                    else if (itemD == 1)
                    {
                        PermaShopToken++;
                        // perma
                    }
                    canTouch = 0;
                    itemInstance[itemN].xposition += -1000;
                    itemInstance[itemN].yposition += -1000;
                    itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
                    itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
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
        private void ResetAll_EIP()
        {
            for (int i = 0; i < enemyInstance.Length; i++)
            {
                enemyInstance[i].xposition = -2000;
                enemyInstance[i].xleftposition = enemyInstance[i].xposition - 25;
                enemyInstance[i].xrightposition = enemyInstance[i].xposition + 25;
                enemyInstance[i].yposition = 0;
            }
            Pushgamei01();
            Pushgamei02();
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
            if (startSwitch == 0)
            {
                startSwitch++;
                currentMTrack = 1;
                SoundBoard(8);
                LoadGame();
                TitleScreenRetreatAnim();
                MainMenuReturnAnim();
                if (settingsVolume == 0)
                {
                    MusicPlayer01(currentMTrack, 1, musicVolume);
                }
                else if (settingsVolume == 1)
                {
                    MusicPlayer01(currentMTrack, 1, 0);
                }
                this.Resources["ColourOfStartGameBTNClicked"] = Colors.White;
            }
        }
        // load perma data on start ( then save perma data when closing game ) { doesn't have any functionality }
        // format = { 0 0 1 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 0 0 0 0 0 5 1000 1 0 0 0 0 0 0 0 0 0 0 } all int's
        public void LoadGame()
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                string filePath = Path.Combine(FileSystem.AppDataDirectory, "permasave.txt");
                using (StreamReader writer = new StreamReader(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    int[] numbers = content
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    PermaShopToken = numbers[0];
                    PermaDiscount = numbers[1];
                    settingsVolume = numbers[2];
                    settingsVolume2 = numbers[3];
                    settingsEnhancedGamePlay = numbers[4];
                    settingsItalienVoiceActing = numbers[5];
                    settingsGameOs = numbers[6];
                    settingsEnhancedAI = numbers[7];
                    settingsGraphics = numbers[8];
                    settingsSpiderMode = numbers[9];
                    for (int i = 0; i < levelCompleted.Length; i++)
                    {
                        levelCompleted[i] = numbers[i + 10];
                    }
                    weaponowned01 = numbers[22];
                    weaponowned02 = numbers[23];
                    weaponowned03 = numbers[24];
                    weaponowned04 = numbers[25];
                    weaponowned05 = numbers[26];
                    weaponowned06 = numbers[27];
                    PlayerDamagePerma = numbers[28];
                    PlayerATKSpeedPerma = numbers[29];
                    PermaPlayerSpeed = numbers[30];
                    PermaAtKUpg = numbers[31];
                    PermaATKSUpg = numbers[32];
                    PermaSPDUpg = numbers[33];
                    PermaGun1Upg = numbers[34];
                    PermaGun2Upg = numbers[35];
                    PermaGun3Upg = numbers[36];
                    PermaGun4Upg = numbers[37];
                    PermaGun5Upg = numbers[38];
                    PermaGun6Upg = numbers[39];
                    PermaAmmoUpg = numbers[40];
                }
            }
        }
        public void SaveGame()
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                string filePath = Path.Combine(FileSystem.AppDataDirectory, "permasave.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(PermaShopToken + " ");
                    writer.Write(PermaDiscount + " ");
                    writer.Write(settingsVolume + " ");
                    writer.Write(settingsVolume2 + " ");
                    writer.Write(settingsEnhancedGamePlay + " ");
                    writer.Write(settingsItalienVoiceActing + " ");
                    writer.Write(settingsGameOs + " ");
                    writer.Write(settingsEnhancedAI + " ");
                    writer.Write(settingsGraphics + " ");
                    writer.Write(settingsSpiderMode + " ");
                    for (int i = 0; i < levelCompleted.Length; i++)
                    {
                        writer.Write(levelCompleted[i] + " ");
                    }
                    writer.Write(weaponowned01 + " ");
                    writer.Write(weaponowned02 + " ");
                    writer.Write(weaponowned03 + " ");
                    writer.Write(weaponowned04 + " ");
                    writer.Write(weaponowned05 + " ");
                    writer.Write(weaponowned06 + " ");
                    writer.Write(PlayerDamagePerma + " ");
                    writer.Write(PlayerATKSpeedPerma + " ");
                    writer.Write(PermaPlayerSpeed + " ");
                    writer.Write(PermaAtKUpg + " ");
                    writer.Write(PermaATKSUpg + " ");
                    writer.Write(PermaSPDUpg + " ");
                    writer.Write(PermaGun1Upg + " ");
                    writer.Write(PermaGun2Upg + " ");
                    writer.Write(PermaGun3Upg + " ");
                    writer.Write(PermaGun4Upg + " ");
                    writer.Write(PermaGun5Upg + " ");
                    writer.Write(PermaGun6Upg + " ");
                    writer.Write(PermaAmmoUpg + " ");
                }
            }
        }
        private void NGameBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            MainMenuRetreatAnim();
            newgameMenuReturnAnim();
        }
        private void ConBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            MainMenuRetreatAnim();
            ContinueMenuReturnAnim();
        }
        private void TrainBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            Training_ClickedAnim();
            TestingGMenuReturnAnim();
            this.Resources["ColourOfTrainingBTNClicked"] = Colors.White;
        }
        private void MissionBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            MainMenuRetreatAnim();
            MissionMenuReturnAnim();
        }
        // shop buttons
        private void SShopBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            SuperShopMenuReturnAnim();
            MainMenuRetreatAnim();
        }
        private void ItemBuy01BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy01BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[0] != 0)
                {
                    item1cost = item1cost * 2;
                }
                purchaseAmount2[0]--;
            }
            purchaseConfirmed[0]++;
            cartTotal += item1cost;
            SoundBoard(8);
        }
        private void ItemBuy02BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy02BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[1] != 0)
                {
                    item2cost = item2cost * 2;
                }
                purchaseAmount2[1]--;
            }
            purchaseConfirmed[1]++;
            cartTotal += item2cost;
            SoundBoard(8);
        }
        private void ItemBuy03BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy03BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[2] != 0)
                {
                    item3cost = item3cost * 2;
                }
                purchaseAmount2[2]--;
            }
            purchaseConfirmed[2]++;
            cartTotal += item3cost;
            SoundBoard(8);
        }
        private void ItemBuy04BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy04BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[3] != 0)
                {
                    item4cost = item4cost * 2;
                }
                purchaseAmount2[3]--;
            }
            purchaseConfirmed[3]++;
            cartTotal += item4cost;
            SoundBoard(8);
        }
        private void ItemBuy05BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy05BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[4] != 0)
                {
                    item5cost = item5cost * 2;
                }
                purchaseAmount2[4]--;
            }
            purchaseConfirmed[4]++;
            cartTotal += item5cost;
            SoundBoard(8);
        }
        private void ItemBuy06BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy06BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[5] != 0)
                {
                    item6cost = item6cost * 2;
                }
                purchaseAmount2[5]--;
            }
            purchaseConfirmed[5]++;
            cartTotal += item6cost;
            SoundBoard(8);
        }
        private void ItemBuy07BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy07BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[6] != 0)
                {
                    item7cost = item7cost * 2;
                }
                purchaseAmount2[6]--;
            }
            purchaseConfirmed[6]++;
            cartTotal += item7cost;
            SoundBoard(8);
        }
        private void ItemBuy08BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy08BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[7] != 0)
                {
                    item1cost = item8cost * 2;
                }
                purchaseAmount2[7]--;
            }
            purchaseConfirmed[7]++;
            cartTotal += item8cost;
            SoundBoard(8);
        }
        private void ItemBuy09BTN_Clicked(object sender, EventArgs e)
        {
            this.Resources["ColourOfItemBuy09BTNClicked"] = Colors.Orange;
            for (int i = 0; i < 10; ++i)
            {
                if (purchaseAmount2[8] != 0)
                {
                    item9cost = item9cost * 2;
                }
                purchaseAmount2[8]--;
            }
            purchaseConfirmed[8]++;
            cartTotal += item9cost;
            SoundBoard(8);
        }
        private void BuyConfirmBTN_Clicked(object sender, EventArgs e)
        {
            int[] tempunit = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            this.Resources["ColourOfBuyConfirm9BTNClicked"] = Colors.Blue;
            SoundBoard(8);
            if (PermaShopToken >= cartTotal)
            {
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[0] != 0)
                    {
                        PermaAmmoUpg +=20;                  
                        purchaseConfirmed[0]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[1] != 0)
                    {
                        PermaAtKUpg += 3;
                        purchaseConfirmed[1]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[2] != 0)
                    {
                        if (PermaATKSUpg >= 200)
                        {
                            PermaATKSUpg += (-100);
                        }
                        purchaseConfirmed[2]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[3] != 0)
                    {
                        PermaGun1Upg += 1;
                        purchaseConfirmed[3]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[4] != 0)
                    {
                        PermaGun2Upg += 1;
                        purchaseConfirmed[4]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[5] != 0)
                    {
                        PermaGun3Upg += 1;
                        purchaseConfirmed[5]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[6] != 0)
                    {
                        PermaGun4Upg += 1;
                        purchaseConfirmed[6]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[7] != 0)
                    {
                        PermaGun5Upg += 1;
                        purchaseConfirmed[7]--;
                    }
                }
                for (int i = 0; i < 10; ++i)
                {
                    if (purchaseConfirmed[8] != 0)
                    {
                        PermaGun6Upg += 1;
                        purchaseConfirmed[8]--;
                    }
                }
            }
            delayC();
            SaveGame();
        }
        async void delayC()
        {
            await Task.Delay(250);
            this.Resources["ColourOfItemBuy01BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy02BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy03BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy04BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy05BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy06BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy07BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy08BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfItemBuy09BTNClicked"] = Colors.Grey;
            this.Resources["ColourOfBuyConfirmBTNClicked"] = Colors.White;
        }
        private void BrutaBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            BrutalDiff();
            Brutal_ClickedAnim();
            MainMenu_Exit();
            this.Resources["ColourOfBrutalBTNClicked"] = Colors.White;
        }
        private void ChallBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            ChallengeMenuReturnAnim();
            MainMenuRetreatAnim();
        }
        private void usesave()
        {
            gamestatus = 1;
            BackgroundCurrentPositionX = 0;
            BackgroundCurrentPositionY = -2350;
            ammunition01 += 50;
            ammunition01 += PermaAmmoUpg;
            ammunitioncurrent = ammunition01;
            MainMenu_Exit();
            showallgamecontent();
            if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                Tutorial_activate();
                tutorialactivated++;
            }
            Tutorial_activate();
            Update_All_Position_Constant();
            Enemy_AI_01();
            Enemy_AI_02();

            difficultysetting = 1;
            newgamedifficulty = 1;
            gamelevelflag = 1;
            weaponEquipped = 0;
            DifficultyChanger();
        }
        private void MusicBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            MusicMenuReturnAnim();
            MainMenuRetreatAnim();
        }
        private void Music1BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 1;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void Music2BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 2;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void Music3BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 3;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void Music4BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 4;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void Music5BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 5;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void Music6BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 6;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void Music7BTN_Clicked(object sender, EventArgs e)
        {
            MusicPlayer01(currentMTrack, 0, musicVolume);
            currentMTrack = 7;
            MusicPlayer01(currentMTrack, 1, musicVolume);
            musicLockKL = 1;
        }
        private void SettingsBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            SettingsMenuReturnAnim();
            MainMenuRetreatAnim();
        }
        // new game menu buttons
        private void EasyDBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            Easy_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.White;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
            newgamedifficulty = 1;
        }
        private void NormDBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            Normal_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.White;
            this.Resources["ColourOfHardBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
            newgamedifficulty = 2;
        }
        private void HardDBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            Hard_ClickedAnim();
            this.Resources["ColourOfEasyBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfNormalBTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfHardBTNClicked"] = Colors.White;
            this.Resources["ColourOfVeryHardBTNClicked"] = Colors.DarkSlateGrey;
            newgamedifficulty = 3;
        }
        private void VHardDBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
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
                    enemyInstance[i].healthpoints = enemytype1hp * 2;
                }
                for (int i = 0; i < eliteEnemyInstance.Length; i++)
                {
                    eliteEnemyInstance[i].healthpoints = enemytype2hp + (enemytype2hp / 2);
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp + (boss1hp / 2);
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
                    eliteEnemyInstance[i].damagevar = enemytype2dmg * 2;
                }
                for (int i = 0; i < bossInstance.Length; i++)
                {
                    bossInstance[i].healthpoints = boss1hp * 2;
                    bossInstance[i].damagevar = boss1dmg * 2;
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
            SoundBoard(8);
            if (missionselected == 1)
            {
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
            SoundBoard(8);
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
            SoundBoard(8);
            // no lvl2 in demo, no stats
        }
        private void Accept01BTN_Clicked(object sender, EventArgs e)// new game accept
        {
            NewGameStart();
        }
        private void NewGameStart()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                ammunition01 += 450;
                newgamedifficulty = 1;
            }
            SoundBoard(8);
            if (newgamedifficulty == 1) // easy
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 += 50;
                ammunition01 += PermaAmmoUpg;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Tutorial_activate();
                    tutorialactivated++;
                }
                Update_All_Position_Constant();
                Enemy_AI_01();
                Enemy_AI_02();
            }
            else if (newgamedifficulty == 2) // normal
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 += 30;
                ammunition01 += PermaAmmoUpg;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Tutorial_activate();
                    tutorialactivated++;
                }
                Update_All_Position_Constant();
                Enemy_AI_01();
                Enemy_AI_02();
            }
            else if (newgamedifficulty == 3) // hard
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 += 20;
                ammunition01 += PermaAmmoUpg;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Tutorial_activate();
                    tutorialactivated++;
                }
                Update_All_Position_Constant();
                Enemy_AI_01();
                Enemy_AI_02();
            }
            else if (newgamedifficulty == 4) // very hard
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 += 10;
                ammunition01 += PermaAmmoUpg;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Tutorial_activate();
                    tutorialactivated++;
                }
                Update_All_Position_Constant();
                Enemy_AI_01();
                Enemy_AI_02();
            }
            if (settingsVolume == 0)
            {
                if (newgamedifficulty != 0 && musicLockKL == 0)
                {
                    MusicPlayer01(currentMTrack, 0, musicVolume);
                    currentMTrack = (newgamedifficulty + 1);
                    MusicPlayer01(currentMTrack, 1, musicVolume);
                }
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
            ResetButtonColours();
            ResetAll_Button_States_Anim();
            Reset_missions_states();
        }
        private void Accept02BTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            if (saveselected == 1) // 1
            {
                LoadGameInfo();
                usesave();
                MainMenu_Exit();
            }
            else if (saveselected == 2) // 2
            {

                LoadGameInfo();
                usesave();
                MainMenu_Exit();
            }
            else if (saveselected == 3) // 3
            {
                LoadGameInfo();
                usesave();
                MainMenu_Exit();
            }
        }
        
        public void LoadGameInfo()
        {

            if (saveselected == 1)
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    string filePath = Path.Combine(FileSystem.AppDataDirectory, "saveslot1.txt");
                    using (StreamReader writer = new StreamReader(filePath))
                    {
                        string content = File.ReadAllText(filePath);
                        int[] numbers = content
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

                        levelsCompleted = numbers[0];
                        for (int i = 0; i < levelStatistics01.Length; i++)
                        {
                            levelStatistics01[i] = numbers[i+1];
                        }
                    }
                }
            }
            else if (saveselected == 2)
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    string filePath = Path.Combine(FileSystem.AppDataDirectory, "saveslot2.txt");
                    using (StreamReader writer = new StreamReader(filePath))
                    {
                        string content = File.ReadAllText(filePath);
                        int[] numbers = content
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

                        levelsCompleted = numbers[0];
                        for (int i = 0; i < levelStatistics01.Length; i++)
                        {
                            levelStatistics01[i] = numbers[i + 1];
                        }
                    }
                }
            }
            else if (saveselected == 3)
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    string filePath = Path.Combine(FileSystem.AppDataDirectory, "saveslot3.txt");
                    using (StreamReader writer = new StreamReader(filePath))
                    {
                        string content = File.ReadAllText(filePath);
                        int[] numbers = content
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

                        levelsCompleted = numbers[0];
                        for (int i = 0; i < levelStatistics01.Length; i++)
                        {
                            levelStatistics01[i] = numbers[i + 1];
                        }
                    }
                }
            }
        }
        private void Accept03BTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            gamestatus = 1;
            BackgroundCurrentPositionX = 0;
            BackgroundCurrentPositionY = -2350;
            ammunition01 += 50;
            ammunition01 += PermaAmmoUpg;
            ammunitioncurrent = ammunition01;
            MainMenu_Exit();
            showallgamecontent();
            Level_Activate_01();
            if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                Tutorial_activate();
                tutorialactivated++;
            }
            Update_All_Position_Constant();
            Enemy_AI_01();
            Enemy_AI_02();
            difficultysetting = 1;
            newgamedifficulty = 1;
            gamelevelflag = 1;
            weaponEquipped = 0;
            DifficultyChanger();
        }
        private void BrutalDiff()
        {
            SoundBoard(8);
            gamestatus = 1;
            BackgroundCurrentPositionX = 0;
            BackgroundCurrentPositionY = -2350;
            ammunition01 += 10;
            ammunition01 += PermaAmmoUpg;
            ammunitioncurrent = ammunition01;
            MainMenu_Exit();
            showallgamecontent();
            Level_Activate_01();
            Update_All_Position_Constant();
            Enemy_AI_01();
            Enemy_AI_02();
            difficultysetting = 5;
            newgamedifficulty = 5;
            gamelevelflag = 1;
            weaponEquipped = 0;
            DifficultyChanger();
        }
        private void Accept04BTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            if (missionselected == 1)
            {
                gamestatus = 1;
                BackgroundCurrentPositionX = 0;
                BackgroundCurrentPositionY = -2350;
                ammunition01 += 30;
                ammunition01 += PermaAmmoUpg;
                ammunitioncurrent = ammunition01;
                MainMenu_Exit();
                showallgamecontent();
                Level_Activate_01();
                if (tutorialactivated == 0 && DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Tutorial_activate();
                    tutorialactivated++;
                }
                Update_All_Position_Constant();
                Enemy_AI_01();
                Enemy_AI_02();
                difficultysetting = 2;
                newgamedifficulty = 2;
                gamelevelflag = 1;
                weaponEquipped = 0;
                DifficultyChanger();
            }
            else if (missionselected == 2)
            {
                // no level 2 in this demo
            }
            else if (missionselected == 3)
            {
                // no level 3 in this demo
            }
            else if (missionselected == 4)
            {
                // no level 4 in this demo
            }
        }
        private void Save1BTN_Clicked(object sender, EventArgs e) // save accept btn = accept02
        {
            SoundBoard(8);
            Save1_ClickedAnim();
            this.Resources["ColourOfSave1BTNClicked"] = Colors.White;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.DarkSlateGrey;
            saveselected = 1;
        }
        private void Save2BTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            Save2_ClickedAnim();
            this.Resources["ColourOfSave1BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.White;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.DarkSlateGrey;
            saveselected = 2;
        }
        private void Save3BTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
            Save3_ClickedAnim();
            this.Resources["ColourOfSave1BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave2BTNClicked"] = Colors.DarkSlateGrey;
            this.Resources["ColourOfSave3BTNClicked"] = Colors.White;
            saveselected = 3;
        }
        private void DelSaveBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
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
            SoundBoard(8);
            MainMenuReturnAnim();
            newgameMenuRetreatAnim();
            ContinueMenuRetreatAnim();
            TestingGMenuRetreatAnim();
            MissionMenuRetreatAnim();
            SuperShopMenuRetreatAnim();
            ChallengeMenuRetreatAnim();
            MusicMenuRetreatAnim();
            SettingsMenuRetreatAnim();
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
        private void GameEndBTN_Clicked(object sender, EventArgs e)
        {
            playerHealthPoints = 140;
            SoundBoard(8);
            SaveGame();
            GameAppear();
        }
        // tutorial stuff
        private void TutorialBTN_Clicked(object sender, EventArgs e)
        {
            SoundBoard(8);
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
            await TutorialBox01.TranslateTo(1700, 100, 800);
        }
        async void Tutorial_dectivate02()
        {
            await TutorialBox02.TranslateTo(1500, 0, 800);
        }
        async void Tutorial_dectivate03()
        {
            await tutorialdynamictext.TranslateTo(1700, 100, 800);
        }
        async void Tutorial_dectivate04()
        {
            await Tutorialbutton.TranslateTo(1500, 160, 800);
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

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuRetreat01ANDR();
                SeperatedMenuRetreat02ANDR();
                SeperatedMenuRetreat09ANDR();
                SeperatedMenuRetreat10ANDR();
                SeperatedMenuRetreat18ANDR();          
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
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
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuReturn01ANDR();
                SeperatedMenuReturn02ANDR();
                SeperatedMenuReturn09ANDR();
                SeperatedMenuReturn10ANDR();
                SeperatedMenuReturn18ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
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
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuRetreat11ANDR();
                SeperatedMenuRetreat12ANDR();
                SeperatedMenuRetreat13ANDR();
                SeperatedMenuRetreat14ANDR();
                SeperatedMenuRetreat15ANDR();
                SeperatedMenuRetreat16ANDR();
                SeperatedMenuRetreat17ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuRetreat11();
                SeperatedMenuRetreat12();
                SeperatedMenuRetreat13();
                SeperatedMenuRetreat14();
                SeperatedMenuRetreat15();
                SeperatedMenuRetreat16();
                SeperatedMenuRetreat17();
            }          
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
            await accept01button.TranslateTo(125, (195 - 1000), 500);

        }
        async void SeperatedMenuRetreat16()
        {
            await leavebutton.TranslateTo(250, (195 - 1000), 500);
        }
        async void SeperatedMenuRetreat17()
        {
            await NewGameScreen01.TranslateTo(0, -1000, 500);
        }
        private void newgameMenuReturnAnim() // seperated between multiples to all move in sync at once
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuReturn11ANDR();
                SeperatedMenuReturn12ANDR();
                SeperatedMenuReturn13ANDR();
                SeperatedMenuReturn14ANDR();
                SeperatedMenuReturn15ANDR();
                SeperatedMenuReturn16ANDR();
                SeperatedMenuReturn17ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuReturn11();
                SeperatedMenuReturn12();
                SeperatedMenuReturn13();
                SeperatedMenuReturn14();
                SeperatedMenuReturn15();
                SeperatedMenuReturn16();
                SeperatedMenuReturn17();
            }       
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
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuRereat19ANDR();
                SeperatedMenuRereat20ANDR();
                SeperatedMenuRereat21ANDR();
                SeperatedMenuRereat22ANDR();
                SeperatedMenuRereat23ANDR();
                SeperatedMenuRereat24ANDR();
                SeperatedMenuRereat25ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuRereat19();
                SeperatedMenuRereat20();
                SeperatedMenuRereat21();
                SeperatedMenuRereat22();
                SeperatedMenuRereat23();
                SeperatedMenuRereat24();
                SeperatedMenuRereat25();
            }           
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
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuReturn19ANDR();
                SeperatedMenuReturn20ANDR();
                SeperatedMenuReturn21ANDR();
                SeperatedMenuReturn22ANDR();
                SeperatedMenuReturn23ANDR();
                SeperatedMenuReturn24ANDR();
                SeperatedMenuReturn25ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuReturn19();
                SeperatedMenuReturn20();
                SeperatedMenuReturn21();
                SeperatedMenuReturn22();
                SeperatedMenuReturn23();
                SeperatedMenuReturn24();
                SeperatedMenuReturn25();
            }          
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
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuRetreat32();
                SeperatedMenuRetreat33();
                SeperatedMenuRetreat34();
                SeperatedMenuRetreat35();
                SeperatedMenuRetreat36();
            }           
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
            await leave03button.TranslateTo(75, (40 - 1000), 5);
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
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuReturn32();
                SeperatedMenuReturn33();
                SeperatedMenuReturn34();
                SeperatedMenuReturn35();
                SeperatedMenuReturn36();
            }          
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
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
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
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
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
            await leave04button.TranslateTo(250, 195, 500);
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
        async void SeperatedMenuReturn39()
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
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuRetreat44();
                SeperatedMenuRetreat45();
                SeperatedMenuRetreat46();
                SeperatedMenuRetreat47();
                SeperatedMenuRetreat48();
                SeperatedMenuRetreat49();
                SeperatedMenuRetreat50();
                SeperatedMenuRetreat51();
                SeperatedMenuRetreat52();
                SeperatedMenuRetreat53();
                SeperatedMenuRetreat54();
                SeperatedMenuRetreat55();
                SeperatedMenuRetreat56();
                SeperatedMenuRetreat57();
                SeperatedMenuRetreat58();
                SeperatedMenuRetreat59();
                SeperatedMenuRetreat60();
                SeperatedMenuRetreat61();
                SeperatedMenuRetreat62();
                SeperatedMenuRetreat63();
                SeperatedMenuRetreat64();
                SeperatedMenuRetreat65();
            }          
        }
        async void SeperatedMenuRetreat44()
        {
            await Permasupershopscreen.TranslateTo(0, -1000, 500);
        }
        async void SeperatedMenuRetreat45()
        {
            await Attackupimage.TranslateTo(-260, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat46()
        {
            await Speedupimage.TranslateTo(-140, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat47()
        {
            await gun1image.TranslateTo(70, (-175 - 1000), 500);
        }
        async void SeperatedMenuRetreat48()
        {
            await gun2image.TranslateTo(70, (-110 - 1000), 500);
        }
        async void SeperatedMenuRetreat49()
        {
            await gun3image.TranslateTo(70, (-45 -1000), 500);
        }
        async void SeperatedMenuRetreat50()
        {
            await gun4image.TranslateTo(70, (20 - 1000), 500);
        }
        async void SeperatedMenuRetreat51()
        {
            await gun5image.TranslateTo(70, (85 - 1000), 500);
        }
        async void SeperatedMenuRetreat52()
        {
            await gun6image.TranslateTo(70, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat53()
        {
            await Ammoupitemimage.TranslateTo(-380, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat54()
        {
            await permastoretextbutton.TranslateTo(-390, (-228 - 1000), 500);
        }
        async void SeperatedMenuRetreat55()
        {
            await buyconfirmbutton.TranslateTo(200, (220 - 1000), 500);
        }
        async void SeperatedMenuRetreat56()
        {
            await leave05button.TranslateTo(350, (220 - 1000), 500);
        }
        async void SeperatedMenuRetreat57()
        {
            await buyitem02button.TranslateTo(-260, (-60 - 1000), 500);
        }
        async void SeperatedMenuRetreat58()
        {
            await buyitem03button.TranslateTo(-140, (-60 - 1000), 500);
        }
        async void SeperatedMenuRetreat59()
        {
            await buyitem04button.TranslateTo(270, (-175 - 1000), 500);
        }
        async void SeperatedMenuRetreat60()
        {
            await buyitem05button.TranslateTo(270, (-110 - 1000), 500);
        }
        async void SeperatedMenuRetreat61()
        {
            await buyitem06button.TranslateTo(270, (-45 - 1000), 500);
        }
        async void SeperatedMenuRetreat62()
        {
            await buyitem07button.TranslateTo(270, (20 - 1000), 500);
        }
        async void SeperatedMenuRetreat63()
        {
            await buyitem08button.TranslateTo(270, (85 - 1000), 500);
        }
        async void SeperatedMenuRetreat64()
        {
            await buyitem09button.TranslateTo(270, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat65()
        {
            await buyitem01button.TranslateTo(-380, (-60 - 1000), 500);
        }
        private void SuperShopMenuReturnAnim()
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuReturn44();
                SeperatedMenuReturn45();
                SeperatedMenuReturn46();
                SeperatedMenuReturn47();
                SeperatedMenuReturn48();
                SeperatedMenuReturn49();
                SeperatedMenuReturn50();
                SeperatedMenuReturn51();
                SeperatedMenuReturn52();
                SeperatedMenuReturn53();
                SeperatedMenuReturn54();
                SeperatedMenuReturn55();
                SeperatedMenuReturn56();
                SeperatedMenuReturn57();
                SeperatedMenuReturn58();
                SeperatedMenuReturn59();
                SeperatedMenuReturn60();
                SeperatedMenuReturn61();
                SeperatedMenuReturn62();
                SeperatedMenuReturn63();
                SeperatedMenuReturn64();
                SeperatedMenuReturn65();
            }         
        }
        async void SeperatedMenuReturn44()
        {
            await Permasupershopscreen.TranslateTo(0, 0, 500);
        }
        async void SeperatedMenuReturn45()
        {
            await Attackupimage.TranslateTo(-260, -150, 500);
        }
        async void SeperatedMenuReturn46()
        {
            await Speedupimage.TranslateTo(-140, -150, 500);
        }
        async void SeperatedMenuReturn47()
        {
            await gun1image.TranslateTo(70, -175, 500);
        }
        async void SeperatedMenuReturn48()
        {
            await gun2image.TranslateTo(70, -110, 500);
        }
        async void SeperatedMenuReturn49()
        {
            await gun3image.TranslateTo(70, -45, 500);
        }
        async void SeperatedMenuReturn50()
        {
            await gun4image.TranslateTo(70, 20, 500);
        }
        async void SeperatedMenuReturn51()
        {
            await gun5image.TranslateTo(70, 85, 500);
        }
        async void SeperatedMenuReturn52()
        {
            await gun6image.TranslateTo(70, 150, 500);
        }
        async void SeperatedMenuReturn53()
        {
            await Ammoupitemimage.TranslateTo(-380, -150, 500);
        }
        async void SeperatedMenuReturn54()
        {
            await permastoretextbutton.TranslateTo(-390, -228, 500);
        }
        async void SeperatedMenuReturn55()
        {
            await buyconfirmbutton.TranslateTo(200, 220, 500);
        }
        async void SeperatedMenuReturn56()
        {
            await leave05button.TranslateTo(350, 220, 500);
        }
        async void SeperatedMenuReturn57()
        {
            await buyitem02button.TranslateTo(-260, -60, 500);
        }
        async void SeperatedMenuReturn58()
        {
            await buyitem03button.TranslateTo(-140, -60, 500);
        }
        async void SeperatedMenuReturn59()
        {
            await buyitem04button.TranslateTo(270, -175, 500);
        }
        async void SeperatedMenuReturn60()
        {
            await buyitem05button.TranslateTo(270, -110, 500);
        }
        async void SeperatedMenuReturn61()
        {
            await buyitem06button.TranslateTo(270, -45, 500);
        }
        async void SeperatedMenuReturn62()
        {
            await buyitem07button.TranslateTo(270, 20, 500);
        }
        async void SeperatedMenuReturn63()
        {
            await buyitem08button.TranslateTo(270, 85, 500);
        }
        async void SeperatedMenuReturn64()
        {
            await buyitem09button.TranslateTo(270, 150, 500);
        }
        async void SeperatedMenuReturn65()
        {
            await buyitem01button.TranslateTo(-380, -60, 500);
        }
        private void ChallengeMenuRetreatAnim()
        {
            //MissionMenuRetreatAnim(); // returns mission menu screen 
        }
        private void ChallengeMenuReturnAnim()
        {
            MissionMenuReturnAnim();
        }
        private void SettingsMenuRetreatAnim()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuRetreat66ANDR();
                SeperatedMenuRetreat67ANDR();
                SeperatedMenuRetreat68ANDR();
                SeperatedMenuRetreat69ANDR();
                SeperatedMenuRetreat70ANDR();
                SeperatedMenuRetreat71ANDR();
                SeperatedMenuRetreat72ANDR();
                SeperatedMenuRetreat73ANDR();
                SeperatedMenuRetreat74ANDR();
                SeperatedMenuRetreat75ANDR();
                SeperatedMenuRetreat76ANDR();
                SeperatedMenuRetreat77ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuRetreat66();
                SeperatedMenuRetreat67();
                SeperatedMenuRetreat68();
                SeperatedMenuRetreat69();
                SeperatedMenuRetreat70();
                SeperatedMenuRetreat71();
                SeperatedMenuRetreat72();
                SeperatedMenuRetreat73();
                SeperatedMenuRetreat74();
                SeperatedMenuRetreat75();
                SeperatedMenuRetreat76();
                SeperatedMenuRetreat77();
            }        
        }
        async void SeperatedMenuRetreat66()
        {
            await Settingmusicscreen.TranslateTo(0, -1000, 500);
        }
        async void SeperatedMenuRetreat67()
        {
            await mainmenubutton01.TranslateTo(-260, (-150 - 1000), 500); 
        }
        async void SeperatedMenuRetreat68()
        {
            await mainmenubutton02.TranslateTo(-260, (-50 - 1000), 500);          
        }
        async void SeperatedMenuRetreat69()
        {
            await mainmenubutton03.TranslateTo(-260, (50 - 1000), 500);
        }
        async void SeperatedMenuRetreat70()
        {
            await mainmenubutton04.TranslateTo(-260, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat71()
        {
            await mainmenubutton05.TranslateTo(0, (220 - 1000), 500);
        }
        async void SeperatedMenuRetreat72()
        {
            await mainmenubutton06.TranslateTo(260, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat73()
        {
            await mainmenubutton07.TranslateTo(260, (-50 - 1000), 500);
        }
        async void SeperatedMenuRetreat74()
        {
            await mainmenubutton08.TranslateTo(260, (50 - 1000), 500);
        }
        async void SeperatedMenuRetreat75()
        {
            await mainmenubutton09.TranslateTo(260, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat76()
        {
            await mainmenutextbutton.TranslateTo(0, (-220 - 1000), 500);         
        }
        async void SeperatedMenuRetreat77()
        {
            await leave06button.TranslateTo(350, (220 - 1000), 500);
        }
        private void SettingsMenuReturnAnim()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                SeperatedMenuReturn66ANDR();
                SeperatedMenuReturn67ANDR();
                SeperatedMenuReturn68ANDR();
                SeperatedMenuReturn69ANDR();
                SeperatedMenuReturn70ANDR();
                SeperatedMenuReturn71ANDR();
                SeperatedMenuReturn72ANDR();
                SeperatedMenuReturn73ANDR();
                SeperatedMenuReturn74ANDR();
                SeperatedMenuReturn75ANDR();
                SeperatedMenuReturn76ANDR();
                SeperatedMenuReturn77ANDR();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuReturn66();
                SeperatedMenuReturn67();
                SeperatedMenuReturn68();
                SeperatedMenuReturn69();
                SeperatedMenuReturn70();
                SeperatedMenuReturn71();
                SeperatedMenuReturn72();
                SeperatedMenuReturn73();
                SeperatedMenuReturn74();
                SeperatedMenuReturn75();
                SeperatedMenuReturn76();
                SeperatedMenuReturn77();
            }           
        }
        async void SeperatedMenuReturn66()
        {
            await Settingmusicscreen.TranslateTo(0, 0, 500);
        }
        async void SeperatedMenuReturn67()
        {
            await mainmenubutton01.TranslateTo(-260, -150, 500);            
        }
        async void SeperatedMenuReturn68()
        {
            await mainmenubutton02.TranslateTo(-260, -50, 500);           
        }
        async void SeperatedMenuReturn69()
        {
            await mainmenubutton03.TranslateTo(-260, 50, 500);            
        }
        async void SeperatedMenuReturn70()
        {
            await mainmenubutton04.TranslateTo(-260, 150, 500);            
        }
        async void SeperatedMenuReturn71()
        {
            await mainmenubutton05.TranslateTo(0, 220, 500);            
        }
        async void SeperatedMenuReturn72()
        {
            await mainmenubutton06.TranslateTo(260, -150, 500);            
        }
        async void SeperatedMenuReturn73()
        {
            await mainmenubutton07.TranslateTo(260, -50, 500);            
        }
        async void SeperatedMenuReturn74()
        {
            await mainmenubutton08.TranslateTo(260, 50, 500);            
        }
        async void SeperatedMenuReturn75()
        {
            await mainmenubutton09.TranslateTo(260, 150, 500);            
        }
        async void SeperatedMenuReturn76()
        {
            await mainmenutextbutton.TranslateTo(0, -220, 500);
        }
        async void SeperatedMenuReturn77()
        {
            await leave06button.TranslateTo(350, 220, 500);
        }
        private void MusicMenuRetreatAnim()
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuRetreat66();
                SeperatedMenuRetreat78();
                SeperatedMenuRetreat79();
                SeperatedMenuRetreat80();
                SeperatedMenuRetreat81();
                SeperatedMenuRetreat82();
                SeperatedMenuRetreat83();
                SeperatedMenuRetreat84();
                SeperatedMenuRetreat85();
                SeperatedMenuRetreat86();
            }            
        }
        async void SeperatedMenuRetreat78()
        {
            await musicbutton01.TranslateTo(-250, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat79()
        {
            await musicbutton02.TranslateTo(-250, (-50 - 1000), 500);
        }
        async void SeperatedMenuRetreat80()
        {
            await musicbutton03.TranslateTo(-250, (50 - 1000), 500);
        }
        async void SeperatedMenuRetreat81()
        {
            await musicbutton04.TranslateTo(-250, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat82()
        {
            await musicbutton05.TranslateTo(250, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat83()
        {
            await musicbutton06.TranslateTo(250, (-50 - 1000), 500);
        }
        async void SeperatedMenuRetreat84()
        {
            await musicbutton07.TranslateTo(250, (50 - 1000), 500);
        }
        async void SeperatedMenuRetreat85()
        {
            await musictextbutton.TranslateTo(0, (-220 - 1000), 500);
        }
        async void SeperatedMenuRetreat86()
        {
            await leave07button.TranslateTo(350, (220-1000), 500);
        }
        private void MusicMenuReturnAnim()
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                SeperatedMenuReturn66();
                SeperatedMenuReturn78();
                SeperatedMenuReturn79();
                SeperatedMenuReturn80();
                SeperatedMenuReturn81();
                SeperatedMenuReturn82();
                SeperatedMenuReturn83();
                SeperatedMenuReturn84();
                SeperatedMenuReturn85();
                SeperatedMenuReturn86();
            }           
        }
        async void SeperatedMenuReturn78()
        {
            await musicbutton01.TranslateTo(-250, -150, 500);           
        }
        async void SeperatedMenuReturn79()
        {
            await musicbutton02.TranslateTo(-250, -50, 500);            
        }
        async void SeperatedMenuReturn80()
        {
            await musicbutton03.TranslateTo(-250, 50, 500);            
        }
        async void SeperatedMenuReturn81()
        {
            await musicbutton04.TranslateTo(-250, 150, 500);            
        }
        async void SeperatedMenuReturn82()
        {
            await musicbutton05.TranslateTo(250, -150, 500);            
        }
        async void SeperatedMenuReturn83()
        {
            await musicbutton06.TranslateTo(250, -50, 500);          
        }
        async void SeperatedMenuReturn84()
        {
            await musicbutton07.TranslateTo(250, 50, 500);          
        }
        async void SeperatedMenuReturn85()
        {
            await musictextbutton.TranslateTo(0, -220, 500);            
        }
        async void SeperatedMenuReturn86()
        {
            await leave07button.TranslateTo(350, 220, 500);
        }
        // android menu anim's
        async void SeperatedMenuRetreat01ANDR()
        {
            await NewGamebutton.TranslateTo(-375, 1185, 500);
        }
        async void SeperatedMenuRetreat02ANDR()
        {
            await Continuebutton.TranslateTo(-250, 1187, 500);
        }
        async void SeperatedMenuRetreat09ANDR()
        {
            await Settingsbutton.TranslateTo(400, (-140 + 1000), 500);
        }
        async void SeperatedMenuRetreat10ANDR()
        {
            await TitleScreen01.TranslateTo(0, 1000, 500);
        }
        async void SeperatedMenuRetreat18ANDR()
        {
            await BattleForAzuraTitle.TranslateTo(-50, 1000, 500);
        }      
        async void SeperatedMenuReturn01ANDR()
        {
            await NewGamebutton.TranslateTo(-150, 120, 500);
        }
        async void SeperatedMenuReturn02ANDR()
        {
            await Continuebutton.TranslateTo(-0, 122, 500);
        }
        async void SeperatedMenuReturn09ANDR()
        {
            await Settingsbutton.TranslateTo(320, -124, 500);
        }
        async void SeperatedMenuReturn10ANDR()
        {
            await TitleScreen01.TranslateTo(0, 0, 500);
        }
        async void SeperatedMenuReturn18ANDR()
        {
            await BattleForAzuraTitle.TranslateTo(-50, 0, 500);
        }
        // new game menu
        async void SeperatedMenuRetreat11ANDR()
        {
            await easydiffbutton.TranslateTo(-280, -1050, 500);
        }
        async void SeperatedMenuRetreat12ANDR()
        {
            await normaldiffbutton.TranslateTo(-280, -1020, 500);
        }
        async void SeperatedMenuRetreat13ANDR()
        {
            await harddiffbutton.TranslateTo(-280, -990, 500);
        }
        async void SeperatedMenuRetreat14ANDR()
        {
            await veryharddiffbutton.TranslateTo(-280, -960, 500);
        }
        async void SeperatedMenuRetreat15ANDR()
        {
            await accept01button.TranslateTo(125, (195 - 1000), 500);
        }
        async void SeperatedMenuRetreat16ANDR()
        {
            await leavebutton.TranslateTo(250, (195 - 1000), 500);
        }
        async void SeperatedMenuRetreat17ANDR()
        {
            await NewGameScreen01.TranslateTo(0, -1000, 500);
        }       
        async void SeperatedMenuReturn11ANDR()
        {
            await easydiffbutton.TranslateTo(-240, -50, 500);
        }
        async void SeperatedMenuReturn12ANDR()
        {
            await normaldiffbutton.TranslateTo(-240, -20, 500);
        }
        async void SeperatedMenuReturn13ANDR()
        {
            await harddiffbutton.TranslateTo(-240, 10, 500);
        }
        async void SeperatedMenuReturn14ANDR()
        {
            await veryharddiffbutton.TranslateTo(-240, 40, 500);
        }
        async void SeperatedMenuReturn15ANDR()
        {
            await accept01button.TranslateTo(0, 20, 500);
        }
        async void SeperatedMenuReturn16ANDR()
        {
            await leavebutton.TranslateTo(125, 20, 500);
        }
        async void SeperatedMenuReturn17ANDR()
        {
            await NewGameScreen01.TranslateTo(0, 0, 500);
        }
        // continue menu
        async void SeperatedMenuRereat19ANDR()
        {
            await saveslot1button.TranslateTo(-325, -1050, 500);
        }
        async void SeperatedMenuRereat20ANDR()
        {
            await saveslot2button.TranslateTo(-325, -1000, 500);
        }
        async void SeperatedMenuRereat21ANDR()
        {
            await saveslot3button.TranslateTo(-325, -950, 500);
        }
        async void SeperatedMenuRereat22ANDR()
        {
            await deletesavebutton.TranslateTo(0, (195 - 1000), 500);
        }
        async void SeperatedMenuRereat23ANDR()
        {
            await accept02button.TranslateTo(125, (195 - 1000), 500);
        }
        async void SeperatedMenuRereat24ANDR()
        {
            await leave02button.TranslateTo(250, (195 - 1000), 500);
        }
        async void SeperatedMenuRereat25ANDR()
        {
            await ContinueScreen01.TranslateTo(0, -1000, 500);
        }        
        async void SeperatedMenuReturn19ANDR()
        {
            await saveslot1button.TranslateTo(-275, -50, 500);
        }
        async void SeperatedMenuReturn20ANDR()
        {
            await saveslot2button.TranslateTo(-275, 0, 500);
        }
        async void SeperatedMenuReturn21ANDR()
        {
            await saveslot3button.TranslateTo(-275, 50, 500);
        }
        async void SeperatedMenuReturn22ANDR()
        {
            await deletesavebutton.TranslateTo(0, 110, 500);
        }
        async void SeperatedMenuReturn23ANDR()
        {
            await accept02button.TranslateTo(125, 110, 500);
        }
        async void SeperatedMenuReturn24ANDR()
        {
            await leave02button.TranslateTo(250, 110, 500);
        }
        async void SeperatedMenuReturn25ANDR()
        {
            await ContinueScreen01.TranslateTo(0, 0, 500);
        }
        // settings andr
        async void SeperatedMenuRetreat66ANDR()
        {
            await Settingmusicscreen.TranslateTo(0, -1000, 500);
        }
        async void SeperatedMenuRetreat67ANDR()
        {
            await mainmenubutton01.TranslateTo(-260, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat68ANDR()
        {
            await mainmenubutton02.TranslateTo(-260, (-50 - 1000), 500);
        }
        async void SeperatedMenuRetreat69ANDR()
        {
            await mainmenubutton03.TranslateTo(-260, (50 - 1000), 500);
        }
        async void SeperatedMenuRetreat70ANDR()
        {
            await mainmenubutton04.TranslateTo(-260, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat71ANDR()
        {
            await mainmenubutton05.TranslateTo(0, (220 - 1000), 500);

        }
        async void SeperatedMenuRetreat72ANDR()
        {
            await mainmenubutton06.TranslateTo(260, (-150 - 1000), 500);
        }
        async void SeperatedMenuRetreat73ANDR()
        {
            await mainmenubutton07.TranslateTo(260, (-50 - 1000), 500);

        }
        async void SeperatedMenuRetreat74ANDR()
        {
            await mainmenubutton08.TranslateTo(260, (50 - 1000), 500);
        }
        async void SeperatedMenuRetreat75ANDR()
        {
            await mainmenubutton09.TranslateTo(260, (150 - 1000), 500);
        }
        async void SeperatedMenuRetreat76ANDR()
        {
            await mainmenutextbutton.TranslateTo(0, (-220 - 1000), 500);
        }
        async void SeperatedMenuRetreat77ANDR()
        {
            await leave06button.TranslateTo(350, (220 - 1000), 500);
        }
        async void SeperatedMenuReturn66ANDR()
        {
            await Settingmusicscreen.TranslateTo(0, 0, 500);
        }
        async void SeperatedMenuReturn67ANDR()
        {
            await mainmenubutton01.TranslateTo(-230, -90, 500);
        }
        async void SeperatedMenuReturn68ANDR()
        {
            await mainmenubutton02.TranslateTo(-230, -35, 500);
        }
        async void SeperatedMenuReturn69ANDR()
        {
            await mainmenubutton03.TranslateTo(-230, 35, 500);
        }
        async void SeperatedMenuReturn70ANDR()
        {
            await mainmenubutton04.TranslateTo(-230, 90, 500);
        }
        async void SeperatedMenuReturn71ANDR()
        {
            await mainmenubutton05.TranslateTo(0, 130, 500);
        }
        async void SeperatedMenuReturn72ANDR()
        {
            await mainmenubutton06.TranslateTo(230, -90, 500);
        }
        async void SeperatedMenuReturn73ANDR()
        {
            await mainmenubutton07.TranslateTo(230, -35, 500);
        }
        async void SeperatedMenuReturn74ANDR()
        {
            await mainmenubutton08.TranslateTo(230, 35, 500);
        }
        async void SeperatedMenuReturn75ANDR()
        {
            await mainmenubutton09.TranslateTo(230, 90, 500);
        }
        async void SeperatedMenuReturn76ANDR()
        {
            await mainmenutextbutton.TranslateTo(0, -130, 500);
        }
        async void SeperatedMenuReturn77ANDR()
        {
            await leave06button.TranslateTo(0, -80, 500);
        }
        // end of android menu's 
        // music module
        async void MusicPlayer01(int musicState, int musicSwitch, double volume)
        {
            if (_currentPlayer != null && _currentPlayer.IsPlaying)
            {
                var playmusic00 = _currentPlayer;
                playmusic00.Volume = volume;
            }
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                switch (musicState)
                {
                    case 1:
                        var playmusic01 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("01fzimtheslime.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic01.Volume = volume;
                            _currentPlayer = playmusic01;
                            playmusic01.Play();
                            playmusic01.Loop = true;
                            this.Resources["Music1BTNText"] = "Playing";
                            this.Resources["Music2BTNText"] = "Track 2";
                            this.Resources["Music3BTNText"] = "Track 3";
                            this.Resources["Music4BTNText"] = "Track 4";
                            this.Resources["Music5BTNText"] = "Track 5";
                            this.Resources["Music6BTNText"] = "Track 6";
                            this.Resources["Music7BTNText"] = "Track 7";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.LightGray;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic01 = _currentPlayer;
                            }
                            playmusic01.Stop();
                            playmusic01.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic01 = _currentPlayer;
                            }
                            playmusic01.Volume = volume;
                        }
                        break;
                    case 2:
                        var playmusic02 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("02fzdirtylove.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic02.Volume = volume;
                            _currentPlayer = playmusic02;
                            playmusic02.Play();
                            playmusic02.Loop = true;
                            this.Resources["Music1BTNText"] = "Track 1";
                            this.Resources["Music2BTNText"] = "Playing";
                            this.Resources["Music3BTNText"] = "Track 3";
                            this.Resources["Music4BTNText"] = "Track 4";
                            this.Resources["Music5BTNText"] = "Track 5";
                            this.Resources["Music6BTNText"] = "Track 6";
                            this.Resources["Music7BTNText"] = "Track 7";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.LightGray;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic02 = _currentPlayer;
                            }
                            playmusic02.Stop();
                            playmusic02.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic02 = _currentPlayer;
                            }
                            playmusic02.Volume = volume;
                        }
                        break;
                    case 3:
                        var playmusic03 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("03fz.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic03.Volume = volume;
                            _currentPlayer = playmusic03;
                            playmusic03.Play();
                            playmusic03.Loop = true;
                            this.Resources["Music1BTNText"] = "Track 1";
                            this.Resources["Music2BTNText"] = "Track 2";
                            this.Resources["Music3BTNText"] = "Playing";
                            this.Resources["Music4BTNText"] = "Track 4";
                            this.Resources["Music5BTNText"] = "Track 5";
                            this.Resources["Music6BTNText"] = "Track 6";
                            this.Resources["Music7BTNText"] = "Track 7";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.LightGray;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic03 = _currentPlayer;
                            }
                            playmusic03.Stop();
                            playmusic03.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic03 = _currentPlayer;
                            }
                            playmusic03.Volume = volume;
                        }
                        break;
                    case 4:
                        var playmusic04 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("04fz.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic04.Volume = volume;
                            _currentPlayer = playmusic04;
                            playmusic04.Play();
                            playmusic04.Loop = true;
                            this.Resources["Music1BTNText"] = "Track 1";
                            this.Resources["Music2BTNText"] = "Track 2";
                            this.Resources["Music3BTNText"] = "Track 3";
                            this.Resources["Music4BTNText"] = "Playing";
                            this.Resources["Music5BTNText"] = "Track 5";
                            this.Resources["Music6BTNText"] = "Track 6";
                            this.Resources["Music7BTNText"] = "Track 7";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.LightGray;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic04 = _currentPlayer;
                            }
                            playmusic04.Stop();
                            playmusic04.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic04 = _currentPlayer;
                            }
                            playmusic04.Volume = volume;
                        }
                        break;
                    case 5:
                        var playmusic05 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("05fz.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic05.Volume = volume;
                            _currentPlayer = playmusic05;
                            playmusic05.Play();
                            playmusic05.Loop = true;
                            this.Resources["Music1BTNText"] = "Track 1";
                            this.Resources["Music2BTNText"] = "Track 2";
                            this.Resources["Music3BTNText"] = "Track 3";
                            this.Resources["Music4BTNText"] = "Track 4";
                            this.Resources["Music5BTNText"] = "Playing";
                            this.Resources["Music6BTNText"] = "Track 6";
                            this.Resources["Music7BTNText"] = "Track 7";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.LightGray;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic05 = _currentPlayer;
                            }
                            playmusic05.Stop();
                            playmusic05.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic05 = _currentPlayer;
                            }
                            playmusic05.Volume = volume;
                        }
                        break;
                    case 6:
                        var playmusic06 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("06fz.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic06.Volume = volume;
                            _currentPlayer = playmusic06;
                            playmusic06.Play();
                            playmusic06.Loop = true;
                            this.Resources["Music1BTNText"] = "Track 1";
                            this.Resources["Music2BTNText"] = "Track 2";
                            this.Resources["Music3BTNText"] = "Track 3";
                            this.Resources["Music4BTNText"] = "Track 4";
                            this.Resources["Music5BTNText"] = "Track 5";
                            this.Resources["Music6BTNText"] = "Playing";
                            this.Resources["Music7BTNText"] = "Track 7";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.LightGray;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.DarkSlateGrey;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic06 = _currentPlayer;
                            }
                            playmusic06.Stop();
                            playmusic06.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic06 = _currentPlayer;
                            }
                            playmusic06.Volume = volume;
                        }
                        break;
                    case 7:
                        var playmusic07 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("07fz.mp3"));
                        if (musicSwitch == 1)
                        {
                            playmusic07.Volume = volume;
                            _currentPlayer = playmusic07;
                            playmusic07.Play();
                            playmusic07.Loop = true;
                            this.Resources["Music1BTNText"] = "Track 1";
                            this.Resources["Music2BTNText"] = "Track 2";
                            this.Resources["Music3BTNText"] = "Track 3";
                            this.Resources["Music4BTNText"] = "Track 4";
                            this.Resources["Music5BTNText"] = "Track 5";
                            this.Resources["Music6BTNText"] = "Track 6";
                            this.Resources["Music7BTNText"] = "Playing";
                            this.Resources["ColourOfMusic1BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic2BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic3BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic4BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic5BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic6BTNClicked"] = Colors.DarkSlateGrey;
                            this.Resources["ColourOfMusic7BTNClicked"] = Colors.LightGray;
                        }
                        if (musicSwitch == 0)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic07 = _currentPlayer;
                            }
                            playmusic07.Stop();
                            playmusic07.Dispose();
                        }
                        if (musicSwitch == 3)
                        {
                            if (_currentPlayer != null)
                            {
                                playmusic07 = _currentPlayer;
                            }
                            playmusic07.Volume = volume;
                        }
                        break;
                }
            }
        }
        async void SoundBoard(int sfxState)
        {
            if (_currentSFX != null)
            {
                var playSFX00 = _currentSFX;
                playSFX00.Volume = SFXVolumeN;
            }
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                if (sfxState == 1)
                {
                    var Playenter = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("drumssoundeffect.wav"));
                    _currentSFX = Playenter;
                    Playenter.Volume = SFXVolumeN;
                    Playenter.Play();
                }
                else if (sfxState == 2)
                {
                    var Playexplosion = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("explosionsoundeffect.wav"));
                    _currentSFX = Playexplosion;
                    Playexplosion.Volume = SFXVolumeN;
                    Playexplosion.Play();
                }
                else if (sfxState == 3)
                {
                    var Playfire = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("gunfiresoundeffect.wav"));
                    _currentSFX = Playfire;
                    Playfire.Volume = SFXVolumeN;
                    Playfire.Play();
                }
                else if (sfxState == 4)
                {
                    var Playitem = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("itempickupsound.wav"));
                    _currentSFX = Playitem;
                    Playitem.Volume = SFXVolumeN;
                    Playitem.Play();
                }
                else if (sfxState == 5)
                {
                    var Playdamaged01 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("oof01.mp3"));
                    _currentSFX = Playdamaged01;
                    Playdamaged01.Volume = SFXVolumeN;
                    Playdamaged01.Play();
                }
                else if (sfxState == 6)
                {
                    var Playdamaged02 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("oof02.mp3"));
                    _currentSFX = Playdamaged02;
                    Playdamaged02.Volume = SFXVolumeN;
                    Playdamaged02.Play();
                }
                else if (sfxState == 7)
                {
                    var Playcontact = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("strongpunchsfx.mp3"));
                    _currentSFX = Playcontact;
                    Playcontact.Volume = SFXVolumeN;
                    Playcontact.Play();
                }
                else if (sfxState == 8)
                {
                    var Playbtn = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("clicksfx.wav"));
                    _currentSFX = Playbtn;
                    Playbtn.Volume = SFXVolumeN;
                    Playbtn.Play();
                }
                if (_currentSFX != null)
                {
                    var playSFX00 = _currentSFX;
                    playSFX00.Volume = SFXVolumeN;
                }
            }
        }
        // RNG lootdrops function
        // manages the dropping, chance of, and where items spawn
        private void DropItemRNG(int enemyN)
        {
            int itemtype = 1, locken = 0;
            int RNGDropLoot = RNGmove.Next(1, 1000);
            testnumberT = RNGDropLoot;
            canDrop[dropSwitch] = 0;
            for (int yn = 0; yn < canDrop.Length; yn++)
            {
                if (canDrop[yn] == 0)
                {
                    locken = 2;
                    break;
                }
            }
            if (locken == 0)
            {
                for (int jn = 0; jn < canDrop.Length; jn++)
                {
                    canDrop[jn] = 0;
                }
            }
            if (RNGDropLoot == 500) // 1 / 1000 chance drop / 0.1% chance
            {
                for (int i = 0; i < itemInstance.Length; ++i)
                {
                    if (canDrop[i] == 0)
                    {
                        itemtype = 1;
                        DropItem(i, enemyN, itemtype);
                        canDrop[dropSwitch] = 1;
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
                        canDrop[dropSwitch] = 1;
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
                        itemtype = 8;
                        DropItem(i, enemyN, itemtype);
                        canDrop[dropSwitch] = 1;
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
                        itemtype = 9;
                        DropItem(i, enemyN, itemtype);
                        canDrop[dropSwitch] = 1;
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
            itemInstance[itemN].xposition = enemyInstance[enemyN].xposition;
            itemInstance[itemN].yposition = enemyInstance[enemyN].yposition;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = itemT;
            switch (itemT)
            {
                case 1:
                    this.Resources["ItemImageR01"] = "permaorbimage.png";
                    break;
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
                case 8:
                    this.Resources["ItemImageR01"] = "healthpackimage.png";
                    break;
                case 9:
                    this.Resources["ItemImageR01"] = "attackitem.png";
                    break;
            }
            itemim(itemN);
        }
        async void items()
        {
            imageCollection.Add(item01);
            imageCollection.Add(item02);
            imageCollection.Add(item03);
            imageCollection.Add(item04);
            imageCollection.Add(item05);
            imageCollection.Add(item06);
            imageCollection.Add(item07);
            imageCollection.Add(item08);         
        }
        async void itemim(int itemN)
        {
            await imageCollection[itemN].TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++) 
            {
                await imageCollection[itemN].FadeTo(0.6, 500);
                await imageCollection[itemN].FadeTo(1, 500);
            }
            await imageCollection[itemN].FadeTo(0, 500);
            itemInstance[itemN].xposition = -1000;
            itemInstance[itemN].yposition = 0;
            itemInstance[itemN].xleftposition = itemInstance[itemN].xposition - 25;
            itemInstance[itemN].xrightposition = itemInstance[itemN].xposition + 25;
            itemInstance[itemN].type = 0;
            await imageCollection[itemN].TranslateTo(itemInstance[itemN].xposition, itemInstance[itemN].yposition, 5);
        }    
        async void PlayerWinLevel()
        {
            playermovelock = 1;
            if (CurrentPlayerPositionX > 1)
            {
                while (CurrentPlayerPositionX > 1)
                {
                    CurrentPlayerPositionX += -1;
                    await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                }
            }
            else if (CurrentPlayerPositionX < -1)
            {
                while (CurrentPlayerPositionX < -1)
                {
                    CurrentPlayerPositionX += 1;
                    await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 1);
                }
            }
            while (CurrentPlayerPositionY >= -500)
            {
                CurrentPlayerPositionY += -2;
                await PlayerIMG.TranslateTo(CurrentPlayerPositionX, CurrentPlayerPositionY, 2);
            }
            await PlayerIMG.FadeTo(0, 400);
            hideplayerui02();
            await Task.Delay(100);
            await BackgroundLevel01.FadeTo(0, 500);
            await BackgroundLevel02.FadeTo(0, 500);
            await BackgroundLevel03.FadeTo(0, 500);
            await BackgroundLevel04.FadeTo(0, 500);
            await BackgroundLevel05.FadeTo(0, 500);
            await BackgroundLevel06.FadeTo(0, 500);
            await BackgroundLevel07.FadeTo(0, 500);
            await BackgroundLevel08.FadeTo(0, 500);
            bossactive = 0;
            if (gamelevelflag == 1)
            {
                gamelevelflag = 2;
                PDSA03();
                await OutOfOrderscreen.TranslateTo(0, 0, 5);
                await OutOfOrderscreen.FadeTo(1, 500);
            }
            else if (gamelevelflag == 2)
            {
                gamelevelflag = 3;
                await OutOfOrderscreen.TranslateTo(0, 0, 5);
                await OutOfOrderscreen.FadeTo(1, 500);
            }
            else if (gamelevelflag == 3)
            {
                gamelevelflag = 4;
                await OutOfOrderscreen.TranslateTo(0, 0, 5);
                await OutOfOrderscreen.FadeTo(1, 500);
            }
            gamestatus = 0;
        }
        // enemy ai
        async void enemies()
        {
            enemyCollection.Add(e001);
            enemyCollection.Add(e002);
            enemyCollection.Add(e003);
            enemyCollection.Add(e004);
            enemyCollection.Add(e005);
            enemyCollection.Add(e006);
            enemyCollection.Add(e007);
            enemyCollection.Add(e008);
            enemyCollection.Add(e009);
            enemyCollection.Add(e010);
            enemyCollection.Add(e011);
            enemyCollection.Add(e012);
            enemyCollection.Add(e013);
            enemyCollection.Add(e014);
            enemyCollection.Add(e015);
            enemyCollection.Add(e016);
        }
        async void Enemy_AI_01() // basic enemy ai { will move towards player after certain distance }
        {
            while (gamestatus != 0 && bossactive != 1)
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
            while (gamestatus != 0 && bossactive != 1)// move at constant
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
        async void Boss_AI_01() // ai for the boss 
        {
            int tempx = 0, tempy = 0;
            int actionstate = 0;
            while (gamestatus != 0)
            {
                while (bossactive == 0)
                {
                    bossInstance[0].yposition = -550;
                    bossInstance[0].xposition = 0;
                    await Task.Delay(1000);
                }
                for (int j = 0; j < 60; j++)// will hover in the same spot for a while before doing an action
                {
                    bossInstance[0].xposition = (bossInstance[0].xposition + RNGmove.Next(-1, 2));
                    bossInstance[0].yposition = (bossInstance[0].yposition + RNGmove.Next(-1, 2));
                    bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                    bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                    if (bossInstance[0].xposition >= 445)
                    {
                        bossInstance[0].xposition = 444;
                    }
                    if (bossInstance[0].xposition <= -445)
                    {
                        bossInstance[0].xposition = -444;
                    }// border check
                    bi01split();
                    await Task.Delay(2);
                }// end neutral
                actionstate = RNGmove.Next(100, 200);
                if (actionstate <= 110) // rng action 1 ( 10% )
                {
                    while (bossInstance[0].yposition <= 50 )
                    {
                        if (bossInstance[0].xposition > 3)
                        {
                            bossInstance[0].xposition += -5;
                        }
                        if (bossInstance[0].xposition < 0)
                        {
                            bossInstance[0].xposition += 3;
                        }
                        bossInstance[0].yposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                       }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    //bossInstance[0].xposition = 0;
                    bi01split();
                    await Task.Delay(2);
                    for (int i = 0; i < 400; ++i)
                    {
                        bossInstance[0].xposition = (bossInstance[0].xposition + RNGmove.Next(-6, 8));
                        bossInstance[0].yposition = (bossInstance[0].yposition + RNGmove.Next(-6, 8));
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;

                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    for (int i = 0; i < 80; ++i)
                    {
                        bossInstance[0].xposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;

                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].yposition >= -400 )
                    {
                        bossInstance[0].yposition += -5;
                        bossInstance[0].xposition += -5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check

                        bi01split();
                        await Task.Delay(2);
                    }
                }// 1
                else if (actionstate >= 111 && actionstate <= 130) // rng action 2 ( 20% )
                {
                    while (bossInstance[0].yposition <= 100 )
                    {
                        bossInstance[0].xposition += -5;
                        bossInstance[0].yposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;

                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check

                        bi01split();
                        await Task.Delay(2);
                    }
                    for (int i = 0; i < 40; ++i)
                    {
                        bossInstance[0].yposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;

                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].xposition <= 400 )
                    {
                        bossInstance[0].xposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    for (int i = 0; i < 20; ++i)
                    {
                        bossInstance[0].yposition += -5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].xposition >= -400 )
                    {
                        bossInstance[0].xposition += -5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    for (int i = 0; i < 20; ++i)
                    {
                        bossInstance[0].yposition += -5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].xposition <= 400 )
                    {
                        bossInstance[0].xposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check

                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].yposition >= -400 )
                    {
                        bossInstance[0].yposition += -5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].xposition <= 0 )
                    {
                        bossInstance[0].xposition += 5;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                }// 2
                else if (actionstate >= 131 && actionstate <= 150) // rng action 3 ( 20% )
                {
                    while (bossInstance[0].yposition <= CurrentPlayerPositionY + 35 && bossactive != 0)
                    {
                        bossInstance[0].xposition += 3;
                        bossInstance[0].yposition += 3;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        bossInstance[0].xposition = (bossInstance[0].xposition + RNGmove.Next(-1, 2));
                        bossInstance[0].yposition = (bossInstance[0].yposition + RNGmove.Next(-1, 2));
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    for (int j = 0; j < 70; j++)
                    {
                        bossInstance[0].xposition += -8;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                            j = 69;
                        }// border check
                        bi01split();
                        await Task.Delay(3);
                    }
                    while (bossInstance[0].yposition >= -400)
                    {
                        bossInstance[0].xposition += 3;
                        bossInstance[0].yposition += -3;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    while (bossInstance[0].xposition != 0 )
                    {
                        bossInstance[0].xposition += 3;
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 5)
                        {
                            bossInstance[0].xposition = 0;
                        }
                        if (bossInstance[0].xposition <= -5)
                        {
                            bossInstance[0].xposition = -0;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                }// 3
                else if (actionstate >= 151) // rng action 4 ( 50% )
                {
                    for (int u = 0; u < 200; u++) // charge & return
                    {
                        tempx = bossInstance[0].xposition;
                        tempy = bossInstance[0].yposition;
                        bossInstance[0].xposition = (bossInstance[0].xposition + RNGmove.Next(-1, 2));
                        bossInstance[0].yposition = (bossInstance[0].yposition + RNGmove.Next(2, 8));
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition >= 445)
                        {
                            bossInstance[0].xposition = 444;
                        }
                        if (bossInstance[0].xposition <= -445)
                        {
                            bossInstance[0].xposition = -444;
                        }// border check
                        bi01split();
                        await Task.Delay(2);
                    }
                    await Task.Delay(1000);
                    while (bossInstance[0].yposition > -400 )
                    {
                        bossInstance[0].yposition = (bossInstance[0].yposition - 5);
                        if (bossInstance[0].yposition < -400)
                        {
                            bossInstance[0].yposition = -400;
                        }
                        bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
                        bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
                        if (bossInstance[0].xposition != tempx) {
                            bossInstance[0].xposition = (bossInstance[0].xposition + RNGmove.Next(-1, 2));
                        }
                        bi01split();
                        await Task.Delay(2);
                    }
                }// end actionstate action(s)
            }// while
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
        async void playerdeathanim03()
        {
            await PlayerIMG.FadeTo(0, 500);
        }
        async void playerdeathanim02()
        {
            await PlayerIMG.ScaleTo(1.4, 200);
            await PlayerIMG.ScaleTo(0.6, 300);
        }
        private void PlayerDeathScreenAct()
        {
            PDSA01();
            PDSA02();
            PDSA03();
            PDSA04();
        }
        async void PDSA01()
        {
            await Pdeathscreen01.FadeTo(1, 1000);
        }
        async void PDSA02()
        {
            await Pdeathscreen02.FadeTo(1, 1000);
        }
        async void PDSA03()
        {
            await deathscreenbutton.FadeTo(1, 1000);
        }
        async void PDSA04()
        {
            await deathscreenbutton.TranslateTo(0, 140,2);
        }
        // enemies
        // enemy death ctrl
        private void EnemyDying(int enemyN)
        {
            int RNGSound = RNGmove.Next(1, 200);
            if (RNGSound < 100)
            {
                SoundBoard(6);
            }
            else
            {
                SoundBoard(5);
            }
            DropItemRNG(enemyN); // played at every death
            edeathanim(enemyN);
        }
        async void edeathanim(int enemyN)
        {
            await enemyCollection[enemyN].RotateTo(720, 500);
            enemyInstance[enemyN].xposition += -1000;
            enemyInstance[enemyN].xleftposition += -1000;
            enemyInstance[enemyN].xrightposition += -1000;
            if (enemyInstance[enemyN].xposition < 1500)
            {
                killCounter++;
            }
            await enemyCollection[enemyN].FadeTo(0, 500);
            await enemyCollection[enemyN].ScaleTo(0.6, 500);
        }
        private void bi01death()
        {
            SoundBoard(2);
            b01deathanim01();
            b01deathanim02();
            b01deathanim03();
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
                killcounter3++;
            }
            await b01.TranslateTo(bossInstance[0].xposition, bossInstance[0].yposition, 300);
            bossactive = 10000;
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
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei1Revive(enemyN);
                    }
                    break;
                case 1:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei2Revive(enemyN);
                    }
                    break;
                case 2:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei3Revive(enemyN);
                    }
                    break;
                case 3:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei4Revive(enemyN);
                    }
                    break;
                case 4:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei5Revive(enemyN);
                    }
                    break;
                case 5:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei6Revive(enemyN);
                    }
                    break;
                case 6:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei7Revive(enemyN);
                    }
                    break;
                case 7:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei8Revive(enemyN);
                    }
                    break;
                case 8:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei9Revive(enemyN);
                    }
                    break;
                case 9:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei10Revive(enemyN);
                    }
                    break;
                case 10:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei11Revive(enemyN);
                    }
                    break;
                case 11:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei12Revive(enemyN);
                    }
                    break;
                case 12:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei13Revive(enemyN);
                    }
                    break;
                case 13:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei14Revive(enemyN);
                    }
                    break;
                case 14:
                    if (BackgroundCurrentPositionY < 1000)
                    {
                        ei15Revive(enemyN);
                    }
                    break;
                case 15:
                    if (BackgroundCurrentPositionY < 1000)
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
                    enemyInstance[enemyN].healthpoints = enemytype1hp * 2;
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
                    enemyInstance[enemyN].healthpoints = enemytype1hp * 3;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp * 2;
                }
            }
            else if (difficultysetting == 4)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp * 4;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp * 3;
                }
            }
            else if (difficultysetting == 5)
            {
                if (enemyT == 1)
                {
                    enemyInstance[enemyN].healthpoints = enemytype1hp * 5;
                }
                if (enemyT == 2)
                {
                    eliteEnemyInstance[enemyN].healthpoints = enemytype2hp * 5;
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
            await e001.TranslateTo(enemyInstance[0].xposition, enemyInstance[0].yposition, 4);//ai's 1
            await e002.TranslateTo(enemyInstance[1].xposition, enemyInstance[1].yposition, 4);
            await e003.TranslateTo(enemyInstance[2].xposition, enemyInstance[2].yposition, 4);
            await e004.TranslateTo(enemyInstance[3].xposition, enemyInstance[3].yposition, 4);
            await e005.TranslateTo(enemyInstance[4].xposition, enemyInstance[4].yposition, 4);
            await e006.TranslateTo(enemyInstance[5].xposition, enemyInstance[5].yposition, 4);
            await e007.TranslateTo(enemyInstance[6].xposition, enemyInstance[6].yposition, 4);
            await e008.TranslateTo(enemyInstance[7].xposition, enemyInstance[7].yposition, 4);
            await e009.TranslateTo(enemyInstance[8].xposition, enemyInstance[8].yposition, 4);//ai's 2
            await e010.TranslateTo(enemyInstance[9].xposition, enemyInstance[9].yposition, 4);
            await e011.TranslateTo(enemyInstance[10].xposition, enemyInstance[10].yposition, 4);
            await e012.TranslateTo(enemyInstance[11].xposition, enemyInstance[11].yposition, 4);
            await e013.TranslateTo(enemyInstance[12].xposition, enemyInstance[12].yposition, 4);
            await e014.TranslateTo(enemyInstance[13].xposition, enemyInstance[13].yposition, 4);
            await e015.TranslateTo(enemyInstance[14].xposition, enemyInstance[14].yposition, 4);
            await e016.TranslateTo(enemyInstance[15].xposition, enemyInstance[15].yposition, 4);
            await b01.TranslateTo(bossInstance[0].xposition, bossInstance[0].yposition, 4);
        }
        private void enemy_instance_resetpos01()
        {
            for (int i = 0; i < enemyInstance.Length; i++)
            {
                enemyInstance[i].xposition = -2000;
                enemyInstance[i].xleftposition = enemyInstance[i].xposition - 25;
                enemyInstance[i].xrightposition = enemyInstance[i].xposition + 25;
                enemyInstance[i].yposition = 0;
            }
            bossInstance[0].xposition = -2000;
            bossInstance[0].yposition = -550;
            bossInstance[0].xleftposition = bossInstance[0].xposition - 25;
            bossInstance[0].xrightposition = bossInstance[0].xposition + 25;
            e001startpos();
        }
        // level set ups / --------------------------------- 1 ---------------------------------/
        private void Level_Activate_01()
        {
            enemy_instance_openpos01();
            SoundBoard(1);
        }
        private void Level_Deactivate_01()
        {
            enemy_instance_resetpos01();
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
            BossManage();
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
            }// while 
        }// end of updatermain
        async void PlayerUpdateBars()
        {
            int excel = 0;
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
                if (playerHealthPoints <= 0 && excel == 0)// ends the game when conditions are met
                {
                    excel++;
                    PlayerDeath();
                    if (bossactive == 1)
                    {
                        gamestatus = 1;
                        bossoverstate = 1;
                        bossactive = 0;
                    }
                    else
                    {
                        gamestatus = 0;
                    }
                    await Task.Delay(100);
                    PlayerDeathScreenAct();
                }
            }
        }
        async void BossManage()
        {
            while (gamestatus != 0)
            {
                if (bossInstance[0].healthpoints <= 0)
                {
                    bi01death();
                    PlayerWinLevel();
                    break;
                }
                await Task.Delay(50);
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
                        playerMoveamount += -2;
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
                                delay = 0;
                            }
                        }
                    }
                }
                delay = 0;
                await Task.Delay(50);
            }
        }    
        async void GameUniversalTimer()
        {
            int vestage = 0;
            //await Task.Delay(3000);
            while (gamestatus != 0 && vestage == 0)
            {
                //await Task.Delay(4000);// for testing

                await Task.Delay(9000);
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
                if (BackgroundCurrentPositionY >= 2300 && bossactive == 0 && BackgroundCurrentPositionX == 0)
                {
                    bossactive = 1;
                    await Task.Delay(1500);
                    // enter the boss
                    for (int y = 0; y < 130; y++)
                    {
                        bossInstance[0].yposition += 2;
                        bi01split();
                        await Task.Delay(3);
                    }
                    Boss_AI_01();
                    vestage = 1;
                    ResetAll_EIP();
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
                    for (int i = 0; i < itemInstance.Length; i++)
                    {
                        itemInstance[i].yposition += 2;
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
            while (gamestatus != 0 && playerHealthPoints >= 1 ) // split the update loop to stop crashing
            {
                await Task.Delay(150);// the delay between the player's percieved colliding, (application of damage)
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
                await PlayerHitBoxbotright.TranslateTo(playercollisionbotrightX, CurrentPlayerPositionY + 35, 1);
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
        async void Setting_updater()
        {
            while (true)
            {
                await Task.Delay(20);
                if (settingsVolume == 0)
                {
                    this.Resources["ColourOfSetting1BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings1BTNText"] = " -> Music Volume (On) <- ";
                    this.Resources["ColourOfSettings1Text"] = Colors.Black;
                }
                else if (settingsVolume == 1)
                {
                    this.Resources["ColourOfSetting1BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings1BTNText"] = " -> Music Volume (Off) <- ";
                    this.Resources["ColourOfSettings1Text"] = Colors.White;
                }

                if (settingsVolume2 == 0)
                {
                    this.Resources["ColourOfSetting2BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings2BTNText"] = " -> SFX Volume (On) <- ";
                    this.Resources["ColourOfSettings2Text"] = Colors.Black;
                }
                else if (settingsVolume2 == 1)
                {
                    this.Resources["ColourOfSetting2BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings2BTNText"] = " -> SFX Volume (Off) <- ";
                    this.Resources["ColourOfSettings2Text"] = Colors.White;
                }

                if (settingsEnhancedGamePlay == 0)
                {
                    this.Resources["ColourOfSetting3BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings3BTNText"] = " -> Enhanced Gameplay (On) <- ";
                    this.Resources["ColourOfSettings3Text"] = Colors.Black;

                }
                else if (settingsEnhancedGamePlay == 1)
                {
                    this.Resources["ColourOfSetting3BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings3BTNText"] = " -> Enhanced Gameplay (Off) <- ";
                    this.Resources["ColourOfSettings3Text"] = Colors.White;
                }

                if (settingsItalienVoiceActing == 0)
                {
                    this.Resources["ColourOfSetting4BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings4BTNText"] = " -> Italian Voice Acting (On) <- ";
                    this.Resources["ColourOfSettings4Text"] = Colors.Black;
                }
                else if (settingsItalienVoiceActing == 1)
                {
                    this.Resources["ColourOfSetting4BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings4BTNText"] = " -> Italian Voice Acting (Off) <- ";
                    this.Resources["ColourOfSettings4Text"] = Colors.White;
                }

                if (settingsQuitgame == 0)
                {
                    this.Resources["ColourOfSetting5BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings5BTNText"] = " -> Quit Game <- ";
                    this.Resources["ColourOfSettings5Text"] = Colors.Black;
                }
                else if (settingsQuitgame == 1)
                {
                    this.Resources["ColourOfSetting5BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings5BTNText"] = " -> Quit Game <- ";
                    this.Resources["ColourOfSettings5Text"] = Colors.White;
                }

                if (settingsGameOs == 0)
                {
                    this.Resources["ColourOfSetting6BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings6BTNText"] = " -> Game Osmosis (On) <- ";
                    this.Resources["ColourOfSettings6Text"] = Colors.Black;

                }
                else if (settingsGameOs == 1)
                {
                    this.Resources["ColourOfSetting6BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings6BTNText"] = " -> Game Osmosis (Off) <- ";
                    this.Resources["ColourOfSettings6Text"] = Colors.White;
                }

                if (settingsEnhancedAI == 0)
                {
                    this.Resources["ColourOfSetting7BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings7BTNText"] = " -> Enhanced AI Features (On) <- ";
                    this.Resources["ColourOfSettings7Text"] = Colors.Black;

                }
                else if (settingsEnhancedAI == 1)
                {
                    this.Resources["ColourOfSetting7BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings7BTNText"] = " -> Enhanced AI Features (Off) <- ";
                    this.Resources["ColourOfSettings7Text"] = Colors.White;
                }

                if (settingsGraphics == 0)
                {
                    this.Resources["ColourOfSetting8BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings8BTNText"] = " -> Graphics (On) <- ";
                    this.Resources["ColourOfSettings8Text"] = Colors.Black;

                }
                else if (settingsGraphics == 1)
                {
                    this.Resources["ColourOfSetting8BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings8BTNText"] = " -> Graphics (Off) <- ";
                    this.Resources["ColourOfSettings8Text"] = Colors.White;
                }

                if (settingsSpiderMode == 0)
                {
                    this.Resources["ColourOfSetting9BTNClicked"] = Colors.Ivory;
                    this.Resources["Settings9BTNText"] = " -> Spider-phobia Mode (On) <- ";
                    this.Resources["ColourOfSettings9Text"] = Colors.Black;

                }
                else if (settingsSpiderMode == 1)
                {
                    this.Resources["ColourOfSetting9BTNClicked"] = Colors.DarkSlateGray;
                    this.Resources["Settings9BTNText"] = " -> Spider-phobia Mode (Off) <- ";
                    this.Resources["ColourOfSettings9Text"] = Colors.White;
                }
            }
        }
        async void Update_backgrounds()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await BackgroundLevel01.TranslateTo(0, 0, 1);
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
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
        }
    }// end of main
}// end of all
