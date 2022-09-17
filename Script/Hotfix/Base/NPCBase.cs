using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class NPCBase : ILMonoBase
{
    public ushort[] msgIds;
    /// <summary>
    /// buffЧ��
    /// </summary>
    public GameObject[] buffArray;

    public int NpcID;//��ɫ��id

    public Transform transform;

    /// <summary>
    /// ����
    /// </summary>
    public bool isDeath;

    //����buff������
    public int buffNumberControll = 0;
    /// <summary>
    /// �Ƿ񱻿��� �޷��ƶ� Ҳ�޷�����
    /// </summary>
    public bool isControll;


    //buff��е��������
    public int buffNumberIsAttack = 0;
    /// <summary>
    /// �Ƿ񱻽�е �����ƶ� �����Թ���
    /// </summary>
    public bool isAttack;
    //buff�����ƶ���������
    public int buffNumberIsMove = 0;
    /// <summary>
    /// �������ƶ������ǿ��Թ���
    /// </summary>
    public bool isMove;


    public void RegistSelf(ILMonoBase mono, params ushort[] msgs)
    {
        ILNpcManager.Instance.RegistMsg(mono, msgs);
    }

    public void UnRegistSelf(ILMonoBase mono, params ushort[] msgs)
    {
        ILNpcManager.Instance.UnRegistMsg(mono, msgs);
    }

    /// <summary>
    /// ���������ű�����
    /// </summary>
    /// <param name="msg"></param>
    public void SendMsg(ILMsgBase msg)
    {
        ILNpcManager.Instance.SendMsg(msg);
    }

    ILMsgBase msg;
    public void SendMsg(ushort msgid)
    {
        if (msg == null)
        {
            msg = new ILMsgBase(msgid);
        }
        else
        {
            msg.msgID = msgid;
        }

        ILUIManager.Instance.SendMsg(msg);
    }
    public void SendAwaitMsg(ILMsgBase msg)
    {
        ILNpcManager.Instance.SendAwaitMsg(msg);
    }

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {

    }

    public void HideGameObject()
    {
        transform.gameObject.SetActive(false);
    }

    public void Destroy(GameObject go)
    {
        OnDestroy();
        GameObject.Destroy(go);
    }

    public virtual void OnDestroy()
    {
        if (msgIds != null)
        {
            UnRegistSelf(this, msgIds);
        }
    }

    /// <summary>
    /// ���buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public void AddBuff(Buff tmpBuff)
    {
        HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.ShowHUDTip, NpcID, tmpBuff.name);
        SendMsg(hUDMsg);

    }
    /// <summary>
    /// �Ƴ�Buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public void RemoveNPCBuff(Buff tmpBuff)
    {


    }

    /// <summary>
    /// �ж�
    /// </summary>
    /// <param name=""></param>
    public virtual void Poison(Buff tmpBuff, bool isStart = false)
    {
        Debug.Log("����ж�buff��isStart::" + isStart);
        buffArray[tmpBuff.buffIndex].SetActive(true);
    }
    /// <summary>
    /// �Ƴ��ж�
    /// </summary>
    public virtual void RemovePoison(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void AddControll(bool isAddBuff = true)
    {
        if (!isAddBuff)
        {
            return;
        }
        buffNumberControll++;
        Debug.Log("buffNumberControll" + buffNumberControll);
        isControll = true;
    }
    /// <summary>
    /// �������
    /// </summary>
    public void CancelControll()
    {
        Debug.Log("CancelControll::kongzhi buff::" + buffNumberControll);
        buffNumberControll--;

        if (buffNumberControll == 0)
        {
            isControll = false;
        }
        Debug.Log("=============================isControll" + isControll);
        Debug.Log("buffNumberControll" + buffNumberControll);
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void AddControllAttack(bool addAttack = true)
    {
        if (!addAttack)
        {
            return;
        }
        buffNumberIsAttack++;
        Debug.Log("buffNumberIsAttack" + buffNumberIsAttack);
        isAttack = true;
    }
    /// <summary>
    ///ȡ�����ù���
    /// </summary>
    public void CancelControllAttack()
    {
        buffNumberIsAttack--;
        if (buffNumberIsAttack == 0)
        {
            isAttack = false;
        }
        Debug.Log("=============================isAttack" + isAttack);
    }
    /// <summary>
    /// ���������ƶ�
    /// </summary>
    public void AddControllMove(bool isAddBuff = true)
    {
        if (!isAddBuff)
        {
            return;
        }
        buffNumberIsMove++;
        Debug.Log("buffNumberIsMove" + buffNumberIsMove);
        isMove = true;
    }
    /// <summary>
    /// ȡ�������ƶ�
    /// </summary>
    public void CancelControllMove()
    {
        buffNumberIsMove--;
        if (buffNumberIsMove == 0)
        {
            isMove = false;

        }
        Debug.Log("==============CancelControllMove===============isMove===============" + isMove);
    }


    /// <summary>
    ///ѣ��
    /// </summary>
    /// <param name="nPCBase"></param>
    public virtual void Vertigo(Buff tmpBuff, bool isAddBuff)
    {
        AddControll(isAddBuff);

        buffArray[tmpBuff.buffIndex].SetActive(true);

    }

    /// <summary>
    /// ȡ��ѣ��
    /// </summary>
    /// <param name="buffGameObject"></param>
    public virtual void RemoveVertigo(Buff tmpBuff)
    {
        CancelControll();
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }


    /// <summary>
    /// ��е
    /// </summary>
    /// <param name="tmpBuff">buff </param>
    /// <param name="isAddBuff">�Ƿ����</param>
    public virtual void Disarm(Buff tmpBuff, bool isAddBuff)
    {
        AddControllAttack(isAddBuff);
        buffArray[tmpBuff.buffIndex].SetActive(true);
    }

    /// <summary>
    /// ȡ����е
    /// </summary>
    public virtual void RemoveDisarm(Buff tmpBuff)
    {
        CancelControllAttack();
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="tmpBuff"></param>
    /// <param name="isAddBuff"></param>
    public virtual void DeformationSheep(Buff tmpBuff, bool isAddBuff)
    {
        AddControll(isAddBuff);
        buffArray[tmpBuff.buffIndex].SetActive(true);
    }

    /// <summary>
    /// ȡ������
    /// </summary>
    public virtual void RemoveDeformationSheep(Buff tmpBuff)
    {
        CancelControll();
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="tmpBuff"></param>
    /// <param name="isAddBuff"></param>
    public virtual void Frozen(Buff tmpBuff, bool isAddBuff = true)
    {
        AddControllMove(isAddBuff);
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);
        buffArray[tmpBuff.buffIndex].transform.GetChild(0).gameObject.SetActive(true);
        buffArray[tmpBuff.buffIndex].transform.GetChild(1).gameObject.SetActive(true);
        buffArray[tmpBuff.buffIndex].transform.GetChild(1).DOScale(Vector3.one * 2, 1.5f);

    }
    //�Ƴ�����
    public virtual async void RemoveFrozen(Buff tmpBuff)
    {
        CancelControllMove();
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);
        buffArray[tmpBuff.buffIndex].transform.GetChild(0).gameObject.SetActive(false);
        GameObject tmp = buffArray[tmpBuff.buffIndex].transform.GetChild(1).gameObject;
        tmp.transform.DOScale(Vector3.zero, 1.5f);
        await Task.Delay(1500);
        tmp.SetActive(false);

    }
    /// <summary>
    ///�ӹ���buff
    /// </summary>
    public virtual void AddAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// �Ƴ��ӹ���buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveAddAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);
    }

    /// <summary>
    /// ������buff
    /// </summary>
    public virtual void SubAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// �Ƴ�����buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveSubAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);
    }


    /// <summary>
    /// �ӷ�buff
    /// </summary>
    public virtual void AddDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);

    }
    /// <summary>
    /// �Ƴ��ӷ�Buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);
    }
    /// <summary>
    /// ������buff
    /// </summary>
    public virtual void SubDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);

    }
    /// <summary>
    /// �Ƴ�����buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveSubDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);

    }

}
