using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TrashIsland
{
    public class TIFPSCounter
    {
        float _timer = 0;
        int _frameCount = 0;
        float _fps = 0;

        public float fps
        {
            get {return _fps;}
        }

        public void Reset()
        {
            _timer = 0;
            _frameCount = 0;
        }

        public float AddFrame(float dt)
        {
            _timer += dt;
            _frameCount += 1;
            if (_timer > 0)
                _fps = _frameCount / _timer;
            if (_timer > 3)
            {
                _timer = 0;
                _frameCount = 0;
            }
            return _fps;
        }
    }

    public class TIDebugView : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        TIFPSCounter fpsCounter = new TIFPSCounter();
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            fpsText.text = $"FPS: {Mathf.Round(fpsCounter.AddFrame(Time.deltaTime))}";
        }
    }
}