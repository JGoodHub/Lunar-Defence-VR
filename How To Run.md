# How To Guide

## How To Run

The game is design and built for Google VR and therefore requires an Android phone running OS version 5.0 'Lollipop' or higher.

To install the game onto the phone two methods can be used, the first is to attach the phone to the computer using a USB cable and select the "Build + Run" option under the File menu. After a minute or so the game will begin playing on the phone. The game is installed as an app on the phone so it can be terminated and run again at any point.

The alternate method is to take the pre-built app file "Lunar-Defence.apk" from the "Builds" folder and transfer it directly to the phones internal storage. Once there the apk can be found and run using the Android file manager and manually installed onto the system. However it should be noted that this method is not recommended as it involves installing an uncertified app onto the phone which can be risky or dangerous.

## How To Play

Once the game is on the phone the device should be attached/slotted into to the appropriate Google VR viewer, such as a cardboard or other headset.

There is a brief delay of 15 seconds before the meteors rain down to give you some time to adjust the headset.

The aim of the game is to target the meteors before they reach the habitats. To target a meteor simply look at it until a target reticle appears. Once locked on the turrets will begin automatically firing on the meteor until either it's destroyed or it hits one of the habitats.

Meteors spawn from all directions in a circle around the player so be sure to keep an eye out. If a meteor hits a habitat it damages it, if too many hit then that habitat is destroyed in an explosive fashion. Once all the habitats have been destroyed the game is over.

5 points are awarded for destroying a meteor and a local highscore is kept on the phone so you can challenge your friends. Once the game is over it can be restarted by clicking on the "Restart" button.

## Altering difficulty

Since the game has no inbuilt settings menu yet the difficulty must be adjusted using the script parameters. Specifically the key variable is the "Spawn Interval" located on the "Meteor Manager" script. This dictates how often new meteors spawn, therefore lowering this value make the game more difficult and rasing it make the game easier.

The game does have to be re-installed after making changes to see the effect on the phone "See -> How To Run".
