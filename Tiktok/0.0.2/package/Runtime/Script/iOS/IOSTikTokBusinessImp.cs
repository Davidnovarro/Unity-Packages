// Copyright (c) 2024. TikTok Inc.
//
// This source code is licensed under the MIT license found in
// the LICENSE file in the root directory of this source tree.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SDK
{
#if UNITY_IOS

    public class IOSTikTokBusinessImp : ITikTokBusiness
    {
        
        private delegate void TikTokBusinessInitHandler(bool result, int code, string message);
        
        private static Action<bool, int, string> _initHandler;


        // 初始化接口
        public void InitializeSdk(TikTokConfig config)
        {
            Debug.Log("Unity iOS InitializeSdk");
            InitializeSdkFromUnityInvoke(config.GetParamsStringFromUnityConfig());
        }
        
        // 初始化接口
        public void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            Debug.Log("Unity iOS InitializeSdk with completionHandler");
            _initHandler = completionHandler;
            InitializeSdkWithHandlerFromUnityInvoke(config.GetParamsStringFromUnityConfig(), TikTokBusinessInitHandlerMethod);
        }
     
        // Identify 接口
        public void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
            Debug.Log("Unity iOS Identify externalId:" + externalId + " externalUserName:" + externalUserName + " phoneNumber:" + phoneNumber + " email:" + email);
            IdentifyFromUnityInvoke(externalId, externalUserName, phoneNumber, email);
        }
     
        // Logout 接口
        public void Logout()
        {
            Debug.Log("Unity iOS Logout");
            LogoutFromUnityInvoke();
        }
        
        // 事件上报接口
        public void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            Debug.Log("Unity iOS TrackTTEvent");
            Dictionary<string, string> eventParams = baseEvent.EventParams;
            TrackTTEventFromUnityInvoke(eventParams["eventName"],eventParams["eventId"],eventParams["properties"]);
        }

        public void StartTrack()
        {
            Debug.Log("Unity iOS StartTrack");
            StartTrackFromUnityInvoke();
        }
        
        // ###################### 与C ｜ C++  代码交互 start #######################

        //通过C ｜ C++与OC交互
        [DllImport("__Internal")]
        private static extern void InitializeSdkFromUnityInvoke(string configParams);
        [DllImport("__Internal")]
        private static extern void InitializeSdkWithHandlerFromUnityInvoke(string configParams, Action<bool, int, string> completionHandler);
        [DllImport("__Internal")]
        private static extern void IdentifyFromUnityInvoke(string externalId, string externalUserName, string phoneNumber, string email);
        [DllImport("__Internal")]
        private static extern void LogoutFromUnityInvoke();
        [DllImport("__Internal")]
        private static extern void StartTrackFromUnityInvoke();
        
        [DllImport("__Internal")]
        private static extern void TrackTTEventFromUnityInvoke(string eventName,string eventId ,string properties);
        
        [AOT.MonoPInvokeCallback(typeof(TikTokBusinessInitHandler))]
        private static void TikTokBusinessInitHandlerMethod(bool result, int code, string message)
        {
            if (_initHandler != null)
            {
                Debug.Log("Unity iOS InitializeSdk result:" + result + " code:" + code + "message:" + message);
                _initHandler.Invoke(result,code,message);
            }
            else
            {
                Debug.Log("Unity iOS InitializeSdk _initHandler == null");
            }
        }
        // ###################### 与C ｜ C++  代码交互 end #######################
    }
#endif

}