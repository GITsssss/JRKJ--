using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace HLVR.AndroidReceiver 
{
    [Icon("Profiler.NetworkMessages")]
    public class SendMessage : MonoBehaviour
    {
        // public TMP_Text log;
        
        public float time = 3;
        public bool  onEnableSendMessage = true;
        public string heartbeat="Unity连接监听中...";
        private  string splitChar="&";

        public List<Send> sends= new List<Send>();
        public string SplitChar 
        {
            get { return splitChar; }
        }
        private void Start()
        {
            if (onEnableSendMessage)
            {
                StartCoroutine(SendMessageCoroutine());
            }
            SendMessageBackServiceCustomKeyContent("ShowStopButton", "000");
        }

        /// <summary>
        /// 发送消息给后台服务 例：key:学号、姓名、分数；content：11 、小明、99
        /// </summary>
        /// <param name="key">关键词</param>
        /// <param name="content">发送内容</param>
        public void SendMessageBackServiceCustomKeyContent(string key,string content)
        {
            string info = key + splitChar + content;
            SendBroadcast(info);
        }

        /// <summary>
        /// 发送消息给后台服务
        /// </summary>
        /// <param name="index">sends消息列表的索引号</param>
        public void SendMessageBackServiceByIndex(int index)
        {

            SendBroadcast(sends[index].Message(splitChar));
        }


        /// <summary>
        /// 发送消息给后台服务
        /// </summary>
        /// <param name="key">键</param>
        public void SendMessageBackServiceByKey(string key)
        {
            foreach (var item in sends)
            {
                if (item.key.Contains(key))
                {
                    SendBroadcast(item.Message(splitChar));
                    return;
                }
            }       
        }


        IEnumerator SendMessageCoroutine()
        {
            while (true)
            {
                SendBroadcast(heartbeat);
                yield return new WaitForSeconds(time);
            }
        }

        private void SendBroadcast(string message)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                //log.text = "Application.platform";
                using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    // log.text = "AndroidJavaClass unityPlayerClass";
                    using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        //  log.text = "AndroidJavaObject currentActivity";
                        using (AndroidJavaClass unityReceiverClass = new AndroidJavaClass("com.JRKJ.quest3App.UnityReceiver"))
                        {
                            //  log.text = "AndroidJavaClass unityReceiverClass";
                            if (unityReceiverClass != null)
                            {
                                // log.text = "unityReceiverClass != null";
                                // 创建 UnityReceiver 实例
                                using (AndroidJavaObject unityReceiver = new AndroidJavaObject("com.JRKJ.quest3App.UnityReceiver"))
                                {
                                    // log.text = "AndroidJavaObject unityReceiver ";

                                    // unityReceiver.Call("setContext", currentActivity);
                                    //  log.text = "unityReceiver.Call(\"setContext\", currentActivity)";

                                    unityReceiver.Call("UnitySendMessageOtherApp", message);
                                    // log.text = message;
                                }
                            }
                            else
                            {
                                Debug.LogError("Failed to get UnityReceiver class.");
                                // log.text = "Failed to get UnityReceiver class.";
                                File.WriteAllText(Application.persistentDataPath + "/log.txt", "Failed to get UnityReceiver class.");
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("This platform is not supported for calling Java methods.");
                //log.text = "This platform is not supported for calling Java methods.";
                File.WriteAllText(Application.persistentDataPath + "/log.txt", "This platform is not supported for calling Java methods.");
            }


        }
    }


    [System.Serializable]
    public struct Send 
    {
        public string key;
        public string value;

        public string Message(string splitchar)
        {
            return key + splitchar + value;
        }
    }
}
