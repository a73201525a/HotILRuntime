using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoader
{
    static TestLoader instance;
    public static TestLoader Instance
    {
        get
        {
            //Debug.Log("dddddddddddddddd");
            if (instance == null)
            {
                instance = new TestLoader();
            }
            return instance;
        }
    }
    Action<bool, UnityEngine.Object> callback;
    public void DownLoad(string url, Action<bool, UnityEngine.Object> downloadEnd)
    {
        //callback = downloadEnd;
        //StartCoroutine(WWWLoad(url, WWWcallback));
    }

    public UnityEngine.Object LoadAssetInBundleImmediately(string assetName, string bundleName)
    {
#if UNITY_EDITOR
        var allassets = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName + ".ld");
        if (allassets == null || allassets.Length <= 0)
        {
            Debug.Log("没有资源文件" + assetName);
            return null;
        }

        int assetCnt = allassets.Length;
        string assetPath = null;
        for (int i = 0; i < assetCnt; i++)
        {
            var path = allassets[i];
            if (assetName == path)
            {
                assetPath = path;
                break;
            }
            if (assetName == Path.GetFileNameWithoutExtension(path))
            {
                assetPath = path;
                break;
            }
        }
        if (assetPath == null)
        {
            Debug.Log("没有资源文件 :" + assetName + " in bundleName:" + bundleName);
            return null;
        }

        var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
        if (obj == null)
        {
            Debug.Log("load file error:" + assetName);
            return null;
        }
        return obj;
#else 
        return null;
#endif
    }


    //public IEnumerator DownloadImage(string url, Action<bool, GameObject> downloadEnd)
    //{

    //    using (UnityWebRequest pdbRequest = UnityWebRequest.Get(url))
    //    {
    //        yield return pdbRequest.SendWebRequest();
    //        if (pdbRequest.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.LogError("未加载到pdb调试数据库");
    //        }
    //        GameObject obj = (GameObject)pdbRequest.downloadHandler;
    //    }
    //    using (UnityWebRequest request = new unitywebr(url))
    //    {
    //        DownloadHandler downloadHandlerTexture = new DownloadHandler();
    //        request.downloadHandler = downloadHandlerTexture;
    //        yield return request.SendWebRequest();
    //        if (string.IsNullOrEmpty(request.error))
    //        {
    //            Texture2D localTexture = downloadHandlerTexture.texture;
    //            downloadEnd.Invoke(true, localTexture);
    //        }
    //        else
    //        {
    //            downloadEnd.Invoke(false, null);
    //            Debug.Log(request.error);
    //        }
    //    }

    //}
}
