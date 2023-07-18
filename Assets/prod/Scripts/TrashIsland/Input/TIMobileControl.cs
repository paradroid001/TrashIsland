using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

        private TISelectableObject objectUnderTouch;
        float objectHeldTime = 0;

        private TIInteractor thisInteractor;

        public override void InitService()
        {
            base.InitService();
            Debug.Log("Starting up TIMobileControl service");
            MonoMobileTapInput.OnTap += OnTap;
            MonoMobileTapInput.OnTapHeld += OnTapHeld;
        }
        public override void ShutdownService()
        {
            base.ShutdownService();
            Debug.Log("Shutting down TIMobileControl service");
            MonoMobileInput.OnTap -= OnTap;
            MonoMobileTapInput.OnTapHeld -= OnTapHeld;
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
            thisInteractor = GetComponent<TIInteractor>();
            _movement = GetComponent<TICharacterMovement>();
            Debug.Log($"This interactor is {thisInteractor}");
        }


        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        void OnTap(Vector2 tappos)
        {
            Debug.Log("ONTAP");
            //Ignore if touch is over UI
            if (IsTouchOverUI(tappos))
            {
                return;
            }

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

        void OnTapHeld(SwipeData data)
        {
            //Ignore if touch is over UI
            if (IsTouchOverUI(data.posStart))
            {
                return;
            }

            Debug.Log($"This gameobject is {gameObject}");
            Debug.Log($"This interactor is {thisInteractor}");
            Vector3 dest = GetTouchPositionInWorld(data.posCurrent, false); //don't allow selection
            if (dest != Vector3.zero)
            {
                _movement.currentDestination = dest;
                if (oldClickDestination != null)
                {
                    Destroy(oldClickDestination);
                }
                oldClickDestination = Instantiate(clickDestinationPrefab, dest, Quaternion.identity);
            }
            else //you're holding on some kind of object
            {
                if (objectHeldTime > 1.0f)
                {
                    TIInteractableObject interactableObject = objectUnderTouch.GetComponent<TIInteractableObject>();
                    if (interactableObject != null && thisInteractor != null)
                    {
                        //TODO: interact using this object as interactor, first interaction.
                        if (interactableObject.IsSelected()
                            //&& interactableObject.IsInteractable()
                            && interactableObject.GetInteractionState(thisInteractor, 0) == InteractionState.IDLE)
                        {
                            interactableObject.Interact(thisInteractor, 0);
                        }

                    }
                    else
                    {
                        Debug.Log("Interactable or interactor was null");
                    }
                }
            }
        }

        bool IsTouchOverUI(Vector2 pos)
        {
            bool overUI = false;
            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    
            //    return;
            //}
            PointerEventData eventPosition = new PointerEventData(EventSystem.current);
            eventPosition.position = pos;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventPosition, results);
            if (results.Count > 0)
            {
                overUI = true;
                Debug.Log("Touch was over UI");
            }
            return overUI;
        }

        Vector3 GetTouchPositionInWorld(Vector2 touch, bool allowSelection = true)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, 9, QueryTriggerInteraction.Ignore))
            {
                //get the game object up the chain (parent rigidbody for example)
                GameObject g;
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb == null)
                    g = hit.collider.gameObject;
                else
                    g = rb.gameObject;
                //We still check for a selectable, because we don't want to
                //return hit positions on selectables.
                TISelectableObject o = g.GetComponent<TISelectableObject>();

                if (o != null)
                {
                    if (o != objectUnderTouch || objectUnderTouch == null)
                    {
                        objectUnderTouch = o;
                        objectHeldTime = 0;
                    }
                    else
                    {
                        objectHeldTime += Time.deltaTime;
                    }

                    //only send the event if allowing selection
                    if (allowSelection)
                    {
                        TISelectionEvent e = new TISelectionEvent();
                        e.selectableObject = o;
                        e.Call();
                    }
                }
                else //it wasn't a selectable.
                {
                    objectHeldTime = 0;
                    objectUnderTouch = null;
                    Debug.Log("Set object under touch to null");
                    return hit.point;
                }
            }

            return Vector3.zero;
        }
    }
}