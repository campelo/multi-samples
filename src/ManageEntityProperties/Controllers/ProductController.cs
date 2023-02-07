using ManageEntityProperties.DbContexts;
using ManageEntityProperties.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEntityProperties.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ManageEntityPropertiesDbContext _dbContext;

    public ProductController(ManageEntityPropertiesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return _dbContext.Products;
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public Product? Get(int id)
    {
        return _dbContext.Products.FirstOrDefault(x => x.Id == id);
    }

    // POST api/<ProductController>
    [HttpPost]
    public void Post([FromBody] Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Product product)
    {
        var p = _dbContext.Products.FirstOrDefault(x => x.Id == id);
        if (product == null)
            throw new KeyNotFoundException();
        p.Name = product.Name;
        p.Description = product.Description;
        _dbContext.Products.Update(p);
        _dbContext.SaveChanges();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
        if (product == null)
            throw new KeyNotFoundException();
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }
}
