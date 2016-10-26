using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            rand = new Random();
        }

        private int size;
        private readonly Random rand;

        private void btnGo_Click(object sender, EventArgs e)
        {
            //Get size of Word Search
            size = Convert.ToInt32(txtSize.Text);

            //For debug
            //size = 6;

            //Set size of Word Search
            dataGridView1.RowCount = dataGridView1.ColumnCount = size;
            //Ensure grid is refreshed every time
            EmptyGrid();


            WriteUp("alex");
        }

        private void WriteDown(string word)
        {
            //Get column and starting row
            int column = rand.Next(size);
            int startRow = rand.Next(size);
            //Ensure word doesn't overflow grid
            if ((startRow + word.Length) > size)
                startRow = (startRow- word.Length) < 0 ? startRow - (size - word.Length) : startRow -= word.Length;

            //Write word
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                dataGridView1.Rows[startRow + step].Cells[column].Value = c;
                step++;
            }
        }

        private void WriteUp(string word)
        {
            //Get column and starting row
            int column = rand.Next(size);
            int startRow = rand.Next(size);
            //Ensure word doesn't overflow grid
            if ((startRow - word.Length) < 0)
                startRow = startRow + word.Length > size ? startRow + (word.Length - startRow) : startRow += word.Length - 1;

            //Write word
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                dataGridView1.Rows[startRow - step].Cells[column].Value = c;
                step++;
            }
        }



        private void EmptyGrid()
        {
            foreach (var cell in from DataGridViewRow row in dataGridView1.Rows
                                 from DataGridViewCell cell in row.Cells
                                 select cell)
            {
                cell.Value = string.Empty;
            }
        }
    }
}
