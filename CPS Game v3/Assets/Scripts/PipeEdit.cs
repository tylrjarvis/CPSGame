using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEdit : MonoBehaviour
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

        transform.position = new Vector3(snapPos.x, transform.position.y, snapPos.z);
    }
}
