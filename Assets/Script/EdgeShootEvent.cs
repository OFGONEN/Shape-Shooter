/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_edge_shoot", menuName = "FF/Game/Event Edge Shoot" ) ]
public class EdgeShootEvent : GameEvent 
{
#region Fields
    public EdgeColorData event_data_edge_color;
    public int event_data_index;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Raise( EdgeColorData data, int index )
    {
		event_data_edge_color = data;
		event_data_index      = index;

		Raise();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
