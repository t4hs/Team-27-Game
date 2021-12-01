using UnityEngine;
using EdgeMultiplay;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PlayerManager : NetworkedPlayer {

    // Use this for initialization

    void Start () {
        ListenToMessages();
    }
    
    void Update(){
        
    }
    
    // Once the GameObject is destroyed
    void OnDestroy () {
        StopListening();
    }
    
    // Called once a GamePlay Event is received from the server
    public override void OnMessageReceived(GamePlayEvent gamePlayEvent){
        //print ("GamePlayEvent received from server, event name: " + gamePlayEvent.eventName );    
        if(gamePlayEvent.eventName.Equals("card chosen")){
            //ToDo Game Logic
            // Game Logic code goes here
        }
    }

       
    
}
