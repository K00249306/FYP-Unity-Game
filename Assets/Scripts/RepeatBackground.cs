using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Repeats background when starting position x is less than specified amount
        if (transform.position.x < startPos.x - 2276) {
            transform.position = startPos;
        }
    }
}
