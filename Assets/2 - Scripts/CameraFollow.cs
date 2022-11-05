using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        this.camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + Vector3.up * 20 + Vector3.forward * -10;
    }
}
