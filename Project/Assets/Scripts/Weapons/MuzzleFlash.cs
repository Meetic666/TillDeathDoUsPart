using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour
{
    public float m_FlashDuration;

    float m_Timer;

    Light m_Light;

    Behaviour m_Halo;

	// Use this for initialization
	void Start ()
    {
        m_Light = GetComponent<Light>();

        m_Halo = (Behaviour) GetComponent("Halo");

        EndFlash();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_Timer > 0.0f)
        {
            m_Timer -= Time.deltaTime;

            if(m_Timer <= 0.0f)
            {
                EndFlash();
            }
        }
	}

    public void StartFlash()
    {
        m_Light.enabled = true;

        m_Halo.enabled = true;

        m_Timer = m_FlashDuration;
    }

    public void EndFlash()
    {
        m_Light.enabled = false;

        m_Halo.enabled = false;
    }
}
