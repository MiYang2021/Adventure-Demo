using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Animator chestController;
    [SerializeField] private GameObject playerUnit;
    [SerializeField] private GameObject UI;

    public Canvas ChestUI;
    public bool SetChestUI;
    private Button openChest;


    public Canvas chestNext;
    public int coinNum;
    public int HPnum;
    public int MPnum;

    public bool isOpen;
    private float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        openChest = ChestUI.gameObject.GetComponentInChildren<Button>();
        openChest.onClick.AddListener(openChestOnClick);

    }

    // Update is called once per frame
    void Update()
    {
        if(SetChestUI)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpen = true;
            }
            if(isOpen)
            {
                isOpen = false;
                SetChestUI = false;
                chestController.SetTrigger("Chest Open");
                Invoke("DestroyThis", 1.5f);
                ChestUI.gameObject.SetActive(false);
                Invoke("openNextPage", 1f);
                
            }
        }
    }

    private void openChestOnClick()
    {
        if(SetChestUI)
        {
            isOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SetChestUI = true;
            UI.GetComponent<Static_UI>().elseUI = true;
            ChestUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetChestUI = false;
            UI.GetComponent<Static_UI>().elseUI = false;
            ChestUI.gameObject.SetActive(false);
        }

    }
    private void openNextPage()
    {
        chestNext.gameObject.GetComponent<ChestPageUI>().coinNum = coinNum;
        chestNext.gameObject.GetComponent<ChestPageUI>().hpNum = HPnum;
        chestNext.gameObject.GetComponent<ChestPageUI>().mpNum = MPnum;
        playerUnit.GetComponent<PlayerState>().coin += coinNum;
        playerUnit.GetComponent<PlayerState>().HPmedicine += HPnum;
        playerUnit.GetComponent<PlayerState>().MPmedicine += MPnum;
        chestNext.gameObject.SetActive(true);
    }
    private void DestroyThis()
    {
        isOpen = false;
        Destroy(this.gameObject);
    }
}
