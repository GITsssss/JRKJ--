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
                GUILayout.Label("1.在你自己的场景中创建一个空物体添加组件，ReceiverPort");
            }

            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/AndroidReceiver/Scripts/Editor/Img1.png"), GUILayout.Width(500), GUILayout.Height(500));
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("2.在菜单栏激活配置工具，XMLTool");
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
            EditorGUILayout.LabelField("请您不要重复添加!!!", style,GUILayout.Height(70));
           
            so.ApplyModifiedProperties();
            if (GUILayout.Button("好的", GUILayout.Height(20)))
            {
                tips.Close();
            }
        }

    }
}
