using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
*/

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
      PhotonNetwork.ConnectUsingSettings(); // Connect to the photon master servers
    }

    public override void OnConnectedToMaster()
    {
      Debug.Log("Successfully connected to the " + PhotonNetwork.CloudRegion + " server.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
