# TPSBase

## Important Note

> This project is my first attempt to make a unity game, while applying all I can techs and techniques in order to make a game in a 21 days sprint with zero prior knowledge of 3d game dev.

> This project is a reference for me when I start develop games Not as a working base, but as a learning experience and to try different practices and methods of implementing games.

## **Intro**

TPSBase is a base for a Third Person Shooter (what a surprise),  Where your aim to survive the most time while being attacked by monsters.

## MVP Aim:
- ### **Charecter**:
    - [x] A charecter that is able to move in a 3D space
    - [x] Proper Third Person Perspective Camera
    - [x] Supporting Unity new input system
    - [x] A suitable Animations and a Humanoid Rig
    - [x] Basic UI for health bar

- ### **Weapon**:
    > my weapon of choice was the bow because it is one of the most tricky weapons to implement.
    - [x] Model attached to player properly
        > I got the model from sketchfab.
    - [x] ability to aim
    - [x] Actually shooting something:
        > currently I have 3 possible way to implement the shooting with
        - Physical game objects
        - Partical system
        - Raycast and mix of both the previous ones
    - [x] Animations for aiming, charging and recoil
    - [x] Shooting sound
    - [x] IK and bone constraints
    - [x] Calculations regarding damage and charging
    - [ ] differant animations with damage increase

- ### **Normal Enemy**:
    - [x] Differant model than the player
        > I got mine from Mixamo
    - [x] Basic UI for health bar
    - [x] Animations for moving and attacking
    - [x] Navigation and path finding
    - [x] Can be shoot with colliders
    - [x] Can perform attacks (2 sets currently) and damage player
    - [x] Properly set attack hitboxes
    - [x] Screen shake for player being hit (if aiming)
    - [x] Shedders for despawn effect
    - [x] Audio source
        > Credits goes to freemusicarchive and zapsplat, no idea whos the author

- ### **Special Enemy**:
    > Same as the normal one but differs in mentioned aspects
    - [x] Moving Animation and Speed
    - [x] Partical effect for the body
    - [x] It self destruct when reaches the player or after a certain amount of time leaving a mist cloud that deals damage to player

- ### **UI**:
    - Timer
    - Kills
    - Health Bar
    - Death Scene and restart button

- ### **Map**:
    Just a part of the default one comes with the HDRP build for unity + some stars and footholds to make it more intersting for the Navigation system using **ProBuilder**

- ### **Gameplay**:
    - Monsters spawn every certain amount of time
    - After (6) Normal Enemies spawns, Special Enemies starts spawning
    - Special Enemies perpose is to close the map slowly if you are not killing enemies and just running.
    - Your goal is to <Bold style="color: MediumSeaGreen;"> **Survive** </Bold> the longest and score the most <span style="color: tomato;"> **Kills** </span>
    > These are managed by a Game Manager script that also utilizes the theme and scene load when restarting the game after death screen

- ### **Testing**:
    To be honest there is **A LOT** that can be tested but I will skip it for this project since I am not willing to build on top of it but to use as a referance.

- ### **Notes For Future Me**:
    - You broke the input system for the mouse after overwriting input class and unity multi crashes, it is so laggy right now, controller works fine tho.
    - You did the Special Enemy Hitbox properly but you left the Normal one with the mesh collider
    - Just like Dark Souls; you can't stop the game lol
    - Next time start with a menu UI or Scene
