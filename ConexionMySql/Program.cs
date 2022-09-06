using System;
using ConexionMySql.Clases;
using System.Collections.Generic;

namespace ConexionMySql
{
    class Program
    {
        static void Main(string[] args)
        {
            var ResultadoConsulta = Clases.ConexionMySql.EjecutarConsulta("select * from holamundo");
            var ResultadoProcedimiento = Clases.ConexionMySql.EjecutarProcedimiento("mostrar_holamundo", new List<(string, object)>() {("mensaje1","hola"),("mensaje2","mundo"),("mensaje3",DateTime.Now) });
            foreach (var miRenglon in ResultadoConsulta)
            {
                Console.WriteLine(miRenglon[0]);
                Console.WriteLine(miRenglon[1]);
                Console.WriteLine(miRenglon[3]);
            }
            foreach (var miRenglon in ResultadoProcedimiento)
            {
                Console.WriteLine(miRenglon[0]);
                Console.WriteLine(miRenglon[1]);
                Console.WriteLine(miRenglon[3]);
            }
        }
    }
}
