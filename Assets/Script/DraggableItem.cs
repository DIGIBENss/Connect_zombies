
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 initialPosition;
    public bool IsDragging = false;
    private Vector3 _offset;
    private Camera _myCamera;
    private float distanceToCamera;
    private Collider myCollider;
    private Check check;
    [SerializeField] private bool isDraggable = true;
    [SerializeField] private PrefabsZombieUpdate _prefabsZombie;
    [SerializeField] private Zombiee _zombiee;
    
    private void Awake()
    { 
        _myCamera = Camera.main;
    }
    private void Start()
    {
        _zombiee = GetComponent<Zombiee>();
        _prefabsZombie = FindObjectOfType<PrefabsZombieUpdate>();
        initialPosition = transform.position;
        myCollider = GetComponent<Collider>();   
        check = GetComponent<Check>();
        distanceToCamera = Mathf.Abs(transform.position.y - _myCamera.transform.position.y);
        
    }
    private void OnMouseDown()
    {
        if (isDraggable)
        {
            myCollider.isTrigger = false;
            IsDragging = true;   
            _offset = transform.position - GetMouseWorldPos();
            _prefabsZombie.CheckZombiesDragging(IsDragging, _zombiee.Type);
        }    
            
    }
    private void OnMouseUp()
    {
        if (isDraggable)
        {
            IsDragging = false;
            myCollider.isTrigger = true;
            transform.position = initialPosition;
            _prefabsZombie.CheckZombiesDragging(IsDragging, _zombiee.Type);
        }
            

    }
    private void OnTriggerEnter(Collider other)
    {
        _prefabsZombie.CheckZombiesDragging(false, -1);
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distanceToCamera;
        return _myCamera.ScreenToWorldPoint(mousePos);
    }
    private void Update()
    {
       
            if (IsDragging && isDraggable)
            {
                Vector3 targetPos = GetMouseWorldPos() + _offset;
                transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            }
        
    }
}
