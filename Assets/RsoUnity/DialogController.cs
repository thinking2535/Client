using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Dialog : MonoBehaviour
{
    public Canvas canvas { get; private set; }
    public float thickness = 0.0f;
    protected virtual void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }
    protected virtual void Update()
    {
    }
    protected virtual void OnDestroy()
    {
    }
    public abstract Task backButtonPressed();
}
public class DialogController
{
    class _Data
    {
        public Dialog dialog;
        public CancellationTokenSource cancellationTokenSource;
        public object returnValue;

        public _Data(Dialog dialog, CancellationTokenSource cancellationTokenSource)
        {
            this.dialog = dialog;
            this.cancellationTokenSource = cancellationTokenSource;
        }
        public void cancelToken(object returnValue)
        {
            this.returnValue = returnValue;
            cancellationTokenSource.Cancel();
        }
    }

    string _sortingLayerName;
    float _startPlaneDistance;
    Stack<_Data> _datas = new Stack<_Data>();
    public Int32 count => _datas.Count;
    public bool isEmpty => _datas.Count == 0;
    public bool isNotEmpty => _datas.Count > 0;
    float _getTotalThickness()
    {
        float thickness = 0.0f;

        foreach (var i in _datas)
            thickness += i.dialog.thickness;

        return thickness;
    }
    float _getTotalPlaneDistance()
    {
        return _startPlaneDistance - _getTotalThickness();
    }

    public DialogController(string sortingLayerName, float startPlaneDistance)
    {
        _sortingLayerName = sortingLayerName;
        _startPlaneDistance = startPlaneDistance;
    }
    ~DialogController()
    {
        clear();
    }
    public async Task<object> push(Dialog dialog, Camera camera)
    {
        dialog.canvas.worldCamera = camera;
        dialog.canvas.sortingLayerName = _sortingLayerName;
        dialog.canvas.sortingOrder = _datas.Count;
        dialog.canvas.planeDistance = _getTotalPlaneDistance();

        var data = new _Data(dialog, new CancellationTokenSource());

        _datas.Push(data);

        await Task.Run(
            () =>
            {
                while (true)
                {
                    if (data.cancellationTokenSource.Token.IsCancellationRequested)
                        break;

                    Task.Delay(1).Wait();
                }
            },
            data.cancellationTokenSource.Token);

        GameObject.Destroy(data.dialog.gameObject);
        return data.returnValue;
    }
    public void pop(object returnValue = null)
    {
        if (isEmpty)
            return;

        _datas.Pop().cancelToken(returnValue);
    }
    public void clear()
    {
        while (isNotEmpty)
            pop();
    }
    public async Task<bool> sendBackButtonPressedEvent()
    {
        if (isEmpty)
            return false;

        await _datas.Peek().dialog.backButtonPressed();
        return true;
    }
    public Dialog peek()
    {
        if (isEmpty)
            return null;

        return _datas.Peek().dialog;
    }
}
