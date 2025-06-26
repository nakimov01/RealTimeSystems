using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryCopyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void CopyButton_Click(object sender, EventArgs e)
        {
            string sourceDir = sourceTextBox.Text;
            string destinationDir = destinationTextBox.Text;

            if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destinationDir))
            {
                MessageBox.Show("������� �������� � ������� ��������", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(sourceDir))
            {
                MessageBox.Show("�������� ������� �� ����������", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            copyButton.Enabled = false;
            statusLabel.Text = "�����������...";
            progressBar.Value = 0;

            try
            {
                // ����������� ����������� ��� ����� ����
                await Task.Run(() => CopyDirectoryAsync(sourceDir, destinationDir));

                statusLabel.Text = "����������� ���������!";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "������ ��� �����������";
                MessageBox.Show($"������: {ex.Message}", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                copyButton.Enabled = true;
            }
        }

        private void CopyDirectoryAsync(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            // ����� ��� ��������-����
            var files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            progressBar.Invoke((MethodInvoker)(() => progressBar.Maximum = files.Length));

            foreach (var file in files)
            {
                string relativePath = file.Substring(sourceDir.Length + 1);
                string destFile = Path.Combine(destinationDir, relativePath);

                // �������� ������������ ���� ����
                string destDir = Path.GetDirectoryName(destFile);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                // �������� �����
                CopyFile(file, destFile);

                // ��������
                progressBar.Invoke((MethodInvoker)(() =>
                {
                    progressBar.Value++;
                    statusLabel.Text = $"�����������: {relativePath}";
                }));
            }
        }

        private void CopyFile(string sourceFile, string destinationFile)
        {
            const int bufferSize = 81920; // �����
            byte[] buffer = new byte[bufferSize];

            using (FileStream source = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (FileStream dest = new FileStream(destinationFile, FileMode.Create, FileAccess.Write))
            {
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(buffer, 0, bytesRead);
                }
            }
        }

        private void BrowseSourceButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    sourceTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void BrowseDestinationButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    destinationTextBox.Text = dialog.SelectedPath;
                }
            }
        }
    }
}