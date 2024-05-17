class SerializarJSON
{
    public static void Main(String[] args) 
    {
        ListaEmpleado plantilla = new();
        plantilla.GenerarAleatorio(10);
        plantilla.ListarJSON(); // Deserializador del JSON

        //Actualizamos directamente el JSON
        if (plantilla.ActualizarSueldoJSON(9, 0.0f))
            Console.WriteLine("Sueldo actualizado");
        else
            Console.WriteLine("Empleado no existe");

        plantilla.ListarJSON();

        if (plantilla.BorrarEmpleadoJSON(9))
            Console.WriteLine("Empleado borrado");
        else
            Console.WriteLine("Empleado no existe");
        
        plantilla.ListarJSON();

    }
}
