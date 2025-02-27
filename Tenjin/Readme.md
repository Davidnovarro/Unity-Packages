### ⚠️ Note: @davitmk added the TenjinSDKEditor.asmdef and TenjinSDK.asmdef files.
This is important because, without them, Unity might ignore C# scripts from the package that don't have .asmdef files defined.

### ⚠️ Note: @davitmk added TenjinSDK namespace to all c# files
- No namespace? Come on, guys, seriously?

### ⚠️ Note: @davitmk updated the integration method for Plugins\iOS\TenjinSDK.xcframework
Instead of extracting the .zip file during the post-process, we now extract TenjinSDK.xcframework directly into the Plugins\iOS\ folder.
This delegates the Xcode integration to Unity, which is the recommended approach.
To ensure everything works correctly, we need to configure the proper settings:
* Enable "Add to Embedded Binaries"
* Ensure the required framework dependencies are enabled: AdServices.framework, AdSupport.framework, AppTrackingTransparency.framework, StoreKit.framework
Additionally, we have commented out OnPostProcessBuild and EmbedSignFramework in BuildPostProcessor.cs to prevent the old integration method from being invoked.


### ⚠️ Note: @davitmk added Tenjin Attribution endpoint to be set in Xcode during the build process
In the BuildPostProcessor.UpdatePlist() the "NSAdvertisingAttributionReportEndpoint" is set to "https://tenjin-skan.com"

### ⚠️ Note: @davitmk commented out variour editor scripts that create unnecessary menu items and clutters the user's project.
Editor\BuildPostProcessor.cs : ExportTenjinUnityPackage()
Tenjin\Scripts\Editor\TenjinAssetSelector.cs
Tenjin\Scripts\Editor\TenjinEditorPrefs.cs
Tenjin\Scripts\Editor\TenjinPackager.cs

### Installation:
Open the Unity Package Manager and add a package from a Git URL. "https://github.com/Davidnovarro/Unity-Packages.git?path=/Tenjin/1.15.10/package"