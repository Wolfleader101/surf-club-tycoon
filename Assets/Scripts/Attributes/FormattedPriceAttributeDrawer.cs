using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Attributes
{
    public class FormattedPriceAttributeDrawer : OdinAttributeDrawer<FormattedPriceAttribute, float>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var price = "$" + this.ValueEntry.SmartValue.ToString("#,##0");
            SirenixEditorGUI.Title($"Price ({price})", "", TextAlignment.Left, false);
            this.CallNextDrawer(label);

        }
    }
}