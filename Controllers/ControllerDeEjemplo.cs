// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using PruebaMVC.Models;

// namespace PruebaMVC.Controllers;

// public class ProductoController : Controller
// {
//     private readonly ILogger<HomeController> _logger;
//     private static List<Producto> productos = new List<Producto>();

//     public ProductoController(ILogger<HomeController> logger)
//     {
//         _logger = logger;
//     }

//     public IActionResult Index()
//     {
//         return View(productos);
//     }

//     [HttpGet]
//     public IActionResult CrearProducto()
//     {   
//         return View(new Producto());
//     }

 
//     [HttpPost]
//     public IActionResult CrearProducto(Producto producto)
//     {   
//         producto.Id = productos.Count()+1;
//         productos.Add(producto);
//         return RedirectToAction("Index");
//     }
   
//     [HttpGet]
//     public IActionResult EditarProducto(int idProducto)
//     {  
//         return View( productos.FirstOrDefault( producto => producto.Id == idProducto));
//     }


//     [HttpPost]
//     public IActionResult EditarProducto(Producto producto)
//     {   
        
//         var producto2 = productos.FirstOrDefault( producto => producto.Id == producto.Id);
//         producto2.Nombre = producto.Nombre;
//         producto2.Precio = producto.Precio;

//         return RedirectToAction("Index");
//     }

    
//     public IActionResult DeleteProducto(int idProducto)
//     {  
//        var productoBuscado = productos.FirstOrDefault( producto => producto.Id == idProducto);
//        productos.Remove(productoBuscado);
//       return RedirectToAction("Index");
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }