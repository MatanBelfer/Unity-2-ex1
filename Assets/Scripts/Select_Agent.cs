using UnityEngine;

public class Select_Agent : MonoBehaviour
{
    public enum AgentType
    {
        Agent1,
        Agent2
    }

    [SerializeField] private AgentType selectedAgentType;
    [SerializeField] private GameObject agent1;
    [SerializeField] private GameObject agent2;
    [SerializeField] private GameObject selectedAgent;
    [SerializeField] private RTSNorthUpCam cameraTarget;
    
    void Start()
    {
        if (selectedAgent == null)
        {
            if (agent1 == null || agent2 == null)
            {
                Debug.LogError("No 2 agents selected");
            }
            else
            {
                selectedAgent = selectedAgentType == AgentType.Agent1 ? agent1 : agent2;
            }
        }
    }

    
    void Update()
    {
        
    }
}