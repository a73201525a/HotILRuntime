using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ð����Ϣ
/// </summary>
public class ILUIAdventureMsg : ILMsgBase
{
    public int[] playerIDArray;//����ð�����id����
    public int[] monsterIDArr;//����ð�����id����
    public int[] buildIDArr;//����ð�����id���� 
    public int[] buildLevel;//����ð�ս����ȼ�
    public Vector2[] monsterPos;//����ؿ�ð�չ���λ��
    public Vector2[] buildPos;//����ؿ�ð�ս���λ��
    public Vector2[] playerPos;//����ؿ�ð�����λ��
    public int AdventureID;//�ؿ�id
    public PlayerJson[] playerJsons;//�������

    public string AdventureCondition;//�ؿ�ʤ������
    public bool isRoundStart;//�Ƿ����µĻغ�
    public ushort CallBack;//�ص�
 
    /// <summary>
    /// ��ȡ�ؿ�id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureID"></param>
    public ILUIAdventureMsg(ushort msgID, int AdventureID)
    {
        this.msgID = msgID;
        this.AdventureID = AdventureID;
    }
    /// <summary>
    /// ��ʾ�ı�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureCondition"></param>
    public ILUIAdventureMsg(ushort msgID, string AdventureCondition,bool isRoundStart)
    {
        this.msgID = msgID;
        this.AdventureCondition = AdventureCondition;
        this.isRoundStart = isRoundStart;
    }
    /// <summary>
    /// �޲���Ϣ
    /// </summary>
    /// <param name="msgID"></param>
    public ILUIAdventureMsg(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// ����ð��
    /// </summary>
    /// <param name="msgID"></param>
    public ILUIAdventureMsg(ushort msgID, PlayerJson[] playerJsons, int AdventureID)
    {
        this.msgID = msgID;
        this.playerJsons = playerJsons;
        this.AdventureID = AdventureID; 
    }

    /// <summary>
    ///  ���ش浵
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerIDArray">���id</param>
    /// <param name="monsterIDArr">����id</param>
    /// <param name="buildIDArr">����id</param>
    /// <param name="monsterPos">����λ��</param>
    /// <param name="buildPos">����λ��</param>
    /// <param name="playerPos">���λ��</param>
    /// <param name="AdventureID">�ؿ�id</param>
    public ILUIAdventureMsg(ushort msgID, int[] playerIDArray, int[] monsterIDArr, int[] buildIDArr,int[]buildLevel,Vector2[] monsterPos,Vector2 [] buildPos, Vector2[] playerPos, int AdventureID)
    {
        this.msgID = msgID;
        this.playerIDArray = playerIDArray;
        this.monsterIDArr = monsterIDArr;
        this.buildIDArr = buildIDArr;
        this.AdventureID = AdventureID; 
        this.monsterPos = monsterPos; 
        this.buildPos = buildPos;
        this.playerPos = playerPos;
        this.buildLevel = buildLevel;
    }


    public ILUIAdventureMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;
    }
}
/// <summary>
///ð�ճ�����Ϣ
/// </summary>
public class AdventureMsg : ILMsgBase
{
    /// <summary>
    /// ����ʤ������
    /// </summary>
    public AdventureWinType winType; 
  
    /// <summary>
    /// �ؿ�����ʤ��
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="winType"></param>
    public AdventureMsg(ushort msgID, AdventureWinType winType)
    {
        this.msgID = msgID;
        this.winType = winType;
    }
}
/// <summary>
/// �ؿ��ص�
/// </summary>
public class ILUIAdventureMsgBack : ILMsgBase
{
    public int AdventureID;//�ؿ�ID

    /// <summary>
    /// ��ȡ�ؿ�id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureID"></param>
    public ILUIAdventureMsgBack(ushort msgID, int AdventureID)
    {
        this.msgID = msgID;
        this.AdventureID = AdventureID;
    }
 
}