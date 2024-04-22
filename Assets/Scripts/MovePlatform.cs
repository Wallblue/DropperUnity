using UnityEngine;

public class MovePl : MonoBehaviour
{
    public Transform objectTransform;
    public float movementSpeed = 10f;

    public float distanceX;
    public float distanceY;
    public float distanceZ;

    public Levels onlyAfter;

    private float _initialX;
    private float _initialY;
    private float _initialZ;

    private float _goalX;
    private float _goalY;
    private float _goalZ;

    private int _prevX;
    private int _prevY;
    private int _prevZ;

    private bool _toCheck;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 initialPosition = objectTransform.position;
        _initialX = initialPosition.x;
        _initialY = initialPosition.y;
        _initialZ = initialPosition.z;
        
        _goalX = _initialX + distanceX;
        _goalY = _initialY + distanceY;
        _goalZ = _initialZ + distanceZ;

        _prevX = distanceX > 0 ? 1 : -1;
        _prevY = distanceY > 0 ? 1 : -1;
        _prevZ = distanceZ > 0 ? 1 : -1;

        switch (onlyAfter)
        {
            case Levels.Gravity:
                _toCheck = GameManager.IsGravityLevelDone;
                break;

            case Levels.MovingPlatforms:
                _toCheck = GameManager.IsMovingPlatformsLevelDone;
                break;

            default:
                _toCheck = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_toCheck) return;

        Vector3 position = objectTransform.position;
        int x = ComputeMove(position.x, _goalX);
        if (x != _prevX) (_initialX, _goalX) = (_goalX, _initialX);
        _prevX = x;

        int y = ComputeMove(position.y, _goalY);
        if (y != _prevY) (_initialY, _goalY) = (_goalY, _initialY);
        _prevY = y;

        int z = ComputeMove(position.z, _goalZ);
        if (z != _prevZ) (_initialZ, _goalZ) = (_goalZ, _initialZ);
        _prevZ = z;

        var movementIntent = new Vector3(
            x,
            y,
            z
        ).normalized;

        objectTransform.position += movementIntent * (movementSpeed * Time.deltaTime);
    }

    private int ComputeMove(float currentCoordinate, float goalCoordinate)
    {
        if (currentCoordinate < goalCoordinate)
        {
            return 1;
        }
        if (currentCoordinate > goalCoordinate)
        {
            return -1;
        }

        return 0;
    }
}
