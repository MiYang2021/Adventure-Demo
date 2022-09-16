using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int directionCon = 1;
    public string moveDirection = "up";//"left"\down\right
    public bool isMove;

    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {

        isMove = false;
        moveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            if(moveDirection == "up")
            {
                this.transform.Translate(0, directionCon * moveSpeed * Time.deltaTime, 0);
            }
            else if(moveDirection == "left")
            {
                this.transform.Translate(directionCon * moveSpeed * Time.deltaTime, 0 , 0);
            }
            Invoke("doDestroy", 3f);
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(50, 0, 100, 100));
        if(GUILayout.Button("move"))
        {
            isMove = true;
        }
        GUILayout.EndArea();
    }
    private void doDestroy()
    {
        Destroy(this.gameObject);
    }
}
