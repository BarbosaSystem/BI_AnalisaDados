using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BI_AnalisaDados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection conexao;
        OleDbDataAdapter adapter;
        Aritmetica produto_B;
        Aritmetica produto_A;
        List<Aritmetica> listProduct = new List<Aritmetica>();
        DataSet ds = new DataSet();

        int[] list;
        int[] listB;
        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.DataBindXY(list, listB);
            //// Set title
            //chart1.Titles.Add("Animals");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            btAnalisar.Enabled = false;
            btVisualizar.Enabled = false;
            // define as propriedades do controle
             //OpenFileDialog
             this.ofd1.Multiselect = true;
            this.ofd1.Title = "Selecionar Planilha";
            ofd1.InitialDirectory = @"D:\";
            //filtra para exibir somente arquivos de imagens
            ofd1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.FilterIndex = 2;
            ofd1.RestoreDirectory = true;
            ofd1.ReadOnlyChecked = true;
            ofd1.ShowReadOnly = true;

            DialogResult dr = this.ofd1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                // Le os arquivos selecionados 
                foreach (String arquivo in ofd1.FileNames)
                {
                    textBox1.Text += arquivo;
                    // cria um PictureBox                                 
                }

                conexao = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source= "+ textBox1.Text+"; Extended Properties ='Excel 12.0 Xml; HDR = YES';");
                adapter = new OleDbDataAdapter("select * from[Plan1$]", conexao);
                btAnalisar.Enabled = true;
            }
        }

        private void btAnalisar_Click(object sender, EventArgs e)
        {
            list = null;
            listB = null;
            try
            {
                int count = 0;
                conexao.Open();
                adapter.Fill(ds);


                Array.Resize(ref list, ds.Tables[0].Rows.Count);
                Array.Resize(ref listB, ds.Tables[0].Rows.Count);

                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    int valor = Convert.ToInt16(linha["A"]);
                    list[count] = valor;

                    int valorB = Convert.ToInt16(linha["B"]);
                    listB[count] = valorB;

                    count++;
                }

                produto_A = new Aritmetica("Matriz A", list);
                produto_B = new Aritmetica("Matriz B", listB);
                
                listProduct.Add(produto_A);
                listProduct.Add(produto_B);


                dataGridView1.DataSource = listProduct;
                label3.Text = Correlacao.CorrerPearson(list, listB).ToString("N2");
                btVisualizar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();

            }
        }
    }
}
