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

    //�����¼�����
    public static Queue<ILMsgBase> MsgQueue = new Queue<ILMsgBase>();
    //�洢UI�ؼ����ã��������
    public static Dictionary<string, UINode> sonMembers = new Dictionary<string, UINode>();
    public static Dictionary<string, ILUIBase> BaseList = new Dictionary<string, ILUIBase>();

    /// <summary>
    /// ִ�ж�Ӧģ���¼�
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
    /// ��һ֡��ִ���¼�
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
    /// ��UI����
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
    /// �ر�UI����
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
        //��ʼ�����ݹ���
        ItemGlobalController item = new ItemGlobalController();
        item.Awake();
        //��ʼ���Զ�ð�տ���
        AdventureController adventureController = new AdventureController();
        adventureController.Awake();

        MainScenePanel main = new MainScenePanel();
        BaseList.Add("MainScenePanel", main);
        // main.Awake();

        //�ؿ�ѡ��
        AdventurePanel level = new AdventurePanel();
        BaseList.Add("AdventurePanel", level);
        // level.Awake();
        //�ֿ����
        //StoragePanel storagePanel = new StoragePanel();
        //BaseList.Add("StoragePanel", storagePanel);

        //��������
        HealthPanel healthPanel = new HealthPanel();
        BaseList.Add("HealthPanel", healthPanel);
        //  healthPanel.Awake();

        //��������
        BagPanel bagPanel = new BagPanel();
        BaseList.Add("BagPanel", bagPanel);
        // bagPanel.Awake();
        //ʹ�ý���
        UseBagPanel useBagPanel = new UseBagPanel();
        BaseList.Add("UseBagPanel", useBagPanel);
        // useBagPanel.Awake();
        //ս��������
        FightMainPanel fightMainPanel = new FightMainPanel();
        BaseList.Add("FightMainPanel", fightMainPanel);
        // fightMainPanel.Awake();
        //��ͼ����

        // mapPanel = new MapPanel();
        //BaseList.Add("MapPanel", mapPanel);
        BuildPanel bulidPanel = new BuildPanel();
        BaseList.Add("BuildPanel", bulidPanel);
        //  bulidPanel.Awake();
        //ս����������
        //FightBagPanel fightBagPanel = new FightBagPanel();
        //BaseList.Add("FightBagPanel", fightBagPanel);
        //ս������
        FightSettlementPanel fightSettlementPanel = new FightSettlementPanel();
        BaseList.Add("FightSettlementPanel", fightSettlementPanel);
        // fightSettlementPanel.Awake();
        //װ��ǿ��
        ModifiedPanel ModifiedPanel = new ModifiedPanel();
        BaseList.Add("ModifiedPanel", ModifiedPanel);
        // ModifiedPanel.Awake();
        //Loading����
        LoadingPanel loadingPanel = new LoadingPanel();
        BaseList.Add("LoadingPanel", loadingPanel);
        loadingPanel.Awake();
        //��ʾ��Ϣ����
        TipPanel TipPanel = new TipPanel();
        BaseList.Add("TipPanel", TipPanel);
        // TipPanel.Awake();
        //HUD��ʾ����
        HudPanel HudPanel = new HudPanel();
        BaseList.Add("HUDPanel", HudPanel);
        //  HudPanel.Awake();
        //������ʾ�������
        DetailPanel DetailPanel = new DetailPanel();
        BaseList.Add("DetailPanel", DetailPanel);
        // DetailPanel.Awake();
        //����Я���佫ѡ�����
        TroopPanel TroopPanel = new TroopPanel();
        BaseList.Add("TroopPanel", TroopPanel);
        // TroopPanel.Awake();
        //���ÿ�غ�ս��������ȡ�Ľ�����Ʒ����
        FightRewardPanel FightRewardPanel = new FightRewardPanel();
        BaseList.Add("FightRewardPanel", FightRewardPanel);
        //FightRewardPanel.Awake();
        //���ý���
        SettingPanel SettingPanel = new SettingPanel();
        BaseList.Add("SettingPanel", SettingPanel);
        //  SettingPanel.Awake();
        //�غ���ʾ����
        RoundPanel RoundPanel = new RoundPanel();
        BaseList.Add("RoundPanel", RoundPanel);
        // RoundPanel.Awake();
        //�浵����
        ArchivePanel ArchivePanel = new ArchivePanel();
        BaseList.Add("ArchivePanel", ArchivePanel);//
        //ս��Ʒ�������
        SpoilsPanel SpoilsPanel = new SpoilsPanel();
        BaseList.Add("SpoilsPanel", SpoilsPanel);
    }


}
