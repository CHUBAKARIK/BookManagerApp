namespace BookManagerApp.WinFormsUI
{
    partial class MainForm
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
            cmbEntityType = new ComboBox();
            dataGridView1 = new DataGridView();
            abilityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnBusiness1 = new Button();
            btnBusiness2 = new Button();
            txtField1 = new TextBox();
            txtField2 = new TextBox();
            txtField3 = new TextBox();
            txtField4 = new TextBox();
            txtField5 = new TextBox();
            lblField1 = new Label();
            lblField2 = new Label();
            lblField3 = new Label();
            lblField4 = new Label();
            lblField5 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbEntityType
            // 
            cmbEntityType.FormattingEnabled = true;
            cmbEntityType.Location = new Point(16, 18);
            cmbEntityType.Margin = new Padding(4, 5, 4, 5);
            cmbEntityType.Name = "cmbEntityType";
            cmbEntityType.Size = new Size(265, 28);
            cmbEntityType.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { abilityDataGridViewTextBoxColumn });
            dataGridView1.Location = new Point(16, 60);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(800, 308);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // abilityDataGridViewTextBoxColumn
            // 
            abilityDataGridViewTextBoxColumn.DataPropertyName = "AbilitiesOfTheBook";
            abilityDataGridViewTextBoxColumn.HeaderText = "Способность";
            abilityDataGridViewTextBoxColumn.MinimumWidth = 6;
            abilityDataGridViewTextBoxColumn.Name = "abilityDataGridViewTextBoxColumn";
            abilityDataGridViewTextBoxColumn.Width = 125;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(8, 200);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(116, 200);
            btnUpdate.Margin = new Padding(4, 5, 4, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 35);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(224, 200);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnBusiness1
            // 
            btnBusiness1.Location = new Point(332, 200);
            btnBusiness1.Margin = new Padding(4, 5, 4, 5);
            btnBusiness1.Name = "btnBusiness1";
            btnBusiness1.Size = new Size(109, 35);
            btnBusiness1.TabIndex = 5;
            btnBusiness1.Text = "Группировка";
            btnBusiness1.UseVisualStyleBackColor = true;
            // 
            // btnBusiness2
            // 
            btnBusiness2.Location = new Point(440, 200);
            btnBusiness2.Margin = new Padding(4, 5, 4, 5);
            btnBusiness2.Name = "btnBusiness2";
            btnBusiness2.Size = new Size(226, 35);
            btnBusiness2.TabIndex = 6;
            btnBusiness2.Text = "Поиск по году/очкам силы";
            btnBusiness2.UseVisualStyleBackColor = true;
            // 
            // txtField1
            // 
            txtField1.Location = new Point(116, 29);
            txtField1.Margin = new Padding(4, 5, 4, 5);
            txtField1.Name = "txtField1";
            txtField1.Size = new Size(265, 27);
            txtField1.TabIndex = 7;
            // 
            // txtField2
            // 
            txtField2.Location = new Point(116, 69);
            txtField2.Margin = new Padding(4, 5, 4, 5);
            txtField2.Name = "txtField2";
            txtField2.Size = new Size(265, 27);
            txtField2.TabIndex = 8;
            // 
            // txtField3
            // 
            txtField3.Location = new Point(116, 109);
            txtField3.Margin = new Padding(4, 5, 4, 5);
            txtField3.Name = "txtField3";
            txtField3.Size = new Size(265, 27);
            txtField3.TabIndex = 9;
            // 
            // txtField4
            // 
            txtField4.Location = new Point(116, 149);
            txtField4.Margin = new Padding(4, 5, 4, 5);
            txtField4.Name = "txtField4";
            txtField4.Size = new Size(265, 27);
            txtField4.TabIndex = 10;
            // 
            // txtField5
            // 
            txtField5.Location = new Point(485, 67);
            txtField5.Margin = new Padding(4, 5, 4, 5);
            txtField5.Name = "txtField5";
            txtField5.Size = new Size(265, 27);
            txtField5.TabIndex = 11;
            // 
            // lblField1
            // 
            lblField1.AutoSize = true;
            lblField1.Location = new Point(8, 34);
            lblField1.Margin = new Padding(4, 0, 4, 0);
            lblField1.Name = "lblField1";
            lblField1.Size = new Size(80, 20);
            lblField1.TabIndex = 12;
            lblField1.Text = "Название:";
            // 
            // lblField2
            // 
            lblField2.AutoSize = true;
            lblField2.Location = new Point(8, 74);
            lblField2.Margin = new Padding(4, 0, 4, 0);
            lblField2.Name = "lblField2";
            lblField2.Size = new Size(54, 20);
            lblField2.TabIndex = 13;
            lblField2.Text = "Автор:";
            // 
            // lblField3
            // 
            lblField3.AutoSize = true;
            lblField3.Location = new Point(8, 114);
            lblField3.Margin = new Padding(4, 0, 4, 0);
            lblField3.Name = "lblField3";
            lblField3.Size = new Size(103, 20);
            lblField3.TabIndex = 14;
            lblField3.Text = "Способность:";
            // 
            // lblField4
            // 
            lblField4.AutoSize = true;
            lblField4.Location = new Point(8, 154);
            lblField4.Margin = new Padding(4, 0, 4, 0);
            lblField4.Name = "lblField4";
            lblField4.Size = new Size(36, 20);
            lblField4.TabIndex = 15;
            lblField4.Text = "Год:";
            // 
            // lblField5
            // 
            lblField5.AutoSize = true;
            lblField5.Location = new Point(423, 36);
            lblField5.Margin = new Padding(4, 0, 4, 0);
            lblField5.Name = "lblField5";
            lblField5.Size = new Size(74, 20);
            lblField5.TabIndex = 16;
            lblField5.Text = "Команда:";
            lblField5.Click += lblField5_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblField1);
            groupBox1.Controls.Add(lblField5);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(lblField4);
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(lblField3);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(lblField2);
            groupBox1.Controls.Add(btnBusiness1);
            groupBox1.Controls.Add(txtField1);
            groupBox1.Controls.Add(btnBusiness2);
            groupBox1.Controls.Add(txtField2);
            groupBox1.Controls.Add(txtField5);
            groupBox1.Controls.Add(txtField3);
            groupBox1.Controls.Add(txtField4);
            groupBox1.Location = new Point(16, 377);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(800, 254);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Управление";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 649);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(cmbEntityType);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "BookManager App";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEntityType;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBusiness1;
        private System.Windows.Forms.Button btnBusiness2;
        private System.Windows.Forms.TextBox txtField1;
        private System.Windows.Forms.TextBox txtField2;
        private System.Windows.Forms.TextBox txtField3;
        private System.Windows.Forms.TextBox txtField4;
        private System.Windows.Forms.TextBox txtField5;
        private System.Windows.Forms.Label lblField1;
        private System.Windows.Forms.Label lblField2;
        private System.Windows.Forms.Label lblField3;
        private System.Windows.Forms.Label lblField4;
        private System.Windows.Forms.Label lblField5;
        private System.Windows.Forms.GroupBox groupBox1;
        private DataGridViewTextBoxColumn abilityDataGridViewTextBoxColumn;
    }
}