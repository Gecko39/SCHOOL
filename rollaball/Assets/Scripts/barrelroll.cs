using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, and Z axes by specified amounts, adjusted for frame rate.
        transform.Rotate(new Vector3(30, 40, 50) * Time.deltaTime);
    }
}
