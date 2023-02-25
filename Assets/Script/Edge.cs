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
  [ Title( "Shared" ) ]
	[ SerializeField ] PoolEdge pool_edge;

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
    public void Shoot( EdgeColorData data, Transform start, Transform end, float sizeStart, float sizeEnd )
    {
		gameObject.SetActive( true );
		_collider.enabled = true;

		edge_color_data = data;
		UpdateVisual();

		transform.position = start.position;
		transform.rotation = start.rotation;

		var duration = Vector3.Distance( end.position, start.position ) / GameSettings.Instance.edge_movement_speed;

		gfx_transform.localScale = Vector3.one.SetX( sizeStart );

		var sequence = recycledSequence.Recycle();

		sequence.Append(
			gfx_transform.DOScaleX( sizeEnd,
				GameSettings.Instance.edge_spawn_duration )
				.SetEase( GameSettings.Instance.edge_spawn_ease )
		);

		sequence.Append( _rigidbody.DOMove(
			end.position,
			duration )
			.SetEase( Ease.Linear )
		);

		sequence.Join(
			gfx_transform.DOScaleX( 1,
				duration )
				.SetEase( Ease.Linear )
		);
	}

	public void OnShapeTriggerIdle()
	{
		recycledSequence.Kill();
		gfx_transform.localScale = Vector3.one;
		_collider.enabled        = false;
	}

	public void OnShapeTriggerDynamic()
	{
		recycledSequence.Kill();
		gfx_transform.localScale = Vector3.one;
		_collider.enabled        = false;

		gameObject.SetActive( false );
	}

	public void StationOnShape( Transform parent, Vector3 localPosition, Vector3 size )
	{
		gameObject.SetActive( true );
		transform.parent        = parent;
		transform.localPosition = localPosition;
		transform.localScale    = size;
	}

	public void OnLevelComplete()
	{
		_collider.enabled = false;
		recycledSequence.Kill();
	}

	public void OnLevelUnloadStart()
	{
		pool_edge.ReturnEntity( this );
		transform.localScale     = Vector3.one;
		gfx_transform.localScale = Vector3.one;
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