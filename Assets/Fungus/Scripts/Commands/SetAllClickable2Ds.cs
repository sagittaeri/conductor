// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Sets a Clickable2D component to be clickable / non-clickable.
    /// </summary>
    [CommandInfo("Sprite", 
                 "Set All Clickable 2Ds", 
                 "Sets all Clickable2D components to be clickable / non-clickable.")]
    [AddComponentMenu("")]
    public class SetAllClickable2Ds : Command
    {       
        [Tooltip("Set to true to enable the components")]
        [SerializeField] protected BooleanData activeState;

        #region Public members

        public override void OnEnter()  
        {
            var clickables = GameObject.FindObjectsOfType<Clickable2D>();

            foreach (var clickable in clickables) {
                clickable.ClickEnabled = activeState.Value;
            }
            
            Continue();
        }
        
        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255); 
        }

        #endregion
    }
        
}