#if UNITY_EDITOR
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.UnityLinker;

namespace Facebook.Unity.PostProcess
{
    //@davitmk added this file
    //Unity ignores link.xml files that are located in packages
    //To resolve this issue we need to use this script
    public class PreservePackageXmLinkFile : IUnityLinkerProcessor
    {
        int IOrderedCallback.callbackOrder => 0;
        string IUnityLinkerProcessor.GenerateAdditionalLinkXmlFile(BuildReport report, UnityLinkerBuildPipelineData data) => Path.GetFullPath("Packages/com.facebook/link.xml");
    }
}
#endif