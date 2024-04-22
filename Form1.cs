using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab_8_TeP
{
    public partial class Form1 : Form
    {
        private int N = 1; // Размерность матрицы по строкам
        private int M = 1; // Размерность матрицы по столбцам
        private double k = 1.0; // Константа k
        private double[,] matrix; // Матрица для хранения значений

        public Form1()
        {
            InitializeComponent();
            InitializeMatrix();
            InitializeFilters();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void InitializeMatrix()
        {
            matrix = new double[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    matrix[i, j] = i != j ? k * Math.Pow(-1, i + j) + j / 10.0 : 0;
                }
            }

            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = M;
            dataGridView1.RowCount = N;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }

        private void InitializeFilters()
        {
            checkBox1.CheckedChanged += ApplyFilters;
            checkBox2.CheckedChanged += ApplyFilters;
            checkBox3.CheckedChanged += ApplyFilters;
            checkBox4.CheckedChanged += ApplyFilters;
        }

        private void ApplyFilters(object sender, EventArgs e)
        {
            // List<double> filteredValues = new List<double>();
            string[,] filteredMatrix = new string[N, M];

            // Заполняем матрицу прочерками
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    filteredMatrix[i, j] = "-";
                }
            }

            // Заполняем отфильтрованные значения в матрицу
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    double value = matrix[i, j];

                    if ((checkBox1.Checked && value > 0) ||
                        (checkBox2.Checked && value < 0) ||
                        (checkBox3.Checked && value == 0) ||
                        (checkBox3.Checked && value == 0))
                    {
                        filteredMatrix[i, j] = value.ToString();
                    }
                }
            }

            // Отображаем отфильтрованную матрицу в ListBox
            DisplayFilteredMatrix(filteredMatrix);
        }

        private void DisplayFilteredMatrix(string[,] matrix)
        {
            listBox1.Items.Clear();

            // Отображаем матрицу в ListBox
            for (int i = 0; i < N; i++)
            {
                string row = "";
                for (int j = 0; j < M; j++)
                {
                    row += matrix[i, j] + "\t";
                }
                listBox1.Items.Add(row);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            N = Math.Max(1, Math.Min(10, hScrollBar1.Value));
            InitializeMatrix();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            M = Math.Max(1, Math.Min(10, hScrollBar2.Value));
            InitializeMatrix();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            k = Math.Max(-5.5, Math.Min(4.5, (double)numericUpDown1.Value));
            InitializeMatrix();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
