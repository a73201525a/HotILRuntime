using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单个AB包管理
public class IABRelationManager
{
    //存储AB依赖关系
    List<string> depedenceBundle = null;
    //存储AB被依赖关系
    List<string> referBundle = null;

    IABLoader assetLoader;

    //自己的包名
    string thisBundleName;
    //加载进度
    LoaderProgrecess loaderProgress;

    public IABRelationManager()
    {
        depedenceBundle = new List<string>();
        referBundle = new List<string>();
    }

    public void AddRefference(string bundleName)
    {
        referBundle.Add(bundleName);
    }
    /// <summary>
    /// 移除依赖关系
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns>是否释放内存</returns>
    public bool RemoveRefference(string bundleName)
    {
        for (int i = 0; i < referBundle.Count; i++)
        {
            if (bundleName.Equals(referBundle[i]))
            {
                referBundle.RemoveAt(i);
            }
        }
        if (referBundle.Count <= 0)
        {
            Dispose();
            return true;
        }
        return false;
    }

    public List<string> GetRefference()
    {
        return referBundle;
    }
    /// <summary>
    /// 设置依赖关系
    /// </summary>
    /// <param name="depence"></param>
    public void SetDepedences(string[] depence)
    {
        if (depence.Length > 0)
        {
            depedenceBundle.AddRange(depence);
        }
    }

    /// <summary>
    /// 获取依赖关系
    /// </summary>
    /// <param name="depence"></param>
    public List<string> GetDepedences()
    {
        return depedenceBundle;

    }

    /// <summary>
    /// 移除依赖关系
    /// </summary>
    /// <param name="depence"></param>
    public void RemoveDepedences(string bundleName)
    {
        for (int i = 0; i < depedenceBundle.Count; i++)
        {
            if (bundleName.Equals(referBundle[i]))
            {
                depedenceBundle.RemoveAt(i);
            }
        }

    }
    bool isLoadFinish;
    //加载完成回调
    public void BunldLoadFinish(string bundleName)
    {
        isLoadFinish = true;
    }
    public bool IsBundleLoadFinish()
    {
        return isLoadFinish;
    }
    public void Init(string bundleName, LoaderProgrecess progress)
    {
        isLoadFinish = false;
        thisBundleName = bundleName;
        loaderProgress = progress;
        assetLoader = new IABLoader(progress, BunldLoadFinish);

        assetLoader.SetBundleName(bundleName);

        string bundlePath = IPathTools.GetWWWAssetBundlePath() + "/" + bundleName;

        assetLoader.LoadResources(bundlePath);
    }

    public string GeBundleName()
    {
        return thisBundleName;
    }

    public LoaderProgrecess GetProgress()
    {
        return loaderProgress;
    }


    #region 下层提供API

    public string[] GetAllBundleResName()
    {
        return assetLoader.GetAllBundleResName();
    }
    public void DebugAsset()
    {
        if (assetLoader != null)
        {
            assetLoader.DebugerLoader();
        }
        else
        {
            Debug.Log("asset load is null");
        }
    }

    public IEnumerator LoadAssetBundle()
    {
        yield return assetLoader.CommonLoad();
    }

    public void Dispose()
    {
        assetLoader.Dispose();
    }

    public Object GetSingleResource(string bundleName)
    {
        return assetLoader.GetResources(bundleName);
    }

    public Object[] GetMutiResource(string bundleName)
    {
        return assetLoader.GetMutiResources(bundleName);
    }

    public Sprite GetResourceSprite(string bundleName)
    {
        return assetLoader.GetResourcesSprite(bundleName);
    }


    #endregion
}
