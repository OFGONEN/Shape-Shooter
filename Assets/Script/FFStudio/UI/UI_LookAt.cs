/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class UI_LookAt : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ]
        public SharedReferenceNotifier lookAt_Reference;
        public Vector3 lookAt_Axis;

		Transform lookAt_Transform;
		UnityMessage updateMethod;
#endregion

#region Properties
#endregion

#region Unity API
        void Awake()
        {
			updateMethod = Extensions.EmptyMethod;
		}

		void OnEnable()
		{
			lookAt_Transform = lookAt_Reference.SharedValue as Transform;
			updateMethod = LookAtTarget;
		}

		void OnDisable()
		{
			updateMethod = Extensions.EmptyMethod;
		}

        void Update()
        {
			updateMethod();
		}
#endregion

#region API
		public void ManualDisable()
		{
			OnDisable();
		}
#endregion

#region Implementation
        void LookAtTarget()
        {
			transform.LookAtAxis( lookAt_Transform.position, lookAt_Axis, -1f );
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}