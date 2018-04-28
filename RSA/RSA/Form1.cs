using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        int n, t,j,i;
        int[] e = new int[100];
        int[] d = new int[100];
        int[] temp = new int[100];
        int[] m = new int[100];
        int[]en = new int[100];
        char[] msg ;
        int p, q;


        // numero primos
        public static bool ePrimo(int num)
        {
            int div = 0;

            for (int i = 1; i <= num; i++)
            {
                if (num % i == 0)
                {
                    div++;
                }
            }

            return div == 2; 
        }


        public int cd(int x)
        {
             int k = 1;
            while (true)
            {
                k = k + t;
                if (k % x == 0)
                    return (k / x);
            }
            
        }


        public void ce()
        {
            int k;
            k = 0;
            for ( i = 2; i < t; i++)
            {
                if (t % i == 0)
                    continue;

                    int flag = Convert.ToInt32(ePrimo(i));
                    if (flag == 1 && i != p && i != q)
                    {
                        e[k] = i;
                        flag = cd(e[k]);
                        if (flag > 0)
                        {
                            d[k] = flag;
                            k++;
                        }
                    if (k == 99)
                        break;

                    }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decrypt();
        }

        public void encriptar()
        {
             int pt, ct, key = e[0], k, len;
            i = 0;
            len = textBox3.TextLength;
            while (i != len)
            {
                pt = m[i];
                pt = pt - 96;
                k = 1;
                for (j = 0; j < key; j++)
                {
                    k = k * pt;
                    k = k % n;
                }
                temp[i] = k;
                ct = k + 96;
                en[i] = ct;
                i++;
            }
            en[i] = -1;

            for (i = 0; i< en.Length; i++)
            {
                if (en[i] != -1)
                {
                    textBox1.Text += (char)en[i]; //Convert.ToChar(Convert.ToInt16(en[i]));//(Convert.ToString(Convert.ToChar((en[i]))));
                }
            }
        }

        void decrypt()
        {
             int pt, ct, key = d[0], k;
             i = 0;
            while (en[i] != -1)
            {
                ct = temp[i];
                k = 1;
             for (j = 0; j < key; j++)
             {
                k = k * ct;
                k = k % n;
             }
               pt = k + 96;
               m[i] = pt;
                i++;
            }
                m[i] = -1;

            for (i = 0; m[i] != -1; i++)
            {
                //textBox2.Text = (" " + m[i]);
                textBox2.Text += Convert.ToChar(Convert.ToInt16(m[i]));  //(Convert.ToString(Convert.ToChar((m[i]))));
                
            }


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            textBox3.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;


            Random r = new Random();
             p = r.Next(1, 50);

            
            while (!ePrimo(p))
            {
                p = r.Next(1, 50);
            }

            Random r2 = new Random();
            q = r.Next(1, 50);


            while (!ePrimo(q))
            {
                q = r.Next(1, 50);
            }

            

            n = p * q;
            t = (p - 1) * (q - 1);

            

            
            msg = textBox3.Text.ToCharArray();



            for (i = 0; i < msg.Length; i++) 
            {
                m[i]= msg[i];
                
            }
            ce();
            encriptar();

            label4.Text = "P: " + p;
            label5.Text = "q: " + q;
            label6.Text = "n: " + n;
           

            
            
        }
    }
}
