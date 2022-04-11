# CookingMaster
Programming Test

Initial commit with only README and .gitignore files


------------------------------------------------------

Final Commit

Next Steps:
  - Script Revision
    - In the CharacterPickup and ItemBase scripts a number of functions could be combined and others should be separated or at least have more overloaded functions
    - Change pickup behaviors to be coded completely in the items themselves. Currently the behavior is split between the item, the player, and in some cases the  item holder so adding new functionality usually results in code form multiple locations giving conflicting directions
  - UI streamlining
    - Make held item indicators over players always face the camera like it does with the salad ingredient readout
  - Save Data
    - Switch Highscore data entry to be done in the form of JSON files instead of the PlayerPref variables
  - Player Feedback
    - Add visual and audio feedback to better convey what is happening, especially on the cutting boards
  - Held/Pocket Item Switch Delay
    - When a held item is put down the item in the pocket immediatley moves into the hands. This can make cutting boards confusing because the players appears to have both items on the cutting board at the same time. Next step would be to put a delay for when the item in the pocket comes forward in front of the player
