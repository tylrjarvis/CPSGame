using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton; //button used for creating and joining a game.
    [SerializeField]
    private GameObject quickCancelButton; // button used to stop search for game
    [SerializeField]
    private int RoomSize; //manually set number of players in the room at a time.

    //callback function for when first connection is made
    public override void OnConnectedToMaster() 
    {
      PhotonNetwork.AutomaticallySyncScene = true;
      quickStartButton.SetActive(true);
    }

    //paired to quick start button
    public void QuickStart()
    {
      quickStartButton.SetActive(false);
      quickCancelButton.SetActive(true);
      PhotonNetwork.JoinRandomRoom(); //first tries to join an existing room
      Debug.Log("Quick start");
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      Debug.Log("Failed to join a room");
      CreateRoom();
    }

    // create our own room
    void CreateRoom()
    {
      Debug.Log("Creating room now");
      int randomRoomNumber = Random.Range(0, 10000); //create rand name for the room
      RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize};
      PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
      Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
      Debug.Log("Failed to create room. Retrying... ");
      CreateRoom(); 
    }

    public void QuickCancel()
    {
      quickCancelButton.SetActive(false);
      quickStartButton.SetActive(true);
      PhotonNetwork.LeaveRoom();
    }
}
