using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMsg : ILMsgBase
{
    /// <summary>
    /// ����id
    /// </summary>
    public int buildID; 
    /// <summary>
    /// ��Ʒindex
    /// </summary>
    public int itemIndex;

    /// <summary>
    /// �ص���Ϣ
    /// </summary>
    public ushort Callback;

    /// <summary>
    /// λ������
    /// </summary>
    public int IndexX;
    /// <summary>
    /// λ������
    /// </summary>
    public int IndexY;
   
    public bool prohibitionAttack;//����� 
    public bool prohibitionBuildAttack;//�ܷ񹥻����� 
    public bool prohibitionSkill;//���ܽ���
    public RoundType roundType;//�Ƿ�����ҹ���
    public bool PoniterEnter;//�������
    public int attackerID;//������ID
   
    /// <summary>
    /// ��ǰ��Ҳ���id
    /// </summary>
    public int curPlayerID;
    /// <summary>
    /// ��Ҫ����ѷ�������Ʒ
    /// </summary>
    public GameObject tmpGameObject;

    public SaveMapData[] saveMapDatas;//�浵������
    /// <summary>
    /// ��ȡ�浵����
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="saveMapDatas"></param>
    public BuildMsg(ushort msgID, SaveMapData[] saveMapDatas)
    {
        this.msgID = msgID;
        this.saveMapDatas = saveMapDatas;
    }
    /// <summary>
    /// ��ȡ��ǰ�������id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="curPlayerID"></param> 
    /// <param name="prohibitionAttack"></param>
    /// <param name="prohibitionBuildAttack"></param>   
    /// /// <param name="prohibitionSkill"></param>
    public BuildMsg(ushort msgID, int curPlayerID,bool prohibitionAttack,bool prohibitionBuildAttack,bool prohibitionSkill)
    {
        this.msgID = msgID;
        this.curPlayerID = curPlayerID;
        this.prohibitionAttack = prohibitionAttack; 
        this.prohibitionBuildAttack = prohibitionBuildAttack;
        this.prohibitionSkill = prohibitionSkill;
    }
    /// <summary>
    ///��Ҵ����ѷ�����
    /// </summary> 
    /// <param name="buildID">����id</param>
    /// <param name="playerID">���id</param>
    /// <param name="IndexX">λ������</param>
    /// <param name="IndexY ">λ������/param>
    /// <param name="msgID"></param>
    public BuildMsg(int buildID,int playerID, int IndexX, int IndexY, ushort msgID)
    {
        this.msgID = msgID;
        this.curPlayerID = playerID;
        this.buildID = buildID;
        this.IndexY = IndexY;
        this.IndexX = IndexX;
    }
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="tmpGameObject">������Ʒ</param>
    /// <param name="attackerID">������id</param> 
    /// <param name="IsPlayerAttack">�������ǲ������</param>
    public BuildMsg(ushort msgID,GameObject tmpGameObject,int attackerID, RoundType roundType)
    {
        this.msgID = msgID;
        this.tmpGameObject = tmpGameObject;
        this.roundType = roundType; 
        this.attackerID = attackerID;
    }
    /// <summary>
    /// ɾ������
    /// </summary>
    /// <param name="tmpGameObject">����ID</param>
    /// <param name="msgID"></param>
    public BuildMsg(ushort msgID,GameObject tmpGameObject)
    {
        this.msgID = msgID;
        this.tmpGameObject = tmpGameObject;
       
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="itemIndex">��Ʒ����</param>
    public BuildMsg(ushort msgID,int itemIndex)
    {
        this.msgID = msgID;
        this.itemIndex = itemIndex;
    } 
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="PoniterEnter"></param>
    public BuildMsg(ushort msgID,bool PoniterEnter)
    {
        this.msgID = msgID;
        this.PoniterEnter = PoniterEnter;
    }
    /// <summary>
    /// �޲���Ϣ
    /// </summary>
    /// <param name="msgID"></param>
    public BuildMsg(ushort msgID)
    {
        this.msgID = msgID;
    } 
    /// <summary>
    /// ��Ϣ�ص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public BuildMsg(ushort msgID,ushort CallBack)
    {
        this.msgID = msgID;
        this.Callback = CallBack;
    }


}
public class BuildMsgBack : ILMsgBase
{
    /// <summary>
    /// ����id
    /// </summary>
    public int buildID;
    /// <summary>
    /// ��������
    /// </summary>
    public List<BuildItemBase> buildList;
    /// <summary>
    /// �޲λص���Ϣ
    /// </summary>
    /// <param name="msgID"></param>
    public BuildMsgBack(ushort msgID)
    {
        this.msgID = msgID;
    }
    /// <summary>
    /// �رյ�ǰUI
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buildID"></param>
    public BuildMsgBack(ushort msgID, int buildID)
    {
        this.msgID = msgID;
        this.buildID = buildID;
    } 
    /// <summary>
    /// ��ȡ���н���
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="buildID"></param>
    public BuildMsgBack(ushort msgID, List<BuildItemBase> buildList)
    {
        this.msgID = msgID;
        this.buildList = buildList;
    }
}