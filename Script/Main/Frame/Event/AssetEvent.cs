using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AssetEvent
{
    GetResObject = ManagerID.AssetManager + 1,
    GetResObjectUI,
    GetResObjectCallBack,




    ReleaseSingleObj,
    ReleaseBundleObj,
    ReleaseScenceObj,

    ReleaseSingleBundle,
    ReleaseScenceBundle,

    ReleaseAll,

}

public enum UILevel
{
    Dwon = 0,
    Two,
    Three,
    Four,
    Five,
    Six,
    Top,

}


public class HunkAssetRes : MsgBase
{
    public string sceneName;

    public string bundleName;

    public string resName;

    public ushort backMsgId;

    public bool isSingle;

    public ushort hierarchy;

    public HunkAssetRes(bool tmpSingle, ushort msgId, string tmpSceneName, string tmpBundle, string tmpRes, ushort tmpBackID)
    {
        this.isSingle = tmpSingle;

        this.msgID = msgId;

        this.sceneName = tmpSceneName;

        this.bundleName = tmpBundle;

        this.resName = tmpRes;

        this.backMsgId = tmpBackID;
    }

    public HunkAssetRes(bool tmpSingle, ushort msgId, string tmpSceneName, string tmpBundle, string tmpRes, ushort tmpBackID, ushort tmphierarchy)
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


public class HunkAssetResBack : MsgBase
{

    public ushort hierarchy;

    public Object value;

    public Object[] Value;




    public HunkAssetResBack()
    {
        this.msgID = 0;
        this.value = null;
    }

    public void Chanager(ushort msgId, ushort tmphierarchy, params Object[] tmpValue)
    {
        this.msgID = msgId;

        if (tmpValue.Length == 1) this.value = tmpValue[0];
        else this.Value = tmpValue;

        this.hierarchy = tmphierarchy;
    }

    public void Chanager(ushort msgId, params Object[] tmpValue)
    {
        this.msgID = msgId;

        if (tmpValue.Length == 1) this.value = tmpValue[0];
        else this.Value = tmpValue;
    }

    public void Chanager(ushort msgId)
    {
        this.msgID = msgId;
    }

    public void Chanager(params Object[] tmpValue)
    {
        if (tmpValue.Length == 1) this.value = tmpValue[0];
        else this.Value = tmpValue;
    }
}