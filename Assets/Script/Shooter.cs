/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;

public class Shooter : MonoBehaviour
{
#region Fields
    [ SerializeField ] ShooterData[] shooter_data_array;
	[ SerializeField ] ListEdgeShooter list_edge_shooter;

    RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        for( var i = 0; i < shooter_data_array.Length; i++ )
			shooter_data_array[ i ].Init( list_edge_shooter );
    }
#endregion

#region API
    public void OnLevelStart()
    {
		var sequence = recycledSequence.Recycle();

        for( var i = 0; i < shooter_data_array.Length; i++ )
        {
			sequence.AppendCallback( shooter_data_array[ i ].Shoot );
			sequence.AppendInterval( shooter_data_array[ i ].shoot_cooldown );
		}
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}


[ System.Serializable ]
public struct ShooterData
{
	public ShootData[] shoot_data_array;
	public float shoot_cooldown;
	[ HideInInspector ] public ListEdgeShooter list_edge_shooter;

    public void Init( ListEdgeShooter list )
    {
		list_edge_shooter = list;
	}

    public void Shoot()
    {
        for( var i = 0; i < shoot_data_array.Length; i++ )
        {
			var shootData = shoot_data_array[ i ];

			EdgeShooter edgeShooter;
			list_edge_shooter.itemDictionary.TryGetValue( shootData.shoot_index, out edgeShooter );

			edgeShooter.Shoot( shootData.shoot_color );
		}
    }
}

[ System.Serializable ]
public struct ShootData
{
	public int shoot_index;
	public EdgeColorData shoot_color;
}