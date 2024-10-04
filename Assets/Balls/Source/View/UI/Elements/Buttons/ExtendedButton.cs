using System;
using UnityEngine.UI;

namespace Balls.Source.View.UI.Elements.Buttons
{
    public class ExtendedButton : Button
    {
        public event Action<ButtonState> StateChanged;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            ButtonState mapState = ButtonState.Normal;

            switch (state)
            {
                case SelectionState.Normal:
                    mapState = ButtonState.Normal;
                    break;
                case SelectionState.Highlighted:
                    mapState = ButtonState.Highlighted;
                    break;
                case SelectionState.Pressed:
                    mapState = ButtonState.Pressed;
                    break;
                case SelectionState.Selected:
                    mapState = ButtonState.Selected;
                    break;
                case SelectionState.Disabled:
                    mapState = ButtonState.Disabled;
                    break;
            }

            StateChanged?.Invoke(mapState);
        }
    }
}
