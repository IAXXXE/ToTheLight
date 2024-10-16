using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform joystickBackground;  // 摇杆背景
    public RectTransform joystickHandle;      // 摇杆手柄
    public float handleRange = 100f;          // 手柄可以移动的最大范围
    private Vector2 inputVector;              // 存储输入向量

    public float moveSpeed = 100f;
    public new Transform light;

    private void Start()
    {
        joystickHandle.anchoredPosition = Vector2.zero; // 初始化手柄位置在中心
    }

    private void Update()
    {
        Vector2 input = GetInput();

        if(input != Vector2.zero)
        {
            Vector3 movement = new Vector3(input.x, input.y, 0);
            light.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // 鼠标按下时也执行拖动逻辑
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero; // 当鼠标抬起时，重置输入
        joystickHandle.anchoredPosition = Vector2.zero; // 手柄回到中心
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 将屏幕空间的坐标转换为摇杆背景的局部坐标
        Vector2 direction = eventData.position - (Vector2)joystickBackground.position;
        float distance = Mathf.Clamp(direction.magnitude, 0f, handleRange); // 限制手柄移动的范围
        Vector2 normalizedDirection = direction.normalized; // 归一化方向向量

        // 更新手柄位置
        joystickHandle.anchoredPosition = normalizedDirection * distance;

        // 计算输入向量
        inputVector = normalizedDirection * (distance / handleRange); // 输入的大小介于 0 到 1 之间
    
    }

    public Vector2 GetInput()
    {
        return inputVector; // 返回输入的向量
    }
}
