using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpGrateTest : MonoBehaviour
{

    // Start is called before the first frame update
    public Button UpGrade;//升级
    public Button StrAdd;//力量强化
    public Button StrSub;
    public Button IntAdd;//智力强化
    public Button IntSub;

    public Button CheckPoint;//确定

    public Text coinText;//剩余金币数
    public Text gradeText;//等级等属性
    public Text HPText;
    public Text MPText;
    public Text pointsText;
    public Text strengthText;
    public Text intelligenceText;
    //public Text hintText;

    private GameObject playerUnit;//玩家
    private GameObject wayPointUnit;//存档点

    private int playerGrade;
    private int playerCoin;
    private int points;

    private int[] upGradeCoin = { 0, 20, 50, 90, 110, 130 };//升级需要的金币
    private int[] upHP = { 0, 3, 5, 6, 7, 9, 10 };//升级可提升的HP
    private int[] upMP = { 0, 1, 2, 3, 4, 5, 6 };

    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player");


        //为页面中的按钮添加监听器
        UpGrade.onClick.AddListener(UpGradeClick);

        CheckPoint.onClick.AddListener(CheckClick);
        StrAdd.onClick.AddListener(StrAddClick);
        StrSub.onClick.AddListener(StrSubClick);
        IntAdd.onClick.AddListener(IntAddClick);
        IntSub.onClick.AddListener(IntSubClick);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerUnit != null)//玩家存在时显示数据
        {
            wayPointUnit = playerUnit.GetComponent<PlayerState>().lastPoint;
            playerGrade = playerUnit.GetComponent<PlayerState>().PlayerGrade;
            playerCoin = playerUnit.GetComponent<PlayerState>().coin;
            points = playerUnit.GetComponent<PlayerState>().Points;
            coinText.text = "剩余金币数：" + playerUnit.GetComponent<PlayerState>().coin.ToString();
            gradeText.text = "Grade:" + playerUnit.GetComponent<PlayerState>().PlayerGrade.ToString();
            pointsText.text = "剩余点数:" +playerUnit.GetComponent<PlayerState>().Points.ToString();
            HPText.text = "HP:" + playerUnit.GetComponent<PlayerState>().maxHP.ToString();
            MPText.text = "MP:" + playerUnit.GetComponent<PlayerState>().maxMP.ToString();
            strengthText.text = playerUnit.GetComponent<PlayerState>().Strength.ToString();
            intelligenceText.text = playerUnit.GetComponent<PlayerState>().Intelligence.ToString();

        }
    }
    public void UpGradeClick()
    {
        if (playerCoin >= upGradeCoin[playerGrade])//金币足够时升级
        {
            playerUnit.GetComponent<PlayerState>().coin -= upGradeCoin[playerGrade];
            playerUnit.GetComponent<PlayerState>().PlayerGrade++;
            playerUnit.GetComponent<PlayerState>().maxHP = upHP[playerGrade];
            playerUnit.GetComponent<PlayerState>().maxMP = upMP[playerGrade];
            playerUnit.GetComponent<PlayerState>().Points ++;
        }
    }
    public void CheckClick()
    {
        this.gameObject.SetActive(false);
    }

    private void StrAddClick()//点数足够时强化
    {
        if(points >= 1)
        {
            playerUnit.GetComponent<PlayerState>().Strength++;
            playerUnit.GetComponent<PlayerState>().Points--;
        }
    }
    private void StrSubClick()
    {
        if(playerUnit.GetComponent<PlayerState>().Strength >= 1)
        {
            playerUnit.GetComponent<PlayerState>().Strength--;
            playerUnit.GetComponent<PlayerState>().Points++;
        }
    }
    private void IntAddClick()
    {
        if (points >= 1)
        {
            playerUnit.GetComponent<PlayerState>().Intelligence++;
            playerUnit.GetComponent<PlayerState>().Points--;
        }
    }
    private void IntSubClick()
    {
        if (playerUnit.GetComponent<PlayerState>().Intelligence >= 1)
        {
            playerUnit.GetComponent<PlayerState>().Intelligence--;
            playerUnit.GetComponent<PlayerState>().Points++;
        }
    }
}
