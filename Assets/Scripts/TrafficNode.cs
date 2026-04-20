using UnityEngine;

public abstract class TrafficNode : MonoBehaviour
{
    // serialized //
    
    [SerializeField] private Transform m_roadConnectionPoint;
    
    
    // private //
    
    // private bool m_isBuilt = false;
    
    
    // getters + setters//
    
    public Transform GetRoadConnectionPoint(){
        if(!m_roadConnectionPoint) return transform;
        
        return m_roadConnectionPoint;
    }
    
    // event listeners //
    
    public void OnDrawGizmos(){
        if(Application.isPlaying) return;
        
        Gizmos.color = Color.white;
        
        Gizmos.DrawSphere(GetRoadConnectionPoint().position, 0.25f);
    }
    
    // methods //
    
    public void Build(Transform parent, TrafficNode[] edges){
        // if(m_isBuilt) return;
        
        OnBuild(parent, edges);
        
        // m_isBuilt = true;
    }
    
    protected abstract void OnBuild(Transform parent, TrafficNode[] edges);
}
