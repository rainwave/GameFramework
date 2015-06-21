using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator
{
    public static Unit genUnit(UnitAttr attr)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Cube")) as GameObject;
        go.name = "unit";
        Unit unit = go.AddComponent<Unit>();
        unit.finalAttr = attr;
        UnitMng.Instance.addUnit(unit);
        return unit;
    }

    public static Unit genUnit<T>(UnitAttr attr) where T : Unit
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Cube")) as GameObject;
        go.name = "unit";
        Unit unit = go.AddComponent<T>();
        unit.finalAttr = attr;
        UnitMng.Instance.addUnit(unit);
        return unit;
    }
}

