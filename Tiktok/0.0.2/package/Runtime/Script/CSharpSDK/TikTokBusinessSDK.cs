// Copyright (c) 2024. TikTok Inc.
//
// This source code is licensed under the MIT license found in
// the LICENSE file in the root directory of this source tree.
using System;

namespace SDK
{
    public static class TikTokBusinessSDK
    {
        public static string TikTokUnitySDKVersion = "0.0.2";

        public static void InitializeSdk(TikTokConfig config)
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().InitializeSdk(config);
        }

        public static void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            if (completionHandler == null)
            {
                TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().InitializeSdk(config);
            }
            else
            {
                TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().InitializeSdk(config,completionHandler);
            }
        }
        
        public static void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
            string _externalId = (externalId == null) ? "" : externalId;
            string _externalUserName = (externalUserName == null) ? "" : externalUserName;
            string _phoneNumber = (phoneNumber == null) ? "" : phoneNumber;
            string _email = (email == null) ? "" : email;

            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().Identify(_externalId,_externalUserName,_phoneNumber,_email);
        }
        
        public static void Logout()
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().Logout();
        }
        
        public static void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().TrackTTEvent(baseEvent);
        }

        public static void StartTrack()
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().StartTrack();
        }
    }
}