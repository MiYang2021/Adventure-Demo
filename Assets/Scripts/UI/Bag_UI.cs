using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag_UI : MonoBehaviour
{
    static Bag_UI bag_UI;
    public Button bagButton;
    private int IDcount = 0;
    //public class Item
    //{
    //    public string ItemText;
    //    public Sprite ItemImage;
    //}

    public List<Item> bagItems = new List<Item>();
    public GameObject gridItem;
    public GameObject GridLayout;
    public Sprite scrollImage;

    public Canvas ShowStory;
    private Button ShowEnd;
    private Image backGround;
    public Image ShowPhoto;

    // Start is called before the first frame update
    void Start()
    {
        backGround = ShowStory.GetComponentInChildren<Image>();
        ShowEnd = backGround.GetComponentInChildren<Button>();
        ShowEnd.onClick.AddListener(StoryEnd);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < bagItems.Count; i++)
            {
                Debug.Log(bagItems.Count);
                Debug.Log("第：" + i);
                Debug.Log(bagItems[i].ItemText);
            }
        }
    }
    public void insertItem()
    {
        GameObject grid = Instantiate(gridItem, GridLayout.transform);
        grid.AddComponent<ScrollItem>();
        grid.GetComponent<ScrollItem>().ID = IDcount;
        grid.GetComponent<ScrollItem>().bag = this.gameObject;
        IDcount++;
        //grid.GetComponent<ScrollItem>().HeroImage = thisItem.ItemImage;
        //grid.GetComponent<ScrollItem>().HeroStory = thisItem.ItemText;
    }
    public void ShowBegin(int ID)
    {
        backGround.GetComponentInChildren<Text>().text = bagItems[ID].ItemText;
        ShowPhoto.sprite = bagItems[ID].ItemImage;
        ShowStory.gameObject.SetActive(true);
    }
    private void StoryEnd()
    {
        ShowStory.gameObject.SetActive(false);
        //Destroy(newCanvas);
    }
    public void createItem(Sprite sprite, string str)
    {
        Item thisItem = new Item();
        thisItem.ItemImage = sprite;
        thisItem.ItemText = str;
        bagItems.Add(thisItem);
    }

}
