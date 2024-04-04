
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 
    public float rotationSpeed = 5.0f;

    private void Update()
    {
        if (target == null)
        {
            ChooseNewTarget();
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        smoothedPosition.y = transform.position.y;
        transform.position = smoothedPosition;
        Quaternion targetRotation = Quaternion.Euler(45f, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void ChooseNewTarget()
    {
        
        for (int i = 0; i < _gameManager._enemyAi.Length; i++)
        { 
            if (_gameManager._enemyAi[i] != null)
            {
                target = _gameManager._enemyAi[i].transform;
                break;
            }
        }

    }
}