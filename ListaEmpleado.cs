using Newtonsoft.Json;

class ListaEmpleado 
{
    #region Campos privados
    private List<Empleado> _plantilla;
    private string _fichero = "plantilla.json";
    #endregion

    #region Propiedades
    #endregion
    #region Constructores
    public ListaEmpleado() {
        _plantilla = new();
    }
    #endregion

    #region Métodos
    private void GenerarJSON() {
        string resultadoJSON = JsonConvert.SerializeObject(_plantilla, Formatting.Indented);
        File.WriteAllText(_fichero, resultadoJSON);
    }
    public bool ActualizarSueldoJSON(int id, float nuevoSueldo) {
        // Editamos el json si existe, en lugar de a traveś de la lista dinámica
        if (File.Exists(_fichero))
        {
            string json = File.ReadAllText(_fichero);
            dynamic? arrayEmpleados = JsonConvert.DeserializeObject(json);
            
            try {
                arrayEmpleados![id]["Sueldo"] = nuevoSueldo;
                string datosASalvar = JsonConvert.SerializeObject(arrayEmpleados, Formatting.Indented);
                File.WriteAllText(_fichero, datosASalvar);
                return true;
            } catch (ArgumentOutOfRangeException) {
                return false;
            }
        }
        return false;
 
    }
    public void GenerarAleatorio(int cuantos) {
        string[] nombres = {"Juan", "Pedro", "Pepe", "Alberto", "Jesús", "Enrique", "Antonio José", "José Luis"};
        string[] apellidos = {"Sánchez", "Pérez", "Gómez", "López", "García", "Morales", "Moreno", "Rodríguez"};

        var rnd = new Random();
        string apellidoCompleto;
        var sueldoMin = 1400;
        var sueldoMax = 4000;
        var sueldo = 0f;
        for (int i = 0; i < cuantos; i++) 
        {
            apellidoCompleto = $"{apellidos[rnd.Next(apellidos.Length)]} {apellidos[rnd.Next(apellidos.Length)]}";
            sueldo = (float) Math.Round((float) rnd.NextDouble() * (sueldoMax - sueldoMin) + sueldoMin, 2);
            _plantilla.Add(new Empleado(
                i + 1, 
                apellidoCompleto,
                nombres[rnd.Next(nombres.Length)],
                sueldo
            ));
        }
        // Generar el JSON
        GenerarJSON();
    }
    public void ListarJSON() {
        if (!File.Exists(_fichero)) {
            Console.WriteLine("No existe el fichero con el JSON");
            return;
        }
        var plantilla = new List<Empleado>();
        using (StreamReader sr = File.OpenText(_fichero))
        {
            plantilla = JsonConvert.DeserializeObject<List<Empleado>>(sr.ReadToEnd());
        }
        if (plantilla != null) 
        {
            foreach (var item in plantilla)
                if (!item.Borrado)
                    Console.WriteLine(item);  
        }
    }
    public override string ToString()
    {
        return String.Join('\n', _plantilla);
    }
    public bool ActualizarSueldo(int id, float nuevoSueldo)
    {
        var empleado = _plantilla.Find(e => e.ID == id && !e.Borrado);
        if (empleado != null)
        {
            empleado.Sueldo = nuevoSueldo;
            return true;
        }
        return false;
    }
    public bool BorrarEmpleado(int id)
    {
        var empleado = _plantilla.Find(e => e.ID == id && !e.Borrado);
        if (empleado != null)
        {
            empleado.Borrado = true;
            GenerarJSON();
            return true;
        }
        return false;
    }
    public bool BorrarEmpleadoJSON(int id) {
        if (File.Exists(_fichero))
        {
            string json = File.ReadAllText(_fichero);
            dynamic? arrayEmpleados = JsonConvert.DeserializeObject(json);
            
            try {
                arrayEmpleados![id]["Borrado"] = true;
                string datosASalvar = JsonConvert.SerializeObject(arrayEmpleados, Formatting.Indented);
                File.WriteAllText(_fichero, datosASalvar);
                return true;
            } catch (ArgumentOutOfRangeException) {
                return false;
            }
        }
        return false;
    }
    #endregion
}