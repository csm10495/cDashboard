cDashboard
==========

A sticky note overlay for Windows

The current keyboard shortcut to bring up the dash is ~ and LCtrl. Otherwise you can also double_click() the notification area icon to bring it up

All files in cDashboard are under the [MIT License](http://opensource.org/licenses/MIT) unless otherwise noted in the file.

Features
========

__cStickies__

First and foremost, the user can create sticky notes (cStickies) to jot down quick thoughts, lists, and other things. They can be made by clicking File -> New Sticky -> <Color>. Also there exists a keyboard shortcut (Ctrl-T) to create a new sticky of your favorite color, which can be set to anything on the RGB spectrum. Within a cSticky, rich text formatting can be done on the fly using various keyboard shortcuts:
Ctrl + I -> Italics
Ctrl + B -> Bold
Ctrl + U -> Underline
Ctrl + S -> Strikethrough

__cPic__

cPic is an easy way to display a photo on the Dashboard. To create one, go to File -> New cPic... and select the picture file(s) (bmp, gif, jpeg, png.) The user can also customize the view property to be stretch, center, tile, none, etc... When a cPic is made, the file is copied, so it can be deleted from its original location. If multiple files are selected, a random image will be chosen during each fade_in(). The Slideshow Manager can also be used to manage (add, delete) the images shown by the cPic. 

__cStopwatch__

cStopwatch is a quick stopwatch that counts up from zero. It also maintains a record of when it was started.The user may also "lap" the stopwatch by pressing the "L" button. The timer can be paused (stopped, or started) by pressing the "S" button. The "R" button resets the stopwatch back to it's initial state. The start time and any subsequent laps are kept below the total timer in an uneditable textbox.

__cWeather__

cWeather is a weather applet for cDashboard. It currently gets its data from the YQL (Yahoo Query Langauge) API. The user can input any location (zip, city, state, country, city, etc...) cWeather can also try to find your location based on IP address, though this isn't overwhelmingly accurate, it is pretty cool. The applet will automatically refresh data on startup and every hour, though it can be manually refreshed at any time.

Preferences
===========

__cDash Background__

The user has the ability to set the color of the Dashboard. The color, along with all other settings stays throughout resets of the program (and computer.) The user can also select a wallpaper image, along with layout for said image. 

__Default Monitor__

When cDashboard detects that the user has multiple monitors, upon first startup, it will give the user the oportunity to select a default monitor for the dash to show up on.

__Set Opacity__

When the dash is fully faded in, the dash will only be a certain percent opaque. This is that certain percentage. Note that it should not be set as a "."Number but just as Number (no decimal or percent sign). The user can set any value between 15 and 100.

__Set Fade Time__

This option allows the user to set the number of milliseconds for both the fade_in() and fade_out() procedures. 

__Set cWeather Unit__

Allows the user to set the default unit of cWeathers to be either Celcuis or Farenheit.

__Set Favorite Sticky Color__

Allows the user to set their favorite color so that a quick sticky can be made with the Ctrl-T keyboard shotcut.

__Set Favorite Sticky Font__

Allows the user to set their favorite font, so that all future stickies come standard with this font.

__Set UI Text Color__

Allows the user to set the UI Text color. This is the color of the time/date and the menustrip on the top of the main dash form.

__Export/Import cDash data__

Export basically copies the cDashboard folder from %Appdata% to wherever. I use this a lot, when debugging, so I don't lose my actual notes, pictures. Importing copies the folder to Appdata and reloads the application.
