using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpMove : MonoBehaviour
{
    public float moveLength = 1f;
    public string moveDirection = "up";//"left"
    public float moveSpeed = 1f;

    private int moveCon;
    private float moveLengthNow; 
    // Start is called before the first frame update
    void Start()
    {
        moveCon = 1;
        moveLengthNow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveLengthNow += moveSpeed * moveCon * Time.deltaTime;
        if(Mathf.Abs(moveLengthNow) <= moveLength)
        {
            if(moveDirection == "up")
            {
                this.transform.Translate(0, moveSpeed * moveCon * Time.deltaTime, 0);
            }
            else if(moveDirection == "left")
            {
                this.transform.Translate(0, 0, moveSpeed * moveCon * Time.deltaTime);
            }
        }
        else
        {
            moveCon = -moveCon;
        }

    }

}
