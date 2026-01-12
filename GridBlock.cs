using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public enum BlockType
{
    None,
    Chess,
    Checked
}
//棋子类（棋格），属性：sprite，棋子状态枚举
public class GridBlock : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool isRed;
    public SpriteRenderer sprite;
    public string sprite_name;
    public BlockType type;
    public int Chess_Core;
    public bool CoreChanged=false;
    //开始，初始化棋子的sprite,枚举状态类型
    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Resources.Load<Sprite>(sprite_name);
        
        if (sprite_name !=null)
        {
            type=BlockType.Chess;
        }
        else
        {
            type = BlockType.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //当点击棋子时执行该方法
    private void OnMouseUpAsButton()
    {
        if (!UIManager.Instance.IsGameStart) return;
        if (type == BlockType.Chess)
        {
            //走子结束后重新开始选择新的棋子
            if (GameManager.instance.index_Checked == -1)
            {
                //保证自己回合不能选对方棋子
                if (UIManager.Instance.RedTurn != isRed) return;
                sprite.color = new Color(220 / 255f, 230 / 255f, 100 / 255f, 1f);
                transform.localScale = Vector3.one;
                type = BlockType.Checked;
                GameManager.instance.isChessChecked = true;
            }

            if (GameManager.instance.index_Checked != -1)
            {
                GridBlock selected = GameManager.instance.Blocks[GameManager.instance.index_Checked];
                //重新选择自己的棋子
                if (selected.isRed == this.isRed)
                {
                    sprite.color = new Color(220 / 255f, 230 / 255f, 100 / 255f, 1f);
                    transform.localScale = Vector3.one;
                    type = BlockType.Checked;
                    GameManager.instance.isChessChecked = true;
                    GameManager.instance.Blocks[GameManager.instance.index_Checked].ChangeToNormal();
                }
                else
                {
                    //移动并摧毁地方棋子并转换回合
                    
                    PlayManager.instance.MoveToTarget(this);
                    return;
                }

            }
            GameManager.instance.index_Checked = GameManager.instance.Blocks.IndexOf(this);
            PlayManager.instance.GetRouteByName(this);
            PlayManager.instance.ShowGreenSpot();
        }
        else if (type == BlockType.Checked)
        {
            ChangeToNormal();
            GameManager.instance.isChessChecked = false;
            GameManager.instance.index_Checked = -1;
        }
        else if (type == BlockType.None)
        {
            if (GameManager.instance.index_Checked != -1)
            {
                 GridBlock selected = GameManager.instance.Blocks[GameManager.instance.index_Checked];
                //移动到目标位置并转换回合
                
                PlayManager.instance.MoveToTarget(this);
                    return;
                
            }
        }
    }
    //将棋子转化为初始状态chess
    public void ChangeToNormal()
    {
        sprite.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 1f);
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        type = BlockType.Chess;
        PlayManager.instance.DestroySpot();
    }
    //将棋子（格子）转化为空状态None
    public void ChangeToNone()
    {
        sprite.sprite = null;
        type = BlockType.None;
        sprite_name=null;
    }
    
}
