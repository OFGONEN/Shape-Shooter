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
	[ SerializeField ] ShapeData shape_data;

  [ Title( "Components" ) ]
    [ SerializeField ] Transform gfx_transform;
    [ SerializeField ] Transform gfx_corner_transform_left;
    [ SerializeField ] Transform gfx_corner_transform_right;
    [ SerializeField ] Renderer[] _renderer_array;
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;
    
    EdgeColorData edge_color_data;
	RecycledSequence recycledSequence = new RecycledSequence();

	[ ShowInInspector, ReadOnly ] ShapeEdge shape_edge;
	[ ShowInInspector, ReadOnly ] int shape_edge_index;
	[ ShowInInspector, ReadOnly ] List< Edge > edge_neighbor_list = new List< Edge >();

	UnityMessage onMerge;
	Cooldown cooldown = new Cooldown();
#endregion

#region Properties
    public int ColorID => edge_color_data.edge_color_ID;
#endregion

#region Unity API
#endregion

#region API
    public void Shoot( EdgeColorData data, Transform start, Transform end, float sizeStart )
    {
		edge_neighbor_list.Clear();
		onMerge = Merge;

		gameObject.SetActive( true );
		_collider.enabled = true;

		edge_color_data = data;
		UpdateVisual();

		transform.position = start.position;
		transform.rotation = start.rotation;

		var distance = Vector3.Distance( end.position, start.position );
		var duration = distance / GameSettings.Instance.edge_movement_speed;

		gfx_transform.localScale = Vector3.one.SetX( sizeStart );
		OnGFXScaleUpdate();
		// gfx_corner_transform_left.localPosition  = Vector3.right * sizeStart / 2f * shape_data.shape_edge_length * -1f;
		// gfx_corner_transform_right.localPosition = Vector3.right * sizeStart / 2f * shape_data.shape_edge_length;

		var sequence = recycledSequence.Recycle();

		sequence.Append(
			gfx_transform.DOScaleX( 1 + (  distance / shape_data.shape_edge_step_position ) * shape_data.shape_edge_step_size,
				GameSettings.Instance.edge_spawn_duration )
				.SetEase( GameSettings.Instance.edge_spawn_ease )
				.OnUpdate( OnGFXScaleUpdate )
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
				.OnUpdate( OnGFXScaleUpdate )
		);
	}

	public void OnShapeTriggerIdle()
	{
		recycledSequence.Kill();
		_collider.enabled = false;
	}

	public void OnShapeTriggerDynamic()
	{
		recycledSequence.Kill();
		_collider.enabled = false;

		gameObject.SetActive( false );
	}

	public void RepositionOnShape( int edgeIndex, Vector3 localPosition, Vector3 size )
	{
		shape_edge_index = edgeIndex;

		var sequence = recycledSequence.Recycle( OnRepositionComplete );

		sequence.Append( transform.DOLocalMove(
			localPosition,
			GameSettings.Instance.edge_reposition_duration )
			.SetEase( GameSettings.Instance.edge_reposition_ease )
		);

		sequence.Join( gfx_transform.DOScale(
			size,
			GameSettings.Instance.edge_reposition_duration )
			.SetEase( GameSettings.Instance.edge_reposition_ease )
			.OnUpdate( OnGFXScaleUpdate )
		);
	}

	public void StationOnShape( ShapeEdge shapeEdge, int edgeIndex, Vector3 localPosition, Vector3 size )
	{
		shape_edge       = shapeEdge;
		shape_edge_index = edgeIndex;

		gameObject.SetActive( true );
		transform.parent         = shapeEdge.transform;
		transform.localPosition  = localPosition;
		gfx_transform.localScale = size;
		OnGFXScaleUpdate();

		cooldown.Start( Time.fixedDeltaTime, CacheNeighborsAndCheckIfMerge );
	}

	public void AddAnotherEdgeAsNeighbor( Edge edge )
	{
		if( !edge_neighbor_list.Contains( edge) )
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
	void OnRepositionComplete()
	{
		edge_neighbor_list.Clear();
		CacheNeighborsAndCheckIfMerge();
	}

	void CacheNeighborsAndCheckIfMerge()
	{
		var edgeDown  = shape_edge.GetEdgeAtIndex( shape_edge_index - 1 );
		var edgeRight = shape_edge.ShapeEdgeNeighborRight.GetEdgeAtIndex( shape_edge_index );
		var edgeLeft  = shape_edge.ShapeEdgeNeighborLeft.GetEdgeAtIndex( shape_edge_index );

		if( edgeDown != null && edgeDown.ColorID == ColorID )
		{
			AddAnotherEdgeAsNeighbor( edgeDown );
			edgeDown.AddAnotherEdgeAsNeighbor( this );
		}
		if( edgeRight != null && edgeRight.ColorID == ColorID )
		{
			AddAnotherEdgeAsNeighbor( edgeRight );
			edgeRight.AddAnotherEdgeAsNeighbor( this );
		}
		if( edgeLeft != null && edgeLeft.ColorID == ColorID )
		{
			AddAnotherEdgeAsNeighbor( edgeLeft );
			edgeLeft.AddAnotherEdgeAsNeighbor( this );
		}

		var merge = CheckIfMerge();

		if( merge || ( edgeDown != null && edgeDown.CheckIfMerge() ) || ( edgeRight != null && edgeRight.CheckIfMerge() ) || ( edgeLeft != null && edgeLeft.CheckIfMerge() ) )
		{
			OnMerge();
			event_shape_merged.Raise();
		}
	}

	void OnGFXScaleUpdate()
	{
		var scaleX                                   = gfx_transform.localScale.x;
		    gfx_corner_transform_left.localPosition  = Vector3.right * scaleX / 2f * shape_data.shape_edge_length * -1f;
		    gfx_corner_transform_right.localPosition = Vector3.right * scaleX / 2f * shape_data.shape_edge_length;
	}

	void ReturnToPool()
	{
		pool_edge.ReturnEntity( this );
		gfx_transform.localScale = Vector3.one;
		OnGFXScaleUpdate();
	}

	void Merge()
	{
		onMerge = Extensions.EmptyMethod;
		cooldown.Kill();

		shape_edge.RemoveEdgeAtIndex( shape_edge_index );

		for( var i = 0; i < edge_neighbor_list.Count; i++ )
			edge_neighbor_list[ i ].OnMerge();

		ReturnToPool();
		//todo spawn particle
	}

    void UpdateVisual()
    {
		for( var i = 0; i < _renderer_array.Length; i++ )
			_renderer_array[ i ].sharedMaterial = edge_color_data.edge_material;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}