using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgCenter : MonoBehaviour
{
    public static MsgCenter Instance = null;

    private void Awake()
    {
        Instance = this;

       //初始化各模块
        gameObject.AddComponent<UIManager>();

        //gameObject.AddComponent<NPCManager>();

        gameObject.AddComponent<AssetManager>();

     //   gameObject.AddComponent<NetManager>();
    }

    public void SendToMsg(MsgBase tmpMsg)
    {
        AnasysisMsg(tmpMsg);
    }

    private void AnasysisMsg(MsgBase tmpMsg)
    {
        ManagerID tmpId = tmpMsg.GetManager();
        switch (tmpId)
        {
            case ManagerID.GameManager:
                break;
            case ManagerID.UIManager:
                UIManager.Instance.SendMsg(tmpMsg);
                break;
            case ManagerID.AssetManager:
                AssetManager.Instance.SendMsg(tmpMsg);
                break;
            //case ManagerID.NetManager:
            //    NetManager.Instance.SendMsg(tmpMsg);
            //    break;
            default:
                Debug.LogError("not ManagerType");
                break;
        }
    }

  
}
