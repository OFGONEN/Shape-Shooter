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
    public void Shoot( EdgeColorData data, Transform start, Transform end )
    {
		var edge = pool_edge.GetEntity();
		edge.Shoot( data, start, end );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}