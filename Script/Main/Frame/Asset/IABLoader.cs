using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//每帧回调进度
public delegate void LoaderProgrecess(string bundle, float Process);
//加载完成回调一次
public delegate void LoadFinish(string bundle);

public class IABLoader
{
    private string bundleName;//资源名称

    private string commonBundlePath;//资源路径

    private UnityWebRequest commonLoader;

    private float commnResLoaderProcess;//加载进度

    private LoaderProgrecess loadProgress;//进度回调

    private LoadFinish loadFinish;//加载完成回调

    private IABResLoader abResLoader;

    public IABLoader(LoaderProgrecess Progress, LoadFinish Finish)
    {
        bundleName = "";
        commonBundlePath = "";
        commnResLoaderProcess = 0;
        loadProgress = Progress;
        loadFinish = Finish;
        abResLoader = null;
    }

    //设置包名
    public void SetBundleName(string bundle)
    {
        this.bundleName = bundle;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path">完整路径</param>
    public void LoadResources(string path)
    {
        commonBundlePath = path;
    }

    //协程加载
    public IEnumerator CommonLoad()
    {
        //UnityWebRequest
        //commonLoader = new UnityWebRequest(commonBundlePath);
        // Debug.Log("commonBundlePath===" + commonBundlePath);
        commonLoader = UnityWebRequestAssetBundle.GetAssetBundle(commonBundlePath);

        //AsyncOperation request = commonLoader.SendWebRequest();
        // WWW commonL = new WWW(commonBundlePath);
        while (!commonLoader.isDone)
        {
            //commnResLoaderProcess = request.progress;//commonLoader.downloadProgress;
            commnResLoaderProcess = commonLoader.downloadProgress;
            //Debug.Log("commonLoader.downloadProgress==" + request.progress);

            if (loadProgress != null)
            {
                loadProgress(bundleName, commnResLoaderProcess);
            }
            yield return commonLoader.SendWebRequest();

            commnResLoaderProcess = commonLoader.downloadProgress;

        }

        if (commnResLoaderProcess >= 1.0f)
        {

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(commonLoader);

            abResLoader = new IABResLoader(bundle);

            if (loadProgress != null) loadProgress(bundleName, commnResLoaderProcess);

            if (loadFinish != null) this.loadFinish(bundleName);

            //  DebugerLoader();

        }
        else
        {
            Debug.LogError("load bundle error ==" + bundleName);
        }

        commonLoader = null;
    }


    #region  下层提供功能


    public string[] GetAllBundleResName()
    {
        return abResLoader.GetAllBundleResName();
    }

    public void DebugerLoader()
    {
        if (commonLoader != null && abResLoader != null)
        {
            abResLoader.DebugAllRes();
        }
        else
        {
            Debug.Log("commonLoader null");
        }
    }
    //获取图片
    public Sprite GetResourcesSprite(string name)
    {
        if (abResLoader != null) return abResLoader.LoadResSprite(name);


        return null;
    }

    //获取单个资源
    public UnityEngine.Object GetResources(string name)
    {
        if (abResLoader != null) return abResLoader[name];

        return null;
    }
    //获取多个资源
    public UnityEngine.Object[] GetMutiResources(string name)
    {
        if (abResLoader != null) return abResLoader.LoadResources(name);


        return null;
    }
    //卸载单个资源
    public void UnLoadAssetBundleRes(UnityEngine.Object tmpObj)
    {
        if (abResLoader != null) abResLoader.UnLoadRes(tmpObj);
    }

    public void Dispose()
    {
        if (abResLoader != null)
        {
            abResLoader.Dispose();
            abResLoader = null;
        }
    }

    #endregion
}
