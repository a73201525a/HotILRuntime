using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMsg:ILMsgBase
{
    public Transform followTarget;//跟随目标
    public Vector3 Pos;//显示位置
    public string  TipConnet;//标签内容
    public int curHp;//当前血量
    public int maxHp;//血量上限
    public float Value;//当前传入值
    public int ID;//根据id查找HUD
  //  public int TipTextIndex;//动态文字索引
    /// <summary>
    /// 创建HUD 跟随 显示
    /// </summary>
    /// <param name="followTarget">跟随目标</param>
    /// <param name="curHp">当前血量</param>
    /// <param name="offset">偏移量</param>
    public HUDMsg(ushort msgID,Transform followTarget, int curHp,int maxHp, int ID)
    {
        this.curHp = curHp;  
        this.maxHp = maxHp;
        this.ID = ID; 
        this.followTarget = followTarget;
        this.msgID = msgID;
    }
    /// <summary>
    /// 显示Damage文本
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="Pos"></param>
    /// <param name="TipConnet"></param>
    
    public HUDMsg(ushort msgID,Vector3 Pos, string TipConnet)
    {
        this.TipConnet = TipConnet;
        this.Pos = Pos;
        this.msgID = msgID;
    }
  

    /// <summary>
    /// 更新血量
    /// </summary>
    /// <param name="ID">id</param>
    /// <param name="Damage">伤害值</param>
    /// <param name="percent">百分比</param>
    public HUDMsg(ushort msgID,int ID, float Value)
    {
        this.Value = Value;
        this.ID = ID;
        this.msgID = msgID;
    }  
    /// <summary>
    /// 显示标签
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ID"></param>
    /// <param name="TipConnet"></param>
    public HUDMsg(ushort msgID,int ID, string TipConnet)
    {
        this.TipConnet = TipConnet;
        this.ID = ID;
        this.msgID = msgID;
    } 
    /// <summary>
    /// 动态文字飘字
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ID"></param>
    /// <param name="TipConnet"></param>
    /// <param name="TipTextIndex"></param>
    //public HUDMsg(ushort msgID,int ID, string TipConnet,int TipTextIndex=0)
    //{
    //    this.TipConnet = TipConnet;
    //    this.ID = ID;
    //    this.msgID = msgID;
    //    this.TipTextIndex = TipTextIndex;
    //}
    /// <summary>
    /// 隐藏血条
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ID"></param>
    public HUDMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// 隐藏标签
    /// </summary>
    /// <param name="msgID"></param>
    public HUDMsg(ushort msgID,int ID)
    {
        this.msgID = msgID;
        this.ID = ID;
    }
}
