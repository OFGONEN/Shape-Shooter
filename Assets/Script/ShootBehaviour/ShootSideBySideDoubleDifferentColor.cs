/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSideBySideDoubleDifferentColor : ShootBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public override void Shoot()
    {
		var shooterIndexFirst  = Random.Range( 0, shape_data.shape_edge_count );
		var shooterIndexSecond = ( shooterIndexFirst + 1 ) % shape_data.shape_edge_count;

		var randomColorFirst  = library_edge_color.GetRandomDistinctEdgeColorData();
		var randomColorSecond = library_edge_color.GetRandomDistinctEdgeColorData();
		library_edge_color.RefreshDistinctColorList();

		EdgeShooter edgeShooter;

		list_edge_shooter.itemDictionary.TryGetValue( shooterIndexFirst, out edgeShooter );
		edgeShooter.Shoot( randomColorFirst );

		list_edge_shooter.itemDictionary.TryGetValue( shooterIndexSecond, out edgeShooter );
		edgeShooter.Shoot( randomColorSecond );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
