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
    public ShapeData shape_data;

  [ Title( "Components" ) ]
    public Transform shape_turnable_instant;
    public Transform shape_turnable_tween;

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
#endregion

#region Implementation
    void Turn()
    {
		var rotationValue = Vector3.zero.SetZ( shape_data.shape_rotation_angle );

		shape_turnable_instant.localEulerAngles += rotationValue;

		recycledTween.Recycle( shape_turnable_tween.DOLocalRotate(
			rotationValue,
			shape_data.shape_rotation_duration )
			.SetEase( shape_data.shape_rotation_ease )
			.SetRelative(),
            OnTurnComplete
		);

		EmptyDelegates();
    }

    void OnTurnComplete()
    {
		onDoTurn = Turn;
	}

    void EmptyDelegates()
    {
		onDoTurn = Extensions.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}