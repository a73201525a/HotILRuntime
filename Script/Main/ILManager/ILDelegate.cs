using ILRuntime.Runtime.Intepreter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ILDelegate
{
    public static void RegisterDelegate(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        //ί�е�ע���ת��


        RegistAction<System.IAsyncResult>(appdomain);
        RegistAction<Sprite>(appdomain);
        RegistAction<Vector2>(appdomain);
        RegistAction<Vector3>(appdomain);
        RegistAction<object>(appdomain);
        RegistAction<Google.Protobuf.IMessage>(appdomain);
        RegistAction<System.EventArgs>(appdomain);
        RegistAction<GameObject>(appdomain);
        RegistAction<Adapt_IMessage.Adaptor>(appdomain);
        RegistAction<byte[]>(appdomain);
        RegistAction<int, byte[]>(appdomain);
        RegistAction<string, string>(appdomain);
        RegistAction<UnityEngine.Networking.UnityWebRequest>(appdomain);
        RegistAction<GameObject, UnityEngine.EventSystems.PointerEventData>(appdomain);
        RegistUnityDelegate<bool>(appdomain);
        RegistUnityDelegate<int>(appdomain);
        RegistUnityDelegate<float>(appdomain);
        RegistUnityDelegate<string>(appdomain);
        RegistUnityDelegate<Color>(appdomain);
        RegistUnityDelegate<Color32>(appdomain);
        RegistUnityDelegate<Vector2>(appdomain);
        RegistUnityDelegate<Vector3>(appdomain);
        RegistUnityDelegate<UnityEngine.EventSystems.BaseEventData>(appdomain);
        //RegistUnityDelegate<System.Threading.ThreadStart>(appdomain);
        Comparison<byte>(appdomain);
        Predicate<byte>(appdomain);

        //����ת����

        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((action) =>
        {
            return new UnityEngine.Events.UnityAction(() => { ((System.Action)action)(); });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((action) =>
        {
            return new DG.Tweening.TweenCallback(() => { ((System.Action)action)(); });
        });
        //appdomain.DelegateManager.RegisterDelegateConvertor<Spine.AnimationState.TrackEntryEventDelegate>((act) =>
        //{
        //    return new Spine.AnimationState.TrackEntryEventDelegate((trackEntry, e) =>
        //    {
        //        ((Action<Spine.TrackEntry, Spine.Event>)act)(trackEntry, e);
        //    });
        //});



        //-----------------------�����ֱ�Ӹ��ݱ༭����ʾ Ȼ���ƴ������ Ҳ�ǿ��Ե�,ֱ��д������-----------------------------------//

        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.GameObject, UnityEngine.Collider>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.EventSystems.PointerEventData>();
       // appdomain.DelegateManager.RegisterMethodDelegate<Spine.TrackEntry, Spine.Event>();


        appdomain.DelegateManager.RegisterDelegateConvertor<System.Threading.ThreadStart>((act) =>
        {
            return new System.Threading.ThreadStart(() =>
            {
                ((Action)act)();
            });
        });



        appdomain.DelegateManager.RegisterDelegateConvertor<global::LoaderProgrecess>((act) =>
        {
            return new global::LoaderProgrecess((bundle, Process) =>
            {
                ((Action<System.String, System.Single>)act)(bundle, Process);
            });
        });

        appdomain.DelegateManager.RegisterMethodDelegate<System.String, System.Single>();


        appdomain.DelegateManager.RegisterFunctionDelegate<global::Adapt_IMessage.Adaptor, global::Adapt_IMessage.Adaptor, System.Boolean>();

        appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<System.Collections.Generic.KeyValuePair<global::Adapt_IMessage.Adaptor, UnityEngine.GameObject>>>((act) =>
        {
            return new System.Comparison<System.Collections.Generic.KeyValuePair<global::Adapt_IMessage.Adaptor, UnityEngine.GameObject>>((x, y) =>
            {
                return ((Func<System.Collections.Generic.KeyValuePair<global::Adapt_IMessage.Adaptor, UnityEngine.GameObject>, System.Collections.Generic.KeyValuePair<global::Adapt_IMessage.Adaptor, UnityEngine.GameObject>, System.Int32>)act)(x, y);
            });
        });
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Video.VideoPlayer>();
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Video.VideoPlayer.EventHandler>((act) =>
        {
            return new UnityEngine.Video.VideoPlayer.EventHandler((source) =>
            {
                ((Action<UnityEngine.Video.VideoPlayer>)act)(source);
            });
        });
        appdomain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<global::Adapt_IMessage.Adaptor, UnityEngine.GameObject>, System.Collections.Generic.KeyValuePair<global::Adapt_IMessage.Adaptor, UnityEngine.GameObject>, System.Int32>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.TextAsset>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.AudioClip, System.String>();

        appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>();
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((obj) =>
            {
                return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>)act)(obj);
            });
        });

        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<UnityEngine.Vector2>>((act) =>
        {
            return new DG.Tweening.Core.DOSetter<UnityEngine.Vector2>((pNewValue) =>
            {
                ((Action<UnityEngine.Vector2>)act)(pNewValue);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<UnityEngine.Vector2>>((act) =>
        {
            return new DG.Tweening.Core.DOGetter<UnityEngine.Vector2>(() =>
            {
                return ((Func<UnityEngine.Vector2>)act)();
            });
        });




        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<System.Single>>((act) =>
        {
            return new DG.Tweening.Core.DOSetter<System.Single>((pNewValue) =>
            {
                ((Action<System.Single>)act)(pNewValue);
            });
        });
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Transform, System.Int32>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.U2D.SpriteAtlas>();
        appdomain.DelegateManager.RegisterMethodDelegate<Texture, Material>();
        appdomain.DelegateManager.RegisterMethodDelegate<Texture2D, Material, string>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Material, UnityEngine.Transform, System.Int32>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.GameObject, UnityEngine.Transform>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Object, UnityEngine.Transform>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Sprite, UnityEngine.Transform>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.U2D.SpriteAtlas, UnityEngine.Transform>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.AudioClip, UnityEngine.Transform>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.TextAsset, UnityEngine.Transform>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Transform>();

        RegistAction<string, bool>(appdomain);
        RegistAction<System.Single, System.Boolean>(appdomain);
        appdomain.DelegateManager.RegisterFunctionDelegate<global::Adapt_IMessage.Adaptor, System.Boolean>();

        appdomain.DelegateManager.RegisterFunctionDelegate<System.Int32, System.Int32, System.Int32>();
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<System.Int32>>((act) =>
        {
            return new System.Comparison<System.Int32>((x, y) =>
            {
                return ((Func<System.Int32, System.Int32, System.Int32>)act)(x, y);
            });
        });

      //  appdomain.DelegateManager.RegisterMethodDelegate<Vector3, ForceMode>();

        appdomain.DelegateManager.RegisterMethodDelegate<List<object>>();
        //appdomain.DelegateManager.RegisterMethodDelegate<AChannel, System.Net.Sockets.SocketError>();
        appdomain.DelegateManager.RegisterMethodDelegate<byte[], int, int>();
        //appdomain.DelegateManager.RegisterMethodDelegate<IResponse>();
        //appdomain.DelegateManager.RegisterMethodDelegate<Session, object>();
        //appdomain.DelegateManager.RegisterMethodDelegate<Session, ushort, MemoryStream>();
        //appdomain.DelegateManager.RegisterMethodDelegate<Session>();
        appdomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();

        //Action ����һ������ ����û�з���ֵ�� ע��
        appdomain.DelegateManager.RegisterMethodDelegate<string>();

        //Func ���з���ֵ ���Ҵ��в�����
        appdomain.DelegateManager.RegisterFunctionDelegate<int, string, string>();
        appdomain.DelegateManager.RegisterFunctionDelegate<string, string>();

        //delegate ת��
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<string>>
        ((act) =>
        {
            return new UnityAction<string>((arg0) =>
            {
                ((Action<string>)act)(arg0);
            });
        });

        appdomain.DelegateManager.RegisterMethodDelegate<string, string, LogType>();
        //��ӡ�õ�ί��ת����
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Application.LogCallback>((action) =>
        {
            return new UnityEngine.Application.LogCallback((s1, s2, t) => { ((System.Action<string, string, LogType>)action)(s1, s2, t); });
        });

        appdomain.DelegateManager.RegisterDelegateConvertor<System.AsyncCallback>((act) =>
        {
            return new System.AsyncCallback((ar) =>
            {
                ((Action<System.IAsyncResult>)act)(ar);
            });
        });

        appdomain.DelegateManager.RegisterFunctionDelegate<global::Adapt_IMessage.Adaptor, global::Adapt_IMessage.Adaptor, System.Int32>();

        appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<global::Adapt_IMessage.Adaptor>>((act) =>
        {
            return new System.Comparison<global::Adapt_IMessage.Adaptor>((x, y) =>
            {
                return ((Func<global::Adapt_IMessage.Adaptor, global::Adapt_IMessage.Adaptor, System.Int32>)act)(x, y);
            });
        });

        appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Int32>();
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>((x, y) =>
            {
                return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Int32>)act)(x, y);
            });
        });


    }


    #region ��������ί��ע�� ͬʱ���ת����

    static void RegistAction<T>(ILRuntime.Runtime.Enviorment.AppDomain app)
    {
        //ע����Ҫ�õ���ί������
        app.DelegateManager.RegisterMethodDelegate<T>();
        //ί��ת����
        app.DelegateManager.RegisterDelegateConvertor<System.Action<T>>((action) =>
        {
            return new System.Action<T>((s) => { ((System.Action<T>)action)(s); });
        });
        //ע���õ��Ĵ�����ֵί������
        app.DelegateManager.RegisterFunctionDelegate<T>();
    }
    static void RegistAction<T1, T2>(ILRuntime.Runtime.Enviorment.AppDomain app)
    {
        //ע����Ҫ�õ���ί������
        app.DelegateManager.RegisterMethodDelegate<T1, T2>();
        //ί��ת����
        app.DelegateManager.RegisterDelegateConvertor<System.Action<T1, T2>>((action) =>
        {
            return new System.Action<T1, T2>((s1, s2) => { ((System.Action<T1, T2>)action)(s1, s2); });
        });
    }

    static void RegistUnityDelegate<T>(ILRuntime.Runtime.Enviorment.AppDomain app)
    {
        //ע����Ҫ�õ���ί������
        app.DelegateManager.RegisterMethodDelegate<T>();
        //ί��ת����
        app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<T>>((action) =>
        {
            return new UnityEngine.Events.UnityAction<T>((s) => { ((System.Action<T>)action)(s); });
        });
    }


    static void Comparison<T>(ILRuntime.Runtime.Enviorment.AppDomain app)
    {
        app.DelegateManager.RegisterFunctionDelegate<T, T, int>();
        app.DelegateManager.RegisterDelegateConvertor<System.Comparison<T>>((action) =>
        {
            return new System.Comparison<T>((x, y) => { return ((System.Func<T, T, int>)action)(x, y); });
        });
    }

    static void Predicate<T>(ILRuntime.Runtime.Enviorment.AppDomain app)
    {
        app.DelegateManager.RegisterFunctionDelegate<T, bool>();
        app.DelegateManager.RegisterDelegateConvertor<System.Predicate<T>>((action) =>
        {
            return new System.Predicate<T>((x) => { return ((System.Func<T, bool>)action)(x); });
        });
    }
    #endregion

}
