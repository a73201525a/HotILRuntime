using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IABSceneManager
{
    private IABManager abManager;

    private Dictionary<string, string> allAssets;

    public IABSceneManager(string sceneName)
    {
        abManager = new IABManager(sceneName);
    }



    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="fileName">场景名称</param>
    public void ReadConfiger(string sceneName)
    {
        string textFileName = "Record.txt";
        string path = IPathTools.GetAssetBundlePath() + "/" + sceneName + textFileName;
        Debug.Log("path::" + path);
        allAssets = new Dictionary<string, string>();

        abManager = new IABManager(sceneName);

        ReadCongfig(path);
    }

    private void ReadCongfig(string path)
    {
        //bw.WriteLine(readDict.Count);

        //foreach (string key in readDict.Keys)
        //{
        //    bw.Write(key);
        //    bw.Write(" ");
        //    bw.Write(readDict[key]);
        //    bw.Write("\n");

        //}
        FileStream fs = new FileStream(path, FileMode.Open);

        StreamReader br = new StreamReader(fs);

        string line = br.ReadLine();
        int allCount = int.Parse(line);

        for (int i = 0; i < allCount; i++)
        {
            string tmpStr = br.ReadLine();

            string[] tmpArr = tmpStr.Split(" ".ToCharArray());

            //Debug.Log("tmpArr[0]==" + tmpArr[0]);
            //Debug.Log("tmpArr[1]==" + tmpArr[1]);

            allAssets.Add(tmpArr[0], tmpArr[1]);
        }

        fs.Close();
        br.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="progress"></param>
    /// <param name="callback"></param>
    public void LoadAsset(string bundleName, LoaderProgrecess progress, LoadAssetBundleCallBack callback)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            string tmpValue = allAssets[bundleName];

            abManager.LoadAssetBundle(tmpValue, progress, callback);
        }
        else
        {
            Debug.Log("dont contain the bundle ===" + bundleName);
        }
    }

    public string GetBundleReateName(string bundleName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            return allAssets[bundleName];
        }
        else
        {
            return null;
        }
    }

    #region 由下层提供功能


    public IEnumerator LoadAssetSys(string bundleName)
    {
        yield return abManager.LoadAssetBundles(bundleName);
    }

    public Object GetSingleResources(string bundleName, string resName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            return abManager.GetSingleResources(allAssets[bundleName], resName);
        }
        else
        {
            Debug.Log("dont contain the bundle ===" + bundleName);
            return null;
        }
    }

    public Object[] GetNuitResources(string bundleName, string resName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            return abManager.GetMuitleResources(allAssets[bundleName], resName);
        }
        else
        {
            Debug.Log("dont contain the bundle ===" + bundleName);
            return null;
        }
    }

    public Sprite GetResourcesSprite(string bundleName, string resName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            return abManager.GetResourcesSprite(allAssets[bundleName], resName);
        }
        else
        {
            Debug.Log("dont contain the bundle ===" + bundleName);
            return null;
        }
    }

    /// <summary>
    /// 释放单个资源
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="resName"></param>
    public void DisposeResObj(string bundleName, string resName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            abManager.DisposeResObj(allAssets[bundleName], resName);
        }
        else
        {
            Debug.Log("dont contain the bundle ===" + bundleName);

        }
    }

    public void DisposeBundleRes(string bundleName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            abManager.DisposeResObj(allAssets[bundleName]);
        }
        else
        {
            Debug.Log("dont contain the bundle ===" + bundleName);

        }
    }

    public void DisposeAllRes()
    {
        abManager.DisposeAllObj();
    }

    public void DisposeBundle(string bundleName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            abManager.DisposeBundle(allAssets[bundleName]);
        }
    }

    public void DisposeAllBundle()
    {
        abManager.DisposeAllBundle();

        allAssets.Clear();
    }

    public void DisposeAllBundleAndRes()
    {
        abManager.DisposeAllBundleAndRes();

        allAssets.Clear();


    }

    /// <summary>
    /// 获取ab包的所有资源名字
    /// </summary>
    /// <param name="bundlename"></param>
    /// <returns></returns>


    public string[] GetAllBundleResName(string bundlename)
    {
        return abManager.GetAllBundleResName(allAssets[bundlename]);
    }
    public void DebugAllAsset()
    {
        List<string> keys = new List<string>();

        keys.AddRange(allAssets.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            abManager.DebugAssetBundle(allAssets[keys[i]]);
        }
    }

    /// <summary>
    /// 是否加载完成
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingFinish(string bundleName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            return abManager.IsLoadingFinish(allAssets[bundleName]);
        }
        else
        {
            Debug.Log("is not contain bundle ==" + bundleName);
        }
        return false;
    }
    /// <summary>
    /// 是否加载过assetbundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingAssetBundle(string bundleName)
    {
        if (allAssets.ContainsKey(bundleName))
        {
            return abManager.IsLoadingAssetBundle(allAssets[bundleName]);
        }
        else
        {
            Debug.Log("is not contain bundle ==" + bundleName);
        }
        return false;
    }

    #endregion

}
