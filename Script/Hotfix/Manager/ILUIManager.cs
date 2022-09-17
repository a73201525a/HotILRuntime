using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Objetc = UnityEngine.Object;

public class UINode
{
    public GameObject Obj;

    public string resName;

    public string bundleName;

    public string sceneName;

    public ILUIBase nodeBase;

    public UINode(string tmpResName, string tmpBundleName, string tmpSceneName, UnityEngine.Object value, ILUIBase Base)
    {
        this.resName = tmpResName;
        this.bundleName = tmpBundleName;
        this.sceneName = tmpSceneName;
        this.Obj = (GameObject)value;
        this.nodeBase = Base;
    }
}

public class ILUIManager : ILManagerBase
{
    //public static ILUIManager Instance = null;

    private static ILUIManager instance;
    public static ILUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ILUIManager();
                GameObject obj = new GameObject("ILUIManager");
                ILMonoBehaviour mono = obj.AddComponent<ILMonoBehaviour>();
                mono.DontDestoryObj(obj);
                mono.OnUpdate += Update;
            }
            return instance;
        }
    }

    static void Update()
    {
        if (MsgQueue.Count > 0)
        {
            ILMsgBase msg = MsgQueue.Dequeue();
            Instance.SendMsg(msg);
        }
    }

    //缓存事件队列
    public static Queue<ILMsgBase> MsgQueue = new Queue<ILMsgBase>();
    //存储UI控件引用，方便调用
    public static Dictionary<string, UINode> sonMembers = new Dictionary<string, UINode>();
    public static Dictionary<string, ILUIBase> BaseList = new Dictionary<string, ILUIBase>();

    /// <summary>
    /// 执行对应模块事件
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        if (msg.GetManager() == ILManagerID.UIManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }

    /// <summary>
    /// 下一帧在执行事件
    /// </summary>
    /// <param name="msg"></param>
    public void SendAwaitMsg(ILMsgBase msg)
    {
        //await  Task.Delay(100);
        if (msg.GetManager() == ILManagerID.UIManager)
        {
            MsgQueue.Enqueue(msg);
            //ProcessEvent(msg);
        }
        else
        {
            ILMsgCenter.Instance.SendToMsg(msg);
        }
    }



    public static void AddUIPanel(string resName, string bundleName, string sceneName, GameObject obj)
    {
        BaseList[resName].Awake();
        UINode node = new UINode(resName, bundleName, sceneName, obj, BaseList[resName]);
        sonMembers.Add(resName, node);
        //node.nodeBase.Init();
        //node.nodeBase.OnShow();
    }
    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <param name="path">UIPanelPath</param>
    /// <param name="level">UILevel</param>
    /// <param name="CallBackID">ILUIEventPanel</param>
    public static void ShowView(string path, UILevel level = 0, ILUIEventPanel CallBackID = 0)
    {
        string[] tmpPath = path.Split('/');

        if (!sonMembers.ContainsKey(tmpPath[2]))
        {
            ILHunkAssetRes load = new ILHunkAssetRes(true, (ushort)ILAssetEvent.GetResObjectUI, tmpPath[0], tmpPath[1], tmpPath[2], (ushort)CallBackID, (ushort)level);
            Instance.SendMsg(load);

            return;
        }

        sonMembers[tmpPath[2]].Obj.SetActive(true);
        sonMembers[tmpPath[2]].nodeBase.OnShow();
        //  Debug.Log("ShowView");
    }
    /// <summary>
    /// 关闭UI界面
    /// </summary>
    /// <param name="resName"></param>
    public static void HideView(string resName, bool autoDestrpy = true)
    {
        if (sonMembers.ContainsKey(resName))
        {
            sonMembers[resName].nodeBase.OnHide();
            sonMembers[resName].Obj.SetActive(false);
            // sonMembers[resName].OnHide();
            if (autoDestrpy)
            {
                DestoryUIObj(resName);
            }

        }
    }


    public static async void DestoryUIObj(string resName)
    {
        await Task.Delay(500);
        if (!Application.isPlaying)
        {
            return;
        }

        if (sonMembers.ContainsKey(resName) && sonMembers[resName].Obj.activeSelf == false && sonMembers != null)
        {
            sonMembers[resName].nodeBase.OnDestory();
            GameObject.Destroy(sonMembers[resName].Obj);

            //#if !UNITY_EDITOR
            ILHunkAssetRes asset = new ILHunkAssetRes(sonMembers[resName].sceneName, sonMembers[resName].bundleName, sonMembers[resName].resName, (ushort)ILAssetEvent.ReleaseSingleBundle);
            Instance.SendAwaitMsg(asset);
            ILHunkAssetRes assetobj = new ILHunkAssetRes(sonMembers[resName].sceneName, sonMembers[resName].bundleName, sonMembers[resName].resName, (ushort)ILAssetEvent.ReleaseBundleObj);
            Instance.SendMsg(assetobj);
            //#endif

            sonMembers.Remove(resName);

        }
    }


    public void Initialize()
    {
        //初始化数据工具
        ItemGlobalController item = new ItemGlobalController();
        item.Awake();
        //初始化自动冒险控制
        AdventureController adventureController = new AdventureController();
        adventureController.Awake();

        MainScenePanel main = new MainScenePanel();
        BaseList.Add("MainScenePanel", main);
        // main.Awake();

        //关卡选择
        AdventurePanel level = new AdventurePanel();
        BaseList.Add("AdventurePanel", level);
        // level.Awake();
        //仓库界面
        //StoragePanel storagePanel = new StoragePanel();
        //BaseList.Add("StoragePanel", storagePanel);

        //健康界面
        HealthPanel healthPanel = new HealthPanel();
        BaseList.Add("HealthPanel", healthPanel);
        //  healthPanel.Awake();

        //背包界面
        BagPanel bagPanel = new BagPanel();
        BaseList.Add("BagPanel", bagPanel);
        // bagPanel.Awake();
        //使用界面
        UseBagPanel useBagPanel = new UseBagPanel();
        BaseList.Add("UseBagPanel", useBagPanel);
        // useBagPanel.Awake();
        //战斗主界面
        FightMainPanel fightMainPanel = new FightMainPanel();
        BaseList.Add("FightMainPanel", fightMainPanel);
        // fightMainPanel.Awake();
        //地图界面

        // mapPanel = new MapPanel();
        //BaseList.Add("MapPanel", mapPanel);
        BuildPanel bulidPanel = new BuildPanel();
        BaseList.Add("BuildPanel", bulidPanel);
        //  bulidPanel.Awake();
        //战斗背包界面
        //FightBagPanel fightBagPanel = new FightBagPanel();
        //BaseList.Add("FightBagPanel", fightBagPanel);
        //战斗结算
        FightSettlementPanel fightSettlementPanel = new FightSettlementPanel();
        BaseList.Add("FightSettlementPanel", fightSettlementPanel);
        // fightSettlementPanel.Awake();
        //装备强化
        ModifiedPanel ModifiedPanel = new ModifiedPanel();
        BaseList.Add("ModifiedPanel", ModifiedPanel);
        // ModifiedPanel.Awake();
        //Loading界面
        LoadingPanel loadingPanel = new LoadingPanel();
        BaseList.Add("LoadingPanel", loadingPanel);
        loadingPanel.Awake();
        //提示消息界面
        TipPanel TipPanel = new TipPanel();
        BaseList.Add("TipPanel", TipPanel);
        // TipPanel.Awake();
        //HUD显示界面
        HudPanel HudPanel = new HudPanel();
        BaseList.Add("HUDPanel", HudPanel);
        //  HudPanel.Awake();
        //怪物显示详情界面
        DetailPanel DetailPanel = new DetailPanel();
        BaseList.Add("DetailPanel", DetailPanel);
        // DetailPanel.Awake();
        //出兵携带武将选择界面
        TroopPanel TroopPanel = new TroopPanel();
        BaseList.Add("TroopPanel", TroopPanel);
        // TroopPanel.Awake();
        //玩家每回合战斗结束获取的奖励物品界面
        FightRewardPanel FightRewardPanel = new FightRewardPanel();
        BaseList.Add("FightRewardPanel", FightRewardPanel);
        //FightRewardPanel.Awake();
        //设置界面
        SettingPanel SettingPanel = new SettingPanel();
        BaseList.Add("SettingPanel", SettingPanel);
        //  SettingPanel.Awake();
        //回合提示界面
        RoundPanel RoundPanel = new RoundPanel();
        BaseList.Add("RoundPanel", RoundPanel);
        // RoundPanel.Awake();
        //存档界面
        ArchivePanel ArchivePanel = new ArchivePanel();
        BaseList.Add("ArchivePanel", ArchivePanel);//
        //战利品浏览界面
        SpoilsPanel SpoilsPanel = new SpoilsPanel();
        BaseList.Add("SpoilsPanel", SpoilsPanel);
    }


}
