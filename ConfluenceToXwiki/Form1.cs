using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfluenceToXwiki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uxNavigate_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(uxURL.Text);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            uxURL.Text = Convert.ToString(webBrowser1.Document.Url);
        }

        private void uxConvert_Click(object sender, EventArgs e)
        {
            try
            {
                var pageCapture = webBrowser1.Document.Body.InnerHtml;
                conversion.sortData(pageCapture, uxURL.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Oops, something went wrong!\nWhat that something is, your guess is as good as mine.");
            }
}

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(uxURL.Text);
        }
    }
}
