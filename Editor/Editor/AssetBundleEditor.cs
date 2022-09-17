using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleEditor
{


    [MenuItem("Tools/MarkAssetBundle")]
    public static void MarkAssetBundle()
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();

        string path = Application.dataPath + "/Art/Scenes/";

        DirectoryInfo dir = new DirectoryInfo(path);

        FileSystemInfo[] fileInfor = dir.GetFileSystemInfos();

        for (int i = 0; i < fileInfor.Length; i++)
        {
            FileSystemInfo tmpFile = fileInfor[i];
            if (tmpFile is DirectoryInfo)
            {
                string tmpPath = Path.Combine(path, tmpFile.Name);
                SceneOverView(tmpPath);
            }
        }

        //string outPath = IPathTools.GetAssetBundlePath();
        //CopyRecord(path, outPath);
        AssetDatabase.Refresh();
    }

    public static void MarkAssetBundle(string outPath)
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();

        string path = Application.dataPath + "/Art/Scenes/";

        DirectoryInfo dir = new DirectoryInfo(path);

        FileSystemInfo[] fileInfor = dir.GetFileSystemInfos();

        for (int i = 0; i < fileInfor.Length; i++)
        {
            FileSystemInfo tmpFile = fileInfor[i];
            if (tmpFile is DirectoryInfo)
            {
                string tmpPath = Path.Combine(path, tmpFile.Name);
                SceneOverView(tmpPath);
            }
        }

        //string outPath = IPathTools.GetAssetBundlePath();
        CopyRecord(path, outPath);
        AssetDatabase.Refresh();
    }


    [MenuItem("Tools/BuildAssetBundle")]
    public static void BuildAssetBundle()
    {
        string outPath = Application.streamingAssetsPath + "/Windows";
        MarkAssetBundle(outPath);
        //Application.streamingAssetsPath + "/AssetBundle";IPathTools.GetAssetBundlePath();
        Debug.Log("outPath::" + outPath);
        BuildPipeline.BuildAssetBundles(outPath, 0, EditorUserBuildSettings.activeBuildTarget);

        MD5Helper.FileMD5(outPath);
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/BuildAssetBundleAndroid")]
    public static void BuildAssetBundleAndroid()
    {
        string outPath = Application.streamingAssetsPath + "/Android";
        MarkAssetBundle(outPath);
        //IPathTools.GetAssetBundlePath();//Application.streamingAssetsPath + "/AssetBundle";
        Debug.Log(outPath);
        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
        AssetDatabase.Refresh();
    }






    public static void CopyRecord(string surcePath, string disPath)
    {
        DirectoryInfo dir = new DirectoryInfo(surcePath);

        if (!dir.Exists)
        {
            Debug.Log("is not exit");
            return;
        }

        if (!Directory.Exists(disPath))
        {
            Directory.CreateDirectory(disPath);
        }

        FileSystemInfo[] files = dir.GetFileSystemInfos();

        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;

            if (file != null && file.Extension == ".txt")
            {
                string sourFile = surcePath + file.Name;

                string disFile = disPath + "/" + file.Name;

                File.Copy(sourFile, disFile, true);
            }
        }


    }

    public static void SceneOverView(string scenePath)
    {
        string textFileName = "Record.txt";

        string tmpPath = scenePath + textFileName;

        FileStream fs = new FileStream(tmpPath, FileMode.OpenOrCreate);

        StreamWriter bw = new StreamWriter(fs);

        Dictionary<string, string> readDict = new Dictionary<string, string>();

        ChangerHead(scenePath, readDict);

        bw.WriteLine(readDict.Count);

        foreach (string key in readDict.Keys)
        {
            bw.Write(key.ToLower());
            bw.Write(" ");
            bw.Write(readDict[key]);
            bw.Write("\n");
        }
        bw.Close();
        fs.Close();

        AssetDatabase.Refresh();
    }

    //截取相对路径  
    public static void ChangerHead(string fullPath, Dictionary<string, string> theWriter)
    {

        int tmpCount = fullPath.IndexOf("Assets");

        int tmpLenght = fullPath.Length;

        string replacePath = fullPath.Substring(tmpCount, tmpLenght - tmpCount);

        DirectoryInfo dir = new DirectoryInfo(fullPath);

        if (dir != null)
        {
            ListFiles(dir, replacePath, theWriter);
        }
        else
        {
            Debug.LogError("this path is not exit");
        }
    }

    //遍历每一个功能文件夹
    public static void ListFiles(FileSystemInfo info, string replacePath, Dictionary<string, string> theWriter)
    {
        if (!info.Exists)
        {
            Debug.LogError("is noe exit");
            return;
        }
        DirectoryInfo dir = info as DirectoryInfo;
        FileSystemInfo[] Files = dir.GetFileSystemInfos();

        for (int i = 0; i < Files.Length; i++)
        {
            FileInfo file = Files[i] as FileInfo;

            if (file != null)
            {
                ChangerMark(file, replacePath, theWriter);
            }
            else
            {
                ListFiles(Files[i], replacePath, theWriter);
            }
        }
    }

    public static string FixedWindowsPath(string path)
    {
        path = path.Replace("\\", "/");
        return path;
    }
    //计算标记值
    public static string GetBundlePath(FileInfo file, string replacePath)
    {
        string tmpPath = file.FullName;

        tmpPath = FixedWindowsPath(tmpPath);

        int assetCount = tmpPath.IndexOf(replacePath);

        assetCount += replacePath.Length + 1;

        int nameCount = tmpPath.LastIndexOf(file.Name);

        int tmpLenght = nameCount - assetCount;

        int tmpCount = replacePath.LastIndexOf("/");

        string sceneHead = replacePath.Substring(tmpCount + 1, replacePath.Length - tmpCount - 1);


        if (tmpLenght > 0)
        {
            string substring = tmpPath.Substring(assetCount, tmpPath.Length - assetCount);

            string[] result = substring.Split("/".ToCharArray());

            return sceneHead + "/" + result[0];
        }

        return sceneHead;
    }


    public static void ChangerMark(FileInfo tmpFile, string replacePath, Dictionary<string, string> theWriter)
    {
        if (tmpFile.Extension == ".meta")
        {
            return;
        }
        string markStr = GetBundlePath(tmpFile, replacePath);
        ChangeAssetMark(tmpFile, markStr, theWriter);
    }

    public static void ChangeAssetMark(FileInfo tmpFile, string markStr, Dictionary<string, string> thenWriter)
    {
        string fullPath = tmpFile.FullName;

        int assetCount = fullPath.IndexOf("Assets");

        string assetPath = fullPath.Substring(assetCount, fullPath.Length - assetCount);

        AssetImporter importer = AssetImporter.GetAtPath(assetPath);

        importer.assetBundleName = markStr;


        if (tmpFile.Extension == ".unity")
        {
            importer.assetBundleVariant = "u3d";
        }
        else
        {
            importer.assetBundleVariant = "ld";
        }

        string[] subMark = markStr.Split("/".ToCharArray());

        string modleName = "";
        if (subMark.Length > 1)
        {
            modleName = subMark[1];
        }
        else
        {
            modleName = markStr;
        }

        string modlePath = markStr.ToLower() + "." + importer.assetBundleVariant;
        if (!thenWriter.ContainsKey(modleName))
        {
            thenWriter.Add(modleName, modlePath);
        }
    }
}
