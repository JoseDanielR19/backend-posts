using Oracle.ManagedDataAccess.Client;
using Posts.Data;

namespace Posts
{
	/// <summary>
	/// Clase que proporciona métodos para la creación de posts en la base de datos.
	/// </summary>
	public class CreatePost
	{

		private readonly String _connectionString;

		/// <summary>
		/// Constructor de la clase CreatePostService.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos Oracle.</param>
		public CreatePost(String connectionString)
		{
			this._connectionString = connectionString;
		}

		/// <summary>
		/// Crea un nuevo post en la base de datos.
		/// </summary>
		/// <param name="post">Objeto Post que representa el post a crear.</param>
		/// <exception cref="Exception">Se lanza cuando ocurre un error durante el proceso de creación del post.</exception>
		public void AddPost(Post post)
		{
			try
			{
				// Establece una conexión con la base de datos Oracle.
				using (OracleConnection connection = new OracleConnection(this._connectionString))
				{
					connection.Open();

					// Crea y ejecuta un comando SQL para insertar un nuevo post en la tabla.
					OracleCommand command = new OracleCommand("INSERT INTO SIF.SIF_DATOS_JDRB(name, description, imgUrl) VALUES (:Name, :Description, :ImgUrl)", connection);

					// Asigna los valores de los parámetros del post al comando SQL.
					command.Parameters.Add(":Name", OracleDbType.Varchar2).Value = post.Name;
					command.Parameters.Add(":Description", OracleDbType.Varchar2).Value = post.Description;
					command.Parameters.Add(":ImgUrl", OracleDbType.Varchar2).Value = post.ImgUrl;

					// Ejecuta el comando SQL para insertar el post en la base de datos.
					command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				// Captura cualquier excepción que ocurra durante el proceso y la propaga hacia arriba.
				throw;
			}
		}
	}
}
