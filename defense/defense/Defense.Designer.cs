namespace defense
{
    partial class Defense
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
            this.mapLabel = new System.Windows.Forms.Label();
            this.menuLabel = new System.Windows.Forms.Label();
            this.ticker = new System.Windows.Forms.Timer(this.components);
            this.addEnemy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mapLabel
            // 
            this.mapLabel.BackColor = System.Drawing.Color.White;
            this.mapLabel.Image = global::defense.Properties.Resources.map;
            this.mapLabel.Location = new System.Drawing.Point(0, 0);
            this.mapLabel.Margin = new System.Windows.Forms.Padding(0);
            this.mapLabel.Name = "mapLabel";
            this.mapLabel.Size = new System.Drawing.Size(450, 450);
            this.mapLabel.TabIndex = 0;
            this.mapLabel.Text = "MAP";
            this.mapLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mapLabel.Click += new System.EventHandler(this.mapLabel_Click);
            this.mapLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapLabel_Paint);
            // 
            // menuLabel
            // 
            this.menuLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.menuLabel.Location = new System.Drawing.Point(0, 450);
            this.menuLabel.Margin = new System.Windows.Forms.Padding(0);
            this.menuLabel.Name = "menuLabel";
            this.menuLabel.Size = new System.Drawing.Size(450, 296);
            this.menuLabel.TabIndex = 1;
            this.menuLabel.Text = "MENU";
            this.menuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ticker
            // 
            this.ticker.Enabled = true;
            this.ticker.Interval = 10;
            this.ticker.Tick += new System.EventHandler(this.ticker_Tick);
            // 
            // addEnemy
            // 
            this.addEnemy.Location = new System.Drawing.Point(3, 749);
            this.addEnemy.Name = "addEnemy";
            this.addEnemy.Size = new System.Drawing.Size(125, 50);
            this.addEnemy.TabIndex = 2;
            this.addEnemy.Text = "&Add Enemy";
            this.addEnemy.UseVisualStyleBackColor = true;
            this.addEnemy.Click += new System.EventHandler(this.addEnemy_Click);
            // 
            // Defense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 801);
            this.Controls.Add(this.addEnemy);
            this.Controls.Add(this.menuLabel);
            this.Controls.Add(this.mapLabel);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Defense";
            this.Text = "Defense";
            this.Load += new System.EventHandler(this.Defense_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label mapLabel;
        private System.Windows.Forms.Label menuLabel;
        private System.Windows.Forms.Timer ticker;
        private System.Windows.Forms.Button addEnemy;
    }
}

