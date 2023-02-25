/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using DG.Tweening;

public class Shape : MonoBehaviour
{
#region Fields
  [ Title( "Shared Data" ) ]
    [ SerializeField ] ShapeData shape_data;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody shape_turnable_tween_rigidbody;
    [ SerializeField ] ShapeEdge[] shape_edge_array;

    RecycledTween recycledTween = new RecycledTween();

    UnityMessage onDoTurn;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		EmptyDelegates();
	}
#endregion

#region API
    public void DoTurn()
    {
		onDoTurn();
	}

    public void OnLevelStarted()
    {
		onDoTurn = Turn;
	}

	public void OnLevelComplete()
	{
		onDoTurn = Extensions.EmptyMethod;
		recycledTween.Kill();
	}
#endregion

#region Implementation
    void Turn()
    {
		var currentRotation = shape_turnable_tween_rigidbody.transform.localEulerAngles.z;
		var targetRotation  = currentRotation + shape_data.shape_rotation_angle;

		for( var i = 0; i < shape_edge_array.Length; i++ )
			shape_edge_array[ i ].OnTurnStart();

		// recycledTween.Recycle( DOTween.To( GetTurnableRigidbodyRotation, SetTurnableRigidbodyRotation,
		// 	targetRotation,
		// 	shape_data.shape_rotation_duration )
		// 	.SetEase( shape_data.shape_rotation_ease ),
		// 	OnTurnComplete 
		// );

		recycledTween.Recycle( shape_turnable_tween_rigidbody.DORotate( Vector3.zero.SetZ( targetRotation ), 
			shape_data.shape_rotation_duration )
			.SetEase( shape_data.shape_rotation_ease )
			.SetUpdate( UpdateType.Fixed ),
			OnTurnComplete 
		);

		EmptyDelegates();
    }

    void OnTurnComplete()
    {
		for( var i = 0; i < shape_edge_array.Length; i++ )
			shape_edge_array[ i ].OnTurnStop();

		onDoTurn = Turn;
	}

    void EmptyDelegates()
    {
		onDoTurn = Extensions.EmptyMethod;
	}

	float GetTurnableRigidbodyRotation()
	{
		return shape_turnable_tween_rigidbody.rotation.eulerAngles.z;
	}

	void SetTurnableRigidbodyRotation( float rotation )
	{
		shape_turnable_tween_rigidbody.MoveRotation( Quaternion.Euler( 0, 0, rotation ) );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}