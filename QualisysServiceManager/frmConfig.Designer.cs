namespace QualisysServiceManager
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.pnlMainContainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlAppSettings = new System.Windows.Forms.GroupBox();
            this.pnlAppSettingsContainer = new System.Windows.Forms.Panel();
            this.pnlConnetionStrings = new System.Windows.Forms.GroupBox();
            this.pnlConnectionStringsContainer = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlMainContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlAppSettings.SuspendLayout();
            this.pnlConnetionStrings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainContainer
            // 
            this.pnlMainContainer.Controls.Add(this.tableLayoutPanel1);
            this.pnlMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContainer.Location = new System.Drawing.Point(8, 8);
            this.pnlMainContainer.Name = "pnlMainContainer";
            this.pnlMainContainer.Size = new System.Drawing.Size(393, 499);
            this.pnlMainContainer.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlButtons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 499);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(3, 452);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(387, 44);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(309, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(228, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlAppSettings
            // 
            this.pnlAppSettings.Controls.Add(this.pnlAppSettingsContainer);
            this.pnlAppSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAppSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAppSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlAppSettings.Name = "pnlAppSettings";
            this.pnlAppSettings.Size = new System.Drawing.Size(387, 218);
            this.pnlAppSettings.TabIndex = 1;
            this.pnlAppSettings.TabStop = false;
            this.pnlAppSettings.Text = "App Settings";
            // 
            // pnlAppSettingsContainer
            // 
            this.pnlAppSettingsContainer.AutoScroll = true;
            this.pnlAppSettingsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAppSettingsContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAppSettingsContainer.Location = new System.Drawing.Point(3, 18);
            this.pnlAppSettingsContainer.Name = "pnlAppSettingsContainer";
            this.pnlAppSettingsContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 32);
            this.pnlAppSettingsContainer.Size = new System.Drawing.Size(381, 197);
            this.pnlAppSettingsContainer.TabIndex = 0;
            // 
            // pnlConnetionStrings
            // 
            this.pnlConnetionStrings.Controls.Add(this.pnlConnectionStringsContainer);
            this.pnlConnetionStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConnetionStrings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlConnetionStrings.Location = new System.Drawing.Point(0, 0);
            this.pnlConnetionStrings.Name = "pnlConnetionStrings";
            this.pnlConnetionStrings.Size = new System.Drawing.Size(387, 221);
            this.pnlConnetionStrings.TabIndex = 2;
            this.pnlConnetionStrings.TabStop = false;
            this.pnlConnetionStrings.Text = "Connection Strings";
            // 
            // pnlConnectionStringsContainer
            // 
            this.pnlConnectionStringsContainer.AutoScroll = true;
            this.pnlConnectionStringsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConnectionStringsContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlConnectionStringsContainer.Location = new System.Drawing.Point(3, 18);
            this.pnlConnectionStringsContainer.Name = "pnlConnectionStringsContainer";
            this.pnlConnectionStringsContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 32);
            this.pnlConnectionStringsContainer.Size = new System.Drawing.Size(381, 200);
            this.pnlConnectionStringsContainer.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlConnetionStrings);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlAppSettings);
            this.splitContainer1.Size = new System.Drawing.Size(387, 443);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 4;
            // 
            // frmConfig
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(409, 515);
            this.Controls.Add(this.pnlMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 400);
            this.Name = "frmConfig";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Configuración del servicio";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.pnlMainContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlAppSettings.ResumeLayout(false);
            this.pnlConnetionStrings.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox pnlAppSettings;
        private System.Windows.Forms.Panel pnlAppSettingsContainer;
        private System.Windows.Forms.GroupBox pnlConnetionStrings;
        private System.Windows.Forms.Panel pnlConnectionStringsContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}