using UnityEngine;

public class CarStartPoint : TrafficNode
{
    
    private Collider2D m_collider;
    
    
    // event listeners //
    
    void Awake()
    {
        m_collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if(m_collider.OverlapPoint(mousePosWorld)){
                OnClick();
            }
        }
    }
    
    void OnClick(){
        Debug.Log("helloooo :)");
    }
    
    // TRAFFIC NODE METHODS //
    
    protected override void OnBuild(Transform parent, TrafficNode[] edges){
    }
}
