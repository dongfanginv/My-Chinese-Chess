using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//管理棋子行走规则和逻辑的类
public class PlayManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayManager instance;
    //routr_blocks存放棋子的可以行走的路径
    public List<GridBlock> route_blocks = new List<GridBlock>();
    public GameObject Spotprefab;
    public GameObject ParticleChi;
    public GameObject ParticleJiangjun;
    [Header("是否被将军")]
    bool RedKing;
    bool BlackKing;
    List<GridBlock> new_routeblock;

    float speed = 60*1.125f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //获取选中棋子的可行路径
    public void GetRouteByName(GridBlock gB)
    {
        List<GridBlock> list = GameManager.instance.Blocks;
        int index_row = GameManager.instance.index_Checked / 9;
        int index_col = GameManager.instance.index_Checked %9;
        bool enemy = gB.isRed;
        if (gB.sprite_name == "che" || gB.sprite_name == "che2")
        {
            for (int i = index_row - 1; i >= 0; i--)
            {
                if (list[i * 9 + index_col].type == BlockType.None)
                {
                    route_blocks.Add(list[i * 9 + index_col]);
                }
                else
                {
                    if(list[i * 9 + index_col].isRed!=enemy) route_blocks.Add(list[i * 9 + index_col]);
                    break;
                }
            }
            for (int i = index_row + 1; i < 10; i++)
            {
                if (list[i * 9 + index_col].type == BlockType.None)
                {
                    route_blocks.Add(list[i * 9 + index_col]);
                }
                else
                {
                    if (list[i * 9 + index_col].isRed != enemy) route_blocks.Add(list[i * 9 + index_col]);
                    break;
                }
            }
            for (int j = index_col - 1; j >= 0; j--)
            {
                if (list[index_row * 9 + j].type == BlockType.None)
                {
                    route_blocks.Add(list[index_row * 9 + j]);
                }
                else
                {
                    if (list[index_row * 9 + j].isRed != enemy) route_blocks.Add(list[index_row * 9 + j]);
                    break;
                }
            }
            for (int j = index_col + 1; j < 9; j++)
            {
                if (list[index_row * 9 + j].type == BlockType.None)
                {
                    route_blocks.Add(list[index_row * 9 + j]);
                }
                else
                {
                    if (list[index_row * 9 + j].isRed != enemy) route_blocks.Add(list[index_row * 9 + j]);
                    break;
                }
            }
        }
        else if (gB.sprite_name == "ma" || gB.sprite_name == "ma2")
        {
            
            if (index_row < 8 && index_col < 8 && (list[(index_row + 2) * 9 + index_col + 1].type == BlockType.None|| list[(index_row + 2) * 9 + index_col + 1].isRed!=enemy) && list[(index_row + 1) * 9 + index_col].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 2) * 9 + index_col + 1]);
            }
            if (index_row < 8 && index_col > 0 &&( list[(index_row + 2) * 9 + index_col - 1].type == BlockType.None || list[(index_row + 2) * 9 + index_col - 1].isRed != enemy) && list[(index_row + 1) * 9 + index_col].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 2) * 9 + index_col - 1]);
            }
            if (index_row > 1 && index_col < 8 &&( list[(index_row - 2) * 9 + index_col + 1].type == BlockType.None || list[(index_row - 2) * 9 + index_col + 1].isRed != enemy)&& list[(index_row - 1) * 9 + index_col].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 2) * 9 + index_col + 1]);
            }
            if (index_row > 1 && index_col > 0 &&( list[(index_row - 2) * 9 + index_col - 1].type == BlockType.None || list[(index_row - 2) * 9 + index_col -1].isRed != enemy) && list[(index_row - 1) * 9 + index_col].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 2) * 9 + index_col - 1]);
            }
            if (index_row < 9 && index_col < 7 && (list[(index_row + 1) * 9 + index_col + 2].type == BlockType.None || list[(index_row + 1) * 9 + index_col + 2].isRed != enemy) && list[(index_row) * 9 + index_col + 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 1) * 9 + index_col + 2]);
            }
            if (index_row < 9 && index_col > 1 && (list[(index_row + 1) * 9 + index_col - 2].type == BlockType.None || list[(index_row + 1) * 9 + index_col -2].isRed != enemy) && list[(index_row) * 9 + index_col - 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 1) * 9 + index_col - 2]);
            }
            if (index_row > 0 && index_col < 7 && (list[(index_row - 1) * 9 + index_col + 2].type == BlockType.None || list[(index_row -1) * 9 + index_col + 2].isRed != enemy) && list[(index_row) * 9 + index_col + 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 1) * 9 + index_col + 2]);
            }
            if (index_row > 0 && index_col > 1 && (list[(index_row - 1) * 9 + index_col - 2].type == BlockType.None || list[(index_row - 1) * 9 + index_col - 2].isRed != enemy) && list[(index_row) * 9 + index_col - 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 1) * 9 + index_col - 2]);
            }
        }
        else if (gB.sprite_name == "xiang")
        {
            if (index_row < 3 && index_col < 7 &&( list[(index_row + 2) * 9 + index_col + 2].type == BlockType.None || list[(index_row + 2) * 9 + index_col + 2].isRed != enemy) && list[(index_row + 1) * 9 + index_col + 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 2) * 9 + index_col + 2]);
            }
            if (index_row < 3 && index_col > 1 &&( list[(index_row + 2) * 9 + index_col - 2].type == BlockType.None || list[(index_row + 2) * 9 + index_col -2].isRed != enemy) && list[(index_row + 1) * 9 + index_col - 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 2) * 9 + index_col - 2]);
            }
            if (index_row > 1 && index_col < 7 && (list[(index_row - 2) * 9 + index_col + 2].type == BlockType.None || list[(index_row - 2) * 9 + index_col + 2].isRed != enemy) && list[(index_row - 1) * 9 + index_col + 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 2) * 9 + index_col + 2]);
            }
            if (index_row > 1 && index_col > 1 && (list[(index_row - 2) * 9 + index_col - 2].type == BlockType.None || list[(index_row - 2) * 9 + index_col -2].isRed != enemy) && list[(index_row - 1) * 9 + index_col - 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 2) * 9 + index_col - 2]);
            }
        }
        else if (gB.sprite_name == "xiang2")
        {
            if (index_row < 8 && index_col < 7 && (list[(index_row + 2) * 9 + index_col + 2].type == BlockType.None || list[(index_row + 2) * 9 + index_col + 2].isRed != enemy) && list[(index_row + 1) * 9 + index_col + 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 2) * 9 + index_col + 2]);
            }
            if (index_row < 8 && index_col > 1 && (list[(index_row + 2) * 9 + index_col - 2].type == BlockType.None || list[(index_row + 2) * 9 + index_col - 2].isRed != enemy) && list[(index_row + 1) * 9 + index_col - 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row + 2) * 9 + index_col - 2]);
            }
            if (index_row > 6 && index_col < 7 && (list[(index_row - 2) * 9 + index_col + 2].type == BlockType.None || list[(index_row - 2) * 9 + index_col + 2].isRed != enemy) && list[(index_row - 1) * 9 + index_col + 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 2) * 9 + index_col + 2]);
            }
            if (index_row > 6 && index_col > 1 && (list[(index_row - 2) * 9 + index_col - 2].type == BlockType.None || list[(index_row - 2) * 9 + index_col - 2].isRed != enemy) && list[(index_row - 1) * 9 + index_col - 1].type == BlockType.None)
            {
                route_blocks.Add(list[(index_row - 2) * 9 + index_col - 2]);
            }
        }
        else if (gB.sprite_name == "pao" || gB.sprite_name == "pao2")
        {
            bool isMidChess = false;
            for (int i = index_row - 1; i >= 0; i--)
            {
                if (list[i * 9 + index_col].type == BlockType.None )
                {
                    if(isMidChess)continue;
                    route_blocks.Add(list[i * 9 + index_col]);
                }
                else
                {
                    if (isMidChess)
                    {
                       
                        if (list[i * 9 + index_col].isRed != enemy)
                            route_blocks.Add(list[i * 9 + index_col]);
                        
                        break;
                    }
                    isMidChess = true;
                    continue;
                }
            }
            isMidChess = false;
            for (int i = index_row + 1; i < 10; i++)
            {
                if (list[i * 9 + index_col].type == BlockType.None )
                {
                    if (isMidChess) continue;
                    route_blocks.Add(list[i * 9 + index_col]);
                }
                else
                {
                    if (isMidChess)
                    {
                        
                        if (list[i * 9 + index_col].isRed!=enemy)
                        route_blocks.Add(list[i * 9 + index_col]);
                        
                        break;
                    }
                    isMidChess = true;
                    continue;
                }
            }
            isMidChess = false;
            for (int j = index_col - 1; j >= 0; j--)
            {
                if (list[index_row * 9 + j].type == BlockType.None )
                {
                    if (isMidChess) continue;
                    route_blocks.Add(list[index_row * 9 + j]);
                }
                else
                {
                    if (isMidChess)
                    {
                        if (list[index_row * 9 + j].isRed != enemy)
                            route_blocks.Add(list[index_row * 9 + j]);
                        
                        break;
                    }
                    isMidChess = true;
                    continue;
                }
            }
            isMidChess = false;
            for (int j = index_col + 1; j < 9; j++)
            {
                if (list[index_row * 9 + j].type == BlockType.None)
                {
                    if (isMidChess) continue;
                    route_blocks.Add(list[index_row * 9 + j]);
                }
                else
                {
                    if (isMidChess)
                    {
                        if (list[index_row * 9 + j].isRed != enemy)
                            route_blocks.Add(list[index_row * 9 + j]);

                        break;
                    }
                    isMidChess = true;
                    continue;
                }
            }
        }
        else if (gB.sprite_name == "shi")
        {
            if (index_row ==1 && index_col ==4 )
            {
                if(list[21].type == BlockType.None|| list[21].isRed!=enemy)
                    route_blocks.Add(list[21]);
                if (list[23].type == BlockType.None || list[23].isRed != enemy)
                    route_blocks.Add(list[23]);
                if (list[3].type == BlockType.None || list[3].isRed != enemy)
                    route_blocks.Add(list[3]);
                if (list[5].type == BlockType.None || list[5].isRed != enemy)
                    route_blocks.Add(list[5]);
            }
            else if(list[13].type == BlockType.None || list[13].isRed != enemy)
            {
                route_blocks.Add(list[13]);
            }
        }
        else if (gB.sprite_name == "shi2")
        {
            if (index_row == 8 && index_col == 4)
            {
                if (list[84].type == BlockType.None || list[84].isRed != enemy)
                    route_blocks.Add(list[84]);
                if (list[86].type == BlockType.None || list[86].isRed != enemy)
                    route_blocks.Add(list[86]);
                if (list[66].type == BlockType.None || list[66].isRed != enemy)
                    route_blocks.Add(list[66]);
                if (list[68].type == BlockType.None || list[68].isRed != enemy)
                    route_blocks.Add(list[68]);
            }
            else if (list[76].type == BlockType.None || list[76].isRed != enemy)
            {
                route_blocks.Add(list[76]);
            }
        }
        else if (gB.sprite_name == "shuai")
        {
            if ( index_col < 5 && (list[(index_row ) * 9 + index_col + 1].type == BlockType.None|| list[(index_row) * 9 + index_col + 1].isRed!=enemy))
            {
                route_blocks.Add(list[(index_row ) * 9 + index_col + 1]);
            }
            if ( index_col > 3 && (list[(index_row ) * 9 + index_col - 1].type == BlockType.None || list[(index_row) * 9 + index_col - 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row ) * 9 + index_col - 1]);
            }
            if (index_row > 0  && (list[(index_row - 1) * 9 + index_col ].type == BlockType.None || list[(index_row-1) * 9 + index_col ].isRed != enemy))
            {
                route_blocks.Add(list[(index_row - 1) * 9 + index_col ]);
            }
            if (index_row <2 && (list[(index_row +1) * 9 + index_col ].type == BlockType.None || list[(index_row+1) * 9 + index_col].isRed != enemy))
            {
                route_blocks.Add(list[(index_row +1) * 9 + index_col ]);
            }
        }
        else if (gB.sprite_name == "jiang")
        {
            if (index_col < 5 && (list[(index_row) * 9 + index_col + 1].type == BlockType.None || list[(index_row) * 9 + index_col + 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row) * 9 + index_col + 1]);
            }
            if (index_col > 3 && (list[(index_row) * 9 + index_col - 1].type == BlockType.None || list[(index_row) * 9 + index_col - 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row) * 9 + index_col - 1]);
            }
            if (index_row > 7 && (list[(index_row - 1) * 9 + index_col].type == BlockType.None || list[(index_row - 1) * 9 + index_col].isRed != enemy))
            {
                route_blocks.Add(list[(index_row - 1) * 9 + index_col]);
            }
            if (index_row < 9 && (list[(index_row + 1) * 9 + index_col].type == BlockType.None || list[(index_row + 1) * 9 + index_col].isRed != enemy))
            {
                route_blocks.Add(list[(index_row + 1) * 9 + index_col]);
            }
        }
        else if (gB.sprite_name == "bing")
        {
            if (index_row< 9 && (list[(index_row+1) * 9 + index_col ].type == BlockType.None || list[(index_row+1) * 9 + index_col ].isRed != enemy))
            {
                route_blocks.Add(list[(index_row+1) * 9 + index_col ]);
            }           
            if (index_row > 4 && index_col <8 && (list[(index_row ) * 9 + index_col+1].type == BlockType.None || list[(index_row) * 9 + index_col + 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row ) * 9 + index_col+1]);
            }
            if (index_row > 4 && index_col >0 && (list[(index_row ) * 9 + index_col-1].type == BlockType.None || list[(index_row) * 9 + index_col - 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row ) * 9 + index_col-1]);
            }
        }
        else if (gB.sprite_name == "zu")
        {
            if (index_row >0 && (list[(index_row -1) * 9 + index_col].type == BlockType.None || list[(index_row-1) * 9 + index_col ].isRed != enemy))
            {
                route_blocks.Add(list[(index_row - 1) * 9 + index_col]);
            }
            if (index_row <5 && index_col < 8 && (list[(index_row) * 9 + index_col + 1].type == BlockType.None || list[(index_row) * 9 + index_col + 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row) * 9 + index_col + 1]);
            }
            if (index_row <5 && index_col > 0 && (list[(index_row) * 9 + index_col - 1].type == BlockType.None || list[(index_row) * 9 + index_col - 1].isRed != enemy))
            {
                route_blocks.Add(list[(index_row) * 9 + index_col - 1]);
            }
        }

    }
    //显示可行路径点为绿色圆点
    public void ShowGreenSpot()
    {
        foreach(var route_block in route_blocks)
        {
            Instantiate(Spotprefab, route_block.transform);
        }
    }
    //删除路径绿色标记点
    public void DestroySpot()
    {
        foreach (var route_block in route_blocks)
        {
            Destroy(route_block.transform.GetChild(0).gameObject);
        }
        route_blocks.Clear();
    }
    //进行移动操作
    public void MoveToTarget(GridBlock target)
    {
        foreach( var route_block in route_blocks)
        {
            if (route_block == target)
            {
                
                //Move
                GridBlock start = GameManager.instance.Blocks[GameManager.instance.index_Checked];
                
                    //判断是否送将
                    bool CanContinue = OutKingWillKilled(start, target);

                    if (!CanContinue)
                    {
                        start.ChangeToNormal();
                        GameManager.instance.isChessChecked = false;
                        GameManager.instance.index_Checked = -1;
                        return;
                    }
                
                //改变战斗力摧毁分数
                GetCoreOnMove(start, target);
                StartCoroutine(DoMove(start, target));
               
                int index = UIManager.Instance.RedTurn ? 3 : 0;
                //改变回合数目
                UIManager.Instance.ChangeText(index, 1);
                return;
            }
        }
       

    }
    //通过协程DoMove执行移动动画，并在协程结束后进行将军判断
    IEnumerator DoMove(GridBlock start,GridBlock target)
    {
        bool play_chi=false;
        if (target.type != BlockType.None)play_chi=true;
        Vector3 pos = start.transform.localPosition;
        while (Vector3.Distance(start.transform.localPosition, target.transform.localPosition) > 0.5f)
        {
            start.transform.Translate((target.transform.localPosition - start.transform.localPosition).normalized * speed*Time.deltaTime);
            yield return null;
        }
        start.ChangeToNormal();
        start.transform.localPosition= target.transform.localPosition;
        target.transform.localPosition= pos;
        target.ChangeToNone();
        int target_index=GameManager.instance.Blocks.IndexOf(target);
        
        GameManager.instance.Blocks[GameManager.instance.index_Checked]= target;
        GameManager.instance.Blocks[target_index]=start;
        //插入方法判断是否将军   
        CheckKingWillKilled();
        if (play_chi&&!RedKing&&!BlackKing)
        {
            //播放“吃”特效
            Instantiate(ParticleChi, transform);
        }
        GameManager.instance.isChessChecked = false;
        GameManager.instance.index_Checked = -1;
        //变换turn
        UIManager.Instance.RedTurn = !UIManager.Instance.RedTurn;
        UIManager.Instance.ChangeTurn();



    }
    //计算移动一步的分数变化
    public void GetCoreOnMove(GridBlock start, GridBlock target)
    {
        int battle_count = 0;
        int destroy_count = 0;
        int index = UIManager.Instance.RedTurn ? 1 : 0;
        if (target.type!=BlockType.None)
        {
            destroy_count += target.Chess_Core;
        }
        if(start.sprite_name=="ma"|| start.sprite_name == "ma2")
        {
            if (!start.CoreChanged&& UIManager.Instance.TotalCore[index * 3 + 1] <= 700)
            {
                start.CoreChanged= true;
                start.Chess_Core = 100;
                battle_count = 20;
            }
        }
        if(start.sprite_name == "pao" || start.sprite_name == "pao2")
        {
            if (!start.CoreChanged && UIManager.Instance.TotalCore[index * 3 + 1] <= 700)
            {
                start.CoreChanged = true;
                start.Chess_Core = 80;
                battle_count = -20;
            }
        }
        if (start.sprite_name == "bing")
        {
            if (!start.CoreChanged && GameManager.instance.index_Checked>35)
            {
                start.CoreChanged = true;
                start.Chess_Core = 40;
                battle_count = 30;
            }
        }
        if (start.sprite_name == "zu")
        {
            if (!start.CoreChanged && GameManager.instance.index_Checked <54)
            {
                start.CoreChanged = true;
                start.Chess_Core = 40;
                battle_count = 30;
            }
        }
        UIManager.Instance.ChangeText(index * 3 + 1, battle_count);
        UIManager.Instance.ChangeText(Mathf.Abs(index-1) * 3 + 1, -destroy_count);
        UIManager.Instance.ChangeText(index * 3 + 2, destroy_count);

    }
    //判断是否触发了将军
    public void CheckKingWillKilled()
    {
        //获取所有伤害点
        foreach(GridBlock gB in GameManager.instance.Blocks)
        {
            if (UIManager.Instance.RedTurn)
            {
                if (gB.sprite_name == "che" || gB.sprite_name == "ma" || gB.sprite_name == "pao" || gB.sprite_name == "bing")
                {
                    GameManager.instance.index_Checked = GameManager.instance.Blocks.IndexOf(gB);
                    GetRouteByName(gB);
                    
                }
            }
            else if(!UIManager.Instance.RedTurn)
            {
                if (gB.sprite_name == "che2" || gB.sprite_name == "ma2" || gB.sprite_name == "pao2" || gB.sprite_name == "zu")
                {
                    GameManager.instance.index_Checked = GameManager.instance.Blocks.IndexOf(gB);
                    GetRouteByName(gB);
                    
                }
            }        
        }
        foreach (var route_block in route_blocks)
        {
            //将军
            
            if (UIManager.Instance.RedTurn && route_block.sprite_name == "jiang")
            {
                //播放“将军”特效
                
                Instantiate(ParticleJiangjun, transform);
                BlackKing=true;
                break;
            }else if(!UIManager.Instance.RedTurn && route_block.sprite_name == "shuai")
            {
                //播放“将军”特效
                Instantiate(ParticleJiangjun, transform);
                RedKing=true;
                break;
            }
            
        }

        route_blocks.Clear();

    }
    //判断是否能够解将
    public bool OutKingWillKilled(GridBlock start, GridBlock target)
    {
        int origin_index = GameManager.instance.index_Checked;
        List<GridBlock> new_blocks=new List<GridBlock>(GameManager.instance.Blocks);
        int target_index = GameManager.instance.Blocks.IndexOf(target);
        GameManager.instance.Blocks[GameManager.instance.index_Checked] = target;
        GameManager.instance.Blocks[target_index] = start;
        string origin_name = target.sprite_name;
        target.sprite_name=null;
        new_routeblock=new List<GridBlock>(route_blocks);
        route_blocks.Clear();
        foreach (GridBlock gB in new_blocks)
        {
            if (!UIManager.Instance.RedTurn)
            {
                if (gB.sprite_name == "che" || gB.sprite_name == "ma" || gB.sprite_name == "pao" || gB.sprite_name == "bing")
                {
                    GameManager.instance.index_Checked = GameManager.instance.Blocks.IndexOf(gB);
                    GetRouteByName(gB);
                    
                }
            }
            else if (UIManager.Instance.RedTurn)
            {
                if (gB.sprite_name == "che2" || gB.sprite_name == "ma2" || gB.sprite_name == "pao2" || gB.sprite_name == "zu")
                {
                    GameManager.instance.index_Checked = GameManager.instance.Blocks.IndexOf(gB);
                    GetRouteByName(gB);
                }
            }
        }
        foreach (var route_block in route_blocks)
        {
            //将军

            if (!UIManager.Instance.RedTurn && route_block.sprite_name == "jiang")
            {
                //解将失败
                if (BlackKing)
                {
                    StartCoroutine(WarnForMove());
                }
                else
                {
                    StartCoroutine(BanForMove());
                }
                
                route_blocks = new List<GridBlock>(new_routeblock);
                target.sprite_name=origin_name;
                GameManager.instance.Blocks = new List<GridBlock>(new_blocks);
                return false;
            }
            else if (UIManager.Instance.RedTurn && route_block.sprite_name == "shuai")
            {
                //解将失败
                if (RedKing)
                {
                    StartCoroutine(WarnForMove());
                }
                else
                {
                    StartCoroutine(BanForMove());
                }
                route_blocks = new List<GridBlock>(new_routeblock);
                target.sprite_name = origin_name;
                GameManager.instance.Blocks = new List<GridBlock>(new_blocks);
                return false ;
            }
        }
        route_blocks=new List<GridBlock>(new_routeblock);
        target.sprite_name = origin_name;
        GameManager.instance.Blocks=new List<GridBlock>(new_blocks);
        GameManager.instance.index_Checked=origin_index;
        RedKing =false;
        BlackKing=false;
        return true;
    }
    //播放是否要“投降”的警告
    IEnumerator WarnForMove()
    {
        UIManager.Instance.panel_Warn.SetActive(true);
        yield return new WaitForSeconds(1);
        UIManager.Instance.panel_Warn.SetActive(false);
    }
    //播放“不能送将”的警告
    IEnumerator BanForMove()
    {
        UIManager.Instance.panel_Ban.SetActive(true);
        yield return new WaitForSeconds(1);
        UIManager.Instance.panel_Ban.SetActive(false);
    }

}
