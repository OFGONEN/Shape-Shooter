/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class DoShakePosition : MonoBehaviour
{
#region Fields
    RecycledTween recycledTween = new RecycledTween();

    Vector3 position_start;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        position_start = transform.position;        
    }
#endregion

#region API
    [ Button() ]
    public void Play()
    {
        if( !recycledTween.IsPlaying )
        {
		    transform.position = position_start;
		    recycledTween.Recycle( GameSettings.Instance.score_current_shakePosition.CreateTween( transform ) );
        }
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
