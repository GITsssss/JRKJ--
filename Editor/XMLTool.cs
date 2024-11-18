using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;



namespace HLVR.AndroidReceiver 
{
    public class XMLTool : EditorWindow
    {
        [MenuItem("HLVR/Tool/XMLTool")]
        public static void OpenWindow()
        {
            EditorWindow.GetWindow(typeof(XMLTool));
        }
        bool toggle;
        string str;
        bool isfinish;
        SerializedObject serializedObject;

        [MenuItem("HLVR/Tool/Add ReceiverPort")]
        public static void AddReceiverPort()
        {
            GameObject g = new GameObject();
            g.AddComponent<ReceiverPort>();
            g.AddComponent<SendMessage>();
            EditorUtility.SetDirty(g);
        }
        [MenuItem("GameObject/3D Object/ReceiverPort", false, 0)]
        public static void AddReceiverPort2()
        {
            GameObject g = new GameObject();
            g.AddComponent<ReceiverPort>();
            g.AddComponent<SendMessage>();
            EditorUtility.SetDirty(g);
        }
        [MenuItem("GameObject/Add ReceiverPort", false, 0)]
        public static void AddReceiverPort3()
        {
            GameObject g = new GameObject();
            g.AddComponent<ReceiverPort>();
            g.AddComponent<SendMessage>();
            EditorUtility.SetDirty(g);

        }
        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
        }
        private void OnGUI()
        {

            serializedObject.Update();

            EditorGUILayout.HelpBox("这个很简单!!! 我给你显示啥按钮你就点啥按钮就完了", MessageType.Info);


            string folder = Application.dataPath + "/Plugins/Android";
            if (!Directory.Exists(folder))
            {
                EditorGUILayout.HelpBox(folder + "目录不存在需要创建", MessageType.Error);
                if (GUILayout.Button("创建文件夹并且导入文件"))
                {
                    Directory.CreateDirectory(folder);
                    using (StreamWriter writer = new StreamWriter(folder + "/AndroidManifest.xml", false))
                    {
                        writer.WriteLine("<manifest xmlns:android=\"http://schemas.android.com/apk/res/android\"\r\n          xmlns:tools=\"http://schemas.android.com/tools\"\r\n          package=\"com.JRKJ.quest3App\">\r\n\r\n  <application\r\n      android:allowBackup=\"true\"\r\n      android:label=\"@string/app_name\"\r\n      android:supportsRtl=\"true\">\r\n\r\n    <activity\r\n        android:name=\"com.unity3d.player.UnityPlayerActivity\"\r\n        android:theme=\"@style/UnityThemeSelector\">\r\n      <intent-filter>\r\n        <action android:name=\"android.intent.action.MAIN\" />\r\n        <category android:name=\"android.intent.category.LAUNCHER\" />\r\n      </intent-filter>\r\n      <meta-data\r\n          android:name=\"unityplayer.UnityActivity\"\r\n          android:value=\"true\" />\r\n    </activity>\r\n\r\n    <!-- 添加 BroadcastReceiver -->\r\n    <receiver\r\n        android:name=\"com.JRKJ.quest3App.UnityReceiver\"\r\n        android:exported=\"true\">\r\n      <!-- 修改这里 -->\r\n      <intent-filter>\r\n        <action android:name=\"com.example.UNITY_ACTION\" />\r\n      </intent-filter>\r\n    </receiver>\r\n\r\n  </application>\r\n</manifest>");
                    }
                    using (StreamWriter writer = new StreamWriter(folder + "/UnityReceiver.java", false))
                    {
                        writer.WriteLine("package  com.JRKJ.quest3App;\r\n\r\nimport android.content.BroadcastReceiver;\r\nimport android.content.Context;\r\nimport android.content.Intent;\r\nimport android.util.Log;\r\n\r\nimport com.unity3d.player.UnityPlayer;\r\n\r\npublic class UnityReceiver extends BroadcastReceiver {\r\n    @Override\r\n    public void onReceive(Context context, Intent intent) {\r\n        String message = intent.getStringExtra(\"message\");\r\n        if (\"CallUnityMethod\".equals(message)) {\r\n            Log.d(\"UnityReceiver\", \"Message received: \" + message);\r\n            UnityPlayer.UnitySendMessage(\"ReceiverPort\", \"Receiver\", message);\r\n        }\r\n    }\r\n}\r\n");
                    }
                    AssetDatabase.Refresh();
                }
            }
            else if (!File.Exists(folder + "/AndroidManifest.xml"))
            {
                if (GUILayout.Button("导入AndroidManifest文件"))
                {
                    using (StreamWriter writer = new StreamWriter(folder + "/AndroidManifest.xml", false))
                    {
                        writer.WriteLine("<manifest xmlns:android=\"http://schemas.android.com/apk/res/android\"\r\n          xmlns:tools=\"http://schemas.android.com/tools\"\r\n          package=\"com.JRKJ.quest3App\">\r\n\r\n  <application\r\n      android:allowBackup=\"true\"\r\n      android:label=\"@string/app_name\"\r\n      android:supportsRtl=\"true\">\r\n\r\n    <activity\r\n        android:name=\"com.unity3d.player.UnityPlayerActivity\"\r\n        android:theme=\"@style/UnityThemeSelector\">\r\n      <intent-filter>\r\n        <action android:name=\"android.intent.action.MAIN\" />\r\n        <category android:name=\"android.intent.category.LAUNCHER\" />\r\n      </intent-filter>\r\n      <meta-data\r\n          android:name=\"unityplayer.UnityActivity\"\r\n          android:value=\"true\" />\r\n    </activity>\r\n\r\n    <!-- 添加 BroadcastReceiver -->\r\n    <receiver\r\n        android:name=\"com.JRKJ.quest3App.UnityReceiver\"\r\n        android:exported=\"true\">\r\n      <!-- 修改这里 -->\r\n      <intent-filter>\r\n        <action android:name=\"com.example.UNITY_ACTION\" />\r\n      </intent-filter>\r\n    </receiver>\r\n\r\n  </application>\r\n</manifest>");
                    }
                    AssetDatabase.Refresh();
                }
            }
            else if (!File.Exists(folder + "/UnityReceiver.java"))
            {
                if (GUILayout.Button("导入UnityReceiver文件"))
                {
                    using (StreamWriter writer = new StreamWriter(folder + "/UnityReceiver.java", false))
                    {
                        writer.WriteLine("package  com.JRKJ.quest3App;\r\n\r\nimport android.content.BroadcastReceiver;\r\nimport android.content.Context;\r\nimport android.content.Intent;\r\nimport android.util.Log;\r\n\r\nimport com.unity3d.player.UnityPlayer;\r\n\r\npublic class UnityReceiver extends BroadcastReceiver {\r\n    @Override\r\n    public void onReceive(Context context, Intent intent) {\r\n        String message = intent.getStringExtra(\"message\");\r\n        if (\"CallUnityMethod\".equals(message)) {\r\n            Log.d(\"UnityReceiver\", \"Message received: \" + message);\r\n            UnityPlayer.UnitySendMessage(\"ReceiverPort\", \"Receiver\", message);\r\n        }\r\n    }\r\n}\r\n");
                    }
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                if (!toggle)
                {
                    if (GUILayout.Button("确认包名修改"))
                    {
                        toggle = true;
                    }

                    EditorGUILayout.LabelField("请确认Player中的包名已经设置好！");
                }


                if (toggle)
                {
                    if (GUILayout.Button("自动获取"))
                    {
                        str = Application.identifier;
                    }

                    EditorGUILayout.LabelField("当前包名:" + str);

                    if (str != null && str != "")
                    {
                        if (GUILayout.Button("确认配置:获取好了包名就点我！"))
                        {
                            SetXML();
                            SetJava_UnityReceiver();
                            //ReceiverPort[] receiverPorts=FindObjectsOfType<ReceiverPort>(true);
                            //foreach (ReceiverPort receiverPort in receiverPorts) 
                            //{
                            //    receiverPort.PackName = Application.identifier;
                            //}
                            isfinish = true;
                        }
                    }
                }

                if (isfinish)
                {
                    EditorGUILayout.HelpBox("配置完成！", MessageType.Info);
                }
            }



            serializedObject.ApplyModifiedProperties();
        }

        public void SetJava_UnityReceiver()
        {
            string path = Application.dataPath + "/Plugins/Android/UnityReceiver.java";
            string[] cons = File.ReadAllLines(path);
            cons[0] = "package" + "  " + Application.identifier + ";";
            File.WriteAllLines(path, cons);
        }

        public void SetXML()
        {
            string path = Application.dataPath + "/Plugins/Android/AndroidManifest.xml";
            if (File.Exists(path))
            {
                string pg = Application.identifier;
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                XmlNodeList xmlNodeList = xml.SelectSingleNode("manifest").ChildNodes;
                xml.DocumentElement.Attributes["package"].Value = pg;
                Debug.Log(xmlNodeList.Item(0).ChildNodes[2].Name);
                xmlNodeList.Item(0).ChildNodes[2].Attributes["android:name"].Value = pg + ".UnityReceiver";

                xml.Save(path);
            }
        }

        public void CreateFile()
        {
            // 定义文件路径
            string filePath = Application.dataPath + "/Plugins/Android/my_file.txt";

            // 获取目录路径
            string directoryPath = Path.GetDirectoryName(filePath);

            // 检查目录是否存在，如果不存在则创建
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Debug.Log($"Directory created: {directoryPath}");
            }

            // 创建或覆盖文件
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("This is a test file created by Unity.");
            }

            Debug.Log($"File created at: {filePath}");
        }
    }


}
