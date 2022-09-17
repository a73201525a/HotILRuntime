using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillItem1005 : SkillItem
{
    public SkillItem1005(int SkillID, GameObject skill, SKillController skillController) : base(SkillID, skill, skillController)
    {
    }

    public override void PerformSkill(NPCBase npc, bool isPlayer,int TheCaster)
    {
        base.PerformSkill(npc, isPlayer, TheCaster);

    }

}
