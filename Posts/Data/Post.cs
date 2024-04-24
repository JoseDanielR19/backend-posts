using System.ComponentModel.DataAnnotations;

namespace Posts.Data
{
	public class Post
	{
		/// <summary>
		/// Obtiene o establece el identificador único del post.
		/// </summary>
		public Int32 Id { get; set; }

		/// <summary>
		/// Obtiene o establece el nombre del post.
		/// </summary>
		[MaxLength(225, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public String Name { get; set; }

		/// <summary>
		/// Obtiene o establece la descripción del post.
		/// </summary>
		[MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public String Description { get; set; }

		/// <summary>
		/// Obtiene o establece la URL de la imagen del post.
		/// </summary>
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public String ImgUrl { get; set; }
	}
}
