using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using ProtoMsg;
using UnityEngine.UI;
using System.Threading.Tasks;

public enum SkillAttackObject
{
    //1玩家 还是 2怪物  好是3建筑
    player = 1,
    monster = 2,
    build = 3,
}


/// <summary>
/// 管理世界效果生成和移除
/// </summary>
public class SKillController : NPCBase
{
    List<SkillItem> SkillList;//所有技能
    private static string SkillName = "Skill";
    int[] Skill;//技能id
    int curSkillID;//当前使用的id
    int curLevel;//当前等级
    private GameObject Parent;//技能父对象
    CreatSkillMsg cretatSkillMsg;//创建相信
    public override void ProcessEvent(ILMsgBase tmpMsg)
    {
        //监听事件
        switch (tmpMsg.msgID)
        {
            case (ushort)ILNpcSKill.LoadSkill://加载技能 多个技能同事创建
                {
                    ILHunkAssetResBack iLHunk = (ILHunkAssetResBack)tmpMsg;
                    for (int i = 0; i < iLHunk.ValueAll.Length; i++)
                    {
                        GameObject skillgo = (GameObject)GameObject.Instantiate(iLHunk.ValueAll[i], Parent.transform);
                        SkillItem item = NewSkillItem(curSkillID, skillgo);
                        SkillList.Add(item);
                    }
                }
                break;
            case (ushort)ILNpcSKill.LoadSingleSkill://加载使用的单个技能效果
                {
                    ILHunkAssetResBack iLHunk = (ILHunkAssetResBack)tmpMsg;

                    CreatSkillSingle(iLHunk.value);
                    CreatSkill(cretatSkillMsg.SkillID, cretatSkillMsg.StartNpc, cretatSkillMsg.EndNpc, cretatSkillMsg.Build, cretatSkillMsg.StartType, cretatSkillMsg.EndType);
                }
                break;
            case (ushort)ILNpcSKill.CreatSkill://使用技能效果
                {
                    CreatSkillMsg skillMsg = (CreatSkillMsg)tmpMsg;
                    cretatSkillMsg = skillMsg;
                    curLevel = skillMsg.SkillLevel;
                    CreatSkill(skillMsg.SkillID, skillMsg.StartNpc, skillMsg.EndNpc, skillMsg.Build, skillMsg.StartType, skillMsg.EndType);
                }
                break;

            case (ushort)ILNpcSKill.InitSkill://初始化技能
                {
                    SkillMsg skillMsg = (SkillMsg)tmpMsg;
                    Init(skillMsg.SkillArray);
                }
                break;
        }
    }
    public void Awake()
    {

        //注册消息
        msgIds = new ushort[]
        {
           (ushort)ILNpcSKill.LoadSkill,
           (ushort)ILNpcSKill.CreatSkill,
           (ushort)ILNpcSKill.InitSkill,
           (ushort)ILNpcSKill.LoadSingleSkill,
        };
        RegistSelf(this, msgIds);
        SkillCfgTable.Initialize(Utility.TablePath + "SkillCfgData");//技能配置
        ILHunkAssetRes iLHunk = new ILHunkAssetRes("scenesone", "effect", (ushort)ILAssetEvent.GetResObject);
        SendMsg(iLHunk);
        SkillList = new List<SkillItem>();
        Parent = new GameObject(SkillName);
        //////////////测试技能//////////////////
        //int[] skillArr = new int[] { 1002 };
        //Init(skillArr);
    }

    /// <summary>
    /// 初始化
    /// </summary> //把玩家和怪物携带的技能传入进来
    public void Init(int[] skillList)
    {
        Skill = skillList;

        ILHunkAssetRes iLHunk = new ILHunkAssetRes("scenesone", "effect", (ushort)ILAssetEvent.GetResObject);
        SendMsg(iLHunk);
    }
    /// <summary>
    /// 创建单个技能
    /// </summary>
    /// <param name="obj"></param>
    void CreatSkillSingle(Object obj)
    {
        GameObject skillgo = (GameObject)GameObject.Instantiate(obj, Parent.transform);
        //SkillItem item = new SkillItem(curSkillID, skillgo, this);
        skillgo.SetActive(false);
        SkillItem item = NewSkillItem(curSkillID, skillgo);
        SkillList.Add(item);
    }
    /// <summary>
    /// 创建技能
    /// </summary>
    /// <param name="id">技能id</param>
    /// <param name="npcStart">施法者</param>
    /// <param name="npcEnd">被击中者</param>
    /// <param name="build">建筑</param>
    /// <param name="StartType">施法者</param>
    /// <param name="EndType">被击中者</param>
    public async void CreatSkill(int id, NPCBase npcStart, NPCBase npcEnd, BuildItemBase build, SkillAttackObject StartType, SkillAttackObject EndType)//type 是攻击1玩家 还是 2怪物  好是3建筑
    {

        SkillItem skill = GetCurSkillItem(id);
        curSkillID = id;

        Debug.Log("CreatSkill::" + skill);

        if (skill != null)//list里找到技能物品了
        {
            Vector3 pos = Vector3.zero;
            if (StartType == SkillAttackObject.player || StartType == SkillAttackObject.monster)//施法者坐标
            {
                pos = npcStart.transform.position;
            }
            else if (StartType == SkillAttackObject.build)//施法者坐标
            {
                pos = build.buildGameObject.transform.position + Vector3.up * 10f;
            }
            else
            {
                Debug.LogError("没有施法者");
            }

            skill.Play(pos, npcEnd.transform.position);
        }
        else
        {
            string skillName = SkillCfgTable.Instance.Get(curSkillID).res_path;
            ILHunkAssetRes iLHunk = new ILHunkAssetRes(true, (ushort)ILAssetEvent.GetResObject, "scenesone", "effect", skillName, (ushort)ILNpcSKill.LoadSingleSkill);
            SendMsg(iLHunk);

            return;
        }
        Debug.Log("使用技能效果等级：" + curLevel);
        skill.SetLevel(curLevel);
        await Task.Delay(500);
        if (EndType == SkillAttackObject.monster && StartType == SkillAttackObject.player)//攻击怪物 施法者是玩家
        {
            Debug.Log("攻击怪物 施法者是玩家");
            SkillDataMsg playerDataMsg = new SkillDataMsg((ushort)ILNpcEvent.MonsterBySkill, npcStart.NpcID, skill, npcEnd, RoundType.Player);
            SendMsg(playerDataMsg);
        }
        else if (EndType == SkillAttackObject.monster && StartType == SkillAttackObject.monster)//攻击怪物 施法者是怪物(增益技能)
        {
            Debug.Log("攻击怪物 施法者是怪物(增益技能)");
            SkillDataMsg playerDataMsg = new SkillDataMsg((ushort)ILNpcEvent.MonsterBySkill, npcStart.NpcID, skill, npcEnd, RoundType.Monster);
            SendMsg(playerDataMsg);
        }
        else if (EndType == SkillAttackObject.player && StartType == SkillAttackObject.monster)//攻击玩家 施法者是怪物
        {
            Debug.Log("攻击玩家 施法者是怪物");
            SkillDataMsg playerDataMsg = new SkillDataMsg((ushort)ILNpcEvent.PlayerBySkill, 0, skill, npcEnd, RoundType.Monster);
            SendMsg(playerDataMsg);

        }
        else if (EndType == SkillAttackObject.player && StartType == SkillAttackObject.player)//攻击玩家 施法者是玩家（增益技能）
        {
            Debug.Log("攻击玩家 施法者是玩家（增益技能）");
            SkillDataMsg playerDataMsg = new SkillDataMsg((ushort)ILNpcEvent.PlayerBySkill, npcStart.NpcID, skill, npcEnd, RoundType.Player);
            SendMsg(playerDataMsg);
        }
        else if (StartType == SkillAttackObject.build && EndType == SkillAttackObject.player)//攻击玩家 施法者是建筑
        {
            Debug.Log("攻击玩家 施法者是建筑");
            SkillDataMsg playerDataMsg = new SkillDataMsg((ushort)ILNpcEvent.PlayerBySkill, build.NpcID, skill, npcEnd, RoundType.Null);
            SendMsg(playerDataMsg);
        }
        else if (StartType == SkillAttackObject.build && EndType == SkillAttackObject.monster)//攻击怪物 施法者是建筑
        {
            Debug.Log("攻击怪物 施法者是建筑");
            SkillDataMsg playerDataMsg = new SkillDataMsg((ushort)ILNpcEvent.MonsterBySkill, build.NpcID, skill, npcEnd, RoundType.Null);
            SendMsg(playerDataMsg);
        }



    }
    /// <summary>
    /// 得到当前使用的技能物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SkillItem GetCurSkillItem(int id)
    {
        for (int i = 0; i < SkillList.Count; i++)
        {
            if (SkillList[i].SkillID == id && SkillList[i].isFinish)
            {
                return SkillList[i];
            }
        }
        return null;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Parent = null;

        for (int i = 0; i < SkillList.Count; i++)
        {
            SkillList[i].Destory();
        }
        SkillList.Clear();
        Skill = null;
        ILHunkAssetRes iLHunk = new ILHunkAssetRes("scenesone", "effect", (ushort)ILAssetEvent.ReleaseSingleBundle);
        SendMsg(iLHunk);
    }


    public SkillItem NewSkillItem(int ID, GameObject gameObject)
    {

        if (ID == 1001)
        {
            SkillItem1001 item = new SkillItem1001(ID, gameObject, this);
            return item;
        }
        if (ID == 1002)
        {
            SkillItem1002 item = new SkillItem1002(ID, gameObject, this);
            return item;
        }
        if (ID == 1003)
        {
            SkillItem1003 item = new SkillItem1003(ID, gameObject, this);
            return item;
        }
        if (ID == 1004)
        {
            SkillItem1004 item = new SkillItem1004(ID, gameObject, this);
            return item;
        }
        if (ID == 1005)
        {
            SkillItem1005 item = new SkillItem1005(ID, gameObject, this);
            return item;
        }
        if (ID == 1006)
        {
            SkillItem1006 item = new SkillItem1006(ID, gameObject, this);
            return item;
        }
        if (ID == 1007)
        {
            SkillItem1007 item = new SkillItem1007(ID, gameObject, this);
            return item;
        }
        if (ID == 1008)
        {
            SkillItem1008 item = new SkillItem1008(ID, gameObject, this);
            return item;
        }
        if (ID == 1009)
        {
            SkillItem1009 item = new SkillItem1009(ID, gameObject, this);
            return item;
        }
        if (ID == 1010)
        {
            SkillItem1010 item = new SkillItem1010(ID, gameObject, this);
            return item;
        }
        if (ID == 1011)
        {
            SkillItem1011 item = new SkillItem1011(ID, gameObject, this);
            return item;
        }
        if (ID == 1012)
        {
            SkillItem1012 item = new SkillItem1012(ID, gameObject, this);
            return item;
        }
        if (ID == 1013)
        {
            SkillItem1013 item = new SkillItem1013(ID, gameObject, this);
            return item;
        }

        Debug.LogError("没有配置技能ID：" + ID);
        return null;
    }

}
