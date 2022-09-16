using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem : MonoBehaviour
{
    //public string HeroStory;
    //public Sprite HeroImage;
    public GameObject bag;
    public int ID;
    private Button showButton;

    void Start()
    {
        showButton = this.GetComponent<Button>();
        showButton.onClick.AddListener(StoryCreate);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StoryCreate()
    {
        Debug.Log("ButtonClick");
        bag.GetComponent<Bag_UI>().ShowBegin(this.ID);
        Debug.Log(this.ID);
    }
 
}
