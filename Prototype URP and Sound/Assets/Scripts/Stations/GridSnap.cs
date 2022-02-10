using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnap : MonoBehaviour
{
    public int gridSize;
    public float gapSize;

    public float activeRadius;
    public float snapRadius;
    public float snapSpeed;
    public float predictionLength;

    private Vector3[,] grid;
    private int size;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        size = gridSize * 2 + 1;
        grid = new Vector3[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = transform.position + new Vector3(-gapSize * gridSize + i * gapSize, 0, -gapSize * gridSize + j * gapSize);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrid();

        //if out-of-range
        if (Vector3.Distance(CharacterComponents.instance.transform.position, transform.position) > activeRadius)
        {
            return;
        }

        SnapPlayer();
    }

    private void SnapPlayer()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            this.target = GetNode();
            return;
        }

        Vector3 target = (this.target - CharacterComponents.instance.transform.position) * snapSpeed;
        Debug.Log(target);
        CharacterComponents.instance.controller.Move(new Vector3(target.x, 0, target.z));
    }

    private Vector3 GetNode()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float distance = Vector3.Distance(grid[i, j], new Vector3(CharacterComponents.instance.transform.position.x, 0, CharacterComponents.instance.transform.position.z) + (CharacterComponents.instance.transform.right * PlayerInput.playerInput.input.x + CharacterComponents.instance.transform.forward * PlayerInput.playerInput.input.y) * predictionLength);

                if (distance < snapRadius)
                {
                    return grid[i, j];
                }
            }
        }

        return CharacterComponents.instance.transform.position;
    }

    private void UpdateGrid()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = transform.position + new Vector3(-gapSize * gridSize + i * gapSize, 0, -gapSize * gridSize + j * gapSize);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(grid[i, j], snapRadius);
            }
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(grid[gridSize, gridSize], activeRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.target, snapRadius);
        Gizmos.DrawLine(CharacterComponents.instance.transform.position, 
            CharacterComponents.instance.transform.position + (CharacterComponents.instance.transform.right * PlayerInput.playerInput.input.x + CharacterComponents.instance.transform.forward * PlayerInput.playerInput.input.y) * predictionLength);
    }
}
