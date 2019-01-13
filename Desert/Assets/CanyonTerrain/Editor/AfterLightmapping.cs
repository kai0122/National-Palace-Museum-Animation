using UnityEngine;
using UnityEditor;

public class AfterLightmapping : ScriptableObject {

	[MenuItem ("Component/Canyon/Lightmapping/Assign Lightmaps To Low LODs")]
	public static void AfterLightmappingScript() 
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
					// Get High mesh lightmap index
					// find Medium and low meshes
					char[] delim = { '-' };
					string[] objname = obj.name.Split( delim );
					//Debug.Log ("Find what?: " + "Low " + objname[1]);
					
					GameObject lowmesh = GameObject.Find( objname[0] + "-Low" );
					lowmesh.GetComponent<Renderer>().lightmapIndex = obj.GetComponent<Renderer>().lightmapIndex;
					lowmesh.GetComponent<Renderer>().lightmapScaleOffset = obj.GetComponent<Renderer>().lightmapScaleOffset;
					
					//GameObject mediummesh = GameObject.Find( objname[0] + "-Medium" );
					//mediummesh.renderer.lightmapIndex = obj.renderer.lightmapIndex;
					//mediummesh.renderer.lightmapTilingOffset = obj.renderer.lightmapTilingOffset;				
				}
			}
			
		}
		
		Debug.Log("[AfterLightmappingScript] End");
	}
}

