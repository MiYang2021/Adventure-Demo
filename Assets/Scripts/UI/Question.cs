using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public GameObject UI;
    public Text QuestionText;
    public Text[] AnswerText;
    public Toggle[] AnswerToggle;
    public ToggleGroup AnswerGroup;

    public Button checkAnswer;
    //public List<qItem> questions = new List<qItem>();
    public string[] questions =
    {
        "中国共产党成立于哪一年",
        "黄继光牺牲于哪里",
        "杨靖宇牺牲前在哪里与日军周旋斗争",
        "刘胡兰牺牲于哪场战争",
        "舍身炸碉堡的是谁"
    };
    public string[] answers =
    {
        "1920", "1921", "1922", "1923",
        "八达岭", "大兴安岭", "上甘岭", "生祠岭",
        "东北地区", "东南地区", "中原地区", "西南地区",
        "甲午战争", "抗日战争", "解放战争", "朝鲜战争",
        "邱少云", "黄继光", "李大钊", "董存瑞"
    };
    public int[] rightAnswer = { 2, 3, 1, 3, 4 };

    public bool QuestionChange = false;
    public int rand;
    private void Awake()
    {
        
        rand = Random.Range(0, 4);
        QuestionText.text = questions[rand];
        for(int i = 0; i <  4; i++)
        {
            AnswerText[i].text = answers[rand * 4 + i];
        }
        checkAnswer.onClick.AddListener(checkAnswerClick);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestionChange)
        {
            QuestionChange = false;
            rand = Random.Range(0, 4);
            QuestionText.text = questions[rand];
            for (int i = 0; i < 4; i++)
            {
                AnswerText[i].text = answers[rand * 4 + i];
            }
        }
    }
    
    void checkAnswerClick()
    {
        if(AnswerGroup.gameObject.GetComponent<SetToggle>().FinalNum == rightAnswer[rand])
        {
            AnswerGroup.GetComponent<ToggleGroup>().SetAllTogglesOff();
            UI.GetComponent<Static_UI>().Player.GetComponent<PlayerState>().Recover();
        }
        else
        {
            UI.GetComponent<Static_UI>().SetQuestionUI = false;
            UI.GetComponent<Static_UI>().SetFailUI = true;
        }
    }

    //void createQItem(string ques, string[] answers, int rightAns)
    //{
    //    qItem thisItem = new qItem();
    //    thisItem.theQuestion = ques;
    //    thisItem.theAnswers = answers;
    //    thisItem.rightAnswer = rightAns;
    //}

}
