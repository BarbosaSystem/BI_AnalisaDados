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
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                
            }
        }

        private void btAnalisar_Click(object sender, EventArgs e)
        {
            
            try
            {

                int[] list = new int[5];
                int[] listB = new int[5];
                //array[0] = min ; array[1] = max ; array[2] = soma ; array[3] = media
                int count = 0;
                conexao.Open();
                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    int valor = Convert.ToInt16(linha["A"]);
                    list[count] = valor;

                    int valorB = Convert.ToInt16(linha["B"]);
                    listB[count] = valorB;

                    count++;
                }

                produto_A = new Aritmetica("Produto A", list);
                produto_B = new Aritmetica("Produto B", listB);
                
                listProduct.Add(produto_A);
                listProduct.Add(produto_B);


                dataGridView1.DataSource = listProduct;
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
