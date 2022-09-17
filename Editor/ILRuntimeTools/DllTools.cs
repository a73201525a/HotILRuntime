using ILRuntime.Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Hosting;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;

public class DllTools
{
    class DllTextAssets
    {
        public string name;
        public string[] references;
        public string[] excludePlatforms;
        public string[] includePlatforms;
        public bool allowUnsafeCode = true;
        public bool overrideReferences = false;
        public string[] precompiledReferences;
        public bool autoReferenced = true;
        public string[] defineConstraints;
        public string[] versionDefines;
    }

    //static string ThirdPartyDll = "Assets/Script/ThirdParty/Unity.ThirdParty.asmdef";
    static string Main = "Assets/Script/Main/Unity.Main.asmdef";
    static string Hotfix = "Assets/Script/Hotfix/Unity.Hotfix.asmdef";

    static string mainDllName = "main";
    static string hotfixDllName = "hotfix";

    //[MenuItem("Assets/删除dll")]
    public static void DeleteDll()
    {
        EditorApplication.LockReloadAssemblies();
        string tPath = Application.dataPath.Replace("Assets", $"{Main}");
        if (File.Exists(tPath))
        {
            Debug.Log("已删除Main.dll");
            File.Delete(tPath);
        }

        string mPath = Application.dataPath.Replace("Assets", $"{Hotfix}");
        if (File.Exists(mPath))
        {
            Debug.Log("已删除Hotfix.dll");
            File.Delete(mPath);
        }
        PlayerBuildSetting(false);
        EditorApplication.UnlockReloadAssemblies();
        CompilationPipeline.RequestScriptCompilation();
    }

    //[MenuItem("Assets/创建dll")]
    public static void CreateDll()
    {
        AssetDatabase.Refresh();
        EditorApplication.LockReloadAssemblies();
        DllTextAssets mainAssemblyDefinition = new DllTextAssets()
        {
            name = mainDllName,
            allowUnsafeCode = true,
            overrideReferences = false,
            autoReferenced = true,
            references = new string[] {  "TextAnimation", "UItimate",
                "MoreMountains.Feedbacks", "BehaviorDesigner","Cinemachine" , "Unity.RenderPipelines.Universal.Runtime" ,"Unity.RenderPipelines.Core.Runtime","kolmich", "Unity.TextMeshPro","AstarPathfindingProject","HT.SpecialEffects" }
        };
        string mainDllPath = Application.dataPath.Replace("Assets", $"{Main}");
        if (File.Exists(mainDllPath))
        {
            Debug.Log($"已包含{Main},现在进行删除...");
            File.Delete(mainDllPath);
        }
        Debug.Log($"重新创建 {Main} ...");
        using (StreamWriter sw = new StreamWriter(mainDllPath))
        {
            sw.Write(JsonUtility.ToJson(mainAssemblyDefinition));
        }


        DllTextAssets hotfixAssemblyDefinition = new DllTextAssets()
        {
            name = hotfixDllName,
            allowUnsafeCode = true,
            overrideReferences = false,
            autoReferenced = true,
            //, "Unity.Timeline"
            references = new string[] { mainDllName, "TextAnimation","UItimate", "MoreMountains.Feedbacks", "BehaviorDesigner", "Cinemachine" ,"kolmich" , "Unity.TextMeshPro" , "HT.SpecialEffects" },
            includePlatforms = new string[1] { "Editor" }
        };
        string hotfixDllPath = Application.dataPath.Replace("Assets", $"{Hotfix}");
        if (File.Exists(Hotfix))
        {
            Debug.Log($"已包含{Hotfix},现在进行删除...");
            File.Delete(Hotfix);
        }
        Debug.Log($"重新创建 {Hotfix} ...");
        using (StreamWriter sw = new StreamWriter(Hotfix))
        {
            sw.Write(JsonUtility.ToJson(hotfixAssemblyDefinition));
        }
        AssetDatabase.Refresh();

        PlayerBuildSetting(true);
        EditorApplication.UnlockReloadAssemblies();

        CompilationPipeline.RequestScriptCompilation();
        CompilationPipeline.compilationFinished += (obj) => { CreateDllConfig(); };
    }

    public static void PlayerBuildSetting(bool hotfixMode)
    {
        string[] defs;
        // PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone,out defs);
        string tmpadefs = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
        defs = tmpadefs.Split(';');
        List<string> newDefs = defs.ToList();
        string[] logic = new string[] { "ILRuntime", "DISABLE_ILRUNTIME_DEBUG" };
        //AndroidArchitecture aac = AndroidArchitecture.None;
        if (hotfixMode)
        {
            for (int i = 0; i < logic.Length; i++)
            {
                if (!newDefs.Contains(logic[i]))
                {
                    newDefs.Add(logic[i]);
                }
            }

            //aac = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        }
        else
        {

            for (int i = 0; i < logic.Length; i++)
            {
                if (newDefs.Contains(logic[i]))
                {
                    newDefs.Remove(logic[i]);
                }
            }
            //aac = AndroidArchitecture.ARMv7;
        }
        string outDefs = "";
        for (int i = 0; i < newDefs.Count; i++)
        {
            if (i == 0)
            {
                outDefs = newDefs[i];

            }
            else
            {
                outDefs = outDefs + ";" + newDefs[i];
            }
        }
#if UNITY_STANDALONE_WIN

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, outDefs);
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
#elif UNITY_ANDROID
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, outDefs);
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, hotfixMode ? ScriptingImplementation.IL2CPP : ScriptingImplementation.Mono2x);
        //PlayerSettings.Android.targetArchitectures = aac;
#elif UNITY_STANDALONE_OSX || UNITY_IPHONE
       PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, outDefs);
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
#endif
        PlayerSettings.allowUnsafeCode = true;

        //这个....根据自己项目需求裁剪....
        PlayerSettings.stripEngineCode = false;

        AssetDatabase.SaveAssets();

    }




    //生成dll版本配置
    public static void CreateDllConfig(string dllOutPath = "")
    {
        //读取
        string dllRoot = Application.dataPath + "/../Library/ScriptAssemblies/";

        string hotfixdll = Path.Combine(dllRoot, hotfixDllName + ".dll");
        string hotfixPdb = Path.Combine(dllRoot, hotfixDllName + ".pdb");

        if (string.IsNullOrEmpty(dllOutPath))
        {
            dllOutPath = Application.streamingAssetsPath + "/Dll/";//Application.dataPath + "/../Dll/";
        }

        if (Directory.Exists(dllOutPath))
        {
            Directory.Delete(dllOutPath, true);
        }
        Directory.CreateDirectory(dllOutPath);
        File.Copy(hotfixdll, Path.Combine(dllOutPath, hotfixDllName + ".dll"));
        File.Copy(hotfixPdb, Path.Combine(dllOutPath, hotfixDllName + ".pdb"));

        DllVersion dllVersion = new DllVersion();
        ReadDllInfo(dllVersion, Path.Combine(dllOutPath, hotfixDllName + ".dll"), "Dll/" + hotfixDllName + ".dll");
        ReadDllInfo(dllVersion, Path.Combine(dllOutPath, hotfixDllName + ".pdb"), "Dll/" + hotfixDllName + ".pdb");

        //AssetBundle
        CreatVersion(dllVersion);
        Debug.Log("dllVersion:::::" + dllVersion);
        string json = LitJson.JsonMapper.ToJson(dllVersion);

        File.WriteAllText(Path.Combine(dllOutPath, "dllVersion.txt"), json);
    }

    public static void ReadDllInfo(DllVersion dllVersion, string path, string FileName)
    {

        string md5 = MD5Helper.FileMD5(path);
        FileInfo fileInfo = new FileInfo(path);
        long size = fileInfo.Length;
        dllVersion.dllFile.Add(FileName, new DllConfig() { md5 = md5, size = size });
    }


    //将dll复制到沙盒目录 方便于PC进行测试
    public static void CopyDllToPersistentDataPath()
    {
        CreateDllConfig(Path.Combine(Application.persistentDataPath, "Dll"));
        Application.OpenURL(Path.Combine(Application.persistentDataPath, "Dll"));
    }


    //[MenuItem("Tools/List Player Assemblies in Console")]
    public static void PrintAssemblyNames()
    {
        UnityEngine.Debug.Log("== Player Assemblies ==");
        Assembly[] playerAssemblies =
            CompilationPipeline.GetAssemblies(AssembliesType.Player);

        foreach (var assembly in playerAssemblies)
        {
            UnityEngine.Debug.Log(assembly.name);
        }
    }

    public static void CreatVersion(DllVersion dllVersion)
    {
        string platform = IPathTools.GetPlatformFolderName(Application.platform);
        string path = Path.Combine(Application.streamingAssetsPath, platform); //IPathTools.GetPlatformFolderName(Application.platform));
        Debug.Log(path);
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);


            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                //Debug.Log("Name:" + files[i].Name);  //打印出来这个文件架下的所有文件
                //Debug.Log( "FullName:" + files[i].FullName );  
                //Debug.Log( "DirectoryName:" + files[i].DirectoryName );


                int tmpCount = files[i].FullName.IndexOf(platform);

                int tmpLenght = files[i].FullName.Length;
                string replacePath;
                //if (tmpCount < 0)
                //{
                //    replacePath = files[i].Name;
                //  //  Debug.Log(files[i].Name);
                //    ReadDllInfo(dllVersion, files[i].FullName); //Path.Combine(Application.streamingAssetsPath,replacePath));
                //    continue;
                //}

                replacePath = files[i].FullName.Substring(tmpCount, tmpLenght - tmpCount);
                string tmpstr = replacePath.Replace("\\", "/");
                ReadDllInfo(dllVersion, files[i].FullName, tmpstr);


                //  Debug.Log(replacePath);
            }
        }

    }



}
