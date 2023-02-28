/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "shared_edge_color_data", menuName = "FF/Game/Edge Color Data" ) ]
public class EdgeColorData : ScriptableObject
{
#region Fields
    public int edge_color_ID;
    public Color edge_color;
    public Material edge_material;
    public string edge_pfx_alias;
    public Color edge_color_text;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
