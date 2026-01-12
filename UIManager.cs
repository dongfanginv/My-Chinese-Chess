using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//UI界面的控制管理类
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
    public bool RedTurn=true;
    Image red_turn;
    Image black_turn;
    GameObject button_click;
    GameObject panel_Q;
    GameObject panel_Over;
    public GameObject panel_Warn;
    public GameObject panel_Ban;
    public bool IsGameStart=false;
    public List<Text> textList=new List<Text>();
    public int[] TotalCore={0,1000,0,0,1000,0};
    private void Awake()
    {
        Instance = this;
    }
    //初始化并获取多个UI组件
    void Start()
    {
        if(SceneManager.GetActiveScene().name!="Main")return;
        red_turn=transform.GetChild(3).GetComponent<Image>();
        
        black_turn=transform.GetChild(1).GetComponent<Image>();
        panel_Q=transform.parent.GetChild(1).gameObject;
        panel_Over=transform.parent.GetChild(2).gameObject;
        panel_Warn=transform.parent.GetChild(3).gameObject;
        panel_Ban = transform.parent.GetChild(4).gameObject;
        for (int i = 0; i < 3; i++)
        {
            textList.Add(transform.GetChild(1).GetChild(i*2+1).GetComponent<Text>());
        }
        for (int i = 0; i < 3; i++)
        {
            textList.Add(transform.GetChild(3).GetChild(i * 2 + 1).GetComponent<Text>());
        }

        ChangeTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //转变回合
    public void ChangeTurn()
    {
        if (RedTurn)
        {
            StartCoroutine(ChangeBackColor(red_turn));
        }
        else
        {
            StartCoroutine(ChangeBackColor(black_turn));
        }
    }
    IEnumerator ChangeBackColor(Image img)
    {
        Color color=img.color;
        bool left=true;
        bool old_turn=RedTurn;
        while(RedTurn==old_turn)
        {
            
            if(color.a<0.2f)left = false;
            if(color.a>=1.0f)left=true;
            if(left) 
            color.a = color.a-100 * Time.deltaTime/255f;
            else
            {
                color.a = color.a + 100 * Time.deltaTime / 255f;
            }
            img.color = color;
            yield return null;
        }
        
        color.a = 0;
        img.color = color;
        
    }
    //游戏结束逻辑
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Login");
    }
    //点击开始按钮
    public void OnPlayButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            button_click = transform.GetChild(0).gameObject;
            IsGameStart = true;
            button_click.SetActive(false);
        }
        else
        {
            SceneManager.LoadSceneAsync("Main");
        }
        

    }
    //点击投降按钮
    public void OnGiveUpButtonClick()
    {
        if (RedTurn) return;
        panel_Q.SetActive(true);
        IsGameStart = false;

    }
    public void OnGiveUpButtonClickRed()
    {
        if(!RedTurn) return;
        panel_Q.SetActive(true);
        IsGameStart = false;

    }
   public void OnYesButtonClick()
    {
        
        panel_Over.SetActive(true);
        if (RedTurn)
        {
            panel_Over.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            panel_Over.transform.GetChild(0).gameObject.SetActive(true);
        }
        //重新开始新的游戏
        StartCoroutine(GameOver());     

    }
    public void OnNoButtonClick()
    {
        panel_Q.SetActive(false );
        IsGameStart=true ;
    }
    //退出游戏按钮
    public void OnQuitGame()
    {
        Application.Quit();
    }
    //更新分数
    public void ChangeText(int index,int delta_core)
    {
        TotalCore[index] += delta_core;
        textList[index].text = TotalCore[index] + "";
    }
}
