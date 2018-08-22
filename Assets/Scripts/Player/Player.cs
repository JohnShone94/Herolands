using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;

public class Player : MonoBehaviour
{
    //21-08-2018//
    //John Shone//

    public string username;
    public bool human;
    public HUD hud;
    public Objects selectedObject { get; set; }
    public int startGold, startGoldLimit, startWood, startWoodLimit, 
        startStone, startStoneLimit, startIron, startIronLimit, startCoal, startCoalLimit;

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }

    public void IncrementResourceLimit(ResourceType type, int amount)
    {
        resourceLimits[type] += amount;
    }
    
    public void AddUnit(string unitName, Vector3 spawnPoint, Quaternion rotation)
    {
        Units units = GetComponentInChildren<Units>();
        GameObject newUnit = (GameObject)Instantiate(ResourceManager.GetUnit(unitName), spawnPoint, rotation);
        newUnit.transform.parent = units.transform;
    }

    private Dictionary<ResourceType, int> resources, resourceLimits;

    private void Start ()
    {
        hud = GetComponentInChildren<HUD>();

        AddStartResourceLimits();
        AddStartResources();
    }
	
	private void Update ()
    {
		if(human)
        {
            hud.SetResourceValues(resources, resourceLimits);
        }
	}

    private void Awake()
    {
        resources = InitResourceList();
        resourceLimits = InitResourceList();
    }

    private Dictionary<ResourceType, int> InitResourceList()
    {
        Dictionary<ResourceType, int> list = new Dictionary<ResourceType, int>();
        list.Add(ResourceType.Gold, 0);
        list.Add(ResourceType.Wood, 0);
        list.Add(ResourceType.Stone, 0);
        list.Add(ResourceType.Iron, 0);
        list.Add(ResourceType.Coal, 0);
        return list;
    }

    private void AddStartResourceLimits()
    {
        IncrementResourceLimit(ResourceType.Gold, startGoldLimit);
        IncrementResourceLimit(ResourceType.Wood, startWoodLimit);
        IncrementResourceLimit(ResourceType.Stone, startStoneLimit);
        IncrementResourceLimit(ResourceType.Iron, startIronLimit);
        IncrementResourceLimit(ResourceType.Coal, startCoalLimit);

    }

    private void AddStartResources()
    {
        AddResource(ResourceType.Gold, startGold);
        AddResource(ResourceType.Wood, startWood);
        AddResource(ResourceType.Stone, startStone);
        AddResource(ResourceType.Iron, startIron);
        AddResource(ResourceType.Coal, startCoal);
    }

}
