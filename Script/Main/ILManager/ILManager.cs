using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Debugger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ILManager : MonoBehaviour
{
    // public string strAndroidStreamingAssetsPath = Application.dataPath + "!/assets" + "/Android";
    public string versionName = "dllVersion.txt";
    public string server = "";//远程服务器地址 测试模式下 就将它设置为本地沙盒目录就可以了
    public string local = "";//本地缓存目录
    public bool isDebug = true;
    public string hotfixdllName = "hotfix.dll";
    public string hotfixpdbName = "hotfix.pdb";

    DllVersion localDllVersion;
    DllVersion serverDllVersion;

    List<string> waitDownloadTasks = new List<string>();
    Dictionary<string, bool> downloaded = new Dictionary<string, bool>();

    public string localVersion;//本地的版本信息

    /// <summary> 更新dll的配置 </summary>
    public void UpdateConfig()
    {


    }


    /// <summary> 初始化 </summary>
    public void Init()
    {

        local = Path.Combine(Application.persistentDataPath, "Dll");
        //server = local;//测试
        //未包含保存dll的目录 则创建一个
        if (!Directory.Exists(local))
        {
            Directory.CreateDirectory(local);
        }

        localVersion = Path.Combine(local, versionName);
        //已经包含本地文件了
        if (File.Exists(localVersion))
        {
            this.localDllVersion = LitJson.JsonMapper.ToObject<DllVersion>(File.ReadAllText(localVersion));
        }
    }



    /// <summary> 获取远端dll的版本 </summary>
    public IEnumerator GetServerDllVersion()
    {

        while (serverDllVersion == null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(server, versionName)))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.responseCode != 200)
                {
                    Debug.LogError("请求失败");

                    //可以弹窗之类的提示 然后通过以下语法阻塞界面 
                    //bool reGet = false;
                    //yield return new WaitUntil((() => reGet));

                    yield return new WaitForSeconds(1);
                }
                else
                {
                    Debug.Log($"Get Content:{webRequest.downloadHandler.text}");
                    serverDllVersion = LitJson.JsonMapper.ToObject<DllVersion>(webRequest.downloadHandler.text);
                }
            }
        }
    }


    /// <summary> 进行比较 </summary>
    public void CheckIsUpdate()
    {
        if (serverDllVersion == null)
        {
            Debug.LogError("请先请求到服务器的文件配置");
            return;
        }

        for (int i = 0; i < serverDllVersion.dllFile.Count; i++)
        {
            var item = serverDllVersion.dllFile.ElementAt(i);
            if (localDllVersion == null)
            {
                waitDownloadTasks.Add(item.Key);
            }
            else
            {
                //if (item.Value.md5!=MD5Helper.FileMD5( Path.Combine(dllSaveDirectory, item.Key)))
                if (item.Value.md5 != localDllVersion.dllFile[item.Key].md5)
                {
                    waitDownloadTasks.Add(item.Key);
                }
                else
                {
                    Debug.Log($"{item.Key} md5 一致!");
                }
            }
        }
    }


    /// <summary> 开始下载任务 </summary>
    public IEnumerator DownloadTasks()
    {
        if (waitDownloadTasks.Count != 0)
        {
            for (int i = 0; i < waitDownloadTasks.Count; i++)
            {
                downloaded.Add(waitDownloadTasks[i], false);
                StartCoroutine(CreateDowloadTask(waitDownloadTasks[i]));
            }
            yield return new WaitUntil(() => downloaded.Values.ToList().TrueForAll(o => { return o; }));
            downloaded.Clear();
            //最后记得将版本文件保存起来
            File.WriteAllText(localVersion, LitJson.JsonMapper.ToJson(serverDllVersion));
        }
    }

    /// <summary> 创建下载任务</summary> 
    public IEnumerator CreateDowloadTask(string file)
    {
        while (true)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(server, file)))
            {
                yield return webRequest.SendWebRequest();

                //if (webRequest.result != UnityWebRequest.Result.Success)
                //{
                //    Debug.LogError($"{file}请求失败,等待1秒重新请求 避免卡死!!!  {webRequest.result}");
                //    yield return new WaitForSeconds(1);
                //}
                if (webRequest.responseCode != 200)
                {
                    Debug.LogError($"{file}请求失败,等待1秒重新请求 避免卡死!!!  {webRequest.downloadProgress}");
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    //写入到本地去 
                    File.WriteAllBytes(Path.Combine(local, file), webRequest.downloadHandler.data);
                    downloaded[file] = true;
                    break;
                }
            }
        }

    }

    public ILRuntime.Runtime.Enviorment.AppDomain Appdomain
    {
        get
        {
            return appdomain;
        }
    }
    ILRuntime.Runtime.Enviorment.AppDomain appdomain;
    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;
    /// <summary> 加载HotfixDll</summary> 
    public IEnumerator LoadHotfixDll()
    {
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
        //local = @"D:\UnityPorject\Poject1\Test2\Dll";

        ////////要做平台适配
        //  local = "file://" + local;
        local = Path.Combine(IPathTools.GetDllPath(), "Dll");
        Debug.Log("local:::" + Path.Combine(local, hotfixdllName));
        using (UnityWebRequest dllRequest = UnityWebRequest.Get(Path.Combine(local, hotfixdllName)))
        {
            yield return dllRequest.SendWebRequest();
            //如果不是完成状态
            if (dllRequest.responseCode != 200)
            {
                Debug.LogError("未加载到热更dll");
            }
            else
            {
                Debug.Log("读取DLL成功");
                fs = new MemoryStream(dllRequest.downloadHandler.data);
                if (isDebug)
                {
                    using (UnityWebRequest pdbRequest = UnityWebRequest.Get(Path.Combine(local, hotfixpdbName)))
                    {
                        yield return pdbRequest.SendWebRequest();
                        if (pdbRequest.responseCode != 200)
                        {
                            Debug.LogError("未加载到pdb调试数据库");
                        }
                        p = new MemoryStream(pdbRequest.downloadHandler.data);
                    }
                }
            }

            try
            {
                if (isDebug)
                {
                    appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                }
                else
                {
                    appdomain.LoadAssembly(fs, null, null);
                }

            }
            catch (Exception e)
            {
                Debug.LogError($"appdomain LoadAssembly Error:{e.Message}");
            }

            InitializeILRuntime();
            OnHotFixLoaded();
        }


    }

    /// <summary> 初始化ILRuntime </summary>
    public void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册

        ILAdaptor.RegisterAdaptor(appdomain);//跨域继承适配器的注册
        ILDelegate.RegisterDelegate(appdomain);//委托适配器

        //LitJson重定向
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);

        RegisterCLRMethod();


        //这个是只有生成了绑定代码之后 才能够调用的
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
    }

    /// <summary> 走入热更代码的入口 </summary>
    void OnHotFixLoaded()
    {

        appdomain.Invoke("Hotfix.HotfixApplication", "Main", null, null);

    }
    /// <summary>
    /// 子弹测试场景
    /// </summary>
    /// <param name="objs"></param>
    public void CreatObjer(params object[] objs)
    {
        appdomain.Instantiate("Bullet", objs);
    }


    private void OnDestroy()
    {
        if (fs != null)
            fs.Close();
        if (p != null)
            p.Close();
        fs = null;
        p = null;
    }



    /// <summary>
    /// 重定向Log方法
    /// </summary>
    unsafe void RegisterCLRMethod()
    {
        var mi = typeof(Debug).GetMethod("Log", new System.Type[] { typeof(object) });
        appdomain.RegisterCLRMethodRedirection(mi, Log_11);

    }
    unsafe static StackObject* Log_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {

        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;

        StackObject* __ret = ILIntepreter.Minus(__esp, 1);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);


        object message = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));

        __intp.Free(ptr_of_this_method);

        var stacktrace = __domain.DebugService.GetStackTrace(__intp);//GetStackTrance(__intp);

        UnityEngine.Debug.Log(message + "\n" + stacktrace);

        return __ret;
    }


    #region 加载streamingasset资源
    /// <summary>
    /// 首次启动加载资源到persistentDataPath文件夹
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetLocalDllVersion()
    {
        //string path = Path.Combine(Application.streamingAssetsPath, IPathTools.GetPlatformFolderName(Application.platform));
        //Debug.Log(path);
        //if (Directory.Exists(path))
        //{
        //    DirectoryInfo direction = new DirectoryInfo(path);
        //    FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

        //    //Debug.Log(files.Length);

        //    for (int i = 0; i < files.Length; i++)
        //    {

        //        if (files[i].Name.EndsWith(".meta"))
        //        {
        //            continue;
        //        }

        DllVersion version = new DllVersion();
        string verPath = Path.Combine(Application.streamingAssetsPath, "Dll/dllVersion.txt");

        Debug.Log("verPath::" + verPath);
        //加载版本文件
        using (UnityWebRequest webRequest = UnityWebRequest.Get(verPath))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.responseCode != 200)
            {
                Debug.Log("加载version失败");
                Debug.Log(webRequest.error);
                yield return new WaitForSeconds(1);
            }
            else
            {
                version = LitJson.JsonMapper.ToObject<DllVersion>(webRequest.downloadHandler.text);
                Debug.Log("读取Vresion：：" + version);
            }
        }

        for (int i = 0; i < version.dllFile.Count; i++)
        {

            var item = version.dllFile.ElementAt(i);

            string[] strArrty = item.Key.Split('/');
            //Debug.Log("111"+strArrty[0]);

            if (strArrty.Length > 1)
            {
                string dir = "";

                for (int j = 0; j < strArrty.Length - 1; j++)
                {
                    dir += strArrty[j] + "/";
                    //Debug.Log("strArrty[i]:::"+strArrty[j]);
                }

                string directoryPath = Path.Combine(Application.persistentDataPath, dir);

                //Debug.Log("directoryPath::" + directoryPath);
                //   Debug.Log("directoryPath::Creat::" + Directory.Exists(directoryPath));

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

            }


            string resPath = Path.Combine(Application.streamingAssetsPath, item.Key);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(resPath))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.responseCode != 200)
                {
                    Debug.LogError("请求失败");

                    //可以弹窗之类的提示 然后通过以下语法阻塞界面 
                    //bool reGet = false;
                    //yield return new WaitUntil((() => reGet));

                    yield return new WaitForSeconds(1);
                }
                else
                {

                    //string outPath11 = CutPath(resPath);

                    // Debug.Log("outPath::" + item.Key);
                    // Debug.Log("创建：：" + Path.Combine(Application.persistentDataPath, item.Key));
                    File.WriteAllBytes(Path.Combine(Application.persistentDataPath, item.Key), webRequest.downloadHandler.data);
                    //downloaded["MonsterRecord.txt"] = true;
                    //  Debug.Log("File::"+File.Exists(Path.Combine(Application.persistentDataPath, item.Key)));
                    continue;
                }
            }

        }


    }


    string CutPath(string cutPath)
    {
        int tmpCount = cutPath.IndexOf("Android");

        int tmpLenght = cutPath.Length;
        string replacePath;
        if (tmpCount < 0)
        {
            replacePath = cutPath;
            //  Debug.Log(files[i].Name);
            return replacePath;
        }

        replacePath = cutPath.Substring(tmpCount, tmpLenght - tmpCount);

        return replacePath;
    }
}

#endregion

