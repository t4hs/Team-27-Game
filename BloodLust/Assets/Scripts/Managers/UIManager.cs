using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIManager : MonoBehaviour
{
    public abstract void ToggleButtons(Button button, bool value);
    public abstract void ToggleScreens(GameObject screen, bool value);

}
