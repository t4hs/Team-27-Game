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
    [SerializeField] private GameObject playerPrefab;
    private List<GameObject> characterInstances = new List<GameObject>();    
    private int currentCharacter = 0;
    private List<GameObject> playerPrefabs = new List<GameObject>();
    void Start()
    {

        if(PhotonNetwork.IsConnected)
        {
            foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                Debug.Log("players in the room " + player.Value.NickName);

                if(PhotonNetwork.IsMasterClient)
                {

                    }else
                    {

                    }
                }
            }

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

        private void UpdatePlayers()
        {

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

        public void OnCharacterSelected()
        {

        }

    }
