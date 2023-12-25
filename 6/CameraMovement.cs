using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Target;
    public GameObject LeftEdge;
    public GameObject RightEdge;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector3(Target.transform.position.x, this.transform.position.y, this.transform.position.z);

        if (this.transform.position.x <= LeftEdge.transform.position.x)
        {
            this.transform.position = new Vector3(LeftEdge.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x >= RightEdge.transform.position.x)
        {
            this.transform.position = new Vector3(RightEdge.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }
}
