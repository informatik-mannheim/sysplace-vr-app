using UnityEditor;
using System.Collections;

public class CreateAssetBundles{

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles");
    }
}
