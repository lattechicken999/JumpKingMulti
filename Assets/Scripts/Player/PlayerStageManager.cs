using UnityEngine;

public class PlayerStageManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("게임 클리어입니다.");

        }
    }
}
