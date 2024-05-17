class Empleado
{
    #region Propiedades
    public int ID {get; set;}
    public string Apellidos {get; set;}
    public string Nombre {get; set;}
    public float Sueldo {get; set;}
    public bool Borrado {get; set;}
    #endregion

    #region Constructores
    public Empleado(int id, string ape, string nom, float sue)
    {
        this.ID = id;
        this.Apellidos = ape;
        this.Nombre = nom;
        this.Sueldo = sue;
    }
    #endregion

    #region Métodos
    public override string ToString()
    {
        return $"#{ID, -3}{Apellidos,-30}{Nombre,-20}{Sueldo,-7}€";
    }
    #endregion

}