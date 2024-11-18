using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace HLVR.AndroidReceiver 
{
    [System.Serializable]
    public struct Receiver
    {
        //关键词
        public string keyword;
        //如果接收到指定的关键词调用这个事件
        public UnityEvent callBack;
        public void Cast(string con)
        {
            if (keyword == con)
            {
                callBack?.Invoke();
            }
        }
    }
}

