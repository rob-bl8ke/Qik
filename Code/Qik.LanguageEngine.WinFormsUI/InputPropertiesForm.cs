using Alsing.SourceCode;
using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Qik.LanguageEngine.WinFormsUI
{
    public partial class InputPropertiesForm : Form
    {
        private ICompiler compiler = new Compiler();
        private Row selectedRow;

        public InputPropertiesForm()
        {
            InitializeComponent();

            compiler = new Compiler();
            compiler.BeforeCompile += compiler_BeforeCompile;
            compiler.AfterCompile += compiler_AfterCompile;
            compiler.AfterInput += compiler_AfterInput;
            compiler.CompileError += compiler_CompileError;
            syntaxBox.RowClick += syntaxBox_RowClick;

            syntaxBox.Document.SyntaxFile = "qiktemplate.syn";
            syntaxBox.Document.Text = File.ReadAllText("Example.txt");

            blueprintSyntaxBox.Document.SyntaxFile = "qikblueprint.syn";
            blueprintSyntaxBox.Document.Text = File.ReadAllText("BlueprintFile.txt");
            outputSyntaxBox.Document.SyntaxFile = "qikblueprint.syn";

            inputPropertyGrid.Reset(compiler);

            AddTemplateTab();
            ExecuteScript();
            //tabControlFile.TabPages.RemoveByKey("templateTabPage"); // the key can be the template file name !!!
        }

        private void compiler_CompileError(object sender, CompileErrorEventArgs e)
        {
            AddErrorLine(e.Line, e.Column, e.Message, e.Location, e.OffendingSymbol);
            if (e.Line > 0)
                AddBookmark(e.Line, e.Column, e.Message, e.Location, e.OffendingSymbol);
        }

        private void syntaxBox_RowClick(object sender, Alsing.Windows.Forms.SyntaxBox.RowMouseEventArgs e)
        {
            DeselectRow();
        }

        private void AddErrorLine(int line, int column, string message, string ruleStack, string symbol)
        {
            ListViewItem item = new ListViewItem();
            item.Text = line.ToString();
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, column.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, message));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ruleStack));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, symbol));

            errorListView.Items.Add(item);
        }

        private void AddTemplateTab()
        {
            //TabPage tabPage = new TabPage("My New Tab Page");
            //tabPage.Name = "templateTabPage";
            //TemplateControl templateCtrl = new TemplateControl();
            //tabPage.Controls.Add(templateCtrl);

            //templateCtrl.Dock = DockStyle.Fill;
            //tabControlFile.TabPages.Add(tabPage);
        }

        private void compiler_AfterInput(object sender, EventArgs e)
        {
            UpdateOutputDocument();
            UpdateAutoList();
        }

        private void compiler_BeforeCompile(object sender, EventArgs e)
        {
            DeselectRow();
            syntaxBox.Document.ClearBookmarks();
            errorListView.Items.Clear();
        }

        private void compiler_AfterCompile(object sender, EventArgs e)
        {
            UpdateOutputDocument();
            UpdateAutoList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ExecuteScript();
        }

        private void ExecuteScript()
        {
            compiler.Compile(syntaxBox.Document.Text);
        }

        private void btnDisplaySymbolTable_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string symbol in compiler.Symbols)
            {
                builder.AppendLine(string.Format("{0} = \"{1}\"", symbol, compiler.GetValueOfSymbol(symbol)));
            }

            MessageBox.Show(builder.ToString());
        }

        private void tabControlFile_Selected(object sender, TabControlEventArgs e)
        {
            // e.Action == TabControlAction.Selected
            if (e.TabPage.Name == "tabOutput")
            {
                UpdateOutputDocument();
                UpdateAutoList();
            }
        }

        private void UpdateOutputDocument()
        {
            string input = blueprintSyntaxBox.Document.Text;
            foreach (string placeholder in compiler.Placeholders)
            {
                string output = compiler.GetValueOfPlaceholder(placeholder);
                input = input.Replace(placeholder, output);
            }

            outputSyntaxBox.Document.Text = input;
        }

        private void UpdateAutoList()
        {
            blueprintSyntaxBox.AutoListClear();

            foreach (string placeholder in compiler.Placeholders)
            {
                ISymbolInfo symbolInfo = compiler.GetPlaceholderInfo(placeholder);
                string title = compiler.GetTitleOfPlaceholder(placeholder);
                string itemText = string.Format("{0} ({1})", symbolInfo.Title, symbolInfo.Placeholder);
                string toolTip = string.Format("{0}\n{1}", itemText, WrapWords(symbolInfo.Description, 150));
                
                blueprintSyntaxBox.AutoListAdd(itemText, placeholder, toolTip, 0);
            }
        }

        private string WrapWords(string text, int interval)
        {
            return WordWrapper.WordWrap(text, interval);
        }

        private void blueprintSyntaxBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.blueprintSyntaxBox.ReadOnly)
            {
                if (e.KeyData == (Keys.Shift | Keys.F8) || e.KeyData == Keys.F8)
                {
                    this.blueprintSyntaxBox.AutoListPosition = new TextPoint(blueprintSyntaxBox.Caret.Position.X, blueprintSyntaxBox.Caret.Position.Y);
                    this.blueprintSyntaxBox.AutoListVisible = true;
                }
            }
        }

        private void errorListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedRow != null)
                selectedRow.BackColor = Color.White;

            if (errorListView.SelectedItems.Count > 0)
            {
                ListViewItem item = errorListView.SelectedItems[0];
                int lineNumber = int.Parse(item.Text);
                SelectRow(lineNumber);
            }
        }

        private void errorListView_Leave(object sender, EventArgs e)
        {
            DeselectRow();
        }

        private void SelectRow(int line)
        {
            Row row = RowFromLine(line);
            if (row != null)
            {
                syntaxBox.GotoLine(line);
                row.BackColor = Color.Gray;
                selectedRow = row;
            }
            else
                selectedRow = null;
        }

        private void DeselectRow()
        {
            if (selectedRow != null)
            {
                selectedRow.BackColor = Color.White;
                this.selectedRow = null;
            }
        }

        private Row RowFromLine(int line)
        {
            int index = line > 1 ? line - 1 : line;
            Row row = syntaxBox.Document[index];
            return row;
        }

        private void AddBookmark(int line, int column, string message, string ruleStack, string symbol)
        {
            Row row = RowFromLine(line);
            if (row != null)
                row.Bookmarked = true;
        }

        //private void btnInfoTip_Click(object sender, EventArgs e)
        //{
        //    blueprintSyntaxBox.InfoTipText = "Hello <b>World</b>";
        //    blueprintSyntaxBox.InfoTipCount = 0;
        //    blueprintSyntaxBox.InfoTipVisible = true;
        //}
    }
}
