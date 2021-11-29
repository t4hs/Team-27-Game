using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDb;

    public SpriteRenderer artworkSprite;

    private int selectedChar = 0;

    void Start()
    {
        UpdateCharacter(selectedChar);
    }


    public void Next()
    {
        selectedChar++;

        if(selectedChar >= characterDb.CharacterCount)
        {
            selectedChar = 0;
        }

        UpdateCharacter(selectedChar);
    }

    public void Back() 
    {
        selectedChar--;

        if(selectedChar < 0)
        {
            selectedChar = characterDb.CharacterCount -1;
        }

        UpdateCharacter(selectedChar);
    }

    private void UpdateCharacter(int selectedChar)
    {
        Character character = characterDb.GetCharacter(selectedChar);
        artworkSprite.sprite = character.characterSprite;
    }
}
