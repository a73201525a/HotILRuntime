using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������������
/// </summary>
public class SettlementMsg : ILMsgBase
{
    public int ConfigID;//����id
    public RoundType isMonsterRound;//����غ�
    public int PlayerID;//���id
    public ushort callBack;//�ص�
    public int[] rewardList;//������Ʒ

    /// <summary>
    /// ��Ʒ���ݷ���
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="rewardList">������Ʒ</param>
    /// <param name="isMonsterRound"></param>
    /// <param name="playerID"></param>
    public SettlementMsg(ushort msgID, int[] rewardList, RoundType isMonsterRound,int playerID)
    {
        this.msgID = msgID;
        this.rewardList = rewardList;   
        this.isMonsterRound = isMonsterRound;
        this.PlayerID = playerID;
    }
}
/// <summary>
/// �ؿ������
/// </summary>
public class AdventureSettlementMsg : ILMsgBase
{
    internal AdventureCfgTable adventureCfg;//��ǰ�ؿ���������
    public bool AdventureWin;//�Ƿ�ʤ��
    public ushort callBack;//�ص�
    /// ���������Ϣ
    /// </summary>
    /// <param name="msgID"></param>
    public AdventureSettlementMsg(ushort msgID, ushort callBack)
    {
        this.msgID = msgID;
        this.callBack = callBack;
    }

    /// <summary>
    /// ������ȡ��Ʒ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="AdventureWin">�Ƿ�ͨ��</param>
    /// <param name="adventureCfg">��������Ʒ</param>
    internal AdventureSettlementMsg(ushort msgID, bool AdventureWin , AdventureCfgTable adventureCfg)
    {
        this.msgID = msgID;
        this.adventureCfg = adventureCfg;
        this.AdventureWin = AdventureWin;
      
    }
}
  
