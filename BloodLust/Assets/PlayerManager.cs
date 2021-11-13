using UnityEngine;
using EdgeMultiplay;
using System.Collections.Generic;
public class PlayerManager : NetworkedPlayer {
 
    // Use this for initialization
    void Start () {
        ListenToMessages();
    }
    
    void Update(){
        if(isLocalPlayer){
            if(Input.GetKeyDown("c")){
                changeColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            }
        }
    }
    
    // Once the GameObject is destroyed
    void OnDestroy () {
        StopListening();
    }
    
    // Called once a GamePlay Event is received from the server
    public override void OnMessageReceived(GamePlayEvent gamePlayEvent){
        print ("GamePlayEvent received from server, event name: " + gamePlayEvent.eventName );
        Debug.Log("eventName:" + gamePlayEvent.eventName);
    }

    public void changeColor(Color c){
        GetComponent<Renderer>().material.color = c;
        if(isLocalPlayer){
            List<float> rgb = new List<float>();
            rgb.AddRange(new List<float>(new float[] {c.r, c.g, c.b}));
            EdgeManager.MessageSender.BroadcastMessage("color", rgb);
        }
    }
 
}
