using UnityEngine;

public class ApplyCameraToShader : MonoBehaviour
{
    public Camera mainCamera;      // 主相机
    public Material material;      // 使用自定义Shader的材质

    void Start()
    {
        // 创建一个RenderTexture
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 16);
        
        // 将RenderTexture分配给相机
        mainCamera.targetTexture = renderTexture;

        // 将RenderTexture设置为材质的_MainTex
        material.SetTexture("_MainTex", renderTexture);
    }
}
