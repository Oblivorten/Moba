using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 15, -10);
    [SerializeField] private float _followSpeed = 5f;

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 desiredPosition = _target.position + _offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            _followSpeed * Time.deltaTime);
    }

}