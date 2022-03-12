using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : UIManager
{
    //Toggles whatever buttons of the game
    public override void ToggleButtons(Button button, bool value)
    {
        button.gameObject.SetActive(value);
    }
    //Toggle whatever screens of the game. 
    public override void ToggleScreens(GameObject screen, bool value)
    {
        screen.SetActive(value);
    }
}
