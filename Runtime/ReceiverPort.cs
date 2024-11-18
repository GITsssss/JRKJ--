using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace HLVR.AndroidReceiver 
{
    [Icon("BuildSettings.Android")]
    public class ReceiverPort : MonoBehaviour
    {
        //[SerializeField]   
        public string packName;
        //�����õ��ı�������Ϊ�գ�
        public TMP_Text debugInfo;
        public Receiver[] receivers;

        /// <summary>
        /// ���ð���
        /// </summary>
        /// <param name="pgname">�����Զ���İ���</param>
        public void SetPackName(string pgname)
        {
            packName = pgname;
        }
        /// <summary>
        /// ���ð������Զ���ȡPlayerSetting�еİ���
        /// </summary>
        public void SetPackName()
        {
            packName = Application.identifier;
        }
        void Reset()
        {
            this.gameObject.name = GetType().Name;
        }
        void Start()
        {
            // ע��BroadcastReceiver
            RegisterBroadcastReceiver();
        }

        void OnDestroy()
        {
            // ע��BroadcastReceiver
            UnregisterBroadcastReceiver();
        }

        void RegisterBroadcastReceiver()
        {
            Debug.Log("com.JRKJ.quest3App.UnityReceiver");

            string pgname = packName + ".UnityReceiver";
            if (Application.platform == RuntimePlatform.Android)
            {
                using (var pluginClass = new AndroidJavaClass(pgname))
                {
                    using (var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        activity.Call("registerReceiver", pluginClass.GetStatic<AndroidJavaObject>("getInstance"), new AndroidJavaObject("android.content.IntentFilter", "com.example.UNITY_ACTION"));
                    }
                }
            }
        }

        void UnregisterBroadcastReceiver()
        {
            string pgname = packName + ".UnityReceiver";
            if (Application.platform == RuntimePlatform.Android)
            {
                using (var pluginClass = new AndroidJavaClass(pgname))
                {
                    using (var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        activity.Call("unregisterReceiver", pluginClass.GetStatic<AndroidJavaObject>("getInstance"));
                    }
                }
            }
        }

        public void Receiver(string message)
        {
            if (debugInfo != null)
                debugInfo.text = message;

            foreach (var item in receivers)
            {
                item.Cast(message);
            }
            if (message == "Quit")
            {
                Application.Quit();
            }
        }
    }
}

