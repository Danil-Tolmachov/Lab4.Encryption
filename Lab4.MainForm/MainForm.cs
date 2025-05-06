using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab4;

namespace Lab4.MainForm
{
    public partial class MainForm : Form
    {
        private FileEncryptor _encryptor;
        private Stopwatch _stopwatch;
        private OperationType _currentOperation; 

        private enum OperationType { Encrypt, Decrypt }

        private class WorkerArguments
        {
            public string InputPath { get; set; }
            public string OutputPath { get; set; }
            public string Password { get; set; }
            public OperationType Operation { get; set; }
        }

        public MainForm()
        {
            InitializeComponent();
            _stopwatch = new Stopwatch();
            _encryptor = new FileEncryptor(); 

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }


        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select Input File";
            openFileDialog.Filter = "All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Select Output File";
            saveFileDialog.Filter = "All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputFile.Text = saveFileDialog.FileName;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            StartOperation(OperationType.Encrypt);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            StartOperation(OperationType.Decrypt);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_encryptor != null && backgroundWorker.IsBusy)
            {
                _encryptor.Pause();
                _stopwatch.Stop();
                uiTimer.Stop();
                lblStatus.Text = "Status: Paused";
                btnPause.Enabled = false;
                btnResume.Enabled = true;
                btnCancel.Enabled = true; 
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (_encryptor != null && backgroundWorker.IsBusy)
            {
                _encryptor.Resume();
                _stopwatch.Start();
                uiTimer.Start();
                lblStatus.Text = $"Status: {_currentOperation}ing...";
                btnPause.Enabled = true;
                btnResume.Enabled = false;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                lblStatus.Text = "Status: Cancelling...";
                _encryptor?.Cancel(); 
                backgroundWorker.CancelAsync(); 
                btnPause.Enabled = false;
                btnResume.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = $"Elapsed: {_stopwatch.Elapsed:hh\\:mm\\:ss}";
        }


        private void StartOperation(OperationType operation)
        {
            if (string.IsNullOrWhiteSpace(txtInputFile.Text) || !File.Exists(txtInputFile.Text))
            {
                MessageBox.Show("Please select a valid input file.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOutputFile.Text))
            {
                MessageBox.Show("Please select a valid output file.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtInputFile.Text.Equals(txtOutputFile.Text, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Input and output files cannot be the same.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (backgroundWorker.IsBusy)
            {
                MessageBox.Show("An operation is already in progress.", "Busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _currentOperation = operation;
            string inputFile = txtInputFile.Text;
            string outputFile = txtOutputFile.Text;
            string password = txtPassword.Text;

            progressBar.Value = 0;
            lblProgress.Text = "Progress: 0%";
            lblTimer.Text = "Elapsed: 00:00:00";
            lblStatus.Text = $"Status: Starting {operation}ion...";
            _stopwatch.Reset();


            var args = new WorkerArguments
            {
                InputPath = inputFile,
                OutputPath = outputFile,
                Password = password,
                Operation = operation
            };

            SetUIState(isRunning: true);

            _encryptor = new FileEncryptor(); 
            _stopwatch.Start();
            uiTimer.Start();
            backgroundWorker.RunWorkerAsync(args);
        }

        private void SetUIState(bool isRunning)
        {
            txtInputFile.Enabled = !isRunning;
            txtOutputFile.Enabled = !isRunning;
            txtPassword.Enabled = !isRunning;
            btnBrowseInput.Enabled = !isRunning;
            btnBrowseOutput.Enabled = !isRunning;
            btnEncrypt.Enabled = !isRunning;
            btnDecrypt.Enabled = !isRunning;

            btnPause.Enabled = isRunning;
            btnResume.Enabled = false; 
            btnCancel.Enabled = isRunning;
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var args = (WorkerArguments)e.Argument;
            long finalSize = 0;
            bool success = false;

            try
            {
                Action<int> progressCallback = p => worker.ReportProgress(p);

                using var inputFile = new FileStream(args.InputPath, FileMode.Open, FileAccess.Read);
                using var outputFile = new FileStream(args.OutputPath, FileMode.Create, FileAccess.Write);

                Task taskToRun;

                if (args.Operation == OperationType.Encrypt)
                {
                    taskToRun = _encryptor.Encrypt(args.Password, inputFile, outputFile, progressCallback);
                }
                else 
                {
                    taskToRun = _encryptor.Decrypt(args.Password, inputFile, outputFile, progressCallback);
                }

                taskToRun.GetAwaiter().GetResult();

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    finalSize = new FileInfo(args.OutputPath).Length;
                    success = true;
                }
            }
            catch (OperationCanceledException) 
            {
                e.Cancel = true; 
            }
            catch (System.Security.Cryptography.CryptographicException cryptEx)
            {
                e.Result = $"Crypto Error: {cryptEx.Message}. Possible wrong password or corrupted file.";
            }
            catch (Exception ex)
            {
                e.Result = $"Error: {ex.Message}"; 
            }
            finally
            {
                if (!success && !e.Cancel && File.Exists(args.OutputPath))
                {
                    try { File.Delete(args.OutputPath); } catch { /*  */ }
                }
                if (e.Cancel && File.Exists(args.OutputPath))
                {
                    try { File.Delete(args.OutputPath); } catch { /*  */ }
                }
            }

            if (success && !e.Cancel)
            {
                e.Result = finalSize;
            }
        }


        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblProgress.Text = $"Progress: {e.ProgressPercentage}%";
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _stopwatch.Stop();
            uiTimer.Stop();

            SetUIState(isRunning: false);
            progressBar.Value = 100; 
            lblProgress.Text = "Progress: 100%";


            if (e.Cancelled)
            {
                lblStatus.Text = "Status: Cancelled";
                MessageBox.Show("Operation was cancelled by the user.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar.Value = 0;
                lblProgress.Text = "Progress: 0%";
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "Status: Error";
                MessageBox.Show($"An unexpected error occurred: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 0;
                lblProgress.Text = "Progress: 0%";
            }
            else if (e.Result is string errorMessage) 
            {
                lblStatus.Text = "Status: Error";
                MessageBox.Show(errorMessage, "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 0;
                lblProgress.Text = "Progress: 0%";
            }
            else if (e.Result is long finalSize) 
            {
                lblStatus.Text = "Status: Done";
                string operationVerb = _currentOperation == OperationType.Encrypt ? "encrypted" : "decrypted";
                MessageBox.Show($"File successfully {operationVerb}!\n\n" +
                                $"Output File: {txtOutputFile.Text}\n" +
                                $"Final Size: {finalSize} bytes\n" +
                                $"Time Taken: {_stopwatch.Elapsed:hh\\:mm\\:ss}",
                                "Operation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                lblStatus.Text = "Status: Unknown Completion State";
                progressBar.Value = 0;
                lblProgress.Text = "Progress: 0%";
            }

            _encryptor = null;
        }
    }
}