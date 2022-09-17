using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILMsgCenter
{
    private static ILMsgCenter instance;
    public static ILMsgCenter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ILMsgCenter();
            }
            return instance;
        }
    }



    public void Awake()
    {
        ////��ʼ����ģ��

        Initialize();
    }
    /// <summary>
    /// ��ʼ�����߼�ģ��
    /// </summary>
    public void Initialize()
    {
        ILNativeResLoader iLNativeResLoader = new ILNativeResLoader();
        iLNativeResLoader.Awake();

        TcpSocket tcpSocket = new TcpSocket();
        tcpSocket.Awake();



    }

    public void SendToMsg(ILMsgBase tmpMsg)
    {
        AnasysisMsg(tmpMsg);
    }

    private void AnasysisMsg(ILMsgBase tmpMsg)
    {
        ILManagerID tmpId = tmpMsg.GetManager();
        switch (tmpId)
        {
            case ILManagerID.NetManager:
                NetManager.Instance.SendMsg(tmpMsg);
                break;
            case ILManagerID.UIManager:
                ILUIManager.Instance.SendMsg(tmpMsg);
                break;
            case ILManagerID.AssetManager:
                ILAssetManager.Instance.SendMsg(tmpMsg);
                break;
            case ILManagerID.NpcManager:
                ILNpcManager.Instance.SendMsg(tmpMsg);
                break;
            case ILManagerID.AdventureManager:
                IAdventureManager.Instance.SendMsg(tmpMsg);
                break;
            default:
                Debug.LogError("not ManagerType");
                break;
        }
    }
}

