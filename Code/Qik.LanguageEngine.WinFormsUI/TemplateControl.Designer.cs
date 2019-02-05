namespace Qik.LanguageEngine.WinFormsUI
{
    partial class TemplateControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControlFile = new System.Windows.Forms.TabControl();
            this.tabBlueprint = new System.Windows.Forms.TabPage();
            this.blueprintSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.outputSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.tabControlFile.SuspendLayout();
            this.tabBlueprint.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlFile
            // 
            this.tabControlFile.Controls.Add(this.tabBlueprint);
            this.tabControlFile.Controls.Add(this.tabOutput);
            this.tabControlFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFile.Location = new System.Drawing.Point(0, 0);
            this.tabControlFile.Name = "tabControlFile";
            this.tabControlFile.SelectedIndex = 0;
            this.tabControlFile.Size = new System.Drawing.Size(836, 503);
            this.tabControlFile.TabIndex = 4;
            // 
            // tabBlueprint
            // 
            this.tabBlueprint.Controls.Add(this.blueprintSyntaxBox);
            this.tabBlueprint.Location = new System.Drawing.Point(4, 22);
            this.tabBlueprint.Name = "tabBlueprint";
            this.tabBlueprint.Padding = new System.Windows.Forms.Padding(3);
            this.tabBlueprint.Size = new System.Drawing.Size(828, 477);
            this.tabBlueprint.TabIndex = 0;
            this.tabBlueprint.Text = "Blueprint";
            this.tabBlueprint.UseVisualStyleBackColor = true;
            // 
            // blueprintSyntaxBox
            // 
            this.blueprintSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.blueprintSyntaxBox.AutoListPosition = null;
            this.blueprintSyntaxBox.AutoListSelectedText = "a123";
            this.blueprintSyntaxBox.AutoListVisible = false;
            this.blueprintSyntaxBox.BackColor = System.Drawing.Color.White;
            this.blueprintSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.blueprintSyntaxBox.CopyAsRTF = false;
            this.blueprintSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintSyntaxBox.FontName = "Courier new";
            this.blueprintSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.blueprintSyntaxBox.InfoTipCount = 1;
            this.blueprintSyntaxBox.InfoTipPosition = null;
            this.blueprintSyntaxBox.InfoTipSelectedIndex = 1;
            this.blueprintSyntaxBox.InfoTipVisible = false;
            this.blueprintSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.blueprintSyntaxBox.LockCursorUpdate = false;
            this.blueprintSyntaxBox.Name = "blueprintSyntaxBox";
            this.blueprintSyntaxBox.ShowScopeIndicator = false;
            this.blueprintSyntaxBox.Size = new System.Drawing.Size(822, 471);
            this.blueprintSyntaxBox.SmoothScroll = false;
            this.blueprintSyntaxBox.SplitviewH = -4;
            this.blueprintSyntaxBox.SplitviewV = -4;
            this.blueprintSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.blueprintSyntaxBox.TabIndex = 0;
            this.blueprintSyntaxBox.Text = "syntaxBoxControl1";
            this.blueprintSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.outputSyntaxBox);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(828, 477);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // outputSyntaxBox
            // 
            this.outputSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.outputSyntaxBox.AutoListPosition = null;
            this.outputSyntaxBox.AutoListSelectedText = "a123";
            this.outputSyntaxBox.AutoListVisible = false;
            this.outputSyntaxBox.BackColor = System.Drawing.Color.White;
            this.outputSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.outputSyntaxBox.CopyAsRTF = false;
            this.outputSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputSyntaxBox.FontName = "Courier new";
            this.outputSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.outputSyntaxBox.InfoTipCount = 1;
            this.outputSyntaxBox.InfoTipPosition = null;
            this.outputSyntaxBox.InfoTipSelectedIndex = 1;
            this.outputSyntaxBox.InfoTipVisible = false;
            this.outputSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.outputSyntaxBox.LockCursorUpdate = false;
            this.outputSyntaxBox.Name = "outputSyntaxBox";
            this.outputSyntaxBox.ShowScopeIndicator = false;
            this.outputSyntaxBox.Size = new System.Drawing.Size(822, 471);
            this.outputSyntaxBox.SmoothScroll = false;
            this.outputSyntaxBox.SplitviewH = -4;
            this.outputSyntaxBox.SplitviewV = -4;
            this.outputSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.outputSyntaxBox.TabIndex = 1;
            this.outputSyntaxBox.Text = "syntaxBoxControl2";
            this.outputSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // TemplateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlFile);
            this.Name = "TemplateControl";
            this.Size = new System.Drawing.Size(836, 503);
            this.tabControlFile.ResumeLayout(false);
            this.tabBlueprint.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlFile;
        private System.Windows.Forms.TabPage tabBlueprint;
        private Alsing.Windows.Forms.SyntaxBoxControl blueprintSyntaxBox;
        private System.Windows.Forms.TabPage tabOutput;
        private Alsing.Windows.Forms.SyntaxBoxControl outputSyntaxBox;
    }
}
