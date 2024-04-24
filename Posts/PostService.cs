using Oracle.ManagedDataAccess.Client;

namespace Posts
{
	/// <summary>
	/// Clase que proporciona métodos para la obtención de posts desde la base de datos.
	/// </summary>
	public class PostService
	{
		private readonly String _connectionString;

		/// <summary>
		/// Constructor de la clase PostService.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos Oracle.</param>
		public PostService(String connectionString)
		{
			this._connectionString = connectionString;
		}

		/// <summary>
		/// Obtiene todos los posts de la base de datos.
		/// </summary>
		/// <returns>Lista de objetos que representan los posts obtenidos.</returns>
		/// <exception cref="Exception">Se lanza cuando ocurre un error durante el proceso de obtención de los posts.</exception>
		public List<Object> GetPosts()
		{
			List<Object> posts = new List<Object>();

			try
			{
				// Establece una conexión con la base de datos Oracle.
				using (OracleConnection connection = new OracleConnection(this._connectionString))
				{
					connection.Open();

					// Crea y ejecuta un comando SQL para seleccionar todos los posts de la tabla.
					OracleCommand command = new OracleCommand("SELECT id, name, description, imgUrl FROM SIF.SIF_DATOS_JDRB", connection);
					using (OracleDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							// Crea un objeto anónimo para representar cada post, con manejo de valores NULL.
							var post = new
							{
								id = reader.IsDBNull(reader.GetOrdinal("id")) ? (Int32?)null : reader.GetInt32(reader.GetOrdinal("id")),
								title = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
								content = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
								imageUrl = reader.IsDBNull(reader.GetOrdinal("imgUrl")) ? null : reader.GetString(reader.GetOrdinal("imgUrl"))
							};

							// Agrega el post a la lista.
							posts.Add(post);
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Captura cualquier excepción que ocurra durante el proceso y la propaga hacia arriba.
				throw;
			}

			return posts;
		}
	}
}
