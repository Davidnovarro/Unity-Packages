// @davitmk added this script
// This script is necessary for iOS builds to automatically embed Dynamic Libraries when 'Link Frameworks Statically' is enabled in ExternalDependencyManager > iOS Settings.
// Sources:
// https://github.com/AppLovin/AppLovin-MAX-Unity-Plugin/commit/07378eee00a848f360c83ac372ef82acb796b552
// https://github.com/AppLovin/AppLovin-MAX-Unity-Plugin/blob/master/DemoApp/Assets/MaxSdk/Scripts/IntegrationManager/Editor/AppLovinPostProcessiOS.cs
#if UNITY_IOS || UNITY_IPHONE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

namespace Facebook.Unity.PostProcess
{
    public class EmbedDynamicLibsPostProcessIOS
    {
        private const string TargetUnityIphonePodfileLine = "target 'Unity-iPhone' do";
        private const string UseFrameworksPodfileLine = "use_frameworks!";
        private const string UseFrameworksDynamicPodfileLine = "use_frameworks! :linkage => :dynamic";
        private const string UseFrameworksStaticPodfileLine = "use_frameworks! :linkage => :static";
        
        private static List<string> DynamicLibrariesToEmbed
        {
            get
            {
                var dynamicLibrariesToEmbed = new List<string>
                {
                    "FBAEMKit.xcframework",
                    "FBSDKCoreKit_Basics.xcframework",
                    "FBSDKCoreKit.xcframework",
                    "FBSDKGamingServicesKit.xcframework",
                    "FBSDKLoginKit.xcframework",
                    "FBSDKShareKit.xcframework",
                };

                return dynamicLibrariesToEmbed;
            }
        }
    
        
        [PostProcessBuild(int.MaxValue)]
        public static void PostProcessPbxProject(BuildTarget buildTarget, string buildPath)
        {
            var projectPath = PBXProject.GetPBXProjectPath(buildPath);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);
            var unityMainTargetGuid = project.GetUnityMainTargetGuid();
            EmbedDynamicLibrariesIfNeeded(buildPath, project, unityMainTargetGuid);
            project.WriteToFile(projectPath);
        }
        

        private static void EmbedDynamicLibrariesIfNeeded(string buildPath, PBXProject project, string targetGuid)
        {
            // Check that the Pods directory exists (it might not if a publisher is building with Generate Podfile setting disabled in EDM).
            var podsDirectory = Path.Combine(buildPath, "Pods");
            if (!Directory.Exists(podsDirectory)) return;

            var dynamicLibraryPathsPresentInProject = new List<string>();
            foreach (var dynamicLibraryToSearch in DynamicLibrariesToEmbed)
            {
                // both .framework and .xcframework are directories, not files
                var directories = Directory.GetDirectories(podsDirectory, dynamicLibraryToSearch, SearchOption.AllDirectories);
                if (directories.Length <= 0) continue;

                var dynamicLibraryAbsolutePath = directories[0];
                var index = dynamicLibraryAbsolutePath.LastIndexOf("Pods", StringComparison.Ordinal);
                var relativePath = dynamicLibraryAbsolutePath.Substring(index);
                dynamicLibraryPathsPresentInProject.Add(relativePath);
            }

            if (dynamicLibraryPathsPresentInProject.Count <= 0) return;

            if (ShouldEmbedDynamicLibraries(buildPath))
            {
                foreach (var dynamicLibraryPath in dynamicLibraryPathsPresentInProject)
                {
                    var fileGuid = project.AddFile(dynamicLibraryPath, dynamicLibraryPath);
                    project.AddFileToEmbedFrameworks(targetGuid, fileGuid);
                }
            }
        }
        
        
        /// <summary>
        /// |-----------------------------------------------------------------------------------------------------------------------------------------------------|
        /// |         embed             |  use_frameworks! (:linkage => :dynamic)  |  use_frameworks! :linkage => :static  |  `use_frameworks!` line not present  |
        /// |---------------------------|------------------------------------------|---------------------------------------|--------------------------------------|
        /// | Unity-iPhone present      | Do not embed dynamic libraries           | Embed dynamic libraries               | Do not embed dynamic libraries       |
        /// | Unity-iPhone not present  | Embed dynamic libraries                  | Embed dynamic libraries               | Embed dynamic libraries              |
        /// |-----------------------------------------------------------------------------------------------------------------------------------------------------|
        /// </summary>
        /// <param name="buildPath">An iOS build path</param>
        /// <returns>Whether or not the dynamic libraries should be embedded.</returns>
        private static bool ShouldEmbedDynamicLibraries(string buildPath)
        {
            var podfilePath = Path.Combine(buildPath, "Podfile");
            if (!File.Exists(podfilePath)) return false;

            // If the Podfile doesn't have a `Unity-iPhone` target, we should embed the dynamic libraries.
            var lines = File.ReadAllLines(podfilePath);
            var containsUnityIphoneTarget = lines.Any(line => line.Contains(TargetUnityIphonePodfileLine));
            if (!containsUnityIphoneTarget) return true;

            // If the Podfile does not have a `use_frameworks! :linkage => static` line, we should not embed the dynamic libraries.
            var useFrameworksStaticLineIndex = Array.FindIndex(lines, line => line.Contains(UseFrameworksStaticPodfileLine));
            if (useFrameworksStaticLineIndex == -1) return false;

            // If more than one of the `use_frameworks!` lines are present, CocoaPods will use the last one.
            var useFrameworksLineIndex = Array.FindIndex(lines, line => line.Trim() == UseFrameworksPodfileLine); // Check for exact line to avoid matching `use_frameworks! :linkage => static/dynamic`
            var useFrameworksDynamicLineIndex = Array.FindIndex(lines, line => line.Contains(UseFrameworksDynamicPodfileLine));

            // Check if `use_frameworks! :linkage => :static` is the last line of the three. If it is, we should embed the dynamic libraries.
            return useFrameworksLineIndex < useFrameworksStaticLineIndex && useFrameworksDynamicLineIndex < useFrameworksStaticLineIndex;
        }
    }
}
#endif
