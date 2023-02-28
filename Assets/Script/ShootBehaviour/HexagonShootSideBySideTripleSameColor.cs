/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonShootSideBySideTripleSameColor : ShootBehaviour
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
		var randomColor = library_edge_color.GetRandomEdgeColorData();

		EdgeShooter edgeShooter;

		int index = Random.Range( 0, shape_data.shape_edge_count );

		list_edge_shooter.itemDictionary.TryGetValue( index, out edgeShooter );
		edgeShooter.Shoot( randomColor );

		list_edge_shooter.itemDictionary.TryGetValue( ( index + 1 ) % shape_data.shape_edge_count, out edgeShooter );
		edgeShooter.Shoot( randomColor );

		list_edge_shooter.itemDictionary.TryGetValue( ( index + 2 ) % shape_data.shape_edge_count, out edgeShooter );
		edgeShooter.Shoot( randomColor );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
