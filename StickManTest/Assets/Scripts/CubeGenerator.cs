using UnityEngine;

using Random = UnityEngine.Random;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private int _cubeCount;
    
    private readonly int _xDefault = 1;
    private readonly int _zDefault = 1;

    private void Awake()
    {
        GenerateCubeMap();
    }

    private void GenerateCubeMap()
    {
        for (int i = 0; i < _cubeCount; i++)
        {
            CreateCubeWithRandomPositionAndHight();
        }
    }

    private GameObject CreateCubeWithRandomPositionAndHight()
    {
        var y = Random.Range(2, 5);

        var position = new Vector3(Random.Range(25, -25), 5, Random.Range(25, -25));

        return CreateCube(position, y);
    }

    private GameObject CreateCube(Vector3 position, int y)
    {
        var cube = Instantiate(_cube, position, Quaternion.identity);

        cube.transform.localScale = new Vector3(_xDefault, y, _zDefault);
        
        cube.transform.position = new Vector3(position.x, AlignCubeToGround(cube.transform.position).y, position.z);

        return cube;
    }

    private Vector3 AlignCubeToGround(Vector3 cubePosition)
    {
        Physics.Raycast(cubePosition, Vector3.down, out var hit, 5f);

        var newUp = hit.normal;

        return newUp;
    }
}