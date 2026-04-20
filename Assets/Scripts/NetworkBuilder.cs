using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NetworkBuilder : MonoBehaviour
{
    // public + serialized //
    
    [Header("Road Structure")]
    
    [SerializeField] private Road[] m_roads;
    
    [Header("Settings")]
    
    public GameObject roadSpritePrefab;
    
    
    // private //
    
    private Dictionary<TrafficNode, List<TrafficNode>> m_trafficGraph;
    
    
    // EVENT LISTENERS //
    
    void Start()
    {
        Build();
    }
    
    void OnDrawGizmos(){
        if(Application.isPlaying) return;
        
        Gizmos.color = Color.white;
        
        foreach(Road road in m_roads){
            try {
                Vector3[] positions = road.GetEndpointPositions();
                
                Gizmos.DrawLine(positions[0], positions[1]);
            } catch(Exception){}
        }
    }
    
    
    // PRIVATE METHODS //
    
    void Build(){
        m_trafficGraph = BuildGraph();
        
        // build road sprites
        foreach(Road road in m_roads){
            // build actual road sprite
            road.GetRoadSpriteTransform(out Vector3 roadPosition, out float roadRotation, out float roadLength);
            
            GameObject roadSprite = Instantiate(roadSpritePrefab, roadPosition, Quaternion.identity, transform);
            roadSprite.transform.eulerAngles = new Vector3(0, 0, roadRotation);
            roadSprite.transform.localScale = new Vector3(roadLength, 1, 1);
        }
        
        // build components
        foreach(KeyValuePair<TrafficNode, List<TrafficNode>> kvp in m_trafficGraph){
            TrafficNode src = kvp.Key;
            TrafficNode[] edges = kvp.Value.ToArray();
            
            src.Build(transform, edges);
        }
    }
    
    Dictionary<TrafficNode, List<TrafficNode>> BuildGraph(){
        Dictionary<TrafficNode, List<TrafficNode>> graph = new Dictionary<TrafficNode, List<TrafficNode>>();
        
        foreach(Road road in m_roads){
            TrafficNode e1 = road.endpoint1;
            TrafficNode e2 = road.endpoint2;
            
            if(graph.TryGetValue(e1, out List<TrafficNode> nodeList1)){
                nodeList1.Add(e2);
            } else {
                graph[e1] = new List<TrafficNode>();
                graph[e1].Add(e2);
            }
            
            if(graph.TryGetValue(e2, out List<TrafficNode> nodeList2)){
                nodeList2.Add(e1);
            } else {
                graph[e2] = new List<TrafficNode>();
                graph[e2].Add(e1);
            }
        }
        
        return graph;
    }
}

[System.Serializable]
public class Road {
    public TrafficNode endpoint1;
    public TrafficNode endpoint2;
    
    public Vector3[] GetEndpointPositions(){
        return new Vector3[]{ endpoint1.GetRoadConnectionPoint().transform.position, endpoint2.GetRoadConnectionPoint().transform.position };
    }
    
    public void GetRoadSpriteTransform(out Vector3 position, out float rotation, out float length){
        Vector3[] endpointPositions = GetEndpointPositions();
        Vector3 e1 = endpointPositions[0];
        Vector3 e2 = endpointPositions[1];
        
        Vector3 dif = (e2 - e1);
        
        position = (e1 + e2) / 2;
        
        rotation = Vector3.Angle(dif, Vector3.right);
        length = dif.magnitude;
    }
}