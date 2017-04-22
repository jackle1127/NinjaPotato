using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordTrail : MonoBehaviour {
    public float trailAlpha;
	public float followAlpha;
    public Transform trailBegin;
    public Transform trailEnd;
	public Transform player;

	private int numOfNodes;
    private Mesh mesh;
    private Vector3[] vertices;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        numOfNodes = mesh.vertices.Length / 2;
	}
	
	// Update is called once per frame
	void Update() {
		vertices = mesh.vertices;
		vertices[0] = trailBegin.position - transform.position;
		vertices[1] = trailEnd.position - transform.position;
		mesh.vertices = vertices;
	}

	void FixedUpdate () {
		transform.position += (player.position - transform.position) * followAlpha;
		vertices = mesh.vertices;
        /*vertices[0].x /= transform.lossyScale.x;
        vertices[0].y /= transform.lossyScale.y;
        vertices[0].z /= transform.lossyScale.z;
        vertices[1].x /= transform.lossyScale.x;
        vertices[1].y /= transform.lossyScale.y;
        vertices[1].z /= transform.lossyScale.z;*/

        for (int i = numOfNodes - 1; i > 0; i--) {
            int currentIndex = i * 2;
            int otherIndex = (i - 1) * 2;
            vertices[currentIndex] += (vertices[(otherIndex)] - vertices[currentIndex]) * trailAlpha;
            vertices[currentIndex + 1] += (vertices[otherIndex + 1] - vertices[currentIndex + 1]) * trailAlpha;
        }

        mesh.vertices = vertices;
	}

    public void trailEnable(bool enabled) {
        GetComponent<MeshRenderer>().enabled = enabled;
    }
}
