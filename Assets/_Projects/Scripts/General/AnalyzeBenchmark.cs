using IA;
using System.Text;
using TMPro;
using UnityEngine;
using Profiler = UnityEngine.Profiling.Profiler;

namespace General
{
    public sealed class AnalyzeBenchmark : MonoBehaviour
    {
        [SerializeField, Header("配下のCanvas")]
        private Canvas _canvas;

        [SerializeField, Header("ベンチマークを表示するテキスト")]
        private TextMeshProUGUI _benchmarkText;

        private int _cnt = 0;
        private float _preT = 0f;
        private float _fps = 0f;

        private bool _isEnable = false;

        private void OnEnable()
        {
            if (_canvas == null) return;
            _canvas.gameObject.SetActive(_isEnable);
        }

        private void Update()
        {
            if (InputGetter.Instance.TriggerBenchmarkText.Bool) _isEnable = !_isEnable;
            if (_canvas != null && _canvas.gameObject.activeSelf != _isEnable) _canvas.gameObject.SetActive(_isEnable);
            if (!_isEnable) return;

            _cnt++;
            float t = Time.realtimeSinceStartup - _preT;
            if (t >= 0.5f)
            {
                _fps = _cnt / t;
                _cnt = 0;
                _preT = Time.realtimeSinceStartup;
            }

            UpdateText();
        }

        private void UpdateText()
        {
            if (_benchmarkText == null) return;

            StringBuilder sb = new();
            sb.Append($"FPS: {_fps:F2}\n");
            sb.Append($"Memory(MB): {_allocatedMemory:F2}/{_reservedMemory:F2}");
            sb.Append($" ({_memoryP:P2}, {_unusedReservedMemory:F2} unused)");
            _benchmarkText.text = sb.ToString();
        }

        private float _allocatedMemory => ByteToMegabyte(Profiler.GetTotalAllocatedMemoryLong());
        private float _unusedReservedMemory => ByteToMegabyte(Profiler.GetTotalUnusedReservedMemoryLong());
        private float _reservedMemory => ByteToMegabyte(Profiler.GetTotalReservedMemoryLong());
        private float _memoryP => _allocatedMemory / _reservedMemory;
        private float ByteToMegabyte(long n) => (n >> 10) / 1024f;
    }
}