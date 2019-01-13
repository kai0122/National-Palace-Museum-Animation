using UnityEngine;
using UnityEditor;

public class AssignNormals : ScriptableObject {

	[MenuItem ("Component/Canyon/Lightmapping/Assign Normals To Shader")]
	public static void AssignNormalsScript() 
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
					char[] delim = { '-' };
					string[] objname = obj.name.Split( delim );
					
					Texture bump = (Texture)AssetDatabase.LoadAssetAtPath( "Assets/CanyonTerrain/Textures/Normals/" + objname[0] + ".png", typeof(Texture));		
					obj.GetComponent<Renderer>().sharedMaterial.SetTexture("_BumpMap", bump );
				}
			}
			
		}
		
		Debug.Log("[AssignNormalsScript] End");
	}
}

