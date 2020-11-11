using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//Class attached to every pipe to monitor if it is selected
//Controls the color change when selected and deselected
public class PipeClick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Oracle;
    public GameObject gameState;
    private GameObject myOracle = null;
    public int flow = 1;
    public int broken = 1;
    public int order = 1;
    private int selected = 0;
    public Material myMaterial;
    Vector3 spawnOracle;
    void Start()
    {
      gameState = GameObject.Find("Gamestate");
      gameObject.AddComponent<BoxCollider>();
      spawnOracle = transform.position + Vector3.up*5;
/*      spawnOracle.x = transform.position.x - 4;
      spawnOracle.y = transform.position.y - 5;
      if(gameObject.tag == "SidewaysPipe")
      {
          spawnOracle.x = transform.position.x;
          spawnOracle.z = transform.position.z+4;
      }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    //called when the gameObject is clicked on
    //determines if the atttacker of defender is the one who selcted the object
    void OnMouseDown()
    {
        if(gameState.GetComponent<GameVariables>().playerTurn == 1)
        {
            print("defender clicked");
            DefenderClicked();
        }
        if (gameState.GetComponent<GameVariables>().playerTurn == 0)
        {
            print("Attacker clicked");
            AttackerClicked();
        }
    }

    void AttackerClicked()
    {
        //true if object was previously selected
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            selected = 1;
        }
        else
        {
            selected = 0;
        }
        //if the object was previously selected remove if from the list of objects
        //that the game logic(GameVariables.cs) looks at
        if (selected == 1)
        {
            GameVariables.attacksSelected--;
            var changeMaterial = gameObject.GetComponent<Renderer>();
            changeMaterial.material = myMaterial;
            for (int i = 0; i < GameVariables.observedObjects.Count; i++)
            {
                if (GameVariables.observedObjects[i].GetInstanceID() == gameObject.GetInstanceID())
                {
                    GameVariables.observedObjects.RemoveAt(i);
                }
            }
            selected = 0;
        }
        //if it is now selected add it to the list of objects for the game logic to act on
        else if (GameVariables.attacksSelected < 1 && selected == 0 && gameObject.GetComponent<Renderer>().material.color != Color.red)
        {
            selected = 1;
            var changeMaterial = gameObject.GetComponent<Renderer>();
            changeMaterial.material.SetColor("_Color", Color.green);
            GameVariables.attacksSelected++;
            GameVariables.observedObjects.Add(gameObject);
        }
    }

    void DefenderClicked()
    {
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            selected = 1;
        }
        else
        {
            selected = 0;
        }
        //if the object was previously selected remove if from the list of objects
        //that the game logic(GameVariables.cs) looks at
        if (selected == 1)
        {
            Destroy(myOracle);
            GameVariables.oraclesPlaced--;
            var changeMaterial = gameObject.GetComponent<Renderer>();
            changeMaterial.material = myMaterial;
            for (int i = 0; i < GameVariables.observedObjects.Count; i++)
            {
                if (GameVariables.observedObjects[i].GetInstanceID() == gameObject.GetInstanceID())
                {
                    GameVariables.observedObjects.RemoveAt(i);
                }
            }
            if (GameVariables.oraclesPlaced < 2)
            {
                GameObject.Find("ConfirmTurn").SetActive(false);
            }
            selected = 0;
        }
        //if it is now selected add it to the list of objects for the game logic to act on
        else if (GameVariables.oraclesPlaced < 2 && selected == 0)
        {
            selected = 1;
            var changeMaterial = gameObject.GetComponent<Renderer>();
            changeMaterial.material.SetColor("_Color", Color.green);
            myOracle = Instantiate(Oracle, spawnOracle, Quaternion.identity);
            myOracle.transform.parent = transform;
            GameVariables.oraclesPlaced++;
            GameVariables.observedObjects.Add(gameObject);
        }
    }

}
