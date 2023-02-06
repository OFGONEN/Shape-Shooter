/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public static class Vector2IntExtensions
    {
		public static int GetUniqueHashCode_PositiveIntegers( this Vector2Int v2 )
		{
			if( Mathf.Max( v2.x, v2.y ) == v2.x )
				return v2.x * v2.x + v2.x + v2.y;
			else
				return v2.x + v2.y * v2.y;
		}
	}
}