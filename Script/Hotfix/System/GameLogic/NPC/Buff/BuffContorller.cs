using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public enum GameBuffType
{
    Null = -1,
    Vertigo = 0,//眩晕
    DeformationSheep = 1,//变羊
    Disarm = 2,//缴械
    Frozen = 3,//冻结
    AddAttack = 4,//加攻击
    AddDefence = 5,//加防
    SubAttack = 6,//减攻击
    RemoveDebuff = 7,//解除控制
    SubDefence = 8,//减防
    Poison = 9,//毒

}

public class BuffContorller : NPCBase
{
    public List<Buff> ThisBuff;

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        switch (tmpMsg.msgID)
        {
            case (ushort)ILNpcBuff.LoadBuff://读档加载
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;

                    AddBuff(buffMsg.buffType, buffMsg, buffMsg.time);
                }
                break;
            case (ushort)ILNpcBuff.ActiveBuff://激活buff
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;

                    AddBuff(buffMsg.buffType, buffMsg);
                }
                break;
            case (ushort)ILNpcBuff.RemoveCounter://解除控制技能 
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;

                    RemoveControl(buffMsg.buff, buffMsg.NPCBase);
                }
                break;
            case (ushort)ILNpcBuff.RoundEnd://回合结束更新buff
                {

                    RoundEnd();

                }
                break;
            case (ushort)ILNpcBuff.RoundStart://回合开始更新buff
                {
                    Debug.Log("RoundStart 新的回合开始 处理buff ");
                    RoundStart();
                }
                break;
            case (ushort)ILNpcBuff.GetAllBuff://获取buff
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;
                    BuffMsgBack back = new BuffMsgBack(buffMsg.backMsg, ThisBuff);
                    SendMsg(back);
                }
                break;
            case (ushort)ILNpcBuff.GetDealtailAll://详情页面打开时获取
                {
                    GetAllDeatail();
                }
                break;
            case (ushort)ILNpcBuff.NpcDeath://详情页面打开时获取
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;
                    NpcDeath(buffMsg.NpcID);
                }
                break;

        }
    }
    public void Awake()
    {

        //注册消息
        msgIds = new ushort[]
        {
           (ushort)ILNpcBuff.ActiveBuff,
           (ushort)ILNpcBuff.RoundEnd,
           (ushort)ILNpcBuff.RoundStart,
           (ushort)ILNpcBuff.GetDealtailAll,
           (ushort)ILNpcBuff.GetAllBuff,
           (ushort)ILNpcBuff.RemoveCounter,
           (ushort)ILNpcBuff.NpcDeath,
           (ushort)ILNpcBuff.LoadBuff,
        };
        RegistSelf(this, msgIds);

    }

    public BuffContorller()
    {
        ThisBuff = new List<Buff>();
        BuffCfgTable.Initialize(Utility.TablePath + "BuffCfgData");
    }

    /// <summary>
    /// Npc死亡 移除身上buff
    /// </summary>
    /// <param name="NpcID"></param>
    public void NpcDeath(int NpcID)
    {
        Debug.Log("NpcDeath buff:::" + NpcID);
        List<Buff> removeBuff = new List<Buff>();
        for (int i = 0; i < ThisBuff.Count; i++)
        {
            if (ThisBuff[i].npcBase.NpcID == NpcID)
            {
                removeBuff.Add(ThisBuff[i]);

            }
        }
        for (int i = 0; i < removeBuff.Count; i++)
        {
            ThisBuff.Remove(removeBuff[i]);
        }
    }


    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="buff">buff类型</param>
    /// <param name="buffGameObject">buff显示效果</param>
    /// /// <param name="loadTime">加载buff的时候所剩时间</param>
    public void AddBuff(GameBuffType buff, BuffMsg msg, int loadTime = -1)
    {
        if (buff != GameBuffType.Null)
        {
            Buff curBuff = new Buff(buff, msg.level, msg.NPCBase, msg.casterID);

            //读档的时候使用
            if (loadTime > 0)
            {
                curBuff.time = loadTime;
            }


            for (int i = 0; i < ThisBuff.Count; i++)
            {
                if (ThisBuff[i].npcBase.NpcID == msg.NPCBase.NpcID && ThisBuff[i].buff == buff)
                {
                    ThisBuff[i].CasterID = curBuff.CasterID;
                    ThisBuff[i].time = curBuff.time;
                    Debug.Log(" ThisBuff[i].CasterID:" + ThisBuff[i].CasterID);
                    Debug.Log(" ThisBuff[i].time:" + ThisBuff[i].time);
                    PerformAddBuff(curBuff, msg, false);
                    return;
                }
            }
            Debug.Log("buff类型::" + buff);
            Debug.Log("bufftime::" + curBuff.time);
            UpdateHud(curBuff);
            ThisBuff.Add(curBuff);
            PerformAddBuff(curBuff, msg, true);


        }
    }

    /// <summary>
    /// 解控技能
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="buffNpc"></param>
    public void RemoveControl(Buff buff, NPCBase buffNpc)
    {
        for (int i = 0; i < ThisBuff.Count; i++)
        {
            if (buffNpc.NpcID == ThisBuff[i].npcBase.NpcID && IsDebuff(ThisBuff[i].buff))
            {
                ThisBuff[i].time = 0;
                PerformRemoveBuff(ThisBuff[i], buffNpc);
            }
        }
        for (int i = 0; i < ThisBuff.Count; i++)
        {
            if (ThisBuff[i].time == 0)
            {
                ThisBuff.RemoveAt(i);
            }

        }

    }


    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="buff">buff类型</param>
    /// <param name="buffNpc">buff显示效果</param>
    public void RemoveBuffList(Buff buff, NPCBase buffNpc)
    {
        if (buffNpc.isDeath)
        {
            ThisBuff.Remove(buff);
            Debug.Log("已经死亡,buff已移除");
            return;
        }

        PerformRemoveBuff(buff, buffNpc);


    }

    /// <summary>
    /// 执行buff
    /// </summary>
    /// <param name="buff"></param>
    public void PerformAddBuff(Buff Buff, BuffMsg msg, bool isAddBuff = true)
    {
        NPCBase buffNpc = msg.NPCBase;
        buffNpc.AddBuff(Buff);
        switch (Buff.buff)
        {
            case GameBuffType.Vertigo:
                buffNpc.Vertigo(Buff, isAddBuff);
                break;
            case GameBuffType.DeformationSheep:
                buffNpc.DeformationSheep(Buff, isAddBuff);
                break;
            case GameBuffType.Disarm:
                buffNpc.Disarm(Buff, isAddBuff);
                break;
            case GameBuffType.Frozen:
                buffNpc.Frozen(Buff, isAddBuff);
                break;
            case GameBuffType.AddAttack:
                buffNpc.AddAttack(Buff);
                break;
            case GameBuffType.AddDefence:
                buffNpc.AddDefence(Buff);
                break;
            case GameBuffType.SubDefence:
                buffNpc.SubDefence(Buff);
                break;
            case GameBuffType.SubAttack:
                buffNpc.SubAttack(Buff);
                break;
            case GameBuffType.Poison://第一次扣血
                buffNpc.Poison(Buff, true);
                break;
        }

    }

    /// <summary>
    /// 解除buff
    /// </summary>
    /// <param name="buff">buff</param>
    /// <param name="buffNpc"></param>
    public void PerformRemoveBuff(Buff Buff, NPCBase buffNpc)
    {
        HidUpdateHud(Buff);
        buffNpc.RemoveNPCBuff(Buff);
        switch (Buff.buff)
        {
            case GameBuffType.Disarm:
                buffNpc.RemoveDisarm(Buff);

                break;
            case GameBuffType.Vertigo:
                buffNpc.RemoveVertigo(Buff);

                break;
            case GameBuffType.DeformationSheep:
                buffNpc.RemoveDeformationSheep(Buff);
                break;
            case GameBuffType.Frozen:
                buffNpc.RemoveFrozen(Buff);
                break;
            case GameBuffType.AddAttack:
                buffNpc.RemoveAddAttack(Buff);
                break;
            case GameBuffType.AddDefence:
                buffNpc.RemoveDefence(Buff);
                break;
            case GameBuffType.SubAttack:
                buffNpc.RemoveSubAttack(Buff);
                break;
            case GameBuffType.SubDefence:
                buffNpc.RemoveSubDefence(Buff);
                break;
            case GameBuffType.Poison:
                buffNpc.RemovePoison(Buff);
                break;
        }

    }

    /// <summary>
    /// 每回合执行一次的buff
    /// </summary>
    public async void RoundStartBuff()
    {
        PlayerMsg playerMsg = new PlayerMsg((ushort)ILNpcEvent.UpdatePlayer, UpdatePlayerType.PlayerOperationClose);
        SendMsg(playerMsg);
        FightMainMsg fightMain = new FightMainMsg((ushort)ILUIEvent.FightMianProhibitionRound, false);//禁用主界面按钮交互
        SendMsg(fightMain);

        List<Buff> tmpBuffPerformList = new List<Buff>();
        for (int i = 0; i < ThisBuff.Count; i++)
        {
            tmpBuffPerformList.Add(ThisBuff[i]);
        }

        for (int i = 0; i < tmpBuffPerformList.Count; i++)
        {
            if (tmpBuffPerformList[i].buff == GameBuffType.Poison)
            {
                PlayerMsg playerMsg1 = new PlayerMsg((ushort)ILNpcEvent.UpdatePlayer, tmpBuffPerformList[i].npcBase.transform.position, UpdatePlayerType.UpdateCameraFollowTarget);
                SendMsg(playerMsg1);

                await Task.Delay(500);

                tmpBuffPerformList[i].npcBase.Poison(tmpBuffPerformList[i]);
                await Task.Delay(500);

            }
        }


        SendMsg((ushort)AdventureBuildEvent.RoundStart);
    }

    void RoundStart()
    {
        RoundStartBuff();
    }


    /// <summary>
    /// 回合结束 更新buff CD
    /// </summary>
    /// <param name="playerRound"></param>
    void RoundEnd()
    {

        for (int i = 0; i < ThisBuff.Count; i++)
        {
            RemoveBuff(ThisBuff[i], i);

        }

        for (int i = 0; i < ThisBuff.Count; i++)
        {
            if (ThisBuff[i].time == 0)
            {
                ThisBuff.RemoveAt(i);
            }

        }


    }
    /// <summary>
    /// 移除玩家已完成的buff
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="index"></param>
    public void RemoveBuff(Buff buff, int index)
    {
        buff.time--;
        Debug.Log("bufftype:::" + buff.buff);
        if (buff.time == 0)
        {
            Debug.Log("移除Buff" + buff.buff);
            RemoveBuffList(buff, buff.npcBase);
        }
    }
    /// <summary>
    /// 获取buff所有详情
    /// </summary>
    public void GetAllDeatail()
    {
        ILUIManager.ShowView(UIPanelPath.DetailPanel, UILevel.Top, ILUIEventPanel.DetailPanel);
        DeatailMsg deatailMsg = new DeatailMsg((ushort)ILUIEvent.DetailBuff, ThisBuff);
        SendMsg(deatailMsg);
    }
    /// <summary>
    ///详情获取 buff消息
    /// </summary>
    /// <param name="buff"></param>
    public void UpdateHud(Buff buff)
    {
        HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.ShowHUDTip, buff.npcBase.NpcID, buff.name);
        SendMsg(hUDMsg);
    }

    public void HidUpdateHud(Buff buff)
    {
        HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.HideHUDTip, buff.npcBase.NpcID);
        SendMsg(hUDMsg);

        for (int i = 0; i < ThisBuff.Count; i++)
        {

            if (ThisBuff[i].npcBase.NpcID == buff.npcBase.NpcID && buff.buff != ThisBuff[i].buff)
            {
                UpdateHud(ThisBuff[i]);
                return;
            }
        }
    }



    public override void OnDestroy()
    {
        base.OnDestroy();
        ThisBuff.Clear();

    }
    /// <summary>
    ///负面效果解除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool IsDebuff(GameBuffType id)
    {
        if (id == GameBuffType.Vertigo ||
            id == GameBuffType.Frozen || id == GameBuffType.DeformationSheep)
        {
            return true;
        }

        return false;
    }
}
