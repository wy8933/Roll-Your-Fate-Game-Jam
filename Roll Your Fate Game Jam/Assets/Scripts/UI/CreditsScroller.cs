using UnityEngine;
using UnityEngine.Events;

public class CreditsScroller : MonoBehaviour
{
    [SerializeField] private RectTransform _creditsTransform;
    [SerializeField] private float _scrollSpeed = 50f;
    [SerializeField] private float _endOffset;
    [SerializeField] private UnityEvent _onCreditsFinished;

    private float _targetY;

    void Start()
    {
        if (_creditsTransform == null)
        {
            enabled = false;
            return;
        }

        _targetY = _creditsTransform.anchoredPosition.y + _endOffset;
        
    }

    void Update()
    {
        var pos = _creditsTransform.anchoredPosition;
        pos.y += _scrollSpeed * Time.deltaTime;
        _creditsTransform.anchoredPosition = pos;

        if (pos.y >= _targetY)
        {
            _onCreditsFinished?.Invoke();
        }

        if (Input.anyKeyDown)
        {
            _onCreditsFinished?.Invoke();
        }
    }
}
