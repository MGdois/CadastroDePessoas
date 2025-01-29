using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDePessoas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BancoDeDados bancoDeDados = new BancoDeDados("BancoDeDados.xml");
            InterfaceUsuario menu = new InterfaceUsuario(bancoDeDados);
            menu.InterfaceMain();


        }
    }
}
