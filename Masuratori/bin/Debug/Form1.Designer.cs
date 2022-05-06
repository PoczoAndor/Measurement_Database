
namespace Masuratori
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
            this.import = new System.Windows.Forms.Button();
            this.import_text = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataGridViewDbControls = new System.Windows.Forms.DataGridView();
            this.searchButton = new System.Windows.Forms.Button();
            this.textBox_data = new System.Windows.Forms.TextBox();
            this.tabControls = new System.Windows.Forms.TabControl();
            this.db_controls = new System.Windows.Forms.TabPage();
            this.textBox_watcher = new System.Windows.Forms.TextBox();
            this.watcher = new System.Windows.Forms.Button();
            this.tab_data = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_isOuttol = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_ax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_cota = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_nume = new System.Windows.Forms.TextBox();
            this.textBox_reper = new System.Windows.Forms.TextBox();
            this.button_save_excel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.backgroundWorker_import = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_convert = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_watch = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDbControls)).BeginInit();
            this.tabControls.SuspendLayout();
            this.db_controls.SuspendLayout();
            this.tab_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // import
            // 
            this.import.Location = new System.Drawing.Point(6, 5);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(141, 23);
            this.import.TabIndex = 0;
            this.import.Text = "Convert PDF";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.button1_Click);
            // 
            // import_text
            // 
            this.import_text.Location = new System.Drawing.Point(153, 6);
            this.import_text.Name = "import_text";
            this.import_text.Size = new System.Drawing.Size(75, 23);
            this.import_text.TabIndex = 2;
            this.import_text.Text = "Import";
            this.import_text.UseVisualStyleBackColor = true;
            this.import_text.Click += new System.EventHandler(this.import_text_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(6, 61);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(759, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // dataGridViewDbControls
            // 
            this.dataGridViewDbControls.AllowUserToAddRows = false;
            this.dataGridViewDbControls.AllowUserToDeleteRows = false;
            this.dataGridViewDbControls.AllowUserToOrderColumns = true;
            this.dataGridViewDbControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDbControls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDbControls.Location = new System.Drawing.Point(6, 109);
            this.dataGridViewDbControls.Name = "dataGridViewDbControls";
            this.dataGridViewDbControls.ReadOnly = true;
            this.dataGridViewDbControls.Size = new System.Drawing.Size(759, 274);
            this.dataGridViewDbControls.TabIndex = 5;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(680, 15);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // textBox_data
            // 
            this.textBox_data.Location = new System.Drawing.Point(113, 20);
            this.textBox_data.Name = "textBox_data";
            this.textBox_data.Size = new System.Drawing.Size(100, 20);
            this.textBox_data.TabIndex = 7;
            // 
            // tabControls
            // 
            this.tabControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControls.Controls.Add(this.db_controls);
            this.tabControls.Controls.Add(this.tab_data);
            this.tabControls.Location = new System.Drawing.Point(9, 26);
            this.tabControls.Name = "tabControls";
            this.tabControls.SelectedIndex = 0;
            this.tabControls.Size = new System.Drawing.Size(779, 412);
            this.tabControls.TabIndex = 9;
            // 
            // db_controls
            // 
            this.db_controls.Controls.Add(this.textBox_watcher);
            this.db_controls.Controls.Add(this.watcher);
            this.db_controls.Controls.Add(this.dataGridViewDbControls);
            this.db_controls.Controls.Add(this.import);
            this.db_controls.Controls.Add(this.progressBar1);
            this.db_controls.Controls.Add(this.import_text);
            this.db_controls.Location = new System.Drawing.Point(4, 22);
            this.db_controls.Name = "db_controls";
            this.db_controls.Padding = new System.Windows.Forms.Padding(3);
            this.db_controls.Size = new System.Drawing.Size(771, 386);
            this.db_controls.TabIndex = 0;
            this.db_controls.Text = "DB_Controls";
            this.db_controls.UseVisualStyleBackColor = true;
            // 
            // textBox_watcher
            // 
            this.textBox_watcher.Location = new System.Drawing.Point(345, 7);
            this.textBox_watcher.Name = "textBox_watcher";
            this.textBox_watcher.Size = new System.Drawing.Size(100, 20);
            this.textBox_watcher.TabIndex = 20;
            // 
            // watcher
            // 
            this.watcher.Location = new System.Drawing.Point(249, 6);
            this.watcher.Name = "watcher";
            this.watcher.Size = new System.Drawing.Size(75, 23);
            this.watcher.TabIndex = 19;
            this.watcher.Text = "Watcher";
            this.watcher.UseMnemonic = false;
            this.watcher.UseVisualStyleBackColor = true;
            this.watcher.Click += new System.EventHandler(this.watcher_Click);
            // 
            // tab_data
            // 
            this.tab_data.Controls.Add(this.label7);
            this.tab_data.Controls.Add(this.textBox_isOuttol);
            this.tab_data.Controls.Add(this.label6);
            this.tab_data.Controls.Add(this.textBox_ax);
            this.tab_data.Controls.Add(this.label5);
            this.tab_data.Controls.Add(this.textBox_cota);
            this.tab_data.Controls.Add(this.label4);
            this.tab_data.Controls.Add(this.label3);
            this.tab_data.Controls.Add(this.label2);
            this.tab_data.Controls.Add(this.textBox_nume);
            this.tab_data.Controls.Add(this.textBox_reper);
            this.tab_data.Controls.Add(this.button_save_excel);
            this.tab_data.Controls.Add(this.dataGridView1);
            this.tab_data.Controls.Add(this.searchButton);
            this.tab_data.Controls.Add(this.textBox_data);
            this.tab_data.Location = new System.Drawing.Point(4, 22);
            this.tab_data.Name = "tab_data";
            this.tab_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_data.Size = new System.Drawing.Size(771, 386);
            this.tab_data.TabIndex = 1;
            this.tab_data.Text = "Data";
            this.tab_data.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(550, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Iesit din toleranta";
            // 
            // textBox_isOuttol
            // 
            this.textBox_isOuttol.Location = new System.Drawing.Point(537, 20);
            this.textBox_isOuttol.Name = "textBox_isOuttol";
            this.textBox_isOuttol.Size = new System.Drawing.Size(100, 20);
            this.textBox_isOuttol.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Nominal";
            // 
            // textBox_ax
            // 
            this.textBox_ax.Location = new System.Drawing.Point(431, 20);
            this.textBox_ax.Name = "textBox_ax";
            this.textBox_ax.Size = new System.Drawing.Size(100, 20);
            this.textBox_ax.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(357, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Cota";
            // 
            // textBox_cota
            // 
            this.textBox_cota.Location = new System.Drawing.Point(325, 20);
            this.textBox_cota.Name = "textBox_cota";
            this.textBox_cota.Size = new System.Drawing.Size(100, 20);
            this.textBox_cota.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Numele fisei";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Reper";
            // 
            // textBox_nume
            // 
            this.textBox_nume.Location = new System.Drawing.Point(219, 20);
            this.textBox_nume.Name = "textBox_nume";
            this.textBox_nume.Size = new System.Drawing.Size(100, 20);
            this.textBox_nume.TabIndex = 11;
            // 
            // textBox_reper
            // 
            this.textBox_reper.Location = new System.Drawing.Point(7, 20);
            this.textBox_reper.Name = "textBox_reper";
            this.textBox_reper.Size = new System.Drawing.Size(100, 20);
            this.textBox_reper.TabIndex = 10;
            // 
            // button_save_excel
            // 
            this.button_save_excel.Location = new System.Drawing.Point(657, 45);
            this.button_save_excel.Name = "button_save_excel";
            this.button_save_excel.Size = new System.Drawing.Size(98, 23);
            this.button_save_excel.TabIndex = 9;
            this.button_save_excel.Text = "Save to Excel";
            this.button_save_excel.UseVisualStyleBackColor = true;
            this.button_save_excel.Click += new System.EventHandler(this.button_save_excel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(758, 284);
            this.dataGridView1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControls);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDbControls)).EndInit();
            this.tabControls.ResumeLayout(false);
            this.db_controls.ResumeLayout(false);
            this.db_controls.PerformLayout();
            this.tab_data.ResumeLayout(false);
            this.tab_data.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button import;
        private System.Windows.Forms.Button import_text;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dataGridViewDbControls;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox textBox_data;
        private System.Windows.Forms.TabControl tabControls;
        private System.Windows.Forms.TabPage db_controls;
        private System.Windows.Forms.TabPage tab_data;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_save_excel;
        private System.Windows.Forms.TextBox textBox_nume;
        private System.Windows.Forms.TextBox textBox_reper;
        private System.Windows.Forms.TextBox textBox_cota;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button watcher;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_isOuttol;
        private System.ComponentModel.BackgroundWorker backgroundWorker_import;
        private System.ComponentModel.BackgroundWorker backgroundWorker_convert;
        private System.ComponentModel.BackgroundWorker backgroundWorker_watch;
        private System.Windows.Forms.TextBox textBox_watcher;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_ax;
    }
}

