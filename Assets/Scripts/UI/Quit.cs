using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    public Button QuitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        QuitButton.onClick.AddListener(QuitClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void QuitClick()
    {
        Application.Quit();
    }
}
