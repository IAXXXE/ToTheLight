using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LineRenderer lineRenderer; // 用于绘制光线
    public Material material;
    public Transform startPoint; // 光线的起点
    public Transform endPoint;
    public float maxDistance = 100f; // 光线的最大长度
    public LayerMask collisionLayers; // 指定哪些图层可以检测到碰撞

    void Start()
    {
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.material = material;
    }

    void Update()
    {
        // 获取光线的起点
        Vector3 startPos = startPoint.position;

        // 射线方向（假设光线向右发射）
        Vector3 direction = endPoint.position;

        // 进行2D射线检测
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, maxDistance, collisionLayers);

        // 如果检测到碰撞
        if (hit.collider != null)
        {
            // 设置光线的终点为碰撞点
            lineRenderer.SetPosition(0, startPos); // 起点
            lineRenderer.SetPosition(1, hit.point); // 终点为碰撞点
        }
        else
        {
            // 如果没有碰到任何物体，光线延伸到最大长度
            lineRenderer.SetPosition(0, startPos); // 起点
            lineRenderer.SetPosition(1, startPos + direction * maxDistance); // 终点为最大长度的方向
        }
    }
}
