using AdventuresUnknownSDK.Core.Objects;
using System;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknownSDK.Editor
{
    public static class ExportAssetBundles
    {
        [MenuItem("AdventuresUnknown/Export AssetBundles")]
        public static void Export()
        {
            BuildPipeline.BuildAssetBundles("Assets", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }
    }
}
