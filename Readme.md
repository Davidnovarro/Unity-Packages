# Creating a Unity Package

This guide explains how to create a Unity package using the Facebook SDK as an example. Follow these steps to create your own package.

## Step-by-Step Instructions

### 1. Extract the Facebook SDK

1. Import the Facebook SDK `.unitypackage` into your Unity project.
2. After importing, collect all the created folders and files (including meta files).
3. Move these folders and files to a directory outside your Unity project. For example, create a folder named `FacebookSDK` on your drive and move them there.

### 2. Create the `package.json` File

Create a `package.json` file in the `FacebookSDK` folder with the following content:

```json
{
    "name": "com.facebook",
    "version": "17.0.1",
    "displayName": "Facebook SDK",
    "keywords": [
      "Facebook",
      "SDK"
    ],
    "author": {
      "name": "Facebook",
      "url": "https://developers.facebook.com/docs/unity/downloads/"
    },
    "description": "Facebook SDK (custom created package by @davitmk)",
    "unity": "2021.1",
    "dependencies": {
      "com.google.external-dependency-manager": "1.2.181"
    }
}
```

### 3. Pack the Package into a Tarball and Export to git package

1. Open any Unity project.
2. Open any script in the Unity project.
3. Call the following method in the script to create a .tgz file: `UnityEditor.PackageManager.Client.Pack("C:/FacebookSDK", "C:/FacebookSDK");`
4. Export the .tgz file content into a package folder next to it

### 4. Import the Package

<B>Option 1:</b> Open Unity Package Manager > Add package from git URL : `https://github.com/Davidnovarro/Unity-Packages.git?path=/Facebook/17.0.1/package`
<br>
<B>Option 2:</b> Modify the `manifest.json` to import the local .tgz as a package by adding the following line `file: "com.facebook": "file:../Assets/Packages/com.facebook.tgz"`
<br>
<br>
### ⚠️ Warning: Unity ignores `link.xml` files located in the Packages folder, which are used to preserve assembly files.
#### One of the ways to fix it is to include a the following Editor script in the package.
```
#if UNITY_EDITOR
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.UnityLinker;

namespace Facebook.Unity.PostProcess
{
    public class PreservePackageXmLinkFile : IUnityLinkerProcessor
    {
        int IOrderedCallback.callbackOrder => 0;
        string IUnityLinkerProcessor.GenerateAdditionalLinkXmlFile(BuildReport report, UnityLinkerBuildPipelineData data) => Path.GetFullPath("Packages/com.facebook/link.xml");
    }
}
#endif
```