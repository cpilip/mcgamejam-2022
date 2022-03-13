using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform m_cam;
    [SerializeField] private Vector2 m_parallaxEffectMultiplier;
    private Vector3 m_lastCameraPos;
    private float m_textureUnitSizeX;
    private float m_textureUnitSizeY;
    [SerializeField] private bool infiniteVertical;
    [SerializeField] private bool infiniteHorizontal;
    void Awake()
    {
        m_cam = Camera.main.transform;
        m_lastCameraPos = m_cam.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        m_textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        m_textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }
    
    void LateUpdate()
    {
        Vector3 deltaMovement = m_cam.position - m_lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * m_parallaxEffectMultiplier.x, deltaMovement.y * m_parallaxEffectMultiplier.y);
        m_lastCameraPos = m_cam.position;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(m_cam.transform.position.x - transform.position.x) >= m_textureUnitSizeX)
            {
                float offsetPositionX = (m_cam.position.x - transform.position.x) % m_textureUnitSizeX;
                transform.position = new Vector3(m_cam.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(m_cam.transform.position.y - transform.position.y) >= m_textureUnitSizeY)
            {
                float offsetPositionY = (m_cam.position.y - transform.position.y) % m_textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, m_cam.position.y + offsetPositionY);
            }
        }
        
    }
}
