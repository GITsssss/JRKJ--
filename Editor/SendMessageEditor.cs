using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HLVR.AndroidReceiver;
using System;

[CustomEditor(typeof(SendMessage))]
public class SendMessageEditor : Editor
{
    SendMessage sendMessage;
    private GUIStyle buttonStyle;

    /// <summary>
    /// 按钮风格
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private GUIStyle ButtonStyle(int index)
    {
        buttonStyle = new GUIStyle(GUI.skin.button);
        Texture2D texture2D = null;
        switch (index)
        {
            case 0:
                texture2D = EditorGUIUtility.IconContent("btn left on@2x").image as Texture2D;
                break;

            case 1:
                texture2D = EditorGUIUtility.IconContent("btn right on@2x").image as Texture2D;
                break;
        }

        buttonStyle.normal.background = texture2D;
        buttonStyle.normal.background.width = 20;
        buttonStyle.normal.background.height= 20;
        buttonStyle.border = new RectOffset(0, 0, 0, 0); // 可选：移除边框
        buttonStyle.alignment = TextAnchor.MiddleCenter; // 文本居中对齐

        return buttonStyle; 
    }



    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        sendMessage= (SendMessage)target;

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox,GUILayout.ExpandWidth(true))) 
        {
            sendMessage.onEnableSendMessage = GUILayout.Toggle(sendMessage.onEnableSendMessage, "启用协程心跳监测");
            if (sendMessage.onEnableSendMessage)
            {

                using ((new EditorGUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.ExpandWidth(true))))
                {
                    EditorGUILayout.LabelField("发送一次心跳监测的间隔时间", GUILayout.Width(160));
                    if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        sendMessage.time += 0.1f;
                    }

                    if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        sendMessage.time -= 0.1f;
                    }
                    sendMessage.time = EditorGUILayout.FloatField(sendMessage.time, GUILayout.Width(30));
                }

                using ((new EditorGUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.ExpandWidth(true)))) 
                {
                    EditorGUILayout.LabelField("心跳监测的内容", GUILayout.Width(90));
                    sendMessage.heartbeat = EditorGUILayout.TextField(sendMessage.heartbeat);
                }

                  
            }
        }

    
        EditorGUILayout.LabelField("消息发送" + sendMessage.SplitChar, GUILayout.Width(90));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sends"));
        serializedObject.ApplyModifiedProperties(); 
    }
}
