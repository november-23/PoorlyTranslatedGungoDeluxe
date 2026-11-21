using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class bundlebuilder
{
    [MenuItem("Assets/Build AssetBundles")]
	public static void BuildBundles()
    {
        var assetBundleDirectory = "AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
            Directory.CreateDirectory(assetBundleDirectory);

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);
    }
}
