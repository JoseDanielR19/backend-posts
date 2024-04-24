using Oracle.ManagedDataAccess.Client;

namespace Posts
{
	public class PostById
	{
		private readonly String _connectionString;

		/// <summary>
		/// Constructor de la clase PostServiceById.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos Oracle.</param>
		public PostById(String connectionString)
		{
			this._connectionString = connectionString;
		}

		/// <summary>
		/// Obtiene un post de la base de datos por su ID.
		/// </summary>
		/// <param name="id">ID del post que se desea obtener.</param>
		/// <returns>Objeto que representa el post obtenido.</returns>
		/// <exception cref="Exception">Se lanza cuando ocurre un error durante el proceso de obtención del post.</exception>
		public Object GetPostById(Int32 id)
		{
			try
			{
				// Establece una conexión con la base de datos Oracle.
				using (OracleConnection connection = new OracleConnection(this._connectionString))
				{
					connection.Open();

					// Crea y ejecuta un comando SQL para seleccionar el post con el ID especificado.
					OracleCommand command = new OracleCommand("SELECT * FROM SIF.SIF_DATOS_JDRB WHERE id = :Id", connection);
					command.Parameters.Add(":Id", OracleDbType.Int32).Value = id;

					using (OracleDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							// Crea un objeto anónimo para representar el post obtenido.
							var post = new
							{
								id = reader.GetInt32(reader.GetOrdinal("id")),
								name = reader.GetString(reader.GetOrdinal("name")),
								description = reader.GetString(reader.GetOrdinal("description")),
								imgUrl = reader.GetString(reader.GetOrdinal("imgUrl")),
							};

							return post;
						}

						// Si no se encuentra ningún post con el ID especificado, devuelve un objeto vacío.
						return new { };
					}
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

