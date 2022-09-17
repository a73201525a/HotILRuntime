using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public delegate void ILNativeResCallBack(ILNativeResCallBackNode tmpNode);

public class ILNativeResCallBackNode
{
    public string sceneName;

    public string bundleName;

    public string resName;

    public ushort backMsgId;

    public bool isSingle;

    public ushort hierarchy;

    public ILNativeResCallBackNode nextValue;

    public ILNativeResCallBack callBack;

    public ILNativeResCallBackNode(bool tmpSingle, string tmpSceneName,
        string tmpBundle, string tmpRes,
        ushort tmpBackID, ILNativeResCallBack tmpBack, ILNativeResCallBackNode tmpNode, ushort tmphierarchy = 100)
    {
        this.isSingle = tmpSingle;

        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.backMsgId = tmpBackID;

        this.nextValue = tmpNode;

        this.callBack = tmpBack;

        this.hierarchy = tmphierarchy;
    }

    public void Dispose()
    {
        nextValue = null;

        callBack = null;
    }

}

public class ILNativeResCallBackManager
{
    Dictionary<string, ILNativeResCallBackNode> manager = null;

    public ILNativeResCallBackManager()
    {
        manager = new Dictionary<string, ILNativeResCallBackNode>();
    }
    /// <summary>
    /// 添加过程
    /// </summary>
    /// <param name="bundle"></param>
    /// <param name="curNode"></param>
    public void AddBundle(string bundle, ILNativeResCallBackNode curNode)
    {
        if (manager.ContainsKey(bundle))
        {
            ILNativeResCallBackNode tmpNode = manager[bundle];

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
            ILNativeResCallBackNode tmpNode = manager[bundle];
            while (tmpNode.nextValue != null)
            {
                ILNativeResCallBackNode curNode = tmpNode;

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
            ILNativeResCallBackNode tmpNode = manager[bundle];

            do
            {
                tmpNode.callBack(tmpNode);

                tmpNode = tmpNode.nextValue;

            } while (tmpNode != null);
        }
    }
}

public class ILNativeResLoader : ILAssetBase
{

    public override void ProcessEvent(ILMsgBase recMsg)
    {
        switch (recMsg.msgID)
        {
            case (ushort)ILAssetEvent.ReleaseSingleObj:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadResObj(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName);
                }
                break;
            case (ushort)ILAssetEvent.ReleaseBundleObj:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadBundleResObjs(tmpMsg.sceneName, tmpMsg.bundleName);
                }
                break;
            case (ushort)ILAssetEvent.ReleaseScenceObj:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAllResObjs(tmpMsg.sceneName);
                }
                break;
            case (ushort)ILAssetEvent.ReleaseSingleBundle:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAssetBundle(tmpMsg.sceneName, tmpMsg.bundleName);
                }
                break;
            case (ushort)ILAssetEvent.ReleaseSingleBundleAndObj:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAssetBundle(tmpMsg.sceneName, tmpMsg.bundleName);
                    ILoaderManager.Instance.UnLoadBundleResObjs(tmpMsg.sceneName, tmpMsg.bundleName);
                }
                break;
            case (ushort)ILAssetEvent.ReleaseScenceBundle:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAllAssetBundle(tmpMsg.sceneName);
                }
                break;
            case (ushort)ILAssetEvent.ReleaseAll:
                {
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    ILoaderManager.Instance.UnLoadAllAssetBundleAndResObjs(tmpMsg.sceneName);
                }
                break;

            case (ushort)ILAssetEvent.GetResObject://加载gameobject
                {
#if UNITY_EDITOR
                    /////编辑器模式下加载
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    if (tmpMsg.resName == null)
                    {
                        return;
                    }
                    string[] strArry = tmpMsg.resName.Split('|');
                    List<Object> ObjList = new List<Object>();

                    for (int i = 0; i < strArry.Length; i++)
                    {
                        Object obj = TestLoader.Instance.LoadAssetInBundleImmediately(strArry[i], tmpMsg.sceneName + "/" + tmpMsg.bundleName);
                        ObjList.Add(obj);
                    }
                    // Object obj = TestLoader.Instance.LoadAssetInBundleImmediately(tmpMsg.resName, tmpMsg.sceneName + "/" + tmpMsg.bundleName);    //("LoadPanel", "scenesone/load");

                    this.RelseaseBack.Chanager(tmpMsg.backMsgId, tmpMsg.hierarchy, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.sceneName, ObjList.ToArray());


                    // SetUIHierarchy(this.RelseaseBack);
                    SendMsg(this.RelseaseBack);
#else
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    Debug.Log("tmpMsg.::" + tmpMsg.resName);
                    GetResources(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.isSingle, tmpMsg.backMsgId);
#endif


                }
                break;
            case (ushort)ILAssetEvent.GetResObjectUI://加载ui界面
                {
#if UNITY_EDITOR
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;

                    Object obj = TestLoader.Instance.LoadAssetInBundleImmediately(tmpMsg.resName, tmpMsg.sceneName + "/" + tmpMsg.bundleName);    //("LoadPanel", "scenesone/load");                  
                    this.RelseaseBack.Chanager(tmpMsg.backMsgId, tmpMsg.hierarchy, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.sceneName, obj);
                    SetUIHierarchy(this.RelseaseBack);

                    SendMsg(this.RelseaseBack);
#else
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    Debug.Log("tmpMsg.::" + tmpMsg.resName);
                    GetResources(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.isSingle, tmpMsg.backMsgId, tmpMsg.hierarchy);
#endif

                }
                break;
            case (ushort)ILAssetEvent.GetResSprite://加载图片
                {
#if UNITY_EDITOR
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    EditorLoadSprite(tmpMsg);
#else
                    ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
                    Debug.Log("tmpMsg.::" + tmpMsg.resName);
                    GetResourcesSprite(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.isSingle, tmpMsg.backMsgId);
#endif
                }
                break;
            case (ushort)ILAssetEvent.GetResBundleAllSprite:
                {
                    //  Debug.Log("tmpMsg.::" + (ILHunkAssetRes)recMsg.resName);
                    GetBundleAllSprite(recMsg);

                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置UI层级
    /// </summary>
    /// <param name="recMsg"></param>
    void SetUIHierarchy(ILHunkAssetResBack recMsg)
    {
        Object o = recMsg.value;
        GameObject obj = GameObject.Instantiate((GameObject)o);
        obj.transform.SetParent(UIPanle.transform.GetChild(recMsg.hierarchy));
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.SetActive(true);
        ILUIManager.AddUIPanel(recMsg.resName, recMsg.bundleName, recMsg.sceneName, obj);
        //ILUIManager.HideView("LoadPanel");
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

    ILHunkAssetResBack resBackMsg = null;

    ILHunkAssetResBack RelseaseBack
    {
        get
        {
            if (resBackMsg == null)
            {
                resBackMsg = new ILHunkAssetResBack();
            }
            return resBackMsg;
        }
    }

    ILNativeResCallBackManager callBack = null;

    ILNativeResCallBackManager CallBack
    {
        get
        {
            if (callBack == null)
            {
                callBack = new ILNativeResCallBackManager();
            }
            return callBack;
        }
    }

    public void Awake()
    {
        this.msgIds = new ushort[]
        {
             (ushort)ILAssetEvent.ReleaseSingleObj,
             (ushort)ILAssetEvent.ReleaseBundleObj,
             (ushort)ILAssetEvent.ReleaseScenceObj,
             (ushort)ILAssetEvent.ReleaseSingleBundle,
             (ushort)ILAssetEvent.ReleaseScenceBundle,
             (ushort)ILAssetEvent.ReleaseAll,
             (ushort)ILAssetEvent.GetResObject,
             (ushort)ILAssetEvent.GetResObjectUI,
             (ushort)ILAssetEvent.GetResObjectCallBack,
              (ushort)ILAssetEvent.GetResSprite,
               (ushort)ILAssetEvent.ReleaseSingleBundleAndObj,
               (ushort)ILAssetEvent.GetResBundleAllSprite,

        };

        RegistSelf(this, msgIds);
    }

    /// <summary>
    /// node 回调
    /// </summary>
    /// <param name="tmpNode"></param>
    public void SendToBackMsg(ILNativeResCallBackNode tmpNode)
    {


        if (tmpNode.isSingle)
        {
            Object tmpObj = ILoaderManager.Instance.GetSingleResources(tmpNode.sceneName, tmpNode.bundleName, tmpNode.resName);
            this.RelseaseBack.Chanager(tmpNode.backMsgId, tmpObj);

            if (tmpNode.hierarchy != 100)
            {
                this.RelseaseBack.Chanager(tmpNode.backMsgId, tmpNode.hierarchy, tmpNode.bundleName, tmpNode.resName, tmpNode.sceneName, tmpObj);

                SetUIHierarchy(this.RelseaseBack);

            }


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


    public void GetResources(string scenceName, string bundleName, string res, bool single, ushort backid, ushort tmphierarchy = 100)//tmphierarchy 默认不填表示非UI界面
    {

        if (!ILoaderManager.Instance.IsLoadingAssetBundle(scenceName, bundleName))
        {
            ILoaderManager.Instance.LoadAsset(scenceName, bundleName, LoaderProgrecess);

            string bundleFullName = ILoaderManager.Instance.GetBundleRetateName(scenceName, bundleName);

            if (bundleFullName != null)
            {
                ILNativeResCallBackNode tmpNode = new ILNativeResCallBackNode(single, scenceName, bundleName, res, backid, SendToBackMsg, null, tmphierarchy);
                CallBack.AddBundle(bundleFullName, tmpNode);
            }
            else
            {
                Debug.LogError("Do not contain bundle==" + bundleName);
            }
        }
        else if (ILoaderManager.Instance.IsLoadingBundleFinish(scenceName, bundleName))//加载完成
        {
            if (single)
            {
                Object tmpObj = ILoaderManager.Instance.GetSingleResources(scenceName, bundleName, res);

                if (tmphierarchy != 100)
                {
                    this.RelseaseBack.Chanager(backid, tmphierarchy, bundleName, res, scenceName, tmpObj);

                    SetUIHierarchy(this.RelseaseBack);

                }
                else { this.RelseaseBack.Chanager(backid, tmpObj); }

                SendMsg(RelseaseBack);

            }
            else
            {
                string[] strArry = res.Split('|');

                List<Object> Obj = new List<Object>();
                for (int i = 0; i < strArry.Length; i++)
                {
                    Object[] tmpObj = ILoaderManager.Instance.GetMuitResources(scenceName, bundleName, res);
                    Obj.AddRange(tmpObj);
                }
                //Object[] tmpObj = ILoaderManager.Instance.GetMuitResources(scenceName, bundleName, res);
                //if (tmphierarchy != 0)
                //{
                //    this.RelseaseBack.Chanager(backid, tmphierarchy, tmpObj);
                //}
                //else {
                this.RelseaseBack.Chanager(backid, Obj.ToArray());

                SendMsg(RelseaseBack);
            }
        }
        else //正在加载中
        {
            string bundleFullName = ILoaderManager.Instance.GetBundleRetateName(scenceName, bundleName);

            if (bundleFullName != null)
            {
                ILNativeResCallBackNode tmpNode = new ILNativeResCallBackNode(single, scenceName, bundleName, res, backid, SendToBackMsg, null, tmphierarchy);
                CallBack.AddBundle(bundleFullName, tmpNode);
            }
            else
            {
                Debug.LogWarning("Do not contain bundle==" + bundleName);
            }
        }
    }
    public void GetResourcesSprite(string scenceName, string bundleName, string res, bool single, ushort backid, ushort tmphierarchy = 100)
    {
        if (!ILoaderManager.Instance.IsLoadingAssetBundle(scenceName, bundleName))
        {
            ILoaderManager.Instance.LoadAsset(scenceName, bundleName, LoaderProgrecess);

            string bundleFullName = ILoaderManager.Instance.GetBundleRetateName(scenceName, bundleName);

            if (bundleFullName != null)
            {
                ILNativeResCallBackNode tmpNode = new ILNativeResCallBackNode(single, scenceName, bundleName, res, backid, SendToBackSpriteMsg, null, tmphierarchy);
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
                Sprite tmpObj = ILoaderManager.Instance.GetResourcesSprite(scenceName, bundleName, res);

                this.RelseaseBack.Chanager(backid, tmpObj);

                SendMsg(RelseaseBack);

            }
            else
            {

                ILoaderManager.Instance.GetBundleAllSprite(scenceName, bundleName);

                // this.RelseaseBack.Chanager(backid, tmpObj);

                // SendMsg(RelseaseBack);
            }
        }
        else   //正在加载中
        {
            string bundleFullName = ILoaderManager.Instance.GetBundleRetateName(scenceName, bundleName);

            if (bundleFullName != null)
            {
                ILNativeResCallBackNode tmpNode = new ILNativeResCallBackNode(single, scenceName, bundleName, res, backid, SendToBackSpriteMsg, null, tmphierarchy);
                CallBack.AddBundle(bundleFullName, tmpNode);
            }
            else
            {
                Debug.LogWarning("Do not contain bundle==" + bundleName);
            }
        }
    }


    /// <summary>
    /// node 回调
    /// </summary>
    /// <param name="tmpNode"></param>
    public void SendToBackSpriteMsg(ILNativeResCallBackNode tmpNode)
    {
        if (tmpNode.isSingle)
        {
            Sprite tmpObj = ILoaderManager.Instance.GetResourcesSprite(tmpNode.sceneName, tmpNode.bundleName, tmpNode.resName);
            this.RelseaseBack.Chanager(tmpNode.backMsgId, tmpObj);
            SendMsg(RelseaseBack);
        }
        else
        {
            Dictionary<string, Sprite> tmpObj = ILoaderManager.Instance.GetBundleAllSprite(tmpNode.sceneName, tmpNode.bundleName);
            this.RelseaseBack.Chanager(tmpNode.backMsgId, tmpObj);
            SendMsg(RelseaseBack);
        }
    }

    public void GetBundleAllSprite(ILMsgBase recMsg)
    {

#if UNITY_EDITOR
        ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
        string fullPath = Path.Combine(Application.dataPath + "/Art/", "Scenes/ScenesTwo/", tmpMsg.bundleName);

        Dictionary<string, Sprite> spriteTmp = new Dictionary<string, Sprite>();
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            // Debug.Log("files.Length:" + files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                //打印出来这个文件架下的所有文件
                string tmp = files[i].Name.Remove(files[i].Name.LastIndexOf("."));
                Sprite sp = EditorLoadSprite(tmpMsg, tmp);
                //  Debug.Log("Name:::" + tmp);
                spriteTmp.Add(tmp, sp);
                //Debug.Log("FullName:" + files[i].FullName);
                //Debug.Log("DirectoryName:" + files[i].DirectoryName);
            }
        }
        this.RelseaseBack.Chanager(tmpMsg.backMsgId, spriteTmp);
        SendMsg(RelseaseBack);

#else
        ILHunkAssetRes tmpMsg = (ILHunkAssetRes)recMsg;
        GetResourcesSprite(tmpMsg.sceneName, tmpMsg.bundleName, tmpMsg.resName, false, tmpMsg.backMsgId);
#endif
    }

    void EditorLoadSprite(ILHunkAssetRes tmpMsg)
    {
        Texture2D imgTexture = (Texture2D)TestLoader.Instance.LoadAssetInBundleImmediately(tmpMsg.resName, tmpMsg.sceneName + "/" + tmpMsg.bundleName);
        Sprite sp = Sprite.Create(imgTexture, new Rect(0, 0, imgTexture.width, imgTexture.height), new Vector2(0.5f, 0.5f));
        sp.name = imgTexture.name;
        this.RelseaseBack.Chanager(tmpMsg.backMsgId, tmpMsg.hierarchy, tmpMsg.bundleName, tmpMsg.resName, tmpMsg.sceneName, sp);
        SendMsg(RelseaseBack);
    }

    Sprite EditorLoadSprite(ILHunkAssetRes tmpMsg, string resName)
    {

        Texture2D imgTexture = (Texture2D)TestLoader.Instance.LoadAssetInBundleImmediately(resName, tmpMsg.sceneName + "/" + tmpMsg.bundleName);
        Sprite sp = Sprite.Create(imgTexture, new Rect(0, 0, imgTexture.width, imgTexture.height), new Vector2(0.5f, 0.5f));
        sp.name = imgTexture.name;
        return sp;
        //this.RelseaseBack.Chanager(tmpMsg.backMsgId, tmpMsg.hierarchy, tmpMsg.bundleName, resName, tmpMsg.sceneName, sp);
        //SendMsg(RelseaseBack);
    }
}
