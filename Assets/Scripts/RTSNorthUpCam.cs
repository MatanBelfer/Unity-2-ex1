using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class RTSNorthUpCam : MonoBehaviour
{
    [Header("Cinemachine")] public CinemachineCamera vcam;

    [Header("Input Actions (New Input System)")]
    public InputActionReference move; // Vector2 (WASD)

    public InputActionReference zoom; // Axis (Mouse scroll y)

    public InputActionReference reCenter; // space 

    [Header("Tuning")] public float moveSpeed = 20f; // units/second
    public float zoomSpeed = 8f; // feel-tuning
    public float minDist = 10f;
    public float maxDist = 60f;

    CinemachineFollow follow;

    public enum AgentType
    {
        Agent1,
        Agent2
    }

    public AgentType selectedAgentType;
    [SerializeField] private GameObject cameraTargetAgent1;
    [SerializeField] private GameObject cameraTargetAgent2;
    private GameObject selectedAgentTarget;


    void Awake()
    {
        if (vcam != null)
            follow = vcam.GetComponent<CinemachineFollow>();
        else
            Debug.LogError("RTSNorthUpCam: No Cinemachine Camera found");

        
    }

    void OnEnable()
    {
        move?.action.Enable();
        zoom?.action.Enable();
        reCenter?.action.Enable();
    }

    void OnDisable()
    {
        move?.action.Disable();
        zoom?.action.Disable();
        reCenter?.action.Enable();
    }

    void Update()
    {
        selectedAgentTarget = selectedAgentType == AgentType.Agent1 ? cameraTargetAgent1 : cameraTargetAgent2;
        if (reCenter != null && reCenter.action.triggered)
        {
            transform.position = selectedAgentTarget.transform.position;
        }

        // Pan in WORLD space (north up)
        Vector2 m = move != null ? move.action.ReadValue<Vector2>() : Vector2.zero;
        Vector3 delta = new Vector3(m.x, 0f, m.y) * (moveSpeed * Time.deltaTime);
        transform.position += delta;

        // Zoom by changing FollowOffset distance (keeps same angle)
        if (follow != null && zoom != null)
        {
            float s = zoom.action.ReadValue<float>();
            if (Mathf.Abs(s) > 0.001f)
            {
                float dist = follow.FollowOffset.magnitude;
                dist = Mathf.Clamp(dist - s * zoomSpeed * Time.deltaTime, minDist, maxDist);
                follow.FollowOffset = follow.FollowOffset.normalized * dist;
            }
        }
    }
}