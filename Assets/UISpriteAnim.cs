using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnim : MonoBehaviour
{
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private Sprite[] m_SpriteArray;
    [SerializeField]
    private float m_Speed = .02f;
    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;
    public bool useAlphaSetter;
    private Renderer r;
    private Material m;

    private void Start()
    {
        if (useAlphaSetter)
        {
            r = GetComponent<Renderer>();
            m = r.material;

            m.SetTexture("_BaseMap", m_SpriteArray[0].texture);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayUIAnim();
        }
    }

    public void PlayUIAnim()
    {
        IsDone = false;
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
    public void  StopUIAnim()
    {
        IsDone = true;
        StopCoroutine(m_CorotineAnim);
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m.SetTexture("_BaseMap", m_SpriteArray[m_IndexSprite].texture);
        m_IndexSprite += 1;

        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}
