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
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerListing;
    private List<GameObject> characterInstances = new List<GameObject>();    
    private int currentCharacter;
    private Fighter player1, player2;
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

    //Event handler for a character selection
    private void OnChosenCharacter(Character chosenCharacter)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            player1.AssignCharacters(chosenCharacter);
            Debug.LogFormat("{0} has selected {1}", player1.NickName, player1.CharacterName);
            customProps["chosenCharacter"] = currentCharacter;
            player.SetCustomProperties(customProps);
        }else
        {
            player2.AssignCharacters(chosenCharacter);
            Debug.LogFormat("{0} has selected {1}", player2.NickName, player2.CharacterName);
            customProps["chosenCharacter"] = currentCharacter;
            player.SetCustomProperties(customProps);
        }
    }

        // Instanciate all the characters prefabs
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

    //For each frame the client checks if every player owns a character
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
            player1 = playerPrefab.GetComponent<Fighter>();
            player1.AssignPlayers(player.NickName, player.ActorNumber, player.IsLocal);
            playerPrefab.GetComponent<Text>().text = player1.NickName;
            GameObject go = Instantiate(playerPrefab);
            go.transform.SetParent(playerListing.transform,false);
        }else
        {
            player2 = playerPrefab.GetComponent<Fighter>();
            player2.AssignPlayers(player.NickName, player.ActorNumber, player.IsLocal);
            playerPrefab.GetComponent<Text>().text = player2.NickName;
            GameObject go = Instantiate(playerPrefab);
            go.transform.SetParent(playerListing.transform,false);
        }

            foreach (var character in characters)
            {
                GameObject characterInstance = Instantiate(character.CharacterPrefab);
                characterInstance.SetActive(false);
                characterInstances.Add(characterInstance);
            }
            characterInstances[currentCharacter].SetActive(true);
            characterName.text = characters[currentCharacter].CharacterName;
            //ToDo event when both players have chosen a character
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
                currentCharacter = characterInstances.Count -1;
            }
            characterInstances[currentCharacter].SetActive(true);
            characterName.text = characters[currentCharacter].CharacterName;
        }

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

        //Call the OnCharacterSelected callback after the player chosen
        //His character
        public void CharacterSelected()
        {
           OnCharacterSelected(characters[currentCharacter]);
       }

   }
