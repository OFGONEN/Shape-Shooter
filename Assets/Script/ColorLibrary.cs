/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "library_edge_color_data", menuName = "FF/Game/Library Edge Color Data" ) ]
public class ColorLibrary : ScriptableObject
{
#region Fields
    [ SerializeField ] EdgeColorData[] edge_color_data_array;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public EdgeColorData GetEdgeColorData( int index )
    {
		return edge_color_data_array[ index ];
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
