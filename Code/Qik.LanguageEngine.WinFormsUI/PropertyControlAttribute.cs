using System;

namespace Qik.LanguageEngine.WinFormsUI
{
    public enum ControlTypeEnum
    {
        TextBox,
        //CheckBox,
        OptionBox,
        ExpressionBox
    }

    public class PropertyControlAttribute : Attribute
    {
        public ControlTypeEnum ControlType { get; private set; }
        public PropertyControlAttribute(ControlTypeEnum controlType)
        {
            this.ControlType = controlType;
        }
    }
}
