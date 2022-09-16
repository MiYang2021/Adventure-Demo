using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepPointUI : MonoBehaviour
{
    public Button keepPointButton;
    public Canvas UpgradeCanvas;
    public GameObject UIContro;
    //public bool setUI;
    //private bool setUpgradeUI;
    // Start is called before the first frame update
    void Start()
    {
        //setUpgradeUI = false;
        //setUI = false;
        keepPointButton.onClick.AddListener(keepPointButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.SetActive(setUI);
        //UpgradeCanvas.gameObject.SetActive(setUpgradeUI);
    }

    private void keepPointButtonClick()
    {
        //setUpgradeUI = true;
        
        UpgradeCanvas.gameObject.SetActive(true);
        UIContro.gameObject.GetComponent<Static_UI>().SetKeepPonintUI = false;
    }
}
