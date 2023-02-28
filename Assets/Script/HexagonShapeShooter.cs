/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class HexagonShapeShooter : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] HexagonShootData[] shoot_data_array;
    [ SerializeField ] Vector2 shoot_interval;
    [ ShowInInspector, ReadOnly ] List< HexagonShootData > shoot_data_list;

  [ Title( "Shared" ) ]
	[ SerializeField ] ShapeData shape_data;
	[ SerializeField ] ListEdgeShooter list_edge_shooter;
	[ SerializeField ] ColorLibrary library_color;

	int shoot_id_previous;
	int shoot_count_previous;
	RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
	private void OnDisable()
	{
		recycledTween.Kill();
	}

    private void Awake()
    {
        shoot_data_list = new List< HexagonShootData >( 100 );

		shoot_id_previous    = -1;
		shoot_count_previous = 0;

		for( var i = 0; i < shoot_data_array.Length; i++ )
        {
			shoot_data_array[ i ].shoot_id = i;
			shoot_data_array[ i ].shoot_behaviour.Init( shape_data, list_edge_shooter, library_color );

			for( var x = 0; x < shoot_data_array[ i ].shoot_chance; x++ )
				shoot_data_list.Add( shoot_data_array[ i ] );
        }
	}
#endregion

#region API
    public void OnLevelStart()
    {
		Shoot();
	}

    public void OnLevelFailed()
    {
		FFLogger.Log( "OnLevelFailed" );
		recycledTween.Kill();
	}
#endregion

#region Implementation
    HexagonShootData DetermineShootMethod()
    {
		HexagonShootData shootData = shoot_data_list.ReturnRandom();

        if( shootData.shoot_id == shoot_id_previous )
        {
            if( shoot_count_previous >= shootData.shoot_count )
            {
			    do
			    {
				    shootData = shoot_data_list.ReturnRandom();

			    } while( shootData.shoot_id == shoot_id_previous );

			    shoot_count_previous = 0;
            }
		}
        else
			shoot_count_previous = 0;


		shoot_id_previous = shootData.shoot_id;
		shoot_count_previous++;

		return shootData;
	}

    void Shoot()
    {
		var shootData = DetermineShootMethod();
		shootData.shoot_behaviour.Shoot();

		recycledTween.Recycle( DOVirtual.DelayedCall( shoot_interval.ReturnRandom(), Extensions.EmptyMethod ), Shoot );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

[ System.Serializable ]
public struct HexagonShootData
{
	[ SerializeReference ] public ShootBehaviour shoot_behaviour;
	[ ReadOnly ] public int shoot_id;
	public int shoot_chance;
	public int shoot_count; // consecutive
}