using UnityEngine;

public class Follow_camera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform; // Camera reference
    [SerializeField] private float _speed = 1f;          // Speed of movement
    [SerializeField] private float _distance = 1f;       // Distance to maintain from the camera
    [SerializeField] private bool _isFixed = false;      // Flag to fix the item's position and rotation
    
    private void Update()
    {
        if (_isFixed)
        {
            // Fix the object in front of the camera without any smoothing
            Vector3 fixedPosition = _cameraTransform.position + (_cameraTransform.forward * _distance);
            Quaternion fixedRotation = Quaternion.LookRotation(_cameraTransform.forward);

            transform.SetPositionAndRotation(fixedPosition, fixedRotation);
            return;
        }

        // Calculate the direction (using camera's forward vector)
        Vector3 direction = _cameraTransform.forward.normalized;

        // Target position and rotation based on camera's position and orientation
        Vector3 targetPosition = _cameraTransform.position + (direction * _distance);
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly interpolate position and rotation
        float t = Time.deltaTime * _speed;
        Vector3 position = Vector3.Lerp(transform.position, targetPosition, t);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);

        // Apply the new position and rotation
        transform.SetPositionAndRotation(position, rotation);
    }
}