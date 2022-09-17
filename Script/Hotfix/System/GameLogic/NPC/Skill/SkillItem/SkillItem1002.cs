using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillItem1002 : SkillItem
{
    public SkillItem1002(int SkillID, GameObject skill, SKillController skillController) : base(SkillID,  skill, skillController)
    {
    }

    public override int GetDataNumber()
    {
        return GetConfig().damage[Level] +( GetConfig().damage[Level] * Level)/2;

    }
    public override void PerformSkill(NPCBase npc, bool isPlayer,int TheCaster)
    {
        base.PerformSkill(npc, isPlayer, TheCaster);
        int damage = GetDataNumber();

        if (isPlayer)
        {
            PlayerBase player = (PlayerBase)npc;
            player.playerData.playerJson.curHp -= damage;
        }
        else
        {
            MonsterBase monster = (MonsterBase)npc;
            monster.monsterData.CurHp -= damage;
        }
        
        HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.ReduceBlood, npc.NpcID, damage);
        skillController.SendMsg(hUDMsg);
    }

}