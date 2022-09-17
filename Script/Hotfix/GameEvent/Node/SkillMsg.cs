using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMsg : ILMsgBase
{
    public PlayerBase playerBase;//���
    public SkillOrBuildType SkillOrBuildType;//��������or ��������
    public int SkillID;//����ID
    public int[] SkillArray;//��������

    /// <summary>
    /// ����ID��������
    /// </summary>
    /// <param name="SkillID"></param>
    public SkillMsg(int SkillID, Vector3 pos, ushort msgID)
    {
        this.SkillID = SkillID;
        this.msgID = msgID;
    }


    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <param name="SkillArray"></param>
    /// <param name="msgID"></param>
    public SkillMsg(int[] SkillArray, ushort msgID)
    {
        this.SkillArray = SkillArray;
        this.msgID = msgID;
    }
    /// <summary>
    /// ����Or����
    /// </summary>
    /// <param name="playerBase">�������</param>
    /// <param name="SkillOrBuildType">����or��������</param>
    public SkillMsg(ushort msgId, PlayerBase playerBase, SkillOrBuildType SkillOrBuildType)
    {
        this.msgID = msgId;
        this.playerBase = playerBase;
        this.SkillOrBuildType = SkillOrBuildType;

    }

}


public class CreatSkillMsg : ILMsgBase
{
    public int SkillID;//����id
    public int SkillLevel;//���ܵȼ�
    public NPCBase StartNpc;//ʩ����
    public NPCBase EndNpc;//�ܷ�����
    public BuildItemBase Build;//����ʩ��

    

    public SkillAttackObject StartType;//ʩ����
    public SkillAttackObject EndType;//��������
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="id">����id</param>
    /// <param name="npcStart">ʩ����</param>
    /// <param name="npcEnd">�ܷ�����</param>
    /// <param name="build">����ʩ��</param>
    /// <param name="StartType">ʩ����</param>
    /// <param name="EndType">��������</param>
    public CreatSkillMsg(ushort msgId, int id,int SkillLevel, NPCBase npcStart, NPCBase npcEnd, BuildItemBase build, SkillAttackObject StartType, SkillAttackObject EndType)
    {
        this.msgID = msgId;
        this.SkillID = id;
        this.SkillLevel = SkillLevel;
        this.StartNpc = npcStart;
        this.EndNpc = npcEnd;
        this.Build = build;
        this.StartType = StartType;
        this.EndType = EndType;


    }
}



/// <summary>
/// ʹ�ü���(��ʹ�ö������Ϣ)
/// </summary>

public class SkillDataMsg : ILMsgBase
{
    public int NPCID;//������ID
    public NPCBase endNpc;//��������
    public SkillItem Skill;//ʹ�ü��ܵ�ID
    public RoundType roundType;//˭�Ļغ�ʩ��
    /// <summary>
    /// 
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerID">���id</param>
    /// <param name="SkillID">ʹ�ü��ܵ�ID</param>
    /// <param name="roundType">˭�Ļغ�ʩ��</param>
    public SkillDataMsg(ushort msgID, int NPCID, SkillItem Skill, NPCBase endNpc, RoundType roundType)
    {
        this.NPCID = NPCID;
        this.msgID = msgID;
        this.Skill = Skill;
        this.roundType = roundType;
        this.endNpc = endNpc;
    }
}