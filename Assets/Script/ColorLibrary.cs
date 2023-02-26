/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[CreateAssetMenu( fileName = "library_edge_color_data", menuName = "FF/Game/Library Edge Color Data" )]
public class ColorLibrary : ScriptableObject
{
#region Fields
	[ SerializeField ] EdgeColorData[] edge_color_data_array;

    List< EdgeColorData > edge_color_data_list;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Init()
    {
        edge_color_data_list = new List< EdgeColorData >( edge_color_data_array.Length );

        for( var i = 0; i < edge_color_data_array.Length; i++ )
			edge_color_data_list.Add( edge_color_data_array[ i ] );
    }

	public EdgeColorData GetEdgeColorData( int index )
	{
		return edge_color_data_array[ index ];
	}

	public EdgeColorData GetRandomEdgeColorData()
	{
		return edge_color_data_array.ReturnRandom();
	}

    //! Can produce exception
    public EdgeColorData GetRandomDistinctEdgeColorData()
    {
		var index = Random.Range( 0, edge_color_data_list.Count );
		var data  = edge_color_data_list[ index ];

		edge_color_data_list.RemoveAt( index );

		return data;
	}
    
    public void RefreshDistinctColorList()
    {
		edge_color_data_list.Clear();

		for( var i = 0; i < edge_color_data_array.Length; i++ )
		    edge_color_data_list.Add( edge_color_data_array[ i ] );
    }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}