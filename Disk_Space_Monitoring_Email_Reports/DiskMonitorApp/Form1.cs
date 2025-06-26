using System;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DiskMonitorApp
{
    public partial class Form1 : Form
    {
        // ����������� ������� GetDiskFreeSpaceEx
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetDiskFreeSpaceEx(
            string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes
        );

        // ��������� SMTP ��� Gmail
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;

        // ������ ��� ������������� ��������
        private System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
        }

        // ��������� ���������� � ��������� ����� �� �����
        private string GetDiskFreeSpaceInfo(string drive)
        {
            ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;

            if (GetDiskFreeSpaceEx(drive, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes))
            {
                return $"����: {drive}\n" +
                       $"����� �����: {totalNumberOfBytes / (1024 * 1024 * 1024)} ��\n" +
                       $"��������� �����: {freeBytesAvailable / (1024 * 1024 * 1024)} ��";
            }
            else
            {
                return "�� ������� �������� ���������� � ��������� ����� �� �����.";
            }
        }

        // �������� email ����� SMTP
        private void SendEmail(string fromEmail, string fromPassword, string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(SmtpHost, SmtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(toEmail);

                    smtpClient.Send(mailMessage);
                    lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] Email ������� ���������.";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] �� ������� ��������� email: {ex.Message}";
            }
        }

        // ���������� ������� ������ "�����"
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timer != null && timer.Enabled)
            {
                timer.Stop();
                btnStart.Text = "�����";
                lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] ������ ����������.";
                return;
            }

            string fromEmail = txtFromEmail.Text;
            string fromPassword = txtFromPassword.Text;
            string toEmail = txtToEmail.Text;
            string drive = txtDrive.Text;
            int interval;

            // �������� ����� ���������
            if (!int.TryParse(txtInterval.Text, out interval) || interval <= 0)
            {
                lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] �������� ������ ���� ������������� ������.";
                return;
            }

            // �������� ������� �����
            if (string.IsNullOrEmpty(drive) || !drive.EndsWith("\\"))
            {
                lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] �������� ������ �����. ����������� ������, �������� 'C:\\'.";
                return;
            }

            // ������ �������
            timer = new System.Windows.Forms.Timer();
            timer.Interval = interval * 1000; // �������� � �������������
            timer.Tick += (s, ev) =>
            {
                string subject = "����� � ��������� ����� �� �����";
                string body = GetDiskFreeSpaceInfo(drive);
                SendEmail(fromEmail, fromPassword, toEmail, subject, body);
            };
            timer.Start();

            btnStart.Text = "����";
            lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] ������ �������.";
        }
    }
}