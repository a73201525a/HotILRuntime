using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILAdaptor
{
    //����̳�-��������ע��
    public static void RegisterAdaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        //appdomain.RegisterCrossBindingAdaptor(new BaseWindowAdaptor());
        //async������
        appdomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor());
        appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        appdomain.RegisterCrossBindingAdaptor(new Adapt_IMessage());
        //appdomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
    }
}
