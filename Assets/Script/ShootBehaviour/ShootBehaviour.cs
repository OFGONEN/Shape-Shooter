/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ System.Serializable ]
public abstract class ShootBehaviour
{
#region Fields
    protected ShapeData shape_data;
    protected ListEdgeShooter list_edge_shooter;
    protected ColorLibrary library_edge_color;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Init( ShapeData data, ListEdgeShooter shooterList, ColorLibrary library )
    {
		shape_data         = data;
		list_edge_shooter  = shooterList;
		library_edge_color = library;
	}

    public abstract void Shoot();
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
