﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicio01 
{
    internal class Program
    {
        static List<Evaluacion> listaDeEvaluacion = new List<Evaluacion>();
        private static int verificadorPorcentaje = 0, porcentajeEvaluacion = 0;
        public static void Main(string[] args)
        {

            bool continuar = true;
            int opcion;
            double notaFinal;
            do
            {
                Console.WriteLine(Menu());
                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException exc)
                {
                    opcion = 0;
                    Console.WriteLine("Favor ingresar algo valido.");
                }

                switch (opcion)
                {
                    case 1:
                        try
                        {
                            AgregarEvaluacion();
                        }
                        catch (PercentageExceedsTheLimits e)
                        {
                            Console.WriteLine("El porcentaje ingresado supera al 100%, volviendo al menu...");
                            verificadorPorcentaje -= porcentajeEvaluacion;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Favor ingresar algo valido.");
                            verificadorPorcentaje -= porcentajeEvaluacion;
                        }

                        break;
                    case 2:
                        MostrarEvaluaciones();
                        break;
                    case 3:
                        EliminarEvaluacion();
                        break;
                    case 4:
                        try
                        {
                            if (verificadorPorcentaje == 100)
                            {
                                Console.WriteLine("Se ingreso el 100% de las calificaciones, procediendo a evaluar nota.....");
                                notaFinal = CalcularNota.CalculandoNota(listaDeEvaluacion);
                                Console.WriteLine("La nota final es: " + notaFinal);
                                continuar = false;
                            }
                            else
                            {
                                Boolean continuarSubMenu = true;
                                do
                                {
                                    Console.WriteLine("No se llego al 100% del porcentaje\nDesea agregar mas evaluaciones?(S/N)");
                                    String masEvaluaciones = Console.ReadLine();
                                    if (masEvaluaciones.ToLower() == "s")
                                    {
                                        Console.WriteLine("Volviendo al menu para agregar más evaluaciones......");
                                        continuarSubMenu = false;
                                    }
                                    else if (masEvaluaciones.ToLower() == "n")
                                    {
                                        Console.WriteLine(
                                            "Se procedera a calcular la nota con las evaluaciones ingresadas." +
                                            "El " + verificadorPorcentaje +
                                            "% se evaluara con las calificaciones que ingrese" +
                                            " mientras que el resto de evaluaciones se tomaran como un 0 de calificacion ");
                                        notaFinal = CalcularNota.CalculandoNota(listaDeEvaluacion);
                                        Console.WriteLine("La nota final es: " + notaFinal);
                                        continuarSubMenu = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Opcion invalida, intente nuevamente.");
                                    }

                                    continuar = false;
                                } while (continuarSubMenu);
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Favor ingresar algo valido.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Volviendo al menu");
                        }
                        break;
                    case 0: break;    //Para el try catch
                    default: Console.WriteLine("Opcion invalida."); break;
                }
            } while (continuar);
        }
        static string Menu(){
            return "\nSeleccione una opcion:\n" +
                   "1) Agregar evaluacion\n" +
                   "2) Mostrar evaluaciones almacenadas\n" +
                   "3) Eliminar evaluacion\n" +
                   "4) Terminar programa\n";
        }

        static void AgregarEvaluacion()
        { 
            Console.WriteLine("Que tipo de evaluacion desea agregar?\n1)Parcial\n2)Laboratorio\n3)Tarea");
           int opcionEvaluacion = Convert.ToInt32(Console.ReadLine());
           Console.WriteLine("Porcentaje disponible: " + (100 - verificadorPorcentaje));
           Console.Write("\nIngrese el porcentaje que tendra la evaluacion: ");
           porcentajeEvaluacion = Convert.ToInt32(Console.ReadLine());
           verificadorPorcentaje += porcentajeEvaluacion;
           if (verificadorPorcentaje <= 100)
           {
               Console.Write("Ingrese el nombre que se le asignara a la evaluacion: ");
               String nombreEvaluacion = Console.ReadLine();
               switch (opcionEvaluacion)
               {
                   case 1:
                       Console.Write("Ingrese la cantidad de preguntas que tendra el parcial: ");
                       int cantidadPreguntas = Convert.ToInt32(Console.ReadLine());
                       listaDeEvaluacion.Add(new Parcial(porcentajeEvaluacion, nombreEvaluacion, cantidadPreguntas));
                       break;
                   case 2:
                       Console.Write("Ingrese el tipo del laboratorio: ");
                       String tipoLaboratorio = Console.ReadLine();
                       listaDeEvaluacion.Add(new Laboratorio(porcentajeEvaluacion, nombreEvaluacion, tipoLaboratorio));
                       break;
                   case 3:
                       Console.Write("Indique la fecha de entrega para la terea con el formate (Mes/Dia/Anio): ");
                       DateTime fechaEntrega = Convert.ToDateTime(Console.ReadLine());
                       listaDeEvaluacion.Add(new Tarea(porcentajeEvaluacion, nombreEvaluacion, fechaEntrega));
                       break;
               }
           }
           else
           {
               throw new PercentageExceedsTheLimits("");
           }
        }

        static void MostrarEvaluaciones()
        {
            if (listaDeEvaluacion.Count == 0)
            {
                Console.WriteLine("La lista no contiene ninguna evaluacion ingresada.");
            }
            else
            {
                foreach (Evaluacion evaluacion in listaDeEvaluacion)
                {
                    Console.WriteLine("\n" + evaluacion);
                }
            }
        }

        static void EliminarEvaluacion()
        {
            Console.Write("Ingrese el nombre de la evaluacion a eliminar:");
            String nombreEliminar = Console.ReadLine();
            String opcion;
            int contadorPrincipal = 0,contador = 0, eliminar=0, porcentajeAuxiliar = 0;
            bool eliminado = false;
            foreach (Evaluacion ev in listaDeEvaluacion)
            {
                if (ev.Nombre.Equals(nombreEliminar, StringComparison.InvariantCultureIgnoreCase))
                {
                    contador++;
                    eliminar = contadorPrincipal;
                    porcentajeAuxiliar = ev.Porcentaje;
                }
                contadorPrincipal++;
            }
            if (contador == 0) {
                Console.WriteLine("No se encontro la evaluacion");
            }else if(contador == 1)
            {
                listaDeEvaluacion.RemoveAt(eliminar);
                verificadorPorcentaje -= porcentajeAuxiliar;
                Console.WriteLine("Se elimino exitosamente ");
            }else if (contador > 1)
            {
                contadorPrincipal = 0;
                contador = 0;
                Console.WriteLine("Hay mas de 1 evaluacion con ese nombre, favor especifique cual evaluacion es la que desea eliminar.");
                foreach (var eva in listaDeEvaluacion )
                {
                    if (eva.Nombre.Equals(nombreEliminar, StringComparison.InvariantCultureIgnoreCase)) {
                        Console.WriteLine(eva + "\n Esta es la evaluacion que desea eliminar? (S/N)");
                        opcion = Console.ReadLine();
                        if(opcion != null && (opcion.Equals("s") || opcion.Equals("S"))){
                            porcentajeAuxiliar = eva.Porcentaje;
                            verificadorPorcentaje -= porcentajeAuxiliar;
                            contador = contadorPrincipal;
                            Console.WriteLine("La evaluacion se elimino con exito");
                            break;
                        }
                        if(opcion != null && (opcion.Equals("n") || opcion.Equals("N"))){
                            Console.WriteLine("\nBuscando siguiente evaluacion....\n");   
                        }
                    }
                    contadorPrincipal++;
                }
                listaDeEvaluacion.RemoveAt(contador);
            }
        }
    }
}