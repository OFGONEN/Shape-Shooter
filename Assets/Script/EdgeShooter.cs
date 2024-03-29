/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class EdgeShooter : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] int shooter_index;
    [ SerializeField ] ShapeData shape_data;

  [ Title( "Shared" ) ]
    [ SerializeField ] PoolEdge pool_edge;
    [ SerializeField ] ListEdgeShooter list_edge_shooter;

  [ Title( "Components" ) ]
    [ SerializeField ] Transform shoot_transform_start;
    [ SerializeField ] Transform shoot_transform_end;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnEnable()
	{
		list_edge_shooter.AddDictionary( shooter_index, this );
	}

	private void OnDisable()
	{
		list_edge_shooter.RemoveDictionary( shooter_index );
	}
#endregion

#region API
    [ Button() ]
    public void Shoot( EdgeColorData data )
    {
		var edge = pool_edge.GetEntity();

		edge.Shoot( data, shoot_transform_start, shoot_transform_end, shape_data.shape_edge_spawn_start );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}