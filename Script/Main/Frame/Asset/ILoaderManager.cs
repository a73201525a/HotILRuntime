using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILoaderManager : MonoBehaviour
{

    public static ILoaderManager Instance;


    private void Awake()
    {
        Instance = this;

        //第一步加载依赖关系 IABManifers

        StartCoroutine(IABManifestLoader.Instance.LoadMainfest());


    }

    //场景管理器
    private Dictionary<string, IABSceneManager> loadScenceManager = new Dictionary<string, IABSceneManager>();

    //读取配置文件
    public void ReadConfig(string scenceName)
    {
        if (!loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = new IABSceneManager(scenceName);

            tmpManager.ReadConfiger(scenceName);

            loadScenceManager.Add(scenceName, tmpManager);
        }
    }

    //加载回调
    public void LoadCallBack(string scenceName, string bundleName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            StartCoroutine(tmpManager.LoadAssetSys(bundleName));
        }
        else
        {
            Debug.LogError("bundle  name is not contain ==" + bundleName);
        }
    }

    //提供加载功能
    public void LoadAsset(string scenceName, string bundleName, LoaderProgrecess progress)
    {
        if (!loadScenceManager.ContainsKey(scenceName))
        {
            ReadConfig(scenceName);
        }

        IABSceneManager tmpManager = loadScenceManager[scenceName];


        tmpManager.LoadAsset(bundleName, progress, LoadCallBack);
    }


    #region 由下层API 提供功能

    public string GetBundleRetateName(string scenceName, string bundleName)
    {
        IABSceneManager tmpManager = loadScenceManager[scenceName];
        if (tmpManager != null)
        {
            return tmpManager.GetBundleReateName(bundleName);
        }
        return null;
    }

    /// <summary>
    /// 获取单个object
    /// </summary>
    /// <param name="scenceName">场景名称</param>
    /// <param name="bundleName">ab包名</param>
    /// <param name="resName">资源名称</param>
    /// <returns></returns>
    public Object GetSingleResources(string scenceName, string bundleName, string resName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            return tmpManager.GetSingleResources(bundleName, resName);
        }
        else
        {
            Debug.LogError("scenceName== " + scenceName + "bundleName==" + bundleName + "resName==" + resName + "not load");
            return null;
        }

    }

    /// <summary>
    /// 获取bundle包的全部object
    /// </summary>
    /// <param name="scenceName">场景名称</param>
    /// <param name="bundleName">ab包名</param>
    /// <param name="resName">资源名称</param>
    /// <returns></returns>
    public Object[] GetMuitResources(string scenceName, string bundleName, string resName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            return tmpManager.GetNuitResources(bundleName, resName);
        }
        else
        {
            Debug.LogError("scenceName== " + scenceName + "bundleName==" + bundleName + "resName==" + resName + "not load");
            return null;
        }

    }

    /// <summary>
    /// 获取bundle包的object
    /// </summary>
    /// <param name="scenceName">场景名称</param>
    /// <param name="bundleName">ab包名</param>
    /// <param name="resName">资源名称</param>
    /// <returns></returns>
    public Sprite GetResourcesSprite(string scenceName, string bundleName, string resName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            return tmpManager.GetResourcesSprite(bundleName, resName);
        }
        else
        {
            Debug.LogError("scenceName== " + scenceName + "bundleName==" + bundleName + "resName==" + resName + "not load");
            return null;
        }
    }

    public Dictionary<string, Sprite> GetBundleAllSprite(string scenceName, string bundleName)
    {
        Dictionary<string, Sprite> tmp = new Dictionary<string, Sprite>();
        string[] str = GetAllBundleResName(scenceName, bundleName);

        //    GetResourcesSprite()
        for (int i = 0; i < str.Length; i++)
        {
            string[] tmpstr = str[i].Split('/');
            string name = tmpstr[tmpstr.Length - 1].Remove(tmpstr[tmpstr.Length - 1].LastIndexOf("."));
            Sprite sp = GetResourcesSprite(scenceName, bundleName, name);
            if (!tmp.ContainsKey(name))
            {
                tmp.Add(name, sp);
            }
            else
            {
                Debug.Log("spriteName重复:" + name);
            }
           

        }
        return tmp;
        //if (loadScenceManager.ContainsKeyscenceName))
        //{
        //    IABSceneManager tmpManager = loadScenceManager[scenceName];

        //    return tmpManager.GetResourcesSprite(bundleName, resName);
        //}
        //else
        //{
        //    Debug.LogError("scenceName== " + scenceName + "bundleName==" + bundleName + "resName==" + resName + "not load");
        //    return null;
        //}
    }


    /// <summary>
    /// 释放某一个object
    /// </summary>
    /// <param name="scenecName"></param>
    /// <param name="bundleName"></param>
    /// <param name="res"></param>
    public void UnLoadResObj(string scenecName, string bundleName, string res)
    {
        if (loadScenceManager.ContainsKey(scenecName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenecName];

            tmpManager.DisposeResObj(bundleName, res);
        }
    }

    /// <summary>
    /// 释放整个bundle的object
    /// </summary>
    /// <param name="scenecName"></param>
    /// <param name="bundleName"></param>
    public void UnLoadBundleResObjs(string scenecName, string bundleName)
    {
        if (loadScenceManager.ContainsKey(scenecName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenecName];

            tmpManager.DisposeBundleRes(bundleName);
        }
    }
    /// <summary>
    /// 释放整个场景的Object
    /// </summary>
    /// <param name="sceneceName"></param>
    public void UnLoadAllResObjs(string sceneceName)
    {
        if (loadScenceManager.ContainsKey(sceneceName))
        {
            IABSceneManager tmpManager = loadScenceManager[sceneceName];

            tmpManager.DisposeAllRes();
        }
    }
    /// <summary>
    /// 释放一个bundle
    /// </summary>
    /// <param name="scenceName"></param>
    /// <param name="bundleName"></param>
    public void UnLoadAssetBundle(string scenceName, string bundleName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            tmpManager.DisposeBundle(bundleName);
        }
    }

    /// <summary>
    /// 释放一个场景的全部bundle
    /// </summary>
    /// <param name="scenceName"></param>
    public void UnLoadAllAssetBundle(string scenceName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            tmpManager.DisposeAllBundle();

            System.GC.Collect();
        }

    }
    /// <summary>
    /// 释放一个场景的全部bundle和object
    /// </summary>
    /// <param name="scenceName"></param>
    public void UnLoadAllAssetBundleAndResObjs(string scenceName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            tmpManager.DisposeAllBundleAndRes();

            System.GC.Collect();
        }
    }

    public string[] GetAllBundleResName(string scenceName, string bundleName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            return tmpManager.GetAllBundleResName(bundleName);
        }
        else
        {
            Debug.LogError(scenceName + "不包含：" + bundleName);
            return null;
        }

    }



    /// <summary>
    /// 调试debug
    /// </summary>
    /// <param name="scenceName"></param>
    public void DebugAllAssetBundle(string scenceName)
    {
        if (loadScenceManager.ContainsKey(scenceName))
        {
            IABSceneManager tmpManager = loadScenceManager[scenceName];

            tmpManager.DebugAllAsset();
        }
    }

    /// <summary>
    /// 是否加载完成
    /// </summary>
    /// <param name="senceName"></param>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingBundleFinish(string senceName, string bundleName)
    {

        bool tmpBool = loadScenceManager.ContainsKey(senceName);
        if (tmpBool)
        {
            IABSceneManager tmpManager = loadScenceManager[senceName];
            return tmpManager.IsLoadingFinish(bundleName);
        }

        return false;
    }

    /// <summary>
    /// 是否加载了bundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingAssetBundle(string senceName, string bundleName)
    {
        bool tmpBool = loadScenceManager.ContainsKey(senceName);
        if (tmpBool)
        {
            IABSceneManager tmpManager = loadScenceManager[senceName];
            return tmpManager.IsLoadingAssetBundle(bundleName);
        }

        return false;
    }

    #endregion



    private void OnDestroy()
    {
        loadScenceManager.Clear();
        System.GC.Collect();
    }
}
