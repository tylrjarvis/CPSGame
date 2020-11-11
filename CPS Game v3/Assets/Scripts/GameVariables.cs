using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public int turns = 2;
    [SerializeField] private float _time = 3f;
    public int playerTurn = 1;
    public static int oraclesPlaced = 0;
    public GameObject canvas;
    public GameObject canvas2;
    public GameObject canvas3;
    private bool waiting = false;
    public static List<GameObject> observedObjects = new List<GameObject>();
    private bool defenderPostTurn = false;
    public Material pipe1Material;
    public Material pipe2Material;

    public static int attacksSelected = 0;
    private bool attackerPostTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        canvas2.SetActive(false);
        canvas3.SetActive(false);
        AttackerTurnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(turns <= 0)
        {
            canvas3.SetActive(true);
        }
        if (oraclesPlaced == 2 && defenderPostTurn == false)
        {
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
        else if (attacksSelected == 1 && attackerPostTurn == false)
        {
            if(!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
        else
        {
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
            }
        }
    }
    public void AttackerTurnStart()
    {
        playerTurn--;
        print("Attacker turn starting...");
        print("Turns left: " + turns);

        canvas.SetActive(false);
        canvas2.SetActive(false);
        defenderPostTurn = false;
        attackerPostTurn = false;
        attacksSelected = 0;
        oraclesPlaced = 0;
        observedObjects.Clear();
        //TODO: Clean up to function call for each type of pipe
        GameObject[] sidewaysPipes;
        GameObject[] forwardPipes;
        GameObject[] bendPipes;
        sidewaysPipes = GameObject.FindGameObjectsWithTag("SidewaysPipe");
        forwardPipes = GameObject.FindGameObjectsWithTag("ForwardPipe");
        bendPipes = GameObject.FindGameObjectsWithTag("BendPipe");
        foreach(GameObject pipe in sidewaysPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //.broken instead
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
            else
            {
                changeMaterial.material = pipe2Material;
            }
        }
        foreach (GameObject pipe in forwardPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
            else
            {
                changeMaterial.material = pipe1Material;
            }
        }
        foreach (GameObject pipe in bendPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
            else
            {
                changeMaterial.material = pipe1Material;
            }
        }

        GameObject[] oracles;
        oracles = GameObject.FindGameObjectsWithTag("Oracle");

        foreach(GameObject destroy in oracles)
        {
            Destroy(destroy);
        }
    }


    private void DefenderTurnStart()
    {
        playerTurn++;
        defenderPostTurn = false;
        attackerPostTurn = false;
        print("Defender turn starting...");
        canvas.SetActive(false);
        canvas2.SetActive(false);
        attacksSelected = 0;
        oraclesPlaced = 0;
        observedObjects.Clear();

        //TODO: Clean up to function call for each type of pipe
        GameObject[] sidewaysPipes;
        GameObject[] forwardPipes;
        GameObject[] bendPipes;
        sidewaysPipes = GameObject.FindGameObjectsWithTag("SidewaysPipe");
        forwardPipes = GameObject.FindGameObjectsWithTag("ForwardPipe");
        bendPipes = GameObject.FindGameObjectsWithTag("BendPipe");
        foreach (GameObject pipe in sidewaysPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //.broken instead
            {
                changeMaterial.material = pipe2Material;
            }
        }
        foreach (GameObject pipe in forwardPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //
            {
                changeMaterial.material = pipe1Material;
            }
        }
        foreach (GameObject pipe in bendPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //
            {
                changeMaterial.material = pipe1Material;
            }
        }
    }

    public void ResetSelection()
    {
        if (playerTurn == 0)
        {
            ResetAttackerTurn();
        }
        if (playerTurn == 1)
        {
            ResetDefenderTurn();
        }
    }

    private void AttackerPost()
    {
        canvas.SetActive(false);
        canvas2.SetActive(true);

        foreach (GameObject tracked in observedObjects)
        {
            var changeMaterial = tracked.GetComponent<Renderer>();
            tracked.GetComponent<PipeClick>().broken = 0;

        }
        //TODO: Clean up to function calls for each type of pipe
        GameObject[] sidewaysPipes;
        GameObject[] forwardPipes;
        GameObject[] bendPipes;
        sidewaysPipes = GameObject.FindGameObjectsWithTag("SidewaysPipe");
        forwardPipes = GameObject.FindGameObjectsWithTag("ForwardPipe");
        bendPipes = GameObject.FindGameObjectsWithTag("BendPipe");
        foreach (GameObject pipe in sidewaysPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //.broken instead
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
        }
        foreach (GameObject pipe in forwardPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
        }
        foreach (GameObject pipe in bendPipes)
        {
            var changeMaterial = pipe.GetComponent<Renderer>();
            if (pipe.GetComponent<PipeClick>().broken == 0) //
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
        }
        //combine all the pipes into 1 list
        var allPipes = forwardPipes.Union(sidewaysPipes).Union(bendPipes);
        //This could be improved if the behavior of the foreach loop can be predicted
        //This nested foreach loop loops through all the pipes and determines which pipes have flow in them
        foreach (GameObject pipe in allPipes)
        {
          //if this pipe is broken then there should be no flow
          if (pipe.GetComponent<PipeClick>().broken == 0)
          {
              pipe.GetComponent<PipeClick>().flow = 0;
              //set all downstream pipes to have no flow
              foreach (GameObject otherPipe in allPipes)
              {
                //determine if the current otherPipe is downstream of this pipe order inceases as you go downstream
                if (pipe.GetComponent<PipeClick>().order < otherPipe.GetComponent<PipeClick>().order)
                {
                    otherPipe.GetComponent<PipeClick>().flow = 0;
                }
              }
          }
        }
    }

    public void DefenderPost()
    {
        defenderPostTurn = true;
        foreach (GameObject tracked in observedObjects)
        {
            var changeMaterial = tracked.GetComponent<Renderer>();
            if(tracked.GetComponent<PipeClick>().broken == 0 || tracked.GetComponent<PipeClick>().flow == 0) //.broken or flow
            {
                changeMaterial.material.SetColor("_Color", Color.red);
            }
            else
            {
                changeMaterial.material.SetColor("_Color", Color.green);
            }
        }
        canvas.SetActive(false);
        Invoke("FixObjects", _time);
    }

    private void FixObjects()
    {
        var fixedPipe = false;
        foreach (GameObject tracked in observedObjects)
        {
            var changeMaterial = tracked.GetComponent<Renderer>();
            if (tracked.GetComponent<PipeClick>().broken == 0)
            {
                //fix the selected pipe
                changeMaterial.material.SetColor("_Color", Color.green);
                tracked.GetComponent<PipeClick>().broken = 1;
                fixedPipe = true;
            }
        }
        if(fixedPipe)
        {
          //gather all the pipes
          GameObject[] sidewaysPipes;
          GameObject[] forwardPipes;
          GameObject[] bendPipes;
          sidewaysPipes = GameObject.FindGameObjectsWithTag("SidewaysPipe");
          forwardPipes = GameObject.FindGameObjectsWithTag("ForwardPipe");
          bendPipes = GameObject.FindGameObjectsWithTag("BendPipe");
          //combine the pipes into 1 list for easy parsing
          var allPipes = forwardPipes.Union(sidewaysPipes).Union(bendPipes);
          //allow flow in all nonbroken pipes to be corrected in the later nested foreach loop
          foreach (GameObject pipe in allPipes)
          {
            //if the pipe is not broken there should be flow
            if (pipe.GetComponent<PipeClick>().broken == 1)
            {
              pipe.GetComponent<PipeClick>().flow = 1;
            }
          }
          //use the nested foreach loop in AttackerPost to determine the new flow
          //in the pipes given that at least 1 pipe was fixed
          foreach (GameObject pipe in allPipes)
          {
            //if this pipe is broken then there should be no flow
            if (pipe.GetComponent<PipeClick>().broken == 0)
            {
                pipe.GetComponent<PipeClick>().flow = 0;
                //set all downstream pipes to have no flow
                foreach (GameObject otherPipe in allPipes)
                {
                  //determine if the current otherPipe is downstream of this pipe order inceases as you go downstream
                  if (pipe.GetComponent<PipeClick>().order < otherPipe.GetComponent<PipeClick>().order)
                  {
                      otherPipe.GetComponent<PipeClick>().flow = 0;
                  }
                }
            }
          }

          //reset the color of the pipes to updated flow measurements
          foreach (GameObject tracked in observedObjects)
          {
            var changeMaterial = tracked.GetComponent<Renderer>();
            if (tracked.GetComponent<PipeClick>().flow == 1)
            {
              changeMaterial.material.SetColor("_Color", Color.green);
            }
          }
        }

        canvas2.SetActive(true);

    }

    public void ResetDefenderTurn()
    {
        foreach (GameObject tracked in observedObjects)
        {
            var changeMaterial = tracked.GetComponent<Renderer>();
            if(tracked.tag == "SidewaysPipe")
            {
                changeMaterial.material = pipe2Material;
            }
            else if (tracked.tag == "ForwardPipe")
            {
                changeMaterial.material = pipe1Material;
            }
            GameObject[] oracles;
            oracles = GameObject.FindGameObjectsWithTag("Oracle");

            foreach(GameObject destroy in oracles)
            {
                Destroy(destroy);
            }
            canvas.SetActive(false);
            oraclesPlaced = 0;
        }
    }
    public void ResetAttackerTurn()
    {
        canvas.SetActive(false);
        foreach (GameObject tracked in observedObjects)
        {
            var changeMaterial = tracked.GetComponent<Renderer>();
            if (tracked.tag == "SidewaysPipe" && tracked.GetComponent<PipeClick>().flow == 1) //.broken
            {
                changeMaterial.material = pipe2Material;
            }
            else if (tracked.tag == "ForwardPipe" && tracked.GetComponent<PipeClick>().flow == 1) //
            {
                changeMaterial.material = pipe1Material;
            }
            attacksSelected = 0;
        }
    }

    public void EnterPostTurn()
    {
        if (playerTurn == 0)
        {
            attackerPostTurn = true;
            canvas.SetActive(false);
            AttackerPost();
        }
        if (playerTurn == 1)
        {
            defenderPostTurn = true;
            canvas.SetActive(false);
            DefenderPost();
        }
    }

    public void EndTurn()
    {
        if (playerTurn == 0)
        {
            DefenderTurnStart();
        }
        else if (playerTurn == 1)
        {
            turns--;
            AttackerTurnStart();

        }
    }

}
