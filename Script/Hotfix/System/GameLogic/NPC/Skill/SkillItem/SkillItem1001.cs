using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¼ÓÑªÊõ
/// </summary>
public class SkillItem1001 : SkillItem
{
    public SkillItem1001(int SkillID, GameObject skill, SKillController skillController) : base(SkillID,  skill, skillController)
    {

    }

    public override int GetDataNumber()
    {
        return GetConfig().revert[Level] + (GetConfig().revert[Level] * Level) / 2;

    }

    public override void PerformSkill(NPCBase npc, bool isPlayer, int TheCaster)
    {
        base.PerformSkill(npc, isPlayer, TheCaster);


        if (isPlayer)
        {
            PlayerBase player = (PlayerBase)npc;
            player.playerData.playerJson.curHp += GetDataNumber();
            if (player.playerData.playerJson.curHp > player.playerData.playerJson.maxHp)
            {
                player.playerData.playerJson.curHp = player.playerData.playerJson.maxHp;

            }

        }
        else
        {
            MonsterBase monster = (MonsterBase)npc;


            monster.monsterData.CurHp += GetDataNumber();
            if (monster.monsterData.CurHp > monster.monsterData.MaxHp)
            {
                monster.monsterData.CurHp = monster.monsterData.MaxHp;

            }
        }

        HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.AddBlood, npc.NpcID, GetConfig().revert[Level]);
        skillController.SendMsg(hUDMsg);
    }
}

