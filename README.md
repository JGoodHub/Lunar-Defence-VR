# Lunar Defence VR

## How To Guide

### How To Run

The game is design and built for Google VR and therefore requires an Android phone running OS version 5.0 'Lollipop' or higher.

To install the game onto the phone two methods can be used, the first is to attach the phone to the computer using a USB cable and select the "Build + Run" option under the File menu. After a minute or so the game will begin playing on the phone. The game is installed as an app on the phone so it can be terminated and run again at any point.

The alternate method is to take the pre-built app file "Lunar-Defence.apk" from the "Builds" folder and transfer it directly to the phones internal storage. Once there the apk can be found and run using the Android file manager and manually installed onto the system. However it should be noted that this method is not recommended as it involves installing an uncertified app onto the phone which can be risky or dangerous.

### How To Play

Once the game is on the phone the device should be attached/slotted into to the appropriate Google VR viewer, such as a cardboard or other headset.

To start the game look at the "Start Game" button. There is a brief delay of 5 seconds before the meteors rain down to give you some time to adjust the headset.

The aim of the game is to target the meteors before they reach the habitats. To target a meteor simply look at it until a target reticle appears. Once locked on the turrets will begin automatically firing on the meteor until either it's destroyed or it hits one of the habitats.

Meteors spawn from all directions in a circle around the player so be sure to keep an eye out. If a meteor hits a habitat it damages it, if too many hit then that habitat is destroyed in an explosive fashion. Once all the habitats have been destroyed the game is over.

5 points are awarded for destroying a meteor and a local highscore is kept on the phone so you can challenge your friends. Once the game is over it can be restarted by looking at the "Restart" button.

### Altering difficulty

Since the game has no inbuilt settings menu yet the difficulty must be adjusted using the script parameters. Specifically the key variable is the "Spawn Interval" located on the "Meteor Manager" script. This dictates how often new meteors spawn, therefore lowering this value make the game more difficult and rasing it make the game easier.

The game does have to be re-installed after making changes to see the effect on the phone "See -> How To Run".

## Game Design Document

### Platform

The game will be targeted for the GoogleVR android platform.

### Story

Shortly after setting up the first lunar based colony in an effort to explore worlds unknown an alert is triggered for an impending meteor storm, as everyone panics the player must brave the danger and single handily fend off the pesky rocks from hitting the bases core buildings.

### Player

The user plays as the colonies only turret defence, a simple auto cannon.

### Environment

Game will be based on the surface of the moon, therefore ground will be a grey uneven dusty surface with minor rocks and scenery, e.g. tools and equipment strewed about.

Around the player will be three key structures.

The habitat and a large domed building that houses all the lunar colonists, its the biggest building in the colony and has the highest health.

The science lab is a medium sized building used for scientific research of the moons surface. It's a long sleek building akin to a greenhouse but with more wires, pipes and glowy bits.

Finally the power generation station is the last key structure in the colony, it features several controls boxes with tesla coil appendages and several large solar arrays that cover a large area of the moons surface.

The three structures are placed equally distance from the turret in a circular pattern.

### Gameplay

The player controls the lone turret at the centre of the base and must defence the colonies key structures from destruction.

The main enemy for the game will be a meteor storm, meteor's will spawn at random point on the shell of a hemisphere around the base. Each rock will spawn with either a guaranteed target building or a random ground location inside the colony. The player must shoot down the meteors before they reach the ground and impact the colony.

Each building has a set number of hit points which are reduced when a rock collides with it, the amount reduced is based off of the size of the rock.

Overtime the intensity of the storm increases until the colony is eventually destroyed. The players final score will be a combination of the time survived, rocks destroyed and hit points remaining on the colony.

### Art

The art for the game will be designed and modelled in blender. Low polygon art will be used to reduce strain on the players phone and improve the VR experience.

### Sound and Music

Menu will feature relatively serene music (as the storm alert hasn't triggered yet)

The game music will be quite intense sci fi music as the rocks start to descend from the sky.

### UI and Controls

The games only method of interacting with the UI will be for the player to hover their vision over a certain element, which after a set period of time will trigger that button, switch, etc.

In game the player will have a choice of two firing methods.

Method one is for the player to only look around using the VR headset adn firing is handled by pressing either the mouse or space bar.

Alternatively method two will have the turret fire automatically when the player looks within a certain radius of a meteor. This radius would be proportional to the visual size of the meteor as it approaches.
