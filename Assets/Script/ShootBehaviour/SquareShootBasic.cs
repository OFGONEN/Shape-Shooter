/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareShootBasic : ShootBehaviour
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
		var shooterIndex = Random.Range( 0, shape_data.shape_edge_count );
		var randomColor  = library_edge_color.GetRandomEdgeColorData();

		EdgeShooter edgeShooter;
		list_edge_shooter.itemDictionary.TryGetValue( shooterIndex, out edgeShooter );

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