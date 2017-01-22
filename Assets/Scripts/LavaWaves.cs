using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWaves : MonoBehaviour {

    [SerializeField]
    private GameObject lavaNodePrefab;

    public Material lavaMaterial;

    [System.Serializable]
    public class SineWaveVariables
    {
        public float waveAmplitude = 1;
        public float radianMultiplier = 1;
        public float waveOffset = Mathf.PI / 2;
    }

    [SerializeField]
    private SineWaveVariables firstSineWave;
    [SerializeField]
    private SineWaveVariables secondSineWave;

    public int nodesLength;
    public float nodeDistance;
    public float nodeUpdateFrequency = 0.1f;
    private float nextUpdateTime;

    public float lavaMeshLowerBoundsOffset = 1.5f;
    public float lavaIncreaseSpeed = 1;
    public float lavaIncreaseAfterStart = 2.5f;

    public float surfaceWidth = 0.3f;
    public float meshOffsetSpeed = 0.1f;

    private GameObject[] lavaNodes;

    private float originalHeight;
    private float heightOffset = 0;
    private float lavaMeshLowerBoundY;

    private MeshFilter meshFilter;
    private LineRenderer lineRenderer;
    private MeshRenderer meshRenderer;
    private Material meshMaterial;
    private float meshXOffset;

    // Use this for initialization
    void Start () {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        if ((lineRenderer = GetComponent<LineRenderer>()) != null)
        {
            lineRenderer.numPositions = nodesLength;
            lineRenderer.startWidth = surfaceWidth;
            lineRenderer.endWidth = surfaceWidth;
        }

        // nodesLength cannot be less than 2
        if (nodesLength < 2)
        {
            nodesLength = 2;
        }

        lavaNodes = new GameObject[nodesLength];

        Vector2 startPosition = new Vector2(transform.position.x + ((nodesLength * nodeDistance) / 2), transform.position.y);
        originalHeight = transform.position.y;

        //initialize lava nodes
		for (int i = 0; i < nodesLength; i++)
        {
            lavaNodes[i] = Instantiate(lavaNodePrefab,
                                        new Vector2(startPosition.x - (i * nodeDistance), startPosition.y),
                                        transform.rotation,
                                        transform);
        }

        lavaMeshLowerBoundY = lavaNodes[0].transform.localPosition.y - lavaMeshLowerBoundsOffset;

        generateLavaMesh();

        StartCoroutine(moveUpwards());
	}

    IEnumerator moveUpwards()
    {
        float destY = transform.position.y + lavaIncreaseAfterStart;
        while (transform.position.y < destY)
        {
            transform.position += new Vector3(0, lavaIncreaseSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (originalHeight != transform.position.y)
        {
            originalHeight = transform.position.y;
        }

        if (Time.time > nextUpdateTime)
        {
            // update all nodes except first
            updateAllNodes();
            // update first node
            updateFirstNode();

            nextUpdateTime = Time.time + nodeUpdateFrequency;
        }

        updateLavaSurface();

        updateLavaMesh();
	}

    void updateLavaSurface()
    {
        if (lineRenderer != null)
        {
            for (int i = 0; i < nodesLength; i++)
            {
                lineRenderer.SetPosition(i, lavaNodes[i].transform.position);
            }
        }
    } 

    void updateLavaMesh()
    {
        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

        //every even vertice is a vertex at the top of the mesh
        int currentVerticeIndex = 0;
        for(int i = 1; i < nodesLength; i++)
        {
            vertices[currentVerticeIndex] = new Vector3(lavaNodes[i].transform.localPosition.x, lavaNodes[i].transform.localPosition.y, 0);
            currentVerticeIndex += 2;

            vertices[currentVerticeIndex] = new Vector3(lavaNodes[i - 1].transform.localPosition.x, lavaNodes[i - 1].transform.localPosition.y, 0);
            currentVerticeIndex += 2;

            vertices[currentVerticeIndex] = new Vector3(lavaNodes[i - 1].transform.localPosition.x, lavaNodes[i - 1].transform.localPosition.y, 0);
            currentVerticeIndex += 2;
        }

        mesh.vertices = vertices;

        if (meshMaterial == null)
        {
            meshMaterial = meshRenderer.sharedMaterial;
        }
        meshXOffset += meshOffsetSpeed * Time.deltaTime;
        meshMaterial.SetTextureOffset("_MainTex", new Vector2(meshXOffset, 0));
    }

    void generateLavaMesh()
    {
        // every additional node to the first node requires 6 vertices
        Vector3[] vertices = new Vector3[(nodesLength - 1) * 6];
        // populate mesh data
        int currentVerticeIndex = -1;
        for (int i = 1; i < nodesLength; i++)
        {
            //top right
            vertices[++currentVerticeIndex] = new Vector3(lavaNodes[i].transform.localPosition.x, lavaNodes[i].transform.localPosition.y, 0);
            
            //bottom right
            vertices[++currentVerticeIndex] = new Vector3(lavaNodes[i].transform.localPosition.x, lavaMeshLowerBoundY, 0);

            //top left
            vertices[++currentVerticeIndex] = new Vector3(lavaNodes[i - 1].transform.localPosition.x, lavaNodes[i - 1].transform.localPosition.y, 0);

            //second set
            //bottom left
            vertices[++currentVerticeIndex] = new Vector3(lavaNodes[i - 1].transform.localPosition.x, lavaMeshLowerBoundY, 0);

            //top left
            vertices[++currentVerticeIndex] = new Vector3(lavaNodes[i - 1].transform.localPosition.x, lavaNodes[i - 1].transform.localPosition.y, 0);

            //bottom right
            vertices[++currentVerticeIndex] = new Vector3(lavaNodes[i].transform.localPosition.x, lavaMeshLowerBoundY, 0);
        }

        int[] triangles = new int[vertices.Length];
        for (int i = 0; i < triangles.Length; i += 3)
        {
            triangles[i] = i + 2;
            triangles[i+1] = i + 1;
            triangles[i+2] = i;
        }

        Vector2[] normals = new Vector2[vertices.Length];
        int currentNormalIndex = -1;
        for (int i = 1; i < nodesLength; i++)
        {
            //top right
            normals[++currentNormalIndex] = new Vector2(1, 1);

            //bottom right
            normals[++currentNormalIndex] = new Vector2(1, 0);

            //top left
            normals[++currentNormalIndex] = new Vector2(0, 1);

            //second set
            //bottom left
            normals[++currentNormalIndex] = new Vector2(0, 0);

            //top left
            normals[++currentNormalIndex] = new Vector2(0, 1);

            //bottom right
            normals[++currentNormalIndex] = new Vector2(1, 0);
        }

        //set mesh data
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.uv = normals;
        meshFilter.mesh.triangles = triangles;

        //meshFilter.mesh.RecalculateBounds();

        //set material
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = lavaMaterial;
        meshRenderer.sortingLayerName = "Default";
        
    }

    //updates first node
    void updateFirstNode()
    {
        //destination for first node
        //uses 2 sine waves and any offsets to determine position 
        Vector2 dest = new Vector2(lavaNodes[0].transform.position.x, 
                                    originalHeight + (Mathf.Sin((Time.time * firstSineWave.radianMultiplier) - firstSineWave.waveOffset) * firstSineWave.waveAmplitude) + 
                                    (Mathf.Sin((Time.time * secondSineWave.radianMultiplier) - secondSineWave.waveOffset) * secondSineWave.waveAmplitude) * Time.deltaTime + heightOffset);

        lavaNodes[0].transform.position = dest;
    }

    //updates all nodes except first
    void updateAllNodes()
    {
        //shift node positions to the left
        for (int i = lavaNodes.Length - 1; i > 0; i--)
        {
            lavaNodes[i].transform.position = new Vector2(lavaNodes[i].transform.position.x,
                                                            lavaNodes[i - 1].transform.position.y);
        }
    }

    public void doWave()
    {
        StartCoroutine(doLargeWave(3, 0.5f, 0.25f));
    }

    // does a single large wave (upward)
    IEnumerator doLargeWave(int height, float upwardSpeedMultiplier, float downwardSpeedMultiplier)
    {
        // currently only supports 1 large wave at a time
        if (heightOffset == 0)
        {
            bool reverse = false;

            //add first iteration so heightOffset would be greater than 0
            heightOffset += height * Time.deltaTime * upwardSpeedMultiplier;
            yield return new WaitForEndOfFrame();
            while (heightOffset > 0)
            {
                if (!reverse)
                {
                    heightOffset += height * Time.deltaTime * upwardSpeedMultiplier;
                    // if heightOffset has reached height
                    if (heightOffset >= height)
                    {
                        reverse = true;
                    }
                }
                else // reverse offset
                {
                    heightOffset -= height * Time.deltaTime * downwardSpeedMultiplier;
                }

                yield return new WaitForEndOfFrame();
            }

            heightOffset = 0;
        }
        else
        {
            Debug.LogWarning("LavaWaves doLargeWave coroutine called when heightOffset != 0");
        }
    }
}
