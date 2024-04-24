using Oracle.ManagedDataAccess.Client;
using Posts.Data;

namespace Posts
{
	/// <summary>
	/// Clase que proporciona métodos para la actualización de posts en la base de datos.
	/// </summary>
	public class UpdatePost
	{
		private readonly String _connectionString;

		/// <summary>
		/// Constructor de la clase UpdatePostService.
		/// </summary>
		/// <param name="connectionString">Cadena de conexión a la base de datos Oracle.</param>
		public UpdatePost(String connectionString)
		{
			this._connectionString = connectionString;
		}

		/// <summary>
		/// Actualiza los datos de un post existente en la base de datos.
		/// </summary>
		/// <param name="id">ID del post que se desea actualizar.</param>
		/// <param name="updatePost">Objeto Post con los nuevos datos del post.</param>
		/// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
		/// <exception cref="Exception">Se lanza cuando ocurre un error durante el proceso de actualización del post.</exception>
		public Boolean UpdatePostById(Int32 id, Post updatePost)
		{
			try
			{
				// Establece una conexión con la base de datos Oracle.
				using (OracleConnection connection = new OracleConnection(this._connectionString))
				{
					connection.Open();

					// Crea y ejecuta un comando SQL para actualizar los datos del post en la tabla.
					OracleCommand command = new OracleCommand("UPDATE SIF.SIF_DATOS_JDRB SET name = :Name, description = :Description, imgUrl = :ImgUrl WHERE id = :Id", connection);

					// Asigna los nuevos valores de los parámetros del post al comando SQL.
					command.Parameters.Add(":Name", OracleDbType.Varchar2).Value = updatePost.Name;
					command.Parameters.Add(":Description", OracleDbType.Varchar2).Value = updatePost.Description;
					command.Parameters.Add(":ImgUrl", OracleDbType.Varchar2).Value = updatePost.ImgUrl;
					command.Parameters.Add(":Id", OracleDbType.Int32).Value = id;

					// Ejecuta el comando SQL para actualizar el post en la base de datos.
					Int32 rowsAffected = command.ExecuteNonQuery();

					// Devuelve True si se actualizaron filas en la base de datos, de lo contrario, devuelve False.
					return rowsAffected > 0;
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
