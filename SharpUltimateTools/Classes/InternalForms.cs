using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Microsoft.CSharp.Tools
{
    /// <summary>
    /// Allows use of some Embedded WinForms.
    /// </summary>
    public static class InternalForms
    {
        /// <summary>
        /// Displays a Form that runs various command to fix the Internet connection.
        /// </summary>
        public sealed class FixInternet : Form
        {
            internal Label lblInfo;
            internal CheckedListBox cbxCheck;
            internal ProgressBar prbOverall;
            internal ListBox lstStatus;
            internal BackgroundWorker backgroundWorker;

            internal void InitalizeComponent()
            {
                this.lblInfo = new Label();
                this.cbxCheck = new CheckedListBox();
                this.prbOverall = new ProgressBar();
                this.lstStatus = new ListBox();
                this.backgroundWorker = new BackgroundWorker();
                this.SuspendLayout();
                //
                // label
                //
                this.lblInfo.AutoSize = true;
                this.lblInfo.Location = new Point(34, 9);
                this.lblInfo.Name = "lblInfo";
                this.lblInfo.Size = new Size(137, 13);
                this.lblInfo.TabIndex = 0;
                this.lblInfo.Text = "Press Start To Continue . . .";
                //
                // cbxCheck
                //
                this.cbxCheck.CheckOnClick = true;
                this.cbxCheck.Enabled = false;
                this.cbxCheck.FormattingEnabled = true;
                this.cbxCheck.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" });
                this.cbxCheck.Location = new Point(12, 27);
                this.cbxCheck.Name = "cbxCheck";
                this.cbxCheck.Size = new Size(19, 169);
                this.cbxCheck.TabIndex = 1;
                //
                // prbOverall
                //
                this.prbOverall.Location = new Point(12, 202);
                this.prbOverall.Name = "prbOverall";
                this.prbOverall.Size = new Size(260, 13);
                this.prbOverall.TabIndex = 3;
                //
                // lstStatus
                //
                this.lstStatus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                this.lstStatus.FormattingEnabled = true;
                this.lstStatus.ItemHeight = 15;
                this.lstStatus.Location = new Point(37, 27);
                this.lstStatus.Name = "lstStatus";
                this.lstStatus.Size = new Size(235, 169);
                this.lstStatus.TabIndex = 4;
                //
                // backgroundWorker
                //
                this.backgroundWorker.DoWork += backgroundWorkerDoWork;
                this.backgroundWorker.RunWorkerCompleted += backgroundWorkerRunWorkerCompleted;
                //
                // FixInternet
                //
                this.AutoScaleDimensions = new SizeF(6F, 13F);
                this.AutoScaleMode = AutoScaleMode.Font;
                this.ClientSize = new Size(284, 222);
                this.ControlBox = false;
                this.Controls.Add(this.lstStatus);
                this.Controls.Add(this.prbOverall);
                this.Controls.Add(this.cbxCheck);
                this.Controls.Add(this.lblInfo);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.Name = "FixInternet";
                this.Text = "Fix Internet";
                this.Load += FormLoad;
                this.ResumeLayout(false);
                this.PerformLayout();
            }

            /// <summary>
            /// This is the form object constructor
            /// </summary>
            public FixInternet() { InitalizeComponent(); }

            internal Int32 indexnum;
            internal Int32 percent = 100 / 11;
            internal Boolean mediadisconnected;

            internal readonly String[] commands =
            {
                    "Starting Please Wait...",
                    "netsh winsock reset catalog",
                    "netsh interface ip reset all",
                    "netsh interface ip delete arpcache",
                    "nbtstat -R",
                    "nbtstat -RR",
                    "ipconfig /flushdns",
                    "ipconfig /registerdns",
                    "ipconfig /release",
                    "ipconfig /renew",
                    "Process Complete!"
                    };

            internal void FormLoad(Object sender, EventArgs e)
            {
                for (Int32 i = 0; i < 11; i++) lstStatus.Items.Add(commands[i]);
                start();
            }

            internal void start()
            {
                indexnum = 0;
                prbOverall.Value = 0;
                lblInfo.Text = "Fixing Internet, Please Wait . . .";
                check(0);
            }

            internal void check(int index)
            {
                prbOverall.Value += percent;
                lstStatus.SelectedIndex = index;
                cbxCheck.SetItemCheckState(index, CheckState.Checked);
                backgroundWorker.RunWorkerAsync();
            }

            internal void backgroundWorkerDoWork(object sender, DoWorkEventArgs e)
            {
                if (indexnum != 0 && indexnum != 10)
                {
                    if (commands[indexnum] == "ipconfig /release")
                    {
                        mediadisconnected = !CommandInfo.Run(commands[indexnum], true).Result.Contains(":");
                    }
                }
                System.Threading.Thread.Sleep(100);
            }

            internal void backgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                indexnum++;
                if (indexnum != 11) check(indexnum);
                else
                {
                    prbOverall.Value = 100;
                    lblInfo.Text = "Process Complete!";

                    if (mediadisconnected)
                    {
                        MessageBox.Show("Please Reconnect To Wireless Network or \nReconnect Internet Cable and Rerun this tool.", "Network Media Disconnected!");
                        mediadisconnected = false;
                    }

                    Close();
                }
            }
        }
    }
}
