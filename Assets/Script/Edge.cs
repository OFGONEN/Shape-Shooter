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
	[ SerializeField ] GameEvent event_shape_merged;

  [ Title( "Components" ) ]
    [ SerializeField ] Transform gfx_transform;
    [ SerializeField ] ColorSetter _colorSetter;
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;
    
    EdgeColorData edge_color_data;
	RecycledSequence recycledSequence = new RecycledSequence();

	[ ShowInInspector, ReadOnly ] ShapeEdge shape_edge;
	[ ShowInInspector, ReadOnly ] int shape_edge_index;
	[ ShowInInspector, ReadOnly ] List< Edge > edge_neighbor_list = new List< Edge >();

	UnityMessage onMerge;
#endregion

#region Properties
    public int ColorID => edge_color_data.edge_color_ID;
#endregion

#region Unity API
#endregion

#region API
    public void Shoot( EdgeColorData data, Transform start, Transform end, float sizeStart, float sizeEnd )
    {
		edge_neighbor_list.Clear();
		onMerge = Merge;

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

	public void RepositionOnShape( int edgeIndex, Vector3 localPosition, Vector3 size )
	{
		shape_edge_index = edgeIndex;

		var sequence = recycledSequence.Recycle();

		sequence.Append( transform.DOLocalMove(
			localPosition,
			GameSettings.Instance.edge_reposition_duration )
			.SetEase( GameSettings.Instance.edge_reposition_ease )
		);

		sequence.Join( transform.DOScale(
			size,
			GameSettings.Instance.edge_reposition_duration )
			.SetEase( GameSettings.Instance.edge_reposition_ease )
		);
	}

	public void StationOnShape( ShapeEdge shapeEdge, int edgeIndex, Vector3 localPosition, Vector3 size )
	{
		shape_edge       = shapeEdge;
		shape_edge_index = edgeIndex;

		gameObject.SetActive( true );
		transform.parent        = shapeEdge.transform;
		transform.localPosition = localPosition;
		transform.localScale    = size;

		var edgeDown  = shape_edge.GetEdgeAtIndex( edgeIndex - 1 );
		var edgeRight = shape_edge.ShapeEdgeNeighborRight.GetEdgeAtIndex( edgeIndex );
		var edgeLeft  = shape_edge.ShapeEdgeNeighborLeft.GetEdgeAtIndex( edgeIndex );

		if( edgeDown != null && edgeDown.ColorID == ColorID )
		{
			edge_neighbor_list.Add( edgeDown );
			edgeDown.AddAnotherEdgeAsNeighbor( this );
		}
		if( edgeRight != null && edgeRight.ColorID == ColorID )
		{
			edge_neighbor_list.Add( edgeRight );
			edgeRight.AddAnotherEdgeAsNeighbor( this );
		}
		if( edgeLeft != null && edgeLeft.ColorID == ColorID )
		{
			edge_neighbor_list.Add( edgeLeft );
			edgeLeft.AddAnotherEdgeAsNeighbor( this );
		}

		var merge = CheckIfMerge();

		if( merge || ( edgeDown != null && edgeDown.CheckIfMerge() ) || ( edgeRight != null && edgeRight.CheckIfMerge() ) || ( edgeLeft != null && edgeLeft.CheckIfMerge() ) )
		{
			OnMerge();
			event_shape_merged.Raise();
		}
	}

	public void AddAnotherEdgeAsNeighbor( Edge edge )
	{
		edge_neighbor_list.Add( edge );
	}

	public void OnLevelComplete()
	{
		_collider.enabled = false;
		recycledSequence.Kill();
	}

	public void OnLevelUnloadStart()
	{
		ReturnToPool();
	}

	public bool CheckIfMerge()
	{
		return edge_neighbor_list.Count >= GameSettings.Instance.edge_merge_count;
	}

	public void OnMerge()
	{
		onMerge();
	}
#endregion

#region Implementation
	void ReturnToPool()
	{
		pool_edge.ReturnEntity( this );
		transform.localScale     = Vector3.one;
		gfx_transform.localScale = Vector3.one;
	}

	void Merge()
	{
		onMerge = Extensions.EmptyMethod;

		shape_edge.RemoveEdgeAtIndex( shape_edge_index );

		for( var i = 0; i < edge_neighbor_list.Count; i++ )
			edge_neighbor_list[ i ].OnMerge();

		ReturnToPool();
		//todo spawn particle
	}

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