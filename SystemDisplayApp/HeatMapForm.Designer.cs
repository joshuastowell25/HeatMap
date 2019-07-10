namespace SystemDisplayApp
{
    partial class HeatMapForm
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
            this.components = new System.ComponentModel.Container();
            this.heatMapDataGrid = new System.Windows.Forms.DataGridView();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataFileBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.granularityListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.systemDataGrid = new System.Windows.Forms.DataGridView();
            this.maTypeListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clearSystemsButton = new System.Windows.Forms.Button();
            this.cbPlayOvernight = new System.Windows.Forms.CheckBox();
            this.filebutton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.heatMapDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // heatMapDataGrid
            // 
            this.heatMapDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heatMapDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.heatMapDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.heatMapDataGrid.Location = new System.Drawing.Point(236, 11);
            this.heatMapDataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.heatMapDataGrid.Name = "heatMapDataGrid";
            this.heatMapDataGrid.RowHeadersVisible = false;
            this.heatMapDataGrid.RowTemplate.Height = 24;
            this.heatMapDataGrid.Size = new System.Drawing.Size(693, 611);
            this.heatMapDataGrid.TabIndex = 0;
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.Location = new System.Drawing.Point(9, 93);
            this.startDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(197, 20);
            this.startDateTimePicker.TabIndex = 1;
            this.startDateTimePicker.CloseUp += new System.EventHandler(this.granularity_or_date_changed);
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Location = new System.Drawing.Point(9, 147);
            this.endDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(197, 20);
            this.endDateTimePicker.TabIndex = 2;
            this.endDateTimePicker.CloseUp += new System.EventHandler(this.granularity_or_date_changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Date:";
            // 
            // dataFileBox
            // 
            this.dataFileBox.Location = new System.Drawing.Point(9, 38);
            this.dataFileBox.Name = "dataFileBox";
            this.dataFileBox.Size = new System.Drawing.Size(136, 20);
            this.dataFileBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Data:";
            // 
            // granularityListBox
            // 
            this.granularityListBox.FormattingEnabled = true;
            this.granularityListBox.Location = new System.Drawing.Point(9, 197);
            this.granularityListBox.Name = "granularityListBox";
            this.granularityListBox.Size = new System.Drawing.Size(197, 56);
            this.granularityListBox.TabIndex = 7;
            this.granularityListBox.SelectedIndexChanged += new System.EventHandler(this.granularity_or_date_changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Granularity:";
            // 
            // systemDataGrid
            // 
            this.systemDataGrid.AllowUserToAddRows = false;
            this.systemDataGrid.AllowUserToDeleteRows = false;
            this.systemDataGrid.AllowUserToResizeColumns = false;
            this.systemDataGrid.AllowUserToResizeRows = false;
            this.systemDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.systemDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.systemDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.systemDataGrid.Location = new System.Drawing.Point(9, 389);
            this.systemDataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.systemDataGrid.Name = "systemDataGrid";
            this.systemDataGrid.RowHeadersVisible = false;
            this.systemDataGrid.RowTemplate.Height = 24;
            this.systemDataGrid.Size = new System.Drawing.Size(209, 233);
            this.systemDataGrid.TabIndex = 10;
            this.systemDataGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.systemDataGrid_CellLeave);
            // 
            // maTypeListBox
            // 
            this.maTypeListBox.FormattingEnabled = true;
            this.maTypeListBox.Location = new System.Drawing.Point(9, 282);
            this.maTypeListBox.Name = "maTypeListBox";
            this.maTypeListBox.Size = new System.Drawing.Size(197, 56);
            this.maTypeListBox.TabIndex = 11;
            this.maTypeListBox.SelectedIndexChanged += new System.EventHandler(this.maTypeListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Avg Type:";
            // 
            // clearSystemsButton
            // 
            this.clearSystemsButton.Location = new System.Drawing.Point(9, 348);
            this.clearSystemsButton.Name = "clearSystemsButton";
            this.clearSystemsButton.Size = new System.Drawing.Size(75, 36);
            this.clearSystemsButton.TabIndex = 13;
            this.clearSystemsButton.Text = "Clear systems";
            this.clearSystemsButton.UseVisualStyleBackColor = true;
            this.clearSystemsButton.Click += new System.EventHandler(this.clearSystemsButton_Click);
            // 
            // cbPlayOvernight
            // 
            this.cbPlayOvernight.AutoSize = true;
            this.cbPlayOvernight.Checked = true;
            this.cbPlayOvernight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPlayOvernight.Location = new System.Drawing.Point(109, 359);
            this.cbPlayOvernight.Name = "cbPlayOvernight";
            this.cbPlayOvernight.Size = new System.Drawing.Size(95, 17);
            this.cbPlayOvernight.TabIndex = 14;
            this.cbPlayOvernight.Text = "Play Overnight";
            this.toolTip1.SetToolTip(this.cbPlayOvernight, "Controls whether calculations are done based on holding or going flat overnight");
            this.cbPlayOvernight.UseVisualStyleBackColor = true;
            this.cbPlayOvernight.CheckedChanged += new System.EventHandler(this.cbPlayOvernight_CheckedChanged);
            // 
            // filebutton
            // 
            this.filebutton.Location = new System.Drawing.Point(150, 37);
            this.filebutton.Margin = new System.Windows.Forms.Padding(2);
            this.filebutton.Name = "filebutton";
            this.filebutton.Size = new System.Drawing.Size(56, 20);
            this.filebutton.TabIndex = 15;
            this.filebutton.Text = "file";
            this.filebutton.UseVisualStyleBackColor = true;
            this.filebutton.Click += new System.EventHandler(this.filebutton_Click);
            // 
            // HeatMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 633);
            this.Controls.Add(this.filebutton);
            this.Controls.Add(this.cbPlayOvernight);
            this.Controls.Add(this.clearSystemsButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maTypeListBox);
            this.Controls.Add(this.systemDataGrid);
            this.Controls.Add(this.heatMapDataGrid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.granularityListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataFileBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.startDateTimePicker);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HeatMapForm";
            this.Text = "Moving Average Heat Map";
            this.Load += new System.EventHandler(this.HeatMapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.heatMapDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView heatMapDataGrid;
        private System.Windows.Forms.TextBox dataFileBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox granularityListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView systemDataGrid;
        private System.Windows.Forms.ListBox maTypeListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button clearSystemsButton;
        private System.Windows.Forms.CheckBox cbPlayOvernight;
        private System.Windows.Forms.Button filebutton;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

