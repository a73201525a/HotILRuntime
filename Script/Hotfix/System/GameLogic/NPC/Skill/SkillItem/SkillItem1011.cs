using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillItem1011 : SkillItem
{
    public SkillItem1011(int SkillID, GameObject skill, SKillController skillController) : base(SkillID,  skill, skillController)
    {
    }

    public override void PerformSkill(NPCBase npc, bool isPlayer,int TheCaster)
    {
        base.PerformSkill(npc, isPlayer, TheCaster);
      
    }
   
}
