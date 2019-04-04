using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Panning : MonoBehaviour
{
    class Axis
    {
        public float x;
        public float y;

        public Axis(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }



    Transform transform;
    Axis minimumBoundary, maximumBoundary;
    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.transform;
        minimumBoundary = new Axis(-0.0082f, 0.3938f);
        maximumBoundary = new Axis(-0.0082f + 558.7227f, 0.3938f + 611.5624f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minimumBoundary.x, maximumBoundary.x),
            Mathf.Clamp(transform.position.y, minimumBoundary.y, maximumBoundary.y),
            transform.position.z
            );
    }
}
