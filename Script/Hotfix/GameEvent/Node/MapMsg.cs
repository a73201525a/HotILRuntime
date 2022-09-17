using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图消息
/// </summary>
public class MapMsg : ILMsgBase
{
    /// <summary>
    /// 回调
    /// </summary>
    public ushort CallBack;
    public AttackTipType attackArea;//攻击区域类型
    public int CenterX;//中心点
    public int CenterY;//中心点
    public int MoveToX;//到达路径X值
    public int MoveToY;//到达路径Y值
    public int Range;//距离
    public bool isObs;//障碍设置
    public int PlayerID;//玩家id
    /// <summary>
    /// 获取地图格子
    /// </summary>
    /// <param name="CenterX">中心点</param>
    /// <param name="CenterY">中心点</param>
    /// <param name="msgID"></param>
    public MapMsg(int CenterX, int CenterY, ushort msgID)
    {
        this.msgID = msgID;
        this.CenterX = CenterX;
        this.CenterY = CenterY;
    }
    /// <summary>
    /// 设置障碍
    /// </summary>
    /// <param name="CenterX"></param>
    /// <param name="CenterY"></param>
    /// <param name="isObs"></param>
    /// <param name="msgID"></param>
    public MapMsg(int CenterX, int CenterY,bool isObs,ushort msgID)
    {
        this.msgID = msgID;
        this.CenterX = CenterX;
        this.CenterY = CenterY;
        this.isObs = isObs;
    }
    /// <summary>
    /// 攻击提示
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="callback">攻击范围</param> 
    /// <param name="Range">攻击范围</param>
    /// <param name="CenterX">玩家所在索引</param>
    /// <param name="CenterY"></param>
    /// <param name="attackArea">攻击区域类型</param>
    public MapMsg(ushort msgID,int Range, int CenterX, int CenterY, AttackTipType attackArea, ushort CallBack=0)
    {
        this.msgID = msgID;
        if (CallBack!=0)
        {
            this.CallBack = CallBack;
        }
        this.Range = Range;
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
        this.attackArea = attackArea;
    } 
    /// <summary>
    /// 默认获取提示
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="Range"></param>
    /// <param name="CenterX"></param>
    /// <param name="CenterY"></param>
    public MapMsg(ushort msgID,int Range, int CenterX, int CenterY)
    {
        this.msgID = msgID;
        this.Range = Range;
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
    }
    /// <summary>
    /// 获取路径
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack">回调</param>
    /// <param name="CenterX">起点</param>
    /// <param name="CenterY"></param>
    /// <param name="MoveToX">终点</param>
    /// <param name="MoveToY"></param>
    public MapMsg(ushort msgID,ushort CallBack,int CenterX, int CenterY,int MoveToX,int MoveToY)
    {
        this.msgID = msgID;
      
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
        this.MoveToX = MoveToX;
        this.MoveToY = MoveToY;
        this.CallBack = CallBack;
    }  
    /// <summary>
    ///获取玩家移动路径
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    /// <param name="PlayerID">玩家id</param>
    /// <param name="CenterX">玩家当前格子索引</param>
    /// <param name="CenterY"></param>
    /// <param name="MoveToX"></param>
    /// <param name="MoveToY">移动到格子索引</param>
    public MapMsg(ushort msgID,ushort CallBack,int PlayerID,int CenterX, int CenterY,int MoveToX,int MoveToY)
    {
        this.msgID = msgID;
        this.PlayerID = PlayerID;
        this.CenterX = CenterX;
        this.CenterY = CenterY; 
        this.MoveToX = MoveToX;
        this.MoveToY = MoveToY;
        this.CallBack = CallBack;
    }
    /// <summary>
    /// 无参消息
    /// </summary>
    /// <param name="msgID"></param>
    public MapMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    
}
/// <summary>
/// 地图消息回调
/// </summary>
public class MapBackMsg : ILMsgBase
{
    public List<Vector3> PathList;//路线集合
    public  List<MapHex> HexList;//地图格子集合
    public int MoveToX;//到达某点
    public int MoveToY;//到达某点
    public bool isMapAttackTipRange;//是否在攻击显示的地图范围内
    /// <summary>
    /// /获取路线路径
    /// </summary>
    /// <param name="PathList">路线集合</param>
    /// <param name="msgID"></param>
    public MapBackMsg(List<Vector3> PathList, ushort msgID)
    {
        this.msgID = msgID;
        this.PathList = PathList;

    }
    /// <summary>
    /// 路径终点
    /// </summary>
    /// <param name="MoveToX">到达格子位置索引</param>
    /// <param name="MoveToY">到达格子位置索引</param>
    /// <param name="msgID"></param>
    public MapBackMsg(int MoveToX, int MoveToY, ushort msgID)
    {
        this.msgID = msgID;
        this.MoveToX = MoveToX;
        this.MoveToY = MoveToY;
    }
    /// <summary>
    /// 获取找到大地图格子集合
    /// </summary>
    /// <param name="HexList">地图集合</param>
    /// <param name="msgID"></param>
    public MapBackMsg(List<MapHex> HexList, ushort msgID)
    {
        this.msgID = msgID;
        this.HexList = HexList;

    }
    /// <summary>
    /// 检测范围
    /// </summary>
    /// <param name="isMapAttackTipRange">是否在地图显示攻击范围内</param>
    /// <param name="msgID"></param>
    public MapBackMsg(bool isMapAttackTipRange, ushort msgID)
    {
        this.msgID = msgID;
        this.isMapAttackTipRange = isMapAttackTipRange;

    }
    


}


