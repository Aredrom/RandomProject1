namespace RandomProject1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxContinent = new ComboBox();
            numericUpDown1 = new NumericUpDown();
            GetCountriesBtn = new Button();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // comboBoxContinent
            // 
            comboBoxContinent.FormattingEnabled = true;
            comboBoxContinent.Location = new Point(12, 12);
            comboBoxContinent.Name = "comboBoxContinent";
            comboBoxContinent.Size = new Size(121, 23);
            comboBoxContinent.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(13, 41);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 1;
            // 
            // GetCountriesBtn
            // 
            GetCountriesBtn.Location = new Point(58, 70);
            GetCountriesBtn.Name = "GetCountriesBtn";
            GetCountriesBtn.Size = new Size(75, 23);
            GetCountriesBtn.TabIndex = 2;
            GetCountriesBtn.Text = "Show";
            GetCountriesBtn.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(223, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(565, 424);
            listBox1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Controls.Add(GetCountriesBtn);
            Controls.Add(numericUpDown1);
            Controls.Add(comboBoxContinent);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBoxContinent;
        private NumericUpDown numericUpDown1;
        private Button GetCountriesBtn;
        private ListBox listBox1;
    }
}