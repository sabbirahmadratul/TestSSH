﻿using Demo.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BrandController : ControllerBase
  {
    private readonly BrandContext _dbContext;
    public BrandController(BrandContext dbContext)
    {
      _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
    {
      if(_dbContext.Brands == null)
      {
        return NotFound();
      }
      return await _dbContext.Brands.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(int id)
    {
      if (_dbContext.Brands == null)
      {
        return NotFound();
      }
      var brand = await _dbContext.Brands.FindAsync(id);
      if(brand == null)
      {
        return NotFound();
      }
      return brand;
    }

    [HttpPost]
    public async Task<ActionResult<Brand>> PostBrand(Brand brand)
    {
      _dbContext.Brands.Add(brand);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction(nameof(GetBrand), new { id = brand.ID }, brand);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Brand>> UpdateBrand(UpdateBrand updateBrand, int id)
    {
      var brand = await _dbContext.Brands.FindAsync(id);
      if(brand != null)
      {
        if(updateBrand.Name != null)
          brand.Name = updateBrand.Name;

        if (updateBrand.Category != null)
          brand.Category = updateBrand.Category;

        if(updateBrand.Model !=  null)
          brand.Model = updateBrand.Model;

        await _dbContext.SaveChangesAsync();

        return Ok(brand);
      }
      return NotFound();
    }
  }
}
