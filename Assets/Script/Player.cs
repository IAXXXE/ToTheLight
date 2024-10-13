using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 角色移动速度

    private void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标位置并转换为世界坐标
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveToPosition(targetPosition);
        }
    }

    private void MoveToPosition(Vector2 targetPosition)
    {
        // 移动角色到目标位置
        StartCoroutine(MoveCoroutine(targetPosition));
    }

    private System.Collections.IEnumerator MoveCoroutine(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            // 平滑移动到目标位置
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // 等待下一帧
        }

        // 确保最终位置准确
        transform.position = targetPosition;
    }
}
