using Microsoft.AspNetCore.Mvc;
using Posts.Data;

namespace Posts.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PostsController : ControllerBase
	{
		private readonly String _connectionString;

		/// <summary>
		/// Constructor de la clase PostsController.
		/// </summary>
		/// <param name="configuration">Configuración de la aplicación.</param>
		public PostsController(IConfiguration configuration)
		{
			this._connectionString = configuration.GetConnectionString("OracleConnection");
		}

		[HttpGet]
		public IActionResult GetPost()
		{
			try
			{
				// Instancia de PostService para obtener los posts desde la base de datos.
				PostService postService = new PostService(this._connectionString);

				// Obtener la lista de posts.
				List<Object> posts = postService.GetPosts();

				// Devolver una respuesta HTTP 200 OK con la lista de posts.
				return this.Ok(new { ok = true, data = posts, message = "Lista de posts" });
			}
			catch (Exception ex)
			{
				// Capturar cualquier excepción que ocurra durante el proceso y devolver un error HTTP 500.
				return this.StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetPostById(Int32 id)
		{
			try
			{
				// Instancia de PostServiceById para obtener un post por su ID desde la base de datos.
				PostById postServiceById = new PostById(this._connectionString);

				// Obtener el post por su ID.
				Object post = postServiceById.GetPostById(id);

				// Verificar si se encontró un post y devolver la respuesta correspondiente.
				if (post != null && post.GetType().GetProperties().Length > 0)
				{
					return this.Ok(new { ok = true, data = post, message = "Post encontrado" });
				}
				else
				{
					return this.NotFound(new { ok = false, data = "", message = "Post no encontrado" });
				}
			}
			catch (Exception ex)
			{
				// Capturar cualquier excepción que ocurra durante el proceso y devolver un error HTTP 500.
				return this.StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost]
		public IActionResult CreatePost([FromBody] Post post)
		{
			try
			{
				// Instancia de CreatePostService para crear un nuevo post en la base de datos.
				CreatePost createPostService = new CreatePost(this._connectionString);

				// Crear un nuevo post.
				createPostService.AddPost(post);

				// Devolver una respuesta HTTP 201 Created indicando que el post se ha creado correctamente.
				return this.Created("", new { ok = true, data = post, message = "Post creado" });
			}
			catch (Exception ex)
			{
				// Capturar cualquier excepción que ocurra durante el proceso y devolver un error HTTP 500.
				return this.StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdatePostById(Int32 id, [FromBody] Post updatePost)
		{
			try
			{
				// Instancia de UpdatePostService para actualizar un post por su ID en la base de datos.
				UpdatePost updatePostService = new UpdatePost(this._connectionString);

				// Actualizar el post por su ID.
				Boolean success = updatePostService.UpdatePostById(id, updatePost);

				// Verificar si se actualizó el post y devolver la respuesta correspondiente.
				if (success)
				{
					return this.Ok(new { ok = true, data = "", message = "Post actualizado" });
				}
				else
				{
					return this.NotFound(new { ok = false, data = "", message = "Post no encontrado" });
				}
			}
			catch (Exception ex)
			{
				// Capturar cualquier excepción que ocurra durante el proceso y devolver un error HTTP 500.
				return this.StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeletePostById(Int32 id)
		{
			try
			{
				// Instancia de DeletePostService para eliminar un post por su ID en la base de datos.
				DeletePost deletePostService = new DeletePost(this._connectionString);

				// Eliminar el post por su ID.
				Boolean success = deletePostService.DeletePostById(id);

				// Verificar si se eliminó el post y devolver la respuesta correspondiente.
				if (success)
				{
					return this.Ok(new { ok = true, data = "", message = "Post eliminado" });
				}
				else
				{
					return this.NotFound(new { ok = false, data = "", message = "Post no encontrado" });
				}
			}
			catch (Exception ex)
			{
				// Capturar cualquier excepción que ocurra durante el proceso y devolver un error HTTP 500.
				return this.StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}

}
