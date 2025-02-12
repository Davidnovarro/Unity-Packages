// Copyright (c) 2024. TikTok Inc.
//
// This source code is licensed under the MIT license found in
// the LICENSE file in the root directory of this source tree.

extern "C"{
void InitializeSdkFromUnityInvoke();
void IdentifyFromUnityInvoke(const char * externalId ,const char * externalUserName,const char * phoneNumber,const char * email);
void LogoutFromUnityInvoke();
}
