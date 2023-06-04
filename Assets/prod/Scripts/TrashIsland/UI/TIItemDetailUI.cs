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
            if (e.selectableObject.objectSettings != null)
            {
                objectImage.sprite = e.selectableObject.objectSettings.objectSprite;
                objectNameText.text = e.selectableObject.objectSettings.objectName;
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