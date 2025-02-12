// Copyright (c) 2024. TikTok Inc.
//
// This source code is licensed under the MIT license found in
// the LICENSE file in the root directory of this source tree.

#import <TikTokBusinessSDK/TikTokBusinessSDK.h>

typedef void(*TikTokBusinessInitHandler)(bool result,int errCode,const char* errMsg);

#if defined (__cplusplus)
extern "C" {
#endif
    // unity层会调用到此方法
void InitializeSdkFromUnityInvoke(const char *configParams) {
    NSString *string = [NSString stringWithUTF8String:configParams];
    NSData *jsonData = [string dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error = nil;
    NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:&error];
    NSString *appId = dictionary[@"appId"];
    NSString *tiktokAppId = dictionary[@"tiktokAppId"];

    TikTokConfig *config = [[TikTokConfig alloc] initWithAppId:appId tiktokAppId:tiktokAppId];
            if ([dictionary[@"disableAutoTrack"] isEqualToString:@"1"]) {
                [config disableAutomaticTracking];
            }
            if ([dictionary[@"disableTrack"] isEqualToString:@"1"]) {
                [config disableTracking];
            }
            if ([dictionary[@"disableRetentionTrack"] isEqualToString:@"1"]) {
                [config disableRetentionTracking];
            }
            if ([dictionary[@"disablePayTrack"] isEqualToString:@"1"]) {
                [config disablePaymentTracking];
            }
            if ([dictionary[@"openDebugMode"] isEqualToString:@"1"]) {
                [config enableDebugMode];
            }
            if ([dictionary[@"OpenLimitedDataUse"] isEqualToString:@"1"]) {
                [config enableLDUMode];
            }
            if ([dictionary[@"disableAppAdTrack"] isEqualToString:@"1"]) {
                [config disableAppTrackingDialog];
            }
            if ([dictionary[@"iOS_disableSKAdNetworkSupport"] isEqualToString:@"1"]) {
                [config disableSKAdNetworkSupport];
            }
            if ([dictionary[@"disableInstallTrack"] isEqualToString:@"1"]) {
                [config disableInstallTracking];
            }
            if ([dictionary[@"disableLaunchTrack"] isEqualToString:@"1"]) {
                [config disableLaunchTracking];
            }
            if (dictionary[@"iOS_setDelayForATTUserAuthorizationInSeconds"]) {
                NSString *Seconds = dictionary[@"iOS_setDelayForATTUserAuthorizationInSeconds"];
                [config setDelayForATTUserAuthorizationInSeconds:Seconds.longLongValue];
            }
            if (dictionary[@"setLogLevel"]) {
                NSString *level = dictionary[@"setLogLevel"];
                if ([level isEqualToString:@"None"]) {
                    [config setLogLevel:TikTokLogLevelSuppress];
                } else if ([level isEqualToString:@"Info"]) {
                    [config setLogLevel:TikTokLogLevelInfo];
                } else if ([level isEqualToString:@"Warn"]) {
                    [config setLogLevel:TikTokLogLevelWarn];
                } else if ([level isEqualToString:@"Debug"]) {
                    [config setLogLevel:TikTokLogLevelDebug];
                } else if ([level isEqualToString:@"Verbose"]) {
                    [config setLogLevel:TikTokLogLevelVerbose];
                }
            }
    
            [TikTokBusiness initializeSdk:config];
    
}

void InitializeSdkWithHandlerFromUnityInvoke(const char *configParams,TikTokBusinessInitHandler handler) {
    NSString *string = [NSString stringWithUTF8String:configParams];
    NSData *jsonData = [string dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error = nil;
    NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:&error];
    NSString *appId = dictionary[@"appId"];
    NSString *tiktokAppId = dictionary[@"tiktokAppId"];
    
    TikTokConfig *config = [[TikTokConfig alloc] initWithAppId:appId tiktokAppId:tiktokAppId];
            if ([dictionary[@"disableAutoTrack"] isEqualToString:@"1"]) {
                [config disableAutomaticTracking];
            }
            if ([dictionary[@"disableTrack"] isEqualToString:@"1"]) {
                [config disableTracking];
            }
            if ([dictionary[@"disableRetentionTrack"] isEqualToString:@"1"]) {
                [config disableRetentionTracking];
            }
            if ([dictionary[@"disablePayTrack"] isEqualToString:@"1"]) {
                [config disablePaymentTracking];
            }
            if ([dictionary[@"openDebugMode"] isEqualToString:@"1"]) {
                [config enableDebugMode];
            }
            if ([dictionary[@"OpenLimitedDataUse"] isEqualToString:@"1"]) {
                [config enableLDUMode];
            }
            if ([dictionary[@"disableAppAdTrack"] isEqualToString:@"1"]) {
                [config disableAppTrackingDialog];
            }
            if ([dictionary[@"iOS_disableSKAdNetworkSupport"] isEqualToString:@"1"]) {
                [config disableSKAdNetworkSupport];
            }
            if ([dictionary[@"disableInstallTrack"] isEqualToString:@"1"]) {
                [config disableInstallTracking];
            }
            if ([dictionary[@"disableLaunchTrack"] isEqualToString:@"1"]) {
                [config disableLaunchTracking];
            }
            if (dictionary[@"iOS_setDelayForATTUserAuthorizationInSeconds"]) {
                NSString *Seconds = dictionary[@"iOS_setDelayForATTUserAuthorizationInSeconds"];
                [config setDelayForATTUserAuthorizationInSeconds:Seconds.longLongValue];
            }
            if (dictionary[@"setLogLevel"]) {
                NSString *level = dictionary[@"setLogLevel"];
                if ([level isEqualToString:@"None"]) {
                    [config setLogLevel:TikTokLogLevelSuppress];
                } else if ([level isEqualToString:@"Info"]) {
                    [config setLogLevel:TikTokLogLevelInfo];
                } else if ([level isEqualToString:@"Warn"]) {
                    [config setLogLevel:TikTokLogLevelWarn];
                } else if ([level isEqualToString:@"Debug"]) {
                    [config setLogLevel:TikTokLogLevelDebug];
                } else if ([level isEqualToString:@"Verbose"]) {
                    [config setLogLevel:TikTokLogLevelVerbose];
                }
            }
    
    [TikTokBusiness initializeSdk:config completionHandler:^(BOOL success, NSError * _Nullable error) {
        if (!error) {
            handler(YES,0,@"".UTF8String);
        } else {
            handler(NO,(int)error.code,error.description.UTF8String);
        }
    }];
}

void IdentifyFromUnityInvoke(const char *externalId ,const char *externalUserName,const char *phoneNumber,const char * email) {
    [TikTokBusiness identifyWithExternalID:[NSString stringWithUTF8String:externalId] externalUserName:[NSString stringWithUTF8String:externalUserName] phoneNumber:[NSString stringWithUTF8String:phoneNumber] email:[NSString stringWithUTF8String:email]];
}

void LogoutFromUnityInvoke() {
    [TikTokBusiness logout];
}

void StartTrackFromUnityInvoke() {
    [TikTokBusiness setTrackingEnabled:YES];
}

void TrackTTEventFromUnityInvoke(const char *eventName,const char *eventId ,const char *properties) {
    NSString *_eventName = [NSString stringWithUTF8String:eventName];
    NSString *_eventId = [NSString stringWithUTF8String:eventId];
    NSString *propertiesString = [NSString stringWithUTF8String:properties];
    NSData *jsonData = [propertiesString dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error = nil;
    NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:&error];
    TikTokBaseEvent *event = [[TikTokBaseEvent alloc] initWithEventName:_eventName properties:dictionary eventId:_eventId];
    [TikTokBusiness trackTTEvent:event];
}

#if defined (__cplusplus)
}
#endif
