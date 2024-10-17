using UnityEngine;
using UnityEngine.UI;

public class SpectrumTextureGenerator : MonoBehaviour
{
    public int textureWidth = 512;  // 光谱图的宽度
    public int textureHeight = 50;  // 光谱图的高度
    private Texture2D spectrumTexture;

    void Start()
    {
        // 创建一张纹理
        spectrumTexture = new Texture2D(textureWidth, textureHeight);

        // 生成光谱图
        GenerateSpectrumTexture();

        // 将纹理应用到材质
        Image renderer = GetComponent<Image>();
        renderer.material.mainTexture = spectrumTexture;
        
    }

    void GenerateSpectrumTexture()
    {
        for (int x = 0; x < textureWidth; x++)
        {
            // 计算当前x坐标在哪一个颜色段
            Color color = GetColorForX(x);

            // 为当前x位置的每一行赋予颜色
            for (int y = 0; y < textureHeight; y++)
            {
                spectrumTexture.SetPixel(x, y, color);
            }
        }

        // 应用更改到纹理
        spectrumTexture.Apply();
    }

    // 根据x位置，计算光谱对应的颜色
    Color GetColorForX(int x)
    {
        float normalizedX = (float)x / textureWidth;  // 将x归一化为0到1之间
        int segmentWidth = textureWidth / 4;  // 每个颜色段的宽度

        // 第1段：从RGB(0, 0, 255)到RGB(0, 255, 255)
        if (x < segmentWidth)
        {
            float t = (float)x / segmentWidth;  // 当前x在段内的插值
            return new Color(0f, t, 1f);       // G从0逐渐增加到1
        }
        // 第2段：从RGB(0, 255, 255)到RGB(0, 255, 0)
        else if (x < segmentWidth * 2)
        {
            float t = (float)(x - segmentWidth) / segmentWidth;
            return new Color(0f, 1f, 1f - t);  // B从1逐渐减小到0
        }
        // 第3段：从RGB(0, 255, 0)到RGB(255, 255, 0)
        else if (x < segmentWidth * 3)
        {
            float t = (float)(x - segmentWidth * 2) / segmentWidth;
            return new Color(t, 1f, 0f);       // R从0逐渐增加到1
        }
        // 第4段：从RGB(255, 255, 0)到RGB(255, 0, 0)
        else
        {
            float t = (float)(x - segmentWidth * 3) / segmentWidth;
            return new Color(1f, 1f - t, 0f);  // G从1逐渐减小到0
        }
    }
}
