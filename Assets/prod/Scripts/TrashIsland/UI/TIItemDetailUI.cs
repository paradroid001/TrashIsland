using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    public class TIItemDetailUI : TIUIWindow
    {
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
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}