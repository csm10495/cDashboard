cDashboard
==========

A sticky note overlay for Windows

The current keyboard shortcut to bring up the dash is ~ and LCtrl. Otherwise you can also double_click() the notification area icon to bring it up

All files in cDashboard are under the MIT License unless otherwise noted in the file.

Features
========

cStickies

First and foremost, the user can create sticky notes (cStickies) to jot down quick thoughts, lists, and other things. They can be made by clicking File -> New Sticky -> <Color>. Also there exists a keyboard shortcut (Ctrl-T) to create a new sticky of your favorite color, which can be set to anything on the RGB spectrum. Within a cSticky, rich text formatting can be done on the fly using various keyboard shortcuts:
Ctrl + I -> Italics
Ctrl + B -> Bold
Ctrl + U -> Underline
Ctrl + S -> Strikethrough

cPic

cPic is an easy way to display a photo on the Dashboard. To create one, go to File -> New cPic... and select the picture file(s) (bmp, gif, jpeg, png.) The user can also customize the view property to be stretch, center, tile, none, etc... When a cPic is made, the file is copied, so it can be deleted from its original location. If multiple files are selected, a random image will be chosen during each fade_in(). The Slideshow Manager can also be used to manage (add, delete) the images shown by the cPic. 

cStopwatch

cStopwatch is a quick stopwatch that counts up from zero. It also maintains a record of when it was started.

Preferences
===========

cDash Background

The user has the ability to set the color of the Dashboard. The color, along with all other settings stays throughout resets of the program (and computer.) The user can also select a wallpaper image, along with layout for said image. 

Default Monitor

When cDashboard detects that the user has multiple monitors, upon first startup, it will give the user the oportunity to select a default monitor for the dash to show up on.

Set Opacity

When the dash is fully faded in, the dash will only be a certain percent opaque. This is that certain percentage. Note that it should not be set as a "."Number but just as Number (no decimal).

Set Fade Time

This option allows the user to set the number of milliseconds for both the fade_in() and fade_out() procedures. It is possible that in the future this may get split into both fadeintime and fadeouttime.

Set Favorite Sticky Color

Allows the user to set their favorite color so that a quick sticky can be made with the Ctrl-T keyboard shotcut.

Set Favorite Sticky Font

Allows the user to set their favorite font, so that all future stickies come standard with this font.

Export/Import cDash data

Export basically copies the cDashboard folder from %Appdata% to wherever. I use this a lot, when debugging, so I don't lose my actual notes, pictures. Importing copies the folder to Appdata and reloads the application.





