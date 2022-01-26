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
            this.DrawSpace.Location = new System.Drawing.Point(-1, 82);
            this.DrawSpace.Name = "DrawSpace";
            this.DrawSpace.Size = new System.Drawing.Size(1633, 842);
            this.DrawSpace.TabIndex = 0;
            this.DrawSpace.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // BtnCreateCircle
            // 
            this.BtnCreateCircle.Location = new System.Drawing.Point(611, 19);
            this.BtnCreateCircle.Name = "BtnCreateCircle";
            this.BtnCreateCircle.Size = new System.Drawing.Size(75, 45);
            this.BtnCreateCircle.TabIndex = 0;
            this.BtnCreateCircle.Text = "Créer un cercle";
            this.BtnCreateCircle.UseVisualStyleBackColor = true;
            this.BtnCreateCircle.Click += new System.EventHandler(this.BtnCreateCircle_Click);
            // 
            // LabelText
            // 
            this.LabelText.AutoSize = true;
            this.LabelText.Location = new System.Drawing.Point(462, 82);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(0, 13);
            this.LabelText.TabIndex = 0;
            // 
            // YInputBox
            // 
            this.YInputBox.Location = new System.Drawing.Point(508, 32);
            this.YInputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.YInputBox.Name = "YInputBox";
            this.YInputBox.Size = new System.Drawing.Size(76, 20);
            this.YInputBox.TabIndex = 1;
            // 
            // XInputBox
            // 
            this.XInputBox.Location = new System.Drawing.Point(337, 32);
            this.XInputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.XInputBox.Name = "XInputBox";
            this.XInputBox.Size = new System.Drawing.Size(76, 20);
            this.XInputBox.TabIndex = 2;
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(438, 35);
            this.YLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(54, 13);
            this.YLabel.TabIndex = 3;
            this.YLabel.Text = "Position Y";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(269, 35);
            this.XLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(54, 13);
            this.XLabel.TabIndex = 0;
            this.XLabel.Text = "Position X";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 459);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XInputBox);
            this.Controls.Add(this.YInputBox);
            this.Controls.Add(this.LabelText);
            this.Controls.Add(this.BtnCreateCircle);
            this.Controls.Add(this.DrawSpace);
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

