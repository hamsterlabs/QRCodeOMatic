using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace HamsterLabs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            string str = txtEncode.Text;
            if (String.IsNullOrWhiteSpace(str)) return;
            QRCodeEncoder enc = new QRCodeEncoder();
            Bitmap qrcode = enc.Encode(str);
            pbox.Image = qrcode as Image;

            txtDecode.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files (.jpg|*.jpg|.png|*.png|All Files (*.*)|*.*)";
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                try
                {
                    Bitmap qr = (Bitmap)pbox.Image;
                    string file = dlg.FileName;
                    qr.Save(file);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            txtEncode.Text = string.Empty;
            txtDecode.Text = string.Empty;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (.jpg|*.jpg|.png|*.png|All Files (*.*)|*.*)";
            dlg.CheckPathExists = true;
            dlg.Title = "Open Image File";
            dlg.Multiselect = false;
            if (DialogResult.OK == dlg.ShowDialog(this))
            {

                try
                {
                    //Bitmap img = (Bitmap)Image.FromFile(dlg.FileName);
                    pbox.Image = Image.FromFile(dlg.FileName);

                    QRCodeDecoder dec = new QRCodeDecoder();
                    txtDecode.Text = (dec.Decode(new QRCodeBitmapImage(pbox.Image as Bitmap)));
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            QRCodeDecoder dec = new QRCodeDecoder();
            txtDecode.Text = (dec.Decode(new QRCodeBitmapImage(pbox.Image as Bitmap)));       
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDecode.Text = String.Empty;
            txtEncode.Text = String.Empty;
            pbox.Image = null;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "QRCodeOMatic 0.1", "HamsterLabs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
