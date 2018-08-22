using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;

public class PrefabsList : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject[] units;
    public GameObject[] worldObjects;
    public GameObject player;

    public GameObject GetBuildings(string name)
    {
        for(int i = 0; i < buildings.Length; i++)
        {
            Building building = buildings[i].GetComponent<Building>();
            if(building && building.name == name)
            {
                return buildings[i];
            }
        }
        return null;
    }

    public GameObject GetUnits(string name)
    {
        for(int i = 0; i < units.Length; i++)
        {
            Unit unit = units[i].GetComponent<Unit>();
            if(unit && unit.name == name)
            {
                return units[i];
            }
        }
        return null;
    }

    public GameObject GetWorldObjects(string name)
    {
        foreach(GameObject worldObject in worldObjects)
        {
            if(worldObject.name == name)
            {
                return worldObject;
            }
        }
        return null;
    }

    public GameObject GetPlayerObject()
    {
        return player;
    }

    public Texture2D GetBuildImage(string name)
    {
        for(int i = 0; i < buildings.Length; i++)
        {
            Building building = buildings[i].GetComponent<Building>();
            if(building && building.name == name)
            {
                return building.buildImage;
            }
        }

        for (int i = 0; i < units.Length; i++)
        {
            Unit unit = units[i].GetComponent<Unit>();
            if (unit && unit.name == name)
            {
                return unit.buildImage;
            }
        }

        return null;
    }
    
    private static bool created = false;

    private void Awake()
    {
        if(!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            ResourceManager.SetGameObjectList(this);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
