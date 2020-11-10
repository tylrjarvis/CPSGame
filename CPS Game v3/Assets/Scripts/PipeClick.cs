using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PipeClick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Oracle;
    public GameObject gameState;
    private GameObject myOracle = null;
    public int flow = 1;
    private int selected = 0;
    public Material myMaterial;
    Vector3 spawnOracle;
    void Start()
    {
      gameObject.AddComponent<BoxCollider>();
      spawnOracle = transform.position;
      spawnOracle.x = transform.position.x - 4;
      spawnOracle.y = transform.position.y - 5;
      if(gameObject.tag == "SidewaysPipe")
      {
          spawnOracle.x = transform.position.x;
          spawnOracle.z = transform.position.z+4;
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //called when the gameObject is clicked on
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
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            selected = 1;
        }
        else
        {
            selected = 0;
        }

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
