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

	[ System.NonSerialized ] public List< Edge > edge_list = new List< Edge >();
	List< Edge > edge_list_temp = new List< Edge >();

    TriggerMessage onTrigger;
#endregion

#region Properties
	public ShapeEdge ShapeEdgeNeighborLeft  => shape_edge_neighbor_left;
	public ShapeEdge ShapeEdgeNeighborRight => shape_edge_neighbor_right;
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
		CollectTriggeredEdges();
	}
#endregion

#region Implementation
    void TriggerIdle( Collider collider )
    {
		var edge = collider.GetComponent< ComponentHost >().HostComponent as Edge;
		edge.OnShapeTriggerIdle();

		edge_list.Add( edge );

		if( edge_list.Count > shape_data.shape_edge_count )
			event_level_failed.Raise();
		else
			StationEdge( edge );
	}

    void TriggerDynamic( Collider collider )
    {
		var edge = collider.GetComponent< ComponentHost >().HostComponent as Edge;
		edge.OnShapeTriggerDynamic();

		edge_list_temp.Add( edge );
	}

	void StationEdge( Edge edge )
	{
		var edgeIndex = edge_list.Count;

		edge.StationOnShape( transform, ( edgeIndex + 1 ) * shape_data.shape_edge_step_position * Vector3.forward, Vector3.one.OffsetX( edgeIndex * shape_data.shape_edge_step_size ) );

		_boxCollider.transform.localScale = Vector3.one.SetZ( ( edge_list.Count + 1 ) * shape_data.shape_edge_step_size );
	}

	void CollectTriggeredEdges()
	{
		edge_list_temp.Sort( ComparteEdges );

		for( var i = 0; i < edge_list_temp.Count; i++ )
		{
			var edge = edge_list_temp[ i ];

			FFLogger.Log( "Edge:", edge_list_temp[ i ] );

			edge_list.Add( edge );
			StationEdge( edge );
		}

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
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}