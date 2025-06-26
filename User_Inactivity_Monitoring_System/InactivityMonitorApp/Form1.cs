using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;
using System.Media;

namespace InactivityMonitorApp
{
    public partial class Form1 : Form
    {
 
        private readonly System.Timers.Timer inactivityTimer;
        private readonly System.Windows.Forms.Timer countdownTimer;
        private const int InactivityTimeout = 60000;
        private string soundFilePath = "";
        private bool isTimerRunning = false;
        private Button startButton;
        private Button stopButton;
        private Label statusLabel;
        private Label countdownLabel;
        private int remainingTime = InactivityTimeout / 1000; 
        private IntPtr mouseHookHandle;
        private LowLevelMouseProc mouseHookCallback;

        // ������ ������� WinAPI
        private const int WH_MOUSE_LL = 14;
        private const int WM_MOUSEMOVE = 0x0200;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public Form1()
        {
            InitializeComponent();

            // ������������� ������� ����������� (System.Timers.Timer)
            inactivityTimer = new System.Timers.Timer(InactivityTimeout);
            inactivityTimer.Elapsed += OnInactivityTimerElapsed;
            inactivityTimer.AutoReset = false;

            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            SubscribeToGlobalMouseEvents();

            InitializeUI();
        }

        private void SubscribeToGlobalMouseEvents()
        {
            mouseHookCallback = MouseHookCallback;
            mouseHookHandle = SetWindowsHookEx(WH_MOUSE_LL, mouseHookCallback, GetModuleHandle(null), 0);
            if (mouseHookHandle == IntPtr.Zero)
            {
                MessageBox.Show("�� ������� ���������� ���������� ��� ����!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_MOUSEMOVE && isTimerRunning)
            {
                ResetInactivityTimer();
                remainingTime = InactivityTimeout / 1000; 
                UpdateCountdownLabel(); 
            }
            return CallNextHookEx(mouseHookHandle, nCode, wParam, lParam);
        }


        private void InitializeUI()
        {
            startButton = new Button
            {
                Text = "�����",
                Location = new Point(50, 50),
                Size = new Size(100, 30)
            };
            startButton.Click += StartButton_Click;

            stopButton = new Button
            {
                Text = "����",
                Location = new Point(200, 50),
                Size = new Size(100, 30)
            };
            stopButton.Click += StopButton_Click;

            statusLabel = new Label
            {
                Text = "������ ����������",
                Location = new Point(50, 100),
                AutoSize = true,
                ForeColor = Color.Red
            };

            countdownLabel = new Label
            {
                Text = "��������: 60 ���",
                Location = new Point(50, 130),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue
            };

            // ���������� ��������� �� �����
            this.Controls.Add(startButton);
            this.Controls.Add(stopButton);
            this.Controls.Add(statusLabel);
            this.Controls.Add(countdownLabel);
        }

        // "�����"
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!isTimerRunning)
            {
                ResetInactivityTimer();
                countdownTimer.Start(); // ������ ������� ��������� �������
                isTimerRunning = true;
                statusLabel.Text = "������ �������";
                statusLabel.ForeColor = Color.Green;
            }
        }

        // ���������� ������� �� ������ "����"
        private void StopButton_Click(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                inactivityTimer.Stop();
                countdownTimer.Stop(); // �������� ��������� �������
                isTimerRunning = false;
                statusLabel.Text = "������ ����������";
                statusLabel.ForeColor = Color.Red;
                countdownLabel.Text = "��������: 60 ���";
                remainingTime = InactivityTimeout / 1000;
            }
        }

        // ����� ������ �������
        private void ResetInactivityTimer()
        {
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }

        // ���������� ������������ �������
        private void OnInactivityTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Invoke((Action)(() =>
            {
                MessageBox.Show("��� ���������� ���� � ������� 1 ������!", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            
                PlaySound();
               
                countdownTimer.Stop();
                isTimerRunning = false;
                statusLabel.Text = "������ ����������";
                statusLabel.ForeColor = Color.Red;
                countdownLabel.Text = "��������: 60 ���";
                remainingTime = InactivityTimeout / 1000; 
            }));
        }

        // ���������� ������� ��������� �������
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
                UpdateCountdownLabel();
            }
            else
            {
                countdownTimer.Stop(); // ��������� ������� ��������� �������
            }
        }

        // ���������� ������ ��������� �������
        private void UpdateCountdownLabel()
        {
            countdownLabel.Text = $"��������: {remainingTime} ���";
        }

        //��������������� �����
        private void PlaySound()
        {
            try
            {
                if (!string.IsNullOrEmpty(soundFilePath))
                {
                    using (SoundPlayer player = new SoundPlayer(soundFilePath))
                    {
                        player.Play();
                    }
                }
                else
                {
                    MessageBox.Show("���� � MP3 ����� �� ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��������������� �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ��� �������� �����
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            isTimerRunning = false;
            statusLabel.Text = "������ ����������";
            statusLabel.ForeColor = Color.Red;
            countdownLabel.Text = "��������: 60 ���"; 
            remainingTime = InactivityTimeout / 1000;
        }

        // ��� �������� �����
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            inactivityTimer.Stop();
            countdownTimer.Stop();
            if (mouseHookHandle != IntPtr.Zero)
            {
                UnhookWindowsHookEx(mouseHookHandle); // �������� ����������� ����
            }
            inactivityTimer.Dispose();
            countdownTimer.Dispose();
            base.OnFormClosed(e);
        }


















        // ������� ��� ����������� ���� ����
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}