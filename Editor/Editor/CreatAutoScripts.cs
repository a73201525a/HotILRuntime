using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;

public class CreatAutoScripts
{
    [MenuItem("Assets/Create/Create Frame C#Script", false, 70)]
    public static void CreatEventCS()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            ScriptableObject.CreateInstance<CreateEventCSScriptAsset>(),
            GetSelectedPathOrFallBack() + "/New.Script.cs", null, "Assets/Editor/Editor/EventCSClass.cs"
            );
    }

    public static string GetSelectedPathOrFallBack()
    {
        string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}

class CreateEventCSScriptAsset : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(obj);
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
    {
        string fullPath = Path.GetFullPath(pathName);
        StreamReader streamReader = new StreamReader(resourceFile);
        string text = streamReader.ReadToEnd();
        streamReader.Close();

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);

        text = Regex.Replace(text, "EventCSClass", fileNameWithoutExtension);

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;

        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);

        bool append = false;

        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);

        streamWriter.Write(text);

        streamWriter.Close();

        AssetDatabase.ImportAsset(pathName);

        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }
}