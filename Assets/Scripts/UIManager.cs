using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text agentArrivalText;
    private int arrivalCount = 0;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Print a message when an agent arrives at its destination
    /// </summary>
    /// <param name="value">Agent name</param>
    public void UpdateAgentArrival(string value)
    {
        if (agentArrivalText == null) return;
        arrivalCount++;
        agentArrivalText.text = value + " arrived at destination #" + arrivalCount + "\n" + agentArrivalText.text;
    }
}