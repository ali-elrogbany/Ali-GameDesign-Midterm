using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;

    public void Start()
    {
        offset = transform.position - player.transform.position;
    }

    public void LateUpdate()
    {
        gameObject.transform.position = player.transform.position + offset;
    }
}
