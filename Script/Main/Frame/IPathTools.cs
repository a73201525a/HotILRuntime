using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IPathTools
{
    public static string GetPlatformFolderName(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "Android";

            case RuntimePlatform.IPhonePlayer:
                return "IOS";

            case RuntimePlatform.WindowsPlayer:
                return "Windows";

            case RuntimePlatform.WindowsEditor:
                return "Windows";

            case RuntimePlatform.OSXPlayer:
                return "OSX";

            case RuntimePlatform.OSXEditor:
                return "OSX";

            default:
                return null;
        }
    }

    public static string GetAppFilePath()
    {
        string tmpPath = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            tmpPath = Application.streamingAssetsPath + "/";
        }
        else
        {
            tmpPath = Application.persistentDataPath;
        }
        return tmpPath;


    }

    public static string GetAssetBundlePath()
    {
#if UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, GetPlatformFolderName(Application.platform) + "/");

#else
        string platFolder = GetPlatformFolderName(Application.platform);

        string allPath = Path.Combine(GetAppFilePath(), platFolder);

        return allPath;
#endif
    }
    /// <summary>
    /// 获取热更代码路径
    /// </summary>
    /// <returns></returns>
    public static string GetDllPath()
    {
        string tmpStr = "";

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            tmpStr = GetAppFilePath();
        }
        else
        {
            string tmpPath = GetAppFilePath();

#if UNITY_ANDROID
            tmpStr = "file://" + tmpPath;
#elif UNITY_STANDALONE_WIN
            tmpStr = "file:///" + tmpPath;
#else
            tmpStr = "file://"+tmpPath;
#endif
        }

        return tmpStr;
    }


    public static string GetWWWAssetBundlePath()
    {

        string tmpStr = "";

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            tmpStr = "file:///" + GetAssetBundlePath();
        }
        else
        {
            string tmpPath = GetAssetBundlePath();

#if UNITY_ANDROID
            tmpStr = "file://" + tmpPath;
#elif UNITY_STANDALONE_WIN
            tmpStr = "file:///" + tmpPath;
#else
            tmpStr = "file://"+tmpPath;
#endif
        }

        return tmpStr;
    }

}
