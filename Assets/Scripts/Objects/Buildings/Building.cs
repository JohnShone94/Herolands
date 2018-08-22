using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;


public class Building : Objects
{
    public float maxBuildProgress = 10.0f;

    public string[] getBuildQueueValues()
    {
        string[] values = new string[buildQueue.Count];
        int pos = 0;
        foreach (string unit in buildQueue)
        {
            values[pos++] = unit;
        }
        return values;
    }

    public float getBuildPercentage()
    {
        return currentBuildProgress / maxBuildProgress;
    }


    private float currentBuildProgress = 0.0f;
    private Vector3 spawnPoint;

    protected Queue<string> buildQueue;
    protected override void Awake()
    {
        base.Awake();

        buildQueue = new Queue<string>();
        float spawnX = selectionBounds.center.x + transform.forward.x * selectionBounds.extents.x + transform.forward.x * 10;
        float spawnZ = selectionBounds.center.z + transform.forward.z + selectionBounds.extents.z + transform.forward.z * 10;
        spawnPoint = new Vector3(spawnX, 1.0f, spawnZ);
    }

    protected void CreateUnit(string unitName)
    {
        buildQueue.Enqueue(unitName);
    }
    protected void ProcessBuildQueue()
    {
        if (buildQueue.Count > 0)
        {
            currentBuildProgress += Time.deltaTime * ResourceManager.BuildSpeed;
            if (currentBuildProgress > maxBuildProgress)
            {
                if (player)
                {
                    player.AddUnit(buildQueue.Dequeue(), spawnPoint, transform.rotation);
                }
                currentBuildProgress = 0.0f;
            }
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        ProcessBuildQueue();
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }
}
