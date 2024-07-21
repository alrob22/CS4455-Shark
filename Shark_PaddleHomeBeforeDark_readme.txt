Team Name: Shark
Group Members: Kruti Maheshwari, Kareena Narine-Singh, Sydney Oden, Aden Robertson, Madison Schlaff

Start scene: Final

How to play: controls are visible in scene. Press ‘C’ to reveal them after they disappear.

Observable technical requirements: 
- Main character with player input immediately visible as playable character
- AI entities in the form of shark enemies present in various locations throughout the level
    - Patrol, Chase, and Attack states
- Animation via player controls and sharks circling during Patrol and opening their mouth to attack during Attack
- Physics present in interactions with environment, obstacles, collectibles, and sharks
- UI implemented throughout entire game (lose screen happens when player gets attacked by shark or timer runs out, and win screen happens when player reaches the end)
- Sound effects when player paddles
- Background music at starting and ending areas

Known problem areas: 
- skybox won't reset back to light when restarting game from in game, but it works when you re-launch the game
- autoforward W animations will play regardless of having no stamina
- no sfx during autoforward

Team Contributions/Manifest:

Kruti:
Added background music in both starting and ending zones
Added paddling sound effects
Coded SFX handler
Edited paddling sound effects to loop seamlessly
Created trigger-based ending area
Coded ending area to turn off game timer and indicate player win
Created map and terrain
Created water
Painted terrain starting and ending areas
Created 3 controller animations
Organized animation controller
Created animation scripts
Modified player prefab with animated paddle and arms

Kareena:
Added collectable and obstacle prefabs
Textured water

Sydney:
Coded Game Control script, handling the buttons and canvases’ open/close conditions
- Game Control script also includes a timer for the controls and pressing ‘c’ to open the controls again
- Coded controls to disappear after a few seconds
Coded Game Timer script, handling the gameplay timer
Coded Kayak Collision Handler script, handling shark and player collisions
Created/implemented UI canvas (main menu screen and controls)
- Drew and designed main menu screen
- Made working start, info, and quit buttons on main menu screen
Created/implemented How To Play canvas
- Drew how to play screen
- Made working menu and start buttons on the how to play screen
Created/implemented Game Over canvas which opens when you hit a shark or the timer runs out
- Drew the Game Over screen
- Implemented changing text per end condition (shark vs. time)
Created a timer that, when it runs out, opens the Game Over screen
Created the pause menu which is the how to play menu without the start button (open and close with 'esc')

Maddi:
Player Character
- Made player model
- Adjusted player 3rd person camera
- Updating player movement speeds
- Rigging player model
End area
- Made ending area house model
Skybox
- Added skybox textures
- Coded color change script (ColorChange.cs) for skybox to change color as time runs out

Aden:
- Player controls
  - Coded BasicMovement.cs and BasicControl.cs (early version of controls, not used)
  - Coded KayakMovement.cs
  - Coded StrokeData.cs
  - Created KayakControls input system
- Created the in-game controls and stamina UI
- Fully implemented the sharks
  - Implemented shark model and material assets
  - Created Shark animator controller and SharkAttack animation
  - Coded EnemyAI.cs that is the full AI implementation of the sharks, including their states, steering, animations, and necessary collisions
  - Coded SharkAnimation.cs that handles when to animate the shark
  - Baked the NavMesh and implemented the NavMeshAgent attribute on the sharks to handle steering
- Setup player, sharks, and obstacles in the scene
  - Added colliders and rigidbodies for all
  - Placed all throughout the level
  - Helped setup main camera placement and created 2nd camera
- Implemented prefabs
  - Player prefab
  - Shark prefab
  - Updated rock prefabs
- Implemented the ending/win screen
  - Setup collisions so the sharks stay out of the ending area
  - Setup player collisions to note when they enter the ending area
  - Updated KayakCollisionHandler.cs and GameTimer.cs to handle collisions and win screen becoming active
- Finalized the lighting in the scene 
  - Created lightmap assets after baking the lighting

External Assets:
Background music is from https://youtu.be/Q-INtD5KsB8?feature=shared
Paddle sound is from https://freesound.org/people/sinewave1kHz/sounds/208099/
Shark model is from https://www.cgtrader.com/free-3d-models/animals/fish/low-poly-style-shark
Rock models are from https://assetstore.unity.com/packages/3d/props/exterior/low-poly-styled-rocks-43486
Skybox textures are from https://assetstore.unity.com/packages/2d/textures-materials/sky/free-stylized-skybox-212257 
