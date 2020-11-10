using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class CubeEdit : MonoBehaviour
{
    [SerializeField] float gridSize = 4f;
    [SerializeField] float boardSize = 28f;

    TextMesh textMesh;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        textMesh = GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            string labelText = (snapPos.x + boardSize) / gridSize + "," + (snapPos.z + boardSize) / gridSize;
            textMesh.text = (snapPos.x + boardSize)/gridSize + "," + (snapPos.z + boardSize)/gridSize;
            gameObject.name = labelText;
        }

        transform.position = new Vector3(snapPos.x, transform.position.y, snapPos.z);
    }
}
