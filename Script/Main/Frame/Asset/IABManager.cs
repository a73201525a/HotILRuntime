using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LoadAssetBundleCallBack(string scenceName, string bundleName);

public class AssetObj
{
    public List<Object> objs;

    public AssetObj(params Object[] tmpObj)
    {
        objs = new List<Object>();

        objs.AddRange(tmpObj);
    }

    public void ReleaseObj()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].GetType() != typeof(UnityEngine.GameObject))
                Resources.UnloadAsset(objs[i]);
        }
        objs.Clear();
    }
}

//一个dundle包里面所有的Object
public class AssetResObj
{
    public Dictionary<string, AssetObj> resObjs;

    public AssetResObj(string name, AssetObj tmpObj)
    {
        resObjs = new Dictionary<string, AssetObj>();

        resObjs.Add(name, tmpObj);
    }

    public void AddResObj(string name, AssetObj tmpObj)
    {
        resObjs.Add(name, tmpObj);

    }
    /// <summary>
    /// 释放单个
    /// </summary>
    /// <param name="name"></param>
    public void ReleaseResObje(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetObj tmp = resObjs[name];
            tmp.ReleaseObj();
        }
        else
        {
            Debug.Log("release object name is not exit==" + name);
        }
    }

    /// <summary>
    /// 释放所有Object
    /// </summary>
    public void ReleaseAllResObj()
    {
        List<string> keys = new List<string>();

        keys.AddRange(resObjs.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            ReleaseResObje(keys[i]);
        }
        resObjs.Clear();
    }

    public List<Object> GetResObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetObj tmpObj = resObjs[name];
            return tmpObj.objs;
        }
        else
        {
            return null;
        }
    }
}




//所有AB包管理
public class IABManager
{
    Dictionary<string, IABRelationManager> loadHelper = new Dictionary<string, IABRelationManager>();

    Dictionary<string, AssetResObj> loadObj = new Dictionary<string, AssetResObj>();

    string scenceName;

    public IABManager(string tmpScenceName)
    {
        scenceName = tmpScenceName;
    }

    /// <summary>
    /// 是否加载了bundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingAssetBundle(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #region 加载assetbundle

    /// <summary>
    /// 加载AssetBundle(对外接口)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="progress"></param>
    /// <param name=""></param>
    public void LoadAssetBundle(string bundleName, LoaderProgrecess progress, LoadAssetBundleCallBack CallBack)
    {
        if (!loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = new IABRelationManager();

            loader.Init(bundleName, progress);

            loadHelper.Add(bundleName, loader);

            CallBack(scenceName, bundleName);
        }
        else
        {
            Debug.Log("IABManger have contain bundle name ==" + bundleName);
        }
    }

    //加载包的依赖关系
    public IEnumerator LoadAssetBundleDependences(string bundleName, string refName, LoaderProgrecess progress)
    {
        if (!loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = new IABRelationManager();

            loader.Init(bundleName, progress);

            if (refName != null)
            {
                loader.AddRefference(refName);
            }

            loadHelper.Add(bundleName, loader);

            yield return LoadAssetBundles(bundleName);
        }
        else
        {
            if (refName != null)
            {
                IABRelationManager loader = loadHelper[bundleName];
                loader.AddRefference(refName);
            }
        }
    }

    /// <summary>
    /// 加载assetbundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public IEnumerator LoadAssetBundles(string bundleName)
    {
        while (!IABManifestLoader.Instance.IsLoadFinsh())
        {
            yield return null;
        }

        IABRelationManager loader = loadHelper[bundleName];

        string[] depences = GetDependences(bundleName);

        loader.SetDepedences(depences);

        for (int i = 0; i < depences.Length; i++)
        {
            yield return LoadAssetBundleDependences(depences[i], bundleName, loader.GetProgress());
        }

        yield return loader.LoadAssetBundle();

    }

    private string[] GetDependences(string bundleName)
    {
        return IABManifestLoader.Instance.GetDepences(bundleName);
    }

    #endregion


    #region 释放缓存

    /// <summary>
    /// 释放缓存物体（单个）
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="resName"></param>
    public void DisposeResObj(string bundleName, string resName)
    {
        if (loadObj.ContainsKey(bundleName))
        {
            AssetResObj tmpObj = loadObj[bundleName];
            // loadObj.Remove(bundleName);
            tmpObj.ReleaseResObje(resName);
        }

    }

    /// <summary>
    /// 释放这个bundle缓存物体(全部缓存)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="resName"></param>
    public void DisposeResObj(string bundleName)
    {
        if (loadObj.ContainsKey(bundleName))
        {
            AssetResObj tmpObj = loadObj[bundleName];
            tmpObj.ReleaseAllResObj();
        }
        Resources.UnloadUnusedAssets();

    }

    /// <summary>
    /// 释放所有Object缓存
    /// </summary>
    public void DisposeAllObj()
    {
        List<string> keys = new List<string>();

        keys.AddRange(loadObj.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            DisposeResObj(keys[i]);
        }
    }

    /// <summary>
    /// 循环处理依赖关系
    /// </summary>
    /// <param name="bundleName"></param>
    public void DisposeBundle(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = loadHelper[bundleName];

            List<string> depences = loader.GetDepedences();

            for (int i = 0; i < depences.Count; i++)
            {
                if (loadHelper.ContainsKey(depences[i]))
                {
                    IABRelationManager depen = loadHelper[depences[i]];

                    if (depen.RemoveRefference(bundleName))
                    {
                        DisposeBundle(depen.GeBundleName());
                    }
                }
            }

            if (loader.GetRefference().Count <= 0)
            {
                loader.Dispose();
                loadHelper.Remove(bundleName);
            }
        }
    }

    public void DisposeAllBundle()
    {
        List<string> keys = new List<string>();

        keys.AddRange(loadHelper.Keys);

        for (int i = 0; i < loadHelper.Count; i++)
        {
            IABRelationManager loader = loadHelper[keys[i]];
            loader.Dispose();
        }
        loadHelper.Clear();
    }

    /// <summary>
    /// 删除所有
    /// </summary>
    public void DisposeAllBundleAndRes()
    {
        DisposeAllObj();

        List<string> keys = new List<string>();

        keys.AddRange(loadHelper.Keys);

        for (int i = 0; i < loadHelper.Count; i++)
        {
            IABRelationManager loader = loadHelper[keys[i]];
            loader.Dispose();
        }
        loadHelper.Clear();
    }
    #endregion

    #region 下层提供的API

    public string[] GetAllBundleResName(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = loadHelper[bundleName];
            return loader.GetAllBundleResName();
        }
        else
        {
            Debug.Log("not bundleName == " + bundleName);
            return null;
        }
    }
    public void DebugAssetBundle(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = loadHelper[bundleName];
            loader.DebugAsset();
        }
        else
        {
            Debug.Log("not bundleName == " + bundleName);
        }
    }

    /// <summary>
    /// 是否加载完成bundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingFinish(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = loadHelper[bundleName];

            return loader.IsBundleLoadFinish();
        }
        Debug.Log("IABRelatioin no contain bundle == " + bundleName);
        return false;
    }

    //返回单个
    public Object GetSingleResources(string bundleName, string resName)
    {
        //是否已经缓存了物体
        if (loadObj.ContainsKey(bundleName))
        {
            AssetResObj tmpRes = loadObj[bundleName];

            List<Object> tmpObj = tmpRes.GetResObj(resName);

            if (tmpObj != null && tmpObj.Count > 0)
            {
                return tmpObj[0];
            }
        }

        //表示是否加载过bundle
        if (loadHelper.ContainsKey(bundleName))
        {
            IABRelationManager loader = loadHelper[bundleName];

            Object tmpObj = loader.GetSingleResource(resName);

            AssetObj tmpAssetObj = new AssetObj(tmpObj);

            //缓存里面是否已经有这个包
            if (loadObj.ContainsKey(bundleName))
            {
                AssetResObj tmpRes = loadObj[bundleName];

                tmpRes.AddResObj(resName, tmpAssetObj);
            }
            else
            {
                AssetResObj tmpRes = new AssetResObj(resName, tmpAssetObj);

                loadObj.Add(bundleName, tmpRes);
            }

            return tmpObj;
        }
        else
        {
            return null;
        }
    }
    //返回多个
    public Object[] GetMuitleResources(string bundleName, string resName)
    {
        ////////////////////////////////////

        string[] strArry = resName.Split('|');

        //是否已经缓存了物体
        if (loadObj.ContainsKey(bundleName))
        {
            AssetResObj tmpRes = loadObj[bundleName];

            List<Object> Obj = new List<Object>();

            for (int i = 0; i < strArry.Length; i++)
            {
                List<Object> tmpObj = tmpRes.GetResObj(strArry[i]);
                Obj.AddRange(tmpObj);
            }


            if (Obj != null)
            {
                return Obj.ToArray();
            }
        }

        //表示是否加载过bundle
        if (loadHelper.ContainsKey(bundleName))
        {

            IABRelationManager loader = loadHelper[bundleName];

            List<Object> Obj = new List<Object>();
            for (int i = 0; i < strArry.Length; i++)
            {


                Object[] tmpObj = loader.GetMutiResource(strArry[i]);
                Obj.AddRange(tmpObj);

                AssetObj tmpAssetObj = new AssetObj(tmpObj);

                //缓存里面是否已经有这个包
                if (loadObj.ContainsKey(bundleName))
                {
                    AssetResObj tmpRes = loadObj[bundleName];

                    tmpRes.AddResObj(strArry[i], tmpAssetObj);
                }
                else
                {
                    AssetResObj tmpRes = new AssetResObj(strArry[i], tmpAssetObj);

                    loadObj.Add(bundleName, tmpRes);
                }
            }
            return Obj.ToArray();
        }
        else
        {
            return null;
        }
    }

    //返回Sprite
    public Sprite GetResourcesSprite(string bundleName, string resName)
    {
        //是否已经缓存了物体
        if (loadObj.ContainsKey(bundleName))
        {
            AssetResObj tmpRes = loadObj[bundleName];

            List<Object> tmpObj = tmpRes.GetResObj(resName);

            if (tmpObj != null)
            {
                return tmpObj[0] as Sprite;
            }
        }

        //表示是否加载过bundle
        if (loadHelper.ContainsKey(bundleName))
        {

            IABRelationManager loader = loadHelper[bundleName];

            Sprite tmpObj = loader.GetResourceSprite(resName);

            AssetObj tmpAssetObj = new AssetObj(tmpObj);

            //缓存里面是否已经有这个包
            if (loadObj.ContainsKey(bundleName))
            {
                AssetResObj tmpRes = loadObj[bundleName];

                tmpRes.AddResObj(resName, tmpAssetObj);
            }
            else
            {
                AssetResObj tmpRes = new AssetResObj(resName, tmpAssetObj);

                loadObj.Add(bundleName, tmpRes);
            }

            return tmpObj;
        }
        else
        {
            return null;
        }
    }



    #endregion
}
