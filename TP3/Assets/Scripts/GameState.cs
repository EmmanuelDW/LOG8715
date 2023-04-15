using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : NetworkBehaviour
{
    [SerializeField]
    private GameObject m_GameArea;

    [SerializeField]
    public float m_StunDuration = 1.0f;

    [SerializeField]
    private Vector2 m_GameSize;

    public Vector2 GameSize { get => m_GameSize; }

    private NetworkVariable<bool> m_IsStunned = new NetworkVariable<bool>();
    
    public bool IsStunned { get => m_IsStunned.Value; }
    
    private Coroutine m_StunCoroutine;
    private Coroutine m_StunCoroutineLocal;

    public bool m_stunnedLocal;
    private float m_stuntimerLocal;
    
    
    public float m_CurrentRtt;
    // info sur les tick rate
    public NetworkVariable<uint> ServerTickRate;
    public NetworkVariable<int> ServerTick;
    public uint localTickRate
    {
        get => NetworkUtility.GetLocalTickRate();
    } 
    public int localTick
    {
        get => NetworkUtility.GetLocalTick();
    }
    public ulong localRTT
    {
        get => NetworkUtility.GetCurrentRtt(OwnerClientId);
    }
    public float CurrentRTT { get => m_CurrentRtt / 1000f; }

    public NetworkVariable<float> ServerTime;

    private void Start()
    {
        m_GameArea.transform.localScale = new Vector3(m_GameSize.x * 2, m_GameSize.y * 2, 1);
        m_stunnedLocal = false;
        m_stuntimerLocal = 0f;

    }

    private void Update()
    {
        if (IsClient)
        {
            if (m_stunnedLocal)
            {
                if (m_stuntimerLocal < 0f)
                {
                    m_stunnedLocal = false;
                    m_stuntimerLocal = 0f;
                }
                else
                {
                    m_stuntimerLocal -= Time.deltaTime;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_stunnedLocal = true;
                m_stuntimerLocal = m_StunDuration;
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsSpawned)
        {
            m_CurrentRtt = NetworkManager.NetworkConfig.NetworkTransport.GetCurrentRtt(NetworkManager.ServerClientId);
        }

        if (IsServer)
        {
            ServerTime.Value = Time.time;
            ServerTickRate.Value = localTickRate;
            ServerTick.Value = localTick;
        }
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.OnClientDisconnectCallback += OnClientDisconnect;
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.OnClientDisconnectCallback -= OnClientDisconnect;
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if (!IsServer)
        {
            // si on est un client, retourner au menu principal
            SceneManager.LoadScene("StartupScene");
        }
    }

    public void Stun()
    {
        if (m_StunCoroutine != null)
        {
            StopCoroutine(m_StunCoroutine);
        }
        if (IsServer)
        {
            m_StunCoroutine = StartCoroutine(StunCoroutine());
        }
        
    }

    private IEnumerator StunCoroutine()
    {
        m_IsStunned.Value = true;
        yield return new WaitForSeconds(m_StunDuration);
        m_IsStunned.Value = false;
    }


}

