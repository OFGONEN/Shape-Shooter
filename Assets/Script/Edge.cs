/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Edge : MonoBehaviour
{
#region Fields
  [ Title( "Components" ) ]
    [ SerializeField ] Transform gfx_transform;
    [ SerializeField ] ColorSetter _colorSetter;
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;
    
    EdgeColorData edge_color_data;

	RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
    public int ColorID => edge_color_data.edge_color_ID;
#endregion

#region Unity API
#endregion

#region API
    public void Shoot( EdgeColorData data, Transform start, Transform end )
    {
		gameObject.SetActive( true );
		_collider.enabled = true;

		edge_color_data = data;
		UpdateVisual();

		transform.position = start.position;
		transform.rotation = start.rotation;

		gfx_transform.localScale = Vector3.one.SetX( GameSettings.Instance.edge_spawn_scale_start );

		var sequence = recycledSequence.Recycle();

		sequence.Append(
			gfx_transform.DOScaleX( GameSettings.Instance.edge_spawn_scale_end,
				GameSettings.Instance.edge_spawn_scale_end )
				.SetEase( GameSettings.Instance.edge_spawn_ease )
		);

		sequence.Append( _rigidbody.DOMove(
			end.position,
			GameSettings.Instance.edge_movement_speed )
			.SetEase( Ease.Linear )
			.SetSpeedBased()
		);
	}

	public void OnShapeTriggerIdle( Transform parent )
	{
		recycledSequence.Kill();
		_collider.enabled = false;

		transform.parent = parent;
	}

	public void OnShapeTriggerDynamic( Transform parent )
	{
		recycledSequence.Kill();
		_collider.enabled = false;

		gameObject.SetActive( false );
		transform.parent = parent;
	}

	public void StationOnShape( Vector3 localPosition )
	{
		transform.localPosition = localPosition;
	}
#endregion

#region Implementation
    void UpdateVisual()
    {
		_colorSetter.SetColor( edge_color_data.edge_color );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}