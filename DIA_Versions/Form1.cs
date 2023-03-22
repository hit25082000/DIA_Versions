using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace DIA_Versions
{
    public partial class Form1 : Form
    {
        public int _versaoAtual { get; set; }

        Versions versions = new Versions(); 
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // This will make the form non-resizable
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void treeView1_ItemDrag(object sender, TreeViewEventArgs e)
        {

        }
        

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Obter o nó clicado.
                TreeNode node = treeView1.GetNodeAt(e.X, e.Y);

                if (node != null)
                {
                    // Selecionar o nó clicado.
                    treeView1.SelectedNode = node;

                    // Exibir o menu de contexto.
                    contextMenuStrip1.Show(treeView1, e.Location);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void AddNodeTextsToAutoComplete(TreeNode node, AutoCompleteStringCollection collection)
        {
            // adicionar o valor de texto do nó atual ao objeto AutoCompleteStringCollection
            collection.Add(node.Text);            
        }
          
        private void button1_Click(object sender, EventArgs e)
        {
           var versao = versions.ProcurarVersaoPorNome(textBox1.Text, treeView1.Nodes);

            if (versao != null)
            {
                treeView1.SelectedNode = versao; 
                treeView1.SelectedNode.Expand();
            }else
                MessageBox.Show("Versão não encontrada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Botão de adicionar
        private void button2_Click(object sender, EventArgs e)
        {
            var versaoSelcionada = versions.ProcurarVersaoPorNome(textBox1.Text, treeView1.Nodes);

            if (versaoSelcionada != null)
                versions.AdicionarNota(textBox2.Text, versaoSelcionada);
            else
                MessageBox.Show("Versão não encontrada","Erro",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            versions.AdicionarVersao(textBox1.Text, treeView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();

            // adicionar os valores de texto dos nós da TreeView ao objeto AutoCompleteStringCollection
            foreach (TreeNode node in treeView1.Nodes)
            {
                AddNodeTextsToAutoComplete(node, autoCompleteCollection);
            }

            // definir a propriedade AutoCompleteCustomSource do TextBox para o objeto AutoCompleteStringCollection
            textBox1.AutoCompleteCustomSource = autoCompleteCollection;

            // ativar o recurso de auto complete para o TextBox
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {            
        }        

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void SalvarTreeViewLocalmente()
        {
            // Cria uma instância do SaveFileDialog
            SaveFileDialog salvarArquivo = new SaveFileDialog();

            // Define as propriedades do SaveFileDialog
            salvarArquivo.Filter = "Arquivo XML (*.xml)|*.xml";
            salvarArquivo.Title = "Salvar TreeView";

            // Exibe o SaveFileDialog e obtém o resultado
            DialogResult resultado = salvarArquivo.ShowDialog();

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.IndentChars = "\t";

            // Verifica se o usuário selecionou um arquivo
            if (resultado == DialogResult.OK)
            {
                // Cria um objeto XmlTextWriter
                XmlWriter writerIdentado = XmlWriter.Create(salvarArquivo.FileName, settings);

                // Escreva a declaração XML
                writerIdentado.WriteStartDocument(true);

                // Escreva o elemento raiz
                writerIdentado.WriteStartElement("TreeView");

                // Chame uma função auxiliar recursiva para percorrer a TreeView e gravar seus nós e subnós
                SalvarTreeView(writerIdentado, treeView1.Nodes);


                // Feche o elemento raiz e o documento XML
                writerIdentado.WriteEndElement();
                writerIdentado.WriteEndDocument();

                // Feche o objeto XmlTextWriter
                writerIdentado.Close();                
            }
        }

        private void SalvarTreeView(XmlWriter writer, TreeNodeCollection nodes)
        {
            // Percorra os nós na coleção
            foreach (TreeNode node in nodes)
            {
                // Escreva o nó atual como um elemento XML
                writer.WriteStartElement("Versão");
                writer.WriteAttributeString("Text", node.Text);
                writer.WriteAttributeString("Checked", node.Checked.ToString());

                // Chame a função recursivamente para gravar os subnós deste nó
                SalvarTreeViewNode(writer, node.Nodes);

                // Feche o elemento XML do nó
                writer.WriteEndElement();
            }
        }

        private void SalvarTreeViewNode(XmlWriter writer, TreeNodeCollection nodes)
        {
            // Percorra os nós na coleção
            foreach (TreeNode node in nodes)
            {
                // Escreva o nó atual como um elemento XML
                writer.WriteStartElement("Nota");
                writer.WriteAttributeString("Text", node.Text);
                writer.WriteAttributeString("Checked", node.Checked.ToString());

                
                // Feche o elemento XML do nó
                writer.WriteEndElement();
            }
        }

        private void ImportarTreeView()
        {
            // Cria uma instância do OpenFileDialog
            OpenFileDialog abrirArquivo = new OpenFileDialog();

            // Define as propriedades do OpenFileDialog
            abrirArquivo.Filter = "Arquivo XML (*.xml)|*.xml";
            abrirArquivo.Title = "Importar TreeView";

            // Exibe o OpenFileDialog e obtém o resultado
            DialogResult resultado = abrirArquivo.ShowDialog();

            // Verifica se o usuário selecionou um arquivo
            if (resultado == DialogResult.OK)
            {
                // Cria um objeto XmlTextReader
                XmlTextReader reader = new XmlTextReader(abrirArquivo.FileName);

                // Cria uma lista de nós para armazenar os nós e subnós lidos do arquivo XML
                List<TreeNode> listaNos = new List<TreeNode>();

                treeView1.Nodes.Clear();
                var position = 0;
                // Percorre o arquivo XML e adiciona os nós e subnós à lista
                while (reader.Read())
                {
                    
                    if (reader.Name == "Versão" || reader.Name == "Nota")
                    {
                        // Cria um novo nó com base nos atributos do elemento XML
                        TreeNode novoNo = new TreeNode(reader.GetAttribute("Text"));

                        if (reader.GetAttribute("Checked") != null)
                        {
                            novoNo.Checked = bool.Parse(reader.GetAttribute("Checked"));
                        }
                        else
                        {
                            continue;
                        }

                        // Adiciona o novo nó à lista
                        listaNos.Add(novoNo);
                        if(reader.Name == "Versão")
                        {                            
                              treeView1.Nodes.Add(novoNo);
                              position = novoNo.Index;                           
                        }
                        else if(reader.Name == "Nota")
                        {
                              treeView1.Nodes[position].Nodes.Add(novoNo);
                        }
                    }
                }

                // Fecha o objeto XmlTextReader
                reader.Close();
            }
        }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportarTreeView();
        }        

        private void xMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SalvarTreeViewLocalmente();
        }

        private void apagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.Remove();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Obter o nó selecionado.
            TreeNode node = treeView1.SelectedNode;

            if (node != null)
            {
                // Abrir o formulário de edição.
                EditarNode editForm = new EditarNode(node.Text);
                editForm.ShowDialog();

                // Atualizar o texto do nó com o novo valor.
                node.Text = editForm.valorEditado;

                if(node.Text != null)
                    treeView1.SelectedNode = node;
            }

        }
    }
}
