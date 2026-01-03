using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEditor;

public class Controller : MonoBehaviour
{
    private NavMeshAgent agent;
    private int lastAreaIndex = -1;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckSurface();
    }

    private void CheckSurface()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 0.1f, NavMesh.AllAreas))
        {
            int currentAreaIndex = hit.mask;

            if (currentAreaIndex != lastAreaIndex)
            {
                lastAreaIndex = currentAreaIndex;
                
                string areaName = GetAreaName(hit.mask);
                
                // OnSurfaceChanged?.Invoke(this, new SurfaceChangedEventArgs(areaName, transform.position));
            }
        }
    }

    private string GetAreaName(int mask)
    {
        for (int i = 0; i < 32; i++)
        {
            if ((mask & (1 << i)) != 0)
            {
                // return GameObjectUtility.GetNavMeshAreaName(i);
            }
        }
        return "Unknown";
    }
}