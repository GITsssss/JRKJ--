using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace HLVR.AndroidReceiver 
{
    [System.Serializable]
    public struct Receiver
    {
        //�ؼ���
        public string keyword;
        //������յ�ָ���Ĺؼ��ʵ�������¼�
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

