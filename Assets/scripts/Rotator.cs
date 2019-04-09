using UnityEngine;

using System.Collections;


public class Rotator : MonoBehaviour
{



    public float speed;

    public float GetRotation()
    {
        return speed;
    }

    void Update()

    {

        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);

    }

}