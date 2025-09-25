using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyIfReachDistances : MonoBehaviour
{
    [SerializeField] private float DistanceDestroy;
    void Start()
    {

    }

    void Update()
    {
        DestroyReachedDistance();
    }

    void DestroyReachedDistance()
    {
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2), 0);
        if (Vector3.Distance(CenterScreen, transform.position) > DistanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
