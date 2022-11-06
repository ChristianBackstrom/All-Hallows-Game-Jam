using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;
    public bool shaking = false;
    public float shakePower = 0.1f;
    private Camera camera;
    private Vector2 shakeOffset = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        this.camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + Vector3.up * 20 + Vector3.forward * -10;
        transform.position += transform.right * shakeOffset.x + transform.up * shakeOffset.y;

        if (shaking)
        {
            shakeOffset = Random.Range(-1, 1) * shakePower * Vector2.one;
        }
    }
}
