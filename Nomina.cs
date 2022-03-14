using System;


public class Empleado
{
	public const double SMLV = 908256;
	public const double auxtrans = 106454;
	public const double uUvt = 36308;


	public string nombreEmpleado, cedula;
	public double sueldo;
	public int dtrabajados;
	public int HED;
	public int HEN;
	public int HEDD;
	public int HEDN;
	public int RECNOC;
	public int Nriesgo;

	public Empleado()
	{
		nombreEmpleado = "";
		cedula = "";
		sueldo = SMLV;
		HED = HEDD = HEDN = RECNOC = Nriesgo = 0;
	}

	// Claculo cueldo básico
	public double calculoSueldoBasico()
	{
		double basico;
		basico = sueldo * dtrabajados / 30;
		basico = Math.Ceiling(basico);
		return basico;
	}


	//Auxilio de Transporte
	public double calculoAuxilioTransporte()
	{
		if (sueldo <= (2 * SMLV))
		{
			double ttrans;
			ttrans = (auxtrans / 30) * dtrabajados;
			ttrans = Math.Ceiling(ttrans);
			return ttrans;
		}
		else
			return 0;
	}

	//Horas Extras
	public double calculoHorasExtras()
	{
		double hordinaria, thoras;
		hordinaria = sueldo / 240;
		hordinaria = Math.Ceiling(hordinaria);
		thoras = (hordinaria * HED * 0.0125 * 100) + (hordinaria * HEN * 0.0175 * 100) + (hordinaria * HEDD * 0.02 * 100) + (hordinaria * HEDN * 0.025 * 100) + (hordinaria * RECNOC * 0.0035 * 100);
		thoras = Math.Ceiling(thoras);
		return thoras;
	}


	// Calculo sueldo devengado
	public double calculoDevengado()
	{
		return calculoSueldoBasico() + calculoHorasExtras() + calculoAuxilioTransporte();
	}

	//Deducidos - Salud
	public double calculoSalud()
	{
		double saludemp = (calculoSueldoBasico() + calculoHorasExtras()) * 0.04;
		return saludemp;
	}

	//Deducidos - Pensión
	public double calculoPension()
	{
		double pensionemp = (calculoSueldoBasico() + calculoHorasExtras()) * 0.04;
		return pensionemp;
	}

	// calculo fondo solidario pensional
	public double calculoFondoSolidadridad()
	{
		double sbase, fondosol;
		fondosol = 0;
		sbase = calculoSueldoBasico() + calculoHorasExtras();
		// Sacar cuantos salarios minimos vigente gana
		int SolP = (Convert.ToInt32(sbase / SMLV));

		if (SolP >= 4 && SolP < 16)
		{
			fondosol = (sbase * 1) / 100;
		}
		else
		{
			if (SolP >= 16 && SolP <= 17)
			{
				fondosol = (sbase * 1.20) / 100;
			}
			else if (SolP > 17 && SolP <= 18)
			{
				fondosol = (sbase * 1.40) / 100;
			}
			else if (SolP > 18 && SolP <= 19)
			{
				fondosol = (sbase * 1.60) / 100;
			}
			else if (SolP > 19 && SolP <= 20)
			{
				fondosol = (sbase * 1.80) / 100;
			}
			else if (SolP > 20)
			{
				fondosol = (sbase * 2) / 100;
			}
			fondosol = Math.Ceiling(fondosol);
		}
		return fondosol;
	}


	//Calculo UVT
	public double calculoUVT()
	{
		double UVT;
		UVT = (((calculoDevengado() - calculoSalud() - calculoPension() - calculoFondoSolidadridad()) * 75) / 100) / uUvt;
		return UVT;
	}

	//calculo retefuente
	public double calculoRetefuente()
	{
		double sbase, UVT;
		sbase = calculoSueldoBasico() + calculoHorasExtras();
		UVT = calculoUVT();
		double uvtC = (Convert.ToDouble(sbase / uUvt));
		double retefuente = 0;

		if (uvtC >= 2300)
		{
			retefuente = ((UVT - 2300) * 39 / 100 + 770);
		}
		else
		{
			if (uvtC > 945 && uvtC <= 2300)
			{
				retefuente = ((UVT - 945) * 37 / 100 + 268);
			}
			else if (uvtC >= 640 && uvtC <= 945)
			{
				retefuente = ((UVT - 640) * 35 / 100 + 162);
			}
			else if (uvtC > 360 && uvtC <= 640)
			{
				retefuente = ((UVT - 360) * 33 / 100 + 69);
			}
			else if (uvtC > 150 && uvtC <= 360)
			{
				retefuente = ((UVT - 150) * 28 / 100 + 10);
			}
			else if (uvtC > 95 && uvtC <= 150)
			{
				retefuente = ((UVT - 95) * 19) / 100;
			}
		}

		return retefuente;
	}


	//Total de retefuente
	public double calculoTotalRetefuente()
	{
		return calculoRetefuente() * uUvt;
	}


	// Total deducido
	public double calculoTotalDeducido()
	{
		double tdeducido = calculoSalud() + calculoPension() + calculoFondoSolidadridad() + calculoUVT() + calculoTotalRetefuente();
		tdeducido = Math.Ceiling(tdeducido);
		return tdeducido;
	}

	public double calculoBase()
	{
		return calculoSueldoBasico() + calculoHorasExtras();
	}

	//----------------- CALCULO TOTAL PARAFISCALES-----------------------------
	// calculo salud patron
	public double calculoPasafiscalesSaludPatron()
	{
		double saludpat;
		double sbase;
		sbase = calculoBase();
		saludpat = (sbase * 8.5) / 100;
		return saludpat;
	}

	// calculo pension patron
	public double calculoPasafiscalesPensionPatron()
	{
		double pensionpat;
		double sbase;
		sbase = calculoBase();
		pensionpat = (sbase * 12) / 100;
		return pensionpat;
	}

	//calculo arl segun nivel de riesgo
	public double calculoARL()
	{
		double arl = 0;
		double sbase;
		sbase = calculoBase();
		switch (Nriesgo)
		{
			case 1:
				arl = (sbase * 0.522) / 100;
				break;
			case 2:
				arl = (sbase * 1.044) / 100;
				break;
			case 3:
				arl = (sbase * 2.436) / 100;
				break;
			case 4:
				arl = (sbase * 4.350) / 100;
				break;
			case 5:
				arl = (sbase * 6.960) / 100;
				break;
		}
		return arl;
	}


	//------ calcula SENA
	public double calculoSena()
	{
		double sena, sbase;
		sbase = calculoBase();
		sena = (sbase * 2) / 100;
		return sena;
	}


	//----- calcula  ICBF
	public double calculoICBF()
	{
		double icbf, sbase;
		sbase = calculoBase();
		icbf = (sbase * 3) / 100;
		return icbf;
	}


	//----- calcula  caja de compensacion
	public double calculoCajaCompensacion()
	{
		double caja;
		caja = (calculoDevengado() * 4) / 100;
		return caja;
	}


	// total de paraficales
	public double calculoTotalPasafiscales()
	{
		double tparafiscales;
		tparafiscales = calculoPasafiscalesSaludPatron() + calculoPasafiscalesPensionPatron() + calculoARL() + calculoSena() + calculoICBF() + calculoCajaCompensacion();
		tparafiscales = Math.Ceiling(tparafiscales);
		return tparafiscales;
	}


	//------- calcula prestaciones ------

	// -- calculo  cesantias
	public double calculoCesantias()
	{
		double cesantias;
		cesantias = (calculoDevengado() * 8.33) / 100;
		return cesantias;
	}

	//-- calcula interes cesantias
	public double calculoInteresCesantias()
	{
		double incesantias;
		incesantias = (calculoCesantias() * 1) / 100;
		return incesantias;
	}

	//-- calcula prima
	public double calculoPrima()
	{
		double prima;
		prima = (calculoDevengado() * 8.33) / 100;
		return prima;
	}


	// calcula valor de las vacaciones 
	public double calculoVacaciones()
	{
		double vacaciones, sbase;
		sbase = calculoBase();
		vacaciones = (sbase * 4.17) / 100;
		return vacaciones;
	}

	//------- calcular total prestaciones legales
	public double calculoTotalPrestaciones()
	{
		double tprestaciones;
		tprestaciones = calculoCesantias() + calculoInteresCesantias() + calculoPrima() + calculoVacaciones();
		return tprestaciones;
	}


	//-------------- calcular total nomina
	public double calculoTotalNomina()
	{
		double tNomina;
		tNomina = calculoDevengado() - calculoTotalDeducido() + calculoTotalPasafiscales() + calculoTotalPrestaciones();
		tNomina = Math.Ceiling(tNomina);
		return tNomina;
	}

}



public class Nomina
{

	public int nempleados = 100;
	private Empleado[] empleadosNomina = new Empleado[100];



	public void Cargar()
	{



		for (int i = 0; i < nempleados; i++)
		{
			empleadosNomina[i] = new Empleado();

			Console.Write("Ingrese el nombre del empleado " + (i + 1) + " :");
			empleadosNomina[i].nombreEmpleado = Console.ReadLine();

			Console.Write("Ingrese cedula del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].cedula = Console.ReadLine();


			Console.Write("Ingrese el sueldo del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].sueldo = Convert.ToDouble(Console.ReadLine());

			Console.Write("Ingrese dias trabajados  del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].dtrabajados = int.Parse(Console.ReadLine());

			Console.Write("Ingrese horas EXTRA diurnas del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].HED = int.Parse(Console.ReadLine());


			Console.Write("Ingrese horas EXTRA NOCTURNAS del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].HEN = int.Parse(Console.ReadLine());


			Console.Write("Ingrese horas EXTRA DOMINICALES DIURNAS del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].HEDD = int.Parse(Console.ReadLine());

			Console.Write("Ingrese horas EXTRA DOMINICALES NOCTURNAS del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].HEDN = int.Parse(Console.ReadLine());


			Console.Write("Ingrese RECARGOS NOCTURNOS del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].RECNOC = int.Parse(Console.ReadLine());

			Console.Write("Ingrese nivel de riesgo  del empleado " + (i + 1) + ":  ");
			empleadosNomina[i].Nriesgo = int.Parse(Console.ReadLine());

		}
	}

	public void Calculardevengado()
	{


		for (int i = 0; i < nempleados; i++)
		{
			//--------------------------CALCULO TOTAL DEVENGADO--------------------------------------------
			//calculo sueldo basico


			Console.WriteLine("el total basico es :   " + empleadosNomina[i].calculoSueldoBasico());
			//calculo auxilio de transporte

			Console.WriteLine("el aux transporte es :   " + empleadosNomina[i].calculoAuxilioTransporte());

			Console.WriteLine("EL TOTAL DE HORAS EXTRA  es:    " + empleadosNomina[i].calculoHorasExtras());
			// calculo total devengado

			Console.WriteLine("EL TOTAL DEVENGADO  es:    " + empleadosNomina[i].calculoDevengado());

			//-------------------CALCULO DEDUCIDOS--------------------------------------------------------

			Console.WriteLine("salud del empleado  es:    " + empleadosNomina[i].calculoSalud());


			Console.WriteLine("pension del empleado  es:    " + empleadosNomina[i].calculoPension());

			Console.WriteLine("base del empleado  es:    " + empleadosNomina[i].calculoBase());

			// calculo fondo solidario pensional
			// Sacar cuantos salarios minimos vigente gana

			Console.WriteLine(" el fondo solidario es :    " + empleadosNomina[i].calculoFondoSolidadridad());


			Console.WriteLine(" valor uvt  es :   " + empleadosNomina[i].calculoUVT());
			// calculo valor retefuente 
			//sacar cantidad de uvt

			Console.WriteLine("  retecion en la fuente es :   " + empleadosNomina[i].calculoRetefuente());


			// calculo valor a pagar retefuente
			Console.WriteLine(" valor total a pagar de rete fuente :    " + empleadosNomina[i].calculoTotalRetefuente());

			Console.WriteLine("valor total deducidos   :          " + empleadosNomina[i].calculoTotalDeducido());

			//----------------- CALCULO TOTAL PARAFISCALES-----------------------------
			// calculo salud patron

			Console.WriteLine("valor que paga salud patron es :       " + empleadosNomina[i].calculoPasafiscalesSaludPatron());
			// calculo pension patron


			Console.WriteLine("valor que paga pension patron es :       " + empleadosNomina[i].calculoPasafiscalesPensionPatron());
			//calculo arl segun nivel de riesgo


			Console.WriteLine("valor de la arl  es :       " + empleadosNomina[i].calculoARL());

			//------ calcula SENA

			Console.WriteLine("valor de SENA  es :       " + empleadosNomina[i].calculoSena());

			//----- calcula  ICBF

			Console.WriteLine("valor de ICBF  es :       " + empleadosNomina[i].calculoICBF());

			//----- calcula  caja de compensacion

			Console.WriteLine("valor de caja  es :       " + empleadosNomina[i].calculoCajaCompensacion());


			Console.WriteLine(" total paraficales es :          " + empleadosNomina[i].calculoTotalPasafiscales());
			//------- calcula prestaciones ------

			// -- calculo  cesantias

			Console.WriteLine("el valor de sus cesantias es:     " + empleadosNomina[i].calculoInteresCesantias());

			//-- calcula interes cesantias

			Console.WriteLine("el valor de interes de la cesantias es :        " + empleadosNomina[i].calculoInteresCesantias());

			//-- calcula prima

			Console.WriteLine(" el valor de la prima es :             " + empleadosNomina[i].calculoPrima());

			// calcula valor de las vacaciones 

			Console.WriteLine(" el valor de las vacaciones  es :             " + empleadosNomina[i].calculoVacaciones());

			//------- calcular total prestaciones legales


			Console.WriteLine("el  valor de las prestaciones legales es:      " + empleadosNomina[i].calculoTotalPrestaciones());

			//-------------- calcular total nomina


			Console.WriteLine(" el total a pagar por concepto de nomina es       " + empleadosNomina[i].calculoTotalNomina());

		}

	}
	public void imprimir()
	{



	}

	public Empleado buscarEmpleado(string cedula)
	{
		for (int i = 0; i < nempleados; i++)
		{
			if (empleadosNomina[i].cedula == cedula)
			{
				return empleadosNomina[i];
			}
		}
		return null;
	}

	public void MostrarEmpleado(Empleado emp1)
	{
		Console.WriteLine(" El empleado " + emp1.nombreEmpleado);
		Console.WriteLine(" con cedula " + emp1.cedula);
		Console.WriteLine(" con sueldo " + emp1.sueldo);
		Console.WriteLine(" devengó " + emp1.calculoDevengado());
		Console.WriteLine(" por fondo de solidaridad " + emp1.calculoFondoSolidadridad());
		Console.WriteLine(" retefuente " + emp1.calculoTotalRetefuente());
		Console.WriteLine(" riesgos profesionales " + emp1.calculoARL());

	}



	static void Main(string[] args)
	{
		Nomina no = new Nomina();
		int nempleados = 0;
		Console.Write("Ingrese el numero de empleados " + " :");
		nempleados = Convert.ToInt32(Console.ReadLine());
		no.nempleados = nempleados;
		no.Cargar();
		no.Calculardevengado();
		Console.Write("Ingrese el numero de cedula del empleado a buscar sin puntos ni comas " + " :");
		String cedula = Console.ReadLine();
		Empleado emp1 = no.buscarEmpleado(cedula);
		if (emp1 == null)
		{
			Console.Write("No se encontró el empleado");
		}
		else
		{
			no.MostrarEmpleado(emp1);
		}



		Console.ReadKey();
	}

}

