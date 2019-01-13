using UnityEngine;
using UnityEditor;

public class RemoveNormals : ScriptableObject {

	[MenuItem ("Component/Canyon/Lightmapping/Remove Normals From Shader")]
	public static void RemoveNormalsScript() 
	{
		//Debug.Log("[AfterLightmappingScript] Start");			
		
		Transform CurrentSelect = Selection.activeTransform;
		Transform[] objects = CurrentSelect.GetComponentsInChildren<Transform>();
		foreach(Transform obj in objects)
		{
			// First assign lightmaps to medium and low terrain zones
			if( (Renderer)obj.GetComponent(typeof(Renderer)) != null ) 
			{
				if( obj.name.Contains("High") )
				{
					//char[] delim = { '-' };
					//string[] objname = obj.name.Split( delim );
					
					//Texture bump = (Texture)AssetDatabase.LoadAssetAtPath( "Assets/CanyonTerrain/Textures/Normals/" + objname[0] + ".png", typeof(Texture));		
					obj.GetComponent<Renderer>().sharedMaterial.SetTexture("_BumpMap", null );
				}
			}
			
		}
		
		Debug.Log("[RemoveNormalsScript] End");
	}
}

