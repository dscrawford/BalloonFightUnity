using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Bounds : MonoBehaviour
{
    private float minY, minX, maxY, maxX;
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);
        float[] y = { v[0].y, v[1].y, v[2].y, v[3].y };
        float[] x = { v[0].x, v[1].x, v[2].x, v[3].x };
        minY = Mathf.Min(y);
        minX = Mathf.Min(x);
        maxX = Mathf.Max(y);
        maxY = Mathf.Max(y);
    }

    public float getMinY()
    {
        return minY;
    }

    public float getMinX()
    {
        return minY;
    }

    public float getMaxY()
    {
        return minY;
    }

    public float getMaxX()
    {
        return minY;
    }
}
