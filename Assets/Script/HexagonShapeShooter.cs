/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class HexagonShapeShooter : MonoBehaviour
{
#region Fields
    [ SerializeField ] HexagonShootData[] shoot_data_array;
    [ SerializeField ] Vector2 shoot_interval;
    [ ShowInInspector, ReadOnly ] List< HexagonShootData > shoot_data_list;

	int shoot_id_previous;
	int shoot_count_previous;
	UnityMessage onShoot;
    Cooldown cooldown = new Cooldown();
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        shoot_data_list = new List< HexagonShootData >( 100 );

		shoot_id_previous    = -1;
		shoot_count_previous = 0;

		for( var i = 0; i < shoot_data_array.Length; i++ )
        {
			shoot_data_array[ i ].shoot_id = i;

			for( var x = 0; x < shoot_data_array[ i ].shoot_chance; x++ )
				shoot_data_list.Add( shoot_data_array[ i ] );
        }

		onShoot = Shoot;
	}
#endregion

#region API
    public void OnLevelStart()
    {
		onShoot();
	}

    public void OnLevelFailed()
    {
		onShoot = Extensions.EmptyMethod;
		cooldown.Kill();
	}
#endregion

#region Implementation
    HexagonShootData DetermineShootMethod()
    {
		HexagonShootData shootData = shoot_data_list.ReturnRandom();

        if( shootData.shoot_id == shoot_id_previous )
        {
            if( shootData.shoot_count >= shoot_count_previous )
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

		cooldown.Start( shoot_interval.ReturnRandom(), Shoot );
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