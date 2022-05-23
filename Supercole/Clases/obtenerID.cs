using System;

namespace Supercole.Clases
{
    class obtenerID
    {
        public obtenerID()
        {

        }

        public string generarID(string ultimoID, string tipo)
        {
            if (ultimoID == null)
            {
                ultimoID = enCasoDePrimeraInsercion(ultimoID, tipo);
            }

            string idGenerado;

            char[] array = ultimoID.ToCharArray();

            char cifra0 = array[0];
            int cifra1 = (int)Char.GetNumericValue(array[1]);
            int cifra2 = (int)Char.GetNumericValue(array[2]);
            int cifra3 = (int)Char.GetNumericValue(array[3]);
            int cifra4 = (int)Char.GetNumericValue(array[4]);

            if (cifra4 == 9)
            {
                cifra4 = 0;
                cifra3 += 1;
            }
            else
            {
                cifra4 += 1;
            }

            idGenerado = Convert.ToString(cifra0) + Convert.ToString(cifra1) + Convert.ToString(cifra2) + Convert.ToString(cifra3) + Convert.ToString(cifra4);

            return idGenerado;
        }

        private string enCasoDePrimeraInsercion(string ultimoID, string tipo)
        {
            string id_nuevo = "";

            if (tipo.Equals("usuario"))
            {
                id_nuevo = "U0000";
            }
            else if (tipo.Equals("materia"))
            {
                id_nuevo = "M0000";
            }
            else if (tipo.Equals("clase"))
            {
                id_nuevo = "C0000";
            }
            else if (tipo.Equals("alumno"))
            {
                id_nuevo = "A0000";
            }

            return id_nuevo;
        }
    }
}
