using UnityEngine;

public class MoveBackGroundInLobby : MonoBehaviour
{
    // 배경 이동 속도 조절 (1~200 사이 값 설정 가능)
    [SerializeField][Range(1f, 200f)] float speed = 3f;

    // 배경이 반복될 구간의 값 (해상도 가로 길이)
   float posValue;

    // 배경의 초기 위치
    Vector2 startPos;

    // 배경의 새로운 위치 계산용 변수
    float newPos;

    void Start()
    {
        startPos = transform.position; // 현재 오브젝트의 초기 위치 저장
        posValue = Screen.width;
    }

    void Update()
    {
        // Mathf.Repeat를 사용하여 posValue 값을 초과하지 않도록 반복적인 값 계산
        newPos = Mathf.Repeat(Time.time * speed, posValue);

        // 배경을 왼쪽으로 이동, 초기 위치에서 newPos만큼 이동시킴
        transform.position = startPos + Vector2.left * newPos;
    }
}
