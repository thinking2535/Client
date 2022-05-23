using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUI : MonoBehaviour
{
    [SerializeField] GameObject _MoveBG = null;
    [SerializeField] GameObject _ChangeBG = null;

    Vector3 _EndPos = new Vector3(256.0f, 256.0f, 0.0f);
    float _MoveSpeed = 18.0f;
    // Update is called once per frame
    private void Update()
    {
        _MoveBG.transform.localPosition = Vector3.MoveTowards(_MoveBG.transform.localPosition, _EndPos, _MoveSpeed * Time.deltaTime);
        if (_MoveBG.transform.localPosition == _EndPos)
            _MoveBG.transform.localPosition = Vector3.zero;
    }
    public void ActiveChangeBG()
    {
        _ChangeBG.SetActive(true);
    }
    public void DeactiveChangeBG()
    {
        _ChangeBG.SetActive(false);
    }
}
