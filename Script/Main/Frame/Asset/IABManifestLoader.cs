using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IABManifestLoader
{
    public AssetBundleManifest assetManifest;

    public string manifestPath;

    public AssetBundle manifeseLoader;

    private bool isLoadFinsh;


    private static IABManifestLoader instance = null;

    public static IABManifestLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new IABManifestLoader();
            }
            return instance;
        }
    }

    public void SetManifestPath(string path)
    {
        manifestPath = path;
    }

    public IABManifestLoader()
    {
        assetManifest = null;

        manifeseLoader = null;

        isLoadFinsh = false;

        manifestPath = IPathTools.GetWWWAssetBundlePath() + "/" + IPathTools.GetPlatformFolderName(Application.platform);
    }

    public IEnumerator LoadMainfest()
    {
        // Debug.Log("manifestPath==" + manifestPath);
        //  UnityWebRequest mainfest = new UnityWebRequest(manifestPath);
        Debug.Log("manifestPath::" + manifestPath);
        UnityWebRequest mainfest = UnityWebRequestAssetBundle.GetAssetBundle(manifestPath);

        yield return mainfest.SendWebRequest();

        if (!string.IsNullOrEmpty(mainfest.error))
        {
#if !UNITY_EDITOR
            Debug.Log(mainfest.error);
#endif
        }
        else
        {
            // Debug.Log("mainfest.downloadProgress==" + mainfest.downloadProgress);
            if (mainfest.downloadProgress >= 1.0f)
            {
                // AssetBundle bundle = (mainfest.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(mainfest);

                manifeseLoader = bundle;

                assetManifest = manifeseLoader.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

                isLoadFinsh = true;

                Debug.Log("Manifest finish");
            }
        }
;
    }

    public string[] GetDepences(string name)
    {
        return assetManifest.GetAllDependencies(name);
    }

    public void UnloadManifest()
    {
        manifeseLoader.Unload(true);
    }

    public bool IsLoadFinsh()
    {
        return isLoadFinsh;
    }

}
