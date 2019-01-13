using UnityEditor;
 
public class AtlasSize_512 : EditorWindow
{
    [MenuItem("Component/Canyon/Lightmapping/AtlasSize/512")]
    static void Init()
    {
        LightmapEditorSettings.maxAtlasHeight = 512;
        LightmapEditorSettings.maxAtlasSize = 512;
    }
}
 
public class AtlasSize_1K : EditorWindow
{
    [MenuItem("Component/Canyon/Lightmapping/AtlasSize/1K")]
    static void Init()
    {
        LightmapEditorSettings.maxAtlasHeight = 1024;
        LightmapEditorSettings.maxAtlasSize = 1024;
    }
}
 
public class AtlasSize_2K : EditorWindow
{
    [MenuItem("Component/Canyon/Lightmapping/AtlasSize/2K")]
    static void Init()
    {
        LightmapEditorSettings.maxAtlasHeight = 2048;
        LightmapEditorSettings.maxAtlasSize = 2048;
    }
}
 
public class AtlasSize_4K : EditorWindow
{
    [MenuItem("Component/Canyon/Lightmapping/AtlasSize/4K")]
    static void Init()
    {
        LightmapEditorSettings.maxAtlasHeight = 4096;
        LightmapEditorSettings.maxAtlasSize = 4096;
    }
}
