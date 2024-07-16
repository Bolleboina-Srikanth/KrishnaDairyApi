using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KrishnaDairyDotNetApi.Entity;
using KrishnaDairyDotNetApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1New.Context;
using WebApplication1New.Entity;
using WebApplication1New.Models;

namespace WebApplication1New.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly Cloudinary _cloudinary;
        public ProductController(UserContext userContext, Cloudinary cloudinary)
        {
            this._userContext = userContext;
            this._cloudinary = cloudinary;
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddNewProduct([FromForm]  ProductModal productModal)
        {
            try
            {
                var upLoadParams = new ImageUploadParams
                {
                    File = new FileDescription(productModal.image.FileName, productModal.image.OpenReadStream())
                };
                var upLoadResult = _cloudinary.Upload(upLoadParams);

                ProductEntity productEnity = new ProductEntity();
                productEnity.ProductName = productModal.productName;
                productEnity.OriginalPrice = productModal.originalPrice;
                productEnity.DiscountPrice = productModal.discountPrice;
                productEnity.Quantity = productModal.quantity;
                productEnity.Category = productModal.category;
                productEnity.Image = upLoadResult.SecureUrl.AbsoluteUri;

                _userContext.ProductTable.Add(productEnity);
                _userContext.SaveChanges();

                if (productEnity != null)
                {
                    return this.Ok(new { success = "true", Message = "product added successfully", data = productEnity });
                }
                else
                {
                    return this.BadRequest(new { success = "false", Message = "Failed to add product", data = productEnity });
                }

                return (IActionResult)productEnity;

            }
            catch(Exception Ex)
            {
                throw Ex;
            } 
        }


        [HttpPut]
        [Route("EditProduct")]
        public IActionResult EditNewProduct([FromForm] EditProductModel productModal)
        {
            var productExist = _userContext.ProductTable.FirstOrDefault(x => x.ProductId == productModal.productId);
            try
            {
                if (productExist != null)
                {
                    var upLoadParams = new ImageUploadParams
                    {
                        File = new FileDescription(productModal.image.FileName, productModal.image.OpenReadStream())
                    };
                    var upLoadResult = _cloudinary.Upload(upLoadParams);

                    productExist.ProductName = productModal.productName;
                    productExist.OriginalPrice = productModal.originalPrice;
                    productExist.DiscountPrice = productModal.discountPrice;
                    productExist.Image = upLoadResult.SecureUrl.AbsoluteUri;

                    _userContext.ProductTable.Update(productExist);
                    _userContext.SaveChanges();

                    
                        return this.Ok(new { success = "true", Message = "product updated successfully", data = productExist });
                }
                else
                {
                    return this.BadRequest(new { success = "false", Message = "Failed to update product", data = productExist });
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public List<ProductEntity> GetProducts()
        {
            try
            {
                return _userContext.ProductTable.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int ProductId)
        {
            try
            {
                var productExist = _userContext.ProductTable.FirstOrDefault(x => x.ProductId == ProductId);
                if (productExist != null)
                {
                    _userContext.ProductTable.Remove(productExist);
                    _userContext.SaveChanges();
                    return this.Ok(new { success = "true", Message = "product deleted successfully", data = productExist });
                }
                return this.BadRequest(new { success = "false", Message = "failed to delete product", data = productExist });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart([FromForm] CartModel model)
        {
            try
            {
                var upLoadParams = new ImageUploadParams
                {
                    File = new FileDescription(model.Image.FileName, model.Image.OpenReadStream())
                };
                var upLoadResult = _cloudinary.Upload(upLoadParams);



                var productExist = _userContext.CartTable.FirstOrDefault(x => x.ProductID == model.ProductID);
                if(productExist != null )
                {
                    productExist.Quantity = productExist.Quantity++;
                    return this.Ok(new { success = true, message = "Quantity updated", data = productExist });
                }
                else
                {
                    CartEntity cartTable = new CartEntity();
                    cartTable.ProductID = model.ProductID;
                    cartTable.ProductName = model.ProductName;
                    cartTable.Quantity = model.Quantity;
                    cartTable.Price = model.Price;
                    cartTable.Image = upLoadResult.SecureUrl.AbsoluteUri;

                    _userContext.CartTable.Add(cartTable);
                    _userContext.SaveChanges();

                    return this.Ok(new { success = true, message = "product added successfully", data = cartTable });

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
