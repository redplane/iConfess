using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iConfess.Admin.ViewModels.ApiCategory;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Services;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/category")]
    public class ApiCategoryController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ConfessionDbContext confessionDbContext;

        public ApiCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Initiate a category with specific information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage InitiateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                //Initiate new category
                var category = new Category();
                category.CreatorIndex = categoryViewModel.CreatorIndex;
                category.Created = categoryViewModel.Created;
                category.Name = categoryViewModel.Name;
                category.LastModified = categoryViewModel.LastModified;

                //Add category record
                confessionDbContext.Categories.Add(category);
                //Save change
                confessionDbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, category);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Error occured while executing category");
            }
        }

        /// <summary>
        /// Update category information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public HttpResponseMessage UpdateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.Created, ModelState);

                //Find category record
                var category = confessionDbContext.Categories.FirstOrDefault(i => i.Id.Equals(categoryViewModel.Id));

                category.Created = categoryViewModel.Created;
                category.CreatorIndex = categoryViewModel.CreatorIndex;
                category.Name = categoryViewModel.Name;
                category.LastModified = categoryViewModel.LastModified;

                //Update category record
                confessionDbContext.Entry(category).State = EntityState.Modified;
                //Save change
                confessionDbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, category);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Error occured while executing category");
            }
        }

        /// <summary>
        /// Delete a specific category.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public HttpResponseMessage DeleteCategories(int id)
        {
            try
            {
                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                //find category record
                var category = confessionDbContext.Categories.FirstOrDefault(i => i.Id.Equals(id));
                //Delete record
                confessionDbContext.Categories.Remove(category);
                //SaveChange
                confessionDbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, category);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Error occured while executing category");
            }
        }

        /// <summary>
        /// Filter categories by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpGet]
        public HttpResponseMessage FindCategories()
        {
            try
            {
                var categories = confessionDbContext.Categories.ToList();
                if (categories.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, categories);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "show message");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Error occured while executing category");
            }
        }
    }
}