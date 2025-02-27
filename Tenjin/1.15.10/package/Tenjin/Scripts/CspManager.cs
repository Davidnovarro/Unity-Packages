#if UNITY_EDITOR && !UNITY_2021_1_OR_NEWER
using System.Collections.Generic;
using System.IO;
using UnityEditor;

// @davitmk added this namespace. "- No namespace? Come on, guys, seriously?"
namespace TenjinSDK
{
    [InitializeOnLoad]
    public class CscRspManager
    {
        static CscRspManager()
        {
            UpdateCscRsp();
        }

        public static void UpdateCscRsp()
        {
            string rspPath = "Assets/csc.rsp";

            string lineToAdd = "-r:System.IO.Compression.FileSystem.dll";

            List<string> lines = File.Exists(rspPath) ? new List<string>(File.ReadAllLines(rspPath)) : new List<string>();

            if (!lines.Contains(lineToAdd))
            {
                lines.Add(lineToAdd);
                File.WriteAllLines(rspPath, lines.ToArray());
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif
