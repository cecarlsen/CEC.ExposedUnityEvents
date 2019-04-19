using UnityEngine;
using UnityEditor;

public class PackageTool
{
    [MenuItem("Package/Update Package")]
    static void UpdatePackage()
    {
		AssetDatabase.ExportPackage( "Assets/CEC/ExposedUnityEvents", "CEC.ExposedUnityEvents.unitypackage", ExportPackageOptions.Recurse );
        Debug.Log( "Package is build!" );
    }
}
