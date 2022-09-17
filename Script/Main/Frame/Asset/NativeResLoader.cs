using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NativeResCallBack(NativeResCallBackNode tmpNode);

public class NativeResCallBackNode
{
    public string sceneName;

    public string bundleName;

    public string resName;

    public ushort backMsgId;

    public bool isSingle;

    public NativeResCallBackNode nextValue;

    public NativeResCallBack callBack;

    public NativeResCallBackNode(bool tmpSingle, string tmpSceneName,
        string tmpBundle, string tmpRes,
        ushort tmpBackID, NativeResCallBack tmpBack, NativeResCallBackNode tmpNode)
    {
        this.isSingle = tmpSingle;

        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.backMsgId = tmpBackID;

        this.nextValue = tmpNode;

        this.callBack = tmpBack;
    }

    public void Dispose()
    {
        nextValue = null;

        callBack = null;
    }

}

public class NativeResCallBackManager
{
    Dictionary<string, NativeResCallBackNode> manager = null;

    public NativeResCallBackManager()
    {
        manager = new Dictionary<string, NativeResCallBackNode>();
    }
    /// <summary>
    /// 添加过程
    /// </summary>
    /// <param name="bundle"></param>
    /// <param name="curNode"></param>
    public void AddBundle(string bundle, NativeResCallBackNode curNode)
    {
        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode tmpNode = manager[bundle];

            while (tmpNode.nextValue != null)
            {
                tmpNode = tmpNode.nextValue;
            }

            tmpNode.nextValue = curNode;
        }
        else
        {
            manager.Add(bundle, curNode);
        }
    }
    /// <summary>
    /// 加载完成 释放缓存命令
    /// </summary>
    /// <param name="bundle"></param>
    public void Dispose(string bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode tmpNode = manager[bundle];
            while (tmpNode.nextValue != null)
            {
                NativeResCallBackNode curNode = tmpNode;

                tmpNode = tmpNode.nextValue;

                curNode.Dispose();
            }

            tmpNode.Dispose();
            manager.Remove(bundle);
        }

    }

    public void CallBackRes(string bundle)
    {
        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode tmpNode = manager[bundle];

            do
            {
                tmpNode.callBack(tmpNode);

                tmpNode = tmpNode.nextValue;

            } while (tmpNode != null);
        }
    }
}

public class NativeResLoader : AssetBase
{

    public override void ProcessEvent(MsgBase recMsg)
    {
        switch (recMsg.msgID)
        {
            case (ushort)AssetEvent.ReleaseSingleObj:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadResObj(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName);
                }
                break;
            case (ushort)AssetEvent.ReleaseBundleObj:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadBundleResObjs(tmpMsg.sceneName, tmpMsg.bundleName);
                }
                break;
            case (ushort)AssetEvent.ReleaseScenceObj:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAllResObjs(tmpMsg.sceneName);
                }
                break;
            case (ushort)AssetEvent.ReleaseSingleBundle:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAssetBundle(tmpMsg.sceneName, tmpMsg.bundleName);
                }
                break;
            case (ushort)AssetEvent.ReleaseScenceBundle:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAllAssetBundle(tmpMsg.sceneName);
                }
                break;
            case (ushort)AssetEvent.ReleaseAll:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAllAssetBundleAndResObjs(tmpMsg.sceneName);
                }
                break;

            case (ushort)AssetEvent.GetResObject:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    GetResources(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.isSingle, tmpMsg.backMsgId);
                }
                break;
            case (ushort)AssetEvent.GetResObjectUI:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)recMsg;
                    GetResources(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.isSingle, tmpMsg.backMsgId, tmpMsg.hierarchy);
                }
                break;
            case (ushort)AssetEvent.GetResObjectCallBack://加载UI界面回调
                {
                    HunkAssetResBack tmp = (HunkAssetResBack)recMsg;
                    Debug.Log("加载完成：" + ((GameObject)tmp.value).name);
                    GameObject obj = GameObject.Instantiate((GameObject)tmp.value);
                    obj.transform.SetParent(UIPanle.transform.GetChild(tmp.hierarchy));
                    obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    obj.SetActive(true);

                }
                break;
            default:
                break;
        }
    }

    GameObject uIPanle = null;
    GameObject UIPanle
    {
        get
        {
            if (uIPanle == null)
            {
                uIPanle = GameObject.Find("Canvas");
            }
            return uIPanle;
        }
    }

    HunkAssetResBack resBackMsg = null;

    HunkAssetResBack RelseaseBack
    {
        get
        {
            if (resBackMsg == null)
            {
                resBackMsg = new HunkAssetResBack();
            }
            return resBackMsg;
        }
    }

    NativeResCallBackManager callBack = null;

    NativeResCallBackManager CallBack
    {
        get
        {
            if (callBack == null)
            {
                callBack = new NativeResCallBackManager();
            }
            return callBack;
        }
    }

    private void Awake()
    {
        this.msgIds = new ushort[]
        {
             (ushort)AssetEvent.ReleaseSingleObj,
             (ushort)AssetEvent.ReleaseBundleObj,
             (ushort)AssetEvent.ReleaseScenceObj,
             (ushort)AssetEvent.ReleaseSingleBundle,
             (ushort)AssetEvent.ReleaseScenceBundle,
             (ushort)AssetEvent.ReleaseAll,
             (ushort)AssetEvent.GetResObject,
             (ushort)AssetEvent.GetResObjectUI,
             (ushort)AssetEvent.GetResObjectCallBack,
        };

        RegistSelf(this, msgIds);
    }

    /// <summary>
    /// node 回调
    /// </summary>
    /// <param name="tmpNode"></param>
    public void SendToBackMsg(NativeResCallBackNode tmpNode)
    {
        if (tmpNode.isSingle)
        {
            Object tmpObj = ILoaderManager.Instance.GetSingleResources(tmpNode.sceneName, tmpNode.bundleName, tmpNode.resName);
            this.RelseaseBack.Chanager(tmpNode.backMsgId, tmpObj);

            SendMsg(RelseaseBack);
        }
        else
        {
            Object[] tmpObj = ILoaderManager.Instance.GetMuitResources(tmpNode.sceneName, tmpNode.bundleName, tmpNode.resName);
            this.RelseaseBack.Chanager(tmpNode.backMsgId, tmpObj);

            SendMsg(RelseaseBack);
        }
    }

    void LoaderProgrecess(string bundleName, float progress)
    {
        if (progress >= 1.0f)
        {
            CallBack.CallBackRes(bundleName);

            CallBack.Dispose(bundleName);
        }
    }


    public void GetResources(string scenceName, string bundleName, string res, bool single, ushort backid, ushort tmphierarchy = 0)
    {
        if (!ILoaderManager.Instance.IsLoadingAssetBundle(scenceName, bundleName))
        {
            ILoaderManager.Instance.LoadAsset(scenceName, bundleName, LoaderProgrecess);

            string bundleFullName = ILoaderManager.Instance.GetBundleRetateName(scenceName, bundleName);

            if (bundleFullName != null)
            {
                NativeResCallBackNode tmpNode = new NativeResCallBackNode(single, scenceName, bundleName, res, backid, SendToBackMsg, null);
                CallBack.AddBundle(bundleFullName, tmpNode);
            }
            else
            {
                Debug.LogWarning("Do not contain bundle==" + bundleName);
            }
        }
        else if (ILoaderManager.Instance.IsLoadingBundleFinish(scenceName, bundleName))//加载完成
        {
            if (single)
            {
                Object tmpObj = ILoaderManager.Instance.GetSingleResources(scenceName, bundleName, res);

                if (tmphierarchy != 0) this.RelseaseBack.Chanager(backid, tmphierarchy, tmpObj);
                else this.RelseaseBack.Chanager(backid, tmpObj);

                SendMsg(RelseaseBack);
            }
            else
            {
                Object[] tmpObj = ILoaderManager.Instance.GetMuitResources(scenceName, bundleName, res);

                if (tmphierarchy != 0) this.RelseaseBack.Chanager(backid, tmphierarchy, tmpObj);
                else this.RelseaseBack.Chanager(backid, tmpObj);

                SendMsg(RelseaseBack);
            }
        }
        else   //正在加载中
        {
            string bundleFullName = ILoaderManager.Instance.GetBundleRetateName(scenceName, bundleName);

            if (bundleFullName != null)
            {
                NativeResCallBackNode tmpNode = new NativeResCallBackNode(single, scenceName, bundleName, res, backid, SendToBackMsg, null);
                CallBack.AddBundle(bundleFullName, tmpNode);
            }
            else
            {
                Debug.LogWarning("Do not contain bundle==" + bundleName);
            }
        }
    }






    public void GetResEditor()
    {

    }
}
