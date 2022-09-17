using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMsg : ILMsgBase
{
    public int NpcID;
    public ushort backMsg;
    public Buff buff;//buffʹ��
    public int casterID;//ʩ���ߵ�ID
    public int time;//����ʹ��  buffʣ��ʱ��
    public int level;//buff�ȼ�
    /// <summary>
    /// buffЧ������
    /// </summary>
    public GameBuffType buffType;

    /// <summary>
    /// Ч������
    /// </summary>
    public NPCBase NPCBase;
    /// <summary>
    /// ����BuffЧ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buffType">buffЧ��</param>
    /// <param name="NPCBase">��Ҫ����Ķ���</param>
    public BuffMsg(ushort msgID, GameBuffType buffType, NPCBase NPCBase, int casterID, int level, int time = -1)
    {
        this.msgID = msgID;
        this.buffType = buffType;
        this.NPCBase = NPCBase;
        this.casterID = casterID;
        this.time = time;
        this.level = level;
    }
    /// <summary>
    /// Buff�Ƴ�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buff"></param>
    /// <param name="NPCBase"></param>
    public BuffMsg(ushort msgID, Buff buff, NPCBase NPCBase)
    {
        this.msgID = msgID;
        this.buff = buff;
        this.NPCBase = NPCBase;
    }
    /// <summary>
    ///��һغϸ���BuffЧ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="isPlayerRefreshBuff">����һغ�?</param>
    public BuffMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// ��Ϣ�ص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="backID"></param>
    public BuffMsg(ushort msgID, ushort backID)
    {
        this.msgID = msgID;
        this.backMsg = backID;
    }
    /// <summary>
    /// �Ƴ�buff
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="NpcID">��ɫid</param>
    public BuffMsg(ushort msgID, int NpcID)
    {
        this.msgID = msgID;
        this.NpcID = NpcID;
    }
}

public class BuffMsgBack : ILMsgBase
{
    public List<Buff> buffList;
    public BuffMsgBack(ushort msgID, List<Buff> buffList)
    {
        this.msgID = msgID;
        this.buffList = buffList;
    }
}
