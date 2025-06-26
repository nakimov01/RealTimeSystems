using System;
using System.Windows.Forms;

namespace DirectoryCopyApp
{
    partial class Form1 : Form  // Добавлено наследование от Form
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.TextBox destinationTextBox;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.Button browseSourceButton;
        private System.Windows.Forms.Button browseDestinationButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.destinationTextBox = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.browseSourceButton = new System.Windows.Forms.Button();
            this.browseDestinationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // sourceTextBox
            this.sourceTextBox.Location = new System.Drawing.Point(120, 20);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(300, 20);
            this.sourceTextBox.TabIndex = 0;

            // destinationTextBox
            this.destinationTextBox.Location = new System.Drawing.Point(120, 60);
            this.destinationTextBox.Name = "destinationTextBox";
            this.destinationTextBox.Size = new System.Drawing.Size(300, 20);
            this.destinationTextBox.TabIndex = 1;

            // copyButton
            this.copyButton.Location = new System.Drawing.Point(120, 100);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(100, 30);
            this.copyButton.TabIndex = 2;
            this.copyButton.Text = "Копировать";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(120, 150);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 20);
            this.progressBar.TabIndex = 3;

            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(120, 180);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 4;

            // sourceLabel
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(20, 23);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(94, 13);
            this.sourceLabel.Text = "Исходный каталог:";

            // destinationLabel
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(20, 63);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(94, 13);
            this.destinationLabel.Text = "Целевой каталог:";

            // browseSourceButton
            this.browseSourceButton.Location = new System.Drawing.Point(430, 18);
            this.browseSourceButton.Name = "browseSourceButton";
            this.browseSourceButton.Size = new System.Drawing.Size(30, 23);
            this.browseSourceButton.TabIndex = 5;
            this.browseSourceButton.Text = "...";
            this.browseSourceButton.UseVisualStyleBackColor = true;
            this.browseSourceButton.Click += new System.EventHandler(this.BrowseSourceButton_Click);

            // browseDestinationButton
            this.browseDestinationButton.Location = new System.Drawing.Point(430, 58);
            this.browseDestinationButton.Name = "browseDestinationButton";
            this.browseDestinationButton.Size = new System.Drawing.Size(30, 23);
            this.browseDestinationButton.TabIndex = 6;
            this.browseDestinationButton.Text = "...";
            this.browseDestinationButton.UseVisualStyleBackColor = true;
            this.browseDestinationButton.Click += new System.EventHandler(this.BrowseDestinationButton_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 220);
            this.Controls.Add(this.browseDestinationButton);
            this.Controls.Add(this.browseSourceButton);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.destinationTextBox);
            this.Controls.Add(this.sourceTextBox);
            this.Name = "Form1";
            this.Text = "Асинхронное копирование каталогов";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}