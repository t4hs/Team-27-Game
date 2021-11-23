using EdgeMultiplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof (EdgeManager))]
public class GameManager : EdgeMultiplayCallbacks
{
    public string playerName;

    public static int id = 0;

    public GameObject textField;

    public GameObject connectButton;

    private Text textDisplay;

    private bool isCreated = false;

    private bool isFull = false;

    // Use this for initialization
    void Start()
    {
        ConnectToEdge();
        if (!isCreated)
        {
            connectButton.GetComponent<Text>().text = "Create a room";
        }
        else
        {
            connectButton.GetComponent<Text>().text = "Join a room";
        }
    }

    // Called once connected to your server deployed on Edge
    public override void OnConnectionToEdge()
    {
        print("Connected to server deployed on Edge");
    }

    // Called once the server registers the player right after the connection is established
    public override void OnRegisterEvent()
    {
        print("Game Session received from server");
    }

    // Called once the JoinRoom request succeeded
    public override void OnRoomJoin(Room room)
    {
        print("Joined room");
        print("Maximum Players in the room :" + room.maxPlayersPerRoom);
        print("Count of Players in the room :" + room.roomMembers.Count);
        if (room.roomMembers.Count == room.maxPlayersPerRoom)
        {
            isCreated = false;
            SceneManager.LoadScene("CharacterScene", LoadSceneMode.Single);
        }
    }

    // Called once the CreateRoom request succeeded
    public override void OnRoomCreated(Room room)
    {
        print("Created a room");
        print("Maximum Players in the room :" + room.maxPlayersPerRoom);
        print("Count of Players in the room :" + room.roomMembers.Count);
        print("waiting for other player to join");
    }

    // Called once the Game start on the server
    // The game starts on the server once the count of room members reachs the maximum players per room
    public override void OnGameStart()
    {
        print("Game Started");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }

    public void PlayerEntry()
    {
        playerName = textField.GetComponent<Text>().text;
        if (NameIsEmpty(playerName))
        {
            Font arial;
            arial =
                (Font) Resources.GetBuiltinResource(typeof (Font), "Arial.ttf");
            GameObject textGo = new GameObject("error message");
            textGo.AddComponent<Text>();
            textDisplay = textGo.GetComponent<Text>();
            textDisplay.font = arial;
            textDisplay.fontSize = 20;
            textDisplay.text = "Please enter an username";
            textGo.GetComponent<Text>().text = textDisplay.text;
        }
        else
        {
            EdgeMultiplay.EdgeManager.JoinOrCreateRoom(playerName, ++id, 2);
            if (!isCreated)
            {
                isCreated = true;
            }
        }
    }

    private bool NameIsEmpty(string name)
    {
        return name.Length == 0;
    }
}
