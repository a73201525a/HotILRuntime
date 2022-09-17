using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hotfix
{
    public class HotfixApplication
    {
        //�ȸ���������
        public static void Main()
        {
            Debug.Log("�����ȸ����! �����ȸ����");
            //��ʼ������Ϣģ��
            ILMsgCenter.Instance.Awake();
            ILUIManager.Instance.Initialize();
            ILNpcManager.Instance.Initialize();
            IAdventureManager.Instance.Initialize();
            HotfixTest hotfixTest = new HotfixTest();
            hotfixTest.Init();
        }
    }

    public class HotfixTest:ILAssetBase
    {
        public void Init()
        {
            GameObject go = new GameObject();
            go.name = "hotfixTest";
            ILMonoBehaviour iLMonoBehaviour = go.AddComponent<ILMonoBehaviour>();

            //��UI��ʾ
            ILUIManager.ShowView(UIPanelPath.MainScenePanel, UILevel.Dwon, ILUIEventPanel.MainScenePanel);

            //iLMonoBehaviour.OnUpdate += OnUpdate;
            //iLMonoBehaviour.OnLateUpdate += OnLateUpdate;
            //ILHunkAssetRes load = new ILHunkAssetRes(true, (ushort)ILAssetEvent.GetResObjectUI, "scenesone", "Load", "LoadPanel", (ushort)ILAssetEvent.GetResObjectCallBack, (ushort)UIAdventure.One);
            //SendMsg(load);
            //TCPConnectMsg tmp = new TCPConnectMsg((ushort)TCPEvent.TcpConnect, "192.168.3.16", 18034);
            //SendMsg(tmp);
            
        }

        private void OnLateUpdate()
        {
            Debug.Log("OnLateUpdate");
        }

        private void OnUpdate()
        {
            Debug.Log("OnUpdate");
        }
    }



}
