using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToggle : MonoBehaviour
{
    public int FinalNum;
    // Start is called before the first frame update
    void Start()
    {
        FinalNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ListenInFunction(int OnNum)
    {
        FinalNum = OnNum;
        Debug.Log(OnNum);
    }
}
