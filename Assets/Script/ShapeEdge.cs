/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class ShapeEdge : MonoBehaviour
{
#region Fields
  [ Title( "Components" ) ]
    [ SerializeField ] BoxCollider _boxCollider;

    TriggerMessage onTrigger;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		onTrigger = TriggerIdle;
	}
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
		onTrigger( collider );
	}

    public void OnTurnStart()
    {
		onTrigger = TriggerDynamic;
	}

    public void OnTurnStop()
    {
		onTrigger = TriggerIdle;
    }
#endregion

#region Implementation
    void TriggerIdle( Collider collider )
    {

    }

    void TriggerDynamic( Collider collider )
    {

    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}