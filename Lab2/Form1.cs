using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Iterative_Methods_Lab2_;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            double[,] A = new double[4, 4]{
                {4.3,0.0217,0.0270,0.0324},
                {0.01,3.4,0.0207,0.0260},
                {0.0037, 0.009, 2.5, 0.0197},
                {-0.0027,0.0027,0.008,1.6}
            };
            double[] B = new double[4] { 2.6632, 2.7779, 2.5330, 1.9285 };
            double[] VecrtorX = new double[4];
            dataGridView2.Columns.Add("","0");
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Columns.Add("", i.ToString());
                dataGridView2.Rows.Add();
                dataGridView2[0, i].Value = B[i];
                for (int j = 0; j < 4; j++)
                {
                    dataGridView1.Rows.Add();                    
                    dataGridView1[i, j].Value = A[i, j];

                }
            }

            Yakobi yakobi = new Yakobi(A, B);
            VecrtorX = yakobi.GetVectorX();
            dataGridView3.Columns.Add("","yakobi");
            for (int i = 0; i < 4; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[0, i].Value = VecrtorX[i];
            }
            textBox1.Text  = yakobi.OperationCount.ToString();

            Zeidel zeidel = new Zeidel(A, B, 1.0);
            VecrtorX = zeidel.GetVectorX();
            dataGridView3.Columns.Add("", "Zeidel");
            for (int i = 0; i < 4; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[1, i].Value = VecrtorX[i];
            }
            textBox1.Text += "\r\n" + yakobi.OperationCount.ToString();

            zeidel = new Zeidel(A, B, 1.9);
            VecrtorX = zeidel.GetVectorX();
            dataGridView3.Columns.Add("", "Relax");
            for (int i = 0; i < 4; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[2, i].Value = VecrtorX[i];
            }
            textBox1.Text += "\r\n" + yakobi.OperationCount.ToString();
        }

        

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
