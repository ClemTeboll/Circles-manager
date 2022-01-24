namespace Algo
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrawSpace = new System.Windows.Forms.Panel();
            this.BtnCreateCircle = new System.Windows.Forms.Button();
            this.LabelText = new System.Windows.Forms.Label();
            this.YInputBox = new System.Windows.Forms.TextBox();
            this.XInputBox = new System.Windows.Forms.TextBox();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DrawSpace
            // 
            this.DrawSpace.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DrawSpace.Location = new System.Drawing.Point(-1, 101);
            this.DrawSpace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrawSpace.Name = "DrawSpace";
            this.DrawSpace.Size = new System.Drawing.Size(1068, 454);
            this.DrawSpace.TabIndex = 0;
            this.DrawSpace.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // BtnCreateCircle
            // 
            this.BtnCreateCircle.Location = new System.Drawing.Point(723, 23);
            this.BtnCreateCircle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnCreateCircle.Name = "BtnCreateCircle";
            this.BtnCreateCircle.Size = new System.Drawing.Size(100, 55);
            this.BtnCreateCircle.TabIndex = 0;
            this.BtnCreateCircle.Text = "Créer un cercle";
            this.BtnCreateCircle.UseVisualStyleBackColor = true;
            this.BtnCreateCircle.Click += new System.EventHandler(this.BtnCreateCircle_Click);
            // 
            // LabelText
            // 
            this.LabelText.AutoSize = true;
            this.LabelText.Location = new System.Drawing.Point(616, 101);
            this.LabelText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(0, 16);
            this.LabelText.TabIndex = 0;
            // 
            // YInputBox
            // 
            this.YInputBox.Location = new System.Drawing.Point(578, 39);
            this.YInputBox.Name = "YInputBox";
            this.YInputBox.Size = new System.Drawing.Size(100, 22);
            this.YInputBox.TabIndex = 1;
            // 
            // XInputBox
            // 
            this.XInputBox.Location = new System.Drawing.Point(365, 39);
            this.XInputBox.Name = "XInputBox";
            this.XInputBox.Size = new System.Drawing.Size(100, 22);
            this.XInputBox.TabIndex = 2;
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(493, 42);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(67, 16);
            this.YLabel.TabIndex = 3;
            this.YLabel.Text = "Position Y";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(271, 42);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(66, 16);
            this.XLabel.TabIndex = 0;
            this.XLabel.Text = "Position X";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XInputBox);
            this.Controls.Add(this.YInputBox);
            this.Controls.Add(this.LabelText);
            this.Controls.Add(this.BtnCreateCircle);
            this.Controls.Add(this.DrawSpace);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawSpace;
        private System.Windows.Forms.Button BtnCreateCircle;
        private System.Windows.Forms.Label LabelText;
        private System.Windows.Forms.TextBox YInputBox;
        private System.Windows.Forms.TextBox XInputBox;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label XLabel;
    }
}

