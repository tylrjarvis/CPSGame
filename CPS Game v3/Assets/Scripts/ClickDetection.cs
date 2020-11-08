using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public int clicks = 0;
    void Start()
    {
      print(gameObject.name);
      gameObject.AddComponent<BoxCollider>();
      var colorChanger = gameObject.GetComponent<Renderer>();
      colorChanger.material.SetColor("_Color",Color.blue);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //called when the gameObject is clicked on
    void OnMouseDown()
    {
      if (clicks == 0)
      {
        var colorChanger = gameObject.GetComponent<Renderer>();
        colorChanger.material.SetColor("_Color",Color.green);
        clicks = 1;
      }
      else
      {
        var colorChanger = gameObject.GetComponent<Renderer>();
        colorChanger.material.SetColor("_Color",Color.blue);
        clicks = 0;
      }
    }
}
