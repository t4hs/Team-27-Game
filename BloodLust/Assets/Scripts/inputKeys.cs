using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inputKeys : MonoBehaviour
{
    public Button button;
    public TMP_InputField nextField;
    TMP_InputField currentField;
    // Start is called before the first frame update
    void Start()
    {
        currentField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentField.isFocused && nextField != null && Input.GetKeyDown(KeyCode.Tab))
        {
            nextField.ActivateInputField();
        }
        if (button.interactable && Input.GetKeyDown(KeyCode.Return))
        {
            button.onClick.Invoke();
        }
    }
}
