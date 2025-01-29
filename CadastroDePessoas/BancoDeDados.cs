using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroDePessoas;
using System.Runtime.Serialization;

namespace CadastroDePessoas
{
    [DataContract]
    internal class BancoDeDados
    {

        //Atributos
        [DataMember]
        private List<Cadastros> listaDeCadastros;
        private string enderecoBancoDeDados;

        //Construtor
        public BancoDeDados(string pEnderecoBancoDeDados)
        {
            enderecoBancoDeDados = pEnderecoBancoDeDados;
            BancoDeDados bancoDeDadosTemp = Serializador.Desserialização(pEnderecoBancoDeDados);
            if (bancoDeDadosTemp != null)
                listaDeCadastros = bancoDeDadosTemp.listaDeCadastros;
            else 
                listaDeCadastros = new List<Cadastros>();
        }

        //Metodos
        public void addPessoas(Cadastros cadastro)
        {  
            listaDeCadastros.Add(cadastro);
            Serializador.Serializa(enderecoBancoDeDados,this);
        }

        public List<Cadastros> PesquisaCadastro(string pDocumento)
        {
            List<Cadastros> listaTemp = listaDeCadastros.Where(x => x.Documento == pDocumento).ToList();
            if (listaTemp.Count > 0)
                return listaTemp;
            else return null;
        }

        public List<Cadastros> DeletaCadastro(string pDocumento)
        {
            List<Cadastros> listaTemp = listaDeCadastros.Where(x => x.Documento == pDocumento).ToList();
            if (listaTemp.Count > 0)
            {
                foreach (Cadastros pessoa in listaTemp)
                {
                    listaDeCadastros.Remove(pessoa);
                }

                return listaTemp;

            }
            else {
                return null;
            } 
        }
    }
}
