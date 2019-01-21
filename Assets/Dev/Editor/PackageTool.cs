using UnityEngine;
using UnityEditor;

public class PackageTool
{
    [MenuItem("Package/Update Package")]
    static void UpdatePackage()
    {
		AssetDatabase.ExportPackage( "Assets/ExposedUnityEvent", "ExposedUnityEvent.unitypackage", ExportPackageOptions.Recurse );
    }
}
