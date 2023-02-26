/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_shape_data", menuName = "FF/Game/Shape Data" ) ]
public class ShapeData : ScriptableObject
{
#region Fields
  [ Title( "Shape" ) ]
    public float shape_rotation_angle;
    public float shape_rotation_duration;
    public Ease shape_rotation_ease;
	public int shape_edge_count;

  [ Title( "Edge" ) ]
    public float shape_edge_length;
    public float shape_edge_spawn_start;
    public float shape_edge_spawn_end;
    public float shape_edge_step_size;
    public float shape_edge_step_position;
    public int shape_edge_depth_count;
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