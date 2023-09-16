# MSU1 Alt Selector
Windows Forms Application for switching pcm tracks for SNES MSU1.

# What is MSU1?
In short: MSU1 makes it possible to play external PCM audio files within SNES games, replacing the sample-based SPCs.

More detailed explanation: https://www.zeldix.net/t1607-msu1-getting-started-guide

# What does this program do?
MSU1 packs sometimes contain alternative versions for the same track, stored in a subfolder.
Instead of having to swap files manually, this application allows you to quickly swap the PCM files with just a few mouse clicks.

You can select the ROM file or MSU file via GUI to load all PCM files inside that folder including all subfolders.
The program saves these configurations as a JSON file and can be loaded in the same way.

# Automatic switching of PCM files
Some MSU1 patches might have the issues, that not all SPC tracks are matched to exactly one MSU1 track.
This is the case for DKC3.
For example, all death jingles are mapped to MSU track ID 52, meaning they all access the same PCM file.
Since DKC3 has one death jingle for each level theme, we can determine the correct death jingle by checking which level theme is currently being played back.
So, if the track ID 11 (Treetop Tumble) is being played back, the death jingle for Treetop Tumble should be used.
I implemented this by checking, if the current PCM file, for the track ID being checked, is locked by another program.
The track IDs that are searched for can be configured on the alt. track by right-clicking on it and opening the edit form.

# Changing speed/volume of PCM files
You can also change the speed and volume of all PCM files of the current MSU1 configuration, by converting them via [MSUPCM++](https://github.com/qwertymodo/msupcmplusplus/releases).
