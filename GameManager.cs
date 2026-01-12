using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //初始化棋盘
    public List<GridBlock> Blocks = new List<GridBlock>();
    public GameObject BlockPrefab;
    public LayerMask mask;
    string[] chess_name = { "che", "ma", "xiang", "shi", "shuai", "shi", "xiang", "ma", "che" };
    string[] chess_name2 = { "che2", "ma2", "xiang2", "shi2", "jiang", "shi2", "xiang2", "ma2", "che2" };
    int[] chess_core = { 200, 80, 40, 50, 10, 50, 40, 80, 200 };
    public static GameManager instance;
    public bool isChessChecked=false;
    public int index_Checked=-1;
    int Row = 10;int Col = 9;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InisizeBlocks();
        FirstInitiateChess();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //初始化棋格
    public void InisizeBlocks()
    {
        //方格大小2.25，起点：-9.09，-9.96
        for(int i = 0; i < Row; i++)
        {
            for(int j = 0; j < Col; j++)
            {
                GameObject go=Instantiate(BlockPrefab, transform);
                go.transform.localPosition=new Vector3(j*2.25f,i*2.25f,0);
                go.AddComponent<GridBlock>();
                Blocks.Add(go.GetComponent<GridBlock>());
                
            }
        }
    }
    //根据不同的位置添加不同的棋子，棋子类为GridBlock;
    void FirstInitiateChess()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                
                GridBlock gridBlock=Blocks[i*Col+j];
                if (i == 0)
                {
                    gridBlock.sprite_name = chess_name[j];
                    gridBlock.Chess_Core = chess_core[j];
                    gridBlock.isRed = true;
                }
                if (i == 2)
                {
                    if (j == 1 || j == 7)
                        gridBlock.sprite_name="pao";
                    gridBlock.Chess_Core = 100;
                        gridBlock.isRed = true;
                }
                if (i == 3)
                {
                    if (j % 2 == 0)
                       gridBlock.sprite_name="bing";
                    gridBlock.Chess_Core = 10;
                    gridBlock.isRed = true;
                }
                if (i == 6)
                {
                    if (j % 2 == 0)
                        gridBlock.sprite_name = "zu";
                    gridBlock.Chess_Core = 10;
                    gridBlock.isRed = false;
                }
                if (i == 7)
                {
                    if (j == 1 || j == 7)
                        gridBlock.sprite_name = "pao2";
                    gridBlock.Chess_Core = 100;
                    gridBlock.isRed = false;
                }
                if (i == 9)
                {
                    gridBlock.sprite_name = chess_name2[j];
                    gridBlock.Chess_Core = chess_core[j];
                    gridBlock.isRed = false;
                }
                gridBlock.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
        }
    }
}
