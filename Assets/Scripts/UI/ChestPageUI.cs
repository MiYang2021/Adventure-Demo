using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestPageUI : MonoBehaviour
{
    private Button OK;
    public GameObject UI;

    public Image HeroImage;
    public Text Story;
    public Sprite heroSprite;

    public Text COIN;
    public Text HP;
    public Text MP;
    public int coinNum;
    public int hpNum;
    public int mpNum;

    
    public GameObject bag;
    public Item newItem = new Item();
    // Start is called before the first frame update
    void Start()
    {
        OK = gameObject.GetComponentInChildren<Button>();
        OK.onClick.AddListener(OKonClick);

        //heroStory = Story.gameObject.GetComponent<Text>().text;
    }

    private void OnEnable()
    {
        COIN.text = "+" + coinNum;
        HP.text = "+" + hpNum;
        MP.text = "+" + mpNum;

        bag.GetComponent<Bag_UI>().createItem(heroSprite, Story.text.ToString());
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OKonClick()
    {
        bag.GetComponent<Bag_UI>().insertItem();
        UI.gameObject.GetComponent<Static_UI>().elseUI = false;
        Destroy(this.gameObject);
    }
}
