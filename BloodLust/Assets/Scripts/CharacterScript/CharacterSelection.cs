using System;
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
    [SerializeField] private GameObject player1Prefab, player2Prefab;
    [SerializeField] private GameObject playerListing;
    private List<GameObject> characterInstances = new List<GameObject>();    
    private int currentCharacter;
    private BloodLustPlayer player1, player2;

    public event Action<Character> OnCharacterSelected;

    public void Awake()
    {
        currentCharacter = UnityEngine.Random.Range(0,characters.Length);
        OnCharacterSelected+=OnChosenCharacter;
    }


    public void OnDestroy()
    {
        OnCharacterSelected-=OnChosenCharacter;
    }

    private void OnChosenCharacter(Character chosenCharacter)
    {
        if(PhotonNetwork.IsConnected)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                player1.SetCharacter(chosenCharacter);
                Debug.LogFormat("{0} chose {1}", player1.NickName, player1.GetCharacter().CharacterName);
                }else
                {
                    player2.SetCharacter(chosenCharacter);
                    Debug.LogFormat("{0} chose {1}", player2.NickName, player2.GetCharacter().CharacterName);
                }
            }

            //ToDo event when both players have chosen a character
        }

        void Start()
        {

            if(PhotonNetwork.IsConnected)
            {
                InstanciatePrefabs();
            }
        }

        public void InstanciatePrefabs()
        {
            foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                if(PhotonNetwork.IsMasterClient)
                {
                    player1 = player1Prefab.GetComponent<BloodLustPlayer>();
                    player1.NickName = player.Value.NickName;
                    player1.IsLocal = player.Value.IsLocal;
                    player1.PlayerId = player.Value.ActorNumber;
                    player1Prefab.GetComponent<Text>().text = player1.NickName;
                    GameObject Go = Instantiate(player1Prefab);
                    Go.transform.SetParent(GameObject.Find("PlayerListing").transform, false);
                    break;
                    }else
                    {
                        player2 = player2Prefab.GetComponent<BloodLustPlayer>();
                        player2.NickName = player.Value.NickName;
                        player2.IsLocal = player.Value.IsLocal;
                        player2.PlayerId = player.Value.ActorNumber;
                        player2Prefab.GetComponent<Text>().text = player2.NickName;
                        GameObject Go = Instantiate(player2Prefab);
                        Go.transform.SetParent(GameObject.Find("PlayerListing").transform, false);
                        break;
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

            public void CharacterSelected()
            {
             OnCharacterSelected(characters[currentCharacter]);
         }

     }
