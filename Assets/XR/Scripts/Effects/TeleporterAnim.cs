using UnityEngine;
using UnityEngine.Serialization;

public class TeleporterAnim : MonoBehaviour
{

    float m_MaxAlphaIntensity = 2f;
    float m_MinAlphaIntensity = 0f;
    float m_CurrentTime = 0f;
    float m_MaxEmission = 1f;
    float m_MinEmission = 0.1f;
    float m_CurrentEmission = 0.1f;

    public MasterController mc;

    [FormerlySerializedAs("fadeSpeed")]
    [SerializeField]
    float m_FadeSpeed = 2.2f;

    bool isVisible = false;
    bool m_Highlighted = false;

    [FormerlySerializedAs("meshRenderer")]
    [SerializeField]
    MeshRenderer m_MeshRenderer = default;
    MaterialPropertyBlock m_Block;

    int m_AlphaIntensityID;
    
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_AlphaIntensityID = Shader.PropertyToID("AlphaIntensity");

        m_Block = new MaterialPropertyBlock();
        m_Block.SetFloat(m_AlphaIntensityID, m_CurrentTime);

        m_CurrentTime = 1f;

        m_MeshRenderer.SetPropertyBlock(m_Block);
    }
    
    void Update()
    {
        if (mc.m_RightLineVisual.enabled || mc.m_LeftLineVisual.enabled)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }
        if (isVisible)
        {
            m_CurrentTime += Time.deltaTime * m_FadeSpeed;
            
        }
        else if (!isVisible)
        {
            m_CurrentTime -= Time.deltaTime * m_FadeSpeed;
        }
        if (m_Highlighted)
        {
            m_CurrentEmission += Time.deltaTime * m_FadeSpeed;
        }
        else
        {
            m_CurrentEmission -= Time.deltaTime * m_FadeSpeed;
        }

        if (m_CurrentTime > m_MaxAlphaIntensity)
            m_CurrentTime = m_MaxAlphaIntensity;
        else if (m_CurrentTime < m_MinAlphaIntensity)
            m_CurrentTime = m_MinAlphaIntensity;

        if (m_CurrentEmission > m_MaxEmission)
            m_CurrentEmission = m_MaxEmission;
        else if (m_CurrentEmission < m_MinEmission)
            m_CurrentEmission = m_MinEmission;

        m_MeshRenderer.GetPropertyBlock(m_Block);
        m_Block.SetFloat(m_AlphaIntensityID, m_CurrentTime);
        //m_Block.SetFloat(m_EmissionID, m_CurrentEmission); 
        m_MeshRenderer.SetPropertyBlock(m_Block);
    }

    public void StartHighlight()
    {
        m_Highlighted = true;
    }

    public void StopHighlight()
    {
        m_Highlighted = false;
    }
}
