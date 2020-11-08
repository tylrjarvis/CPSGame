using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      print(gameObject.name);

      gameObject.AddComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
      /*
      var colorChanger = gameObject.GetComponent<Renderer>();
      if (Input.GetMouseButtonDown(0))
        colorChanger.material.SetColor("_Color",Color.blue);

      if (Input.GetMouseButtonDown(1))
        colorChanger.material.SetColor("_Color",Color.green);

      if (Input.GetMouseButtonDown(2))
        colorChanger.material.SetColor("_Color",Color.yellow);
        */
    }

    void OnMouseDown()
    {
      var colorChanger = gameObject.GetComponent<Renderer>();
      colorChanger.material.SetColor("_Color",Color.green);
    }
}
