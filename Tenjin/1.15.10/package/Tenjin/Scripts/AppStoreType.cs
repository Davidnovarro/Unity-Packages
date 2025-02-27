//
//  Copyright (c) 2022 Tenjin. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @davitmk added this namespace. "- No namespace? Come on, guys, seriously?"
namespace TenjinSDK
{
    public enum AppStoreType
    {
        unspecified,

        /**
         * Google Play Store
         */
        googleplay,

        /**
         * Amazon Appstore
         */
        amazon,

        /**
         * Other App Stores
         */
        other
    }
}
