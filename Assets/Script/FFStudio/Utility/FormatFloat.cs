/* Created by and for usage of FF Studios (2023). */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FFStudio
{
	public class FormatFloat : MonoBehaviour
	{
#region Fields
		[ SerializeField ] UnityEvent< string > onFormatFloatEvent;
		[ SerializeField ] string suffix;
		[ SerializeField ] string prefix;
		[ SerializeField ] string format;
#endregion

#region Unity API
#endregion

#region API
		public void UpdateTextRenderer( float value )
		{
			onFormatFloatEvent.Invoke( suffix + ExtensionMethods.FormatNumber( value ) + prefix );
		}

		public void UpdateTextRendererFormat( float value )
		{
			onFormatFloatEvent.Invoke( suffix + value.ToString( format ) + prefix );
		}
#endregion

#region Implementation
#endregion
	}
}