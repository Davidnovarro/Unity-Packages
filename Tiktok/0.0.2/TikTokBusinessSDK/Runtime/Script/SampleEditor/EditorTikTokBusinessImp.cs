// Copyright (c) 2024. TikTok Inc.
//
// This source code is licensed under the MIT license found in
// the LICENSE file in the root directory of this source tree.
using System;
using UnityEngine;

namespace SDK
{
#if UNITY_EDITOR

    public class EditorTikTokBusinessImp : ITikTokBusiness
    {
        // 初始化接口
        public void InitializeSdk(TikTokConfig config)
        {
            Debug.Log("Uniy Editor InitializeSdk");
        }
        
        // 初始化接口
        public void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            if (completionHandler != null)
            {
                Debug.Log("Uniy Editor InitializeSdk with completionHandler");
                completionHandler.Invoke(true,0,"");
            }
            else
            {
                Debug.Log("Uniy Editor InitializeSdk without completionHandler");

            }
        }
     
        // Identify 接口
        public void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
            Debug.Log("Uniy Editor Identify externalId:" + externalId + " externalUserName:" + externalUserName + " phoneNumber:" + phoneNumber + " email:" + email);
        }
     
        // Logout 接口
        public void Logout()
        {
            Debug.Log("Uniy Editor Logout");
        }
        
        // 事件上报接口
        public void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            Debug.Log("Uniy Editor TrackTTEvent");
            foreach (var pair in baseEvent.getEventParams())
            {
                Debug.Log($"Key: {pair.Key}, Value: {pair.Value}");
            }
        }

        public void StartTrack()
        {
            Debug.Log("Uniy Editor StartTrack");
        }
    }
#endif

}