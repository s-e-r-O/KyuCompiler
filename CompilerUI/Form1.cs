using KyuCompilerF;
using KyuCompilerF.Models;
using KyuCompilerF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerUI
{
    public partial class Form1 : Form
    {
        int countSaving = 0;
        int environmentColor = 0;
        string pathSelectedFile = "";
        string fileName = "";
        string[] keywordsList = new string[] { "change", "changed", "given", "otherwise", "done", "return", "forevery", "forever", "done", "in", "stop", "kyu#", "is", "function" };

        Color keywordColor = Color.MediumBlue;
        Color cadenaColor = Color.DarkRed;
        Color wordsColor = Color.Black;

        Tokenizer tokenizer = new Tokenizer();

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            StopButton.Visible = false;
            LineNumberTextBox.ReadOnly = true;
            LineNumberTextBox.Font = TextEditorTextBox.Font;
        }


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofd.FileName;
                pathSelectedFile = ofd.InitialDirectory + fileName;
            }

            string contentOfFile = File.ReadAllText(pathSelectedFile);
            TextEditorTextBox.Text = contentOfFile;
        }

        public int getAnchoTextEditor()
        {
            int w = 25;
            
            int line = TextEditorTextBox.Lines.Length;      

            if (line <= 99)
            {
                w = 20 + (int)TextEditorTextBox.Font.Size;
            }

            else if (line <= 999)
            {
                w = 30 + (int)TextEditorTextBox.Font.Size;
            }

            else
            {
                w = 50 + (int)TextEditorTextBox.Font.Size;
            }

            return w;
        }

        public void AddLineNumeration()
        {
            Point p = new Point(30, 0);

            int primerIndice = TextEditorTextBox.GetCharIndexFromPosition(p);
            int primeraLinea = TextEditorTextBox.GetLineFromCharIndex(primerIndice);

            p.X = ClientRectangle.Width;
            p.Y = ClientRectangle.Height;

            int ultimoIndice = TextEditorTextBox.GetCharIndexFromPosition(p);
            int ultimaLinea = TextEditorTextBox.GetLineFromCharIndex(ultimoIndice);
            
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getAnchoTextEditor();

            for (int i = primeraLinea; i <= ultimaLinea; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        private void TextEditorTextBox_TextChanged(object sender, EventArgs e)
        {
            AddLineNumeration();
            highligthKeywords(0);
        }

        private void highligthKeywords(int startIndex)
        {
            string word;
            for (int i = 0; i < keywordsList.Length; i++)
            {
                word = keywordsList[i];
                if (this.TextEditorTextBox.Text.Contains(word))
                {
                    int index = -1;
                    int selectStart = this.TextEditorTextBox.SelectionStart;

                    while ((index = this.TextEditorTextBox.Text.IndexOf(word, (index + 1))) != -1)
                    {
                        this.TextEditorTextBox.Select((index + startIndex), word.Length);
                        this.TextEditorTextBox.SelectionColor = keywordColor;
                        this.TextEditorTextBox.Select(selectStart, 0);
                        this.TextEditorTextBox.SelectionColor = wordsColor;
                    }
                }
            }
        }


        private void TextEditorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AddLineNumeration();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveFile()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Kyu# File|*.kyu";
            saveFileDialog1.Title = "Save a kyu# file";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                pathSelectedFile = saveFileDialog1.FileName;
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

                byte[] data = new UTF8Encoding(true).GetBytes(TextEditorTextBox.Text);
                fs.Write(data, 0, TextEditorTextBox.Text.Length);
                fs.Close();
                countSaving = countSaving + 1;
            }
        }

        private void moreInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/s-e-r-O/KyuCompiler/wiki/Documentation");
        }

        private void exceptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/s-e-r-O/KyuCompiler/wiki/Exceptions");
        }

        private void codeExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/s-e-r-O/KyuCompiler/wiki/Code-Sample");
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            StopButton.Visible = true;
            TextEditorTextBox.ReadOnly = true;
            PlayButton.Enabled = false;

            if (countSaving == 0)
            {
                saveFile();
            }
            else
            {
                string text = TextEditorTextBox.Text;
                System.IO.File.WriteAllText(pathSelectedFile, text);
            }

            Lector l = new Lector();
            try
            {
                string[] file = l.Leer(pathSelectedFile, false);
                Tokenizer t = new Tokenizer();
                List<Token> tokens = t.Analizar(file).ToList();
                Parser p = new Parser();
                p.CalcularLL1(KyuValues.Gramatica);
                Nodo raiz = p.Evaluar(tokens);
                SemanticAnalyzer s = new SemanticAnalyzer();
                s.Evaluar(raiz);
                OutputLabel.Text = "Compiled Successfully!\n" + TablaSimbolo.Tabla;
            }
            catch (AggregateException except)
            {
                tabPage2.Text = "(1) Error List";
                OutputLabel.Text = "Couldn't compile successfully.\nError thrown at " + pathSelectedFile + "\n";
                ErrorLabel.ForeColor = Color.Red;
                string err = "";
                except.Handle(ex => {
                    err += ex + "\n";
                    return true;
                });
                ErrorLabel.Text = err;
            }
            catch (Exception excep)
            {
                tabPage2.Text = "(1) Error List";
                OutputLabel.Text = "Couldn't compile successfully.\nError thrown at " + pathSelectedFile + "\n";
                ErrorLabel.ForeColor = Color.Red;
                ErrorLabel.Text = excep.ToString();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopButton.Visible = false;
            PlayButton.Enabled = true;
            TextEditorTextBox.ReadOnly = false;
            OutputLabel.Text = "";
            ErrorLabel.Text = "";
            tabPage2.Text = "Error List";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (countSaving == 0)
            {
                saveFile();
            }
            else
            {
                string text = TextEditorTextBox.Text;
                System.IO.File.WriteAllText(pathSelectedFile, text);
            }
        }

        private void themeColor(Color colorForm, Color color)
        {
            this.BackColor = colorForm;

            TextEditorTextBox.BackColor = color;
            LineNumberTextBox.BackColor = color;
            tabPage1.BackColor = color;
            tabPage2.BackColor = color;
        }

        private void TextEditorTextBox_CursorChanged(object sender, EventArgs e)
        {

        }

        private void darkNigthToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            environmentColor = 2;

            themeColor(Color.Black, Color.FromArgb(0, 0, 64));
            keywordColor = Color.DarkTurquoise;
            cadenaColor = Color.DarkOrange;
            wordsColor = Color.RosyBrown;
            OutputLabel.ForeColor = Color.RosyBrown;
            TextEditorTextBox.ForeColor = wordsColor;
            highligthKeywords(0);
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            environmentColor = 1;
            
            themeColor(Color.WhiteSmoke, Color.White);
            keywordColor = Color.MediumBlue;
            cadenaColor = Color.DarkRed;
            wordsColor = Color.Black;
            OutputLabel.ForeColor = Color.Black;
            TextEditorTextBox.ForeColor = wordsColor;
            highligthKeywords(0);
        }

        private void LineNumberTextBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (environmentColor == 2)
            {
                LineNumberTextBox.BackColor = Color.FromArgb(0, 0, 64);
            }
            else
            {
                LineNumberTextBox.BackColor = Color.White;
            }
            
        }

        private void TextEditorTextBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (environmentColor == 2)
            {
                TextEditorTextBox.BackColor = Color.FromArgb(0, 0, 64);
            }
            else
            {
                TextEditorTextBox.BackColor = Color.White;
            }
        }
    }
}
