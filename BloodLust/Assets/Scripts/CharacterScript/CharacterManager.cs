using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase charDb;

    public Text nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;
    
    //Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    //next option
    public void NextButton()
    {
        selectedOption++;

        if(selectedOption >= charDb.CharacterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
    }

    //back option
    public void BackButton()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = charDb.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
    }

    //updates "selected character" sprite and text 
    private void UpdateCharacter(int selectedOption)
    {
        Character character = charDb.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }
}
