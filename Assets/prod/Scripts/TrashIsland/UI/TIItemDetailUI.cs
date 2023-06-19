using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore;

namespace TrashIsland
{
    public class TIItemDetailUI : TIUIWindow
    {
        [SerializeField]
        protected Image objectImage;
        [SerializeField]
        protected TextMeshProUGUI objectNameText;
        [SerializeField]
        protected Button action1Button;
        [SerializeField]
        protected TextMeshProUGUI action1ButtonText;

        [SerializeField]
        protected Button action2Button;
        [SerializeField]
        protected TextMeshProUGUI action2ButtonText;
        [SerializeField]
        protected Button action3Button;


        void HandleTISelectionEvent(TISelectionEvent e)
        {
            Debug.Log($"A selection was made: {e.selectableObject.name}");
            TIObjectData objectData = e.selectableObject.objectData;

            if (objectData != null)
            {
                //TODO: There seem to be occasions / frames where the objectImage is null?
                if (objectImage != null)
                {
                    if (objectData.sprite != null)
                    {
                        objectImage.sprite = e.selectableObject.objectData.sprite;
                    }
                }

                if (objectData.name != "")
                {
                    objectNameText.text = e.selectableObject.objectData.name;
                }


                action1Button?.gameObject.SetActive(false);
                action2Button?.gameObject.SetActive(false);
                action3Button?.gameObject.SetActive(false);

                TIInteractableObject interactable = e.selectableObject.GetComponent<TIInteractableObject>();
                if (interactable != null)
                {
                    InteractionDef[] interactiondefs = interactable.GetInteractions(null);
                    if (interactiondefs.Length > 0)
                    {
                        action1Button?.gameObject.SetActive(true);
                        action1ButtonText.text = interactiondefs[0].interactionName;
                    }
                    if (interactiondefs.Length > 1)
                    {
                        action2Button?.gameObject.SetActive(true);
                        action2ButtonText.text = interactiondefs[1].interactionName;
                    }
                    if (objectData.type == TIObjectType.TRASH)
                    {
                        action3Button?.gameObject.SetActive(true);
                    }
                    else
                    {
                        action3Button?.gameObject.SetActive(false);
                    }
                }

            }
        }

        public override void Refresh()
        {
            base.Refresh();
            Debug.Log("Item detail window is refreshing.");
        }

        public override void Show()
        {
            Refresh();
            base.Show();
        }
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Debug.Log("ITEM DETAIL UI REGISTERED");
            TISelectionEvent.Register(HandleTISelectionEvent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TISelectionEvent.Unregister(HandleTISelectionEvent);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}