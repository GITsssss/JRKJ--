using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;

public class Imgt : EditorWindow
{
    [MenuItem("HLVR/ToolUse")]
    public static void Open()
    { 
        EditorWindow.GetWindow<Imgt>().minSize=new Vector2(540,700); 
    }

    private void OnEnable()
    {
        
    }
    Vector2 zer;
    private void OnGUI()
    {
        zer= GUILayout.BeginScrollView(zer);
        using (new GUILayout.HorizontalScope(EditorStyles.helpBox)) 
        {
            GUILayout.Label("1.�����Լ��ĳ����д���һ����������������ReceiverPort");
        }
        
        GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/AndroidReceiver/Scripts/Editor/Img1.png"), GUILayout.Width(500), GUILayout.Height(500));
        using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
        {
            GUILayout.Label("2.�ڲ˵����������ù��ߣ�XMLTool");
        }
        GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/AndroidReceiver/Scripts/Editor/Img2.png"), GUILayout.Width(500), GUILayout.Height(100));
        GUILayout.EndScrollView();    
      
    }
}
