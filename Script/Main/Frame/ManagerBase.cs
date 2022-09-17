using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventNode
{
    //当前数据
    public MonoBase data;
    //下一个节点
    public EventNode next;

    public EventNode(MonoBase tmpMono)
    {
        this.data = tmpMono;
        this.next = null;
    }
}

public class ManagerBase : MonoBase
{
    //存储注册消息
    public Dictionary<ushort, EventNode> eventTree = new Dictionary<ushort, EventNode>();

    /// <summary>
    /// 消息注册
    /// </summary>
    /// <param name="mono">需要注册的脚本</param>
    /// <param name="msgs">注册消息</param>
    public void RegistMsg(MonoBase mono, params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            EventNode tmp = new EventNode(mono);
            RegistMsg(msgs[i], tmp);
        }
    }

    /// <summary>
    /// 消息注册
    /// </summary>
    /// <param name="ID">msgID</param>
    /// <param name="node">node 链表</param>
    public void RegistMsg(ushort ID, EventNode node)
    {
        //如果没有ID
        if (!eventTree.ContainsKey(ID))
        {
            eventTree.Add(ID, node);
        }
        else
        {
            EventNode tmp = eventTree[ID];

            while (tmp.next != null)
            {
                tmp = tmp.next;
            }
            tmp.next = node;
        }
    }

    /// <summary>
    /// 注销脚本的若干个消息
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="msgs"></param>
    public void UnRegistMsg(MonoBase mono, params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            UnRegistMsg(msgs[i], mono);
        }
    }

    /// <summary>
    /// 注销消息
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="node"></param>
    public void UnRegistMsg(ushort ID, MonoBase node)
    {
        if (!eventTree.ContainsKey(ID))
        {
            Debug.LogError("not contain id ==" + ID);
            return;
        }
        else
        {
            EventNode tmp = eventTree[ID];
            if (tmp.data == node) //去掉头部节点
            {
                EventNode header = tmp;

                if (header.next != null)
                {
                    eventTree[ID] = tmp.next;
                    header.next = null;

                }
                else
                {
                    eventTree.Remove(ID);
                }
            }
            else//去掉尾部或者中间节点
            {
                while (tmp.next != null && tmp.next.data != node)
                {
                    tmp = tmp.next;
                }

                if (tmp.next.next != null)
                {
                    EventNode curNode = tmp.next;
                    tmp.next = curNode.next;
                    curNode.next = null;
                    //tmp.next = tmp.next.next;
                }
                else
                {
                    tmp.next = null;
                }
            }
        }
    }

    public override void ProcessEvent(MsgBase tmpMsg)
    {
        if (!eventTree.ContainsKey(tmpMsg.msgID))
        {
            Debug.LogError("消息未注册 msg not contain msgID====" + tmpMsg.msgID);
            return;
        }
        else
        {
            EventNode tmp = eventTree[tmpMsg.msgID];

            do
            {
                tmp.data.ProcessEvent(tmpMsg);
                tmp = tmp.next;
            } while (tmp != null);
        }
    }
}
