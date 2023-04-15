using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CircleGhost : NetworkBehaviour
{
    [SerializeField]
    private MovingCircle m_MovingCircle;

    private Vector2 m_positionLocal;
    private Vector2 m_velocityLocal;

    private void Start()
    {
        m_positionLocal = m_MovingCircle.Position;
        m_velocityLocal = m_MovingCircle.Velocity;
        Debug.Log(m_positionLocal);
        Debug.Log(m_velocityLocal);

        
    }

    private void Update()
    {
        
        
        if (IsClient)
        {

            
            if (m_MovingCircle.m_GameState.m_stunnedLocal)
            {
                return;
            }
            
            var size = m_MovingCircle.m_GameState.GameSize;
            if (m_positionLocal.x - m_MovingCircle.m_Radius < -size.x)
            {
                m_positionLocal = new Vector2(-size.x + m_MovingCircle.m_Radius, m_positionLocal.y);
                m_velocityLocal *= new Vector2(-1, 1);
            }
            else if (m_positionLocal.x + m_MovingCircle.m_Radius > size.x)
            {
                m_positionLocal = new Vector2(size.x - m_MovingCircle.m_Radius, m_positionLocal.y);
                m_velocityLocal *= new Vector2(-1, 1);
            }

            if (m_positionLocal.y + m_MovingCircle.m_Radius > size.y)
            {
                m_positionLocal = new Vector2(m_positionLocal.x, size.y - m_MovingCircle.m_Radius);
                m_velocityLocal *= new Vector2(1, -1);
            }
            else if (m_positionLocal.y - m_MovingCircle.m_Radius < -size.y)
            {
                m_positionLocal = new Vector2(m_positionLocal.x, -size.y + m_MovingCircle.m_Radius);
                m_velocityLocal *= new Vector2(1, -1);
            }


            m_positionLocal += m_velocityLocal * Time.deltaTime;
            transform.position = m_positionLocal;
        }




        if (IsServer)
        {
            transform.position = m_MovingCircle.Position; 
        }
    }
}
