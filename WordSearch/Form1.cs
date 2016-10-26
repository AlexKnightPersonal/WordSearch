using System;
using System.Collections;
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
            //Init random
            rand = new Random();
            //Default words
            words = new ArrayList() { "integer", "array", "byte", "boolean", "for", "string", "parameter", "while", "method", "loop"};
            //Display the default wors
            DisplayWords();
        }

        //Size of the wordsearch
        private int size;
        //The random var
        private readonly Random rand;
        //The words in the word search
        private ArrayList words;
        //Letters for filling cells
        private const string letters = "abcdefghijklmnopqrstuvwxyz";

        private void btnGo_Click(object sender, EventArgs e)
        {
            //Get size of Word Search
            if (!GetSize())
                return;

            //Set size of Word Search
            dataGridView1.RowCount = dataGridView1.ColumnCount = size;
            //Ensure grid is refreshed every time
            EmptyGrid();
            //Fill the word search
            WriteWords();
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            //If it's not all letter show a message
            if (!txtAddWord.Text.All(char.IsLetter)
                || txtAddWord.Text == "")
            {
                MessageBox.Show("The value entered for size was not accepted." +
                                "\n Please only enter letters", "Invalid Input");
                return;
            }
            //Add the word
            words.Add(txtAddWord.Text);
            //Clear the text box
            txtAddWord.Text = "";
            //Refresh word display
            DisplayWords();
        }

        private void btnRemoveWord_Click(object sender, EventArgs e)
        {
            //Check if the word is in the list, if so remove it
            foreach (string word in words)
            {
                if (txtRemoveWord.Text != word) continue;
                words.Remove(word);
                break;
            }
            //Clear the text box
            txtRemoveWord.Text = "";
            //Refresh word display
            DisplayWords();
        }

        private void DisplayWords()
        {
            //Clear the box
            rtxtWords.Text = "";
            //Write out the words on seperate lines
            foreach (var word in words)
            {
                rtxtWords.Text += word + "\n";
            }
        }

        private void WriteWords()
        {
            //Pick a random direction and write the word
            foreach (string word in words)
            {
                PickDirection(word, rand.Next(4));
            }

            //Fill all the empty cells with random chars
            foreach (var cell in from DataGridViewRow row in dataGridView1.Rows
                                 from DataGridViewCell cell in row.Cells
                                 select cell)
            {
                if (cell.Value == "")
                    cell.Value = RandomChar();
            }
        }

        private char RandomChar()
        {
            //Getting a random char
            return letters[rand.Next(letters.Length)];
        }

        private void PickDirection(string word, int direction)
        {
            //Placing word in different directions
            switch (direction)
            {
                case 0:
                    WriteUp(word);
                    break;
                case 1:
                    WriteRight(word);
                    break;
                case 2:
                    WriteDown(word);
                    break;
                case 3:
                    WriteLeft(word);
                    break;
            }
        }

        private bool GetSize()
        {
            //Show error if size isn't a number
            try
            {
                size = Convert.ToInt32(txtSize.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("The value entered for size was not accepted." +
                                "\n Please try again", "Invalid Input");
                return false;
            }
            return true;
        }

        //TODO
        private void CheckSize()
        {
            //Check whether size < longest word
        }

        private void WriteDown(string word)
        {
            //Get column and starting row
            var column = rand.Next(size);
            var startRow = rand.Next(size);
            //Ensure word doesn't overflow grid
            if ((startRow + word.Length) >= size)
                startRow -= ((startRow + word.Length) - size);
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
            var column = rand.Next(size);
            var startRow = rand.Next(size);
            //Ensure word doesn't overflow grid
            if ((startRow - word.Length) < 0)
                startRow -= startRow - word.Length;

            //Write word
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                dataGridView1.Rows[startRow - step].Cells[column].Value = c;
                step++;
            }
        }

        private void WriteLeft(string word)
        {
            //Get column and starting row
            var row = rand.Next(size);
            var startColumn = rand.Next(size);
            //Ensure word doesn't overflow grid
            if ((startColumn - word.Length) < 0)
                startColumn -= startColumn - word.Length;

            //Write word
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                dataGridView1.Rows[row].Cells[startColumn - step].Value = c;
                step++;
            }
        }

        private void WriteRight(string word)
        {
            //Get column and starting row
            var row = rand.Next(size);
            var startColumn = rand.Next(size);
            //Ensure word doesn't overflow grid
            if ((startColumn + word.Length) >= size)
                startColumn -= ((startColumn + word.Length) - size);
            //Write word
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                dataGridView1.Rows[row].Cells[startColumn + step].Value = c;
                step++;
            }
        }

        private void EmptyGrid()
        {
            //Set value in every cell to empty
            foreach (var cell in from DataGridViewRow row in dataGridView1.Rows
                                 from DataGridViewCell cell in row.Cells
                                 select cell)
            {
                cell.Value = string.Empty;
            }
        }

        private static void CheckWordKeypress(KeyPressEventArgs e)
        {
            //Ignore keypress if it isn't a letter
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only allow numbers
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAddWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeypress(e);
        }

        private void txtRemoveWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeypress(e);
        }
    }
}
