using UnityEngine;

public class PointText : MonoBehaviour
{
    [SerializeField] TextMesh _TextMesh;
    [SerializeField] float _ShowingDuration = 1.0f;
    [SerializeField] float _EmphasisDuration = 0.1f;
    [SerializeField] Vector2 _OriginalLocalScale;
    [SerializeField] Vector3 _MoveVector;

    Vector2 _EmphasisLocalScale;
    float _StartTime;

    public void Awake()
    {
        transform.localScale = _OriginalLocalScale;
    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            var Elapsed = Time.time - _StartTime;

            if (Elapsed > _ShowingDuration)
                gameObject.SetActive(false);
            else
                transform.localPosition += (_MoveVector * Time.deltaTime);

            if (Elapsed > _EmphasisDuration)
            {
                transform.localScale = _OriginalLocalScale;
            }
            else
            {
                var EmphasisTime = Elapsed / _EmphasisDuration;
                var CurrentLocalScaleX = Mathf.Lerp(_EmphasisLocalScale.x, _OriginalLocalScale.x, EmphasisTime);
                var CurrentLocalScaleY = Mathf.Lerp(_EmphasisLocalScale.y, _OriginalLocalScale.y, EmphasisTime);
                transform.localScale = new Vector2(CurrentLocalScaleX, CurrentLocalScaleY);
            }
        }
    }
    public void Show(Vector3 LocalPosition_, string Text_, float EmphasisScale_)
    {
        _TextMesh.text = Text_;
        _StartTime = Time.time;
        transform.localPosition = LocalPosition_;
        transform.localScale = _EmphasisLocalScale = _OriginalLocalScale * EmphasisScale_;
        gameObject.SetActive(true);
    }
}
