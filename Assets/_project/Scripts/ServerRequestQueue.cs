using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WeatherDogs
{
    public class ServerRequestQueue
    {
        private class RequestWrapper
        {
            public Func<CancellationToken, UniTask> TaskFunc;
            public CancellationTokenSource CTS;
        }

        private readonly Queue<RequestWrapper> _queue = new();
        private bool _isProcessing;
        private CancellationTokenSource _loopCts;

        public void Start()
        {
            _loopCts = new CancellationTokenSource();
            ProcessLoop(_loopCts.Token).Forget();
        }

        public void Stop()
        {
            _loopCts?.Cancel();
            _queue.Clear();
        }

        public void Enqueue(Func<CancellationToken, UniTask> taskFunc)
        {
            var wrapper = new RequestWrapper
            {
                CTS = new CancellationTokenSource(),
                TaskFunc = taskFunc
            };
            _queue.Enqueue(wrapper);
        }

        private async UniTaskVoid ProcessLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_isProcessing || _queue.Count == 0)
                {
                    await UniTask.Yield();
                    continue;
                }

                var request = _queue.Dequeue();
                _isProcessing = true;

                try
                {
                    await request.TaskFunc(request.CTS.Token);
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("Запрос отменён.");
                }
                catch (Exception e)
                {
                    Debug.LogError("Ошибка в запросе: " + e.Message);
                }

                _isProcessing = false;
            }
        }
    }
}


