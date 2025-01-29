using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDePessoas
{
    internal class InterfaceUsuario
    {
        //Atributo
        BancoDeDados bancoDeDados;

        //Construtor
        public InterfaceUsuario(BancoDeDados pBancoDeDados)
        {
            bancoDeDados = pBancoDeDados;
        }

        public void showMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
            Console.WriteLine("Pressione qualquer tecla para voltar");
            Console.ReadKey();
            Console.WriteLine("");
            Console.Clear();
        }

        public enum Resposta_e
        {
            Confirma = 0,
            Saida = 1,
            Exececao = 2
        }

        public Resposta_e pegaNome(ref string resposta, string mensagem)
        {
            Resposta_e retorno;
            Console.WriteLine(mensagem);
            string temp = Console.ReadLine();
            if (temp == "s" || temp == "S") 
            {
                retorno = Resposta_e.Saida;
            }
            else
            {
                resposta = temp;
                retorno = Resposta_e.Confirma;
            }
            //Console.Clear();
            return retorno;
        }

        public Resposta_e pegaData(ref DateTime data, string mensagem)
        {
            Resposta_e retorno;
            do
            {
                try
                {
                    Console.WriteLine(mensagem);
                    string temp = Console.ReadLine().ToLower();
                    if(temp == "s")
                    {
                        retorno = Resposta_e.Saida;
                    }
                    else
                    {
                        data = Convert.ToDateTime(temp);
                        retorno = Resposta_e.Confirma;
                    }
                    return retorno;
                }
                catch (Exception e)
                {
                    showMensagem("EXECECAO: " + e.Message);
                    Console.WriteLine("Aperte qualquer botão para sair");
                    Console.ReadKey();
                    Console.Clear();
                    retorno = Resposta_e.Exececao;
                }

            } while (retorno == Resposta_e.Exececao);

            //Console.Clear();
            return retorno;
        }

        public Resposta_e pegaNumInt32(ref UInt32 numero, string mensagem)
        {
            Resposta_e retorno;
            do
            {
                try
                {
                    Console.WriteLine(mensagem);
                    string temp = Console.ReadLine().ToLower();
                    if (temp == "s")
                    {
                        retorno = Resposta_e.Saida;
                    }
                    else
                    {
                        numero = Convert.ToUInt32(temp);
                        retorno = Resposta_e.Confirma;
                    }
                    return retorno;
                }
                catch (Exception e)
                {
                    showMensagem("EXECECAO: " + e.Message);
                    Console.WriteLine("Aperte qualquer botão para sair");
                    Console.ReadKey();
                    Console.Clear();
                    retorno = Resposta_e.Exececao;
                }

            } while (retorno == Resposta_e.Exececao);

            //Console.Clear();
            return retorno;
        }

        public void ImprimeDados(Cadastros cadastros)
        {
            Console.WriteLine("Nome: " + cadastros.Nome);
            Console.WriteLine("Documento: " + cadastros.Documento);
            Console.WriteLine("Data de nascimento: " + cadastros.Nascimento);
            Console.WriteLine("Endereço: " + cadastros.Rua);
            Console.WriteLine("Numero da Casa: " + cadastros.NumDaCasa);
            Console.WriteLine("");
        }

        public void ImprimeTodosDados(List<Cadastros> cadastros)
        {
            foreach(Cadastros cadastro  in cadastros)
            {
                ImprimeDados(cadastro);
            }
        }

        public void BuscaDocs()
        {
            Console.Clear();
            Console.WriteLine("Digite o número do documento do usuário ou aperte S para sair");
            string temp = Console.ReadLine().ToLower();
            if (temp == "S") return;
            List<Cadastros> listaTemp = bancoDeDados.PesquisaCadastro(temp);
            if(listaTemp != null)
            {
                Console.WriteLine("Usuário com o documento: " + temp + " foi entroncado");
                ImprimeTodosDados(listaTemp);
            }
            else
            {
               //Console.WriteLine("Usuário com o documento: " + temp + " não foi entroncado");
                showMensagem("Usuário com o documento: " + temp + " não foi entroncado ");

            }

        }

        public void RemoveUsuario()
        {
            Console.Clear();
            Console.WriteLine("Digite o número do documento do usuário que você deseja deletar");
            string temp = Console.ReadLine().ToLower();
            if (temp == "S") return;
            List<Cadastros> listaTemp = bancoDeDados.DeletaCadastro(temp);
            if (listaTemp != null)
            {           
                Console.WriteLine("Usuário com o documento: " + temp + " foi deletado");
                ImprimeTodosDados(listaTemp);   
            }
            else
            {
                Console.WriteLine("Usuário com o documento: " + temp + " não foi entroncado");
                showMensagem("");

            }

        }
        public Resposta_e CadastraUsuario()
        {
            Console.Clear();
            string Nome = "";
            string Docs = "";
            DateTime nascimento = new DateTime();
            string Rua = "";
            UInt32 NumCasa = 0;

            if (pegaNome(ref Nome, "Digite o nome completo ou aperte S para sair") == Resposta_e.Saida)           
                return Resposta_e.Saida;
            
            if (pegaNome(ref Docs, "Digite o numero do seu documento ou aperte S para sair") == Resposta_e.Saida)         
                return Resposta_e.Saida;
            
            if (pegaData(ref nascimento, "Digite a sua data de nasicmento (dd//mm/aaaa) ou aperte S para sair") == Resposta_e.Saida)          
                return Resposta_e.Saida;
            
            if (pegaNome(ref Rua, "Digite o nome da sua rua ou aperte S para sair") == Resposta_e.Saida)           
                return Resposta_e.Saida;
            
            if (pegaNumInt32(ref NumCasa, "Digite o numero da sua casa ou aperte S para sair") == Resposta_e.Saida)          
                return Resposta_e.Saida;

            Cadastros cadastraUsuario = new Cadastros(Nome, Docs, nascimento, Rua, NumCasa);
            bancoDeDados.addPessoas(cadastraUsuario);
            Console.Clear();
            Console.WriteLine("Adicionando usuário:");
            ImprimeDados(cadastraUsuario);
            showMensagem(" ");
            return Resposta_e.Confirma;
        }

        public void InterfaceMain()
        {
            string opcao = "";

            do
            {
                Console.WriteLine("Digite 'C' para cadastras um novo usuário.");
                Console.WriteLine("Digite 'B' para buscar usuário.");
                Console.WriteLine("Digite 'D' para deletar um usuário.");
                Console.WriteLine("Digite 'H' para deletar um usuário.");
                Console.WriteLine("Digite 'S' para encerrar o programa.");
                Console.WriteLine(" ");
                opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();

                switch (opcao)
                {
                    case "c": 
                        if(bancoDeDados.BaseDisponivel == false)
                        {
                            showMensagem("Arquivo indisponível. Aguarde alguns instantes!");
                            break;
                        }
                        CadastraUsuario(); 
                        break;
                    case "b":
                        if (bancoDeDados.BaseDisponivel == false)
                        {
                            showMensagem("Arquivo indisponível. Aguarde alguns instantes!");
                            break;
                        }
                        BuscaDocs(); 
                        break;
                    case "d":
                        if (bancoDeDados.BaseDisponivel == false)
                        {
                            showMensagem("Arquivo indisponível. Aguarde alguns instantes!");
                            break;
                        }
                        RemoveUsuario(); 
                        break;
                    case "s":
                        if (bancoDeDados.BaseDisponivel == false)
                        {
                            showMensagem("Arquivo indisponível. Aguarde alguns instantes!");
                            break;
                        }
                        Console.WriteLine("Encerrando programa...");
                        break;
                    case "h":
                        //Mostra a hora atual
                        showMensagem(DateTime.Now.ToString());
                        break;
                    default:
                        Console.WriteLine("Opcao Desconhecida"); 
                        break;
                }               
            } while (opcao != "s");
        }
    }
}