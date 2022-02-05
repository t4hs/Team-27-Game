using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharacterSelection : MonoBehaviourPunCallbacks
{
    [SerializeField] private Character[] characters = default;
    [SerializeField] private Text characterName = default;
    private List<GameObject> characterInstances = new List<GameObject>();    
    private int currentCharacter = 0;    
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        foreach (var character in characters)
        {
            GameObject characterInstance = Instantiate(character.CharacterPrefab);

            characterInstance.SetActive(false);

            characterInstances.Add(characterInstance);
        }

        characterInstances[currentCharacter].SetActive(true);
        characterName.text = characters[currentCharacter].CharacterName;
    }

    public void Next()
    {
        characterInstances[currentCharacter].SetActive(false);

        currentCharacter = (currentCharacter + 1)% characterInstances.Count;

        characterInstances[currentCharacter].SetActive(true);
        characterName.text = characters[currentCharacter].CharacterName;
    }

    public void Back()
    {
        characterInstances[currentCharacter].SetActive(false);

        currentCharacter--;
        if(currentCharacter < 0)
        {
            currentCharacter += characterInstances.Count;
        }

        characterInstances[currentCharacter].SetActive(true);
        characterName.text = characters[currentCharacter].CharacterName;
    }

}
