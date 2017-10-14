using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
	[MenuItem("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles()
	{
		string assetBundleDirectory = "../Bundles/";
		if(!Directory.Exists(assetBundleDirectory)) {
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
		FileUtil.ReplaceFile(assetBundleDirectory + "CSNGVisuals", assetBundleDirectory + "ngv_windows.bundle");
		FileUtil.ReplaceFile(assetBundleDirectory + "CSNGVisuals.manifest", assetBundleDirectory + "ngv_windows.bundle.manifest");

		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);
		FileUtil.ReplaceFile(assetBundleDirectory + "CSNGVisuals", assetBundleDirectory + "ngv_linux.bundle");
		FileUtil.ReplaceFile(assetBundleDirectory + "CSNGVisuals.manifest", assetBundleDirectory + "ngv_linux.bundle.manifest");

		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXIntel64);
		FileUtil.ReplaceFile(assetBundleDirectory + "CSNGVisuals", assetBundleDirectory + "ngv_osx.bundle");
		FileUtil.ReplaceFile(assetBundleDirectory + "CSNGVisuals.manifest", assetBundleDirectory + "ngv_osx.bundle.manifest");

		FileUtil.DeleteFileOrDirectory (assetBundleDirectory + "Bundles");
		FileUtil.DeleteFileOrDirectory (assetBundleDirectory + "Bundles.manifest");
		FileUtil.DeleteFileOrDirectory (assetBundleDirectory + "CSNGVisuals");
		FileUtil.DeleteFileOrDirectory (assetBundleDirectory + "CSNGVisuals.manifest");
	}
}