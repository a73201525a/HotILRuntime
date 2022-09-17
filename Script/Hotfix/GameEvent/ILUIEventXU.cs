using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ILUIEventBag
{

    GetItemAll = ILUIEvent.maxValue + 1,//��ȡ����˵�������
    GetFightItemAll,//ս����ȡ����Ʒ
    GetFightItemType,//ս��ĳ�����͵���Ʒ
    LoadSpriteAll,//����ͼ������ͼƬ
    GetGoldDataItemBack,//�õ��������,
    GetSettingDataItemBack,//�õ���ǰ�������
    LoadUseWeapon,//����ʹ�ý���������ʾ
    LoadFightUseWeapon,//����ʹ�ý���������ʾ

    UpdateEquipment,//����װ��

    UpdateEquipmentByPlayerJson,//�������װ��  
    UpdateBagByPlayerJson,//������ұ��� 
    UpdateBallByPlayerJson,//�����������
    RemoveBagItem,//�Ƴ�Bag��Ʒ����
   // AddBagItem,//���ӱ�������
    RemoveFightBagItem,//�Ƴ�Bag��Ʒ����

    SaveFightItem,//ս����Ʒ����
    AddFightWorldItem,//���ս��ʰȡ��Ʒ
    GetFightWorldALLItem,//��ȡս������������Ʒ
    UpdateFightBulletData,//�����ӵ�
    GetFightMaterialDataByID,//ͨ��id��ȡ����item
    UpdateFightItemData,//���»�������
    GetFightPlayerID,//�õ���ǰս�����id
                     // GetMianPlayerID,//�õ���ǰ���������id
    GetPlayerBack,//��ȡ���
 
    maxValue,
}

/// <summary>
/// ����
/// </summary>
public enum ILUIEventBuild
{

    LoadAllSprite = ILUIEventBag.maxValue + 1,//��������ͼƬ
    LoadItemSprte,//���ص���Itemͼ��
 
    CloseItem,//�ر� item

    GetCurPlayerBtnAttack,//��ǰ������ҹ���״̬
    GetPlayer,//��ǰ�������
    ShowSkill,//��ʾUI����
    ShowProp,//��ʾUI���� 
    Showbuild,//��ʾUI������Ʒ
    UpdatewPropItem,//���µ�����Ʒ
    SkillFinishBack,//�����ͷ���ɻص�
    BuildFinishBack,//�������
    SkillUpdateCDTime,//���ܸ���CD��ʱ
    UpdateBagByPlayerJson,//�õ���ҵ���
    maxValue,
}
/// <summary>
/// ��ǰս���������ｱ������
/// </summary>
public enum ILUIEventCurrentFightEndRound
{
    LoadAllSprite = ILUIEventBuild.maxValue + 1,//��������ͼƬ
    LoadItemSprite,//����item��Ʒ
    ShowRewardItem,//��ʾ������Ʒ
    FightEnd,
    maxValue,
}
/// <summary>
/// ���ý�����Ϣ�¼�
/// </summary>
public enum ILUIEventSetting
{
    LoadAllSprite = ILUIEventCurrentFightEndRound.maxValue + 1,//��������ͼƬ
    
    GetSettingJsonBack,//��ȡϵͳ��������
    maxValue,
}
/// <summary>
///����
/// </summary>
public enum ILUIEventSettlement
{
    LoadSettlementAllSprite = ILUIEventSetting.maxValue+1,//��������ͼƬ
   
    SettlementItem,//��ȡ������Ʒ  
    SettlementSuccess,//ս���ɹ�����
    GetPlayerBack,//��ȡ��ɫ����
    GetRewardItemBack,//��ȡս��Ʒ
    maxValue,
}
/// <summary>
/// ս��ʱ,������Ϣ
/// </summary>
public enum ILUIEventPlayerPanel
{
    LoadAllSprite = ILUIEventSettlement.maxValue + 1,//��������ͼƬ

    UpdateItem,//��ȡ���Я�������Ʒ  
    UpdataPlaerJson,//�����������
    HidePlayerData,//�������������ʾ
    HideMonsterCommon,//���ع���ͨ������
    ShowMonsterDetail,//��ʾ���ﵱǰϸ��
    HideBuildCommon,//���ؽ���ͨ������
    ShowBuildDetail,//����ϸ��������ʾ
    HideBuildDetail,//���ؽ���ϸ������
    maxValue,
}
//ð��
public enum ILUIAdventureEvent
{
    LoadAdventure = ILUIEventPlayerPanel.maxValue+1,//���عؿ�
    ReadFileAdventure,//��ȡ�ؿ�
    AdventureEnd,//�����ؿ�
    ShowWinCondition,//��ʾʤ������ 
    ShowRoundText,//��ʾ�غ���
    AdventureAngin,//�ؿ�����һ��
    GetAdventureID,//��ȡ��ǰ�ؿ�  
    GetPlayerBallItem,//��ȡ��ǰ�ؿ��������Я������
    AdventureWin,//�ؿ�ʤ��
    GetPlayerJsonBack,//��ȡ�������
    maxValue,
}
/// <summary>
/// ѡ����������������
/// </summary>
public enum ILUIEventTroop
{
    LoadAllSprite = ILUIAdventureEvent.maxValue+1,//��������ͼƬ
    GetAllPlayerJsonBack,//�õ������������
    GetAdventureID,//�õ��ؿ�id
    GetMainPlayerBack,//���س����������
    LoadModel,//����ģ��
    maxValue,
}
/// <summary>
/// �浵
/// </summary>
public enum ILUIEventArchive
{
    LoadAllSprite = ILUIEventTroop.maxValue+1,//��������ͼƬ
    SaveArchiveShow,//�浵��Ϣ
    maxValue,
}
//ս��Ʒ
public enum ILUIEventSpoils
{
    LoadAllSprite= ILUIEventArchive.maxValue+1,//����ͼ��
    GetSpoilsItemJsonBack,//��ȡս��Ʒ����
    maxValue,
}
/// <summary>
/// ��������
/// </summary>
public enum ILUIEventSKill
{
    LoadAllSprite= ILUIEventArchive.maxValue+1,//����ͼ��
    GetSkillItem,//��ȡ��Ҽ�������
    maxValue,
}