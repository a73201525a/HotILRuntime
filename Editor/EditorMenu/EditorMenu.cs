using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorMenu : Editor
{

    //[MenuItem("Hotfix/Test")]
    //public static void CrDll()
    //{
    //   // DllTools.CopyToPersistent();
    //}

    [MenuItem("Hotfix/CreateDll")]
    public static void CreateDll() {
        DllTools.CreateDll();
    }

    [MenuItem("Hotfix/DeleteDll")]
    public static void DeleteDll()
    {
        DllTools.DeleteDll();
    }

    [MenuItem("Hotfix/CopyDllToPersistentDataPath")]
    public static void CopyDllToPersistentDataPath() {
        DllTools.CopyDllToPersistentDataPath();
    }


    [MenuItem("Hotfix/OpenPersistentDataPath")]
    public static void OpenPersistentDataPath()
    {
        Application.OpenURL(Application.persistentDataPath);
    }
}
