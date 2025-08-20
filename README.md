" Battle For Azura TLOV " 

By Kyle Killilea, 
Y2 August Project Submission,
G00433001

The app was built using .NET 8.0
The project was built using standard .NET MAUI App. No Blazor,
Using the standard .NET MAUI libraries, ones that have been referred to in lectures/labs,
or mentioned on the project brief, and does not use .NET MAUI Community Toolkit.

The App itself, is a linear, action, 2d shooting game demo.

It features a custom UI i designed primarilly using X,Y positioning on the default grid, 
With all 4 vertical, and horizontal corner grids in use.

> Menu Features, <

Starting with the Title Screen, the game features Multiple Menu options, 
The ' Main menu ' which connects all the menu options, ' New Game menu ', ' Continue Game menu ', 
' Select Mission menu ', ' Permability SuperStore menu ',' Audio menu ',' settings menu '.
Every menu is interactive, and functional.


> Game Features, <

You can select a different game difficulty before the launch of a new game.

Once started, it plays in the style of a Linear 2D Vertical Shooter game.
You control ' The Player ', and can move between the 4 cardinal directions,
using the game buttons, and can fire projectiles upwards towards the enemy.

Enemy units have ' HP ', at 0, they die, and have a chance at dropping an item,
With a chance of dropping, 'Gun ammo', 'Damage Bonus', 'Hp Pack', and the rare ' Perma orb '.

these enemies will move at random, using custom AI's, to try catch the player,
contact with an enemy will rapidly reduce the Player ' HP '.

At the end of the level, there is an ' Area Boss ', which the player will have to kill,
to progress. The boss has it;'s own Custom Ai, and moveset, moving erraticly to catch the
player, and even dodge the player's bullets, hovering just outside of the bullet range when
not moving around the board.

The player uses 1, of 6 gun options at a time, starting with ' gun 1', 
gain the rest to power up, so that you can beat the harder difficulties.

Use the rare 'Perma orbs ' at the Supershop on multiple playthroughs to unlock, and 
upgrade your guns, increase attack damage, or starting ammunition.

When you feel ready, you can then play the ' Brutal Difficulty ', the hardest
test to any player, the ammo is low and the enemies are stronger.

The game loads and saves the player information, using the ' Permament player file ',
At the start of the game, it will load this information, and whenever the player uses the settings
option ' Quit game ', or ingame menu settings option of ' Quit game ', it will then save,
Before closing.

There may also be the individual gamesave files, which will save individual playthrough progress, 
(which would have saved at the end of every level) including the player level statistics,
Keeping a hold of how far the player has gotten between levels, 
(Demo includes only 1 level. )

> Audio Features, <

A custom Audioboard is integrated with the game, sound effects, and music,
which can be set on and off individually in the menu settings.

With music in particular, you can switch between a selection of tracks,
to have playing while playing.

> opening the game, <

To Start playing, all you should have to do, is download and unzip the file, 
(to the main hardrive of the device)

Then, Open it, either through visual studio, or as it's own application.

For Andriod, please flip to landscape mode to play the game as common with most app games of this nature.


