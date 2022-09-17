using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AssetBase
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GameObject.Find("Canvas").gameObject);
        DontDestroyOnLoad(GameObject.Find("FPSHUD"));
        
        //DontDestroyOnLoad(GameObject.Find("Hex Grid"));
        //DontDestroyOnLoad(GameObject.Find("Map"));
        gameObject.AddComponent<MsgCenter>();
        //Camera.main.transform.position = new Vector3(0, 0, -10);

        // AssetBundle.CreatFromsFile
        Init();
    }

    private void Init()
    {


        // Debug.Log(path);
        //检测是否需要更新
#if NOTHOT
        Debug.Log("调试模式");
        StartCoroutine(NotHotfixStart());
#else
        StartCoroutine(HotfixStart());
#endif
        //加载ab包
        //HunkAssetRes load = new HunkAssetRes(true, (ushort)AssetEvent.GetResObjectUI, "scenesone", "Load", "LoadPanel", (ushort)AssetEvent.GetResObjectCallBack, (ushort)UIAdventure.One);
        //SendMsg(load);

    }

    IEnumerator HotfixStart()
    {
       
        ILManager iL = this.gameObject.AddComponent<ILManager>();
        iL.Init();
        yield return StartCoroutine(iL.GetServerDllVersion());
        iL.CheckIsUpdate();
        yield return StartCoroutine(iL.DownloadTasks());
       
        SceneManager.LoadScene("Main");
        yield return StartCoroutine(iL.LoadHotfixDll());
    }

    IEnumerator NotHotfixStart()
    {
        ILManager iL = this.gameObject.AddComponent<ILManager>();

        iL.Init();
        //#if !UNITY_EDITOR
        yield return StartCoroutine(iL.GetLocalDllVersion());
        //#endif
        this.gameObject.AddComponent<ILoaderManager>();
        this.gameObject.AddComponent<NativeResLoader>();
     
        SceneManager.LoadScene("Main");
        yield return StartCoroutine(iL.LoadHotfixDll());



        //gameObject.AddComponent<ILoaderManager>();
        //gameObject.AddComponent<NativeResLoader>();

      
    }



}
