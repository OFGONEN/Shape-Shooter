/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAllSymmetricColor : ShootBehaviour
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
		
		for( var i = 0; i < shape_data.shape_edge_count; i += 2 )
		{
			list_edge_shooter.itemDictionary.TryGetValue( i, out edgeShooter );
			edgeShooter.Shoot( randomColorFirst );
		}

		for( var i = 1; i < shape_data.shape_edge_count; i += 2 )
		{
			list_edge_shooter.itemDictionary.TryGetValue( i, out edgeShooter );
			edgeShooter.Shoot( randomColorSecond );
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
