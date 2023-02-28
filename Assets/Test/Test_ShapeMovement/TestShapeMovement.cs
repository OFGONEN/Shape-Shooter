/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using FFStudio;

public class TestShapeMovement : MonoBehaviour
{
#region Fields
	public Transform movement_start;
	public Transform movement_end;
	public Transform gfx_transform;
    public Rigidbody movement_body;

	public float gfx_size_start;
	public float gfx_size_duration;
	public Ease gfx_size_ease;
	public float movement_size_start_target;
	public float movement_speed;

    UnityMessage onFixedUpdate;

    [ ShowInInspector, ReadOnly ] float movement_distance;
    [ ShowInInspector, ReadOnly ] float movement_distance_current;
    [ ShowInInspector, ReadOnly ] float movement_progress;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		onFixedUpdate     = Extensions.EmptyMethod;
		movement_distance = movement_distance_current = Vector3.Distance( movement_start.position, movement_end.position );
	}

    void FixedUpdate()
    {
		onFixedUpdate();
	}
#endregion

#region API
    [ Button() ]
    public void StartMovement()
    {
		transform.position = movement_start.position;
		transform.rotation = movement_start.rotation;

		transform.localScale     = Vector3.one * movement_size_start_target;
		gfx_transform.localScale = Vector3.one.SetX( gfx_size_start );

		recycledTween.Recycle(
			gfx_transform.DOScaleX( 1,
			gfx_size_duration / movement_speed )
            .SetEase( gfx_size_ease ) );

		onFixedUpdate = Movement;
	}
#endregion

#region Implementation
    void Movement()
    {
		var position = Vector3.MoveTowards( movement_body.position, movement_end.position, Time.fixedDeltaTime * movement_speed );
		movement_body.MovePosition( position );

		movement_distance_current = Vector3.Distance( position, movement_end.position );
		movement_progress         = Mathf.InverseLerp( movement_distance, 0, movement_distance_current );

		transform.localScale = Mathf.Lerp( movement_size_start_target, 1f, movement_progress ) * Vector3.one;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}