using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrashIsland
{
    public class TIItemDetailUI : TIUIWindow
    {
        [SerializeField]
        protected Image objectImage;
        [SerializeField]
        protected TextMeshProUGUI objectNameText;

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
            TISelectionEvent.Register(HandleTISelectionEvent);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}