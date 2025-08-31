using UnityEngine;

[ExecuteInEditMode]
public class MobileOutlineEffect : MonoBehaviour
{
    [Range(0, 5)]
    public float thickness = 1.0f;
    
    [Range(0, 1)]
    public float sensitivity = 0.5f;
    
    public Color outlineColor = Color.black;
    
    public Shader outlineShader;
    private Material outlineMaterial;
    
    void OnEnable()
    {
        if (outlineShader == null)
            outlineShader = Shader.Find("Hidden/MobileOutline");
            
        outlineMaterial = new Material(outlineShader);
        outlineMaterial.hideFlags = HideFlags.HideAndDontSave;
        
        // تنظیمات اولیه
        UpdateShaderProperties();
    }
    
    void OnDisable()
    {
        if (outlineMaterial != null)
            DestroyImmediate(outlineMaterial);
    }
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (outlineMaterial != null)
        {
            UpdateShaderProperties();
            Graphics.Blit(source, destination, outlineMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
    
    void UpdateShaderProperties()
    {
        if (outlineMaterial != null)
        {
            outlineMaterial.SetFloat("_Thickness", thickness);
            outlineMaterial.SetFloat("_Sensitivity", sensitivity);
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
        }
    }
}