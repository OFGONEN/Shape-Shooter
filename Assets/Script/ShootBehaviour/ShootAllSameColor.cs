/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAllSameColor : ShootBehaviour
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

		for( var i = 0; i < shape_data.shape_edge_count; i++ )
		{
			list_edge_shooter.itemDictionary.TryGetValue( i, out edgeShooter );
			edgeShooter.Shoot( randomColor );
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
