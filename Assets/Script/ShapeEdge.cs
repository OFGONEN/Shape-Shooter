/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class ShapeEdge : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
	[ SerializeField ] ShapeData shape_data;
	[ SerializeField ] GameEvent event_level_failed;

  [ Title( "Components" ) ]
    [ SerializeField ] BoxCollider _boxCollider;
    [ SerializeField ] ShapeEdge shape_edge_neighbor_left;
    [ SerializeField ] ShapeEdge shape_edge_neighbor_right;

	[ System.NonSerialized, ShowInInspector, ReadOnly ] public List< Edge > edge_list = new List< Edge >();
	List< Edge > edge_list_temp = new List< Edge >();

	UnityMessage onEdgesMerged;
    TriggerMessage onTrigger;
	Cooldown cooldown = new Cooldown();
#endregion

#region Properties
	public ShapeEdge ShapeEdgeNeighborLeft  => shape_edge_neighbor_left;
	public ShapeEdge ShapeEdgeNeighborRight => shape_edge_neighbor_right;
#endregion

#region Unity API
    private void Awake()
    {
		onTrigger     = TriggerIdle;
		onEdgesMerged = Extensions.EmptyMethod;
	}
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
		onTrigger( collider );
	}

    public void OnTurnStart()
    {
		// onTrigger = TriggerDynamic;
		_boxCollider.enabled = false;
	}

    public void OnTurnStop()
    {
		// onTrigger = TriggerIdle;

		cooldown.Start( Time.fixedDeltaTime, () => _boxCollider.enabled = true );
		// CollectTriggeredEdges();
	}

	public Edge GetEdgeAtIndex( int index )
	{
		if( index < 0 || index >= edge_list.Count )
			return null;
		else
			return edge_list[ index ];
	}

	public void OnEdgesMerged()
	{
		onEdgesMerged();
	}

	public void RemoveEdgeAtIndex( int index )
	{
		onEdgesMerged = MergeEdges;
		edge_list[ index ] = null;
	}
#endregion

#region Implementation
	void MergeEdges()
	{
		onEdgesMerged = Extensions.EmptyMethod;

		//todo move edges to correct positions
		for( var i = edge_list.Count - 1; i >= 0; i-- )
			if( edge_list[ i ] == null )
				edge_list.RemoveAt( i );
			
		for( var i = 0; i < edge_list.Count; i++ )
			RepositionEdge( edge_list[ i ], i );

		_boxCollider.transform.localScale = Vector3.one.SetZ( ( edge_list.Count + 1 ) * shape_data.shape_edge_step_position );
	}

    void TriggerIdle( Collider collider )
    {
		var edge = collider.GetComponent< ComponentHost >().HostComponent as Edge;
		edge.OnShapeTriggerIdle();

		edge_list.Add( edge );
		StationEdge( edge );

		CheckIfLevelFailed();
	}

    void TriggerDynamic( Collider collider )
    {
		var edge = collider.GetComponent< ComponentHost >().HostComponent as Edge;
		edge.OnShapeTriggerDynamic();

		edge_list_temp.Add( edge );
	}

	void StationEdge( Edge edge )
	{
		var edgeIndex = edge_list.Count - 1;

		edge.StationOnShape( this, edgeIndex, ( edgeIndex + 2 ) * shape_data.shape_edge_step_position * Vector3.forward, Vector3.one.OffsetX( ( edgeIndex + 1 ) * shape_data.shape_edge_step_size ) );

		_boxCollider.transform.localScale = Vector3.one.SetZ( ( edge_list.Count + 1 ) * shape_data.shape_edge_step_position );
	}

	void RepositionEdge( Edge edge, int index )
	{
		edge.RepositionOnShape( index,
			( index + 2 ) * shape_data.shape_edge_step_position * Vector3.forward,
			Vector3.one.OffsetX( ( index + 1 ) * shape_data.shape_edge_step_size ) );
	}

	void CollectTriggeredEdges()
	{
		edge_list_temp.Sort( ComparteEdges );

		for( var i = 0; i < edge_list_temp.Count; i++ )
		{
			var edge = edge_list_temp[ i ];

			edge_list.Add( edge );
			StationEdge( edge );
		}

		CheckIfLevelFailed();

		edge_list_temp.Clear();
	}

	int ComparteEdges( Edge left, Edge right )
	{
		var position      = transform.position;
		var leftDistance  = Vector3.Distance( left.transform.position, position );
		var rightDistance = Vector3.Distance( right.transform.position, position );

		if( Mathf.Approximately( leftDistance, rightDistance ) )
			return 0;
		else if( leftDistance > rightDistance )
			return 1;
		else
			return -1;
	}

	void CheckIfLevelFailed()
	{
		if( edge_list.Count > shape_data.shape_edge_depth_count )
			event_level_failed.Raise();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}