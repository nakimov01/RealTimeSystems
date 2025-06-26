namespace DiskMonitorApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtFromEmail;
        private TextBox txtFromPassword;
        private TextBox txtToEmail;
        private TextBox txtDrive;
        private TextBox txtInterval;
        private Button btnStart;
        private Label lblStatus;

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
            this.txtFromEmail = new System.Windows.Forms.TextBox();
            this.txtFromPassword = new System.Windows.Forms.TextBox();
            this.txtToEmail = new System.Windows.Forms.TextBox();
            this.txtDrive = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtFromEmail
            this.txtFromEmail.Location = new System.Drawing.Point(10, 10);
            this.txtFromEmail.Size = new System.Drawing.Size(200, 20);
            this.txtFromEmail.Text = "ваш_email@gmail.com";

            // txtFromPassword
            this.txtFromPassword.Location = new System.Drawing.Point(10, 40);
            this.txtFromPassword.Size = new System.Drawing.Size(200, 20);
            this.txtFromPassword.Text = "ваш_пароль_приложения";
            this.txtFromPassword.PasswordChar = '*';

            // txtToEmail
            this.txtToEmail.Location = new System.Drawing.Point(10, 70);
            this.txtToEmail.Size = new System.Drawing.Size(200, 20);
            this.txtToEmail.Text = "получатель@example.com";

            // txtDrive
            this.txtDrive.Location = new System.Drawing.Point(10, 100);
            this.txtDrive.Size = new System.Drawing.Size(200, 20);
            this.txtDrive.Text = "C:\\";

            // txtInterval
            this.txtInterval.Location = new System.Drawing.Point(10, 130);
            this.txtInterval.Size = new System.Drawing.Size(200, 20);
            this.txtInterval.Text = "60"; // Интервал в секундах

            // btnStart
            this.btnStart.Location = new System.Drawing.Point(10, 160);
            this.btnStart.Size = new System.Drawing.Size(200, 30);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(10, 200);
            this.lblStatus.Size = new System.Drawing.Size(200, 20);
            this.lblStatus.Text = "Status: Idle";

            // Form1
            this.ClientSize = new System.Drawing.Size(220, 230);
            this.Controls.Add(this.txtFromEmail);
            this.Controls.Add(this.txtFromPassword);
            this.Controls.Add(this.txtToEmail);
            this.Controls.Add(this.txtDrive);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblStatus);
            this.Text = "Disk Space Monitor";
            this.ResumeLayout(false);
        }
    }
}