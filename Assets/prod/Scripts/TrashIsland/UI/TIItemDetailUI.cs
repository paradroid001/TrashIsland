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

        TIInteractableObject interactableObject;
        TIInteractionDef interaction1;
        TIInteractionDef interaction2;
        TIInteractionDef interaction3;



        void HandleTISelectionEvent(TISelectionEvent e)
        {
            interaction1 = null;
            interaction2 = null;
            interaction3 = null;
            interactableObject = null;

            Debug.Log($"A selection was made: {e.selectableObject.name}");
            TIObjectData objectData = e.selectableObject.objectData;

            if (objectData != null)
            {
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
                    interactableObject = interactable;

                    InteractionDef[] interactiondefs = interactable.GetInteractions(null);
                    if (interactiondefs.Length > 0)
                    {
                        action1Button?.gameObject.SetActive(true);
                        action1ButtonText.text = interactiondefs[0].interactionName;
                        interaction1 = (TIInteractionDef)interactiondefs[0];
                    }
                    if (interactiondefs.Length > 1)
                    {
                        action2Button?.gameObject.SetActive(true);
                        action2ButtonText.text = interactiondefs[1].interactionName;
                        interaction2 = (TIInteractionDef)interactiondefs[1];
                    }
                    //recipes button
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

        public void Action1()
        {
            if (interaction1 != null)
            {
                TIInteractor player = GameObject.FindObjectOfType<TIInteractor>();
                if (player != null)
                    interaction1.interaction.Interact(player);
            }
        }
        public void Action2()
        {
            if (interaction2 != null)
            {
                TIInteractor player = GameObject.FindObjectOfType<TIInteractor>();
                if (player != null)
                    interaction2.interaction.Interact(player);
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