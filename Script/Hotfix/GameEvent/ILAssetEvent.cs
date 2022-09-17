using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum ILAssetEvent
{
    GetResObject = ILManagerID.AssetManager + 1,
    GetResObjectUI,
    GetResSprite,
    GetResObjectCallBack,
    GetResBundleAllSprite,//获取这个bundle下所有的图片



    ReleaseSingleObj,
    ReleaseBundleObj,
    ReleaseScenceObj,

    ReleaseSingleBundle,
    ReleaseScenceBundle,

    ReleaseSingleBundleAndObj,//卸载包的obj和bundle

    ReleaseAll,

}



public class ILHunkAssetRes : ILMsgBase
{
    public string sceneName;

    public string bundleName;

    public string resName;

    public string[] resNameArray;

    public ushort backMsgId;

    public bool isSingle;

    public ushort hierarchy;

    public ILHunkAssetRes(string tmpSceneName, string tmpBundle, ushort msgId)
    {
        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.msgID = msgId;
    }
    public ILHunkAssetRes(string tmpSceneName, string tmpBundle, string tmpRes, ushort msgId)
    {
        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.msgID = msgId;
    }

    public ILHunkAssetRes(bool tmpSingle, ushort msgId, string tmpSceneName, string tmpBundle, string tmpRes, ushort tmpBackID)
    {
        this.isSingle = tmpSingle;

        this.msgID = msgId;

        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.backMsgId = tmpBackID;
    }

    public ILHunkAssetRes(ushort msgId, string tmpSceneName, string tmpBundle, string tmpRes, ushort tmpBackID)
    {
        this.msgID = msgId;

        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.backMsgId = tmpBackID;
    }

    public ILHunkAssetRes(bool tmpSingle, ushort msgId, string tmpSceneName, string tmpBundle, string tmpRes, ushort tmpBackID, ushort tmphierarchy)
    {
        this.isSingle = tmpSingle;

        this.msgID = msgId;

        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.backMsgId = tmpBackID;

        this.hierarchy = tmphierarchy;
    }

}


public class ILHunkAssetResBack : ILMsgBase
{

    public ushort hierarchy;

    public Object value;

    public Object[] ValueAll;

    public Sprite sprite;

    public string bundleName;

    public string resName;

    public string sceneName;

    public Dictionary<string, Sprite> AtlasSpriteAll;
    public ILHunkAssetResBack()
    {
        this.msgID = 0;
        this.value = null;
        bundleName = "";
        resName = "";
        sceneName = "";
    }
    public void Chanager(ushort msgId, Dictionary<string, Sprite> tmpValue)
    {
        this.msgID = msgId;
        this.AtlasSpriteAll = tmpValue;
    }

    public void Chanager(ushort msgId, ushort tmphierarchy, string tmpBundleName, string tmpResName, string tmpSceneName, params Sprite[] tmpValue)
    {
        this.msgID = msgId;
        this.bundleName = tmpBundleName;
        this.resName = tmpResName;
        this.sceneName = tmpSceneName;


        if (tmpValue.Length == 1) this.sprite = tmpValue[0];

        this.ValueAll = tmpValue;
        this.hierarchy = tmphierarchy;
    }

    public void Chanager(ushort msgId, ushort tmphierarchy, string tmpBundleName, string tmpResName, string tmpSceneName, params Object[] tmpValue)
    {
        this.msgID = msgId;
        this.bundleName = tmpBundleName;
        this.resName = tmpResName;
        this.sceneName = tmpSceneName;

        if (tmpValue.Length == 1) this.value = tmpValue[0];

        this.ValueAll = tmpValue;
        this.hierarchy = tmphierarchy;
    }

    //public void Chanager(ushort msgId, ushort tmphierarchy, params Object[] tmpValue)
    //{
    //    this.msgID = msgId;

    //    if (tmpValue.Length == 1) this.value = tmpValue[0];
    //    else this.ValueAll = tmpValue;

    //    this.hierarchy = tmphierarchy;
    //}

    public void Chanager(ushort msgId, params Object[] tmpValue)
    {
        this.msgID = msgId;

        if (tmpValue.Length == 1) this.value = tmpValue[0];

        this.ValueAll = tmpValue;
    }

    //public void Chanager(ushort msgId)
    //{
    //    this.msgID = msgId;
    //}

    //public void Chanager(params Object[] tmpValue)
    //{
    //    if (tmpValue.Length == 1) this.value = tmpValue[0];
    //    else this.ValueAll = tmpValue;
    //}
}