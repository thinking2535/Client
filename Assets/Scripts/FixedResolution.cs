using UnityEngine;


public class FixedResolution : MonoBehaviour
{
    // 고정해상도를 적용하고 남는 부분(Letterbox)에 사용될 게임오브젝트(Prefab)
    public GameObject _ObjBackScissor;

    public float Width = 16.0f;
    public float Height = 9.0f;


    void Awake()
    {
        // 시작시 한번 실행(게임 실행중에 해상도가 변경되면 다시 호출)
        UpdateResolution();
    }


    void UpdateResolution()
    {
        // 프로젝트 내에 있는 모든 카메라 얻어오기
        Camera[] ObjCameras = Camera.allCameras;

        // 비율 구하기
        float ResolutionX = Screen.width / Width;
        float ResolutionY = Screen.height / Height;

        // X가 Y보가 큰 경우는 화면이 가로로 놓인 경우
        if (ResolutionX > ResolutionY)
        {
            // 종횡비(Aspect Ratio) 구하기
            float ValueRatio = (ResolutionX - ResolutionY) * 0.5f;
            ValueRatio = ValueRatio / ResolutionX;

            // 위에서 구한 종횡비를 기준으로 카메라의 뷰포트를 재설정
            // 정규화된 좌표라는걸 잊으면 안됨!
            foreach (Camera obj in ObjCameras)
            {
                obj.rect = new Rect(((Screen.width * ValueRatio) / Screen.width) + (obj.rect.x * (1.0f - (2.0f * ValueRatio))),
                                    obj.rect.y,
                                    obj.rect.width * (1.0f - (2.0f * ValueRatio)),
                                    obj.rect.height);
            }


            // 왼쪽에 들어갈 레터박스를 생성하고 위치지정
            GameObject ObjLeftScissor = (GameObject)Instantiate(_ObjBackScissor);
            ObjLeftScissor.GetComponent<Camera>().rect = new Rect(0, 0, (Screen.width * ValueRatio) / Screen.width, 1.0f);

            // 오른쪽 레터박스
            GameObject ObjRightScissor = (GameObject)Instantiate(_ObjBackScissor);
            ObjRightScissor.GetComponent<Camera>().rect = new Rect((Screen.width - (Screen.width * ValueRatio)) / Screen.width,
                                                                   0,
                                                                   (Screen.width * ValueRatio) / Screen.width,
                                                                   1.0f);


            // 생성된 두 레터박스를 자식으로 추가
            ObjLeftScissor.transform.SetParent(gameObject.transform);
            ObjRightScissor.transform.SetParent(gameObject.transform);
        }
        // 화면이 세로로 놓은 경우도 동일한 과정을 거침
        else if (ResolutionX < ResolutionY)
        {
            float ValueRatio = (ResolutionY - ResolutionX) * 0.5f;
            ValueRatio = ValueRatio / ResolutionY;

            foreach (Camera obj in ObjCameras)
            {
                obj.rect = new Rect(obj.rect.x,
                                    ((Screen.height * ValueRatio) / Screen.height) + (obj.rect.y * (1.0f - (2.0f * ValueRatio))),
                                    obj.rect.width,
                                    obj.rect.height * (1.0f - (2.0f * ValueRatio)));
            }


            GameObject ObjTopScissor = (GameObject)Instantiate(_ObjBackScissor);
            ObjTopScissor.GetComponent<Camera>().rect = new Rect(0, 0, 1.0f, (Screen.height * ValueRatio) / Screen.height);

            GameObject ObjBottomScissor = (GameObject)Instantiate(_ObjBackScissor);
            ObjBottomScissor.GetComponent<Camera>().rect = new Rect(0, (Screen.height - (Screen.height * ValueRatio)) / Screen.height
                                                    , 1.0f, (Screen.height * ValueRatio) / Screen.height);


            ObjTopScissor.transform.SetParent(gameObject.transform);
            ObjBottomScissor.transform.SetParent(gameObject.transform);
        }
        else
        {
            // Do Not Setting Camera
        }
    }
}