using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ӦUI����ģ��
/// </summary>
/// ����UIģ�����ֶ����
public enum ILUIEventPanel
{
    LoadPanel = ILManagerID.UIManager + 1,

    MainScenePanel,
    TipPanel,//��Ϣ��ʾ

    HUDPanel,//HUDҳ��
    DetailPanel,//��ʾ����ҳ��
    TroopPanel,//����ѡ�����
    LoadingPanel,//����ҳ��
    AdventureMainPanel,//ѡ��ؿ�UI
    RoundPanel,//�غ���ʾ����
    HealthPanel,//��������
    BagPanel,//��������
    UseBagPanel,//UseBag
    ModifiedPanel,//��ǹ
    SettingPanel,//���ý���
    //EquipmentPanel,//

    FightMainPanel,//ս��������
    FightBagPanel,//ս������
    // FightEquipmentPanel,
    FightRewardPanel,//ս��������ȡ����
    FightSettlementPanel,//ս���������

    MapPanel,//��ͼ����
    BuildPanel,//��������
    ArchivePanel,//�浵����
    SpoilsPanel,//ս��Ʒ����
    
    maxValue,

}

public class UIPanelPath
{
    public static string MainScenePanel = "scenestwo/ui/MainScenePanel";
    public static string LevelMainPanel = "scenestwo/ui/AdventurePanel";
    public static string StorageMainPanel = "scenestwo/ui/StoragePanel";
    public static string HealthPanel = "scenestwo/ui/HealthPanel";

    public static string FightMainPanel = "scenestwo/ui/FightMainPanel";
    public static string FightBagPanel = "scenestwo/ui/FightBagPanel";
    public static string FightEquipmentPanel = "scenestwo/ui/FightEquipmentPanel";
    public static string FightSettlementPanel = "scenestwo/ui/FightSettlementPanel";
    public static string ModifiedPanel = "scenestwo/ui/ModifiedPanel";
    public static string LoadingPanel = "scenestwo/ui/LoadingPanel";
    public static string BagPanel = "scenestwo/ui/BagPanel";
    public static string UsePanel = "scenestwo/ui/UseBagPanel";
    public static string BuildPanel = "scenestwo/ui/BuildPanel";
    public static string TipPanel = "scenestwo/ui/TipPanel";
    public static string HUDPanel = "scenestwo/ui/HUDPanel";
    public static string DetailPanel = "scenestwo/ui/DetailPanel"; 
    public static string TroopPanel = "scenestwo/ui/TroopPanel"; 
    public static string FightRewardPanel = "scenestwo/ui/FightRewardPanel";  
    public static string SettingPanel = "scenestwo/ui/SettingPanel"; 
    public static string RoundPanel = "scenestwo/ui/RoundPanel";
    public static string ArchivePanel = "scenestwo/ui/ArchivePanel"; 
    public static string SpoilsPanel = "scenestwo/ui/SpoilsPanel"; 
   

}
public enum ILUIEvent
{
    LoadHero = ILUIEventPanel.maxValue + 1,
    CardFinish,
    CardClick,
    TipGridFinish,
    ItmeGridFinish,
    UpdateSelectPanel,
    ShowDragItem,
    HideDragItem,
    RaodFinish,
    SetObjectParent,
    GetMonsterMovePath,

    LoadMainScene,//Mian���� 
    LoadSaveMainScene,//����Mian�����浵

    CreatDetailBuildItem,//������������
    CreatDetailMosnterItem,//����������������
    CreatDetailPlayerItem,//���������������
    RemoveDetailItem,//�Ƴ�������Item
    DetailBuff,//��ʾbuff

    LoadingSceneStart,//Lodaing����
    SetLoadingProgress,//���ý���������
    LoadingComplete,//�������


    LoadAllUseSprite,//��������ʹ�ý���ͼƬ
    LoadGunPart,//����ʹ��ǹ����
    UsedItem,//ʹ����Ʒ
    UpdateEquipmentItemData,//����ҳ������
    UsedFightItem,//ʹ��ս����������
    RemoveFightItem,//�Ƴ�ս������


    LoadFightSettlementItem,//���ؽ�����Ʒ
    LoadFightSettlementIcon,//������Ʒͼ��
    GetFightNewAllItemBack,//�õ�ս��������Ʒ���ݻص�

    PlayerLoadEndBack,//��Ҽ������

    SaveItemMap,//�浵
    LoadItemMap,//����

    AddItem,//�����Ʒ
    UpdateItemAll,//���±�������
    UpdatePlayerData,//�����������
    UpdateFightDataBack,//����ս������
   // UpdateWoodDataBack,//ľ�ĸ��·ַ���������Ҫ����ľ�ĵ�λ��
   // UpdateStoneData,//����ʯ������
    UpdateVolume,//��������
 //   UpdateStoneDataBack,//ʯ�ĸ��·ַ���������Ҫ����ʯ�ĵ�λ��
    UpdateSetting,//����ϵͳ����
    SaveItem,//������Ʒ����

    GetPlayerData,//�õ��������
    GetAllPlayerJson,//�õ������������
    AddPlayerJson,//����������
    GetItemAll,//��ȡ���е���
    GetGoldItem,//��ȡ���
    //GetWoodItem,//��ȡľ��
    //GetStoneItem,//��ȡʯ��
    GetItemByType,//��ȡ������Ӧ������Ʒ
    GetItemByUniqueIDArray,//����Ψһid����,��ȡ��Ӧ������Ʒ
    GetItemByUniqueID,//����Ψһid��ȡ��Ӧ��Ʒ����
    GetSettingData,//��ȡ��������
    GetSettingDataBack,//��ȡ�������ݻص�
    GetMainSettingData,//��ȡ����������

    InitGunModified,//����ǿ����ǹе

    LoadModifeGun,//���ػ���ǿ����Ʒ
    LoadModifedIcon,//����ͼƬ
    MosdifedItemData,//ǿ��װ������
    LoadMosdifedItem,//�򿪿�ʹ�ý���
    ModifiedGunJson,//GunJson����
    LoadOneMosdifedModel,//����һ��ǿ����Ʒģ��



    LoadFightMainSprite,//����ս��������ͼ��

    LoadHealthPulsSprite,//���ؽ���Ѫ��ͼ��

  
    PlayerHealthInit,//��ҽ��������ʼ���ݻ�ȡ
    PlayerHealthBack,//��ҽ��������ȡ����
    ShowHealthItem,//��ʾ��������
    PlayerRoundBack,//���ս���غ��Ƿ����
    FightMainItemBack,//���ս����������
    PlayerFightEquimentBack,//���װ����ȡ�������

    FightMainInit,//��ʼ��ս��������
    FightMainInitWoodBack,//��ʼ��ľ������ 
    FightMainInitStoneBack,//��ʼ��ʯ������
    FightMainUpdateItem,//��������Item
    FightMianProhibitionRound,//���ûغ�
    ShowRoundNumber,//��ʾ�غ���



    LoadHealthSprite,//��������,��������ͼ������
    LoadMapTexture,//��ͼͼƬ
    GetMapPlayer,//��ͼ��������
    MapInit,//��ͼ��ʼ
    PlayerBack,
    PlayerMainPanelCreatBack,//�����洴����������Ϣ�ص�
    //tip��Ϣ
    ShowTipText,//��ʾ��Ϣ�ı�

    //HUDPanel��Ϣ
    CreatHUDBlood,//����Ѫ��
    RemoveHUDBlood,//�Ƴ�
    ReduceBlood,//HUD��Ѫ
    AddBlood,//HUD��Ѫ
    HideHUDBlood,//����Ѫ��
    ShowHUDMpTip,//��ʾHUD �����ı�
    HideHUDTip,//����HUD��ʾ�ı�
    ShowHUDTip,//��ʾHUD��ʾ�ı�
    ShowHUDBlood,//��ʾHUD����Ѫ��
    ShowHUDFightDamage,//��ʾս���˺��ı�
    ShowHUDDynamicTip,//��ʾ��̬����
    maxValue,
}



public enum ILUILevel
{
    Dwon = 0,
    Two,
    Three,
    Four,
    Five,
    Six,
    Top,
}
/// <summary>
/// ������Ϣ�ص�
/// </summary>
public class LoadingMsg : ILMsgBase
{
    public ushort callBack;//���ػص�
    public string loadingName;//���س�������
    public Action endAction;//�������ִ��
    public float curValue;//��ǰ����
    public float maxValue;//������
    /// <summary>
    /// ���õ�ǰ����
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="curValue"></param>
    public LoadingMsg(ushort msgid, float curValue, float maxValue = 1f)
    {
        this.msgID = msgid;
        this.curValue = curValue;
        this.maxValue = maxValue;
    }
    /// <summary>
    /// ��ʼ������
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="loadingValue"></param>
    public LoadingMsg(ushort msgid, string loadingName, Action endAction)
    {

        this.loadingName = loadingName;
        this.msgID = msgid;
        this.endAction = endAction;
    }
    /// <summary>
    /// ���ȼ��ػص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="callBack"></param>
    public LoadingMsg(ushort msgID, ushort callBack)
    {
        this.msgID = msgID;
        this.callBack = callBack;
    }
}

public class ItemMsg : ILMsgBase
{
    public ChangeItem changeItemType;//�ı�����
    public ItemJson item;//��Ʒ
    public List<ItemJson> itemList;//��Ʒ
    public int index;//����
    public int newIndex;//������
    public int Type;//��Ʒ����
    public ushort CallBack;
    public int[] UniqueIDArray;//Ψһid����
    public int UniqueID;//��ƷΨһ��ʾ

    public string SavePath;//�浵·��
    public ItemMsg(ushort msgid, string SavePath)
    {
        this.msgID = msgid;
        this.SavePath = SavePath;

    }

    /// <summary>
    /// �޸���Ʒ����
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="type"></param>
    /// <param name="item"></param>
    /// <param name="index">�ո�������</param>
    public ItemMsg(ushort msgid, ChangeItem type, ItemJson item, int index)
    {
        this.msgID = msgid;
        this.item = item;
        this.index = index;
        this.changeItemType = type;
    }

    /// <summary>
    /// ɾ��Item
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="type"></param>
    /// <param name="UniqueID"></param>
    public ItemMsg(ushort msgid, ChangeItem type, int UniqueID)
    {
        this.msgID = msgid;
        this.UniqueID = UniqueID;
        this.changeItemType = type;

    }
    /// <summary>
    /// ������ӵ�����Ϣ
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="item"></param>
    public ItemMsg(ushort msgid, ItemJson item, ChangeItem type)
    {
        this.msgID = msgid;
        this.item = item;
        this.changeItemType = type;
    }
    /// <summary>
    /// ����Ψһid�����ȡ��Ʒ����
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="UniqueID"></param>
    /// <param name="CallBack"></param>
    public ItemMsg(ushort msgid, int[] UniqueIDArray, ushort CallBack)
    {
        this.msgID = (ushort)msgid;
        this.UniqueIDArray = UniqueIDArray;
        this.CallBack = CallBack;
    }
    /// <summary>
    /// �õ���������
    /// </summary>
    /// <param name="items"></param>
    /// <param name="msgid"></param>
    public ItemMsg(ushort msgid, ushort CallBack)
    {
        this.msgID = msgid;
        this.CallBack = CallBack;
    }
    /// <summary>
    ///����Ψһid��ȡ��Ʒ
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="UniqueID"></param>
    /// <param name="CallBack"></param>
    public ItemMsg(ushort msgid,int UniqueID,  ushort CallBack)
    {
        this.msgID = msgid;
        this.UniqueID = UniqueID;
        this.CallBack = CallBack;
    }

    public ItemMsg(List<ItemJson> itemJsons,  ushort msgid)
    {
        this.msgID = msgid;
        this.itemList = itemJsons;
    }

}

public class ItemMsgBack : ILMsgBase
{
    public List<ItemJson> ItemData;//��Ʒ����

    public ItemJson tmpItem;//��������
    /// <summary>
    /// ����ͬ������Ʒ����
    /// </summary>
    public ItemMsgBack(List<ItemJson> ItemData, ushort CallBack)
    {
        this.msgID = CallBack;
        this.ItemData = ItemData;
    }

    /// <summary>
    /// ���󵥸���Ʒ
    /// </summary>
    /// <param name="tmpItem"></param>
    /// <param name="CallBack"></param>
    public ItemMsgBack(ItemJson tmpItem, ushort CallBack)
    {
        this.msgID = CallBack;
        this.tmpItem = tmpItem;
    }
}
public class ModifiedMsg : ILMsgBase
{
    public ItemJson item;//��Ʒ����
    public ushort CallBack;
    /// <summary>
    /// �õ���ǹ��λ����
    /// </summary>
    /// <param name="msgid"></param>
    /// <param name="item">ǹ��Ʒ</param>
    public ModifiedMsg(ushort msgid, ItemJson item)
    {
        this.msgID = msgid;
        this.item = item;
    }
}

/// <summary>
/// ս����������Ϣ
/// </summary>
public class FightMainMsg : ILMsgBase
{
    public ItemJson GunItem;//��������Ʒ

    public ushort CallBack;//CallBack
    public int currentItemIndex;//��ǰ��Ʒ����
    public bool prohibitionRound;//��ֹ�غ�
    public int[] PlayerID;//���id
    public int Exp;//����
    public int Gold;//���
    public int RoundNumber;//�غ���
    /// <summary>
    /// ���»غ���
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="RoundNumber"></param>
    public FightMainMsg(ushort msgID, int RoundNumber)
    {
        this.msgID = msgID;
        this.RoundNumber = RoundNumber;
    }
    /// <summary>
    /// �ص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="currentItemIndex"></param>
    public FightMainMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;

    } 
    /// <summary>
    /// ���ð�ť
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="prohibitionRound"></param>
    public FightMainMsg(ushort msgID, bool prohibitionRound)
    {
        this.msgID = msgID;
        this.prohibitionRound = prohibitionRound;

    }


}
public class FightMainMsgBack : ILMsgBase
{
    public int[] PlayerID;//���id
    /// <summary>
    /// �������id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="PlayerID">���id</param>
    public FightMainMsgBack(ushort msgID, int[] PlayerID)
    {
        this.PlayerID = PlayerID;
        this.msgID = msgID;
    }
}
public class MainMsg:ILMsgBase
{
   
    public ushort CallBack;//�ص�
    /// <summary>
    /// ��Ϣ�ص�
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public MainMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID;
        this.CallBack = CallBack;

    }
}
public class MainMsgBack:ILMsgBase
{
    public int[] playerUniqueID;//���Ψһid
    public List<MainScenePlayer> PlayerList;//�������������
    /// <summary>
    /// ��ȡ���������
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="playerUniqueID">���Ψһid</param>
    public MainMsgBack(ushort msgID, List<MainScenePlayer> PlayerList)
    {
        this.msgID = msgID;
        this.PlayerList = PlayerList;

    }
}

/// <summary>
/// ������Ϣ
/// </summary>
public class HealthMsg : ILMsgBase
{
    public PlayerBase  Player;//���
    public ItemJson  ItemJson;//��Ʒ
  
    /// <summary>
    /// ������ȡ���id
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="PlayerID">���id</param>
    public HealthMsg(ushort msgID, PlayerBase Player)
    {
        this.msgID = msgID;
        this.Player = Player;
    } 
    /// <summary>
    /// ��ȡ��Ʒ
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="ItemJson"></param>
     public HealthMsg(ushort msgID, ItemJson ItemJson)
    {
        this.msgID = msgID;
        this.ItemJson = ItemJson;
    } 
    
}

public class SettingMsg:ILMsgBase
{
    public SetingJson setingJson;//�������� 
    public ushort CallBack;//�ص�
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="setingJson"></param>
    /// <param name="msgID"></param>
    public SettingMsg(SetingJson setingJson,ushort msgID)
    {
        this.setingJson = setingJson;
        this.msgID = msgID;
    }
    /// <summary>
    /// ��ȡϵͳ��������
    /// </summary>
    /// <param name="setingJson"></param>
    /// <param name="msgID"></param>
    /// <param name="CallBack"></param>
    public SettingMsg(ushort msgID, ushort CallBack)
    {
        this.msgID = msgID; 
        this.CallBack = CallBack;
    }
}
public class SettingBackMsg:ILMsgBase
{
    public SetingJson setingJson;//��������
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="setingJson"></param>
    /// <param name="msgID"></param>
    public SettingBackMsg(SetingJson setingJson,ushort msgID)
    {
        this.setingJson = setingJson;
        this.msgID = msgID;
    }
}


