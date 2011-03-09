namespace ASCOM.OpenFocuser
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

        private ASCOM.OpenFocuser.Focuser _focuser;
        private Config _config;
        public DebugForm _debugForm;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TargetPosition = new System.Windows.Forms.NumericUpDown();
            this.PositionLabel = new System.Windows.Forms.Label();
            this.ActualPosition = new System.Windows.Forms.TextBox();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.debugButton = new System.Windows.Forms.Button();
            this.PositionTimer = new System.Windows.Forms.Timer(this.components);
            this.GoButton = new System.Windows.Forms.Button();
            this.InButton = new System.Windows.Forms.Button();
            this.OutButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.TargetPosition, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.PositionLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ActualPosition, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TargetLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.InButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.OutButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.StopButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ResetButton, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(143, 125);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // TargetPosition
            // 
            this.TargetPosition.Location = new System.Drawing.Point(74, 29);
            this.TargetPosition.Maximum = new decimal(new int[] {
            65355,
            0,
            0,
            0});
            this.TargetPosition.Name = "TargetPosition";
            this.TargetPosition.Size = new System.Drawing.Size(66, 20);
            this.TargetPosition.TabIndex = 3;
            // 
            // PositionLabel
            // 
            this.PositionLabel.AutoSize = true;
            this.PositionLabel.Location = new System.Drawing.Point(3, 0);
            this.PositionLabel.Name = "PositionLabel";
            this.PositionLabel.Size = new System.Drawing.Size(47, 13);
            this.PositionLabel.TabIndex = 2;
            this.PositionLabel.Text = "Position:";
            // 
            // ActualPosition
            // 
            this.ActualPosition.Location = new System.Drawing.Point(74, 3);
            this.ActualPosition.Name = "ActualPosition";
            this.ActualPosition.ReadOnly = true;
            this.ActualPosition.Size = new System.Drawing.Size(66, 20);
            this.ActualPosition.TabIndex = 3;
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.Location = new System.Drawing.Point(3, 26);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(41, 13);
            this.TargetLabel.TabIndex = 4;
            this.TargetLabel.Text = "Target:";
            // 
            // debugButton
            // 
            this.debugButton.Location = new System.Drawing.Point(12, 152);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(101, 23);
            this.debugButton.TabIndex = 3;
            this.debugButton.Text = "Debug window";
            this.debugButton.UseVisualStyleBackColor = true;
            this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
            // 
            // PositionTimer
            // 
            this.PositionTimer.Interval = 1000;
            this.PositionTimer.Tick += new System.EventHandler(this.PositionTimer_Tick);
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(161, 41);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(29, 23);
            this.GoButton.TabIndex = 4;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // InButton
            // 
            this.InButton.Location = new System.Drawing.Point(3, 55);
            this.InButton.Name = "InButton";
            this.InButton.Size = new System.Drawing.Size(65, 23);
            this.InButton.TabIndex = 5;
            this.InButton.Text = "In";
            this.InButton.UseVisualStyleBackColor = true;
            // 
            // OutButton
            // 
            this.OutButton.Location = new System.Drawing.Point(74, 55);
            this.OutButton.Name = "OutButton";
            this.OutButton.Size = new System.Drawing.Size(66, 23);
            this.OutButton.TabIndex = 6;
            this.OutButton.Text = "Out";
            this.OutButton.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(3, 84);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(65, 23);
            this.StopButton.TabIndex = 7;
            this.StopButton.Text = "Stop!";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(74, 84);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(66, 23);
            this.ResetButton.TabIndex = 8;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 263);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.debugButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Open Focuser";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetPosition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label PositionLabel;
        private System.Windows.Forms.TextBox ActualPosition;
        private System.Windows.Forms.Label TargetLabel;
        private System.Windows.Forms.NumericUpDown TargetPosition;
        private System.Windows.Forms.Button debugButton;
        private System.Windows.Forms.Timer PositionTimer;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.Button InButton;
        private System.Windows.Forms.Button OutButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button ResetButton;

    }
}