using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PropsPool : MonoBehaviour
{
    public int numberOfProps; //number of props to instantiate
    public GameObject[] propsPrefabs;

    private static GameObject[] propsPool;
    private static int propsPoolSize;

    private static PropsPool pp;

    void Awake()
    {
        //simple singleton
        if (pp == null)
        {
            pp = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        propsPoolSize = numberOfProps;
        propsPool = new GameObject[propsPoolSize];

        for (int i = 0; i < numberOfProps; i++)//instantiate a number of props equal to numberOfProps
        {
            propsPool[i] = ((GameObject)Instantiate(propsPrefabs[i % propsPrefabs.Length], transform));
        }
    }

    public static GameObject getProp()
    {
        propsPoolSize--; //should not go below 0
        if (propsPoolSize < 0) //number of props in the pool should be increased if ever reaches here
        {
            return null;
        }
        GameObject temp = propsPool[propsPoolSize];
        propsPool[propsPoolSize] = null;
        return temp;

    }

    public static void returnProp(GameObject prop)
    {
        if (prop != null)
        {
            if (propsPoolSize == propsPool.Length)//if any props are returned when the pool is full (if instantiated elsewhere)
            {
                Destroy(prop);
            }
            else
            {
                prop.SetActive(false);
                prop.transform.parent = pp.transform;

                propsPool[propsPoolSize] = prop;
                propsPoolSize++;
            }
        }
        else
        {
            Debug.LogWarning("Null was passed into PropsPool");
        }
    }
}
