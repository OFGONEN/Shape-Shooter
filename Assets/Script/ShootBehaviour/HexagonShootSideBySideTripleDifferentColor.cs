/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonShootSideBySideTripleDifferentColor : ShootBehaviour
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
		var randomColorFirst  = library_edge_color.GetRandomDistinctEdgeColorData();
		var randomColorSecond = library_edge_color.GetRandomDistinctEdgeColorData();

		library_edge_color.RefreshDistinctColorList();

		EdgeShooter edgeShooter;

		int index = Random.Range( 0, shape_data.shape_edge_count );

		list_edge_shooter.itemDictionary.TryGetValue( index, out edgeShooter );
		edgeShooter.Shoot( randomColorFirst );

		list_edge_shooter.itemDictionary.TryGetValue( ( index + 1 ) % shape_data.shape_edge_count, out edgeShooter );
		edgeShooter.Shoot( randomColorSecond );

		list_edge_shooter.itemDictionary.TryGetValue( ( index + 2 ) % shape_data.shape_edge_count, out edgeShooter );
		edgeShooter.Shoot( randomColorFirst );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
