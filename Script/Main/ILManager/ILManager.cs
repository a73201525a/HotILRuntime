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
    public string server = "";//Զ�̷�������ַ ����ģʽ�� �ͽ�������Ϊ����ɳ��Ŀ¼�Ϳ�����
    public string local = "";//���ػ���Ŀ¼
    public bool isDebug = true;
    public string hotfixdllName = "hotfix.dll";
    public string hotfixpdbName = "hotfix.pdb";

    DllVersion localDllVersion;
    DllVersion serverDllVersion;

    List<string> waitDownloadTasks = new List<string>();
    Dictionary<string, bool> downloaded = new Dictionary<string, bool>();

    public string localVersion;//���صİ汾��Ϣ

    /// <summary> ����dll������ </summary>
    public void UpdateConfig()
    {


    }


    /// <summary> ��ʼ�� </summary>
    public void Init()
    {

        local = Path.Combine(Application.persistentDataPath, "Dll");
        //server = local;//����
        //δ��������dll��Ŀ¼ �򴴽�һ��
        if (!Directory.Exists(local))
        {
            Directory.CreateDirectory(local);
        }

        localVersion = Path.Combine(local, versionName);
        //�Ѿ����������ļ���
        if (File.Exists(localVersion))
        {
            this.localDllVersion = LitJson.JsonMapper.ToObject<DllVersion>(File.ReadAllText(localVersion));
        }
    }



    /// <summary> ��ȡԶ��dll�İ汾 </summary>
    public IEnumerator GetServerDllVersion()
    {

        while (serverDllVersion == null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(server, versionName)))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.responseCode != 200)
                {
                    Debug.LogError("����ʧ��");

                    //���Ե���֮�����ʾ Ȼ��ͨ�������﷨�������� 
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


    /// <summary> ���бȽ� </summary>
    public void CheckIsUpdate()
    {
        if (serverDllVersion == null)
        {
            Debug.LogError("�������󵽷��������ļ�����");
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
                    Debug.Log($"{item.Key} md5 һ��!");
                }
            }
        }
    }


    /// <summary> ��ʼ�������� </summary>
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
            //���ǵý��汾�ļ���������
            File.WriteAllText(localVersion, LitJson.JsonMapper.ToJson(serverDllVersion));
        }
    }

    /// <summary> ������������</summary> 
    public IEnumerator CreateDowloadTask(string file)
    {
        while (true)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(server, file)))
            {
                yield return webRequest.SendWebRequest();

                //if (webRequest.result != UnityWebRequest.Result.Success)
                //{
                //    Debug.LogError($"{file}����ʧ��,�ȴ�1���������� ���⿨��!!!  {webRequest.result}");
                //    yield return new WaitForSeconds(1);
                //}
                if (webRequest.responseCode != 200)
                {
                    Debug.LogError($"{file}����ʧ��,�ȴ�1���������� ���⿨��!!!  {webRequest.downloadProgress}");
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    //д�뵽����ȥ 
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
    /// <summary> ����HotfixDll</summary> 
    public IEnumerator LoadHotfixDll()
    {
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();

        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ����������ڻ��������ڴ棬��ʽ����ʱ�뽫PDBȥ��������LoadAssembly��ʱ��pdb��null����
        //local = @"D:\UnityPorject\Poject1\Test2\Dll";

        ////////Ҫ��ƽ̨����
        //  local = "file://" + local;
        local = Path.Combine(IPathTools.GetDllPath(), "Dll");
        Debug.Log("local:::" + Path.Combine(local, hotfixdllName));
        using (UnityWebRequest dllRequest = UnityWebRequest.Get(Path.Combine(local, hotfixdllName)))
        {
            yield return dllRequest.SendWebRequest();
            //����������״̬
            if (dllRequest.responseCode != 200)
            {
                Debug.LogError("δ���ص��ȸ�dll");
            }
            else
            {
                Debug.Log("��ȡDLL�ɹ�");
                fs = new MemoryStream(dllRequest.downloadHandler.data);
                if (isDebug)
                {
                    using (UnityWebRequest pdbRequest = UnityWebRequest.Get(Path.Combine(local, hotfixpdbName)))
                    {
                        yield return pdbRequest.SendWebRequest();
                        if (pdbRequest.responseCode != 200)
                        {
                            Debug.LogError("δ���ص�pdb�������ݿ�");
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

    /// <summary> ��ʼ��ILRuntime </summary>
    public void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //����Unity��Profiler�ӿ�ֻ���������߳�ʹ�ã�Ϊ�˱�����쳣����Ҫ����ILRuntime���̵߳��߳�ID������ȷ���������к�ʱ�����Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //������һЩILRuntime��ע��

        ILAdaptor.RegisterAdaptor(appdomain);//����̳���������ע��
        ILDelegate.RegisterDelegate(appdomain);//ί��������

        //LitJson�ض���
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);

        RegisterCLRMethod();


        //�����ֻ�������˰󶨴���֮�� ���ܹ����õ�
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
    }

    /// <summary> �����ȸ��������� </summary>
    void OnHotFixLoaded()
    {

        appdomain.Invoke("Hotfix.HotfixApplication", "Main", null, null);

    }
    /// <summary>
    /// �ӵ����Գ���
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
    /// �ض���Log����
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


    #region ����streamingasset��Դ
    /// <summary>
    /// �״�����������Դ��persistentDataPath�ļ���
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
        //���ذ汾�ļ�
        using (UnityWebRequest webRequest = UnityWebRequest.Get(verPath))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.responseCode != 200)
            {
                Debug.Log("����versionʧ��");
                Debug.Log(webRequest.error);
                yield return new WaitForSeconds(1);
            }
            else
            {
                version = LitJson.JsonMapper.ToObject<DllVersion>(webRequest.downloadHandler.text);
                Debug.Log("��ȡVresion����" + version);
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
                    Debug.LogError("����ʧ��");

                    //���Ե���֮�����ʾ Ȼ��ͨ�������﷨�������� 
                    //bool reGet = false;
                    //yield return new WaitUntil((() => reGet));

                    yield return new WaitForSeconds(1);
                }
                else
                {

                    //string outPath11 = CutPath(resPath);

                    // Debug.Log("outPath::" + item.Key);
                    // Debug.Log("��������" + Path.Combine(Application.persistentDataPath, item.Key));
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

