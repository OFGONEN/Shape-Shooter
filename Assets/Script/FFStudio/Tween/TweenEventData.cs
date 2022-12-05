/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
    [ System.Serializable ]
	public abstract class TweenEventData
    {
		public UnityEvent onThresholdReached;
		[ HideInEditorMode ] public bool isConsumed;
        
		public abstract void InvokeEventIfThresholdIsPassed( Tween tween, Ease ease );
    }

	[ System.Serializable ]
	public class TweenEventData_Duration : TweenEventData
	{
        [ SuffixLabel( "seconds" ), Min( 0 ) ] public float threshold;
        
		public override void InvokeEventIfThresholdIsPassed( Tween tween, Ease ease )
        {
            if( tween.Elapsed() > threshold )
			{
                onThresholdReached.Invoke();
                isConsumed = true;
            }
		}
	}
	
	[ System.Serializable ]
	public class TweenEventData_TweenPercentage : TweenEventData
	{
		[ MappedFloat ] public float threshold;
        
		public override void InvokeEventIfThresholdIsPassed( Tween tween, Ease ease )
        {
            if( tween.ElapsedPercentage() > threshold )
			{
                onThresholdReached.Invoke();
                isConsumed = true;
            }
		}
	}
    
    [ System.Serializable ]
	public class TweenEventData_ValuePercentage : TweenEventData
	{
		[ MappedFloat ] public float threshold;
        
		public override void InvokeEventIfThresholdIsPassed( Tween tween, Ease ease )
        {
            if( DOVirtual.EasedValue( 0.0f, 1.0f, tween.ElapsedPercentage(), ease ) > threshold )
			{
                onThresholdReached.Invoke();
                isConsumed = true;
            }
		}
	}
}