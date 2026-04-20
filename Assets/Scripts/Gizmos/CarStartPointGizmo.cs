using UnityEngine;

public class CarStartPointGizmo : MonoBehaviour
{
    
    public Color color = Color.white;
    
    void OnDrawGizmos(){
        if(Application.isPlaying) return;
        
        Gizmos.DrawIcon(transform.position, "triangle.tiff", true, color);
    }
}
