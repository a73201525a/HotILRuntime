using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SkillItem
{

    /// <summary>
    /// 技能id
    /// </summary>
    public int SkillID;
    public GameObject skill;//技能对象
    public bool isFinish;//是否完成
    public int durTime;//技能持续时间
    public SKillController skillController;
    public int Level;//技能等级

    public SkillItem(int SkillID,  GameObject skill, SKillController skillController)
    {
        this.SkillID = SkillID;
        this.skill = skill;
        skill.SetActive(false);
        //=========ToDo根据技能等级
        durTime = GetConfig().durationTime[0];// 赋值
        isFinish = true;
        this.skillController = skillController;
        this.Level = 0;
    }

    /// <summary>
    /// 执行播放移动动画效果
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    public void Play(Vector3 startPos, Vector3 endPos)
    {
        isFinish = false;
        this.skill.transform.position = startPos;
        skill.SetActive(true);
        this.skill.transform.DOMove(endPos, 1f).OnComplete(WaitTimeHide);

    }
    public void WaitTimeHide()
    {
        Hide();
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public void Hide()
    {
        isFinish = true;
        skill.SetActive(false);

    }
    /// <summary>
    /// 
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(skill);
    }
    /// <summary>
    /// 技能配置
    /// </summary>
    /// <returns></returns>
    internal SkillCfgTable GetConfig()
    {
        return SkillCfgTable.Instance.data[SkillID];
    }

    public void SetLevel(int Level)
    {
        this.Level = Level;
    }

    public virtual void PerformSkill(NPCBase npc, bool isPlayer, int TheCaster)
    {
        HUDMsg HUDMsg = new HUDMsg((ushort)ILUIEvent.ShowHUDDynamicTip, npc.NpcID, Utility.GetLanguage(GetConfig().name));
        skillController.SendMsg(HUDMsg);


        if (GetConfig().isBuff)
        {
            BuffMsg buffMsg = new BuffMsg((ushort)ILNpcBuff.ActiveBuff, (GameBuffType)GetConfig().buffID, npc, TheCaster, Level);
            skillController.SendMsg(buffMsg);
        }
    }

    public virtual int GetDataNumber()
    {
        return 0;
    }

    public void Perform(NPCBase npc, bool isPlaye, int TheCaster)
    {
        if (SkillID == 1001)//治疗
        {
            SkillItem1001 skillItem = (SkillItem1001)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1002)//火球术
        {
            SkillItem1002 skillItem = (SkillItem1002)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1003)
        {
            SkillItem1003 skillItem = (SkillItem1003)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1004)
        {
            SkillItem1004 skillItem = (SkillItem1004)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }

        if (SkillID == 1005)
        {
            SkillItem1005 skillItem = (SkillItem1005)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1006)
        {
            SkillItem1006 skillItem = (SkillItem1006)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1007)
        {
            SkillItem1007 skillItem = (SkillItem1007)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1008)
        {
            SkillItem1008 skillItem = (SkillItem1008)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1009)
        {
            SkillItem1009 skillItem = (SkillItem1009)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1010)//解控
        {
            SkillItem1010 skillItem = (SkillItem1010)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1011)
        {
            SkillItem1011 skillItem = (SkillItem1011)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1012)
        {
            SkillItem1012 skillItem = (SkillItem1012)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
        if (SkillID == 1013)
        {
            SkillItem1013 skillItem = (SkillItem1013)this;
            skillItem.PerformSkill(npc, isPlaye, TheCaster);
        }
    }
}
