using EdgeMultiplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[RequireComponent(typeof (EdgeManager))]
public class GameManager : EdgeMultiplayCallbacks
{
    public string playerName;

    public static int id = 0;

    public GameObject textField;

    public GameObject connectButton;
    
    private Text textDisplay;

    private GameObject canvasGO;



    // Use this for initialization
    void Start()
    {
        ConnectToEdge();
        connectButton.GetComponent<Text>().text = "Go to character screen";
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
    }

    // Called once the CreateRoom request succeeded
    public override void OnRoomCreated(Room room)
    {
        GameObject canvasGO = new GameObject();
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();
        Canvas canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Font arial;
        arial = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        print("Created a room");
        print("Maximum Players in the room :" + room.maxPlayersPerRoom);
        print("Count of Players in the room :" + room.roomMembers.Count);
        print("waiting for other player to join");
        SceneManager.LoadScene("RoomScene", LoadSceneMode.Single);
    }

    // Called once the Game start on the server
    // The game starts on the server once the count of room members reachs the maximum players per room
    public override void OnGameStart()
    {
        print("Game Started");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }

    /**
    *
    * Called once the player has clicked on a button
    */
    public void PlayerEntry()
    {
        playerName = textField.GetComponent<Text>().text;
        if (NameIsEmpty(playerName))
        {
            canvasGO = new GameObject("Canvas");
            canvasGO.AddComponent<Canvas>();
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            Canvas canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Font arial;
            arial =
                (Font) Resources.GetBuiltinResource(typeof (Font), "Arial.ttf");
            GameObject textGo = new GameObject("error message");
            textGo.transform.parent = canvasGO.transform;
            textGo.AddComponent<Text>();
            textDisplay = textGo.GetComponent<Text>();
            textDisplay.font = arial;
            textDisplay.fontSize = 20;
            textDisplay.text = "Please enter an username";
            textDisplay.alignment = TextAnchor.MiddleCenter;
            RectTransform rectTransform = textDisplay.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0,120,0);
            rectTransform.sizeDelta = new Vector2(600,200); 
        }
        else
        {
            SceneManager.LoadScene("CharacterScene", LoadSceneMode.Single);

        }
    }

    private void CharacterSelection(int id){
        //ToDo character selection onclick events
    }

    private bool NameIsEmpty(string name)
    {
        return name.Length == 0;
    }
}
