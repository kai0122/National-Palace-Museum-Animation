using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]

public class TerrainLOD : MonoBehaviour {

	private enum LOD_LEVEL { LOD0, LOD1, LOD2 };
	private MeshRenderer LOD0;
	private MeshRenderer LOD1;
	private MeshRenderer LOD2;

	private float DistanceLOD1 = 2000f;
	private float DistanceLOD2 = 4000f;
	
	public float UpdateInterval = 0.2f;
	private float currentTimeToInterval = 0.0f;
	private LOD_LEVEL LOD = LOD_LEVEL.LOD0;

	private void Start()
	{
		//Debug.Log("[TerrainLOD] Start");
	
		currentTimeToInterval = UpdateInterval;
		Color fogColor = RenderSettings.fogColor * 0.5f;
		fogColor.a = 1.0f;
	
		if(gameObject.name.Contains("High")) // This is the high LOD node which will be mantained in scene and have 
			// its LOD changed by meshes from the other LOD nodes.
		{
			// Determine the LOD meshes based on the name of the current terrain zone.
			// We assume that the terrain zones are named Low_node_X, Medium_node_X, High_node_X
			char[] delim = { '-' };
			string[] objname = gameObject.name.Split( delim );

			// High detail mesh renderer reference.
			LOD0 = (MeshRenderer)GetComponent(typeof(MeshRenderer));
			//if(LOD0)
				//Debug.Log("[TerrainLOD] Detail mesh: High " + terrain_zone_str);
			LOD0.sharedMaterial.SetColor("_FogColor", fogColor );
			// Medium detail mesh.
			GameObject obj = GameObject.Find( objname[0] + "-Medium" );
			if(obj)
			{
				LOD1 = (MeshRenderer)obj.GetComponent(typeof(MeshRenderer));
				LOD1.sharedMaterial.SetColor("_FogColor", fogColor );
				//if(LOD1)
					//Debug.Log("[TerrainLOD] Detail mesh: " + terrain_zone_str);
			}
	
			// Low detail mesh.
			obj = GameObject.Find( objname[0] + "-Low" );
			if(obj)
			{
				LOD2 = (MeshRenderer)obj.GetComponent(typeof(MeshRenderer));
				LOD2.sharedMaterial.SetColor("_FogColor", fogColor );
				//if(LOD2)
				//	Debug.Log("[TerrainLOD] Detail mesh: " + terrain_zone_str);
			}
		}
		else // This is a mesh from the other LODS, mark it as invisible.
		{
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Renderer>().castShadows = false;
			//Debug.Log("[TerrainLOD] Invisible LOD mesh: " + gameObject.name);
			
			// Remove this script from the game object to avoid calling Update.
			TerrainLOD lod_script = gameObject.GetComponent(typeof(TerrainLOD)) as TerrainLOD;
			if(lod_script)
			{
				//Debug.Log("[TerrainLOD] Remove LOD script");
				Object.DestroyImmediate(lod_script);
			}
		}
		if( LOD1 != null ) StartCoroutine( CoUpdate3Lods() );
		else StartCoroutine( CoUpdate2Lods() );
		//Debug.Log("[TerrainLOD] End");
	}
	
	IEnumerator CoUpdate3Lods()
	{
		while( true ) {
		yield return new WaitForSeconds( UpdateInterval );
		/*
		if(gameObject.renderer.enabled == false)
		{
			//Debug.Log("[TerrainLOD] We should not call Update for disabled LODs!");			
			return;
		}*/
			
		//if (currentTimeToInterval <= 0.0f)
		//{
			float distanceFromObject = Vector2.Distance(new Vector2
			(Camera.main.transform.position.x, Camera.main.transform.position.z),
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
	
			if (distanceFromObject < DistanceLOD1 && LOD != LOD_LEVEL.LOD0)
			{
				LOD = LOD_LEVEL.LOD0;
				LOD0.enabled = true;
				LOD1.enabled = false;
				LOD2.enabled = false;
				//Debug.Log("LOD0");
			}
			else if (distanceFromObject >= DistanceLOD1 && distanceFromObject < DistanceLOD2 && LOD != LOD_LEVEL.LOD1)
			{
				LOD = LOD_LEVEL.LOD1;
				LOD0.enabled = false;
				LOD1.enabled = true;
				LOD2.enabled = false;
				//Debug.Log("LOD1");
			}
			else if (distanceFromObject >= DistanceLOD2 && LOD != LOD_LEVEL.LOD2)
			{
				LOD = LOD_LEVEL.LOD2;
				LOD0.enabled = false;
				LOD1.enabled = false;
				LOD2.enabled = true;
				//Debug.Log("LOD2");
			}
	
			// Reset check timer.
			//currentTimeToInterval = UpdateInterval;
		/*}
		else
		{
			currentTimeToInterval -= Time.deltaTime;
		}*/
		}
	}	


	IEnumerator CoUpdate2Lods()
	{
		while( true ) {
			yield return new WaitForSeconds( UpdateInterval );
			/*
		if(gameObject.renderer.enabled == false)
		{
			//Debug.Log("[TerrainLOD] We should not call Update for disabled LODs!");			
			return;
		}*/
			
			//if (currentTimeToInterval <= 0.0f)
			//{
			float distanceFromObject = Vector2.Distance(new Vector2
			                                            (Camera.main.transform.position.x, Camera.main.transform.position.z),
			                                            new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
			
			if (distanceFromObject < DistanceLOD1)
			{
				LOD = LOD_LEVEL.LOD0;
				LOD0.enabled = true;
				LOD2.enabled = false;
				//renderer.material.shader.maximumLOD = 600;
				//Debug.Log("LOD0: " + LOD0.name );
			}
			else 
			{
				LOD = LOD_LEVEL.LOD2;
				LOD0.enabled = false;
				LOD2.enabled = true;
				//renderer.material.shader.maximumLOD = 200;
				//Debug.Log("LOD2: " + LOD2.name);
			}
			
			// Reset check timer.
			//currentTimeToInterval = UpdateInterval;
			/*}
		else
		{
			currentTimeToInterval -= Time.deltaTime;
		}*/
		}
	}	
}
