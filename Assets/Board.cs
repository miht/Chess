using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{

    public Vector3 dimensions;
    public MeshRenderer bounds;
    public Piece piecePrefab;
    private Piece[] pieces = new Piece[64];

    void Awake()
    {
        dimensions = bounds.bounds.size;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 position = new Vector3(i * dimensions.x / 8, j * dimensions.z / 8);
                pieces[i * 8 + j] = Instantiate(piecePrefab, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
