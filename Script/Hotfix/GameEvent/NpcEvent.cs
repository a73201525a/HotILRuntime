using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ILNpcEvent
{
    LoadPlayer = ILManagerID.NpcManager + 1,
    //�����ӵ�
    LoadBullet,
    LoadBulletEf,


    CreartBullet,//�����ӵ�
    BulletInit,//�ӵ���ʼ��

    UnLoadAllBullet,//ж���ӵ�
    UnLoadSceneBullet,//ж��С�����ӵ�

    OnFireAreaDownEvent,
    OnFireAreaMoveEvent,
    OnFireAreaUpEvent,
    LoadCircle,
    LoadCube,
    LoadSector60,
    LoadSector120,
    LoadEquipment,//����װ��
    LoadEquipmentBody,//����װ�����
    LoadSingleEquipment,//���ص���װ��
    LoadGunPart,//����ǹ����
    //�ƶ�����
    CreatPlayer,
    PlayerInitEnd,//��Ҽ��ؽ���
    PlayerMove,
    PlayerBenginMove,
    PlayerEndMove,
    PlayerIsStopMove,
    PlayerIsRunState,
    PlayerTurn,
    PlayerFireTurn,
    PlayerFire,
    RemoverMainPlayer,//����ս�� ɾ������main������ɫ
    GetAllMainPlayer,//��ȡmain�����Ľ�ɫ
    RemoverPlayerSingle,//ɾ���������
    ChangeEquipment,//�л�����װ��
    GetFireDownPointCube,//��ȡ�����
    GetFireDownPointCircle,
    GetFireDownPointBack,


    GetPlayerObjectBack,//��ȡ��һص�
    GetPlayerGameObjectBack,//�����ȡ��һص�

    GetPlayerDataBack,//��ȡ��ұ�������
    PlayerDataBack,//��ȡ���Json���ݻص�
    PlayerEquipmentBack,//��һ�ȡ���д�����Ʒ
    PlayerGunDataBack,//ǹ����
    UpdatePlayer,//�������
    ShowPlayerUI,//��ʾ��Ҳ���UI
    GetPlayerMaxRound,//�õ�������غ���
    //����
    LoadMonster,
    LoadMonserComb,//���ع������
    CreatMonster,//��������

    PointEntrer,//��������¼�
    RemoverDeathMonster,//ɾ�������Ĺ���
    GetALlMonster,//�õ����й���
    GetALlMonsterAndDeath,//�õ����й���(�����Ѿ����˵�)
    GetAllMonsterBack,//��һ�ȡ���й���
    GetAllBuildBack,//�����ȡ���н���
    LoadMainPlayer,//���������
    PlayerUseProp,//���ʹ�õ��� 
    SkillBall,//�����������ս��

    PlayerUpdatePlayData,//������ݸ���
    PlayerByAttack,//��ұ�����
    PlayerBySkill,//��ұ����ܹ���
    MonsterStartRound,//����غϼ������߿�ʼ
    MonsterByAttack,//���ﱻ���� 
    MonsterCounterAttack,//���ﷴ��
    MonsterBySkill,//���ﱻ��������
    GetAllPlayer,//�õ��������
    GetALlPlayerAndDeath,//�õ��������(�����Ѿ����˵�)
    GetAllPlayerBack,//�õ�������һص�
    GetPlayerAllBuildBack,//��ҵõ����н���
    DestoryAllPlayer,//ɾ���������


    GetPlayerByID,//��id�õ����
    PlayerEnterFight,//��ҽ���ս��
    GetClickPlayerRound,//�õ�����Ƿ�ȫ�������غ�
    PlayerCounterAttack,//��ҷ���
    UpdatePlayerOperation,//������Ҳ�������

    PlayerRoundStart,//��һغϿ�ʼ
    PlayerRoundEnd,//��ҵ�ǰ��������
    PlayerRoundAllEnd,//��һغϽ���
    PlayerCancel,//ȡ����Ҳ���  
    PlayerUseSkillID,//��ҵ��ʹ�õļ���id
    PlayerForcedToCancel,//ǿ��ȡ��

    SavePlayerjson,//ս��ʤ�������������

    InitFitgt,//����ؿ�����ʼ��
    GameOver,//��Ϸ����
    GetRound,//��ȡ�غ�
    LoadGetRoundBack,//������ȡ�غ�
    LoadSaveMapDataEnd,//����������ö�������
    maxValue,

}

public enum ILNpcEventMap
{
    CreatMap = ILNpcEvent.maxValue + 1,

    HideMapTip,//������ʾ
               //  ShowSkillTip,//��ʾ����
    SetObs,//�����ϰ�
    HideAttckTip,//���ع�����ʾ
    ShowMapTip,//��ʾ�ƶ���ʾ
    Rest,//����
    GetMonsterTipHex,//�õ�������ʾ��ʾ
    ShowAttackTip,//��ʾ���﹥����ʾ
    ShowAttackTargetTip,//��ʾѰ�ҵ�AI����ʾ
    UpdateMap,//���µ���
    maxValue,
}

public enum ILNpcSKill
{
    LoadSkill = ILNpcEventMap.maxValue + 1,//���ؼ���
    CreatSkill,//ʹ�ü���
    LoadSingleSkill,//���ص�������
    InitSkill,//��ʼ��������м���
    maxValue,
}

public enum ILNpcBall
{
    LoadBallBack = ILNpcSKill.maxValue + 1,//���ص���
    LoadWallBallBack,//����ǽ����
    EnterFight,//����ս��
    GetBallItem,//��ȡ����
    maxValue,
}

public enum ILNpcBuff
{
    ActiveBuff = ILNpcBall.maxValue + 1,//����buff
    RemoveCounter,//��ؼ���
    RoundEnd,//�غϽ���
    RoundStart,//�غϿ�ʼ
    GetAllBuff,//��ȡ����buff
    GetDealtailAll,//��ȡ����buff����
    NpcDeath,//Npc����
    LoadBuff,//����buff
    maxValue,
}

public enum ILNpcSaveMap
{
    GetMonsterBack = ILNpcBuff.maxValue + 1,//��ȡ��������
    GetPlayerBack,//��ȡ�������
    GetBuildBack,//��ȡ��������
    GetBuffBack,//��ȡbuff
    GetRewardItem,//��ȡ�Ѿ���õĽ�����Ʒ
    GetAdventureIDBack,//��ȡ��ǰ�ؿ�ID
    SetPlayerData,//������� ����
    SetMonsterData,//���ù��� ����
    SetBuildData,//���ý��� ����
    maxValue,
}

