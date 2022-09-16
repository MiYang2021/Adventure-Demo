using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraTest : MonoBehaviour
{

    public GameObject target;
    public GameObject follow;
    public GameObject newFollow;
    public GameObject newTarget;

    void Start()
    {

    }
    void Update()
    {
        transform.position = follow.transform.position;
        Vector3 lookPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
        transform.LookAt(lookPosition);
        if(Input.GetMouseButton(1))
        {
            transform.position = newFollow.transform.position;
            transform.LookAt(newTarget.transform);
        }
        else
        {
            transform.position = follow.transform.position;
            lookPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
            transform.LookAt(lookPosition);
        }
    }
}