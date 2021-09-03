using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RSA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        CryptRsa cryptrsa= new CryptRsa();
        public string ChekingNum(string num, string label)
        {
            Regex rx = new Regex("[a-z|а-я]");

            if ((num!="") & (rx.IsMatch(num)!=true) & (num != " "))
            {
                if (PrimeCheking(int.Parse(num)) == true)
                {
                    label = "true";
                }
                if (PrimeCheking(int.Parse(num)) == false)
                {
                    label = "false";
                }
                return label;
            }
            else
            {
                return (label = "undef");
            }
        }

        public bool PrimeCheking(int num)
        {
            for (long i = 2; i <= Math.Sqrt(num); i++)
                if (num % i == 0)
                    return false;
            return true;
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            label2.Text = ChekingNum(textBox3.Text, label2.Text);
            ColorSet(label2);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            label4.Text = ChekingNum(textBox4.Text, label4.Text);
            ColorSet(label4);
        }

        public void ColorSet(Label label)
        {
            if (label.Text == "true")
            {
                label.ForeColor = Color.Green;
            }
            if (label.Text == "false")
            {
                label.ForeColor = Color.Red;
            }
            if (label.Text == "undef")
            {
                label.ForeColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] A=new int[1000];
            cryptrsa.p = int.Parse(textBox3.Text);
            cryptrsa.q = int.Parse(textBox4.Text);

            A = cryptrsa.EComputing();

            for (int i = 0; A[i]!=0; i++)
            {
                comboBox1.Items.Add(A[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string key;

            cryptrsa.e = int.Parse(comboBox1.Text);
            key ="{"+comboBox1.Text+", "+cryptrsa.n+"}";
            label8.Text = key;
            cryptrsa.DComputing();
            key = "{" + cryptrsa.d + ", " + cryptrsa.n + "}";
            label10.Text = key;

            string str = textBox1.Text+"0";
            char[] ar = str.ToArray<char>();

            ar = cryptrsa.Crypting(ar);
            str = new string(ar);
            str=str.Remove(str.Length - 1);
            textBox2.Text = str;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = textBox2.Text+"0";
            char[] ar = str.ToArray<char>();

            ar = cryptrsa.Decrypting(ar);
            str = new string(ar);
            str=str.Remove(str.Length - 1);
            textBox1.Text = str;
        }
    }
}
