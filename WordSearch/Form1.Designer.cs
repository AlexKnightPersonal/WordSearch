namespace WordSearch
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtWords = new System.Windows.Forms.RichTextBox();
            this.lblFindWords = new System.Windows.Forms.Label();
            this.btnRemoveWord = new System.Windows.Forms.Button();
            this.btnAddWord = new System.Windows.Forms.Button();
            this.lblRemoveWord = new System.Windows.Forms.Label();
            this.lblAddWord = new System.Windows.Forms.Label();
            this.txtRemoveWord = new System.Windows.Forms.TextBox();
            this.txtAddWord = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtWords);
            this.groupBox1.Controls.Add(this.lblFindWords);
            this.groupBox1.Controls.Add(this.btnRemoveWord);
            this.groupBox1.Controls.Add(this.btnAddWord);
            this.groupBox1.Controls.Add(this.lblRemoveWord);
            this.groupBox1.Controls.Add(this.lblAddWord);
            this.groupBox1.Controls.Add(this.txtRemoveWord);
            this.groupBox1.Controls.Add(this.txtAddWord);
            this.groupBox1.Controls.Add(this.lblSize);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.txtSize);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 490);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grid Settings";
            // 
            // rtxtWords
            // 
            this.rtxtWords.Location = new System.Drawing.Point(6, 222);
            this.rtxtWords.Name = "rtxtWords";
            this.rtxtWords.ReadOnly = true;
            this.rtxtWords.Size = new System.Drawing.Size(100, 190);
            this.rtxtWords.TabIndex = 10;
            this.rtxtWords.Text = "";
            // 
            // lblFindWords
            // 
            this.lblFindWords.AutoSize = true;
            this.lblFindWords.Location = new System.Drawing.Point(9, 206);
            this.lblFindWords.Name = "lblFindWords";
            this.lblFindWords.Size = new System.Drawing.Size(90, 13);
            this.lblFindWords.TabIndex = 9;
            this.lblFindWords.Text = "Find these words:";
            // 
            // btnRemoveWord
            // 
            this.btnRemoveWord.Location = new System.Drawing.Point(113, 138);
            this.btnRemoveWord.Name = "btnRemoveWord";
            this.btnRemoveWord.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveWord.TabIndex = 8;
            this.btnRemoveWord.Text = "Remove";
            this.btnRemoveWord.UseVisualStyleBackColor = true;
            this.btnRemoveWord.Click += new System.EventHandler(this.btnRemoveWord_Click);
            // 
            // btnAddWord
            // 
            this.btnAddWord.Location = new System.Drawing.Point(113, 94);
            this.btnAddWord.Name = "btnAddWord";
            this.btnAddWord.Size = new System.Drawing.Size(75, 23);
            this.btnAddWord.TabIndex = 7;
            this.btnAddWord.Text = "Add";
            this.btnAddWord.UseVisualStyleBackColor = true;
            this.btnAddWord.Click += new System.EventHandler(this.btnAddWord_Click);
            // 
            // lblRemoveWord
            // 
            this.lblRemoveWord.AutoSize = true;
            this.lblRemoveWord.Location = new System.Drawing.Point(6, 124);
            this.lblRemoveWord.Name = "lblRemoveWord";
            this.lblRemoveWord.Size = new System.Drawing.Size(82, 13);
            this.lblRemoveWord.TabIndex = 6;
            this.lblRemoveWord.Text = "Remove word...";
            // 
            // lblAddWord
            // 
            this.lblAddWord.AutoSize = true;
            this.lblAddWord.Location = new System.Drawing.Point(6, 77);
            this.lblAddWord.Name = "lblAddWord";
            this.lblAddWord.Size = new System.Drawing.Size(61, 13);
            this.lblAddWord.TabIndex = 5;
            this.lblAddWord.Text = "Add word...";
            // 
            // txtRemoveWord
            // 
            this.txtRemoveWord.Location = new System.Drawing.Point(6, 140);
            this.txtRemoveWord.Name = "txtRemoveWord";
            this.txtRemoveWord.Size = new System.Drawing.Size(100, 20);
            this.txtRemoveWord.TabIndex = 4;
            this.txtRemoveWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoveWord_KeyPress);
            // 
            // txtAddWord
            // 
            this.txtAddWord.Location = new System.Drawing.Point(6, 96);
            this.txtAddWord.Name = "txtAddWord";
            this.txtAddWord.Size = new System.Drawing.Size(100, 20);
            this.txtAddWord.TabIndex = 3;
            this.txtAddWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddWord_KeyPress);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(7, 20);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(56, 13);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "Enter size:";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(113, 36);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(6, 38);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(100, 20);
            this.txtSize.TabIndex = 0;
            this.txtSize.Text = "20";
            this.txtSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSize_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(210, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(580, 490);
            this.dataGridView1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 490);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Word Search";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Button btnRemoveWord;
        private System.Windows.Forms.Button btnAddWord;
        private System.Windows.Forms.Label lblRemoveWord;
        private System.Windows.Forms.Label lblAddWord;
        private System.Windows.Forms.TextBox txtRemoveWord;
        private System.Windows.Forms.TextBox txtAddWord;
        private System.Windows.Forms.RichTextBox rtxtWords;
        private System.Windows.Forms.Label lblFindWords;
    }
}

