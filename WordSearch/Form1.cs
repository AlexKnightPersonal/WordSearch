using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            words = new ArrayList()
            {
                "integer",
                "array",
                "byte",
                "boolean",
                "for",
                "string",
                "parameter",
                "while",
                "method",
                "loop"
            };
            //Display the default wors
            DisplayWords();
        }

        //Size of the wordsearch
        private int size;
        //The random var
        public static Random rand;
        //The words in the word search
        private ArrayList words;
        //Letters for filling cells
        private const string letters = "abcdefghijklmnopqrstuvwxyz";

        private List<Word> wordsInCells;

        private void btnGo_Click(object sender, EventArgs e)
        {
            //Get size of Word Search
            if (!GetSize())
                return;

            //Check size of the grid
            if (!CheckSize())
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
            //If it's not all letters show a message
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

        private void btnShowAnswers_Click(object sender, EventArgs e)
        {
            //Avoid error if grid is not setup
            if (wordsInCells == null)
                return;

            if (btnShowAnswers.Text == "Show Answers")
            {
                showAnswers();
                btnShowAnswers.Text = "Hide Answers";
            }
            else
            {
                hideAnswers();
                btnShowAnswers.Text = "Show Answers";
            }
        }

        private void showAnswers()
        {
            //Highlight words
            foreach (Word word in wordsInCells)
            {
                int rowChange = getRowChange(word.Direction);
                int columnChange = getColumnChange(word.Direction);

                var step = 0;
                for (int i = 0; i < word.Length; i++)
                {
                    dataGridView1.Rows[word.Row + (rowChange*i)].Cells[word.Column + (columnChange*i)].Style.BackColor =
                        Color.LightGreen;
                }
            }
        }

        private void hideAnswers()
        {
            //UnHighlight words (Instead of doing all)

            foreach (Word word in wordsInCells)
            {
                int rowChange = getRowChange(word.Direction);
                int columnChange = getColumnChange(word.Direction);

                var step = 0;
                for (int i = 0; i < word.Length; i++)
                {
                    dataGridView1.Rows[word.Row + (rowChange*i)].Cells[word.Column + (columnChange*i)].Style.BackColor =
                        Color.White;
                }
            }
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
            wordsInCells = new List<Word>();
            //Pick random direction, and search for a suitable space for the word until found
            foreach (string word in words)
            {
                int direction;
                int row;
                int column;
                int rowChange;
                int columnChange;
                do
                {
                    direction = rand.Next(4);
                    //direction = 0;
                    rowChange = getRowChange(direction);
                    columnChange = getColumnChange(direction);
                    column = rand.Next(size);
                    row = rand.Next(size);

                    //Checking for overflow edge of grid
                    switch (direction)
                    {
                        case 0:
                            row = checkOverflowUp(row, word.Length);
                            break;
                        case 1:
                            column = checkOverflowRight(column, word.Length);
                            break;
                        case 2:
                            row = checkOverflowDown(row, word.Length);
                            break;
                        case 3:
                            column = checkOverflowLeft(column, word.Length);
                            break;
                    }

                } while (!isValidStart(row, column, rowChange, columnChange, word));

                //Writing the actual word
                var step = 0;
                foreach (var c in word.ToCharArray())
                {
                    dataGridView1.Rows[row + (rowChange*step)].Cells[column + (columnChange*step)].Value = c;
                    step++;
                }
                wordsInCells.Add(new Word(word, row, column, direction));

            }

            //Fill all the empty cells with random chars
            foreach (var cell in from DataGridViewRow gridRow in dataGridView1.Rows
                from DataGridViewCell cell in gridRow.Cells
                select cell)
            {
                if (cell.Value == "")
                    cell.Value = RandomChar();
            }
        }

        private bool isValidStart(int row, int column, int rowChange, int columnChange, string word)
        {
            //Check whether start is actually valid
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                if (dataGridView1.Rows[row + (rowChange*step)].Cells[column + (columnChange*step)].Value == "")
                {
                    step++;
                    continue;
                }
                if (c.Equals(dataGridView1.Rows[row + (rowChange*step)].Cells[column + (columnChange*step)].Value))
                {
                    step++;
                    continue;
                }
                return false;
            }
            return true;
        }

        private int getRowChange(int direction)
        {
            switch (direction)
            {
                case 0:
                    return -1;
                case 2:
                    return 1;
                default:
                    return 0;
            }  
        }

        private int getColumnChange(int direction)
        {
            switch (direction)
            {
                case 1:
                    return 1;
                case 3:
                    return -1;
                default:
                    return 0;
            }
        }


        private char RandomChar()
        {
            //Getting a random char
            return letters[rand.Next(letters.Length)];
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

        private bool CheckSize()
        {
            //Set a max size of 40 (still 1.6k boxes)
            if (size > 40)
            {
                MessageBox.Show("The value entered for size is too large" +
                                "\nThe max size available is 40", "Invalid Size");
                return false;
            }

            //Make sure size = longest word x 2
            foreach (string word in words)
            {
                if (word.Length*2 <= size) continue;
                MessageBox.Show("The value entered for size is too small" +
                                "\nIt needs to be double the length of the longest word" +
                                "\n\"" + word + "\" is too long for the size", "Invalid Size");
                return false;
            }

            return true;
        }

        private int checkOverflowUp(int row, int length)
        {
            if ((row - length) < 0)
                row -= row - length;
            return row;
        }

        private int checkOverflowDown(int row, int length)
        {
            if ((row + length) >= size)
                row -= ((row + length) - size);
            return row;
        }

        private int checkOverflowLeft(int column, int length)
        {
            if ((column - length) < 0)
                column -= column - length;
            return column;
        }

        private int checkOverflowRight(int column, int length)
        {
            if ((column + length) >= size)
                column -= ((column + length) - size);
            return column;
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
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnGo_Click(sender, new EventArgs());
        }

        private void txtAddWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddWord_Click(sender, new EventArgs());
        }

        private void txtRemoveWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnRemoveWord_Click(sender, new EventArgs());
        }
    }

    internal class Word
    {
        public int Length { get; }
        public int Row { get; }
        public int Column { get; }
        public int Direction { get; }

        public Word(string word, int row, int column, int direction)
        {
            this.Length = word.Length;
            this.Row = row;
            this.Column = column;
            this.Direction = direction;
        }
    }
}
