using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attribute : MonoBehaviour
{
    private GameObject Player;
    public GameObject UI;
    public Text GradeText;
    public Text HPText;
    public Text MPText;
    public Text StrengthText;
    public Text IntelligenceText;
    public Text HPmedicineText;
    public Text MPmedicineText;
    public Text CoinText;
    public Button Close;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Close.onClick.AddListener(CloseOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        GradeText.text = "等级：" + Player.gameObject.GetComponent<PlayerState>().PlayerGrade;
        HPText.text = "HP：" + Player.gameObject.GetComponent<PlayerState>().HP + "/" + +Player.gameObject.GetComponent<PlayerState>().maxHP;
        MPText.text = "MP：" + Player.gameObject.GetComponent<PlayerState>().MP + "/" + +Player.gameObject.GetComponent<PlayerState>().maxMP;
        StrengthText.text = "力量：" + Player.gameObject.GetComponent<PlayerState>().Strength;
        IntelligenceText.text = "智力" + Player.gameObject.GetComponent<PlayerState>().Intelligence;
        HPmedicineText.text = "剩余血瓶：" + Player.gameObject.GetComponent<PlayerState>().HPmedicine;
        MPmedicineText.text = "剩余蓝瓶：" + Player.gameObject.GetComponent<PlayerState>().MPmedicine;
        CoinText.text = "金币数：" + Player.gameObject.GetComponent<PlayerState>().coin;
    }
    void CloseOnClick()
    {
        UI.GetComponent<Static_UI>().SetAttributeUI = false;
    }
}
