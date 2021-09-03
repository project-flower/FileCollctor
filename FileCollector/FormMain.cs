using FileCollector.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FileCollector
{
    public partial class FormMain : Form
    {
        #region Private Fields

        private const string buttonAbortText = "&Abort";
        private readonly string buttonCollectText;
        private string errorMessage = string.Empty;

        #endregion

        #region Public Methods

        public FormMain()
        {
            InitializeComponent();
            MinimumSize = Size;
            MaximumSize = new Size(int.MaxValue, Height);
            Settings settings = Settings.Default;
            comboBoxSource.Text = settings.Source;
            checkBoxRecursive.Checked = settings.Recursive;
            comboBoxFilter.Text = settings.Filter;
            checkBoxRegExpression.Checked = settings.RegExpression;
            comboBoxDestination.Text = settings.Destination;
            checkBoxDirectoryTree.Checked = settings.DirectoryTree;
            buttonCollectText = buttonCollect.Text;
        }

        #endregion

        #region Private Methods

        private void ApplyOptions(MainEngine.Options options)
        {
            comboBoxSource.Text = options.Source;
            checkBoxRecursive.Checked = options.Recursive;
            comboBoxFilter.Text = options.Filter;
            checkBoxRegExpression.Checked = options.RegExpression;
            comboBoxDestination.Text = options.Destination;
            checkBoxDirectoryTree.Checked = options.DirectoryTree;
        }

        private void BeginCollect()
        {
            var options = GetCurrentOptions();
            SetEnabled(false);
            backgroundWorker.RunWorkerAsync(options);
        }

        private void CancelCollect()
        {
            MainEngine.CancellationRequired = true;
        }

        private MainEngine.Options GetCurrentOptions()
        {
            return new MainEngine.Options()
            {
                Destination = comboBoxDestination.Text,
                DirectoryTree = checkBoxDirectoryTree.Checked,
                Filter = comboBoxFilter.Text,
                Recursive = checkBoxRecursive.Checked,
                RegExpression = checkBoxRegExpression.Checked,
                Source = comboBoxSource.Text
            };
        }

        private void SetEnabled(bool enabled)
        {
            foreach (Control control in Controls)
            {
                if ((control as Button) == buttonCollect)
                {
                    buttonCollect.Text = (enabled ? buttonCollectText : buttonAbortText);
                    continue;
                }

                control.Enabled = enabled;
            }
        }

        private void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private DialogResult ShowMessage(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(this, text, Text, buttons, icon);
        }

        #endregion

        // Designer's Methods

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            errorMessage = string.Empty;
            var options = (MainEngine.Options)e.Argument;

            try
            {
                MainEngine.Collect(options);
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ShowErrorMessage(errorMessage);
            }
            else
            {
                string message;
                MessageBoxIcon icon;

                if (MainEngine.Cancelled)
                {
                    message = "中断しました。";
                    icon = MessageBoxIcon.Warning;
                }
                else
                {
                    message = "コピーが完了しました。";
                    icon = MessageBoxIcon.Information;
                }

                ShowMessage(message, MessageBoxButtons.OK, icon);
            }

            SetEnabled(true);
        }

        private void buttonCollect_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                CancelCollect();
            }
            else
            {
                BeginCollect();
            }
        }

        private void comboBoxPath_DragDrop(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(DataFormats.FileDrop) is string[] dropData) || (dropData.Length < 1))
            {
                return;
            }

            (sender as ComboBox).Text = dropData[0];
        }

        private void comboBoxPath_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None);
        }

        private void load(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            if ((args == null) || (args.Length < 2))
            {
                return;
            }

            string[] args_ = new string[args.Length - 1];
            Array.Copy(args, 1, args_, 0, args_.Length);
            var options = GetCurrentOptions();
            CommandLineArgsAnalyzer.Analyze(args_, ref options);
            ApplyOptions(options);
        }
    }
}
