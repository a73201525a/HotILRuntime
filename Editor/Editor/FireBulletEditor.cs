using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class FireBulletEditor: EditorWindow
{ 
    [MenuItem("Bullet/弹道编辑场景", false, 0)]
    public static void Scene()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(Application.dataPath+ "/Scenes/bulletEditor.unity");
    }
    public static SaveBulletEditor windows;
    [MenuItem("Bullet/SaveBullte", false, 0)]
    public static void SaveBullte()
    {
        windows = EditorWindow.GetWindow<SaveBulletEditor>();
        windows.titleContent.text = "是否保存";
        windows.Show();
       
    }

    [MenuItem("Bullet/CreatBullet", false, 0)]
    public static void CreatBullet()
    {
        GameObject go = new GameObject("BulletEidtor");
        go.AddComponent<BulletData>();
    }


    [MenuItem("Bullet/ReadBullet", false, 0)]
    public static void OpenWindow()
    {
        var window = EditorWindow.GetWindow<FireBulletEditor>();
        window.titleContent.text = "读取子弹数据";
        window.Show();
    }


    private string m_Path;
    void OnGUI()
    {
        EditorGUILayout.LabelField("ID:");
        m_Path = RelativeAssetPathTextField(m_Path, "");
    

        if (GUILayout.Button("Read"))
        {
            BulletEidtor.Read(m_Path);
        }
    }

    public static GUIStyle TextFieldRoundEdge;
    public static GUIStyle TextFieldRoundEdgeCancelButton;
    public static GUIStyle TextFieldRoundEdgeCancelButtonEmpty;
    public static GUIStyle TransparentTextField;

    private string RelativeAssetPathTextField(string path, string extension)
    {
        if (TextFieldRoundEdge == null)
        {
            TextFieldRoundEdge = new GUIStyle("SearchTextField");
            TextFieldRoundEdgeCancelButton = new GUIStyle("SearchCancelButton");
            TextFieldRoundEdgeCancelButtonEmpty = new GUIStyle("SearchCancelButtonEmpty");
            TransparentTextField = new GUIStyle(EditorStyles.whiteLabel);
            TransparentTextField.normal.textColor = EditorStyles.textField.normal.textColor;
        }

        Rect position = EditorGUILayout.GetControlRect();
        GUIStyle textFieldRoundEdge = TextFieldRoundEdge;
        GUIStyle transparentTextField = TransparentTextField;
        GUIStyle gUIStyle = (path != "") ? TextFieldRoundEdgeCancelButton : TextFieldRoundEdgeCancelButtonEmpty;
        position.width -= gUIStyle.fixedWidth;
        if (Event.current.type == EventType.Repaint)
        {
            GUI.contentColor = (EditorGUIUtility.isProSkin ? Color.black : new Color(0f, 0f, 0f, 0.5f));
            textFieldRoundEdge.Draw(position, new GUIContent(""), 0);
            GUI.contentColor = Color.white;
        }
        Rect rect = position;
        float num = textFieldRoundEdge.CalcSize(new GUIContent("")).x - 2f;
        rect.x += num;
        rect.y += 1f;
        rect.width -= num;
        EditorGUI.BeginChangeCheck();
        path = EditorGUI.TextField(rect, path, transparentTextField);
        if (EditorGUI.EndChangeCheck())
        {
            path = path.Replace('\\', '/');
        }
        if (Event.current.type == EventType.Repaint)
        {
            Rect position2 = rect;
            float num2 = transparentTextField.CalcSize(new GUIContent(path + ".")).x - EditorStyles.whiteLabel.CalcSize(new GUIContent(".")).x; ;
            position2.x += num2;
            position2.width -= num2;
            GUI.contentColor = (EditorGUIUtility.isProSkin ? Color.black : new Color(0f, 0f, 0f, 0.5f));
            EditorStyles.label.Draw(position2, extension, false, false, false, false);
            GUI.contentColor = Color.white;
        }
        position.x += position.width;
        position.width = gUIStyle.fixedWidth;
        position.height = gUIStyle.fixedHeight;
        if (GUI.Button(position, GUIContent.none, gUIStyle) && path != "")
        {
            path = "";
            GUI.changed = true;
            GUIUtility.keyboardControl = 0;
        }
        return path;
    }

}

public  class  SaveBulletEditor: EditorWindow
{
    void OnGUI()
    {

        if (GUILayout.Button("是"))
        {
            BulletEidtor.Save();
            FireBulletEditor.windows.Close();
        }

        if (GUILayout.Button("否"))
        {
            FireBulletEditor.windows.Close();
        }
    }
}