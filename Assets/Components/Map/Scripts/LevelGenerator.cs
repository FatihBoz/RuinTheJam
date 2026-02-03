using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Components")]
    [SerializeField] private List<LevelComponent> startComponents;
    [SerializeField] private List<LevelComponent> endComponents;
    [SerializeField] private List<LevelComponent> middleComponents;

    [Header("Grid Components")]
    [SerializeField] private Grid grid;
    private Vector2 cellSize;
    private readonly float componentCellSize = 25f;

    void Start()
    {
        cellSize = grid.cellSize;
        var componentWidth = cellSize.x * componentCellSize;

        int r = Random.Range(0, startComponents.Count);

        var startComponent = Instantiate(startComponents[r], grid.transform);
        startComponent.transform.position = new Vector2(0, 0);

        for (int i = 1; i < 5; i++)
        {
            int a = Random.Range(0, middleComponents.Count);
            var component = Instantiate(middleComponents[a], grid.transform);
            component.transform.position = new Vector2(i * componentWidth, 0);
        }

        int s = Random.Range(0, endComponents.Count);
        var endComponent = Instantiate(endComponents[s], grid.transform);
        endComponent.transform.position = new Vector2(5 * componentWidth, 0);
    }

    
}
