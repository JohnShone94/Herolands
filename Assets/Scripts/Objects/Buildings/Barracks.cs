using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    public override void PerformAction(string actionToPerform)
    {
        base.PerformAction(actionToPerform);
        CreateUnit(actionToPerform);
    }

    protected override void Start()
    {
        base.Start();
        actions = new string[] { "Militia" };
    }
}
