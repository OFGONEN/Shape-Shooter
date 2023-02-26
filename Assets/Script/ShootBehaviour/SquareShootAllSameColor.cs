/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareShootAllSameColor : ShootBehaviour
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

		list_edge_shooter.itemDictionary.TryGetValue( 0, out edgeShooter );
		edgeShooter.Shoot( randomColor );

		list_edge_shooter.itemDictionary.TryGetValue( 1, out edgeShooter );
		edgeShooter.Shoot( randomColor );

		list_edge_shooter.itemDictionary.TryGetValue( 2, out edgeShooter );
		edgeShooter.Shoot( randomColor );

		list_edge_shooter.itemDictionary.TryGetValue( 3, out edgeShooter );
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
