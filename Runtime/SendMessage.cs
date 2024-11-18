using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using System.IO;

namespace HLVR.AndroidReceiver 
{
    public class SendMessage : MonoBehaviour
    {
        // public TMP_Text log;
        public float time = 3;
        public bool onEnableSendMessage = true;

        private void Start()
        {
            if (onEnableSendMessage)
            {
                StartCoroutine(SendMessageCoroutine());
            }
        }
        IEnumerator SendMessageCoroutine()
        {
            while (true)
            {
                SendBroadcast("Unity 发送消息");
                yield return new WaitForSeconds(time);
            }
        }

        public void SendBroadcast(string message)
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

}
