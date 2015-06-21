using UnityEngine;
using System.Collections;
using UnityEditor;

public class CommonMenu {

    [MenuItem("Common/Bake")]
    static void Init()
    {
        LightmapEditorSettings.maxAtlasHeight = 512;
        LightmapEditorSettings.maxAtlasWidth = 512;
        Lightmapping.Clear();
        Lightmapping.Bake();
    }
}
