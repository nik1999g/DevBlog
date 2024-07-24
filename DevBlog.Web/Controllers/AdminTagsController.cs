using DevBlog.Web.Data;
using DevBlog.Web.Models.Domain;
using DevBlog.Web.Models.ViewModels;
using DevBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DevBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

             await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }


      
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }


       
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }


            return View(null);
        }

      
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {

            //Converting it into domain model
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

              var updatedTag = await tagRepository.UpdateAsync(tag);

              if(updatedTag != null)
            {
               //show success notification 
            }
              else
            {
                //show error notification
            }
 

            //show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }
 
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
           var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id); 

            if(deletedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
