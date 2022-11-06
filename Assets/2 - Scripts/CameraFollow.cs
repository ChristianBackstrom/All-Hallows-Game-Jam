using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;
    public bool shaking = false;
    public float shakePower = 0.1f;
    public Transform fixedPoint = null;
    private Camera camera;
    private Vector2 shakeOffset = Vector2.zero;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        this.camera = GetComponent<Camera>();
        this.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            shakeOffset.x = Random.Range(-1, 1) * shakePower;
            shakeOffset.y = Random.Range(-1, 1) * shakePower;
        }

        if (fixedPoint != null)
        {
            position = Vector3.Lerp(position, fixedPoint.position, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, fixedPoint.rotation, Time.deltaTime);
        }
        else
        {
            position = player.transform.position + Vector3.up * 20 + Vector3.forward * -10;
        }

        transform.position = position + transform.right * shakeOffset.x + transform.up * shakeOffset.y;
    }
}
