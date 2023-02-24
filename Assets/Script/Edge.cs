/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Edge : MonoBehaviour
{
#region Fields
    [ SerializeField ] ColorSetter _colorSetter;
    
    EdgeColorData edge_color_data;
#endregion

#region Properties
    public int ColorID => edge_color_data.edge_color_ID;
#endregion

#region Unity API
#endregion

#region API
    public void UpdateVisual( EdgeColorData data )
    {
		edge_color_data = data;

		_colorSetter.SetColor( data.edge_color );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
