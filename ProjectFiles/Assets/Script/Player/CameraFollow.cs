using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Transform target;
	public Vector3 offSet;
	public float damping;

	private Vector3 velocity = Vector3.zero;
    void Start()
    {
		target = GameObject.FindObjectOfType<CharacterMove>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 movePosition = target.position + offSet;
		transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
