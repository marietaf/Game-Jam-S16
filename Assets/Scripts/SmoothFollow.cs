using UnityEngine;
using System.Collections;
using Prime31;


public class SmoothFollow : MonoBehaviour
{
    public bool isTwoPlayer = false;
    public GameObject player1, player2;
    public float smoothDampTime = 0.2f;
    [HideInInspector]
    public new Transform transform;
    public Vector3 cameraOffset;
    public float minCameraSize = 5, maxCameraSize = 10;
    public bool useFixedUpdate = false;

    private Camera _camera;
    private float _smoothSizeDampVelocity;
    private float _sizeSmoothDampVelocity;
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
            if(player1.GetComponent<PlayerClone>() != null && player1.GetComponent<PlayerClone>().isCloneActive)
            {
                // Single player & clone not active
                Vector3 player1Position = player1.transform.position;
                Vector3 clonePosition = player1.GetComponent<PlayerClone>().clone.transform.position;

                float disBetweenPlayers = (player1Position - clonePosition).magnitude;
                float newCameraSize = Mathf.Max(minCameraSize, Mathf.Min(maxCameraSize, disBetweenPlayers));
                _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, newCameraSize, ref _smoothSizeDampVelocity, smoothDampTime);

                Vector3 midpointPosition = (player1Position - clonePosition) * 0.5f + clonePosition;
                transform.position = Vector3.SmoothDamp(transform.position, midpointPosition - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            }
            else
            {
                // Single player & clone not active
                _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, minCameraSize, ref _sizeSmoothDampVelocity, smoothDampTime);
                transform.position = Vector3.SmoothDamp(transform.position, player1.transform.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            }

            
        }
        else
        {
            // Multiplayer
            Vector3 player1Position = player1.transform.position;
            Vector3 player2Position = player2.transform.position;

            float disBetweenPlayers = (player1Position - player2Position).magnitude;
            float newCameraSize = Mathf.Max(minCameraSize, Mathf.Min(maxCameraSize, disBetweenPlayers ));
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, newCameraSize, ref _smoothSizeDampVelocity, smoothDampTime);

            Vector3 midpointPosition = (player1Position - player2Position) * 0.5f + player2Position;
            transform.position = Vector3.SmoothDamp(transform.position, midpointPosition - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
        }
    }

}
