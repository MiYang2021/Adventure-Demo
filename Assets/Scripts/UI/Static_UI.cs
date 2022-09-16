using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Static_UI : MonoBehaviour
{
    public GameObject Player;

    public Image Attack;//攻击类型显示
    public Text AttackState;
    public Slider HPSlider;//血条
    public Slider MPSlider;
    //public Image Defense;
    public Button Bag;//背包
    public Button Character;//角色属性
    public Text HPmedicine;//血恢复药
    public Text MPmedicine;
    public Text IconNumber;//金币数量

    public Slider MonsterHP;//怪物血条

    public Sprite Close;//近战、远战图标
    public Sprite Far;

    public Canvas KeepPointUI;//存档点UI
    public bool SetKeepPonintUI;//存档点UI显示控制变量
    public Canvas BagUI;//背包
    public bool SetBagUI;
    public Canvas QuestionUI;//复活问题
    public bool SetQuestionUI;
    public Canvas FailUI;//失败
    public bool SetFailUI;
    public Canvas SuccessUI;//成功
    public bool SetSuccessUI;
    public Canvas AttributeUI;//属性
    public bool SetAttributeUI;


    public bool elseUI = false;//是否有其他UI正在显示

    public int MonsterInitHp;//怪物初始HP
    public int MonsterHp;//怪物当前血量
    public bool isMonsterClose;//是否有怪物接近

    // Start is called before the first frame update
    void Start()
    {
        AttackState.text = "Close";
        Attack.sprite = Close;
        HPSlider.maxValue = Player.GetComponent<PlayerState>().maxHP;
        MPSlider.maxValue = Player.GetComponent<PlayerState>().maxMP;
        HPSlider.value = Player.GetComponent<PlayerState>().HP;
        MPSlider.value = Player.GetComponent<PlayerState>().MP;
        MonsterHP.gameObject.SetActive(false);
        SetKeepPonintUI = false;
        Bag.onClick.AddListener(bagOnClick);
        SetBagUI = false;
        SetQuestionUI = false;
        SetFailUI = false;
        SetSuccessUI = false;
        SetAttributeUI = false;
        Character.onClick.AddListener(attributeOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        //静态UI控制
        if (Player.GetComponent<PlayerController>().attack_index == 0)
        {
            AttackState.text = "Close";
            Attack.sprite = Close;
        }
        else
        {
            AttackState.text = "Far";
            Attack.sprite = Far;
        }
        HPSlider.maxValue = Player.GetComponent<PlayerState>().maxHP;
        MPSlider.maxValue = Player.GetComponent<PlayerState>().maxMP;
        HPSlider.value = Player.GetComponent<PlayerState>().HP;
        MPSlider.value = Player.GetComponent<PlayerState>().MP;

        HPmedicine.text = Player.GetComponent<PlayerState>().HPmedicine + "瓶";
        MPmedicine.text = Player.GetComponent<PlayerState>().MPmedicine + "瓶";

        IconNumber.text = Player.GetComponent<PlayerState>().coin + " ";

        //动态UI控制
        if (isMonsterClose)
        {
            MonsterHP.gameObject.SetActive(true);
            MonsterHP.maxValue = MonsterInitHp;
            MonsterHP.value = MonsterHp;
        }
        else
        {
            MonsterHP.gameObject.SetActive(false);
        }
        KeepPointUI.gameObject.SetActive(SetKeepPonintUI);
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetBagUI = !SetBagUI;
        }
        BagUI.gameObject.SetActive(SetBagUI);
        if (SetKeepPonintUI || elseUI || SetBagUI)//判断页面中是否有动态UI正在显示
        {
            Player.GetComponent<PlayerController>().isUI = true;
        }
        else
        {
            Player.GetComponent<PlayerController>().isUI = false;
        }
        SetQuestionUI = Player.GetComponent<PlayerController>().setQuestion;
        QuestionUI.gameObject.SetActive(SetQuestionUI);
        FailUI.gameObject.SetActive(SetFailUI);
        SuccessUI.gameObject.SetActive(SetSuccessUI);
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetAttributeUI = !SetAttributeUI;
        }
        AttributeUI.gameObject.SetActive(SetAttributeUI);
    }
    private void bagOnClick()
    {
        SetBagUI = !SetBagUI;
    }
    private void attributeOnClick()
    {
        SetAttributeUI = !SetAttributeUI;
    }

}
