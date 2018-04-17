using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebConnect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string LastUrl { set; get; }
        private void Form1_Load(object sender, EventArgs e)
        {
            webKitBrowser1.Navigate("http://192.168.149.1/ArasMVC01/Login");
            webKitBrowser1.DocumentCompleted += SetArasLogin;
        }

        private void webKitBrowser1_Load(object sender, EventArgs e)
        {

        }

        private void webKitBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }
        private void SetArasLogin(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webKitBrowser1.Url.ToString() == LastUrl) return;
            if (e.Url.ToString() != DocCompleteUrl.Login) return;
            LastUrl = webKitBrowser1.Url.ToString();

            List<string> loginTag = new List<string>() {"ip","db","admin","pwd" };
            List<string> loginInfo = new List<string>() { "http://192.168.149.1/plm", "InnovatorSolutions", "admin", "innovator" };
            for(int i =0; i < loginInfo.Count(); i++)
            {
                WebKit.DOM.Element element = webKitBrowser1.Document.GetElementById(loginTag[i]);
                element.SetAttribute("value", loginInfo[i]);
            }
           
            webKitBrowser1.StringByEvaluatingJavaScriptFromString("document.getElementById('loginSubmit').click();");
            webKitBrowser1.DocumentCompleted += RunDocumentCompleted;
            //webKitBrowser1.Navigate(DocCompleteUrl.AdminSearch);
        }
        private void RunDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() != DocCompleteUrl.AdminSearch)
            {
                webKitBrowser1.Navigate(DocCompleteUrl.AdminSearch);
                webKitBrowser1.DocumentCompleted += AdminSearchCompleted;
            }
        }
        private void AdminSearchCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webKitBrowser1.Url.ToString() == LastUrl) return;
            if (e.Url.ToString() != DocCompleteUrl.AdminSearch) return;
            webKitBrowser1.Document.GetElementById("btnGetNumber").SetAttribute("type", "hidden");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(webKitBrowser1.Document.GetElementById("seq").TextContent);
        }
    }
    public class DocCompleteUrl 
    {
        public static string Login { set; get; } = "http://192.168.149.1/ArasMVC01/Login";
        public static string AdminSearch { set; get; } = "http://192.168.149.1/ArasMVC01/AdminSearch";
    }
}
