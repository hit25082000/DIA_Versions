using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DIA_Versions
{
    class Versions
    {
        public TreeNode ProcurarVersaoPorNome(string versaoProcurada, TreeNodeCollection versoes)
        {
            for (int i = 0; i < versoes.Count; i++)
            {
                if (versaoProcurada.ToLower() == versoes[i].Text.ToLower())
                    return versoes[i];
            }
            return null;
        }

        public TreeNode ProcurarNodes(string notaProcurada, TreeNodeCollection versao)
        {
            for (int i = 0; i < versao.Count; i++)
            {
                if (notaProcurada.ToLower() == versao[i].Text.ToLower())
                    return versao[i];
            }            
                return null;
        }

        public void AdicionarNota(string nota, TreeNode versao)
        {
            var notaJaExiste = ProcurarNodes(nota, versao.Nodes);

            if (notaJaExiste == null)
                versao.Nodes.Insert(0, nota);
            else
                MessageBox.Show("Nota ja existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AdicionarVersao(string nome, TreeView treeView)
        {
            // Vai verificar se já existe uma Versão já Existente, Se não houver ele inscreve
            var versaoJaExiste = ProcurarVersaoPorNome(nome, treeView.Nodes);

            if(versaoJaExiste == null)
                treeView.Nodes.Insert(0, nome);
            // Se houver uma versão existente, retorna erro infomando o "versão já existente"
            else
                MessageBox.Show("Versão ja existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
