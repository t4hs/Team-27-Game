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
    [SerializeField] private Button PlayerReadyButton;
    [SerializeField] private Button selectButton;
    public event Action<Character> OnCharacterSelected;
    private Player player;
    private ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();

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
        if(PhotonNetwork.IsMasterClient)
        {
            player1.ChosenCharacter = chosenCharacter;
            player1.CharacterName = chosenCharacter.CharacterName;
            Debug.LogFormat("{0} has selected {1}", player1.NickName, player1.CharacterName);
            customProps["chosenCharacter"] = currentCharacter;
            player.SetCustomProperties(customProps);
        }else
        {
            player2.ChosenCharacter = chosenCharacter;
            player2.CharacterName = chosenCharacter.CharacterName;
            Debug.LogFormat("{0} has selected {1}", player2.NickName, player2.CharacterName);
            customProps["chosenCharacter"] = currentCharacter;
            player.SetCustomProperties(customProps);
        }
    }

        void Start()
        {

            PlayerReadyButton.gameObject.SetActive(false);

            if(PhotonNetwork.IsConnected)
            {
                InstanciatePrefabs();
            }

            PlayerReadyButton.onClick.AddListener(()=>{
                OnPlayerPressed();
                });
        }

        public void OnPlayerPressed()
        {
           if(PhotonNetwork.IsConnected)
           {
            Debug.LogFormat("heading to the chatadcter scene");
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    void Update()
    {
        if(FighterReady() && PhotonNetwork.IsMasterClient)
        {
            PlayerReadyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }
    }

    public void InstanciatePrefabs()
    {
            player = PhotonNetwork.LocalPlayer;

            if(PhotonNetwork.IsMasterClient)
            {
                player1 = player1Prefab.GetComponent<BloodLustPlayer>();
                player1.PlayerId = player.ActorNumber;
                player1.NickName = player.NickName;
                player1Prefab.GetComponent<Text>().text = player1.NickName;
                GameObject go = Instantiate(player1Prefab);
                go.transform.SetParent(playerListing.transform,false);
            }else
            {
                player2 = player2Prefab.GetComponent<BloodLustPlayer>();
                player2.PlayerId = player.ActorNumber;
                player2.NickName = player.NickName;
                player2Prefab.GetComponent<Text>().text = player2.NickName;
                GameObject go = Instantiate(player2Prefab);
                go.transform.SetParent(playerListing.transform,false);
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

        public bool FighterReady()
        {
            bool fighterSelected = true;
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                if(!player.CustomProperties.ContainsKey("chosenCharacter"))
                {
                    fighterSelected = false;
                }
            }

            return fighterSelected;
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


        public void CharacterSelected()
        {
           OnCharacterSelected(characters[currentCharacter]);
       }

   }
