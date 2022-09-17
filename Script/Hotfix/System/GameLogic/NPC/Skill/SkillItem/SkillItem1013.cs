using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ªÿ¿∂
/// </summary>
public class SkillItem1013 : SkillItem
{
    public SkillItem1013(int SkillID, GameObject skill, SKillController skillController) : base(SkillID, skill, skillController)
    {
    }
    public override int GetDataNumber()
    {
        return GetConfig().revert[Level] + (GetConfig().revert[Level] * Level) / 2;

    }
    public override void PerformSkill(NPCBase npc, bool isPlayer, int TheCaster)
    {
        base.PerformSkill(npc, isPlayer, TheCaster);
        int revert = GetDataNumber();

        if (isPlayer)
        {
            PlayerBase player = (PlayerBase)npc;

            player.playerData.playerJson.curMp += revert;

            if (player.playerData.playerJson.curMp > player.playerData.playerJson.maxMp)
            {
                player.playerData.playerJson.curMp = player.playerData.playerJson.maxMp;
            }
            PlayerDataMsg dataMsg = new PlayerDataMsg((ushort)ILUIEventPlayerPanel.UpdataPlaerJson, (Player)player);
            skillController.SendMsg(dataMsg);
            HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.ShowHUDDynamicTip, npc.NpcID, Utility.GetLanguage(GetConfig().name));
            skillController.SendMsg(hUDMsg);
        }
    }

}
