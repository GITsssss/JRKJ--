using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HLVR.AndroidReceiver
{

    public class Tips : EditorWindow
    {
        GUIStyle style;
        static Tips tips;
        public static void Open()
        {
            tips = EditorWindow.GetWindow<Tips>();
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
                fontStyle = FontStyle.Bold,
                fontSize = 30,

            };
            style.normal.textColor = Color.yellow;

        }


        private void OnGUI()
        {
            so.Update();
            EditorGUILayout.LabelField("������Ҫ�ظ����!!!", style, GUILayout.Height(70));

            so.ApplyModifiedProperties();
            if (GUILayout.Button("�õ�", GUILayout.Height(20)))
            {
                tips.Close();
            }
        }

    }
}
