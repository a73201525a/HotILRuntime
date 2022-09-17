using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public enum GameBuffType
{
    Null = -1,
    Vertigo = 0,//ѣ��
    DeformationSheep = 1,//����
    Disarm = 2,//��е
    Frozen = 3,//����
    AddAttack = 4,//�ӹ���
    AddDefence = 5,//�ӷ�
    SubAttack = 6,//������
    RemoveDebuff = 7,//�������
    SubDefence = 8,//����
    Poison = 9,//��

}

public class BuffContorller : NPCBase
{
    public List<Buff> ThisBuff;

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        switch (tmpMsg.msgID)
        {
            case (ushort)ILNpcBuff.LoadBuff://��������
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;

                    AddBuff(buffMsg.buffType, buffMsg, buffMsg.time);
                }
                break;
            case (ushort)ILNpcBuff.ActiveBuff://����buff
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;

                    AddBuff(buffMsg.buffType, buffMsg);
                }
                break;
            case (ushort)ILNpcBuff.RemoveCounter://������Ƽ��� 
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;

                    RemoveControl(buffMsg.buff, buffMsg.NPCBase);
                }
                break;
            case (ushort)ILNpcBuff.RoundEnd://�غϽ�������buff
                {

                    RoundEnd();

                }
                break;
            case (ushort)ILNpcBuff.RoundStart://�غϿ�ʼ����buff
                {
                    Debug.Log("RoundStart �µĻغϿ�ʼ ����buff ");
                    RoundStart();
                }
                break;
            case (ushort)ILNpcBuff.GetAllBuff://��ȡbuff
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;
                    BuffMsgBack back = new BuffMsgBack(buffMsg.backMsg, ThisBuff);
                    SendMsg(back);
                }
                break;
            case (ushort)ILNpcBuff.GetDealtailAll://����ҳ���ʱ��ȡ
                {
                    GetAllDeatail();
                }
                break;
            case (ushort)ILNpcBuff.NpcDeath://����ҳ���ʱ��ȡ
                {
                    BuffMsg buffMsg = (BuffMsg)tmpMsg;
                    NpcDeath(buffMsg.NpcID);
                }
                break;

        }
    }
    public void Awake()
    {

        //ע����Ϣ
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
    /// Npc���� �Ƴ�����buff
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
    /// ���buff
    /// </summary>
    /// <param name="buff">buff����</param>
    /// <param name="buffGameObject">buff��ʾЧ��</param>
    /// /// <param name="loadTime">����buff��ʱ����ʣʱ��</param>
    public void AddBuff(GameBuffType buff, BuffMsg msg, int loadTime = -1)
    {
        if (buff != GameBuffType.Null)
        {
            Buff curBuff = new Buff(buff, msg.level, msg.NPCBase, msg.casterID);

            //������ʱ��ʹ��
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
            Debug.Log("buff����::" + buff);
            Debug.Log("bufftime::" + curBuff.time);
            UpdateHud(curBuff);
            ThisBuff.Add(curBuff);
            PerformAddBuff(curBuff, msg, true);


        }
    }

    /// <summary>
    /// ��ؼ���
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
    /// �Ƴ�buff
    /// </summary>
    /// <param name="buff">buff����</param>
    /// <param name="buffNpc">buff��ʾЧ��</param>
    public void RemoveBuffList(Buff buff, NPCBase buffNpc)
    {
        if (buffNpc.isDeath)
        {
            ThisBuff.Remove(buff);
            Debug.Log("�Ѿ�����,buff���Ƴ�");
            return;
        }

        PerformRemoveBuff(buff, buffNpc);


    }

    /// <summary>
    /// ִ��buff
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
            case GameBuffType.Poison://��һ�ο�Ѫ
                buffNpc.Poison(Buff, true);
                break;
        }

    }

    /// <summary>
    /// ���buff
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
    /// ÿ�غ�ִ��һ�ε�buff
    /// </summary>
    public async void RoundStartBuff()
    {
        PlayerMsg playerMsg = new PlayerMsg((ushort)ILNpcEvent.UpdatePlayer, UpdatePlayerType.PlayerOperationClose);
        SendMsg(playerMsg);
        FightMainMsg fightMain = new FightMainMsg((ushort)ILUIEvent.FightMianProhibitionRound, false);//���������水ť����
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
    /// �غϽ��� ����buff CD
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
    /// �Ƴ��������ɵ�buff
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="index"></param>
    public void RemoveBuff(Buff buff, int index)
    {
        buff.time--;
        Debug.Log("bufftype:::" + buff.buff);
        if (buff.time == 0)
        {
            Debug.Log("�Ƴ�Buff" + buff.buff);
            RemoveBuffList(buff, buff.npcBase);
        }
    }
    /// <summary>
    /// ��ȡbuff��������
    /// </summary>
    public void GetAllDeatail()
    {
        ILUIManager.ShowView(UIPanelPath.DetailPanel, UILevel.Top, ILUIEventPanel.DetailPanel);
        DeatailMsg deatailMsg = new DeatailMsg((ushort)ILUIEvent.DetailBuff, ThisBuff);
        SendMsg(deatailMsg);
    }
    /// <summary>
    ///�����ȡ buff��Ϣ
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
    ///����Ч�����
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
