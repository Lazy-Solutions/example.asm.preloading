Set the profile to the "Preloading" profile.

In the "Globals" scene you will find a preloader game object, with the "Preloader" script, and in levels doors with "Door_LevelLoader".
These 2 are the main scripts of this example.

The goal of this preload is:
Have a trigger zone to start preloading a scene specified,
Have a door and a trigger, so that when you get close to the door, it activates the scene and opens the door, to showcase how to open a scene on demand.

Preloading is an old way to make a game seamless, which is why older games could have long corridors or elevators to "hide" the loading

I also recommend using a background loading priority low that can be set before preloading.