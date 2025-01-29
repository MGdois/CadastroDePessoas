using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace CadastroDePessoas
{
    internal static class Serializador
    {
        static private DataContractSerializer serializador = new DataContractSerializer(typeof(BancoDeDados));

        //Processo de serialização
        /* Criar o config do XMl 
             * Criar a stringWriter (que vai ser a nossa stringXml)
             * Criar o writer XmlWriter que vai que vai usar oq estiver no nosso construtor de string e usar a configXMl
             * Serializar o objeto com WriteObject com o nosso writer e com o banco de dados que ele vai acessar
             * Usar o flush no nosso escritor
             * criar uma string serializada pra salvar o processo que vai usar o nosso construtor de stringXml
             * e passar para uma string normal
             * Fechar o stringWriter
             * Depois de tudo isso criar um arquivo e passar o endereco
             * fechar o arquivo
             * Escrever nesse arquivo a nossa String serializada 
        */
        public static void Serializa(string pEnderecoXml,BancoDeDados pBancoDeDados)
        {
           
            XmlWriterSettings configXml = new XmlWriterSettings();
            StringBuilder construtorStringXml = new StringBuilder();
            XmlWriter escritorXML = XmlWriter.Create(construtorStringXml, configXml);
            serializador.WriteObject(escritorXML, pBancoDeDados);
            escritorXML.Flush();
            string strSerializada = construtorStringXml.ToString();
            escritorXML.Close();

            FileStream arquivoXMl = File.Create(pEnderecoXml);
            arquivoXMl.Close();

            File.WriteAllText(pEnderecoXml, strSerializada);
        }

        //Processor de Desserialização
        /*Criar um static BancoDeDados e passar o endereço 
         * Criar um tratamento de excessão que vai testar se o arquivo existe
         * Nesse caso nós vamos criar um objeto que vai puxar o arquivo com ReadAllText
         * Vamos criar um stringReader que vai ler o objeto anterior
         * Vamos criar um XML Reader que vai criar o que estiver na nossa stringReader
         * Vamos criar uma banco de dados temporária que vai usar o nosso BancoDeDados
         * com o serializador e vai ler o nosso objeto com ReadObject
         * Então retornar oq estiver no nosso banco de dados temporário
         */

        public static BancoDeDados Desserialização(string pEnderecoXml)
        {
            try
            {
                if (File.Exists(pEnderecoXml))
                {
                    string dentroArquivo = File.ReadAllText(pEnderecoXml);
                    StringReader leitorString = new StringReader(dentroArquivo);
                    XmlReader leitorXml = XmlReader.Create(leitorString);
                    BancoDeDados bancoDeDadosTemp = (BancoDeDados)serializador.ReadObject(leitorXml);
                    return bancoDeDadosTemp;
                }else
                    return null;
            }
            catch
            {
                return null;
            }
        }


    }
}
