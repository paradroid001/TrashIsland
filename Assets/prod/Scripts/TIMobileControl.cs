using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class TIMobileControl : TIPlayerInputService
    {
        protected TICharacterMovement _movement;
        [SerializeField]
        protected MonoMobileTapInput tapInput;
        public GameObject clickDestinationPrefab;
        private GameObject oldClickDestination = null;

        public override void InitService()
        {
            base.InitService();
            MonoMobileInput.OnTap += OnTap;
        }
        public override void ShutdownService()
        {
            base.ShutdownService();
            Debug.Log("Shutting down TIMobileControl service");
            MonoMobileInput.OnTap -= OnTap;
        }

        public override void StartCollecting()
        {
            MonoMobileInput.controlsEnabled = true;
            base.StartCollecting();
        }
        public override void StopCollecting()
        {
            MonoMobileInput.controlsEnabled = false;
            base.StopCollecting();
        }

        protected override void Start()
        {
            base.Start();
            _movement = GetComponent<TICharacterMovement>();
        }


        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        void OnTap(Vector2 tappos)
        {
            //Debug.Log("Tapped");
            Vector3 dest = GetTouchPositionInWorld(tappos);
            if (dest != Vector3.zero)
            {
                _movement.currentDestination = dest;
                if (oldClickDestination != null)
                {
                    Destroy(oldClickDestination);
                }
                oldClickDestination = Instantiate(clickDestinationPrefab, dest, Quaternion.identity);
            }

        }

        Vector3 GetTouchPositionInWorld(Vector2 touch)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}