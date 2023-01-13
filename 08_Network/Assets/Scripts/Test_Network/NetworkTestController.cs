using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkTestController : MonoBehaviour
{
    public Button startHost;
    public Button starClient;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        startHost = child.GetComponent<Button>();
        child = transform.GetChild(1);
        starClient = child.GetComponent<Button>();

        startHost.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("호스트가 시작되었습니다.");
            }
            else
            {
                Debug.Log("호스트 시작에 실패했습니다.");
            }
        });

        starClient.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("클라이언트의 연결이 시작되었습니다.");
            }
            else
            {
                Debug.Log("클라이언트의 연결이 실패했습니다.");
            }
        });
    }
}
