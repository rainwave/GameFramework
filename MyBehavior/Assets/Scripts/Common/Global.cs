using System;
using System.Collections.Generic;
using UnityEngine;
public class Global
{
    public static float v3SqrDis(Vector3 v1, Vector3 v2)
    {
        float x = (v1.x - v2.x);
        float y = (v1.y - v2.y);
        float z = (v1.z - v2.z);

        return x * x + y * y + z * z;
    }

    public static bool v3Near(Vector3 a,Vector3 b)
    {
        if (v3SqrDis(a, b) < 5.0f)
            return true;
        return false;
    }

    public static string LogCurNode = "";

}

