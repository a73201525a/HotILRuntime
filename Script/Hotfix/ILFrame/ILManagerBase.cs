using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ILEventNode
{
    //��ǰ����
    public ILMonoBase data;
    //��һ���ڵ�
    public ILEventNode next;

    public ILEventNode(ILMonoBase tmpMono)
    {
        this.data = tmpMono;
        this.next = null;
    }
}

public class ILManagerBase : ILMonoBase
{
    //�洢ע����Ϣ
    public Dictionary<ushort, ILEventNode> eventTree = new Dictionary<ushort, ILEventNode>();

    /// <summary>
    /// ��Ϣע��
    /// </summary>
    /// <param name="mono">��Ҫע��Ľű�</param>
    /// <param name="msgs">ע����Ϣ</param>
    public void RegistMsg(ILMonoBase mono, params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            ILEventNode tmp = new ILEventNode(mono);

            RegistMsg(msgs[i], tmp);
        }
    }

    /// <summary>
    /// ��Ϣע��
    /// </summary>
    /// <param name="ID">msgID</param>
    /// <param name="node">node ����</param>
    public void RegistMsg(ushort ID, ILEventNode node)
    {
        //���û��ID
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
    /// ע���ű������ɸ���Ϣ
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
    /// ע����Ϣ
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
            if (tmp.data == node) //ȥ��ͷ���ڵ�
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
            else//ȥ��β�������м�ڵ�
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
            Debug.LogError("��Ϣδע�� msg not contain msgID====" + tmpMsg.msgID);
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
