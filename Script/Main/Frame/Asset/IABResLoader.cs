using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IABResLoader : IDisposable
{
    private AssetBundle ABRes;

    public IABResLoader(AssetBundle tmpBundle)
    {
        ABRes = tmpBundle;
    }

    public Sprite LoadResSprite(string resName)
    {
        if (this.ABRes == null || !this.ABRes.Contains(resName))
        {
            Debug.Log("res not contain:" + resName);

            return null;
        }
       
        Texture2D imgTexture =  this.ABRes.LoadAsset(resName) as Texture2D;
        Sprite sp = Sprite.Create(imgTexture, new Rect(0, 0, imgTexture.width, imgTexture.height), new Vector2(0.5f, 0.5f));
        sp.name = imgTexture.name;
        return sp;
    }


    /// <summary>
    /// 加载单个资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object this[string resName]
    {

        get
        {
            if (this.ABRes == null || !this.ABRes.Contains(resName))
            {
                Debug.Log("res not contain:" + resName);

                return null;
            }
            return ABRes.LoadAsset(resName);
        }

    }

    /// <summary>
    /// 加载多个资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object[] LoadResources(string resName)
    {

        if (this.ABRes == null || !this.ABRes.Contains(resName))
        {
            Debug.Log("res not contain:" + resName);

            return null;
        }

        return this.ABRes.LoadAssetWithSubAssets(resName);

    }

    /// <summary>
    /// 卸载单个资源
    /// </summary>
    /// <param name="resObj"></param>
    public void UnLoadRes(UnityEngine.Object resObj)
    {
        Resources.UnloadAsset(resObj);
    }

    /// <summary>
    /// 释放assetbundle包
    /// </summary>
    public void Dispose()
    {
        if (ABRes == null) return;

        ABRes.Unload(false);
    }


    public string[] GetAllBundleResName()
    {
        string[] tmpAssetName = ABRes.GetAllAssetNames();
        return tmpAssetName;
    }

    public void DebugAllRes()
    {
        string[] tmpAssetName = ABRes.GetAllAssetNames();

        for (int i = 0; i < tmpAssetName.Length; i++)
        {
            Debug.LogWarning("ABRes Contain Asset Name ==" + tmpAssetName[i]);
        }
    }
}
