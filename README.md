# Muck Seed Finder

## Installation instructions

### Step Zero: Uninstalling incompatible mods

Make sure you uninstall the muck anticheat before you try to install this mod. If you don't know what the muck anticheat is, don't worry about this step.

To uninstall the muck anticheat, go into steam and verify the integrity of game files.

### First Step: Downloading BepInEx

Written instructions: https://docs.bepinex.dev/articles/user_guide/installation/index.html

### Second Step: Downloading the Seed Finder

Go to the releases tab on GitHub, click on the latest release, and download one of the dll files. I think the fast reset dll works fine but if you want to be safe there's a no fast reset dll you can download as well. (Fast reset finds seeds faster as the name suggests)

### Third Step: Putting the dll file inside your plugins folder

Go to your muck folder (it's probably in C:\Program Files (x86)\Steam\steamapps\common\Muck for windows users), and go into BepInEx\plugins. Put the dll inside of this folder.

If you did all 3 steps correctly the muck seed finder should now work. To disable it just remove the BepInEx folder (drag it to your desktop or something).


## How to use the mod

When you start muck it should instantly press the "host lobby" button. If this doesn't happen, check that you installed the seed finder correctly, and also check your internet connection.

On this screen, you can enter a starting seed in the seed text box. (this seed finder starts at a seed and then just increments by positive numbers) If you don't enter anything into the text box it will just start testing from a random seed.

Press start and it should automatically enter and reset worlds.

After a few seconds, there will be a file called muck_seeds.csv that appears on your desktop. It's probably safer to refrain from opening/making changes to the file while the program is running, but I have no idea if it's bad to do so or not. This file is where all the good seeds are stored.

## What's inside muck_seeds.csv?

After you close the seed finder, you can look at what's inside the file. If you open the file with excel or some other spreadsheet program, you will see 3 columns: seed, distance, and ancient bow.

Seed is just the seed. Pretty self-explanatory. All the seeds listed have a chiefs spear inside of the chiefs chest.

Distance is the minimum distance required to travel from spawn -> village -> all 5 guardians -> village -> shipwreck. For some reference, the current seed used in the set seed wr has a distance of 1852.

Ancient bow is whether or not the chiefs chest contains an ancient bow. You will probably mostly see this column filled with "yes" since this seed finder is optimized for finding seeds with both chiefs spears and ancient bows.
