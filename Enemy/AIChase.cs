using UnityEngine;

public class AIChase : MonoBehaviour
{

    // Chase player
    public GameObject player;
    public float speed;                 // Tốc độ di chuyển
    public float distanceBetween;       // Khoảng cách phát hiện player
    private float distance;             // Khoảng cách thực tế

    // Wander
    public float maxDistance;           // Bán kính di chuyển tối đa khi wander
    public float range;                 // Khoảng cách coi như đã đến waypoint
    Vector2 wayPoint;                   // Điểm đích wander
    public float visionRange = 10f;     // Enemy chỉ quay về khi player ra xa hơn tầm này


    // Set time
    public float waitTime = 4f;         // Thời gian dừng trước khi chọn điểm mới
    private float waitCounter = 0f;     // Bộ đếm
    private bool isWaiting = false;     // Trạng thái chờ

    // Return to startPosition
    private Vector2 startPosition;      // Vị trí spawn ban đầu
    private bool isReturning = false;   // Đang quay về?

    void Start()
    {
        startPosition = transform.position;
        setNewDestination();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween) // Chase player
        {
            isWaiting = false;
            isReturning = false;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (isReturning) // Returning
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, startPosition) < range)
            {
                isReturning = false;
                setNewDestination();
            }
        }
        else // Wander
        {
            if (distance > visionRange)
            {
                isReturning = true; // Kích hoạt returning nếu enemy nằm ngoài phạm vi tầm nhìn
            }
            else 
            {
                isReturning = false;
            }

            if (isWaiting)
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0f)
                {
                    setNewDestination();
                    isWaiting = false;
                }
                return;
            }

            transform.position = Vector2.MoveTowards(this.transform.position, wayPoint, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, wayPoint) < range)
            {
                isWaiting = true;
                waitCounter = waitTime;
            }
        }
    }

    void setNewDestination()
    {
        wayPoint = startPosition + new Vector2(
            Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance)
        );
    }


}
