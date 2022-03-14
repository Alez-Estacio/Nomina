using System;

namespace Nomina2021
{
    class Nomina
    {
        private int nempleados;
        private string[] empleado;
        private double[] cedula;
        private double[] sueldo;
        private int[] dtrabajados;
        private int[] HED;
        private int[] HEN;
        private int[] HEDD;
        private int[] HEDN;
        private int[] RECNOC;
        private int[] Nriesgo;
        double SMLV = 908256;
        double auxtrans = 106454;
        double uUvt = 36308;
        public void Cargar()

        {

            empleado = new string[100];
            cedula = new double[100];
            sueldo = new double[100];
            dtrabajados = new int[100];
            HED = new int[100];
            HEN = new int[100];
            HEDD = new int[100];
            HEDN = new int[100];
            RECNOC = new int[100];
            Nriesgo = new int[100];
            Console.Write("Ingrese el numero de empleados " + " :");
            nempleados = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < nempleados; i++)

            {

                Console.Write("Ingrese el nombre del empleado " + (i + 1) + " :");
                empleado[i] = Console.ReadLine();

                Console.Write("Ingrese cedula del empleado " + (i + 1) + ":  ");
                cedula[i] = Convert.ToDouble(Console.ReadLine());


                Console.Write("Ingrese el sueldo del empleado " + (i + 1) + ":  ");
                sueldo[i] = Convert.ToDouble(Console.ReadLine());

                Console.Write("Ingrese dias trabajados  del empleado " + (i + 1) + ":  ");
                dtrabajados[i] = int.Parse(Console.ReadLine());

                Console.Write("Ingrese horas EXTRA diurnas del empleado " + (i + 1) + ":  ");
                HED[i] = int.Parse(Console.ReadLine());


                Console.Write("Ingrese horas EXTRA NOCTURNAS del empleado " + (i + 1) + ":  ");
                HEN[i] = int.Parse(Console.ReadLine());


                Console.Write("Ingrese horas EXTRA DOMINICALES DIURNAS del empleado " + (i + 1) + ":  ");
                HEDD[i] = int.Parse(Console.ReadLine());

                Console.Write("Ingrese horas EXTRA DOMINICALES NOCTURNAS del empleado " + (i + 1) + ":  ");
                HEDN[i] = int.Parse(Console.ReadLine());


                Console.Write("Ingrese RECARGOS NOCTURNOS del empleado " + (i + 1) + ":  ");
                RECNOC[i] = int.Parse(Console.ReadLine());

                Console.Write("Ingrese nivel de riesgo  del empleado " + (i + 1) + ":  ");
                Nriesgo[i] = int.Parse(Console.ReadLine());

                            }

        }

        public void Calculardevengado()
        {
            double[] basico = new double[100];
            double[] ttrans = new double[100];
            double[] thoras = new double[100];
            double[] hordinaria = new double[100];
            double[] tdevengado = new double[100];
            double[] saludemp = new double[100];
            double[] pensionemp = new double[100];
            double[] fondosol = new double[100];
            double[] sbase = new double[100];


            for (int i = 0; i < nempleados; i++)
            {
                //--------------------------CALCULO TOTAL DEVENGADO--------------------------------------------
                //calculo sueldo basico
                basico[i] = sueldo[i] * dtrabajados[i] / 30;
                basico[i] = Math.Ceiling(basico[i]);

                Console.WriteLine("el total basico es :   " + basico[i]);
                //calculo auxilio de transporte
                if (sueldo[i] <= (2 * SMLV))
                {
                    ttrans[i] = (auxtrans / 30) * dtrabajados[i];
                    ttrans[i] = Math.Ceiling(ttrans[i]);
                    Console.WriteLine("el aux transporte es :   " + ttrans[i]);
                }

                hordinaria[i] = sueldo[i] / 240;
                hordinaria[i] = Math.Ceiling(hordinaria[i]);
                Console.WriteLine("la hora ordinaria es :   " + hordinaria[i]);

                thoras[i] = (hordinaria[i] * HED[i] * 0.0125 * 100) + (hordinaria[i] * HEN[i] * 0.0175 * 100) + (hordinaria[i] * HEDD[i] * 0.02 * 100) + (hordinaria[i] * HEDN[i] * 0.025 * 100) + (hordinaria[i] * RECNOC[i] * 0.0035 * 100);
                thoras[i] = Math.Ceiling(thoras[i]);
                Console.WriteLine("EL TOTAL DE HORAS EXTRA  es:    " + thoras[i]);
                // calculo total devengado
                tdevengado[i] = basico[i] + ttrans[i] + thoras[i];
                tdevengado[i] = Math.Truncate(tdevengado[i]);


                Console.WriteLine("EL TOTAL DEVENGADO  es:    " + tdevengado[i]);

                //-------------------CALCULO DEDUCIDOS--------------------------------------------------------
                saludemp[i] = (tdevengado[i] - ttrans[i]) * 0.04;
                Console.WriteLine("salud del empleado  es:    " + saludemp[i]);


                pensionemp[i] = (tdevengado[i] - ttrans[i]) * 0.04;
                Console.WriteLine("pension del empleado  es:    " + pensionemp[i]);

                sbase[i] = tdevengado[i] - ttrans[i];

                Console.WriteLine("base del empleado  es:    " + sbase[i]);

                // calculo fondo solidario pensional
                // Sacar cuantos salarios minimos vigente gana
                int SolP = (Convert.ToInt32(sbase[i] / SMLV));

                if (SolP >= 4 && SolP < 16)
                {


                    fondosol[i] = (sbase[i] * 1) / 100;
                    Console.WriteLine(" el fondo solidario es :    " + fondosol[i]);
                }
                else
                {
                    if (SolP >= 16 && SolP <= 17)
                    {

                        fondosol[i] = (sbase[i] * 1.20) / 100;


                    }
                    else if (SolP >= 17 && SolP <= 18)

                    {
                        fondosol[i] = (sbase[i] * 1.40) / 100;
                    }
                    else if (SolP > 18 && SolP <= 19)

                    {

                        fondosol[i] = (sbase[i] * 1.60) / 100;

                    }
                    else if (SolP > 19 && SolP <= 20)
                    {
                        fondosol[i] = (sbase[i] * 1.80) / 100;
                    }
                    else if (SolP > 20)
                    {
                        fondosol[i] = (sbase[i] * 2) / 100;
                    }
                    fondosol[i] = Math.Ceiling(fondosol[i]);
                    Console.WriteLine(" el fondo solidario es:    " + fondosol[i]);
                }

                double[] UVT = new double[100];

                UVT[i] = (((tdevengado[i] - saludemp[i] - pensionemp[i] - fondosol[i]) * 75) / 100) / uUvt;
               
                Console.WriteLine(" valor uvt  es :   " + UVT[i]);
                // calculo valor retefuente 
                //sacar cantidad de uvt
                double uvtC = (Convert.ToDouble(sbase[i] / uUvt));

                double[] retefuente = new double[100];

                if (uvtC >= 2300)
                {

                    retefuente[i] = ((UVT[i] - 2300) * 39 / 100 + 770);

                }
                else
                {
                    if (uvtC > 945 && uvtC <= 2300)
                    {

                        retefuente[i] = ((UVT[i] - 945) * 37 / 100 + 268);

                    }
                    else if (uvtC >= 640 && uvtC <= 945)

                    {
                        retefuente[i] = ((UVT[i] - 640) * 35 / 100 + 162);
                    }
                    else if (uvtC > 360 && uvtC <= 640)

                    {

                        retefuente[i] = ((UVT[i] - 360) * 33 / 100 + 69);

                    }
                    else if (uvtC > 150 && uvtC <= 360)
                    {
                        retefuente[i] = ((UVT[i] - 150) * 28 / 100 + 10);
                    }
                    else if (uvtC > 95 && uvtC <= 150)
                    {
                        retefuente[i] = ((UVT[i] - 95) * 19) / 100;
                    }

                    Console.WriteLine("  retecion en la fuente es :   " + retefuente[i]);

                }
                // calculo valor a pagar retefuente
                double[] tretefuente = new double[100];

                tretefuente[i] = retefuente[i] * uUvt;


                Console.WriteLine(" valor total a pagar de rete fuente :    " + tretefuente[i]);

                double[] tdeducido = new double[100];

                tdeducido[i] = saludemp[i] + pensionemp[i] + fondosol[i] + UVT[i] + tretefuente[i];
                tdeducido[i] = Math.Ceiling(tdeducido[i]);

                Console.WriteLine("valor total deducidos   :          " + tdeducido[i]);

                //----------------- CALCULO TOTAL PARAFISCALES-----------------------------
                // calculo salud patron
                double[] saludpat = new double[100];

                saludpat[i] = (sbase[i] * 8.5) / 100;

                Console.WriteLine("valor que paga salud patron es :       " + saludpat[i]);
                // calculo pension patron
                double[] pensionpat = new double[100];

                pensionpat[i]=(sbase[i]*12)/ 100;

                Console.WriteLine("valor que paga pension patron es :       " + pensionpat[i]);
                //calculo arl segun nivel de riesgo

                double[] arl = new double[100];


                if (Nriesgo[i] == 1)
                {
                    arl[i] = (sbase[i] * 0.522) / 100;

                }
                else
                {
                    if (Nriesgo[i] == 2)
                    {
                        arl[i] = (sbase[i] * 1.044) / 100;

                    }
                    else
                        if (Nriesgo[i] == 3)
                    {
                        arl[i] = (sbase[i] * 2.436) / 100;

                    }
                    else if (Nriesgo[i] == 4)
                    {

                        arl[i] = (sbase[i] * 4.350) / 100;
                    }
                    else if (Nriesgo[i] == 5)
                    {

                        arl[i] = (sbase[i] * 6.960) / 100;
                    }
                }
                    Console.WriteLine("valor de la arl  es :       " + arl[i]);

                    //------ calcula SENA

                    double[] sena = new double[100];

                    sena[i]= (sbase[i]*2)/ 100;

                Console.WriteLine("valor de SENA  es :       " + sena[i]);

                //----- calcula  ICBF

                double[] icbf = new double[100];

                    icbf[i] = (sbase[i] * 3) / 100;

                Console.WriteLine("valor de ICBF  es :       " + icbf[i]);

                //----- calcula  caja de compensacion

                double[] caja = new double[100];

                caja[i] = (tdevengado[i] * 4 )/ 100;

                Console.WriteLine("valor de caja  es :       " + caja[i]);

                double[] tparafiscales = new double[100];

                tparafiscales[i] = saludpat[i] + pensionpat[i] + arl[i] + sena[i] + icbf[i] + caja[i];
                tparafiscales[i] = Math.Ceiling(tparafiscales[i]);
                Console.WriteLine(" total paraficales es :          " + tparafiscales[i]);
                //------- calcula prestaciones ------

                // -- calculo  cesantias

                double[] cesantias = new double[100];

                cesantias[i] = (tdevengado[i] * 8.33) / 100;

                Console.WriteLine("el valor de sus cesantias es:     " + cesantias[i]);

                //-- calcula interes cesantias

                double[] incesantias = new double[100];

                incesantias[i] = (cesantias[i] * 1) / 100;

                Console.WriteLine("el valor de interes de la cesantias es :        " + incesantias[i]);

                //-- calcula prima

                double[] prima = new double[100];

                prima[i] = (tdevengado[i] * 8.33 )/100;

                Console.WriteLine(" el valor de la prima es :             " + prima[i]);

                // calcula valor de las vacaciones 

                double[] vacaciones = new double[100];

                vacaciones[i] = (sbase[i] * 4.17 )/ 100;
                Console.WriteLine(" el valor de las vacaciones  es :             " + vacaciones[i]);

                //------- calcular total prestaciones legales

                double []tprestaciones = new double[100];

                tprestaciones[i] = cesantias[i] + incesantias[i] + prima[i] + vacaciones[i];

                Console.WriteLine("el  valor de las prestaciones legales es:      " + tprestaciones[i]);

                //-------------- calcular total nomina

                double[] tNomina = new double[100];

                tNomina[i] = tdevengado[i] - tdeducido[i] + tparafiscales[i] + tprestaciones[i];
                tNomina[i] = Math.Ceiling(tNomina[i]);

                Console.WriteLine(" el total a pagar por concepto de nomina es       " + tNomina[i]);

            }
           
        }
        public void imprimir()
        {



        }


       /* static void Main(string[] args)
        {
            Nomina no = new Nomina();
            no.Cargar();
            no.Calculardevengado();




            Console.ReadKey();
        }*/

    }
}

                        
                    



                
            


        







       