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
    [SerializeField] private GameObject playerListing;
    private List<GameObject> characterInstances = new List<GameObject>();    
    private int currentCharacter;
    [SerializeField] private Button PlayerReadyButton;
    [SerializeField] private Button selectButton;
    [SerializeField] CharacterUIManager characterUIManager;
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
    
    // Instantiate all the characters prefabs
        void Start()
        {

        characterUIManager.ToggleButtons(PlayerReadyButton, false);

            if(PhotonNetwork.IsConnected)
            {
                InstantiatePrefabs();
            }

            PlayerReadyButton.onClick.AddListener(()=>{
                OnPlayerPressed();
                });
        }

        public void OnPlayerPressed()
        {
          
            Debug.LogFormat("heading to the chatadcter scene");
            PhotonNetwork.LoadLevel("GameScene");
        }

    //For each frame the client checks if every player owns a character
    void Update()
    {
        if(FighterReady() && PhotonNetwork.IsMasterClient)
        {
          characterUIManager.ToggleButtons(PlayerReadyButton, true);
          characterUIManager.ToggleButtons(selectButton, false);
        }
    }

    //Event handler for a character selection
    private void OnChosenCharacter(Character chosenCharacter)
    {
        PlayerManager.instance.InitCharacters(chosenCharacter, currentCharacter);
    }
    public void InstantiatePrefabs()
    {
        PlayerManager.instance.InitPlayers();
        GameObject playerGO = Instantiate(PlayerManager.instance.playerPrefab);
        playerGO.transform.SetParent(playerListing.transform, false);
            foreach (var character in characters)
            {
                GameObject characterInstance = Instantiate(character.characterPrefab);
                characterInstance.SetActive(false);
                characterInstances.Add(characterInstance);
            }
            characterInstances[currentCharacter].SetActive(true);
            characterName.text = characters[currentCharacter].characterName;
        }

        public void Next()
        {
            characterInstances[currentCharacter].SetActive(false);
            currentCharacter = (currentCharacter + 1)% characterInstances.Count;
            characterInstances[currentCharacter].SetActive(true);
            characterName.text = characters[currentCharacter].characterName;
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
            characterName.text = characters[currentCharacter].characterName;
        }

        public bool FighterReady()
        {
            return PlayerManager.instance.PlayersReady();
        }
        public void CharacterSelected()
        {
           OnCharacterSelected(characters[currentCharacter]);
       }

   }
