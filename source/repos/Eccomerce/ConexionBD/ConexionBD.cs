namespace Eccomerce.ConexionBD
{
    public class ConexionBD
    {
        public string ConexionString = string.Empty;


        //CONSTRUCTOR
        public ConexionBD()
        {
            //Se está creando una instancia de la clase ConfigurationBuilder. Esta clase se usa para construir un objeto de configuración
            //SetBasePath(Directory.GetCurrentDirectory()) Este método establece la ruta base donde ConfigurationBuilder buscará los archivos de configuración.
            //AddJsonFile("Appsettings.json"):Este método especifica que el archivo appsettings.json debe ser agregado como una fuente de configuración
            // Build contruyye el opbjeto de la configuracion
            var constructor = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile("Appsettings.json").Build();

            //Este código intenta acceder a la configuración específica para la cadena de conexión llamada conexionDB dentro de la sección ConnectionStrings en el archivo appsettings.json y asignar su valor a la variable ConexionString.
            ConexionString = constructor.GetSection
                ("ConnectionStrings:conexionDB").Value;

        }

        //se encargara de mapear usuarios con la tabla de la DB
        public string CadenaSQL()
        {
            return ConexionString;
        }
    }
}

