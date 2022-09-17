using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Buff
{
    /// <summary>
    /// �������غ�
    /// </summary>
    public int time;
    /// <summary>
    /// ���˾���
    /// </summary>
    public int repelDis;
    /// <summary>
    /// Ч������
    /// </summary>
    public string name;
    /// <summary>
    /// ��Դ����
    /// </summary>
    public string resName;
    /// <summary>
    /// ��ǰbuff����
    /// </summary>
    public GameBuffType buff;
    /// <summary>
    /// ������
    /// </summary>
    public int attack;
    /// <summary>
    /// ������
    /// </summary>
    public int defence;
    /// <summary>
    /// buff����
    /// </summary>
    public int buffIndex;
    /// <summary>
    /// buff������Ѫ�˺�
    /// </summary>
    public int durDamage;
    /// <summary>
    /// ��ǰbuffЧ��
    /// </summary>
    public NPCBase npcBase;

    public int Level;//buff�ȼ�


    public int CasterID;//ʩ��buff��ID


    public Buff(GameBuffType buff,int Level, NPCBase npcBase, int CasterID)
    {
        this.buff = buff;
        //����
        this.npcBase = npcBase;
        this.Level = Level;
        this.CasterID = CasterID;
        this.buffIndex = (int)buff;
        BuffCfgTable buffCfg = BuffCfgTable.Instance.Get((int)buff);
        time = buffCfg.time;
        name = Utility.GetLanguage(buffCfg.name);
        resName = buffCfg.resName;
        //=====================TODO::::���ݵȼ���ʵ��======================
        attack = buffCfg.attack[Level];
        defence = buffCfg.defence[Level];
        durDamage = buffCfg.durDamage[Level];

    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(time);
        writer.Write((int)buff);
        writer.Write(npcBase.NpcID);
        writer.Write(CasterID);
        writer.Write(Level);
    }

    public void Load()
    {

    }
}
