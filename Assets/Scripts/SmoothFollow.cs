using UnityEngine;
using System.Collections;
using Prime31;


public class SmoothFollow : MonoBehaviour
{
    public bool isTwoPlayer = false;
    public Transform player1, player2;
    public float smoothDampTime = 0.2f;
    [HideInInspector]
    public new Transform transform;
    public Vector3 cameraOffset;
    public float minCameraSize = 5, maxCameraSize = 10;
    public bool useFixedUpdate = false;

    private Camera _camera;
    private float _smoothSizeDampVelocity;
    private Vector3 _smoothDampVelocity;


    void Awake()
    {
        transform = gameObject.transform;
        _camera = GetComponent<Camera>();
    }


    void LateUpdate()
    {
        if (!useFixedUpdate)
            updateCameraPosition();
    }


    void FixedUpdate()
    {
        if (useFixedUpdate)
            updateCameraPosition();
    }


    void updateCameraPosition()
    {
        if(!isTwoPlayer)
        {
            _camera.orthographicSize = minCameraSize;
            transform.position = Vector3.SmoothDamp(transform.position, player1.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
        }
        else
        {
            float disBetweenPlayers = (player1.position - player2.position).magnitude;
            float newCameraSize = Mathf.Max(minCameraSize, Mathf.Min(maxCameraSize, disBetweenPlayers ));
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, newCameraSize, ref _smoothSizeDampVelocity, smoothDampTime);

            Vector3 midpointPosition = (player1.position - player2.position) * 0.5f + player2.position;
            transform.position = Vector3.SmoothDamp(transform.position, midpointPosition - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
        }
    }

}
