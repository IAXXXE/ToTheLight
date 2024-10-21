using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UILaserBeam : MonoBehaviour
{
    public RectTransform laserImage;  // 用于表示光线的UI Image
    public float maxDistance = 500f;  // 光线的最大长度
    public GraphicRaycaster raycaster;  // UI中的射线投射器
    public EventSystem eventSystem;  // 事件系统，用于检测UI碰撞
    private RectTransform canvasRect; // Canvas的RectTransform

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 startPos = laserImage.position;  // 光线的起点
        Vector3 direction = laserImage.right;    // 光线的方向

        // 射线起点为光线起点，方向为光线方向
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = startPos;

        // 存储UI元素射线检测的结果
        List<RaycastResult> results = new List<RaycastResult>();

        // 发射射线
        raycaster.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            // 找到最近的碰撞物体
            RaycastResult closestHit = results[0];
            float closestDistance = Vector3.Distance(startPos, closestHit.worldPosition);

            // 截断光线到碰撞位置
            UpdateLaserLength(closestDistance);
        }
        else
        {
            // 如果没有碰撞，则光线长度最大
            UpdateLaserLength(maxDistance);
        }
    }

    void UpdateLaserLength(float distance)
    {
        // 调整RectTransform的宽度来表示光线的长度
        laserImage.sizeDelta = new Vector2(distance, laserImage.sizeDelta.y);
    }
}
