using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float moveSpeed = 20f;

    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currIndex = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;

            CreatePath(target);
        }

        Move();
    }

    private void Move()
    {
        if(pathPointList == null || pathPointList.Count <= 0)
            return;

        if(Vector2.Distance(transform.position, pathPointList[currIndex]) > 0.1f)
        {
            Vector3 dir = (pathPointList[currIndex] - transform.position).normalized;

            transform.position += dir * Time.deltaTime * moveSpeed;
        }
        else
        {
            if(currIndex == pathPointList.Count - 1)
                return;
            currIndex++;
        }

        
    }

    private void CreatePath(Vector3 target)
    {
        currIndex = 0;
        seeker.StartPath(transform.position, target, path =>
        {
            pathPointList = path.vectorPath;
        });
    }
    // public float moveSpeed = 5f;

    // private void Update()
    // {
    //     // 检测鼠标左键点击
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         // 获取鼠标位置并转换为世界坐标
    //         Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         MoveToPosition(targetPosition);
    //     }
    // }

    // private void MoveToPosition(Vector2 targetPosition)
    // {
    //     // 移动角色到目标位置
    //     StartCoroutine(MoveCoroutine(targetPosition));
    // }

    // private System.Collections.IEnumerator MoveCoroutine(Vector2 targetPosition)
    // {
    //     while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
    //     {
    //         // 平滑移动到目标位置
    //         transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    //         yield return null; // 等待下一帧
    //     }

    //     // 确保最终位置准确
    //     transform.position = targetPosition;
    // }


}
