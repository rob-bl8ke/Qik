using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynamicTypeDescriptor;
using System.Drawing.Design;
using Dyn = DynamicTypeDescriptor;
using Scm = System.ComponentModel;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;

namespace Qik.LanguageEngine.WinFormsUI
{
    // http://www.codeproject.com/Articles/415070/Dynamic-Type-Description-Framework-for-PropertyGri

    public partial class InputPropertyGrid : UserControl
    {
        // important: required to add dynamic properties to...
        private class UserInputProperties { }

        private const string CATEGORY_USER_INPUT = "1. User Input";
        private const string CATEGORY_EXPRESSION = "2. Expressions";

        private ICompiler compiler;
        private UserInputProperties properties;

        public InputPropertyGrid()
        {
            InitializeComponent();
        }

        public void Reset(ICompiler compiler)
        {
            InitializeCompiler(compiler);
        }

        private void CreateTextBox(ITextField textBox)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);

            Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
                                                        textBox.Symbol,
                                                        typeof(string), textBox.DefaultValue,
                                                        new Scm.BrowsableAttribute(true),
                                                        new Scm.DisplayNameAttribute(textBox.Title),
                                                        new Scm.DescriptionAttribute(CreatePropertyDescription(textBox)),
                                                        new Scm.DefaultValueAttribute(textBox.DefaultValue)
                                                        );
            propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_USER_INPUT), true);
            propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.TextBox), true);

            typeDescriptor.GetProperties().Add(propertyDescriptor);
        }

        private void CreateOptionsBox(IOptionsField optionBox)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);
            Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
                                                        optionBox.Symbol,
                                                        typeof(int), optionBox.SelectedIndex,
                                                        new Scm.BrowsableAttribute(true),
                                                        new Scm.DisplayNameAttribute(optionBox.Title),
                                                        new Scm.DescriptionAttribute(CreatePropertyDescription(optionBox)),
                                                        new Scm.DefaultValueAttribute(optionBox.SelectedIndex)
                                                        );
            propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_USER_INPUT), true);
            propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.OptionBox), true);

            propertyDescriptor.Attributes.Add(new Scm.TypeConverterAttribute(typeof(Dyn.StandardValueConverter)), true);
            propertyDescriptor.Attributes.Add(new Scm.EditorAttribute(typeof(Dyn.StandardValueEditor), typeof(UITypeEditor)), true);

            BuildOptions(propertyDescriptor, optionBox.Options);

            typeDescriptor.GetProperties().Add(propertyDescriptor);
        }

        private void CreateExpression(IExpression expression)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);

            Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
                                                        expression.Symbol,
                                                        typeof(string), expression.Value,
                                                        //typeof(string), null,
                                                        new Scm.BrowsableAttribute(true),
                                                        new Scm.DisplayNameAttribute(expression.Title),
                                                        new Scm.DescriptionAttribute(CreatePropertyDescription(expression)),
                                                        new Scm.DefaultValueAttribute(null),
                                                        new Scm.ReadOnlyAttribute(true)
                                                        );
            propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_EXPRESSION), true);
            propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.ExpressionBox), true);
            
            // If you don't want to raise  an "InputPropertyChanged" event for this property, then don't add a delegate.
            // Also, you'll be able to know exactly what property changed by having different handlers for different property
            // descriptors... good thing to know for the future !!!
            //propertyDescriptor.AddValueChanged(propertyGrid.SelectedObject, new EventHandler(this.InputPropertyChanged));

            typeDescriptor.GetProperties().Add(propertyDescriptor);
        }

        private string CreatePropertyDescription(ISymbol expression)
        {
            if (string.IsNullOrEmpty(expression.Description))
                return string.Format("PLACEHOLDER: {0}\nSYMBOL: {1}", expression.Placeholder, expression.Symbol);
            else
                return string.Format("{0}\n\nPLACEHOLDER: {1}\nSYMBOL: {2}", expression.Description, expression.Placeholder, expression.Symbol);
        }

        private void BuildOptions(Dyn.PropertyDescriptor pd, IOption[] options)
        {
            pd.StandardValues.Clear();

            foreach (IOption option in options)
            {
                Dyn.StandardValue sv = new Dyn.StandardValue(option.Index, option.Title);
                sv.Description = option.Description;
                pd.StandardValues.Add(sv);
            }
        }

        private void InitializeCompiler(ICompiler compiler)
        {
            if (this.compiler != null)
            {
                this.compiler.AfterCompile -= compiler_AfterCompile;
                this.compiler.AfterInput -= compiler_AfterInput;
            }

            this.compiler = compiler;
            this.compiler.AfterCompile += compiler_AfterCompile;
            this.compiler.AfterInput += compiler_AfterInput;
        }

        private void CreateControls()
        {
            properties = new UserInputProperties();
            Dyn.TypeDescriptor.IntallTypeDescriptor(properties);
            propertyGrid.SelectedObject = properties;

            if (compiler.HasErrors)
                return;

            foreach (IInputField field in compiler.InputFields)
            {
                if (field is ITextField)
                    CreateTextBox(field as ITextField);
                else if (field is IOptionsField)
                    CreateOptionsBox(field as IOptionsField);
            }

            foreach (IExpression expression in compiler.Expressions)
            {
                if (expression.IsVisibleToEditor)
                    CreateExpression(expression);
            }

            propertyGrid.Refresh();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            GridItem gridItem = e.ChangedItem;
            string itemSymbol = gridItem.PropertyDescriptor.Name;

            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);
            PropertyDescriptorCollection propertyDescriptors = typeDescriptor.GetProperties();

            Dyn.PropertyDescriptor propertyDescriptor = propertyDescriptors[itemSymbol] as Dyn.PropertyDescriptor;
            PropertyControlAttribute propertyControl = propertyDescriptor.Attributes[typeof(PropertyControlAttribute)] as PropertyControlAttribute;

            if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.TextBox)
            {
                string value = propertyDescriptor.GetValue(this.properties) != null ? propertyDescriptor.GetValue(this.properties).ToString() : null;
                compiler.Input(propertyDescriptor.Name, value);
            }
            else if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.OptionBox)
            {
                compiler.Input(propertyDescriptor.Name, propertyDescriptor.GetValue(this.properties).ToString());
            }
        }

        private void compiler_AfterInput(object sender, EventArgs e)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);
            PropertyDescriptorCollection propertyDescriptors = typeDescriptor.GetProperties();

            foreach (Dyn.PropertyDescriptor propertyDescriptor in propertyDescriptors)
            {
                PropertyControlAttribute propertyControl = propertyDescriptor.Attributes[typeof(PropertyControlAttribute)] as PropertyControlAttribute;
                if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.ExpressionBox)
                {
                    string newValue = compiler.GetValueOfSymbol(propertyDescriptor.Name);
                    propertyDescriptor.SetValue(properties, newValue == null ? string.Empty : newValue);
                }
            }

            propertyGrid.Refresh();
        }

        private void compiler_AfterCompile(object sender, EventArgs e)
        {
            CreateControls();
        }
    }
}
