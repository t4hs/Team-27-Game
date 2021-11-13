using UnityEngine;
using EdgeMultiplay;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EdgeManager))]
public class GameManager : EdgeMultiplayCallbacks {
 
 
    // Use this for initialization
    void Start () {
        ConnectToEdge();
    }
 
    // Called once connected to your server deployed on Edge
    public override void OnConnectionToEdge(){
        print ("Connected to server deployed on Edge");
    }
 
    // Called once the server registers the player right after the connection is established
    public override void OnRegisterEvent(){
        print ("Game Session received from server");
    }
 
    // Called once the JoinRoom request succeeded 
    public override void OnRoomJoin(Room room){
        print ("Joined room");
        print ("Maximum Players in the room :"+ room.maxPlayersPerRoom); 
        print ("Count of Players in the room :"+ room.roomMembers.Count); 
    }
 
    // Called once the CreateRoom request succeeded 
    public override void OnRoomCreated(Room room){
        print ("Created a room");
        print ("Maximum Players in the room :"+ room.maxPlayersPerRoom); 
        print ("Count of Players in the room :"+ room.roomMembers.Count); 
    }
 
    // Called once the Game start on the server
    // The game starts on the server once the count of room members reachs the maximum players per room
    public override void OnGameStart(){
        print ("Game Started"); 
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }
    
    public void SelectedPlayer(int id){
        EdgeManager.JoinOrCreateRoom("player" + id, id, 2);
    }
}
