// Copyright (c) 2024. TikTok Inc.
//
// This source code is licensed under the MIT license found in
// the LICENSE file in the root directory of this source tree.
using System;

namespace SDK
{
    public interface ITikTokBusiness
    {
        void InitializeSdk(TikTokConfig config);
        
        void InitializeSdk(TikTokConfig config,Action<bool,int,string> completionHandler);
     
        void Identify(string externalId, string externalUserName, string phoneNumber, string email);
     
        void Logout();

        void TrackTTEvent(TikTokBaseEvent baseEvent);

        void StartTrack();
    }
    
}

