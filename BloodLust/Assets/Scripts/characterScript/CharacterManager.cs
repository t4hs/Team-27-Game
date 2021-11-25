using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDb;
    public SpriteRenderer artworkSprite;

    //Keeps track of the character that is selected
    private int selectedChar = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(selectedChar);
        
    }

    // Function is called when player presses the 'next' button
    public void Next()
    {
        selectedChar++;

        if(selectedChar >= characterDb.CharacterCount)
        {
            selectedChar = 0;
        }

        UpdateCharacter(selectedChar);
    }

    //Function is called when player presses 'back' option
    public void Back()
    {
        selectedChar--;

        if(selectedChar < 0)
        {
            selectedChar = characterDb.CharacterCount - 1;
        }

        UpdateCharacter(selectedChar);
    }

    //Retrieves info from db and updates character details(artwork)
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDb.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }
}
