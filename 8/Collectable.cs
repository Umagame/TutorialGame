using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string ID;

    void Start()
    {
        
    }

    void Update()
    {
        //Rotation
        this.transform.Rotate(0, 3f, 0);
    }
}
