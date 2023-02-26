/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BoxShapeShooter : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeReference ] List< ShootBehaviour > shoot_behaviour_list;
    [ SerializeField ] float shoot_interval      = 2f;
    [ SerializeField ] float shoot_interval_last = 4;

  [ Title( "Shared" ) ]
	[ SerializeField ] ShapeData shape_data;
	[ SerializeField ] ListEdgeShooter list_edge_shooter;
	[ SerializeField ] ColorLibrary library_edge_color;
    [ SerializeField ] GameEvent event_level_complete;

    RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        for( var i = 0; i < shoot_behaviour_list.Count; i++ )
			shoot_behaviour_list[ i ].Init( shape_data, list_edge_shooter, library_edge_color );
	}
#endregion

#region API
    public void OnLevelStart()
    {
		var sequence = recycledSequence.Recycle();

        for( var i = 0; i < shoot_behaviour_list.Count; i++ )
        {
			sequence.AppendCallback( shoot_behaviour_list[ i ].Shoot );
			sequence.AppendInterval( shoot_interval );
		}

		sequence.AppendInterval( Mathf.Max( 0, shoot_interval_last - shoot_interval ) );
		sequence.AppendCallback( event_level_complete.Raise );
	}

    public void OnLevelFailed()
    {
		recycledSequence.Kill();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
