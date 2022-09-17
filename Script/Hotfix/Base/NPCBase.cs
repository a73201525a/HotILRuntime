using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class NPCBase : ILMonoBase
{
    public ushort[] msgIds;
    /// <summary>
    /// buff效果
    /// </summary>
    public GameObject[] buffArray;

    public int NpcID;//角色的id

    public Transform transform;

    /// <summary>
    /// 死亡
    /// </summary>
    public bool isDeath;

    //控制buff的数量
    public int buffNumberControll = 0;
    /// <summary>
    /// 是否被控制 无法移动 也无法攻击
    /// </summary>
    public bool isControll;


    //buff缴械触发次数
    public int buffNumberIsAttack = 0;
    /// <summary>
    /// 是否被缴械 可以移动 不可以攻击
    /// </summary>
    public bool isAttack;
    //buff不能移动触发次数
    public int buffNumberIsMove = 0;
    /// <summary>
    /// 不可以移动，但是可以攻击
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
    /// 调用其他脚本功能
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
    /// 添加buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public void AddBuff(Buff tmpBuff)
    {
        HUDMsg hUDMsg = new HUDMsg((ushort)ILUIEvent.ShowHUDTip, NpcID, tmpBuff.name);
        SendMsg(hUDMsg);

    }
    /// <summary>
    /// 移除Buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public void RemoveNPCBuff(Buff tmpBuff)
    {


    }

    /// <summary>
    /// 中毒
    /// </summary>
    /// <param name=""></param>
    public virtual void Poison(Buff tmpBuff, bool isStart = false)
    {
        Debug.Log("添加中毒buff：isStart::" + isStart);
        buffArray[tmpBuff.buffIndex].SetActive(true);
    }
    /// <summary>
    /// 移除中毒
    /// </summary>
    public virtual void RemovePoison(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }
    /// <summary>
    /// 被控制了
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
    /// 解除控制
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
    /// 被控制了 攻击
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
    ///取消不让攻击
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
    /// 被控制了移动
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
    /// 取消控制移动
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
    ///眩晕
    /// </summary>
    /// <param name="nPCBase"></param>
    public virtual void Vertigo(Buff tmpBuff, bool isAddBuff)
    {
        AddControll(isAddBuff);

        buffArray[tmpBuff.buffIndex].SetActive(true);

    }

    /// <summary>
    /// 取消眩晕
    /// </summary>
    /// <param name="buffGameObject"></param>
    public virtual void RemoveVertigo(Buff tmpBuff)
    {
        CancelControll();
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }


    /// <summary>
    /// 缴械
    /// </summary>
    /// <param name="tmpBuff">buff </param>
    /// <param name="isAddBuff">是否添加</param>
    public virtual void Disarm(Buff tmpBuff, bool isAddBuff)
    {
        AddControllAttack(isAddBuff);
        buffArray[tmpBuff.buffIndex].SetActive(true);
    }

    /// <summary>
    /// 取消缴械
    /// </summary>
    public virtual void RemoveDisarm(Buff tmpBuff)
    {
        CancelControllAttack();
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }

    /// <summary>
    /// 变羊
    /// </summary>
    /// <param name="tmpBuff"></param>
    /// <param name="isAddBuff"></param>
    public virtual void DeformationSheep(Buff tmpBuff, bool isAddBuff)
    {
        AddControll(isAddBuff);
        buffArray[tmpBuff.buffIndex].SetActive(true);
    }

    /// <summary>
    /// 取消变羊
    /// </summary>
    public virtual void RemoveDeformationSheep(Buff tmpBuff)
    {
        CancelControll();
        buffArray[tmpBuff.buffIndex].SetActive(false);
    }


    /// <summary>
    /// 冻结
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
    //移除冻结
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
    ///加攻击buff
    /// </summary>
    public virtual void AddAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// 移除加攻击buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveAddAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);
    }

    /// <summary>
    /// 减攻击buff
    /// </summary>
    public virtual void SubAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// 移除减攻buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveSubAttack(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);
    }


    /// <summary>
    /// 加防buff
    /// </summary>
    public virtual void AddDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(true);

    }
    /// <summary>
    /// 移除加防Buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);
    }
    /// <summary>
    /// 减防御buff
    /// </summary>
    public virtual void SubDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);

    }
    /// <summary>
    /// 移除防御buff
    /// </summary>
    /// <param name="tmpBuff"></param>
    public virtual void RemoveSubDefence(Buff tmpBuff)
    {
        buffArray[tmpBuff.buffIndex].gameObject.SetActive(false);

    }

}
