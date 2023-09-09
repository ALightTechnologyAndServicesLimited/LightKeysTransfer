using LightKeysTransfer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightKeysTransfer.WinClient
{
    public partial class frmMain : Form
    {
        private CryptHelper cryptHelper = new CryptHelper();
        private string plainText;
        private int counter;

        public frmMain()
        {
            InitializeComponent();
            tabControl1.TabPages[0].Text = "Small Data";
            tabControl1.TabPages[0].Text = "Larger Data";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnGenerateKeyPair_Click(object sender, EventArgs e)
        {
            cryptHelper.GenerateRSAKeyPair();
        }

        private void btnCopyPublicKey_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(cryptHelper.GetPublicKey());
            tmr.Enabled = true;
        }

        private void btnSendPublicKey_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            SendKeys.SendWait(cryptHelper.GetPublicKey());
            SendKeys.Flush();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                plainText = cryptHelper.DecryptRSA(txtEncrypted.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopyPlainText_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(plainText);
        }

        private void btnSendPlainText_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            SendKeys.SendWait(plainText);
            SendKeys.Flush();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > 50)
            {
                counter = 0;
                Clipboard.Clear();
                tmr.Enabled = false;
            }
        }

        private void btnClearClipBoard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            tmr.Enabled = false;
            counter = 0;
        }
    }
}
