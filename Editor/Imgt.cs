using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;


namespace HLVR.AndroidReceiver 
{
    public class Imgt : EditorWindow
    {
        [MenuItem("HLVR/ToolUse")]
        public static void Open()
        {
            EditorWindow.GetWindow<Imgt>().minSize = new Vector2(540, 700);
        }

        private void OnEnable()
        {

        }
        Vector2 zer;
        private void OnGUI()
        {
            zer = GUILayout.BeginScrollView(zer);
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


    public class Tips : EditorWindow
    {
        GUIStyle style;
        static Tips tips;
        public static void Open()
        {
            tips= EditorWindow.GetWindow<Tips>();
           //  ScriptableWizard.DisplayWizard<Tips>("Create Light", "Create", "Apply");
            tips.minSize = new Vector2(300, 100);
            tips.maxSize = new Vector2(300, 100);
        }

        SerializedObject so;
        private void OnEnable()
        {
            so = new SerializedObject(this);
            style = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle= FontStyle.Bold,
                fontSize = 30,
                
            };
            style.normal.textColor = Color.yellow;
           
        }

       
        private void OnGUI()
        {
            so.Update();
            EditorGUILayout.LabelField("������Ҫ�ظ����!!!", style,GUILayout.Height(70));
           
            so.ApplyModifiedProperties();
            if (GUILayout.Button("�õ�", GUILayout.Height(20)))
            {
                tips.Close();
            }
        }

    }
}
