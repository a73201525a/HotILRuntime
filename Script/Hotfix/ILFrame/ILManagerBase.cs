using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ILEventNode
{
    //当前数据
    public ILMonoBase data;
    //下一个节点
    public ILEventNode next;

    public ILEventNode(ILMonoBase tmpMono)
    {
        this.data = tmpMono;
        this.next = null;
    }
}

public class ILManagerBase : ILMonoBase
{
    //存储注册消息
    public Dictionary<ushort, ILEventNode> eventTree = new Dictionary<ushort, ILEventNode>();

    /// <summary>
    /// 消息注册
    /// </summary>
    /// <param name="mono">需要注册的脚本</param>
    /// <param name="msgs">注册消息</param>
    public void RegistMsg(ILMonoBase mono, params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            ILEventNode tmp = new ILEventNode(mono);

            RegistMsg(msgs[i], tmp);
        }
    }

    /// <summary>
    /// 消息注册
    /// </summary>
    /// <param name="ID">msgID</param>
    /// <param name="node">node 链表</param>
    public void RegistMsg(ushort ID, ILEventNode node)
    {
        //如果没有ID
        if (!eventTree.ContainsKey(ID))
        {
            eventTree.Add(ID, node);
        }
        else
        {
            ILEventNode tmp = eventTree[ID];

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
    public void UnRegistMsg(ILMonoBase mono, params ushort[] msgs)
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
    public void UnRegistMsg(ushort ID, ILMonoBase node)
    {

        if (!eventTree.ContainsKey(ID))
        {
            Debug.LogError("not contain id ==" + ID);
            return;
        }
        else
        {
            ILEventNode tmp = eventTree[ID];
            if (tmp.data == node) //去掉头部节点
            {
                ILEventNode header = tmp;

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
                    ILEventNode curNode = tmp.next;
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

    public override void ProcessEvent(ILMsgBase tmpMsg)
    {



        if (!eventTree.ContainsKey(tmpMsg.msgID))
        {
            Debug.LogError("消息未注册 msg not contain msgID====" + tmpMsg.msgID);
            return;
        }
        else
        {
            ILEventNode tmp = eventTree[tmpMsg.msgID];

            do
            {
                tmp.data.ProcessEvent(tmpMsg);
                tmp = tmp.next;

            } while (tmp != null);
        }
    }
}
