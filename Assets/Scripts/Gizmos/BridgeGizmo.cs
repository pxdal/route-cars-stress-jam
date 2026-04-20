using UnityEngine;

public class BridgeGizmo : MonoBehaviour
{
    public Color color = Color.white;
    
    void OnDrawGizmos(){
        if(Application.isPlaying) return;
        
        Gizmos.DrawIcon(transform.position, "bridge.tiff", true, color);
    }
}
