using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerGhost : NetworkBehaviour
{
    [SerializeField] 
    private Player m_Player;

    private float _LocalSpeed;
    private Vector2 _LocalPosition;
    private List<Vector2> _LocalPositionBuffer;
    private NetworkUtility netUtility;
    private static uint _serverTickRate;
    private static int _serverTick;
    private static ulong _RTT;
    private int _localTickRate;
    private uint _localTick;
    private ulong _bufferMax;
    private int Tolerance;
    
    private int tempXLocal;
    private int tempYLocal;
    private int tempXServer;
    private int tempYServer;
    private int diffx;
    private int diffy;



    [SerializeField] 
    private SpriteRenderer m_SpriteRenderer;

    public override void OnNetworkSpawn()
    {
        // L'entite qui appartient au client est recoloriee en rouge
        
        
        if (IsClient && IsOwner)
        {
            Tolerance = 2;
            _LocalPosition = m_Player.Position;
            _LocalSpeed = m_Player.m_Velocity;
            _localTick = 0;
            _localTickRate = 60;
            _serverTick = 0;
            _serverTickRate = 60;
            _RTT = 2 + (1 / (ulong)_serverTickRate);
            _bufferMax = _RTT * (ulong)_localTickRate;
            
        }

            if (IsOwner)
        {
            m_SpriteRenderer.color = Color.red;
        }
    }

    private void Update()
    {
        if (IsClient && IsOwner)
        {
            _LocalPosition += InputCollecting() * (Time.deltaTime * _LocalSpeed);
            _LocalPositionBuffer.Add(_LocalPosition);
            tempXLocal = (int)_LocalPositionBuffer[_LocalPositionBuffer.Count() - 1].x;
            tempYLocal = (int)_LocalPositionBuffer[_LocalPositionBuffer.Count() - 1].y;
            tempXServer = (int)m_Player.Position.x;
            tempYServer = (int)m_Player.Position.y;
            
            if ((tempXLocal - Tolerance < tempXServer) && ((tempXLocal + Tolerance > tempXServer))
                                                       && (tempYLocal - Tolerance < tempYServer) &&
                                                       ((tempYLocal + Tolerance > tempYServer)))
            {
                diffx = tempXServer - tempXLocal ;
                diffx = tempYServer - tempYLocal ;
                for (int i = 0 ; i < _LocalPositionBuffer.Count() ; i++)
                {
                    var temp = _LocalPositionBuffer[i];
                    temp.x -= diffx;
                    temp.y -= diffy;
                    _LocalPositionBuffer[i] = temp;
                }
            }

            transform.position = _LocalPositionBuffer[_LocalPositionBuffer.Count()-1];
            
            if ((ulong)_LocalPositionBuffer.Count() > _bufferMax)
            {
                _LocalPositionBuffer.RemoveAt(0);
            }
            
            
            
            
            
            
            _localTick++;
        }
        
        //_LocalPosition += InputCollecting() * (Time.deltaTime * _LocalSpeed);
        //transform.position = _LocalPosition ;
        if (IsServer)
        {
            transform.position = m_Player.Position;
        }
        
        
    
    
    
    }

    private Vector2 InputCollecting()
    {
        Vector2 inputDirection = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputDirection += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputDirection += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputDirection += Vector2.right;
        }

        return inputDirection;

    }



    
}

